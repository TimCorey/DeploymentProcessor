using ProcessorLibrary;
using ProcessorLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeploymentWinFormUI
{
    public partial class Dashboard : Form
    {
        Deployment deploy = new Deployment();
        Progress<ProgressReportModel> progress;
        BindingList<string> availableSites = new BindingList<string>();
        BindingList<string> selectedSites = new BindingList<string>();

        public Dashboard()
        {
            InitializeComponent();
            progress = new Progress<ProgressReportModel>();
            deploy.GetDeploymentDirectoryOptions().ForEach(x => availableSites.Add(x));

            availableSitesListBox.DataSource = availableSites;
            selectedSitesListBox.DataSource = selectedSites;

            selectedSites.ListChanged += SelectedSites_ListChanged;
        }

        private void SelectedSites_ListChanged(object sender, ListChangedEventArgs e)
        {
            bool enableButton = selectedSites.Count > 0;

            deploySitesButton.Enabled = enableButton;
            restoreSitesButton.Enabled = enableButton;
            backUpSitesButton.Enabled = enableButton;
        }

        private void ReportProgress(object sender, ProgressReportModel value)
        {
            dashboardProgress.Visible = true;
            dashboardStatus.Text = value.ActionName;
            dashboardProgress.Value = value.ProgressPercentage;
        }

        private async void deploySitesButton_Click(object sender, EventArgs e)
        {
            string waitMessage = "Please Wait...";

            if (deploySitesButton.Text == waitMessage)
            {
                return;
            }

            string originalButtonText = deploySitesButton.Text;
            deploySitesButton.Text = waitMessage;
            progress.ProgressChanged += ReportProgress;

            try
            {
                await deploy.DeploySoftwareAsync(selectedSites.ToList(), progress);
                dashboardStatus.Text = "Process Complete";
            }
            catch (Exception ex)
            {
                dashboardStatus.Text = ex.Message;
            }
            finally
            {
                dashboardProgress.Value = 0;
                dashboardProgress.Visible = false;
                deploySitesButton.Text = originalButtonText;
            }
        }

        private void selectSiteButton_Click(object sender, EventArgs e)
        {
            AddSelectedSite();
        }

        private void AddSelectedSite()
        {
            string val = (string)availableSitesListBox.SelectedItem;

            if (string.IsNullOrWhiteSpace(val))
            {
                return;
            }

            selectedSites.Add(val);
            availableSites.Remove(val);
        }

        private void RemoveSelectedSite()
        {
            string val = (string)selectedSitesListBox.SelectedItem;

            if (string.IsNullOrWhiteSpace(val))
            {
                return;
            }

            availableSites.Add(val);
            selectedSites.Remove(val);
        }

        private void removeSiteButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedSite();
        }

        private async void backUpSitesButton_Click(object sender, EventArgs e)
        {
            string waitMessage = "Please Wait...";

            if (backUpSitesButton.Text == waitMessage)
            {
                return;
            }

            string originalButtonText = backUpSitesButton.Text;
            backUpSitesButton.Text = waitMessage;
            progress.ProgressChanged += ReportProgress;

            try
            {
                await deploy.BackupSoftwareAsync(selectedSites.ToList(), progress);
                dashboardStatus.Text = "Process Complete";
            }
            catch (Exception ex)
            {
                dashboardStatus.Text = ex.Message;
            }
            finally
            {
                dashboardProgress.Value = 0;
                dashboardProgress.Visible = false;
                backUpSitesButton.Text = originalButtonText;
            }
        }

        private async void restoreSitesButton_Click(object sender, EventArgs e)
        {
            string waitMessage = "Please Wait...";

            if (restoreSitesButton.Text == waitMessage)
            {
                return;
            }

            string originalButtonText = restoreSitesButton.Text;
            restoreSitesButton.Text = waitMessage;
            progress.ProgressChanged += ReportProgress;

            try
            {
                await deploy.RollbackSoftware(selectedSites.ToList(), progress);
                dashboardStatus.Text = "Process Complete";
            }
            catch (Exception ex)
            {
                dashboardStatus.Text = ex.Message;
            }
            finally
            {
                dashboardProgress.Value = 0;
                dashboardProgress.Visible = false;
                restoreSitesButton.Text = originalButtonText;
            }
        }

        private void availableSitesListBox_DoubleClick(object sender, EventArgs e)
        {
            AddSelectedSite();
        }

        private void selectedSitesListBox_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedSite();
        }
    }
}
