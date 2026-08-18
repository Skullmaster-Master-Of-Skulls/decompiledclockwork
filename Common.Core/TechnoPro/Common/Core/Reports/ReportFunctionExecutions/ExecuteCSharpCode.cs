using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000077 RID: 119
	public class ExecuteCSharpCode : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000672B File Offset: 0x0000492B
		public ExecuteCSharpCode()
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001AC07 File Offset: 0x00018E07
		public ExecuteCSharpCode(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001AC19 File Offset: 0x00018E19
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0001AC21 File Offset: 0x00018E21
		public OperationContext OpContext { get; set; }

		// Token: 0x0600049F RID: 1183 RVA: 0x0001AC2C File Offset: 0x00018E2C
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			List<ReportParameter> parameters = (from rp in CurrentWholeReportResult.CurrentReportParameters
			select new ReportParameter
			{
				Name = rp.Name,
				Value = rp.Value
			}).ToList<ReportParameter>();
			string text = defaultFunctionParameter ?? "";
			RunFunctionData primaryData = CurrentWholeReportResult.PrimaryData;
			DataTable previousTable = (primaryData != null) ? primaryData.Table : null;
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(text))
			{
				bool flag = false;
				string text2;
				while ((text2 = stringReader.ReadLine()) != null)
				{
					bool flag2 = flag;
					if (flag2)
					{
						stringBuilder.AppendLine(text2);
					}
					else
					{
						string text3 = text2.Trim();
						bool flag3 = text3.Length < 1;
						if (flag3)
						{
							stringBuilder.AppendLine();
						}
						else
						{
							bool flag4 = text3.StartsWith("imports ");
							if (flag4)
							{
								list.Add(text3.Substring(7).Trim().Trim(new char[]
								{
									';'
								}));
							}
							else
							{
								flag = true;
								stringBuilder.AppendLine(text2);
							}
						}
					}
				}
				text = stringBuilder.ToString();
			}
			RunReportResult runReportResult = CompilerUtility.ExecuteReport(this.OpContext.WhoAmI, previousTable, text, list, parameters, this.OpContext.AppContext.ExecutingPath);
			bool flag5 = runReportResult.ReportStatus != null;
			if (flag5)
			{
				eRunStatusStep lastStatusStep = runReportResult.ReportStatus.LastStatusStep;
				bool flag6 = lastStatusStep == eRunStatusStep.Failed || lastStatusStep == eRunStatusStep.FailedUnableToStart;
				if (flag6)
				{
					throw new Exception(string.Format("Failed: {0}:{1}", lastStatusStep.ToString(), runReportResult.ReportStatus.ErrorMessage ?? "NULL"));
				}
			}
			Result.Data = runReportResult.PrimaryData;
			runReportResult.PrimaryData.IsPrimary = true;
			Result.ReportParametersOut = runReportResult.CurrentReportParameters;
		}
	}
}
