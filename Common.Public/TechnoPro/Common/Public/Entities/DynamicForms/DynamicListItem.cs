using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200036A RID: 874
	public class DynamicListItem : BusinessBase<int>
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0001EE0C File Offset: 0x0001D00C
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int LookupListId
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

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0001EE24 File Offset: 0x0001D024
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0001EE2C File Offset: 0x0001D02C
		public string LookupText { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0001EE35 File Offset: 0x0001D035
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0001EE3D File Offset: 0x0001D03D
		public string LookupValue { get; set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0001EE46 File Offset: 0x0001D046
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0001EE4E File Offset: 0x0001D04E
		public int OrderNum { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0001EE57 File Offset: 0x0001D057
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0001EE5F File Offset: 0x0001D05F
		public string Children { get; set; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0001EE68 File Offset: 0x0001D068
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0001EE70 File Offset: 0x0001D070
		public DynamicListGroup Group { get; set; }
	}
}
