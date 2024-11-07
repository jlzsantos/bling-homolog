namespace AppHomolog
{
    partial class frmMain
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
            txtApiBaseUrl = new TextBox();
            txtReq1 = new TextBox();
            txtReq2 = new TextBox();
            txtReq3 = new TextBox();
            txtReq4 = new TextBox();
            txtReq5 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            btnStart = new Button();
            btnClose = new Button();
            txtToken = new TextBox();
            txtRefreshToken = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txtClientId = new TextBox();
            txtClientSecret = new TextBox();
            label10 = new Label();
            label11 = new Label();
            txtBaseUrl = new TextBox();
            label12 = new Label();
            rtbResume = new RichTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtApiBaseUrl
            // 
            txtApiBaseUrl.Font = new Font("Segoe UI", 9F);
            txtApiBaseUrl.ForeColor = Color.Green;
            txtApiBaseUrl.Location = new Point(12, 92);
            txtApiBaseUrl.Name = "txtApiBaseUrl";
            txtApiBaseUrl.Size = new Size(322, 23);
            txtApiBaseUrl.TabIndex = 0;
            txtApiBaseUrl.Text = "https://api.bling.com.br/Api/v3/homologacao";
            // 
            // txtReq1
            // 
            txtReq1.Location = new Point(12, 140);
            txtReq1.Name = "txtReq1";
            txtReq1.Size = new Size(322, 23);
            txtReq1.TabIndex = 1;
            txtReq1.Text = "/produtos";
            // 
            // txtReq2
            // 
            txtReq2.Location = new Point(12, 190);
            txtReq2.Name = "txtReq2";
            txtReq2.Size = new Size(322, 23);
            txtReq2.TabIndex = 2;
            txtReq2.Text = "/produtos";
            // 
            // txtReq3
            // 
            txtReq3.Location = new Point(12, 242);
            txtReq3.Name = "txtReq3";
            txtReq3.Size = new Size(322, 23);
            txtReq3.TabIndex = 3;
            txtReq3.Text = "/produtos/{0}";
            // 
            // txtReq4
            // 
            txtReq4.Location = new Point(12, 293);
            txtReq4.Name = "txtReq4";
            txtReq4.Size = new Size(322, 23);
            txtReq4.TabIndex = 4;
            txtReq4.Text = "/produtos/{0}/situacoes";
            // 
            // txtReq5
            // 
            txtReq5.Location = new Point(12, 342);
            txtReq5.Name = "txtReq5";
            txtReq5.Size = new Size(322, 23);
            txtReq5.TabIndex = 5;
            txtReq5.Text = "/produtos/{0}";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 74);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 6;
            label1.Text = "URL base API";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 122);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 7;
            label2.Text = "[GET] Request 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 172);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 8;
            label3.Text = "[POST] Request 2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 224);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 9;
            label4.Text = "[PUT] Request 3";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 275);
            label5.Name = "label5";
            label5.Size = new Size(104, 15);
            label5.TabIndex = 10;
            label5.Text = "[PATCH] Request 4";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 324);
            label6.Name = "label6";
            label6.Size = new Size(107, 15);
            label6.TabIndex = 11;
            label6.Text = "[DELETE] Request 5";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnStart);
            panel1.Controls.Add(btnClose);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 392);
            panel1.Name = "panel1";
            panel1.Size = new Size(803, 49);
            panel1.TabIndex = 12;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(538, 13);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(163, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Iniciar homologação";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(707, 13);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 0;
            btnClose.Text = "Fechar";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtToken
            // 
            txtToken.Location = new Point(357, 142);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(426, 23);
            txtToken.TabIndex = 13;
            // 
            // txtRefreshToken
            // 
            txtRefreshToken.Location = new Point(357, 190);
            txtRefreshToken.Name = "txtRefreshToken";
            txtRefreshToken.Size = new Size(426, 23);
            txtRefreshToken.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(357, 124);
            label7.Name = "label7";
            label7.Size = new Size(77, 15);
            label7.TabIndex = 15;
            label7.Text = "Access Token";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(357, 172);
            label8.Name = "label8";
            label8.Size = new Size(80, 15);
            label8.TabIndex = 16;
            label8.Text = "Refresh Token";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Red;
            label9.Location = new Point(357, 224);
            label9.Name = "label9";
            label9.Size = new Size(152, 15);
            label9.TabIndex = 18;
            label9.Text = "Resultado da homologação";
            // 
            // txtClientId
            // 
            txtClientId.Location = new Point(357, 44);
            txtClientId.Name = "txtClientId";
            txtClientId.Size = new Size(426, 23);
            txtClientId.TabIndex = 19;
            // 
            // txtClientSecret
            // 
            txtClientSecret.Location = new Point(357, 92);
            txtClientSecret.Name = "txtClientSecret";
            txtClientSecret.Size = new Size(426, 23);
            txtClientSecret.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(357, 26);
            label10.Name = "label10";
            label10.Size = new Size(52, 15);
            label10.TabIndex = 21;
            label10.Text = "Client ID";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(357, 74);
            label11.Name = "label11";
            label11.Size = new Size(73, 15);
            label11.TabIndex = 22;
            label11.Text = "Client Secret";
            // 
            // txtBaseUrl
            // 
            txtBaseUrl.ForeColor = Color.Green;
            txtBaseUrl.Location = new Point(12, 44);
            txtBaseUrl.Name = "txtBaseUrl";
            txtBaseUrl.Size = new Size(322, 23);
            txtBaseUrl.TabIndex = 23;
            txtBaseUrl.Text = "https://bling.com.br/Api/v3";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 26);
            label12.Name = "label12";
            label12.Size = new Size(127, 15);
            label12.TabIndex = 24;
            label12.Text = "URL base refresh token";
            // 
            // rtbResume
            // 
            rtbResume.BorderStyle = BorderStyle.FixedSingle;
            rtbResume.Location = new Point(357, 242);
            rtbResume.Name = "rtbResume";
            rtbResume.Size = new Size(426, 123);
            rtbResume.TabIndex = 25;
            rtbResume.Text = "";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 441);
            Controls.Add(rtbResume);
            Controls.Add(label12);
            Controls.Add(txtBaseUrl);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(txtClientSecret);
            Controls.Add(txtClientId);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtRefreshToken);
            Controls.Add(txtToken);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtReq5);
            Controls.Add(txtReq4);
            Controls.Add(txtReq3);
            Controls.Add(txtReq2);
            Controls.Add(txtReq1);
            Controls.Add(txtApiBaseUrl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bling Homologação";
            Activated += frmMain_Activated;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtApiBaseUrl;
        private TextBox txtReq1;
        private TextBox txtReq2;
        private TextBox txtReq3;
        private TextBox txtReq4;
        private TextBox txtReq5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Button btnClose;
        private Button btnStart;
        private TextBox txtToken;
        private TextBox txtRefreshToken;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtClientId;
        private TextBox txtClientSecret;
        private Label label10;
        private Label label11;
        private TextBox txtBaseUrl;
        private Label label12;
        private RichTextBox rtbResume;
    }
}
