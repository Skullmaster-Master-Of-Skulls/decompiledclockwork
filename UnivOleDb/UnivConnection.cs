using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace UnivOleDb22
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class UnivConnection : IDisposable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002050 File Offset: 0x00001050
		~UnivConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002084 File Offset: 0x00001084
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 == dbName.MSSQL)
					{
						SqlConnection sqlConnection = (SqlConnection)this.myConnection;
						sqlConnection.Dispose();
					}
				}
				else
				{
					OleDbConnection oleDbConnection = (OleDbConnection)this.myConnection;
					oleDbConnection.Dispose();
				}
				this.myConnection = null;
			}
			this.disposed = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E7 File Offset: 0x000010E7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020FC File Offset: 0x000010FC
		public UnivTransaction Transaction
		{
			get
			{
				return this.myTransaction;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002114 File Offset: 0x00001114
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000212C File Offset: 0x0000112C
		public string WildCardChar
		{
			get
			{
				return this.myWildCardChar;
			}
			set
			{
				this.myWildCardChar = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002138 File Offset: 0x00001138
		public string GetDatabaseName()
		{
			switch (this.myDbName)
			{
			case dbName.MSAccess:
				return "MSAccess";
			case dbName.MSSQL:
				return "MSSQL";
			case dbName.MySQL:
				return "MySQL";
			case dbName.Sqlite:
				return "Sqlite";
			case dbName.SqliteMono:
				return "Sqlite.mono";
			case dbName.Postgresql:
				return "Postgresql";
			}
			return "???";
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B0 File Offset: 0x000011B0
		public string GetDatabaseDescription()
		{
			switch (this.myDbName)
			{
			case dbName.MSAccess:
			{
				string path = this.ExtractValue(this.originalConnectionString, "data source");
				string fileName = Path.GetFileName(path);
				string directoryName = Path.GetDirectoryName(path);
				return fileName + " (" + directoryName + ")";
			}
			case dbName.MSSQL:
			{
				string str = this.ExtractValue(this.originalConnectionString, "data source");
				string str2 = this.ExtractValue(this.originalConnectionString, "initial catalog");
				return str2 + " (" + str + ")";
			}
			case dbName.MySQL:
				return "MySQL";
			case dbName.Sqlite:
			case dbName.SqliteMono:
				return this.ExtractValue(this.originalConnectionString, "data source");
			}
			return "???";
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00001290
		private string ExtractValue(string semiColonSeparatedNameEqualsPairs, string nameValuePairName)
		{
			string[] array = semiColonSeparatedNameEqualsPairs.Split(new char[]
			{
				';'
			});
			string strB = nameValuePairName.Trim().ToLower();
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				bool flag = num > 0 && num < text.Length - 1;
				if (flag)
				{
					string text2 = text.Substring(0, num).Trim().ToLower();
					bool flag2 = text2.CompareTo(strB) == 0;
					if (flag2)
					{
						return text.Substring(num + 1);
					}
				}
			}
			return "";
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002340 File Offset: 0x00001340
		public static string GetAccessConnectionString(string fn)
		{
			return UnivConnection.GetAccessConnectionString(fn, "admin", "");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00001364
		public static string GetAccessConnectionString(string fn, string userid, string password)
		{
			return UnivConnection.GetAccessConnectionString2(fn).Replace("=Admin;", "=" + userid + ";").Replace(";Password=", ";Password=" + password);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023AC File Offset: 0x000013AC
		public static string GetAccessConnectionString2(string filename)
		{
			bool flag = IntPtr.Size == 4;
			string result;
			if (flag)
			{
				result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Password=", filename);
			}
			else
			{
				result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};User ID=Admin;Password=", filename);
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E8 File Offset: 0x000013E8
		public UnivConnection(string connectionString)
		{
			this.originalConnectionString = connectionString;
			this.myConnectionString = connectionString;
			int num = connectionString.IndexOf("Provider=Microsoft.Jet");
			int num2 = connectionString.IndexOf("Provider=SQLOLEDB");
			int num3 = connectionString.IndexOf("Provider=MySQL");
			int num4 = connectionString.IndexOf("Provider=SQLite");
			int num5 = connectionString.IndexOf("Provider=SqliteMono");
			bool flag = num >= 0;
			if (flag)
			{
				this.myConnection = new OleDbConnection(connectionString);
				this.myDbName = dbName.MSAccess;
				this.myWildCardChar = "*";
			}
			else
			{
				bool flag2 = num2 >= 0;
				if (flag2)
				{
					connectionString = this.RemoveParameter(connectionString, num2);
					this.myConnection = new SqlConnection(connectionString);
					this.myDbName = dbName.MSSQL;
					this.myWildCardChar = "%";
				}
				else
				{
					bool flag3 = num3 >= 0;
					if (flag3)
					{
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024D4 File Offset: 0x000014D4
		public string GetConcatString(string[] strings)
		{
			string text = "";
			switch (this.myDbName)
			{
			case dbName.MSAccess:
			{
				bool flag = true;
				foreach (string str in strings)
				{
					bool flag2 = !flag;
					if (flag2)
					{
						text += " & ";
					}
					else
					{
						flag = false;
					}
					text += str;
				}
				return text;
			}
			case dbName.MSSQL:
			case dbName.Sqlite:
			case dbName.SqliteMono:
				for (int j = 0; j < strings.Length; j++)
				{
					bool flag3 = j > 0;
					if (flag3)
					{
						text += " + ";
					}
					text += strings[j];
				}
				return text;
			case dbName.MySQL:
			{
				bool flag = true;
				text = "CONCAT(";
				foreach (string str2 in strings)
				{
					bool flag4 = !flag;
					if (flag4)
					{
						text += ", ";
					}
					else
					{
						flag = false;
					}
					text += str2;
				}
				return text + ")";
			}
			case dbName.Postgresql:
				for (int l = 0; l < strings.Length; l++)
				{
					bool flag5 = l > 0;
					if (flag5)
					{
						text += " || ";
					}
					text += strings[l];
				}
				return text;
			}
			return text;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000265C File Offset: 0x0000165C
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002674 File Offset: 0x00001674
		public void Open()
		{
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlConnection sqlConnection = (SqlConnection)this.myConnection;
					bool flag = sqlConnection.State != ConnectionState.Open;
					if (flag)
					{
						try
						{
							sqlConnection.Open();
							this.isOpen = true;
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				OleDbConnection oleDbConnection = (OleDbConnection)this.myConnection;
				bool flag2 = oleDbConnection.State != ConnectionState.Open;
				if (flag2)
				{
					try
					{
						oleDbConnection.Open();
						this.isOpen = true;
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002724 File Offset: 0x00001724
		public void Close()
		{
			this.isOpen = false;
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlConnection sqlConnection = (SqlConnection)this.myConnection;
					bool flag = sqlConnection.State > ConnectionState.Closed;
					if (flag)
					{
						try
						{
							sqlConnection.Close();
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				OleDbConnection oleDbConnection = (OleDbConnection)this.myConnection;
				bool flag2 = oleDbConnection.State > ConnectionState.Closed;
				if (flag2)
				{
					try
					{
						oleDbConnection.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027C8 File Offset: 0x000017C8
		public UnivTransaction BeginTransaction()
		{
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			UnivTransaction result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					SqlConnection sqlConnection = (SqlConnection)this.myConnection;
					SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
					this.myTransaction = new UnivTransaction(this, sqlTransaction);
					result = this.myTransaction;
				}
			}
			else
			{
				OleDbConnection oleDbConnection = (OleDbConnection)this.myConnection;
				OleDbTransaction oledbTransaction = oleDbConnection.BeginTransaction();
				this.myTransaction = new UnivTransaction(this, oledbTransaction);
				result = this.myTransaction;
			}
			return result;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000284C File Offset: 0x0000184C
		public string ConnectionString
		{
			get
			{
				return this.myConnectionString;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002864 File Offset: 0x00001864
		public string RemoveParameter(string connectionString, int startIndex)
		{
			int num = connectionString.IndexOf(";", startIndex + 1);
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				string oldValue = connectionString.Substring(startIndex, num - startIndex + 1);
				connectionString = connectionString.Replace(oldValue, "");
				result = connectionString;
			}
			else
			{
				bool flag2 = startIndex == 0;
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = connectionString.Substring(0, startIndex);
				}
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028D4 File Offset: 0x000018D4
		public object GetConnection()
		{
			return this.myConnection;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028EC File Offset: 0x000018EC
		public dbName GetDbName()
		{
			return this.myDbName;
		}

		// Token: 0x0400000A RID: 10
		private bool disposed = false;

		// Token: 0x0400000B RID: 11
		private dbName myDbName;

		// Token: 0x0400000C RID: 12
		private object myConnection;

		// Token: 0x0400000D RID: 13
		private string myConnectionString;

		// Token: 0x0400000E RID: 14
		private string myWildCardChar;

		// Token: 0x0400000F RID: 15
		public string originalConnectionString;

		// Token: 0x04000010 RID: 16
		private UnivTransaction myTransaction = null;

		// Token: 0x04000011 RID: 17
		private bool isOpen = false;
	}
}
