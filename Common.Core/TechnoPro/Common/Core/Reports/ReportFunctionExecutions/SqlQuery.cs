using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200009C RID: 156
	public class SqlQuery : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00020090 File Offset: 0x0001E290
		public SqlQuery()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000200AB File Offset: 0x0001E2AB
		public SqlQuery(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000200C9 File Offset: 0x0001E2C9
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000200D1 File Offset: 0x0001E2D1
		public OperationContext OpContext { get; set; }

		// Token: 0x06000571 RID: 1393 RVA: 0x000200DC File Offset: 0x0001E2DC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string text = function.GetDefaultFunctionParameter();
			text = (SqlQuery.ReplaceWebSettingsWithValues(text) ?? text);
			DataTable table = this.dao.RunReportSql(SqlQuery.ExtractReportParameters(CurrentWholeReportResult), text);
			result.Data.Table = table;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00020120 File Offset: 0x0001E320
		public static IList<ReportParameter> ExtractReportParameters(RunReportResult currentWholeReportResult)
		{
			IList<ReportParameter> list;
			if (currentWholeReportResult == null)
			{
				list = null;
			}
			else
			{
				Report report = currentWholeReportResult.Report;
				list = ((report != null) ? report.ReportParameters : null);
			}
			IList<ReportParameter> source = list ?? new List<ReportParameter>();
			IList<ReportParameter> source2 = ((currentWholeReportResult != null) ? currentWholeReportResult.CurrentReportParameters : null) ?? new List<ReportParameter>();
			List<ReportParameter> pp = (from g in source2
			select g.Clone()).ToList<ReportParameter>();
			pp.AddRange(from g in source
			where !pp.Any((ReportParameter h) => h.Name.Equals(g.Name, StringComparison.OrdinalIgnoreCase))
			select g);
			return pp;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000201C4 File Offset: 0x0001E3C4
		public static string ReplaceWebSettingsWithValues(string sql)
		{
			string text = SqlQuery.ReplaceWebSettingsWithValues(new Regex("@$([_a-zA-Z]+)"), sql, SqlQuery.EWebSettingType.Int);
			return SqlQuery.ReplaceWebSettingsWithValues(new Regex("@%([_a-zA-Z]+)"), text ?? sql, SqlQuery.EWebSettingType.String);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00020200 File Offset: 0x0001E400
		private static string ReplaceWebSettingsWithValues(Regex regex, string sql, SqlQuery.EWebSettingType webSettingType)
		{
			MatchCollection matchCollection = regex.Matches(sql);
			bool flag = matchCollection.Count < 1;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = sql;
				ISettingManager currentInstance = SettingManager.CurrentInstance;
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					string s = match.Value.Substring(2);
					int num;
					bool flag2 = !int.TryParse(s, out num) || num <= 0 || !Enum.IsDefined(typeof(Setting), num);
					if (!flag2)
					{
						Setting setting = (Setting)num;
						string newValue;
						if (webSettingType != SqlQuery.EWebSettingType.Int)
						{
							newValue = "'" + (currentInstance.GetSettingValue<string>(setting) ?? "") + "'";
						}
						else
						{
							newValue = currentInstance.GetSettingValue<int>(setting).ToString();
						}
						text = text.Replace(match.Value, newValue);
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x04000114 RID: 276
		private ReportDAO dao;

		// Token: 0x0200023B RID: 571
		internal enum EWebSettingType
		{
			// Token: 0x040006A3 RID: 1699
			Int,
			// Token: 0x040006A4 RID: 1700
			String
		}
	}
}
