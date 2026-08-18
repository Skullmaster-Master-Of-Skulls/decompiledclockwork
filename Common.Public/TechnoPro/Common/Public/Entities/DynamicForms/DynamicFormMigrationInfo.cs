using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034D RID: 845
	public class DynamicFormMigrationInfo : BusinessBase<int>
	{
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0001E653 File Offset: 0x0001C853
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x0001E65B File Offset: 0x0001C85B
		public string ScreenName { get; set; }

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0001E664 File Offset: 0x0001C864
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ScreenNum
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

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0001E67C File Offset: 0x0001C87C
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0001E684 File Offset: 0x0001C884
		public IList<DynamicFieldMigrationInfo> Fields { get; set; }
	}
}
