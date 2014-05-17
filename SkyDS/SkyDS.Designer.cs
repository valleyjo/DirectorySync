namespace SkyDS
{
    partial class SkyDS
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
            this.beginWatch = new System.Windows.Forms.Button();
            this.endWatch = new System.Windows.Forms.Button();
            this.choseFilesToSync = new System.Windows.Forms.Button();
            this.activityLabel = new System.Windows.Forms.Label();
            this.activityArea = new System.Windows.Forms.TextBox();
            this.remoteMachineName = new System.Windows.Forms.TextBox();
            this.remoteMachineLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // beginWatch
            // 
            this.beginWatch.Location = new System.Drawing.Point(395, 11);
            this.beginWatch.Name = "beginWatch";
            this.beginWatch.Size = new System.Drawing.Size(104, 24);
            this.beginWatch.TabIndex = 1;
            this.beginWatch.Text = "Sync Files";
            this.beginWatch.UseVisualStyleBackColor = true;
            this.beginWatch.Click += new System.EventHandler(this.beginWatch_Click);
            // 
            // endWatch
            // 
            this.endWatch.Location = new System.Drawing.Point(505, 11);
            this.endWatch.Name = "endWatch";
            this.endWatch.Size = new System.Drawing.Size(104, 24);
            this.endWatch.TabIndex = 2;
            this.endWatch.Text = "Stop Syncing Files";
            this.endWatch.UseVisualStyleBackColor = true;
            this.endWatch.Click += new System.EventHandler(this.endWatch_Click);
            // 
            // choseFilesToSync
            // 
            this.choseFilesToSync.Location = new System.Drawing.Point(266, 12);
            this.choseFilesToSync.Name = "choseFilesToSync";
            this.choseFilesToSync.Size = new System.Drawing.Size(123, 24);
            this.choseFilesToSync.TabIndex = 3;
            this.choseFilesToSync.Text = "Choose Files to Sync";
            this.choseFilesToSync.UseVisualStyleBackColor = true;
            this.choseFilesToSync.Click += new System.EventHandler(this.chooseFilesToSync_Click);
            // 
            // activityLabel
            // 
            this.activityLabel.AutoSize = true;
            this.activityLabel.Location = new System.Drawing.Point(9, 41);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(44, 13);
            this.activityLabel.TabIndex = 4;
            this.activityLabel.Text = "Activity:";
            // 
            // activityArea
            // 
            this.activityArea.Location = new System.Drawing.Point(12, 57);
            this.activityArea.Multiline = true;
            this.activityArea.Name = "activityArea";
            this.activityArea.ReadOnly = true;
            this.activityArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.activityArea.ShortcutsEnabled = false;
            this.activityArea.Size = new System.Drawing.Size(597, 185);
            this.activityArea.TabIndex = 5;
            this.activityArea.Text = "Welcome to Sky Directory Sync!\r\n\r\nThis utility will keep your local build directo" +
    "ry in sync with a virtual machine that is hosting skyweb";
            // 
            // remoteMachineName
            // 
            this.remoteMachineName.CausesValidation = false;
            this.remoteMachineName.Location = new System.Drawing.Point(140, 15);
            this.remoteMachineName.Name = "remoteMachineName";
            this.remoteMachineName.Size = new System.Drawing.Size(120, 20);
            this.remoteMachineName.TabIndex = 6;
            this.remoteMachineName.TextChanged += new System.EventHandler(this.remoteMachineName_TextChanged);
            // 
            // remoteMachineLabel
            // 
            this.remoteMachineLabel.AutoSize = true;
            this.remoteMachineLabel.Location = new System.Drawing.Point(12, 18);
            this.remoteMachineLabel.Name = "remoteMachineLabel";
            this.remoteMachineLabel.Size = new System.Drawing.Size(122, 13);
            this.remoteMachineLabel.TabIndex = 7;
            this.remoteMachineLabel.Text = "Remote Machine Name:";
            // 
            // SkyDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 254);
            this.Controls.Add(this.remoteMachineLabel);
            this.Controls.Add(this.remoteMachineName);
            this.Controls.Add(this.activityArea);
            this.Controls.Add(this.activityLabel);
            this.Controls.Add(this.choseFilesToSync);
            this.Controls.Add(this.endWatch);
            this.Controls.Add(this.beginWatch);
            this.Name = "SkyDS";
            this.Text = "Sky Directory Sync";
            this.Load += new System.EventHandler(this.SkyDSForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button beginWatch;
        private System.Windows.Forms.Button endWatch;
        private System.Windows.Forms.Button choseFilesToSync;
        private System.Windows.Forms.Label activityLabel;
        private System.Windows.Forms.TextBox activityArea;
        private System.Windows.Forms.TextBox remoteMachineName;
        private System.Windows.Forms.Label remoteMachineLabel;
    }
}
