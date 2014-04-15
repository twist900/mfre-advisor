namespace WindowsFormsApplication1
{
    partial class SelectDir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDir));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dirPathTextBox = new System.Windows.Forms.TextBox();
            this.chooseDirBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(411, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select a directory to be watched by the installed service. ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dirPathTextBox
            // 
            this.dirPathTextBox.Location = new System.Drawing.Point(12, 68);
            this.dirPathTextBox.Name = "dirPathTextBox";
            this.dirPathTextBox.Size = new System.Drawing.Size(428, 22);
            this.dirPathTextBox.TabIndex = 2;
            // 
            // chooseDirBtn
            // 
            this.chooseDirBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseDirBtn.Location = new System.Drawing.Point(446, 68);
            this.chooseDirBtn.Name = "chooseDirBtn";
            this.chooseDirBtn.Size = new System.Drawing.Size(38, 23);
            this.chooseDirBtn.TabIndex = 3;
            this.chooseDirBtn.Text = "...";
            this.chooseDirBtn.UseVisualStyleBackColor = true;
            this.chooseDirBtn.Click += new System.EventHandler(this.chooseDirBtn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(409, 105);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // SelectDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 140);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.chooseDirBtn);
            this.Controls.Add(this.dirPathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectDir";
            this.Text = "Select directory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dirPathTextBox;
        private System.Windows.Forms.Button chooseDirBtn;
        private System.Windows.Forms.Button OKBtn;
    }
}

