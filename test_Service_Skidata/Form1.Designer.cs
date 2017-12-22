namespace SkiData
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txBxID = new System.Windows.Forms.TextBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelServer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxServer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelCertificat = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxCertificat = new System.Windows.Forms.ToolStripTextBox();
            this.passwordTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.passwordLabel = new System.Windows.Forms.ToolStripLabel();
            this.usernameTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.usernameLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelOnline = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelOffline = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeconnect = new System.Windows.Forms.ToolStripButton();
            this.tBxResult = new System.Windows.Forms.TextBox();
            this.gBxOutput = new System.Windows.Forms.GroupBox();
            this.gBxGetData = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cBxFormats = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.gBxOutput.SuspendLayout();
            this.gBxGetData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // txBxID
            // 
            this.txBxID.Location = new System.Drawing.Point(63, 43);
            this.txBxID.Name = "txBxID";
            this.txBxID.Size = new System.Drawing.Size(206, 20);
            this.txBxID.TabIndex = 1;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(317, 38);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 2;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelServer,
            this.toolStripTextBoxServer,
            this.toolStripSeparator1,
            this.toolStripLabelCertificat,
            this.toolStripTextBoxCertificat,
            this.passwordTextBox,
            this.passwordLabel,
            this.usernameTextBox,
            this.usernameLabel,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripLabelOnline,
            this.toolStripLabelOffline,
            this.toolStripSeparator3,
            this.toolStripButtonConnect,
            this.toolStripButtonDeconnect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1279, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelServer
            // 
            this.toolStripLabelServer.Name = "toolStripLabelServer";
            this.toolStripLabelServer.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabelServer.Text = "Server";
            // 
            // toolStripTextBoxServer
            // 
            this.toolStripTextBoxServer.Name = "toolStripTextBoxServer";
            this.toolStripTextBoxServer.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelCertificat
            // 
            this.toolStripLabelCertificat.Name = "toolStripLabelCertificat";
            this.toolStripLabelCertificat.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabelCertificat.Text = "Certificat";
            // 
            // toolStripTextBoxCertificat
            // 
            this.toolStripTextBoxCertificat.Name = "toolStripTextBoxCertificat";
            this.toolStripTextBoxCertificat.Size = new System.Drawing.Size(100, 25);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ShortcutsEnabled = false;
            this.passwordTextBox.Size = new System.Drawing.Size(100, 25);
            this.passwordTextBox.Visible = false;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(60, 22);
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Visible = false;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 25);
            this.usernameTextBox.Visible = false;
            // 
            // usernameLabel
            // 
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(63, 22);
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel2.Text = "Statut : ";
            // 
            // toolStripLabelOnline
            // 
            this.toolStripLabelOnline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabelOnline.ForeColor = System.Drawing.Color.Green;
            this.toolStripLabelOnline.Name = "toolStripLabelOnline";
            this.toolStripLabelOnline.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabelOnline.Text = "En ligne";
            // 
            // toolStripLabelOffline
            // 
            this.toolStripLabelOffline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabelOffline.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabelOffline.Name = "toolStripLabelOffline";
            this.toolStripLabelOffline.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabelOffline.Text = "Hors ligne";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(56, 22);
            this.toolStripButtonConnect.Text = "Connect";
            this.toolStripButtonConnect.ToolTipText = "Connect";
            this.toolStripButtonConnect.Click += new System.EventHandler(this.toolStripButtonConnect_Click);
            // 
            // toolStripButtonDeconnect
            // 
            this.toolStripButtonDeconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDeconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeconnect.Image")));
            this.toolStripButtonDeconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeconnect.Name = "toolStripButtonDeconnect";
            this.toolStripButtonDeconnect.Size = new System.Drawing.Size(68, 22);
            this.toolStripButtonDeconnect.Text = "Deconnect";
            // 
            // tBxResult
            // 
            this.tBxResult.Location = new System.Drawing.Point(12, 25);
            this.tBxResult.Multiline = true;
            this.tBxResult.Name = "tBxResult";
            this.tBxResult.ReadOnly = true;
            this.tBxResult.Size = new System.Drawing.Size(718, 229);
            this.tBxResult.TabIndex = 5;
            // 
            // gBxOutput
            // 
            this.gBxOutput.Controls.Add(this.tBxResult);
            this.gBxOutput.Location = new System.Drawing.Point(12, 141);
            this.gBxOutput.Name = "gBxOutput";
            this.gBxOutput.Size = new System.Drawing.Size(736, 260);
            this.gBxOutput.TabIndex = 7;
            this.gBxOutput.TabStop = false;
            this.gBxOutput.Text = "Resultat";
            // 
            // gBxGetData
            // 
            this.gBxGetData.Controls.Add(this.label2);
            this.gBxGetData.Controls.Add(this.cBxFormats);
            this.gBxGetData.Controls.Add(this.txBxID);
            this.gBxGetData.Controls.Add(this.label1);
            this.gBxGetData.Controls.Add(this.btnGetData);
            this.gBxGetData.Location = new System.Drawing.Point(12, 28);
            this.gBxGetData.Name = "gBxGetData";
            this.gBxGetData.Size = new System.Drawing.Size(736, 84);
            this.gBxGetData.TabIndex = 8;
            this.gBxGetData.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Format";
            // 
            // cBxFormats
            // 
            this.cBxFormats.FormattingEnabled = true;
            this.cBxFormats.Location = new System.Drawing.Point(63, 18);
            this.cBxFormats.Name = "cBxFormats";
            this.cBxFormats.Size = new System.Drawing.Size(206, 21);
            this.cBxFormats.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 413);
            this.Controls.Add(this.gBxGetData);
            this.Controls.Add(this.gBxOutput);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gBxOutput.ResumeLayout(false);
            this.gBxOutput.PerformLayout();
            this.gBxGetData.ResumeLayout(false);
            this.gBxGetData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txBxID;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelServer;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCertificat;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCertificat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelOnline;
        private System.Windows.Forms.ToolStripLabel toolStripLabelOffline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.TextBox tBxResult;
        private System.Windows.Forms.GroupBox gBxOutput;
        private System.Windows.Forms.GroupBox gBxGetData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBxFormats;
        private System.Windows.Forms.ToolStripTextBox passwordTextBox;
        private System.Windows.Forms.ToolStripLabel passwordLabel;
        private System.Windows.Forms.ToolStripTextBox usernameTextBox;
        private System.Windows.Forms.ToolStripLabel usernameLabel;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeconnect;
    }
}

