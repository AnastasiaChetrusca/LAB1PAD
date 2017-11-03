namespace Client
{
    partial class Form1
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
            this.msgBox = new System.Windows.Forms.TextBox();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.trimiteBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listIPAddr = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InregistrareBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(8, 15);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(390, 20);
            this.msgBox.TabIndex = 0;
            // 
            // clientListBox
            // 
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.HorizontalScrollbar = true;
            this.clientListBox.Location = new System.Drawing.Point(8, 189);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.clientListBox.Size = new System.Drawing.Size(165, 186);
            this.clientListBox.TabIndex = 1;
            // 
            // trimiteBtn
            // 
            this.trimiteBtn.Location = new System.Drawing.Point(404, 12);
            this.trimiteBtn.Name = "trimiteBtn";
            this.trimiteBtn.Size = new System.Drawing.Size(75, 23);
            this.trimiteBtn.TabIndex = 2;
            this.trimiteBtn.Text = "Trimite";
            this.trimiteBtn.UseVisualStyleBackColor = true;
            this.trimiteBtn.Click += new System.EventHandler(this.trimiteBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 41);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(390, 116);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lista de Clienti";
            // 
            // listIPAddr
            // 
            this.listIPAddr.FormattingEnabled = true;
            this.listIPAddr.Items.AddRange(new object[] {
            "127.0.0.2",
            "127.0.0.3",
            "127.0.0.4",
            "127.0.0.5",
            "127.0.0.6",
            "127.0.0.7",
            "127.0.0.8",
            "127.0.0.9"});
            this.listIPAddr.Location = new System.Drawing.Point(6, 30);
            this.listIPAddr.Name = "listIPAddr";
            this.listIPAddr.Size = new System.Drawing.Size(121, 21);
            this.listIPAddr.TabIndex = 5;
            this.listIPAddr.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Alege adresa locala";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InregistrareBtn);
            this.groupBox2.Controls.Add(this.listIPAddr);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(198, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 89);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inregistrare";
            // 
            // InregistrareBtn
            // 
            this.InregistrareBtn.Location = new System.Drawing.Point(6, 57);
            this.InregistrareBtn.Name = "InregistrareBtn";
            this.InregistrareBtn.Size = new System.Drawing.Size(75, 23);
            this.InregistrareBtn.TabIndex = 9;
            this.InregistrareBtn.Text = "Inregistrare";
            this.InregistrareBtn.UseVisualStyleBackColor = true;
            this.InregistrareBtn.Click += new System.EventHandler(this.InregistrareBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 385);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.trimiteBtn);
            this.Controls.Add(this.clientListBox);
            this.Controls.Add(this.msgBox);
            this.Name = "Form1";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.ListBox clientListBox;
        private System.Windows.Forms.Button trimiteBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listIPAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button InregistrareBtn;
    }
}

