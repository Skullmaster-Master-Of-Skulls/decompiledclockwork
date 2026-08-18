using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011B9 RID: 4537
	public class RadMenuEventArgs : EventArgs
	{
		// Token: 0x0600BA4B RID: 47691 RVA: 0x00297C9A File Offset: 0x00295E9A
		public RadMenuEventArgs(RadMenuItem item)
		{
			this.Item = item;
		}

		// Token: 0x17003C11 RID: 15377
		// (get) Token: 0x0600BA4C RID: 47692 RVA: 0x00297CA9 File Offset: 0x00295EA9
		// (set) Token: 0x0600BA4D RID: 47693 RVA: 0x00297CB1 File Offset: 0x00295EB1
		public RadMenuItem Item { get; private set; }
	}
}
