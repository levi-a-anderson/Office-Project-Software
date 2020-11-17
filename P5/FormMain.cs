﻿using Builder;
using System.Windows.Forms;

namespace P5
{
    public partial class FormMain : Form
    {
        private AppUser _CurrentAppUser = new AppUser();
        private int currentProjectID = -1;
        public FormMain()
        {
            InitializeComponent();
        }

        private void preferencesCreateProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormCreateProject form = new FormCreateProject();
            form.ShowDialog();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            this.CenterToScreen();
            // Force the user to login successfully or abort the application
            DialogResult isOK = DialogResult.OK;
            while (!_CurrentAppUser.IsAuthenticated && isOK == DialogResult.OK)
            {
                FormLogin login = new FormLogin();
                isOK = login.ShowDialog();
                _CurrentAppUser = login._CurrentAppUser;
                login.Dispose();
            }
            if (isOK != DialogResult.OK)
            {
                Close();
                return;
            }
            this.Text = "Main - No Project Selected";
            while (selectAProject() == "")
            {
                DialogResult result = MessageBox.Show("A project must be selected.", "Attention", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    Close();
                    return;
                }
            }
        }

        private void preferencesSelectProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            selectAProject();
        }

        private string selectAProject()
        {
            string selectedProject = "";
            FormSelectProject form = new FormSelectProject();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                FakePreferenceRepository preferenceRepository = new FakePreferenceRepository();
                preferenceRepository.SetPreference(_CurrentAppUser.UserName,
                                                   FakePreferenceRepository.PREFERENCE_PROJECT_NAME,
                                                   form._SelectedProjectName);
                int selectedProjectId = currentProjectID = form._SelectedProjectId;
                preferenceRepository.SetPreference(_CurrentAppUser.UserName,
                                                   FakePreferenceRepository.PREFERENCE_PROJECT_ID,
                                                   selectedProjectId.ToString());
                this.Text = "Main - " + form._SelectedProjectName;
                selectedProject = form._SelectedProjectName;
            }
            form.Dispose();
            return selectedProject;
        }

        private void preferencesModifyProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormModifyProject form = new FormModifyProject(_CurrentAppUser);
            form.ShowDialog();
            form.Dispose();
        }

        private void preferencesRemoveProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormRemoveProject form = new FormRemoveProject(_CurrentAppUser);
            form.ShowDialog();
            form.Dispose();
        }

        private void issuesDashboardToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormSelectProject selectProjectForm = new FormSelectProject(); // Just to get the curent project id. 
            FormDashboard dashboardForm = new FormDashboard(1);
            dashboardForm.ShowDialog();
            dashboardForm.Dispose();
        }

        private void featureToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void createToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormCreateFeature form = new FormCreateFeature(currentProjectID);
            form.ShowDialog();
            form.Dispose();
        }

        private void createToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            FormCreateRequirement form = new FormCreateRequirement(currentProjectID);
            form.ShowDialog();
            form.Dispose();
        }

        private void modifyToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
         
        }

        private void modifyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormSelectFeature selectAFeature = new FormSelectFeature(currentProjectID);
            selectAFeature.ShowDialog();

            FormModifyFeature modifyAFeature = new FormModifyFeature(selectAFeature.featureId);
            modifyAFeature.ShowDialog();

            selectAFeature.Dispose();
            modifyAFeature.Dispose();
        }

        private void removeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }
    }
}
