using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using fNbt;
using System.IO;
using System.Text.RegularExpressions;
using System.Numerics;
using Emgu.CV.Cuda;
using Emgu.CV.Ocl;

namespace MinecraftVideoConverter {
    public partial class Form1 :Form {
        BackgroundWorker worker = new BackgroundWorker();
        String VideoFilePath = null;
        String WorldFolderPath = null;
        int TargetFrameRate = 20;

        class WorldItem {
            public string name { get; set; }
            public string path { get; set; }
        }

        public Form1() {
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(processVideo);
            worker.ProgressChanged += new ProgressChangedEventHandler(updateProgress);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(conversionFinished);

            InitializeComponent();

            //update list of minecraft worlds in save folder
            UpdateMinecraftWorlds();
        }

        private void UpdateMinecraftWorlds() {
            List<WorldItem> worlds = new List<WorldItem>();

            string minecraftSavesPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\.minecraft\\saves");
            string[] paths = Directory.GetDirectories(minecraftSavesPath);

            foreach (string dir in paths) {
                //check if leveldata and levelname can be determined
                try {
                    NbtFile level = new NbtFile(Path.Join(dir, "/level.dat"));
                    string name = level.RootTag.Get<NbtCompound>("Data").Get<NbtString>("LevelName").Value.ToString();
                    worlds.Add(new WorldItem { name = name, path = dir });
                } catch { }
            }

            WorldSelection.DataSource = worlds;
        }

        private void SelectVideo_Click(object sender, EventArgs e) {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK) {
                VideoFilePath = Openfile.FileName;
                VideoPath.Text = String.Concat("Video: \"", VideoFilePath, "\"");
            }
        }

        private void updateProgress(object sender, ProgressChangedEventArgs e) {
            ProgressLabel.Text = e.ProgressPercentage + "%   Remaining:" + Math.Round((double) e.UserState / 60000,2) +"minutes" ;
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void conversionFinished(object sender, RunWorkerCompletedEventArgs e) {
            convertResult result = (convertResult) e.Result;
            MessageBox.Show("Video conversion finished! \n" +
                "Note down the following values.\n" +
                "Video framecount: " + result.framecount + "\n"+
                "Video offset: " + result.offset);
        }

        double framerate, frames, reducedFrames;
        long elapsedTime = 0;
        double position = 0;
        int frame = 0;

        UMat singleFrame = new UMat(new Size(640, 384), DepthType.Cv32F, 3, UMat.Usage.AllocateDeviceMemory);
        private void processVideo(object sender, DoWorkEventArgs e) {

            CvInvoke.UseOpenCL = true;
            CvInvoke.UseOptimized = true;

            //determine amount of maps already present in the world
            int mapCount = 0;
            NbtFile map = null;
            try {
                map = new NbtFile(Path.Join(WorldFolderPath, "\\data\\idcounts.dat"));
                mapCount = Int32.Parse(map.RootTag.Get<NbtCompound>("data").Get<NbtInt>("map").Value.ToString())+1;
            } catch { }
            Debug.Write("MapCount:" + mapCount + "\n");

            //start videocapture
            VideoCapture video = new VideoCapture(VideoFilePath);

            //get framerate
            framerate = video.GetCaptureProperty(CapProp.Fps);
            //get framecount
            frames = video.GetCaptureProperty(CapProp.FrameCount);

            //calculate allFrames at the target framerate
            reducedFrames = Math.Floor((frames / framerate) * TargetFrameRate);

            if (map != null) {
                map.RootTag.Get<NbtCompound>("data").Get<NbtInt>("map").Value = mapCount + ((int)reducedFrames) * 15;
                map.SaveToFile(Path.Join(WorldFolderPath, "\\data\\idcounts.dat"), NbtCompression.None);
            } 

            //create Preset for map data
            NbtCompound preset = new NbtCompound("") {
                new NbtCompound("data"){
                    new NbtString("dimension", "minecraft:overworld"),
                    new NbtLong("xCenter", 128),
                    new NbtLong("zCenter", 128),
                    new NbtByte("scale", 3),
                    new NbtByte("locked", 1),
                    new NbtByte("unlimitedTracking", 0),
                    new NbtByte("trackingPosition", 0),
                    new NbtByteArray("colors")
                },
                new NbtInt("DataVersion",2584)
            };

            //create path to output folder
            string mapOutputFolder = Path.Join(WorldFolderPath, "/data");
            
            UMat ones = new UMat(1, 3, DepthType.Cv8U, 1, UMat.Usage.AllocateDeviceMemory);
            ones.SetTo(new MCvScalar(1));
            

            UMat calculation = new UMat(new Size(640, 384), DepthType.Cv32S, 3, UMat.Usage.AllocateDeviceMemory);
            UMat singleChannel = new UMat(new Size(640, 384), DepthType.Cv32S, 1, UMat.Usage.AllocateDeviceMemory);
            
            //keeps lowest value
            UMat lowestDiversion = new UMat(new Size(640, 384), DepthType.Cv32S, 1, UMat.Usage.AllocateDeviceMemory);
            //bool
            UMat lessDiversion = new UMat(new Size(640, 384), DepthType.Cv8U, 1, UMat.Usage.AllocateDeviceMemory);
            //store block value
            UMat blocks = new UMat(new Size(640, 384), DepthType.Cv8U, 1, UMat.Usage.AllocateDeviceMemory);


            while (frame < reducedFrames) {
                //calculate position in video and set to next frame
                position = frame / reducedFrames;
                video.SetCaptureProperty(CapProp.PosFrames, Math.Round(position * frames));

                var watch = System.Diagnostics.Stopwatch.StartNew();

                //get video frame
                if (!video.Read(singleFrame)) break;

                //resize to minecraft compatible resolution
                CvInvoke.Resize(singleFrame, singleFrame, new Size(640, 384));
                singleFrame.ConvertTo(singleFrame, DepthType.Cv32F);

                //display current Frame to user
                if (PreviewPicture.Image != null) PreviewPicture.Image.Dispose();
                PreviewPicture.Image = singleFrame.ToBitmap();

                lowestDiversion.SetTo(new MCvScalar(255));
                lessDiversion.SetTo(new MCvScalar(0));
                blocks.SetTo(Colors.minecraftColors[Colors.minecraftColors.Length-1]);

                for (int i = 0; i < Colors.minecraftColors.Length; i++) {

                    calculation = singleFrame - Colors.minecraftColors[i];
                    CvInvoke.Multiply(calculation, calculation, calculation);
                    CvInvoke.Transform(calculation, singleChannel, ones);

                    CvInvoke.Sqrt(singleChannel, singleChannel);
                    singleChannel.ConvertTo(singleChannel, DepthType.Cv32S);

                    CvInvoke.Compare(singleChannel,lowestDiversion, lessDiversion, CmpType.LessThan);
                
                    singleChannel.CopyTo(lowestDiversion, lessDiversion);
                
                    blocks.SetTo(new MCvScalar(i + 4), lessDiversion);
                }

                for (int y = 0; y < 3; y++) {
                    for (int x = 0; x < 5; x++) {
                        UMat output = new UMat(blocks, new Rectangle(128 * x, 128 * y, 128, 128));
                        preset.Get<NbtCompound>("data").Get<NbtByteArray>("colors").Value = output.Bytes;

                        NbtFile file = new NbtFile(preset);
                        file.SaveToFile(Path.Join(mapOutputFolder, String.Concat("map_",mapCount + (frame * 15) + (y * 5 + x),".dat")), NbtCompression.None);
                    }
                }
                
                watch.Stop();
                elapsedTime = watch.ElapsedMilliseconds;
                Debug.Write("Took:" + elapsedTime + "\n");

                System.GC.Collect();

                //send progress update to ui and console
                worker.ReportProgress((int)Math.Round(position * 100), elapsedTime * (reducedFrames - frame));
                Debug.Write(frame + "/" + reducedFrames + "-" + position + "\n");

                //increase framecount
                frame++;
            }

            worker.ReportProgress(100, 0.0);

            //pass the values to display to the user
            e.Result = new convertResult((int) frame-1, mapCount);
        }

        private struct convertResult {
            public int framecount;
            public int offset;

            public convertResult(int _framecount,int _offset) {
                framecount = _framecount;
                offset = _offset;
            }
        }

        private void Convert_Click(object sender, EventArgs e) {

            //check if a video has been selected
            if (VideoFilePath == null) {
                MessageBox.Show("Please select a Video");
                return;
            }
            //check a folder has been selected
            if (WorldFolderPath == null) {
                MessageBox.Show("Please select a World");
                return;
            }

            //disable ui elements
            WorldSelection.Enabled = false;
            VideoSelect.Enabled = false;
            OpenWorldButton.Enabled = false;
            //start conversion worker
            worker.RunWorkerAsync(argument: VideoFilePath);
        }

        private void Worlds_SelectedIndexChanged(object sender, EventArgs e) {
            //update path on world selection
            WorldFolderPath = ((WorldItem)((ComboBox)sender).SelectedItem).path;
            UpdateWorldPath();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            TargetFrameRate = (int) ((NumericUpDown)sender).Value;
        }

        private void OpenWorldButton_Click(object sender, EventArgs e) {
            //open dialog for manual world selection
            FolderBrowserDialog Openfolder = new FolderBrowserDialog();
            if (Openfolder.ShowDialog() == DialogResult.OK) {
                WorldFolderPath = Openfolder.SelectedPath;
                UpdateWorldPath();
            }
        }

        private void UpdateWorldPath() {
            //update label text
            WorldPath.Text = String.Concat("World: \"", WorldFolderPath, "\"");
        }
    }
}
