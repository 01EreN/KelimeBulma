namespace OyunDeneme1
{
    partial class Ayarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ayarlar));
            this.Kelime_textBox1 = new System.Windows.Forms.TextBox();
            this.Anlami_textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Gonder_button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.id_textBox3 = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // Kelime_textBox1
            // 
            this.Kelime_textBox1.Location = new System.Drawing.Point(6, 93);
            this.Kelime_textBox1.Multiline = true;
            this.Kelime_textBox1.Name = "Kelime_textBox1";
            this.Kelime_textBox1.Size = new System.Drawing.Size(250, 32);
            this.Kelime_textBox1.TabIndex = 0;
            // 
            // Anlami_textBox2
            // 
            this.Anlami_textBox2.Location = new System.Drawing.Point(6, 143);
            this.Anlami_textBox2.Multiline = true;
            this.Anlami_textBox2.Name = "Anlami_textBox2";
            this.Anlami_textBox2.Size = new System.Drawing.Size(250, 126);
            this.Anlami_textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(62, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kelime Ekle ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(3, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kelime :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(3, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bu kelimeyi anlatan ip ucu :";
            // 
            // Gonder_button1
            // 
            this.Gonder_button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Gonder_button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Gonder_button1.Location = new System.Drawing.Point(6, 275);
            this.Gonder_button1.Name = "Gonder_button1";
            this.Gonder_button1.Size = new System.Drawing.Size(75, 23);
            this.Gonder_button1.TabIndex = 5;
            this.Gonder_button1.Text = "Gönder";
            this.Gonder_button1.UseVisualStyleBackColor = false;
            this.Gonder_button1.Click += new System.EventHandler(this.Gonder_button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.id_textBox3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Gonder_button1);
            this.panel1.Controls.Add(this.Kelime_textBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Anlami_textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(477, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 300);
            this.panel1.TabIndex = 6;
            // 
            // id_textBox3
            // 
            this.id_textBox3.Enabled = false;
            this.id_textBox3.Location = new System.Drawing.Point(6, 42);
            this.id_textBox3.Multiline = true;
            this.id_textBox3.Name = "id_textBox3";
            this.id_textBox3.Size = new System.Drawing.Size(250, 32);
            this.id_textBox3.TabIndex = 6;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(696, 477);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(43, 33);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 14;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // Ayarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(737, 511);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(753, 550);
            this.MinimumSize = new System.Drawing.Size(753, 550);
            this.Name = "Ayarlar";
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.Ayarlar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Kelime_textBox1;
        private System.Windows.Forms.TextBox Anlami_textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Gonder_button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox id_textBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}