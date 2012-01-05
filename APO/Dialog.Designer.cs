namespace APO
{
    partial class myCustomDialog
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
            this.labelDesc = new System.Windows.Forms.Label();
            this.myAcceptButton = new System.Windows.Forms.Button();
            this.myCancelButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelDesc2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(13, 13);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(58, 13);
            this.labelDesc.TabIndex = 0;
            this.labelDesc.Text = "description";
            // 
            // myAcceptButton
            // 
            this.myAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.myAcceptButton.Location = new System.Drawing.Point(12, 105);
            this.myAcceptButton.Name = "myAcceptButton";
            this.myAcceptButton.Size = new System.Drawing.Size(75, 23);
            this.myAcceptButton.TabIndex = 1;
            this.myAcceptButton.Text = "OK";
            this.myAcceptButton.UseVisualStyleBackColor = true;
            this.myAcceptButton.Click += new System.EventHandler(this.myAcceptButton_Click);
            // 
            // myCancelButton
            // 
            this.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.myCancelButton.Location = new System.Drawing.Point(135, 105);
            this.myCancelButton.Name = "myCancelButton";
            this.myCancelButton.Size = new System.Drawing.Size(75, 23);
            this.myCancelButton.TabIndex = 2;
            this.myCancelButton.Text = "Anuluj";
            this.myCancelButton.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(13, 30);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(197, 20);
            this.textBox.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Visible = false;
            // 
            // labelDesc2
            // 
            this.labelDesc2.AutoSize = true;
            this.labelDesc2.Location = new System.Drawing.Point(13, 57);
            this.labelDesc2.Name = "labelDesc2";
            this.labelDesc2.Size = new System.Drawing.Size(58, 13);
            this.labelDesc2.TabIndex = 4;
            this.labelDesc2.Text = "description";
            this.labelDesc2.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(198, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 30);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(198, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Visible = false;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 58);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(198, 45);
            this.trackBar2.TabIndex = 8;
            this.trackBar2.Visible = false;
            // 
            // myCustomDialog
            // 
            this.AcceptButton = this.myAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.myCancelButton;
            this.ClientSize = new System.Drawing.Size(222, 138);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelDesc2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.myCancelButton);
            this.Controls.Add(this.myAcceptButton);
            this.Controls.Add(this.labelDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "myCustomDialog";
            this.Text = "Dialog2";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Button myAcceptButton;
        private System.Windows.Forms.Button myCancelButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelDesc2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
    }
}