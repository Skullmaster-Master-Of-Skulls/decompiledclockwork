using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D2E RID: 3374
	internal class OlapCubeInfo
	{
		// Token: 0x06007D8F RID: 32143 RVA: 0x001CBC03 File Offset: 0x001C9E03
		public OlapCubeInfo()
		{
			this.kpis = new List<KpiSchemaElement>();
			this.dimensions = new List<DimensionSchemaElement>();
			this.measures = new List<MeasureSchemaElement>();
			this.namedSets = new List<NamedSetSchemaElement>();
		}

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x001CBC37 File Offset: 0x001C9E37
		public IList<KpiSchemaElement> Kpis
		{
			get
			{
				return this.kpis;
			}
		}

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x06007D91 RID: 32145 RVA: 0x001CBC3F File Offset: 0x001C9E3F
		public IList<NamedSetSchemaElement> NamedSets
		{
			get
			{
				return this.namedSets;
			}
		}

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x06007D92 RID: 32146 RVA: 0x001CBC47 File Offset: 0x001C9E47
		public IList<DimensionSchemaElement> Dimensions
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x06007D93 RID: 32147 RVA: 0x001CBC4F File Offset: 0x001C9E4F
		public IList<MeasureSchemaElement> Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x001CBC57 File Offset: 0x001C9E57
		// (set) Token: 0x06007D95 RID: 32149 RVA: 0x001CBC5F File Offset: 0x001C9E5F
		public string Name { get; set; }

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x001CBC68 File Offset: 0x001C9E68
		// (set) Token: 0x06007D97 RID: 32151 RVA: 0x001CBC70 File Offset: 0x001C9E70
		public string Caption { get; set; }

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x001CBC79 File Offset: 0x001C9E79
		// (set) Token: 0x06007D99 RID: 32153 RVA: 0x001CBC81 File Offset: 0x001C9E81
		public string CatalogName { get; set; }

		// Token: 0x0400227C RID: 8828
		private List<KpiSchemaElement> kpis;

		// Token: 0x0400227D RID: 8829
		private List<NamedSetSchemaElement> namedSets;

		// Token: 0x0400227E RID: 8830
		private List<DimensionSchemaElement> dimensions;

		// Token: 0x0400227F RID: 8831
		private List<MeasureSchemaElement> measures;
	}
}
