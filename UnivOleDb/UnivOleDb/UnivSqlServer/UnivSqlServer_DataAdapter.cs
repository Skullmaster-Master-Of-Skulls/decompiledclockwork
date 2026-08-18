using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TechnoPro.Common.ClientManager.Core.UnivThroughServer;
using TechnoPro.Common.ClientManager.ICore.UnivThroughServer;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class UnivSqlServer_DataAdapter : UnivDataAdapter, IDisposable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000691C File Offset: 0x0000591C
		public UnivSqlServer_DataAdapter(UnivSqlServer_Connection univConnection)
		{
			this.myUnivConnection = univConnection;
			SqlConnection connection = this.myUnivConnection.Connection;
			this.myDataAdapter = new SqlDataAdapter("", connection);
			this.myDataAdapter.SelectCommand = new SqlCommand("", connection);
			this.myDataAdapter.InsertCommand = new SqlCommand("", connection);
			this.myDataAdapter.DeleteCommand = new SqlCommand("", connection);
			this.myDataAdapter.UpdateCommand = new SqlCommand("", connection);
			this.mySelectCommand = new UnivSqlServer_Command(this.myUnivConnection, this.myDataAdapter.SelectCommand);
			this.myInsertCommand = new UnivSqlServer_Command(this.myUnivConnection, this.myDataAdapter.InsertCommand);
			this.myDeleteCommand = new UnivSqlServer_Command(this.myUnivConnection, this.myDataAdapter.DeleteCommand);
			this.myUpdateCommand = new UnivSqlServer_Command(this.myUnivConnection, this.myDataAdapter.UpdateCommand);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006A28 File Offset: 0x00005A28
		~UnivSqlServer_DataAdapter()
		{
			this.Dispose(false);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006A5C File Offset: 0x00005A5C
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			this.disposed = true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006A78 File Offset: 0x00005A78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000138 RID: 312 RVA: 0x00006A8C File Offset: 0x00005A8C
		// (remove) Token: 0x06000139 RID: 313 RVA: 0x00006AC4 File Offset: 0x00005AC4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600013A RID: 314 RVA: 0x00006AFC File Offset: 0x00005AFC
		// (remove) Token: 0x0600013B RID: 315 RVA: 0x00006B34 File Offset: 0x00005B34
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600013C RID: 316 RVA: 0x00006B6C File Offset: 0x00005B6C
		// (remove) Token: 0x0600013D RID: 317 RVA: 0x00006BA4 File Offset: 0x00005BA4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseErrorHandler databaseError;

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006BDC File Offset: 0x00005BDC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006C0C File Offset: 0x00005C0C
		public ArrayList availableFeatures
		{
			get
			{
				bool flag = this.myAvailableFeatures == null;
				if (flag)
				{
					this.myAvailableFeatures = new ArrayList();
				}
				return this.myAvailableFeatures;
			}
			set
			{
				this.myAvailableFeatures = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006C18 File Offset: 0x00005C18
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006C48 File Offset: 0x00005C48
		public ArrayList unavailableFeatures
		{
			get
			{
				bool flag = this.myUnavailableFeatures == null;
				if (flag)
				{
					this.myUnavailableFeatures = new ArrayList();
				}
				return this.myUnavailableFeatures;
			}
			set
			{
				this.myUnavailableFeatures = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006C54 File Offset: 0x00005C54
		public UnivConnection Connection
		{
			get
			{
				return this.myUnivConnection;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006C6C File Offset: 0x00005C6C
		public UnivCommand CreateCommand(string sql)
		{
			return new UnivSqlServer_Command(sql, this.myUnivConnection, this.myUnivConnection.SqlTransaction);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006C98 File Offset: 0x00005C98
		public bool DoesTableExist(string tableName)
		{
			this.SelectCommand.CommandText = "SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[" + tableName + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
			DataTable dataTable = new DataTable();
			this.SelectCommand.Parameters.Clear();
			this.Fill(dataTable);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006CF4 File Offset: 0x00005CF4
		public bool DoesColumnExist(string tableName, string colName)
		{
			this.SelectCommand.CommandText = string.Concat(new string[]
			{
				"SELECT * from syscolumns WHERE id=object_id('",
				tableName,
				"') AND name='",
				colName,
				"'"
			});
			DataTable dataTable = new DataTable();
			this.SelectCommand.Parameters.Clear();
			this.Fill(dataTable);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006D6C File Offset: 0x00005D6C
		public string GetSQLCommandParametersFilledIn(UnivDataAdapter da)
		{
			string text = da.SelectCommand.CommandText;
			string pattern = "(?<=@)\\w+";
			Regex regex = new Regex(pattern);
			MatchCollection matchCollection = regex.Matches(text);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text2 = "@" + match.Value;
				bool success = match.Success;
				if (success)
				{
					string newValue;
					try
					{
						newValue = da.SelectCommand.Parameters.Value(text2).ToString();
					}
					catch
					{
						newValue = "?";
					}
					text = text.Replace(text2, newValue);
				}
			}
			return text;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006E50 File Offset: 0x00005E50
		public UnivDataAdapter Clone()
		{
			return new UnivSqlServer_DataAdapter(this.myUnivConnection);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006E70 File Offset: 0x00005E70
		private void CopyTable(ref DataTable dataTable, DataTable t)
		{
			bool flag = dataTable != null && t != null;
			if (flag)
			{
				dataTable.Rows.Clear();
				dataTable.Columns.Clear();
				foreach (object obj in t.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataTable.Columns.Add(dataColumn.ColumnName, dataColumn.DataType, dataColumn.Expression);
				}
				foreach (object obj2 in t.Rows)
				{
					DataRow row = (DataRow)obj2;
					dataTable.ImportRow(row);
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006F68 File Offset: 0x00005F68
		public int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.FillReturnIdentity(ref t, autoIncrementColName, tableName);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006FA0 File Offset: 0x00005FA0
		public int FillReturnIdentity(ref DataTable dataTable, string autoIncrementColName, string tableName)
		{
			string text;
			return this.FillReturnIdentity(dataTable, autoIncrementColName, tableName, out text);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006FC0 File Offset: 0x00005FC0
		public int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName, out string emsg)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.FillReturnIdentity(ref t, autoIncrementColName, tableName, out emsg);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006FF8 File Offset: 0x00005FF8
		public int FillReturnIdentity(ref DataTable dataTable, string autoIncrementColName, string tableName, out string emsg)
		{
			this.OnDatabaseAccessStarted();
			bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
			if (runThroughClockWorkServer)
			{
				string sqlCommandText;
				List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.SelectCommand.CommandText, this.SelectCommand.Parameters, out sqlCommandText);
				IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
				univThroughServerClientManager.FillReturnIdentity(ref dataTable, tableName, autoIncrementColName, sqlCommandText, parameters);
			}
			else
			{
				try
				{
					UnivCommand selectCommand = this.SelectCommand;
					selectCommand.CommandText = string.Concat(new string[]
					{
						selectCommand.CommandText,
						"; SELECT ",
						autoIncrementColName,
						" FROM ",
						tableName,
						" WHERE ",
						autoIncrementColName,
						"=@@identity"
					});
					using (SqlConnection sqlConnection = new SqlConnection(this.myUnivConnection.ConnectionString))
					{
						using (SqlDataAdapter sqlDataAdapter = this.CreateDataAdapter(sqlConnection))
						{
							sqlDataAdapter.Fill(dataTable);
						}
					}
				}
				catch (Exception ex)
				{
					this.OnDatabaseAccessEnded();
					emsg = ex.ToString();
					return -1;
				}
			}
			this.OnDatabaseAccessEnded();
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				result = (int)dataTable.Rows[0].ItemArray[0];
			}
			else
			{
				emsg = "Can't find identity";
				result = -1;
			}
			emsg = "";
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseAccessStarted()
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseAccessEnded()
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007188 File Offset: 0x00006188
		public int Fill(DataTable dataTable, out string errorMessage)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.Fill(ref t, out errorMessage);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000071BC File Offset: 0x000061BC
		public int Fill(ref DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			DataSet dataSet = null;
			string text;
			int result = this.Fill(ref dataSet, "", ref t, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000071F4 File Offset: 0x000061F4
		public int Fill(DataTable dataTable)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.Fill(ref t);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007228 File Offset: 0x00006228
		public int Fill(ref DataTable t)
		{
			DataSet dataSet = null;
			string text;
			int result = this.Fill(ref dataSet, "", ref t, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007258 File Offset: 0x00006258
		public int Fill(ref DataSet ds, string tableName)
		{
			return this.Fill(ref ds, tableName);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007274 File Offset: 0x00006274
		public int Fill(DataSet ds, string tableName)
		{
			this.OnDatabaseAccessStarted();
			DataTable dataTable = null;
			string text;
			int result = this.Fill(ref ds, tableName, ref dataTable, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000072A8 File Offset: 0x000062A8
		public int Fill(DataSet ds, string tableName, out string errorMessage)
		{
			return this.Fill(ref ds, tableName, out errorMessage);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000072C4 File Offset: 0x000062C4
		public int Fill(ref DataSet ds, string tableName, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			DataTable dataTable = null;
			string text;
			int result = this.Fill(ref ds, tableName, ref dataTable, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000072F8 File Offset: 0x000062F8
		public int Fill(DataSet ds, string tableName, DataTable dataTable, out string errorMessage)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.Fill(ref ds, tableName, ref t, out errorMessage);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007330 File Offset: 0x00006330
		private SqlDataAdapter CreateDataAdapter(SqlConnection conn)
		{
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", conn);
			sqlDataAdapter.SelectCommand = new SqlCommand("", conn);
			sqlDataAdapter.InsertCommand = new SqlCommand("", conn);
			sqlDataAdapter.UpdateCommand = new SqlCommand("", conn);
			sqlDataAdapter.DeleteCommand = new SqlCommand("", conn);
			this.CopyCommandAndParameters(this.SelectCommand, sqlDataAdapter.SelectCommand);
			this.CopyCommandAndParameters(this.UpdateCommand, sqlDataAdapter.UpdateCommand);
			this.CopyCommandAndParameters(this.InsertCommand, sqlDataAdapter.InsertCommand);
			this.CopyCommandAndParameters(this.DeleteCommand, sqlDataAdapter.DeleteCommand);
			return sqlDataAdapter;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000073E4 File Offset: 0x000063E4
		private void CopyCommandAndParameters(UnivCommand cmdOld, SqlCommand cmdNew)
		{
			cmdNew.CommandText = cmdOld.CommandText;
			SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)cmdOld.Parameters.ParameterCollection;
			foreach (object obj in sqlParameterCollection)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				cmdNew.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.SqlDbType, sqlParameter.Size, sqlParameter.Direction, sqlParameter.IsNullable, sqlParameter.Precision, sqlParameter.Scale, sqlParameter.SourceColumn, sqlParameter.SourceVersion, sqlParameter.Value));
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000074A4 File Offset: 0x000064A4
		public int Fill(ref DataSet ds, string tableName, ref DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			bool flag = ds == null;
			int result;
			if (flag)
			{
				try
				{
					bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
					if (runThroughClockWorkServer)
					{
						string sqlCommandText;
						List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.SelectCommand.CommandText, this.SelectCommand.Parameters, out sqlCommandText);
						IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
						result = univThroughServerClientManager.Fill(ref t, sqlCommandText, parameters);
					}
					else
					{
						using (SqlConnection sqlConnection = new SqlConnection(this.myUnivConnection.ConnectionString))
						{
							using (SqlDataAdapter sqlDataAdapter = this.CreateDataAdapter(sqlConnection))
							{
								bool flag2 = this.SelectCommand != null;
								if (flag2)
								{
									sqlDataAdapter.SelectCommand.CommandTimeout = this.SelectCommand.CommandTimeout;
								}
								result = sqlDataAdapter.Fill(t);
							}
						}
					}
					errorMessage = "";
				}
				catch (Exception ex)
				{
					errorMessage = ex.ToString() + Environment.NewLine + UnivOleDbFactory.ToStringParametersExpanded(this.SelectCommand);
					result = -1;
					this.OnDatabaseError(errorMessage);
				}
			}
			else
			{
				try
				{
					bool runThroughClockWorkServer2 = this.myUnivConnection.RunThroughClockWorkServer;
					if (runThroughClockWorkServer2)
					{
						string sqlCommandText2;
						List<CommonParameter> parameters2 = UnivOleDbFactory.ConvertParameters(this.SelectCommand.CommandText, this.SelectCommand.Parameters, out sqlCommandText2);
						IUnivThroughServerClientManager univThroughServerClientManager2 = new UnivThroughServerClientManager();
						result = univThroughServerClientManager2.Fill(ref ds, tableName, sqlCommandText2, parameters2);
					}
					else
					{
						using (SqlConnection sqlConnection2 = new SqlConnection(this.myUnivConnection.ConnectionString))
						{
							using (SqlDataAdapter sqlDataAdapter2 = this.CreateDataAdapter(sqlConnection2))
							{
								bool flag3 = this.SelectCommand != null;
								if (flag3)
								{
									sqlDataAdapter2.SelectCommand.CommandTimeout = this.SelectCommand.CommandTimeout;
								}
								result = sqlDataAdapter2.Fill(ds, tableName);
							}
						}
					}
					errorMessage = "";
				}
				catch (Exception ex2)
				{
					errorMessage = ex2.ToString();
					result = -1;
					this.OnDatabaseError(errorMessage);
				}
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseError(string errorMessage)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000076F8 File Offset: 0x000066F8
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007710 File Offset: 0x00006710
		public UnivCommand SelectCommand
		{
			get
			{
				return this.mySelectCommand;
			}
			set
			{
				this.mySelectCommand = (UnivSqlServer_Command)value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007720 File Offset: 0x00006720
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007738 File Offset: 0x00006738
		public UnivCommand InsertCommand
		{
			get
			{
				return this.myInsertCommand;
			}
			set
			{
				this.myInsertCommand = (UnivSqlServer_Command)value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007748 File Offset: 0x00006748
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007760 File Offset: 0x00006760
		public UnivCommand UpdateCommand
		{
			get
			{
				return this.myUpdateCommand;
			}
			set
			{
				this.myUpdateCommand = (UnivSqlServer_Command)value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007770 File Offset: 0x00006770
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007788 File Offset: 0x00006788
		public UnivCommand DeleteCommand
		{
			get
			{
				return this.myDeleteCommand;
			}
			set
			{
				this.myDeleteCommand = (UnivSqlServer_Command)value;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007798 File Offset: 0x00006798
		public int Update(DataTable dataTable)
		{
			DataTable t = (dataTable == null) ? null : dataTable.Clone();
			int result = this.Update(ref t);
			this.CopyTable(ref dataTable, t);
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000077CC File Offset: 0x000067CC
		public int Update(ref DataTable dataTable)
		{
			this.OnDatabaseAccessStarted();
			int result;
			try
			{
				bool runThroughClockWorkServer = this.myUnivConnection.RunThroughClockWorkServer;
				if (runThroughClockWorkServer)
				{
					string sqlCommandText;
					List<CommonParameter> parameters = UnivOleDbFactory.ConvertParameters(this.UpdateCommand.CommandText, this.UpdateCommand.Parameters, out sqlCommandText);
					IUnivThroughServerClientManager univThroughServerClientManager = new UnivThroughServerClientManager();
					result = univThroughServerClientManager.Update(ref dataTable, sqlCommandText, parameters);
				}
				else
				{
					using (SqlConnection sqlConnection = new SqlConnection(this.myUnivConnection.ConnectionString))
					{
						using (SqlDataAdapter sqlDataAdapter = this.CreateDataAdapter(sqlConnection))
						{
							result = sqlDataAdapter.Update(dataTable);
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = 0;
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000078B0 File Offset: 0x000068B0
		public DataTable GetTableList(out string errmsg)
		{
			this.SelectCommand.CommandText = "SELECT name AS TABLE_NAME FROM sysobjects WHERE type='u'";
			DataTable dataTable = new DataTable();
			this.Fill(dataTable, out errmsg);
			return dataTable;
		}

		// Token: 0x04000041 RID: 65
		private UnivSqlServer_Connection myUnivConnection;

		// Token: 0x04000042 RID: 66
		private SqlDataAdapter myDataAdapter;

		// Token: 0x04000043 RID: 67
		private UnivSqlServer_Command mySelectCommand;

		// Token: 0x04000044 RID: 68
		private UnivSqlServer_Command myInsertCommand;

		// Token: 0x04000045 RID: 69
		private UnivSqlServer_Command myDeleteCommand;

		// Token: 0x04000046 RID: 70
		private UnivSqlServer_Command myUpdateCommand;

		// Token: 0x04000047 RID: 71
		private bool disposed = false;

		// Token: 0x04000048 RID: 72
		private ArrayList myAvailableFeatures;

		// Token: 0x04000049 RID: 73
		private ArrayList myUnavailableFeatures;
	}
}
