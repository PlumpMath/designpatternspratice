using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DonOfDesign.PracticePatterns.SetupWizard
{
	/// <summary>
	/// Step6BodyUC ��ժҪ˵����
	/// </summary>
	public class SetupCompletedCommand : System.Windows.Forms.UserControl,ISetupCommand
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
        private SetupForm m_form;

		public SetupCompletedCommand(SetupForm form)
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "��װ�ɹ����ѽ�ϵͳ��װ��ָ��Ŀ¼�С�";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "�������رա��˳���";
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
            // SixthStep
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SixthStep";
            this.Size = new System.Drawing.Size(456, 220);
            this.ResumeLayout(false);

		}
		#endregion

        #region ISetupCommand Members

        public void SetupForward()
        {
            //do nothing
        }

        public void SetupBackward()
        {
            m_form.AddTitleUC("��ʼ��װ");
            InstallingCommand command = new InstallingCommand(m_form);
            m_form.AddBodyUC(command);
            m_form.CurrentCommand = command;
            command.SetupBackward();

            m_form.RestoreNextButton();
            m_form.SetNextButtonEnabled(true);
        }

        #endregion
    }
}
