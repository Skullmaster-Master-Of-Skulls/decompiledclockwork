using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Students
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	public class DuplicateDynamicDataItem
	{
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00019A57 File Offset: 0x00017C57
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00019A5F File Offset: 0x00017C5F
		public DynamicData DataItem1 { get; set; }

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00019A68 File Offset: 0x00017C68
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x00019A70 File Offset: 0x00017C70
		public DynamicData DataItem2 { get; set; }

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00019A79 File Offset: 0x00017C79
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x00019A81 File Offset: 0x00017C81
		public eDuplicateItemToUse DataItemToUse { get; set; }
	}
}
