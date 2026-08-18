using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000349 RID: 841
	public class DynamicFieldMigrationInfo : BusinessBase<int>
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ControlId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public string Caption { get; set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0001E4F9 File Offset: 0x0001C6F9
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0001E501 File Offset: 0x0001C701
		public eControlCode ControlCode { get; set; }

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0001E50A File Offset: 0x0001C70A
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x0001E512 File Offset: 0x0001C712
		public IList<DynamicListItem> ListItems { get; set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0001E51B File Offset: 0x0001C71B
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x0001E523 File Offset: 0x0001C723
		public string SectionOnForm { get; set; }
	}
}
