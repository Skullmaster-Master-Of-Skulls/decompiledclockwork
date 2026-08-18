namespace AutoComboBox
{
	// Token: 0x02000089 RID: 137
	public partial class PleaseWait : global::System.Windows.Forms.Form
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0002DC90 File Offset: 0x0002CC90
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002DCCC File Offset: 0x0002CCCC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::AutoComboBox.PleaseWait));
			this.p_pleaseWait = new global::System.Windows.Forms.Panel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.imageList3 = new global::System.Windows.Forms.ImageList(this.components);
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.p_pleaseWait.SuspendLayout();
			base.SuspendLayout();
			this.p_pleaseWait.BackColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.p_pleaseWait.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.p_pleaseWait.Controls.Add(this.label2);
			this.p_pleaseWait.Controls.Add(this.pictureBox1);
			this.p_pleaseWait.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_pleaseWait.Location = new global::System.Drawing.Point(0, 0);
			this.p_pleaseWait.Name = "p_pleaseWait";
			this.p_pleaseWait.Size = new global::System.Drawing.Size(333, 98);
			this.p_pleaseWait.TabIndex = 7;
			this.p_pleaseWait.Click += new global::System.EventHandler(this.p_pleaseWait_Click);
			this.label2.BackColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.label2.Font = new global::System.Drawing.Font("Arial", 20.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.label2.Location = new global::System.Drawing.Point(72, 28);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(189, 34);
			this.label2.TabIndex = 1;
			this.label2.Text = "Please Wait ...";
			this.pictureBox1.BackColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.pictureBox1.Location = new global::System.Drawing.Point(275, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(54, 54);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.imageList3.ImageSize = new global::System.Drawing.Size(48, 48);
			this.imageList3.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("imageList3.ImageStream");
			this.imageList3.TransparentColor = global::System.Drawing.Color.Transparent;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(333, 98);
			base.Controls.Add(this.p_pleaseWait);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "PleaseWait";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PleaseWait";
			base.Load += new global::System.EventHandler(this.PleaseWait_Load);
			this.p_pleaseWait.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400048C RID: 1164
		private global::System.Windows.Forms.Panel p_pleaseWait;

		// Token: 0x0400048D RID: 1165
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400048E RID: 1166
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x0400048F RID: 1167
		private global::System.Windows.Forms.ImageList imageList3;

		// Token: 0x04000490 RID: 1168
		private global::System.Windows.Forms.Timer timer1;

		// Token: 0x04000491 RID: 1169
		private global::System.ComponentModel.IContainer components;
	}
}
