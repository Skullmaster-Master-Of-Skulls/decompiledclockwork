using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using UnivOleDb;

namespace TechnoPro.Common.DAO.UnivDataAccess.Impl
{
	// Token: 0x02000002 RID: 2
	public class UnivDataAccessDAO : IUnivDataAccessDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002076 File Offset: 0x00000276
		public UnivDataAdapter Da
		{
			get
			{
				UnivDataAdapter result;
				if ((result = this.da) == null)
				{
					result = (this.da = this.GetDa());
				}
				return result;
			}
			set
			{
				this.da = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000207F File Offset: 0x0000027F
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002087 File Offset: 0x00000287
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public UnivDataAccessDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BD File Offset: 0x000002BD
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020C5 File Offset: 0x000002C5
		public OperationContext OpContext { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020D0 File Offset: 0x000002D0
		private UnivDataAdapter GetDa()
		{
			string text = this.DatabaseManager.ConnectionString;
			if (text.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(text.IndexOf(";") + 1);
			}
			text = string.Format("{0}{1}", "Provider=SQLOLEDB;", text);
			return UnivOleDbFactory.CreateConnection(text).CreateDataAdapter();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		private void SetupDa(QueryRequest Query)
		{
			this.Da.SelectCommand.CommandText = Query.Sql;
			this.Da.SelectCommand.Parameters.Clear();
			foreach (CommonParameter commonParameter in Query.Parameters)
			{
				if (commonParameter.Value == null)
				{
					commonParameter.Value = DBNull.Value;
				}
				if (commonParameter.Value == DBNull.Value)
				{
					if (commonParameter.DbType == null)
					{
						commonParameter.DbType = new DbType?(DbType.String);
					}
					this.Da.SelectCommand.Parameters.AddNull(commonParameter.Name);
				}
				else
				{
					this.Da.SelectCommand.Parameters.Add(commonParameter.Name, commonParameter.Value);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002224 File Offset: 0x00000424
		public QueryResult Fill(QueryRequest Query)
		{
			int num = 0;
			QueryResult result;
			try
			{
				this.SetupDa(Query);
				num = 1;
				DataTable dataTable = new DataTable("t");
				string text;
				int id = this.Da.Fill(dataTable, out text);
				num = 2;
				if (!string.IsNullOrEmpty(text))
				{
					throw new Exception(text);
				}
				num = 3;
				CWLogger.Logger.Trace("UnivDataAccessDAO:Fill:Success:Columns={0}:Rows={1}", (dataTable == null) ? "NULL" : dataTable.Columns.Count.ToString(), (dataTable == null) ? "NULL" : dataTable.Rows.Count.ToString());
				result = new QueryResult
				{
					DataTable = dataTable,
					ErrorMessage = null,
					Id = id
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("UnivDataAccessDAO:Fill:loc={0}:Error={1}", num.ToString(), ex.ToString());
				result = new QueryResult
				{
					DataTable = new DataTable("t"),
					ErrorMessage = ex.ToString(),
					Id = 0
				};
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002330 File Offset: 0x00000530
		public bool DoesTableExist(string tableName)
		{
			bool result;
			try
			{
				string sql = "SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[" + tableName + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
				this.SetupDa(new QueryRequest
				{
					Sql = sql,
					Parameters = new List<CommonParameter>()
				});
				DataTable dataTable = new DataTable("t");
				this.Da.Fill(dataTable);
				result = (dataTable.Rows.Count > 0);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023AC File Offset: 0x000005AC
		public bool DoesColumnExist(string tableName, string colName)
		{
			bool result;
			try
			{
				string sql = string.Concat(new string[]
				{
					"SELECT * from syscolumns WHERE id=object_id('",
					tableName,
					"') AND name='",
					colName,
					"'"
				});
				this.SetupDa(new QueryRequest
				{
					Sql = sql,
					Parameters = new List<CommonParameter>()
				});
				DataTable dataTable = new DataTable("t");
				this.Da.Fill(dataTable);
				result = (dataTable.Rows.Count > 0);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002444 File Offset: 0x00000644
		public string GetSQLCommandParametersFilledIn(QueryRequest Query)
		{
			string text = Query.Sql;
			foreach (object obj in new Regex("(?<=@)\\w+").Matches(text))
			{
				Match match = (Match)obj;
				string n = "@" + match.Value;
				if (match.Success)
				{
					string newValue;
					try
					{
						CommonParameter commonParameter = Query.Parameters.Find((CommonParameter pp) => pp.Name.Equals(n, StringComparison.OrdinalIgnoreCase));
						newValue = ((commonParameter == null) ? "??" : commonParameter.Value.ToString());
					}
					catch
					{
						newValue = "?";
					}
					text = text.Replace(n, newValue);
				}
			}
			return text;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000252C File Offset: 0x0000072C
		public QueryResult FillReturnIdentity(QueryRequest Query, string autoIncrementColName, string tableName)
		{
			string sql = string.Concat(new string[]
			{
				Query.Sql,
				"; SELECT ",
				autoIncrementColName,
				" FROM ",
				tableName,
				" WHERE ",
				autoIncrementColName,
				"=@@identity"
			});
			DataTable dataTable = new DataTable("t");
			try
			{
				this.SetupDa(new QueryRequest
				{
					Sql = sql,
					Parameters = Query.Parameters
				});
				this.Da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				return new QueryResult
				{
					DataTable = new DataTable("t"),
					ErrorMessage = ex.ToString(),
					Id = 0
				};
			}
			if (dataTable.Rows.Count > 0)
			{
				int id = (int)dataTable.Rows[0].ItemArray[0];
				return new QueryResult
				{
					DataTable = dataTable,
					ErrorMessage = "",
					Id = id
				};
			}
			return new QueryResult
			{
				DataTable = dataTable,
				ErrorMessage = "Can't find identity",
				Id = 0
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002658 File Offset: 0x00000858
		public QueryResult ExecuteScalar(QueryRequest Query)
		{
			this.SetupDa(Query);
			object obj = this.Da.SelectCommand.ExecuteScalar();
			int id;
			if (obj != null && obj is int)
			{
				id = (int)obj;
			}
			else
			{
				id = 0;
			}
			return new QueryResult
			{
				DataTable = null,
				ErrorMessage = "",
				Id = id
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026B4 File Offset: 0x000008B4
		public QueryResult ExecuteNonQuery(QueryRequest Query)
		{
			this.SetupDa(Query);
			int id = this.Da.SelectCommand.ExecuteNonQuery2();
			return new QueryResult
			{
				DataTable = null,
				ErrorMessage = "",
				Id = id
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026F7 File Offset: 0x000008F7
		public SqlDataReader ExecuteReader(QueryRequest Query)
		{
			this.SetupDa(Query);
			return (SqlDataReader)this.Da.SelectCommand.ExecuteReader2().GetNativeDataReader();
		}

		// Token: 0x04000001 RID: 1
		private UnivDataAdapter da;
	}
}
