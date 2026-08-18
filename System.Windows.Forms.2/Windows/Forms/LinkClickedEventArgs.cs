using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002C3 RID: 707
	[ComVisible(true)]
	public class LinkClickedEventArgs : EventArgs
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000C24A1 File Offset: 0x000C06A1
		public string LinkText
		{
			get
			{
				return this.linkText;
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000C24A9 File Offset: 0x000C06A9
		public LinkClickedEventArgs(string linkText)
		{
			this.linkText = linkText;
		}

		// Token: 0x04001237 RID: 4663
		private string linkText;
	}
}
