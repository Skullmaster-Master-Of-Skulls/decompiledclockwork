using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000024 RID: 36
	public class UnivMSAccess_DataAdapter : UnivDataAdapter, IDisposable
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000086D4 File Offset: 0x000076D4
		public UnivMSAccess_DataAdapter(UnivMSAccess_Connection univConnection)
		{
			this.myUnivConnection = univConnection;
			OleDbConnection connection = this.myUnivConnection.Connection;
			this.myDataAdapter = new OleDbDataAdapter("", connection);
			this.myDataAdapter.SelectCommand = new OleDbCommand("", connection);
			this.myDataAdapter.InsertCommand = new OleDbCommand("", connection);
			this.myDataAdapter.DeleteCommand = new OleDbCommand("", connection);
			this.myDataAdapter.UpdateCommand = new OleDbCommand("", connection);
			this.mySelectCommand = new UnivMSAccess_Command(this.myUnivConnection, this.myDataAdapter.SelectCommand);
			this.myInsertCommand = new UnivMSAccess_Command(this.myUnivConnection, this.myDataAdapter.InsertCommand);
			this.myDeleteCommand = new UnivMSAccess_Command(this.myUnivConnection, this.myDataAdapter.DeleteCommand);
			this.myUpdateCommand = new UnivMSAccess_Command(this.myUnivConnection, this.myDataAdapter.UpdateCommand);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000087E0 File Offset: 0x000077E0
		~UnivMSAccess_DataAdapter()
		{
			this.Dispose(false);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008814 File Offset: 0x00007814
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.myDataAdapter.Dispose();
				this.myDataAdapter = null;
			}
			this.disposed = true;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008843 File Offset: 0x00007843
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060001C3 RID: 451 RVA: 0x00008858 File Offset: 0x00007858
		// (remove) Token: 0x060001C4 RID: 452 RVA: 0x00008890 File Offset: 0x00007890
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060001C5 RID: 453 RVA: 0x000088C8 File Offset: 0x000078C8
		// (remove) Token: 0x060001C6 RID: 454 RVA: 0x00008900 File Offset: 0x00007900
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060001C7 RID: 455 RVA: 0x00008938 File Offset: 0x00007938
		// (remove) Token: 0x060001C8 RID: 456 RVA: 0x00008970 File Offset: 0x00007970
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseErrorHandler databaseError;

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000089A8 File Offset: 0x000079A8
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000089D8 File Offset: 0x000079D8
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000089E4 File Offset: 0x000079E4
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00008A14 File Offset: 0x00007A14
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008A20 File Offset: 0x00007A20
		public UnivConnection Connection
		{
			get
			{
				return this.myUnivConnection;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008A38 File Offset: 0x00007A38
		public UnivCommand CreateCommand(string sql)
		{
			return new UnivMSAccess_Command(sql, this.myUnivConnection, this.myUnivConnection.AccessTransaction);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008A64 File Offset: 0x00007A64
		public bool DoesTableExist(string tableName)
		{
			this.SelectCommand.CommandText = "SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[" + tableName + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
			DataTable dataTable = new DataTable();
			this.Fill(dataTable);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008AB0 File Offset: 0x00007AB0
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
			this.Fill(dataTable);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008B18 File Offset: 0x00007B18
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

		// Token: 0x060001D2 RID: 466 RVA: 0x00008BFC File Offset: 0x00007BFC
		public UnivDataAdapter Clone()
		{
			return new UnivMSAccess_DataAdapter(this.myUnivConnection);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008C1C File Offset: 0x00007C1C
		public int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName)
		{
			string text;
			return this.FillReturnIdentity(dataTable, autoIncrementColName, tableName, out text);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008C3C File Offset: 0x00007C3C
		public int FillReturnIdentity(DataTable dataTable, string autoIncrementColName, string tableName, out string emsg)
		{
			this.OnDatabaseAccessStarted();
			try
			{
				this.myUnivConnection.Open();
				this.myDataAdapter.SelectCommand.ExecuteNonQuery();
				this.myDataAdapter.SelectCommand.CommandText = "SELECT MAX(" + autoIncrementColName + ") FROM " + tableName;
				this.myDataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				emsg = ex.ToString();
				dataTable = null;
				return 0;
			}
			finally
			{
				try
				{
					this.myUnivConnection.Close();
					this.OnDatabaseAccessEnded();
				}
				catch
				{
				}
			}
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				result = (int)dataTable.Rows[0].ItemArray[0];
			}
			else
			{
				emsg = "Couldn't find identity";
				result = -1;
			}
			emsg = "";
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseAccessStarted()
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseAccessEnded()
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008D54 File Offset: 0x00007D54
		public int Fill(DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(null, "", t, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008D88 File Offset: 0x00007D88
		public int Fill(DataTable t)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(null, "", t, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008DBC File Offset: 0x00007DBC
		public int Fill(DataSet ds, string tableName)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(ds, tableName, null, out text);
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008DEC File Offset: 0x00007DEC
		public int Fill(DataSet ds, string tableName, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			string text;
			int result = this.Fill(ds, tableName, null, out text);
			errorMessage = text;
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008E1C File Offset: 0x00007E1C
		public int Fill(DataSet ds, string tableName, DataTable t, out string errorMessage)
		{
			this.OnDatabaseAccessStarted();
			bool flag = ds == null;
			int result;
			if (flag)
			{
				try
				{
					result = this.myDataAdapter.Fill(t);
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
					result = this.myDataAdapter.Fill(ds, tableName);
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

		// Token: 0x060001DC RID: 476 RVA: 0x00006527 File Offset: 0x00005527
		private void OnDatabaseError(string errorMessage)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008EE0 File Offset: 0x00007EE0
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00008EF8 File Offset: 0x00007EF8
		public UnivCommand SelectCommand
		{
			get
			{
				return this.mySelectCommand;
			}
			set
			{
				this.mySelectCommand = (UnivMSAccess_Command)value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008F08 File Offset: 0x00007F08
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008F20 File Offset: 0x00007F20
		public UnivCommand InsertCommand
		{
			get
			{
				return this.myInsertCommand;
			}
			set
			{
				this.myInsertCommand = (UnivMSAccess_Command)value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008F30 File Offset: 0x00007F30
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008F48 File Offset: 0x00007F48
		public UnivCommand UpdateCommand
		{
			get
			{
				return this.myUpdateCommand;
			}
			set
			{
				this.myUpdateCommand = (UnivMSAccess_Command)value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008F58 File Offset: 0x00007F58
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008F70 File Offset: 0x00007F70
		public UnivCommand DeleteCommand
		{
			get
			{
				return this.myDeleteCommand;
			}
			set
			{
				this.myDeleteCommand = (UnivMSAccess_Command)value;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008F80 File Offset: 0x00007F80
		public int Update(DataTable dataTable)
		{
			this.OnDatabaseAccessStarted();
			OleDbDataAdapter oleDbDataAdapter = this.myDataAdapter;
			int result;
			try
			{
				result = oleDbDataAdapter.Update(dataTable);
			}
			catch (Exception ex)
			{
				result = 0;
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008FCC File Offset: 0x00007FCC
		public DataTable GetTableList(out string errmsg)
		{
			this.SelectCommand.CommandText = "SELECT name AS TABLE_NAME FROM sysobjects WHERE type='u'";
			DataTable dataTable = new DataTable();
			this.Fill(dataTable, out errmsg);
			return dataTable;
		}

		// Token: 0x04000061 RID: 97
		private UnivMSAccess_Connection myUnivConnection;

		// Token: 0x04000062 RID: 98
		private OleDbDataAdapter myDataAdapter;

		// Token: 0x04000063 RID: 99
		private UnivMSAccess_Command mySelectCommand;

		// Token: 0x04000064 RID: 100
		private UnivMSAccess_Command myInsertCommand;

		// Token: 0x04000065 RID: 101
		private UnivMSAccess_Command myDeleteCommand;

		// Token: 0x04000066 RID: 102
		private UnivMSAccess_Command myUpdateCommand;

		// Token: 0x04000067 RID: 103
		private bool disposed = false;

		// Token: 0x04000068 RID: 104
		private ArrayList myAvailableFeatures;

		// Token: 0x04000069 RID: 105
		private ArrayList myUnavailableFeatures;
	}
}
