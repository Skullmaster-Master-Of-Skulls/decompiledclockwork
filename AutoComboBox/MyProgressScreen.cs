using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000106 RID: 262
	public partial class MyProgressScreen : Form
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x000501C8 File Offset: 0x0004F1C8
		public MyProgressScreen(int max, string caption)
		{
			this.InitializeComponent();
			this.max = max;
			this.label1.Text = caption;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000503E9 File Offset: 0x0004F3E9
		private void MyProgressScreen_Load(object sender, EventArgs e)
		{
			this.progressBar1.Maximum = this.max;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00050400 File Offset: 0x0004F400
		public void IncrementProgressBar(object sender, EventArgs e)
		{
			int num = this.curr + 1;
			if (num <= this.max)
			{
				this.curr = num;
				this.RefreshAll();
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00050434 File Offset: 0x0004F434
		private void RefreshAll()
		{
			this.progressBar1.Value = this.curr;
		}

		// Token: 0x0400079C RID: 1948
		private int max;

		// Token: 0x0400079D RID: 1949
		private int curr = 0;
	}
}
