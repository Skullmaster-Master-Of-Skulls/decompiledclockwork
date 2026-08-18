using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace UnivOleDb22
{
	// Token: 0x02000006 RID: 6
	public class UnivDataAdapter : IDisposable
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002904 File Offset: 0x00001904
		~UnivDataAdapter()
		{
			this.Dispose(false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002938 File Offset: 0x00001938
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				dbName dbName = this.myUnivConnection.GetDbName();
				dbName dbName2 = dbName;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 == dbName.MSSQL)
					{
						SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)this.myDataAdapter;
						sqlDataAdapter.Dispose();
					}
				}
				else
				{
					OleDbDataAdapter oleDbDataAdapter = (OleDbDataAdapter)this.myDataAdapter;
					oleDbDataAdapter.Dispose();
				}
				this.myDataAdapter = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029A0 File Offset: 0x000019A0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029B4 File Offset: 0x000019B4
		public object GetDataAdapter()
		{
			return this.myDataAdapter;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029CC File Offset: 0x000019CC
		public UnivConnection Connection
		{
			get
			{
				return this.myUnivConnection;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000024 RID: 36 RVA: 0x000029E4 File Offset: 0x000019E4
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002A1C File Offset: 0x00001A1C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000026 RID: 38 RVA: 0x00002A54 File Offset: 0x00001A54
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002A8C File Offset: 0x00001A8C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002AC4 File Offset: 0x00001AC4
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002AFC File Offset: 0x00001AFC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseErrorHandler databaseError;

		// Token: 0x0600002A RID: 42 RVA: 0x00002B34 File Offset: 0x00001B34
		public bool DoesTableExist(string tableName)
		{
			switch (this.myUnivConnection.GetDbName())
			{
			case dbName.MSAccess:
			{
				this.SelectCommand.CommandText = "IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME=@tablename) ";
				UnivCommand selectCommand = this.SelectCommand;
				selectCommand.CommandText += " SELECT 1 ";
				UnivCommand selectCommand2 = this.SelectCommand;
				selectCommand2.CommandText += " ELSE SELECT 0";
				this.SelectCommand.Parameters.Clear();
				this.SelectCommand.Parameters.Add("@tablename", tableName);
				DataTable dataTable = new DataTable();
				this.Fill(dataTable);
				bool flag = dataTable.Rows.Count > 0;
				if (flag)
				{
					int num = (int)dataTable.Rows[0][0];
					dataTable.Dispose();
					return num == 1;
				}
				dataTable.Dispose();
				return false;
			}
			case dbName.MSSQL:
			{
				this.SelectCommand.CommandText = "SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[" + tableName + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
				DataTable dataTable = new DataTable();
				this.Fill(dataTable);
				return dataTable.Rows.Count > 0;
			}
			case dbName.Sqlite:
			case dbName.SqliteMono:
			{
				this.SelectCommand.CommandText = "SELECT tbl_name FROM sqlite_master WHERE type='table' AND tbl_name='" + tableName + "'";
				DataTable dataTable = new DataTable();
				this.Fill(dataTable);
				return dataTable.Rows.Count > 0;
			}
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CC8 File Offset: 0x00001CC8
		public bool DoesColumnExist(string tableName, string colName)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			bool result;
			if (dbName2 != dbName.MSSQL)
			{
				this.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
				DataTable dataTable = new DataTable();
				this.Fill(dataTable);
				result = dataTable.Columns.Contains(colName);
			}
			else
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
				this.Fill(dataTable);
				result = (dataTable.Rows.Count > 0);
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D7C File Offset: 0x00001D7C
		public static string GetSQLCommandParametersFilledIn(UnivDataAdapter da)
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002E60 File Offset: 0x00001E60
		public virtual void OnDatabaseError(string error)
		{
			bool flag = this.databaseError != null;
			if (flag)
			{
				this.databaseError(error);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E88 File Offset: 0x00001E88
		public virtual void OnDatabaseAccessStarted()
		{
			bool flag = this.databaseAccessStarted != null;
			if (flag)
			{
				this.databaseAccessStarted();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002EB4 File Offset: 0x00001EB4
		public virtual void OnDatabaseAccessEnded()
		{
			bool flag = this.databaseAccessEnded != null;
			if (flag)
			{
				this.databaseAccessEnded();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002EE0 File Offset: 0x00001EE0
		public UnivDataAdapter Clone()
		{
			return new UnivDataAdapter(this.myUnivConnection);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F00 File Offset: 0x00001F00
		public UnivDataReader ExecuteSelectCommandReaderInTransaction(UnivTransaction univTransaction)
		{
			string commandText = this.SelectCommand.CommandText;
			UnivParameterCollection parameters = this.SelectCommand.Parameters;
			this.SelectCommand = new UnivCommand(commandText, this.Connection, univTransaction, parameters);
			return this.SelectCommand.ExecuteReader2();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F4A File Offset: 0x00001F4A
		public UnivDataAdapter()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F6C File Offset: 0x00001F6C
		public UnivDataAdapter(UnivConnection univConnection)
		{
			this.myUnivConnection = univConnection;
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = this.myUnivConnection.GetDbName();
			dbName dbName3 = dbName2;
			if (dbName3 != dbName.MSAccess)
			{
				if (dbName3 == dbName.MSSQL)
				{
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", (SqlConnection)this.myUnivConnection.GetConnection());
					sqlDataAdapter.SelectCommand = new SqlCommand("", (SqlConnection)this.myUnivConnection.GetConnection());
					sqlDataAdapter.InsertCommand = new SqlCommand("", (SqlConnection)this.myUnivConnection.GetConnection());
					sqlDataAdapter.DeleteCommand = new SqlCommand("", (SqlConnection)this.myUnivConnection.GetConnection());
					sqlDataAdapter.UpdateCommand = new SqlCommand("", (SqlConnection)this.myUnivConnection.GetConnection());
					this.myDataAdapter = sqlDataAdapter;
					this.mySelectCommand = new UnivCommand(this.myUnivConnection, sqlDataAdapter.SelectCommand);
					this.myInsertCommand = new UnivCommand(this.myUnivConnection, sqlDataAdapter.InsertCommand);
					this.myDeleteCommand = new UnivCommand(this.myUnivConnection, sqlDataAdapter.DeleteCommand);
					this.myUpdateCommand = new UnivCommand(this.myUnivConnection, sqlDataAdapter.UpdateCommand);
				}
			}
			else
			{
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", (OleDbConnection)this.myUnivConnection.GetConnection());
				oleDbDataAdapter.SelectCommand = new OleDbCommand("", (OleDbConnection)this.myUnivConnection.GetConnection());
				oleDbDataAdapter.UpdateCommand = new OleDbCommand("", (OleDbConnection)this.myUnivConnection.GetConnection());
				oleDbDataAdapter.InsertCommand = new OleDbCommand("", (OleDbConnection)this.myUnivConnection.GetConnection());
				oleDbDataAdapter.DeleteCommand = new OleDbCommand("", (OleDbConnection)this.myUnivConnection.GetConnection());
				this.myDataAdapter = oleDbDataAdapter;
				this.mySelectCommand = new UnivCommand(this.myUnivConnection, oleDbDataAdapter.SelectCommand);
				this.myInsertCommand = new UnivCommand(this.myUnivConnection, oleDbDataAdapter.InsertCommand);
				this.myDeleteCommand = new UnivCommand(this.myUnivConnection, oleDbDataAdapter.DeleteCommand);
				this.myUpdateCommand = new UnivCommand(this.myUnivConnection, oleDbDataAdapter.UpdateCommand);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031D8 File Offset: 0x000021D8
		public int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName)
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			int result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = -1;
				}
				else
				{
					SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)this.myDataAdapter;
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
					sqlDataAdapter.Fill(dataTable);
					this.OnDatabaseAccessEnded();
					bool flag = dataTable.Rows.Count > 0;
					if (flag)
					{
						result = (int)dataTable.Rows[0].ItemArray[0];
					}
					else
					{
						result = -1;
					}
				}
			}
			else
			{
				OleDbDataAdapter oleDbDataAdapter = (OleDbDataAdapter)this.myDataAdapter;
				OleDbConnection oleDbConnection = (OleDbConnection)this.Connection.GetConnection();
				oleDbConnection.Open();
				oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				this.SelectCommand.CommandText = "SELECT @@IDENTITY";
				object obj = oleDbDataAdapter.SelectCommand.ExecuteScalar();
				oleDbConnection.Close();
				bool flag2 = obj is int;
				if (flag2)
				{
					result = (int)obj;
				}
				else
				{
					result = -1;
				}
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003338 File Offset: 0x00002338
		public int Fill(DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(null, "", t, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000336C File Offset: 0x0000236C
		public int Fill(DataTable t)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(null, "", t, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033A0 File Offset: 0x000023A0
		public int Fill(DataSet ds, string tableName)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(ds, tableName, null, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000033D0 File Offset: 0x000023D0
		public int Fill(DataSet ds, string tableName, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(ds, tableName, null, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003400 File Offset: 0x00002400
		private int Fill(DataSet ds, string tableName, DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			int result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					errorMessage = "Unknown Database!";
					result = -1;
				}
				else
				{
					SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)this.myDataAdapter;
					bool flag = ds == null;
					if (flag)
					{
						try
						{
							result = sqlDataAdapter.Fill(t);
							errorMessage = "";
						}
						catch (Exception ex)
						{
							errorMessage = ex.ToString() + Environment.NewLine + this.SelectCommand.ToStringParametersExpanded();
							result = -1;
							this.OnDatabaseError(errorMessage);
						}
					}
					else
					{
						try
						{
							result = sqlDataAdapter.Fill(ds, tableName);
							errorMessage = "";
						}
						catch (Exception ex2)
						{
							errorMessage = ex2.ToString();
							result = -1;
							this.OnDatabaseError(errorMessage);
						}
					}
				}
			}
			else
			{
				OleDbDataAdapter oleDbDataAdapter = (OleDbDataAdapter)this.myDataAdapter;
				errorMessage = "";
				try
				{
					bool flag2 = ds == null;
					if (flag2)
					{
						result = oleDbDataAdapter.Fill(t);
					}
					else
					{
						result = oleDbDataAdapter.Fill(ds, tableName);
					}
				}
				catch (Exception ex3)
				{
					result = 0;
				}
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003554 File Offset: 0x00002554
		private Type FindColumnDataType(DataTable sqliteMasterTable_sql_col, string colName)
		{
			string text = colName.ToLower().Trim();
			bool flag = text.CompareTo("isactivatedcurrentyear") == 0;
			Type result;
			if (flag)
			{
				result = typeof(int);
			}
			else
			{
				bool flag2 = text.CompareTo("controlvalue") == 0;
				if (flag2)
				{
					string text2 = this.SelectCommand.CommandText.ToLower();
					bool flag3 = text2.IndexOf("maininfo") >= 0;
					if (flag3)
					{
						return typeof(int);
					}
					bool flag4 = text2.IndexOf("otherinfo") >= 0;
					if (flag4)
					{
						return typeof(byte[]);
					}
					bool flag5 = text2.IndexOf("datetimeinfo") >= 0;
					if (flag5)
					{
						return typeof(DateTime);
					}
				}
				foreach (object obj in sqliteMasterTable_sql_col.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					bool flag6 = dataRow[0] != DBNull.Value;
					if (flag6)
					{
						string text3 = (string)dataRow[0];
						string[] array = text3.Split(Environment.NewLine.ToCharArray());
						foreach (string text4 in array)
						{
							string text5 = text4.Trim().ToLower();
							int num = text5.IndexOf(" ");
							bool flag7 = num > 0;
							if (flag7)
							{
								string text6 = text5.Substring(0, num).Trim();
								string text7 = text5.Substring(num + 1).Trim();
								bool flag8 = text6.CompareTo(text) == 0;
								if (flag8)
								{
									bool flag9 = text7.IndexOf("integer") >= 0;
									if (flag9)
									{
										return typeof(int);
									}
									bool flag10 = text7.IndexOf("string") >= 0;
									if (flag10)
									{
										return typeof(string);
									}
									bool flag11 = text7.IndexOf("boolean") >= 0;
									if (flag11)
									{
										return typeof(bool);
									}
									bool flag12 = text7.IndexOf("datetime") >= 0;
									if (flag12)
									{
										return typeof(DateTime);
									}
									bool flag13 = text7.IndexOf("blob") >= 0;
									if (flag13)
									{
										return typeof(byte[]);
									}
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003828 File Offset: 0x00002828
		private void FixInt64Cols(DataTable t)
		{
			for (int i = 0; i < t.Columns.Count; i++)
			{
				string columnName = t.Columns[i].ColumnName;
				int num = columnName.IndexOf('.');
				bool flag = num > 0;
				if (flag)
				{
					t.Columns[i].ColumnName = columnName.Substring(num + 1).ToLower();
				}
				else
				{
					t.Columns[i].ColumnName = columnName.ToLower();
				}
			}
			bool flag2 = this.Connection.GetDbName() == dbName.SqliteMono && t.Rows.Count < 1;
			if (flag2)
			{
				string commandText = this.SelectCommand.CommandText;
				this.SelectCommand.CommandText = "SELECT sql FROM sqlite_master WHERE type='table'";
				DataTable dataTable = new DataTable();
				this.Fill(dataTable);
				this.SelectCommand.CommandText = commandText;
				foreach (object obj in t.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					Type type = this.FindColumnDataType(dataTable, dataColumn.ColumnName);
					bool flag3 = type != null;
					if (flag3)
					{
						dataColumn.DataType = type;
					}
				}
			}
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < t.Columns.Count; j++)
			{
				DataColumn dataColumn2 = t.Columns[j];
				bool flag4 = dataColumn2.DataType == typeof(long) || dataColumn2.DataType == typeof(decimal);
				if (flag4)
				{
					arrayList.Add(j);
				}
			}
			bool flag5 = arrayList.Count > 0;
			if (flag5)
			{
				DataTable dataTable2 = t.Copy();
				t.Rows.Clear();
				t.Columns.Clear();
				for (int k = 0; k < dataTable2.Columns.Count; k++)
				{
					bool flag6 = dataTable2.Columns[k].DataType == typeof(long) || dataTable2.Columns[k].DataType == typeof(decimal);
					Type type2;
					if (flag6)
					{
						type2 = typeof(int);
					}
					else
					{
						type2 = dataTable2.Columns[k].DataType;
					}
					t.Columns.Add(dataTable2.Columns[k].ColumnName, type2);
				}
				foreach (object obj2 in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					object[] itemArray = dataRow.ItemArray;
					foreach (object obj3 in arrayList)
					{
						int num2 = (int)obj3;
						bool flag7 = dataRow[num2] != DBNull.Value;
						if (flag7)
						{
							itemArray[num2] = Convert.ToInt32(dataRow[num2]);
						}
					}
					t.Rows.Add(itemArray);
				}
				dataTable2.Dispose();
				t.AcceptChanges();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003BF4 File Offset: 0x00002BF4
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003C0C File Offset: 0x00002C0C
		public UnivCommand SelectCommand
		{
			get
			{
				return this.mySelectCommand;
			}
			set
			{
				this.mySelectCommand = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003C18 File Offset: 0x00002C18
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003C30 File Offset: 0x00002C30
		public UnivCommand InsertCommand
		{
			get
			{
				return this.myInsertCommand;
			}
			set
			{
				this.myInsertCommand = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003C3C File Offset: 0x00002C3C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003C54 File Offset: 0x00002C54
		public UnivCommand UpdateCommand
		{
			get
			{
				return this.myUpdateCommand;
			}
			set
			{
				this.myUpdateCommand = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003C60 File Offset: 0x00002C60
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003C78 File Offset: 0x00002C78
		public UnivCommand DeleteCommand
		{
			get
			{
				return this.myDeleteCommand;
			}
			set
			{
				this.myDeleteCommand = value;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C84 File Offset: 0x00002C84
		public int Update(DataTable dataTable)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			int result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = -1;
				}
				else
				{
					SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)this.myDataAdapter;
					try
					{
						result = sqlDataAdapter.Update(dataTable);
					}
					catch (Exception ex)
					{
						result = 0;
					}
				}
			}
			else
			{
				OleDbDataAdapter oleDbDataAdapter = (OleDbDataAdapter)this.myDataAdapter;
				result = oleDbDataAdapter.Update(dataTable);
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003D08 File Offset: 0x00002D08
		public DataTable GetTableList(out string errmsg)
		{
			DataTable dataTable;
			switch (this.myUnivConnection.GetDbName())
			{
			case dbName.MSAccess:
				try
				{
					OleDbConnection oleDbConnection = (OleDbConnection)this.myUnivConnection.GetConnection();
					oleDbConnection.Open();
					object[] restrictions = new object[]
					{
						null,
						null,
						null,
						"Table"
					};
					DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
					oleDbConnection.Close();
					errmsg = null;
					return oleDbSchemaTable;
				}
				catch (Exception ex)
				{
					errmsg = ex.ToString();
					return null;
				}
				break;
			case dbName.MSSQL:
				this.SelectCommand.CommandText = "SELECT name AS TABLE_NAME FROM sysobjects WHERE type='u'";
				dataTable = new DataTable();
				this.Fill(dataTable, out errmsg);
				return dataTable;
			case dbName.MySQL:
				errmsg = null;
				return null;
			case dbName.Oracle:
			case dbName.Unknown:
				goto IL_E2;
			case dbName.Sqlite:
			case dbName.SqliteMono:
				break;
			default:
				goto IL_E2;
			}
			this.SelectCommand.CommandText = "SELECT tbl_name AS TABLE_NAME FROM sqlite_master WHERE type='table' ORDER BY tbl_name";
			dataTable = new DataTable();
			this.Fill(dataTable, out errmsg);
			return dataTable;
			IL_E2:
			errmsg = null;
			return null;
		}

		// Token: 0x04000012 RID: 18
		private bool disposed = false;

		// Token: 0x04000013 RID: 19
		private UnivConnection myUnivConnection;

		// Token: 0x04000014 RID: 20
		private object myDataAdapter;

		// Token: 0x04000015 RID: 21
		private UnivCommand mySelectCommand;

		// Token: 0x04000016 RID: 22
		private UnivCommand myInsertCommand;

		// Token: 0x04000017 RID: 23
		private UnivCommand myDeleteCommand;

		// Token: 0x04000018 RID: 24
		private UnivCommand myUpdateCommand;

		// Token: 0x04000019 RID: 25
		public ArrayList availableFeatures = null;

		// Token: 0x0400001A RID: 26
		public ArrayList unavailableFeatures = null;
	}
}
