using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000306 RID: 774
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportReq : BaseReportMessageReq
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00008533 File Offset: 0x00006733
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0000853B File Offset: 0x0000673B
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00008544 File Offset: 0x00006744
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0000854C File Offset: 0x0000674C
		[DataMember]
		public ReportDTO Report { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00008555 File Offset: 0x00006755
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x0000855D File Offset: 0x0000675D
		[DataMember]
		public ReportExecutionPlanDTO ExecutionPlan { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00008566 File Offset: 0x00006766
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x0000856E File Offset: 0x0000676E
		[DataMember]
		public RunReportResultDTO PreviousRunReportResult { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00008577 File Offset: 0x00006777
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x0000857F File Offset: 0x0000677F
		[DataMember]
		public IList<eFunctionType> FunctionTypesToSkip { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00008588 File Offset: 0x00006788
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00008590 File Offset: 0x00006790
		[DataMember]
		public IList<ReportParameterDTO> ReportParameters { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00008599 File Offset: 0x00006799
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x000085A1 File Offset: 0x000067A1
		[DataMember]
		public IList<int> OnlyRunFunctionIds { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x000085AA File Offset: 0x000067AA
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x000085B2 File Offset: 0x000067B2
		[DataMember]
		public eReportExecutedFromLocation ExecutedFromLocation { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x000085BB File Offset: 0x000067BB
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x000085C3 File Offset: 0x000067C3
		[DataMember]
		public bool RunningOnServer { get; set; }
	}
}
