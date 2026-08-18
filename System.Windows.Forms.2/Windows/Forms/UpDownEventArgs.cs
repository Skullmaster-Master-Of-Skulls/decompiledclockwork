using System;

namespace System.Windows.Forms
{
	// Token: 0x0200042A RID: 1066
	public class UpDownEventArgs : EventArgs
	{
		// Token: 0x060049F6 RID: 18934 RVA: 0x001375CF File Offset: 0x001357CF
		public UpDownEventArgs(int buttonPushed)
		{
			this.buttonID = buttonPushed;
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x001375DE File Offset: 0x001357DE
		public int ButtonID
		{
			get
			{
				return this.buttonID;
			}
		}

		// Token: 0x040027C8 RID: 10184
		private int buttonID;
	}
}
