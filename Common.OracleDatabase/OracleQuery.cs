using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkLogger;
using Oracle.DataAccess.Client;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;

namespace TechnoPro.Common.OracleDatabase
{
	// Token: 0x02000002 RID: 2
	public class OracleQuery
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static DataTable ExecuteQuery(string ConnectionString, OracleQueryRequest QueryRequest)
		{
			DataTable dataTable = new DataTable("q");
			try
			{
				using (OracleConnection oracleConnection = new OracleConnection(ConnectionString))
				{
					oracleConnection.Open();
					using (OracleCommand oracleCommand = new OracleCommand(QueryRequest.Sql, oracleConnection))
					{
						oracleCommand.CommandType = ((QueryRequest.QueryType == eOracleQueryType.StoredProcedure) ? CommandType.StoredProcedure : CommandType.Text);
						Type typeFromHandle = typeof(OracleDbType);
						foreach (TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions.OracleParameter oracleParameter in (QueryRequest.Parameters ?? new List<TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions.OracleParameter>()))
						{
							Oracle.DataAccess.Client.OracleParameter oracleParameter2 = oracleCommand.CreateParameter();
							oracleParameter2.ParameterName = oracleParameter.Name;
							oracleParameter2.OracleDbType = ((!string.IsNullOrEmpty(oracleParameter.OracleDbType) && Enum.IsDefined(typeFromHandle, oracleParameter.OracleDbType)) ? ((OracleDbType)Enum.Parse(typeFromHandle, oracleParameter.OracleDbType)) : OracleDbType.Varchar2);
							if (oracleParameter.IsOutParameter)
							{
								oracleParameter2.Direction = ParameterDirection.Output;
							}
							else
							{
								oracleParameter2.Value = oracleParameter.Value;
							}
							oracleCommand.Parameters.Add(oracleParameter2);
						}
						using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection))
						{
							dataTable.Load(oracleDataReader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Trace("Common.OracleDatabase.OracleQuery:ExecuteQuery:err={0}", ex.ToString());
			}
			return dataTable;
		}
	}
}
