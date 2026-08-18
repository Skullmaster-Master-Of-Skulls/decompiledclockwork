using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D2A RID: 3370
	internal class MultiDimensionalInfo
	{
		// Token: 0x06007D6F RID: 32111 RVA: 0x001CBA34 File Offset: 0x001C9C34
		public MultiDimensionalInfo()
		{
			this.Catalogs = new List<OlapCatalogInfo>();
			this.Cubes = new List<OlapCubeInfo>();
			this.Dimensions = new List<DimensionSchemaElement>();
			this.MeasureGroups = new List<MeasureGroupSchemaElement>();
			this.Measures = new List<MeasureSchemaElement>();
			this.DimensionHierarchies = new List<HierarchySchemaElement>();
			this.DimensionLevels = new List<LevelSchemaElement>();
			this.Sets = new List<NamedSetSchemaElement>();
			this.Kpis = new List<KpiSchemaElement>();
		}

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x06007D70 RID: 32112 RVA: 0x001CBAAA File Offset: 0x001C9CAA
		// (set) Token: 0x06007D71 RID: 32113 RVA: 0x001CBAB2 File Offset: 0x001C9CB2
		public IList<OlapCatalogInfo> Catalogs { get; private set; }

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x06007D72 RID: 32114 RVA: 0x001CBABB File Offset: 0x001C9CBB
		// (set) Token: 0x06007D73 RID: 32115 RVA: 0x001CBAC3 File Offset: 0x001C9CC3
		public IList<OlapCubeInfo> Cubes { get; private set; }

		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x06007D74 RID: 32116 RVA: 0x001CBACC File Offset: 0x001C9CCC
		// (set) Token: 0x06007D75 RID: 32117 RVA: 0x001CBAD4 File Offset: 0x001C9CD4
		public IList<DimensionSchemaElement> Dimensions { get; private set; }

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x001CBADD File Offset: 0x001C9CDD
		// (set) Token: 0x06007D77 RID: 32119 RVA: 0x001CBAE5 File Offset: 0x001C9CE5
		public IList<HierarchySchemaElement> DimensionHierarchies { get; private set; }

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x001CBAEE File Offset: 0x001C9CEE
		// (set) Token: 0x06007D79 RID: 32121 RVA: 0x001CBAF6 File Offset: 0x001C9CF6
		public IList<LevelSchemaElement> DimensionLevels { get; private set; }

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x06007D7A RID: 32122 RVA: 0x001CBAFF File Offset: 0x001C9CFF
		// (set) Token: 0x06007D7B RID: 32123 RVA: 0x001CBB07 File Offset: 0x001C9D07
		public IList<MeasureGroupSchemaElement> MeasureGroups { get; private set; }

		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x06007D7C RID: 32124 RVA: 0x001CBB10 File Offset: 0x001C9D10
		// (set) Token: 0x06007D7D RID: 32125 RVA: 0x001CBB18 File Offset: 0x001C9D18
		public IList<MeasureSchemaElement> Measures { get; private set; }

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x06007D7E RID: 32126 RVA: 0x001CBB21 File Offset: 0x001C9D21
		// (set) Token: 0x06007D7F RID: 32127 RVA: 0x001CBB29 File Offset: 0x001C9D29
		public IList<NamedSetSchemaElement> Sets { get; private set; }

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x06007D80 RID: 32128 RVA: 0x001CBB32 File Offset: 0x001C9D32
		// (set) Token: 0x06007D81 RID: 32129 RVA: 0x001CBB3A File Offset: 0x001C9D3A
		public IList<KpiSchemaElement> Kpis { get; private set; }
	}
}
