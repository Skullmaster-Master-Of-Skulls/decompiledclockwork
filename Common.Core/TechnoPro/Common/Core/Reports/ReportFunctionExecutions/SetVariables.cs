using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200009A RID: 154
	public class SetVariables : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0000672B File Offset: 0x0000492B
		public SetVariables()
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001FC32 File Offset: 0x0001DE32
		public SetVariables(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001FC44 File Offset: 0x0001DE44
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000566 RID: 1382 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public Dictionary<string, object> GetVariables(string functionParams)
		{
			string[] array = functionParams.Split(new char[]
			{
				'`'
			});
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string text in array)
			{
				string text2 = text.Trim();
				bool flag = text2.Length > 0;
				if (flag)
				{
					int num = text2.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						string text3 = text2.Substring(0, num).Trim();
						int num2 = text3.IndexOf('.');
						string text4 = text2.Substring(num + 1);
						object value = text4;
						bool flag3 = num2 > 0;
						if (flag3)
						{
							string text5 = text3.Substring(num2 + 1).Trim().ToLower();
							text3 = text3.Substring(0, num2);
							string text6 = text3;
							string a = text6;
							if (!(a == "int"))
							{
								if (!(a == "double"))
								{
									if (a == "date" || a == "datetime")
									{
										DateTime dateTime;
										bool flag4 = DateTime.TryParse(text4, out dateTime);
										if (flag4)
										{
											value = dateTime;
										}
										else
										{
											value = null;
										}
									}
								}
								else
								{
									double num3;
									bool flag5 = double.TryParse(text4, out num3);
									if (flag5)
									{
										value = num3;
									}
									else
									{
										value = 0.0;
									}
								}
							}
							else
							{
								int num4;
								bool flag6 = int.TryParse(text4, out num4);
								if (flag6)
								{
									value = num4;
								}
								else
								{
									value = 0;
								}
							}
						}
						bool flag7 = !dictionary.ContainsKey(text3);
						if (flag7)
						{
							dictionary.Add(text3, value);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001FE0C File Offset: 0x0001E00C
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			Dictionary<string, object> variables = this.GetVariables(Function.GetDefaultFunctionParameter());
			Result.ReportParametersOut = variables.ToList<KeyValuePair<string, object>>().ConvertAll<ReportParameter>((KeyValuePair<string, object> g) => new ReportParameter
			{
				Name = g.Key,
				Value = g.Value
			});
			Result.Data.IsPrimary = false;
		}
	}
}
