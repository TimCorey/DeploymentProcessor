namespace DeploymentWinFormUI
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.deploySitesButton = new System.Windows.Forms.Button();
            this.restoreSitesButton = new System.Windows.Forms.Button();
            this.backUpSitesButton = new System.Windows.Forms.Button();
            this.stepOneBox = new System.Windows.Forms.GroupBox();
            this.removeSiteButton = new System.Windows.Forms.Button();
            this.selectSiteButton = new System.Windows.Forms.Button();
            this.selectedSitesLabel = new System.Windows.Forms.Label();
            this.selectedSitesListBox = new System.Windows.Forms.ListBox();
            this.availableSitesLabel = new System.Windows.Forms.Label();
            this.availableSitesListBox = new System.Windows.Forms.ListBox();
            this.stepTwoBox = new System.Windows.Forms.GroupBox();
            this.dashboardStatusStrip = new System.Windows.Forms.StatusStrip();
            this.dashboardStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dashboardProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.stepOneBox.SuspendLayout();
            this.stepTwoBox.SuspendLayout();
            this.dashboardStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // deploySitesButton
            // 
            this.deploySitesButton.BackColor = System.Drawing.Color.White;
            this.deploySitesButton.Enabled = false;
            this.deploySitesButton.Location = new System.Drawing.Point(10, 47);
            this.deploySitesButton.Margin = new System.Windows.Forms.Padding(7);
            this.deploySitesButton.Name = "deploySitesButton";
            this.deploySitesButton.Size = new System.Drawing.Size(273, 71);
            this.deploySitesButton.TabIndex = 1;
            this.deploySitesButton.Text = "Deploy Site(s)";
            this.deploySitesButton.UseVisualStyleBackColor = false;
            this.deploySitesButton.Click += new System.EventHandler(this.deploySitesButton_Click);
            // 
            // restoreSitesButton
            // 
            this.restoreSitesButton.BackColor = System.Drawing.Color.White;
            this.restoreSitesButton.Enabled = false;
            this.restoreSitesButton.Location = new System.Drawing.Point(10, 132);
            this.restoreSitesButton.Margin = new System.Windows.Forms.Padding(7);
            this.restoreSitesButton.Name = "restoreSitesButton";
            this.restoreSitesButton.Size = new System.Drawing.Size(273, 71);
            this.restoreSitesButton.TabIndex = 3;
            this.restoreSitesButton.Text = "Restore Site(s)";
            this.restoreSitesButton.UseVisualStyleBackColor = false;
            this.restoreSitesButton.Click += new System.EventHandler(this.restoreSitesButton_Click);
            // 
            // backUpSitesButton
            // 
            this.backUpSitesButton.BackColor = System.Drawing.Color.White;
            this.backUpSitesButton.Enabled = false;
            this.backUpSitesButton.Location = new System.Drawing.Point(10, 217);
            this.backUpSitesButton.Margin = new System.Windows.Forms.Padding(7);
            this.backUpSitesButton.Name = "backUpSitesButton";
            this.backUpSitesButton.Size = new System.Drawing.Size(273, 71);
            this.backUpSitesButton.TabIndex = 4;
            this.backUpSitesButton.Text = "Back Up Site(s)";
            this.backUpSitesButton.UseVisualStyleBackColor = false;
            this.backUpSitesButton.Click += new System.EventHandler(this.backUpSitesButton_Click);
            // 
            // stepOneBox
            // 
            this.stepOneBox.Controls.Add(this.removeSiteButton);
            this.stepOneBox.Controls.Add(this.selectSiteButton);
            this.stepOneBox.Controls.Add(this.selectedSitesLabel);
            this.stepOneBox.Controls.Add(this.selectedSitesListBox);
            this.stepOneBox.Controls.Add(this.availableSitesLabel);
            this.stepOneBox.Controls.Add(this.availableSitesListBox);
            this.stepOneBox.Location = new System.Drawing.Point(40, 54);
            this.stepOneBox.Name = "stepOneBox";
            this.stepOneBox.Size = new System.Drawing.Size(558, 314);
            this.stepOneBox.TabIndex = 5;
            this.stepOneBox.TabStop = false;
            this.stepOneBox.Text = "Step 1 - Select Sites";
            // 
            // removeSiteButton
            // 
            this.removeSiteButton.BackColor = System.Drawing.Color.White;
            this.removeSiteButton.Location = new System.Drawing.Point(249, 198);
            this.removeSiteButton.Name = "removeSiteButton";
            this.removeSiteButton.Size = new System.Drawing.Size(61, 43);
            this.removeSiteButton.TabIndex = 5;
            this.removeSiteButton.Text = "<-";
            this.removeSiteButton.UseVisualStyleBackColor = false;
            this.removeSiteButton.Click += new System.EventHandler(this.removeSiteButton_Click);
            // 
            // selectSiteButton
            // 
            this.selectSiteButton.BackColor = System.Drawing.Color.White;
            this.selectSiteButton.Location = new System.Drawing.Point(249, 149);
            this.selectSiteButton.Name = "selectSiteButton";
            this.selectSiteButton.Size = new System.Drawing.Size(61, 43);
            this.selectSiteButton.TabIndex = 4;
            this.selectSiteButton.Text = "->";
            this.selectSiteButton.UseVisualStyleBackColor = false;
            this.selectSiteButton.Click += new System.EventHandler(this.selectSiteButton_Click);
            // 
            // selectedSitesLabel
            // 
            this.selectedSitesLabel.AutoSize = true;
            this.selectedSitesLabel.Location = new System.Drawing.Point(320, 66);
            this.selectedSitesLabel.Name = "selectedSitesLabel";
            this.selectedSitesLabel.Size = new System.Drawing.Size(155, 32);
            this.selectedSitesLabel.TabIndex = 3;
            this.selectedSitesLabel.Text = "Selected Sites";
            // 
            // selectedSitesListBox
            // 
            this.selectedSitesListBox.FormattingEnabled = true;
            this.selectedSitesListBox.ItemHeight = 32;
            this.selectedSitesListBox.Location = new System.Drawing.Point(326, 101);
            this.selectedSitesListBox.Name = "selectedSitesListBox";
            this.selectedSitesListBox.Size = new System.Drawing.Size(216, 196);
            this.selectedSitesListBox.TabIndex = 2;
            // 
            // availableSitesLabel
            // 
            this.availableSitesLabel.AutoSize = true;
            this.availableSitesLabel.Location = new System.Drawing.Point(10, 66);
            this.availableSitesLabel.Name = "availableSitesLabel";
            this.availableSitesLabel.Size = new System.Drawing.Size(159, 32);
            this.availableSitesLabel.TabIndex = 1;
            this.availableSitesLabel.Text = "Available Sites";
            // 
            // availableSitesListBox
            // 
            this.availableSitesListBox.FormattingEnabled = true;
            this.availableSitesListBox.ItemHeight = 32;
            this.availableSitesListBox.Location = new System.Drawing.Point(16, 101);
            this.availableSitesListBox.Name = "availableSitesListBox";
            this.availableSitesListBox.Size = new System.Drawing.Size(216, 196);
            this.availableSitesListBox.TabIndex = 0;
            // 
            // stepTwoBox
            // 
            this.stepTwoBox.Controls.Add(this.deploySitesButton);
            this.stepTwoBox.Controls.Add(this.restoreSitesButton);
            this.stepTwoBox.Controls.Add(this.backUpSitesButton);
            this.stepTwoBox.Location = new System.Drawing.Point(630, 54);
            this.stepTwoBox.Name = "stepTwoBox";
            this.stepTwoBox.Size = new System.Drawing.Size(304, 314);
            this.stepTwoBox.TabIndex = 6;
            this.stepTwoBox.TabStop = false;
            this.stepTwoBox.Text = "Step 2 - Choose Action";
            // 
            // dashboardStatusStrip
            // 
            this.dashboardStatusStrip.BackColor = System.Drawing.Color.White;
            this.dashboardStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardStatus,
            this.dashboardProgress});
            this.dashboardStatusStrip.Location = new System.Drawing.Point(0, 394);
            this.dashboardStatusStrip.Name = "dashboardStatusStrip";
            this.dashboardStatusStrip.Size = new System.Drawing.Size(982, 22);
            this.dashboardStatusStrip.TabIndex = 7;
            this.dashboardStatusStrip.Text = "statusStrip1";
            // 
            // dashboardStatus
            // 
            this.dashboardStatus.Margin = new System.Windows.Forms.Padding(10, 3, 20, 2);
            this.dashboardStatus.Name = "dashboardStatus";
            this.dashboardStatus.Size = new System.Drawing.Size(39, 17);
            this.dashboardStatus.Text = "Ready";
            // 
            // dashboardProgress
            // 
            this.dashboardProgress.Name = "dashboardProgress";
            this.dashboardProgress.Size = new System.Drawing.Size(200, 16);
            this.dashboardProgress.Visible = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(982, 416);
            this.Controls.Add(this.dashboardStatusStrip);
            this.Controls.Add(this.stepTwoBox);
            this.Controls.Add(this.stepOneBox);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Dashboard";
            this.Text = "Deployment Dashboard";
            this.stepOneBox.ResumeLayout(false);
            this.stepOneBox.PerformLayout();
            this.stepTwoBox.ResumeLayout(false);
            this.dashboardStatusStrip.ResumeLayout(false);
            this.dashboardStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button deploySitesButton;
        private System.Windows.Forms.Button restoreSitesButton;
        private System.Windows.Forms.Button backUpSitesButton;
        private System.Windows.Forms.GroupBox stepOneBox;
        private System.Windows.Forms.Button removeSiteButton;
        private System.Windows.Forms.Button selectSiteButton;
        private System.Windows.Forms.Label selectedSitesLabel;
        private System.Windows.Forms.ListBox selectedSitesListBox;
        private System.Windows.Forms.Label availableSitesLabel;
        private System.Windows.Forms.ListBox availableSitesListBox;
        private System.Windows.Forms.GroupBox stepTwoBox;
        private System.Windows.Forms.StatusStrip dashboardStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel dashboardStatus;
        private System.Windows.Forms.ToolStripProgressBar dashboardProgress;
    }
}

