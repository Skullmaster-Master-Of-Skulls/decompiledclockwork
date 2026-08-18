using System;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200005C RID: 92
	public class ReportDataColumn
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00014E84 File Offset: 0x00013084
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00014E8C File Offset: 0x0001308C
		public string ColumnName { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00014E95 File Offset: 0x00013095
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00014E9D File Offset: 0x0001309D
		public Type ColumnDataType { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00014EA6 File Offset: 0x000130A6
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00014EAE File Offset: 0x000130AE
		public string DefaultValue { get; set; }
	}
}
