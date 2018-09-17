namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.load = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.save = new System.Windows.Forms.Button();
            this.trackBarHUE = new System.Windows.Forms.TrackBar();
            this.trackBarBr = new System.Windows.Forms.TrackBar();
            this.trackBarSat = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHUE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSat)).BeginInit();
            this.SuspendLayout();
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(13, 13);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 23);
            this.load.TabIndex = 0;
            this.load.Text = "load";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(425, 383);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(111, 12);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 2;
            this.save.Text = "save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // trackBarHUE
            // 
            this.trackBarHUE.LargeChange = 20;
            this.trackBarHUE.Location = new System.Drawing.Point(458, 42);
            this.trackBarHUE.Maximum = 255;
            this.trackBarHUE.Name = "trackBarHUE";
            this.trackBarHUE.Size = new System.Drawing.Size(341, 45);
            this.trackBarHUE.TabIndex = 3;
            this.trackBarHUE.ValueChanged += new System.EventHandler(this.trackBarHUE_ValueChanged);
            // 
            // trackBarBr
            // 
            this.trackBarBr.Location = new System.Drawing.Point(458, 82);
            this.trackBarBr.Maximum = 255;
            this.trackBarBr.Name = "trackBarBr";
            this.trackBarBr.Size = new System.Drawing.Size(341, 45);
            this.trackBarBr.TabIndex = 4;
            this.trackBarBr.ValueChanged += new System.EventHandler(this.trackBarHUE_ValueChanged);
            // 
            // trackBarSat
            // 
            this.trackBarSat.LargeChange = 20;
            this.trackBarSat.Location = new System.Drawing.Point(458, 133);
            this.trackBarSat.Maximum = 255;
            this.trackBarSat.Name = "trackBarSat";
            this.trackBarSat.Size = new System.Drawing.Size(341, 45);
            this.trackBarSat.TabIndex = 5;
            this.trackBarSat.ValueChanged += new System.EventHandler(this.trackBarHUE_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trackBarSat);
            this.Controls.Add(this.trackBarBr);
            this.Controls.Add(this.trackBarHUE);
            this.Controls.Add(this.save);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.load);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHUE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button load;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TrackBar trackBarHUE;
        private System.Windows.Forms.TrackBar trackBarBr;
        private System.Windows.Forms.TrackBar trackBarSat;
    }
}

