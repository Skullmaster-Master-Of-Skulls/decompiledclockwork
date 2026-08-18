using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000359 RID: 857
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunReportResultDTO
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0000929B File Offset: 0x0000749B
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x000092A3 File Offset: 0x000074A3
		[DataMember]
		public ReportDTO Report { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000092AC File Offset: 0x000074AC
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x000092B4 File Offset: 0x000074B4
		[DataMember]
		public IList<RunFunctionResultDTO> FunctionResults { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000092BD File Offset: 0x000074BD
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x000092C5 File Offset: 0x000074C5
		[DataMember]
		public RunFunctionDataDTO PrimaryData { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000092CE File Offset: 0x000074CE
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000092D6 File Offset: 0x000074D6
		[DataMember]
		public IList<RunFunctionDataDTO> AdditionalData { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000092DF File Offset: 0x000074DF
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x000092E7 File Offset: 0x000074E7
		[DataMember]
		public RunStatusDTO ReportStatus { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000092F0 File Offset: 0x000074F0
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x000092F8 File Offset: 0x000074F8
		[DataMember]
		public DateTime Started { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00009301 File Offset: 0x00007501
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00009309 File Offset: 0x00007509
		[DataMember]
		public DateTime Ended { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00009312 File Offset: 0x00007512
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0000931A File Offset: 0x0000751A
		[DataMember]
		public ReportExecutionPlanDTO ExecutionPlan { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00009324 File Offset: 0x00007524
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x0000938D File Offset: 0x0000758D
		public IList<ReportParameterDTO> CurrentReportParameters
		{
			get
			{
				bool flag = this._currentReportParameters != null;
				IList<ReportParameterDTO> currentReportParameters;
				if (flag)
				{
					currentReportParameters = this._currentReportParameters;
				}
				else
				{
					bool flag2 = this.Report != null && this.Report.ReportParameters != null;
					if (flag2)
					{
						this._currentReportParameters = this.Report.ReportParameters;
					}
					else
					{
						this._currentReportParameters = new List<ReportParameterDTO>();
					}
					currentReportParameters = this._currentReportParameters;
				}
				return currentReportParameters;
			}
			set
			{
				this._currentReportParameters = value;
			}
		}

		// Token: 0x04000661 RID: 1633
		[DataMember]
		private IList<ReportParameterDTO> _currentReportParameters;
	}
}
