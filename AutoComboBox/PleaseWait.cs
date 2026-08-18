using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000089 RID: 137
	public partial class PleaseWait : Form
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0002DC75 File Offset: 0x0002CC75
		public PleaseWait()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002E01C File Offset: 0x0002D01C
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (++this.currPic >= this.imageList3.Images.Count)
			{
				this.currPic = 0;
			}
			this.pictureBox1.Image = this.imageList3.Images[this.currPic];
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002E07A File Offset: 0x0002D07A
		private void p_pleaseWait_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002E084 File Offset: 0x0002D084
		private void PleaseWait_Load(object sender, EventArgs e)
		{
			this.timer1.Enabled = true;
		}

		// Token: 0x04000492 RID: 1170
		private int currPic = -1;
	}
}
