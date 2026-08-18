using System;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000098 RID: 152
	public class RunAnotherReportAndMergeInToCurrentTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x0001F720 File Offset: 0x0001D920
		public RunAnotherReportAndMergeInToCurrentTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001F73B File Offset: 0x0001D93B
		public RunAnotherReportAndMergeInToCurrentTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001F759 File Offset: 0x0001D959
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001F761 File Offset: 0x0001D961
		public OperationContext OpContext { get; set; }

		// Token: 0x0600055C RID: 1372 RVA: 0x0001F76C File Offset: 0x0001D96C
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string[] array = function.GetDefaultFunctionParameter().Split(new char[]
			{
				'`'
			});
			string[] array2 = array[0].Split(new char[]
			{
				'~'
			});
			int num = int.Parse(array2[0]);
			bool flag = array2.Length > 1;
			if (flag)
			{
				string[] array3 = array2[1].Split(new char[]
				{
					';'
				});
				foreach (string text in array3)
				{
					int num2 = text.IndexOf('=');
					bool flag2 = num2 > 0;
					if (flag2)
					{
						string name = text.Substring(0, num2).Trim().ToLower();
						bool flag3 = ++num2 < text.Length;
						object obj;
						if (flag3)
						{
							string text2 = text.Substring(num2).Trim();
							bool flag4 = text2.Length > 0 && char.IsDigit(text2[0]);
							if (flag4)
							{
								try
								{
									obj = int.Parse(text2);
								}
								catch
								{
									obj = text2;
								}
							}
							else
							{
								obj = text2;
							}
						}
						else
						{
							string text2 = "";
							obj = text2;
						}
						bool flag5 = obj is string;
						if (flag5)
						{
							string text3 = (string)obj;
							bool flag6 = text3.Length == 2;
							if (flag6)
							{
								bool flag7 = text3[0] == '\'' && text3[text3.Length - 1] == '\'';
								if (flag7)
								{
									obj = "";
								}
							}
							else
							{
								bool flag8 = text3.Length > 2;
								if (flag8)
								{
									bool flag9 = text3[0] == '\'' && text3[text3.Length - 1] == '\'' && text3[1] != '\'';
									if (flag9)
									{
										obj = text3.Substring(1, text3.Length - 2);
									}
								}
							}
						}
						ReportParameter reportParameter = CurrentWholeReportResult.CurrentReportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
						bool flag10 = reportParameter != null;
						if (flag10)
						{
							reportParameter.Value = obj;
						}
						else
						{
							CurrentWholeReportResult.CurrentReportParameters.Add(new ReportParameter
							{
								Name = name,
								Value = obj
							});
						}
					}
				}
			}
			RunAnotherReport runAnotherReport = new RunAnotherReport(this.OpContext);
			RunReportResult runReportResult = runAnotherReport.RunAnotherClockWorkReport(num.ToString(), CurrentWholeReportResult);
			bool flag11 = runReportResult.AdditionalData != null;
			if (flag11)
			{
				foreach (RunFunctionData item in runReportResult.AdditionalData)
				{
					CurrentWholeReportResult.AdditionalData.Add(item);
				}
			}
			result.Data.Table = ((runReportResult == null || runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table);
		}

		// Token: 0x0400010D RID: 269
		private ReportDAO dao;
	}
}
