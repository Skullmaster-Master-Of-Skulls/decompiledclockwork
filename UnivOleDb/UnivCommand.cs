using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace UnivOleDb22
{
	// Token: 0x02000007 RID: 7
	public class UnivCommand : IDisposable
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003E14 File Offset: 0x00002E14
		~UnivCommand()
		{
			this.Dispose(false);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003E48 File Offset: 0x00002E48
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
						SqlCommand sqlCommand = (SqlCommand)this.myCommand;
						sqlCommand.Dispose();
					}
				}
				else
				{
					OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
					oleDbCommand.Dispose();
				}
				this.myCommand = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003EB0 File Offset: 0x00002EB0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000049 RID: 73 RVA: 0x00003EC4 File Offset: 0x00002EC4
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00003EFC File Offset: 0x00002EFC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003F34 File Offset: 0x00002F34
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x00003F6C File Offset: 0x00002F6C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003FA4 File Offset: 0x00002FA4
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003FC4 File Offset: 0x00002FC4
		public UnivTransaction Transaction
		{
			get
			{
				return this.myUnivConnection.Transaction;
			}
			set
			{
				dbName dbName = this.myUnivConnection.GetDbName();
				dbName dbName2 = dbName;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 == dbName.MSSQL)
					{
						SqlCommand sqlCommand = (SqlCommand)this.myCommand;
						bool flag = value == null;
						if (flag)
						{
							sqlCommand.Transaction = null;
						}
						else
						{
							sqlCommand.Transaction = value.TransactionSQLServer;
						}
					}
				}
				else
				{
					OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
					bool flag2 = value == null;
					if (flag2)
					{
						oleDbCommand.Transaction = null;
					}
					else
					{
						oleDbCommand.Transaction = value.TransactionOleDb;
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000404C File Offset: 0x0000304C
		public virtual void OnDatabaseAccessStarted()
		{
			bool flag = this.databaseAccessStarted != null;
			if (flag)
			{
				this.databaseAccessStarted();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004078 File Offset: 0x00003078
		public virtual void OnDatabaseAccessEnded()
		{
			bool flag = this.databaseAccessEnded != null;
			if (flag)
			{
				this.databaseAccessEnded();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000040A4 File Offset: 0x000030A4
		public object CreateCommand()
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					result = new SqlCommand("", (SqlConnection)this.myUnivConnection.GetConnection());
				}
			}
			else
			{
				result = new OleDbCommand("", (OleDbConnection)this.myUnivConnection.GetConnection());
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000410C File Offset: 0x0000310C
		public UnivCommand(string commandText, UnivConnection univConnection, UnivTransaction univTransaction)
		{
			this.myUnivConnection = univConnection;
			this.myCommandText = commandText;
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlCommand sqlCommand = new SqlCommand(commandText, (SqlConnection)univConnection.GetConnection(), (SqlTransaction)univTransaction.Transaction);
					this.myCommand = sqlCommand;
					this.myUnivParameters = new UnivParameterCollection(this.myUnivConnection, this, sqlCommand.Parameters);
				}
			}
			else
			{
				OleDbCommand oleDbCommand = new OleDbCommand(commandText, (OleDbConnection)univConnection.GetConnection(), (OleDbTransaction)univTransaction.Transaction);
				this.myCommand = oleDbCommand;
				this.myUnivParameters = new UnivParameterCollection(this.myUnivConnection, this, oleDbCommand.Parameters);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000041CC File Offset: 0x000031CC
		public UnivCommand(string commandText, UnivConnection univConnection, UnivTransaction univTransaction, UnivParameterCollection univParameters)
		{
			this.myUnivConnection = univConnection;
			this.myUnivParameters = univParameters;
			this.myCommandText = commandText;
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					bool flag = univTransaction == null || univTransaction.Transaction == null;
					if (flag)
					{
						SqlCommand sqlCommand = new SqlCommand(commandText, (SqlConnection)univConnection.GetConnection());
						this.myCommand = sqlCommand;
					}
					else
					{
						SqlCommand sqlCommand = new SqlCommand(commandText, (SqlConnection)univConnection.GetConnection(), (SqlTransaction)univTransaction.Transaction);
						this.myCommand = sqlCommand;
					}
				}
			}
			else
			{
				OleDbCommand oleDbCommand = new OleDbCommand(commandText, (OleDbConnection)univConnection.GetConnection(), (OleDbTransaction)univTransaction.Transaction);
				this.myCommand = oleDbCommand;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000429C File Offset: 0x0000329C
		public UnivCommand(UnivConnection univConnection, object command)
		{
			this.myUnivConnection = univConnection;
			this.myCommandText = "";
			bool flag = command == null;
			if (flag)
			{
				command = this.CreateCommand();
			}
			this.myCommand = command;
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					this.myUnivParameters = new UnivParameterCollection(this.myUnivConnection, this, sqlCommand.Parameters);
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				this.myUnivParameters = new UnivParameterCollection(this.myUnivConnection, this, oleDbCommand.Parameters);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000434C File Offset: 0x0000334C
		public object GetCommand()
		{
			return this.myCommand;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004364 File Offset: 0x00003364
		public UnivConnection GetUnivConnection()
		{
			return this.myUnivConnection;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000437C File Offset: 0x0000337C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00004394 File Offset: 0x00003394
		public string CommandText
		{
			get
			{
				return this.GetCommandText();
			}
			set
			{
				this.SetCommandText(value);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000043A0 File Offset: 0x000033A0
		public string GetCommandText()
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			string result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = "";
				}
				else
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					result = sqlCommand.CommandText;
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				result = oleDbCommand.CommandText;
			}
			return result;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004400 File Offset: 0x00003400
		public UnivParameterCollection Parameters
		{
			get
			{
				return this.myUnivParameters;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004418 File Offset: 0x00003418
		private void SetCommandText(string commandText)
		{
			this.myCommandText = commandText;
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					sqlCommand.CommandText = commandText;
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				oleDbCommand.CommandText = commandText;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004474 File Offset: 0x00003474
		public object ExecuteReader()
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					result = sqlCommand.ExecuteReader();
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				result = oleDbCommand.ExecuteReader();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000044E0 File Offset: 0x000034E0
		public UnivDataReader ExecuteReader2()
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object dataReader;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					dataReader = null;
				}
				else
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					dataReader = sqlCommand.ExecuteReader();
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				dataReader = oleDbCommand.ExecuteReader();
			}
			this.OnDatabaseAccessEnded();
			return new UnivDataReader(this.myUnivConnection.GetDbName(), dataReader);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000455C File Offset: 0x0000355C
		public int ExecuteNonQuery()
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			int result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = 0;
				}
				else
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					this.myUnivConnection.Open();
					int num = sqlCommand.ExecuteNonQuery();
					this.myUnivConnection.Close();
					result = num;
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				this.myUnivConnection.Open();
				int num2 = oleDbCommand.ExecuteNonQuery();
				this.myUnivConnection.Close();
				result = num2;
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004604 File Offset: 0x00003604
		public int ExecuteNonQuery2()
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			int result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = 0;
				}
				else
				{
					try
					{
						SqlCommand sqlCommand = (SqlCommand)this.myCommand;
						int num = sqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (Exception ex)
					{
						result = 0;
					}
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				int num2 = oleDbCommand.ExecuteNonQuery();
				result = num2;
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004698 File Offset: 0x00003698
		public object ExecuteScalar()
		{
			this.OnDatabaseAccessStarted();
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = 0;
				}
				else
				{
					SqlCommand sqlCommand = (SqlCommand)this.myCommand;
					result = sqlCommand.ExecuteScalar();
				}
			}
			else
			{
				OleDbCommand oleDbCommand = (OleDbCommand)this.myCommand;
				result = oleDbCommand.ExecuteScalar();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000470C File Offset: 0x0000370C
		public string ToStringParametersExpanded()
		{
			string text = this.CommandText;
			for (int i = 0; i < this.myUnivParameters.Count; i++)
			{
				string text2 = this.myUnivParameters.ParameterName(i);
				object obj = this.myUnivParameters.Value(i);
				bool flag = obj is byte[];
				string str;
				if (flag)
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					str = utf8Encoding.GetString((byte[])obj);
				}
				else
				{
					bool flag2 = obj == null || obj == DBNull.Value;
					if (flag2)
					{
						str = "NULL";
					}
					else
					{
						str = obj.ToString();
					}
				}
				text = text.Replace(text2, text2 + " '" + str + "'");
			}
			return text;
		}

		// Token: 0x0400001E RID: 30
		private bool disposed = false;

		// Token: 0x0400001F RID: 31
		private UnivConnection myUnivConnection;

		// Token: 0x04000020 RID: 32
		private object myCommand;

		// Token: 0x04000021 RID: 33
		private string myCommandText;

		// Token: 0x04000022 RID: 34
		private UnivParameterCollection myUnivParameters;
	}
}
