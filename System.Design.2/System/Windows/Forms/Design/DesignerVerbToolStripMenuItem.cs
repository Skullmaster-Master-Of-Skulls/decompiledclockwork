using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DA RID: 730
	internal class DesignerVerbToolStripMenuItem : ToolStripMenuItem
	{
		// Token: 0x06001D1B RID: 7451 RVA: 0x000AFA4B File Offset: 0x000ADC4B
		public DesignerVerbToolStripMenuItem(DesignerVerb verb)
		{
			this.verb = verb;
			this.Text = verb.Text;
			this.RefreshItem();
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000AFA6C File Offset: 0x000ADC6C
		public void RefreshItem()
		{
			if (this.verb != null)
			{
				base.Visible = this.verb.Visible;
				this.Enabled = this.verb.Enabled;
				base.Checked = this.verb.Checked;
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000AFAA9 File Offset: 0x000ADCA9
		protected override void OnClick(EventArgs e)
		{
			if (this.verb != null)
			{
				this.verb.Invoke();
			}
		}

		// Token: 0x0400174E RID: 5966
		private DesignerVerb verb;
	}
}
