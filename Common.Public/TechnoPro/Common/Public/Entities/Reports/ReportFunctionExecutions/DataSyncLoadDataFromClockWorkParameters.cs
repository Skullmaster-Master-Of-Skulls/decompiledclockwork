using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000244 RID: 580
	public class DataSyncLoadDataFromClockWorkParameters
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00018478 File Offset: 0x00016678
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00018480 File Offset: 0x00016680
		public eDataSyncLoadDataFromClockWorkParametersType LoadDataType { get; set; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00018489 File Offset: 0x00016689
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00018491 File Offset: 0x00016691
		public string CustomTableNameWithoutCustomPrefix { get; set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0001849A File Offset: 0x0001669A
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x000184A2 File Offset: 0x000166A2
		public string[] LookupFieldParameterNames { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000184AB File Offset: 0x000166AB
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x000184B3 File Offset: 0x000166B3
		public string LookupExternalColumnName { get; set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x000184BC File Offset: 0x000166BC
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x000184C4 File Offset: 0x000166C4
		public string[] ExternalColumnNamesToReturn { get; set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000184CD File Offset: 0x000166CD
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x000184D5 File Offset: 0x000166D5
		public string OverrideSql { get; set; }
	}
}
