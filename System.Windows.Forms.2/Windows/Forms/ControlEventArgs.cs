using System;

namespace System.Windows.Forms
{
	// Token: 0x0200016E RID: 366
	public class ControlEventArgs : EventArgs
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0003D37A File Offset: 0x0003B57A
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0003D382 File Offset: 0x0003B582
		public ControlEventArgs(Control control)
		{
			this.control = control;
		}

		// Token: 0x04000910 RID: 2320
		private Control control;
	}
}
