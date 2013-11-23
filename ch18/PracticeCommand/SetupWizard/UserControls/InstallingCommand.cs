using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

//using CVST.AppFramework.CFWServerIntegrityValidate; 

namespace DonOfDesign.PracticePatterns.SetupWizard
{
	/// <summary>
	/// Step5BodyUC ��ժҪ˵����
	/// </summary>
	public class InstallingCommand : System.Windows.Forms.UserControl,ISetupCommand
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progBar;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private System.Windows.Forms.Label lbMessage;
		private System.Windows.Forms.GroupBox groupBox1;
        private SetupForm m_form;

		public InstallingCommand(SetupForm form)
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
			this.label1 = new System.Windows.Forms.Label();
			this.lbMessage = new System.Windows.Forms.Label();
			this.progBar = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "���ڰ�װ��";
			// 
			// lbMessage
			// 
			this.lbMessage.Location = new System.Drawing.Point(24, 56);
			this.lbMessage.Name = "lbMessage";
			this.lbMessage.Size = new System.Drawing.Size(264, 23);
			this.lbMessage.TabIndex = 1;
			this.lbMessage.Text = "���Ժ�...";
			// 
			// progBar
			// 
			this.progBar.Location = new System.Drawing.Point(24, 88);
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(408, 23);
			this.progBar.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 3);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// Step5BodyUC
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.progBar);
			this.Controls.Add(this.lbMessage);
			this.Controls.Add(this.label1);
			this.Name = "Step5BodyUC";
			this.Size = new System.Drawing.Size(456, 220);
            this.Load += new System.EventHandler(this.InstallingCommand_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void InstallingCommand_Load(object sender, System.EventArgs e)
		{
			lbMessage.Text = "���Ժ�...";					
		}		

        #region ISetupCommand Members

        public void SetupForward()
        {            
            lbMessage.Text = "���ڰ�װ�����ļ�...";

            string setupDir = PropertySet.GetValue("setupdir").ToString();
            string zipFileName = PropertySet.GetValue("zipfile").ToString();
            
            ZipFileHandler zipHandler = new ZipFileHandler();

            try
            {
                List<string> tempfiles = new List<string>();
                zipHandler.BeginDecompress(zipFileName,setupDir,ref tempfiles);

                progBar.Value = 0;
                progBar.Minimum = 0;
                progBar.Maximum = tempfiles.Count;
                progBar.Step = 1;

                foreach (string file in tempfiles)
                {
                    zipHandler.DecompressSingleFile(zipFileName, setupDir, file);                    
                    progBar.PerformStep();
                }

                lbMessage.Text = "��װ��ϣ��뵥��\"��һ��\"������װ";
                this.Refresh();

                m_form.AddTitleUC("��װ���");
                SetupCompletedCommand command = new SetupCompletedCommand(m_form);
                m_form.AddBodyUC(command);                
                m_form.CurrentCommand = command;

                m_form.SetNextButtonToClose();
            }
            catch (Exception)
            {
                MessageBox.Show("��װ�����󣬰�װ������ֹ");
                m_form.Abort();
            }
        }

        public void SetupBackward()
        {            
            m_form.AddTitleUC("׼��");
            PrepareSetupCommand command = new PrepareSetupCommand(m_form);
            m_form.AddBodyUC(command);
            m_form.SetNextButtonEnabled(true);
            m_form.CurrentCommand = command;
        }

        #endregion
    }
}
