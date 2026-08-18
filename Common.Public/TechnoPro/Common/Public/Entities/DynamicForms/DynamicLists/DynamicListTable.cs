using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists
{
	// Token: 0x0200037D RID: 893
	public class DynamicListTable
	{
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0001F6D6 File Offset: 0x0001D8D6
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x0001F6DE File Offset: 0x0001D8DE
		public DynamicField Field { get; set; }

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0001F6E7 File Offset: 0x0001D8E7
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0001F6EF File Offset: 0x0001D8EF
		public IList<DynamicListColumn> Columns { get; set; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0001F700 File Offset: 0x0001D900
		public IList<DynamicListRow> Rows { get; set; }
	}
}
