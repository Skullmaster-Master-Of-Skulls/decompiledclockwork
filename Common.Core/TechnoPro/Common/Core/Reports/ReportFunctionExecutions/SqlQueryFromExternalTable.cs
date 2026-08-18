using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Entity.Reports;
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
	// Token: 0x0200009F RID: 159
	public class SqlQueryFromExternalTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0002051B File Offset: 0x0001E71B
		public SqlQueryFromExternalTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00020536 File Offset: 0x0001E736
		public SqlQueryFromExternalTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00020554 File Offset: 0x0001E754
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0002055C File Offset: 0x0001E75C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000583 RID: 1411 RVA: 0x00020568 File Offset: 0x0001E768
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			int num = defaultFunctionParameter.IndexOf("`");
			bool flag = num > 0;
			if (!flag)
			{
				throw new Exception("Incorrect function parameters for sql query from external table: " + defaultFunctionParameter);
			}
			string text = defaultFunctionParameter.Substring(0, num).Trim().ToLower();
			bool flag2 = text.StartsWith("factory");
			string providerType;
			if (flag2)
			{
				text = "factory";
				providerType = text.Substring(8);
			}
			else
			{
				providerType = "";
			}
			bool flag3 = Enum.IsDefined(typeof(eExternalQueryDatabaseType), text);
			if (!flag3)
			{
				throw new Exception("Undefined dbtype: " + text);
			}
			eExternalQueryDatabaseType dbType = (eExternalQueryDatabaseType)Enum.Parse(typeof(eExternalQueryDatabaseType), text);
			int num2 = defaultFunctionParameter.IndexOf("`", num + 1);
			bool flag4 = num2 >= 0;
			if (flag4)
			{
				string text2 = defaultFunctionParameter.Substring(num + 1, num2 - num - 1);
				string sql = defaultFunctionParameter.Substring(num2 + 1);
				bool flag5 = text2 != null;
				if (flag5)
				{
					bool flag6 = text2 == "10000101";
					if (flag6)
					{
						ISettingManager settingManager = new SettingManager(this.OpContext);
						text2 = settingManager.GetSettingValue<string>(Setting.CUSTOM_Password_Setting_1);
					}
				}
				DataTable table = this.dao.RunReportSqlExternal(dbType, providerType, text2, sql, SqlQuery.ExtractReportParameters(CurrentWholeReportResult));
				Result.Data.Table = table;
				return;
			}
			throw new Exception("Incorrect / invalid function parameters for sql query from external table: " + defaultFunctionParameter);
		}

		// Token: 0x0400011A RID: 282
		private ReportDAO dao;
	}
}
