using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001DF RID: 479
	public class RadDataFormInsertedEventArgs : RadDataFormDataChangeEventArgs
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0003E94B File Offset: 0x0003CB4B
		public RadDataFormInsertedEventArgs(int affectedRows, Exception e, RadDataFormDataItem item) : base(affectedRows, e, item)
		{
			this.KeepInInsertMode = false;
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0003E95D File Offset: 0x0003CB5D
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x0003E965 File Offset: 0x0003CB65
		public bool KeepInInsertMode { get; set; }
	}
}
