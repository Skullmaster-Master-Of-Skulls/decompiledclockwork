using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200020E RID: 526
	public class RadDataFormItemEventArgs : EventArgs
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x000448AD File Offset: 0x00042AAD
		public RadDataFormItemEventArgs(RadDataFormItem item)
		{
			this.Item = item;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000448BC File Offset: 0x00042ABC
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x000448C4 File Offset: 0x00042AC4
		public RadDataFormItem Item { get; private set; }
	}
}
