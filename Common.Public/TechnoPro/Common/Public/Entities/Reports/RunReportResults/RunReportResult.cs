using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000239 RID: 569
	public class RunReportResult
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00018154 File Offset: 0x00016354
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0001815C File Offset: 0x0001635C
		public Report Report { get; set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00018165 File Offset: 0x00016365
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0001816D File Offset: 0x0001636D
		public IList<RunFunctionResult> FunctionResults { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00018176 File Offset: 0x00016376
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0001817E File Offset: 0x0001637E
		public RunFunctionData PrimaryData { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00018187 File Offset: 0x00016387
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0001818F File Offset: 0x0001638F
		public IList<RunFunctionData> AdditionalData { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00018198 File Offset: 0x00016398
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x000181A0 File Offset: 0x000163A0
		public RunStatus ReportStatus { get; set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x000181A9 File Offset: 0x000163A9
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x000181B1 File Offset: 0x000163B1
		public DateTime Started { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000181BA File Offset: 0x000163BA
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x000181C2 File Offset: 0x000163C2
		public DateTime Ended { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000181CB File Offset: 0x000163CB
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x000181D3 File Offset: 0x000163D3
		public ReportExecutionPlan ExecutionPlan { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000181DC File Offset: 0x000163DC
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00018245 File Offset: 0x00016445
		public IList<ReportParameter> CurrentReportParameters
		{
			get
			{
				bool flag = this._currentReportParameters != null;
				IList<ReportParameter> currentReportParameters;
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
						this._currentReportParameters = new List<ReportParameter>();
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

		// Token: 0x04000F71 RID: 3953
		private IList<ReportParameter> _currentReportParameters;
	}
}
