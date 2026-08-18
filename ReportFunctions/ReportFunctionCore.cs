using System;
using System.Data;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000022 RID: 34
	public class ReportFunctionCore
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00039934 File Offset: 0x00038934
		public static DataTable LoadReportTitles(string connectionString, string reportIdsCommaSeparated)
		{
			DataTable result;
			using (UnivConnection univConnection = UnivOleDbFactory.CreateConnection(connectionString))
			{
				UnivDataAdapter da = univConnection.CreateDataAdapter();
				result = ReportFunctionCore.LoadReportTitles(da, reportIdsCommaSeparated);
			}
			return result;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00039980 File Offset: 0x00038980
		public static DataTable LoadReportTitles(UnivDataAdapter da, string reportIdsCommaSeparated)
		{
			da.SelectCommand.CommandText = "SELECT searchinfoid,title,description FROM searchinfo WHERE searchinfoid IN (SELECT orderid AS searchinfoid FROM splitorderids(@rids,',')) ORDER BY title";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@rids", reportIdsCommaSeparated);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000399DC File Offset: 0x000389DC
		public static void LogReportExecution(UnivDataAdapter da, int pid, int rid)
		{
			if (DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.ReportExecutionLog))
			{
				string commandText = "INSERT INTO SearchInfoLog (personid,searchinfoid) VALUES (@pid,@rid)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.SelectCommand.Parameters.Add("@rid", rid);
				da.Fill(new DataTable());
			}
		}
	}
}
