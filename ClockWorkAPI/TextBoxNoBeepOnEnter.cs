using System;
using System.Windows.Forms;

namespace ClockWorkAPI
{
	// Token: 0x02000059 RID: 89
	public class TextBoxNoBeepOnEnter : TextBox
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00017AAC File Offset: 0x00016AAC
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00017AC4 File Offset: 0x00016AC4
		public bool SuppressEnter
		{
			get
			{
				return this.suppressEnter;
			}
			set
			{
				this.suppressEnter = value;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017AD0 File Offset: 0x00016AD0
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && this.suppressEnter)
			{
				e.Handled = true;
			}
		}

		// Token: 0x040001D3 RID: 467
		private bool suppressEnter = true;
	}
}
