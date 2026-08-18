using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001DE RID: 478
	public class RadDataFormUpdatedEventArgs : RadDataFormDataChangeEventArgs
	{
		// Token: 0x06001106 RID: 4358 RVA: 0x0003E928 File Offset: 0x0003CB28
		public RadDataFormUpdatedEventArgs(int affectedRows, Exception e, RadDataFormDataItem item) : base(affectedRows, e, item)
		{
			this.KeepInEditMode = false;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0003E93A File Offset: 0x0003CB3A
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0003E942 File Offset: 0x0003CB42
		public bool KeepInEditMode { get; set; }
	}
}
