using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200023C RID: 572
	public class CustomDataParametersWithLoadParameters : CustomDataParameters
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x00018293 File Offset: 0x00016493
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x0001829B File Offset: 0x0001649B
		public string SourceFileName { get; set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000182A4 File Offset: 0x000164A4
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x000182AC File Offset: 0x000164AC
		public eCustomDataLoadType LoadType { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x000182B5 File Offset: 0x000164B5
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x000182BD File Offset: 0x000164BD
		public string CustomDelimiter { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x000182C6 File Offset: 0x000164C6
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x000182CE File Offset: 0x000164CE
		public bool FirstRowDoesntHaveHeaders { get; set; }
	}
}
