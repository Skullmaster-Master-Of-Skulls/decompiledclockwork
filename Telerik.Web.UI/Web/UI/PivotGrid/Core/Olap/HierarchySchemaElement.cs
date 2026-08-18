using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D21 RID: 3361
	internal class HierarchySchemaElement : UniqueSchemaElement
	{
		// Token: 0x06007D2B RID: 32043 RVA: 0x001CB52A File Offset: 0x001C972A
		public HierarchySchemaElement()
		{
			this.levels = new List<LevelSchemaElement>();
			this.Grouping = DimensionHierarchyGroupingBehavior.Unknown;
			this.ViewType = DimensionHierarchyInstanceSelection.Unknown;
		}

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x06007D2C RID: 32044 RVA: 0x001CB54B File Offset: 0x001C974B
		public IList<LevelSchemaElement> Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x06007D2D RID: 32045 RVA: 0x001CB553 File Offset: 0x001C9753
		// (set) Token: 0x06007D2E RID: 32046 RVA: 0x001CB55B File Offset: 0x001C975B
		public string DefaultMember { get; set; }

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x06007D2F RID: 32047 RVA: 0x001CB564 File Offset: 0x001C9764
		// (set) Token: 0x06007D30 RID: 32048 RVA: 0x001CB56C File Offset: 0x001C976C
		public string DisplayFolder { get; set; }

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x06007D31 RID: 32049 RVA: 0x001CB575 File Offset: 0x001C9775
		// (set) Token: 0x06007D32 RID: 32050 RVA: 0x001CB57D File Offset: 0x001C977D
		public DimensionHierarchyGroupingBehavior Grouping { get; set; }

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x06007D33 RID: 32051 RVA: 0x001CB586 File Offset: 0x001C9786
		// (set) Token: 0x06007D34 RID: 32052 RVA: 0x001CB58E File Offset: 0x001C978E
		public DimensionHierarchyInstanceSelection ViewType { get; set; }

		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x06007D35 RID: 32053 RVA: 0x001CB597 File Offset: 0x001C9797
		// (set) Token: 0x06007D36 RID: 32054 RVA: 0x001CB59F File Offset: 0x001C979F
		public string AllMemberName { get; set; }

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x001CB5A8 File Offset: 0x001C97A8
		// (set) Token: 0x06007D38 RID: 32056 RVA: 0x001CB5B0 File Offset: 0x001C97B0
		public string DimensionUniqueName { get; set; }

		// Token: 0x0400225B RID: 8795
		private List<LevelSchemaElement> levels;
	}
}
