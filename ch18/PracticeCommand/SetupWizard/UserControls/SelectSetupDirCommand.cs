using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DonOfDesign.PracticePatterns.SetupWizard
{
	/// <summary>
	/// BodyUC ��ժҪ˵����
	/// </summary>
	public class SelectSetupDirCommand : System.Windows.Forms.UserControl,ISetupCommand
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnImport;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.TextBox txtFolderName;
		private string deployDirectory;

		private System.Windows.Forms.Label lbMessage1;
		private System.Windows.Forms.Label lbMessage2;
		private SetupForm m_form;


		public SelectSetupDirCommand(SetupForm form)
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

            m_form = form;

		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.lbMessage1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.lbMessage2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lbMessage1
            // 
            this.lbMessage1.Location = new System.Drawing.Point(16, 24);
            this.lbMessage1.Name = "lbMessage1";
            this.lbMessage1.Size = new System.Drawing.Size(288, 23);
            this.lbMessage1.TabIndex = 8;
            this.lbMessage1.Text = "��װ���߽�ϵͳ�ļ���װ��ָ��Ŀ¼�С�";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 3);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnImport
            // 
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.Location = new System.Drawing.Point(320, 132);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(112, 26);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "���(&B)...";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtFolderName
            // 
            this.txtFolderName.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFolderName.Location = new System.Drawing.Point(16, 136);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.ReadOnly = true;
            this.txtFolderName.Size = new System.Drawing.Size(296, 20);
            this.txtFolderName.TabIndex = 5;
            // 
            // lbMessage2
            // 
            this.lbMessage2.Location = new System.Drawing.Point(16, 56);
            this.lbMessage2.Name = "lbMessage2";
            this.lbMessage2.Size = new System.Drawing.Size(416, 23);
            this.lbMessage2.TabIndex = 10;
            this.lbMessage2.Text = "���а�װ���뵥������һ������Ҫ��װ�������ļ��У��뵥�����������";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "�ļ���(&F):";
            // 
            // ThirdStep
            // 
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbMessage2);
            this.Controls.Add(this.lbMessage1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtFolderName);
            this.Name = "ThirdStep";
            this.Size = new System.Drawing.Size(456, 220);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			folderDlg.Description = "��ѡ���ļ���...";
			folderDlg.SelectedPath = txtFolderName.Text;			

			if (folderDlg.ShowDialog() == DialogResult.OK)
			{	
				txtFolderName.Text = folderDlg.SelectedPath;
                deployDirectory = txtFolderName.Text;
                m_form.SetNextButtonEnabled(true);
			}
		}		

        #region ISetupCommand Members

        public void SetupForward()
        {
            if (string.IsNullOrEmpty(deployDirectory))
            {
                MessageBox.Show("��ѡ��װ�ļ���!");
                m_form.SetNextButtonEnabled(false);
            }
            else
            {
                PropertySet.AddProperty("setupdir", deployDirectory);
                if (Directory.Exists(deployDirectory))
                {
                    Directory.Delete(deployDirectory,true);
                }
                Directory.CreateDirectory(deployDirectory);

                m_form.AddTitleUC("׼��");
                PrepareSetupCommand command = new PrepareSetupCommand(m_form);
                m_form.AddBodyUC(command);
                m_form.CurrentCommand = command;
            }
        }

        public void SetupBackward()
        {
            PropertySet.RemoveProperty("zipfile"); 
            m_form.AddTitleUC("���밲װ��");
            ImportSetupFileCommand command = new ImportSetupFileCommand(m_form);
            m_form.AddBodyUC(command);
            m_form.SetBeforeButtonEnabled(false);
            m_form.CurrentCommand = command;
        }

        #endregion
    }
}
