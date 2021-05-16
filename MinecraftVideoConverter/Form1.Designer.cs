namespace MinecraftVideoConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VideoSelect = new System.Windows.Forms.Button();
            this.PreviewPicture = new System.Windows.Forms.PictureBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StartConversion = new System.Windows.Forms.Button();
            this.WorldSelection = new System.Windows.Forms.ComboBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.WorldPath = new System.Windows.Forms.Label();
            this.VideoPath = new System.Windows.Forms.Label();
            this.OpenWorldButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoSelect
            // 
            this.VideoSelect.Location = new System.Drawing.Point(64, 12);
            this.VideoSelect.Name = "VideoSelect";
            this.VideoSelect.Size = new System.Drawing.Size(120, 23);
            this.VideoSelect.TabIndex = 0;
            this.VideoSelect.Text = "Select Video";
            this.VideoSelect.UseVisualStyleBackColor = true;
            this.VideoSelect.Click += new System.EventHandler(this.SelectVideo_Click);
            // 
            // PreviewPicture
            // 
            this.PreviewPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PreviewPicture.Location = new System.Drawing.Point(12, 82);
            this.PreviewPicture.Name = "PreviewPicture";
            this.PreviewPicture.Size = new System.Drawing.Size(640, 384);
            this.PreviewPicture.TabIndex = 1;
            this.PreviewPicture.TabStop = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 472);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(640, 23);
            this.ProgressBar.TabIndex = 2;
            // 
            // StartConversion
            // 
            this.StartConversion.Location = new System.Drawing.Point(532, 501);
            this.StartConversion.Name = "StartConversion";
            this.StartConversion.Size = new System.Drawing.Size(120, 23);
            this.StartConversion.TabIndex = 4;
            this.StartConversion.Text = "Convert";
            this.StartConversion.UseVisualStyleBackColor = true;
            this.StartConversion.Click += new System.EventHandler(this.Convert_Click);
            // 
            // WorldSelection
            // 
            this.WorldSelection.DisplayMember = "name";
            this.WorldSelection.FormattingEnabled = true;
            this.WorldSelection.Location = new System.Drawing.Point(290, 12);
            this.WorldSelection.Name = "WorldSelection";
            this.WorldSelection.Size = new System.Drawing.Size(156, 23);
            this.WorldSelection.TabIndex = 5;
            this.WorldSelection.SelectedIndexChanged += new System.EventHandler(this.Worlds_SelectedIndexChanged);
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(12, 498);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(22, 15);
            this.ProgressLabel.TabIndex = 6;
            this.ProgressLabel.Text = "---";
            // 
            // WorldPath
            // 
            this.WorldPath.AutoSize = true;
            this.WorldPath.Location = new System.Drawing.Point(12, 61);
            this.WorldPath.Name = "WorldPath";
            this.WorldPath.Size = new System.Drawing.Size(102, 15);
            this.WorldPath.TabIndex = 7;
            this.WorldPath.Text = "No world selected";
            // 
            // VideoPath
            // 
            this.VideoPath.AutoSize = true;
            this.VideoPath.Location = new System.Drawing.Point(12, 42);
            this.VideoPath.Name = "VideoPath";
            this.VideoPath.Size = new System.Drawing.Size(101, 15);
            this.VideoPath.TabIndex = 8;
            this.VideoPath.Text = "No video selected";
            // 
            // OpenWorldButton
            // 
            this.OpenWorldButton.Location = new System.Drawing.Point(504, 12);
            this.OpenWorldButton.Name = "OpenWorldButton";
            this.OpenWorldButton.Size = new System.Drawing.Size(148, 23);
            this.OpenWorldButton.TabIndex = 9;
            this.OpenWorldButton.Text = "Select World manually";
            this.OpenWorldButton.UseVisualStyleBackColor = true;
            this.OpenWorldButton.Click += new System.EventHandler(this.OpenWorldButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "World : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Video : ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(477, 501);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(49, 23);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Target frame rate :";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(664, 528);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenWorldButton);
            this.Controls.Add(this.VideoPath);
            this.Controls.Add(this.WorldPath);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.WorldSelection);
            this.Controls.Add(this.StartConversion);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.PreviewPicture);
            this.Controls.Add(this.VideoSelect);
            this.Name = "Form1";
            this.Text = "Minecraft Video Converter";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button VideoSelect;
        private System.Windows.Forms.PictureBox PreviewPicture;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button StartConversion;
        private System.Windows.Forms.ComboBox WorldSelection;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Label WorldPath;
        private System.Windows.Forms.Label VideoPath;
        private System.Windows.Forms.Button OpenWorldButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
    }
}

