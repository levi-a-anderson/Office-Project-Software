﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P5;

namespace Builder
{
    public partial class FormModifyFeature : Form
    {
        FakeFeatureRepository featureRepository = new FakeFeatureRepository();
        Feature feature;

        public FormModifyFeature(int featureId)
        {
            feature = featureRepository.GetFeatureById(featureId);
            InitializeComponent();
            this.TitleTextBox.Text = feature.Title;
            this.CenterToScreen();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyFeatureButton_Click(object sender, EventArgs e)
        {
            Feature featureToBeModifiedAs = new Feature();
            featureToBeModifiedAs.Title = this.TitleTextBox.Text;
            featureToBeModifiedAs.ProjectId = feature.ProjectId;
            featureToBeModifiedAs.Id = feature.Id;

            String featureToModify = featureRepository.Modify(featureToBeModifiedAs);

            if (!featureToModify.Equals(""))
            {
                MessageBox.Show(featureToModify, "Attention");
            }
            else
            {
                this.Close();
            }
        }
    }
}
