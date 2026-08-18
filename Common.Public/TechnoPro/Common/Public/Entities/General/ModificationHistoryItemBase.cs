using System;

namespace TechnoPro.Common.Public.Entities.General
{
	// Token: 0x0200032F RID: 815
	public class ModificationHistoryItemBase
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0001DF5D File Offset: 0x0001C15D
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x0001DF65 File Offset: 0x0001C165
		public DateTime? DateCreated { get; set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0001DF6E File Offset: 0x0001C16E
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x0001DF76 File Offset: 0x0001C176
		public virtual int WhoCreatedPersonId { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0001DF7F File Offset: 0x0001C17F
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x0001DF87 File Offset: 0x0001C187
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0001DF90 File Offset: 0x0001C190
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x0001DF98 File Offset: 0x0001C198
		public virtual int WhoLastModifiedPersonId { get; set; }
	}
}
