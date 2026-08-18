using System;
using System.Data;
using System.Data.SqlClient;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class UnivSqlServer_Connection : UnivConnection, IDisposable
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000652C File Offset: 0x0000552C
		public UnivSqlServer_Connection(string connectionString)
		{
			this.myConnectionString = connectionString;
			this.originalConnectionString = connectionString;
			int startIndex = connectionString.IndexOf("Provider=SQLOLEDB");
			this.myConnectionString = UnivOleDbFactory.RemoveParameter(connectionString, startIndex);
			this.myConnection = new SqlConnection(this.myConnectionString);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006590 File Offset: 0x00005590
		public UnivSqlServer_Connection(string connectionString, bool noDirectDbAccess)
		{
			this.myConnectionString = connectionString;
			this.originalConnectionString = connectionString;
			int startIndex = connectionString.IndexOf("Provider=SQLOLEDB");
			this.myConnectionString = UnivOleDbFactory.RemoveParameter(connectionString, startIndex);
			this.myConnection = new SqlConnection(this.myConnectionString);
			if (noDirectDbAccess)
			{
				this.runThroughClockWorkServer = true;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006600 File Offset: 0x00005600
		~UnivSqlServer_Connection()
		{
			this.Dispose(false);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006634 File Offset: 0x00005634
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.myConnection.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000665C File Offset: 0x0000565C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006670 File Offset: 0x00005670
		public string GetDatabaseName()
		{
			return "MSSQL";
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006688 File Offset: 0x00005688
		public string GetTempTablePrefix()
		{
			return "#";
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000066A0 File Offset: 0x000056A0
		public string GetDatabaseDescription()
		{
			string str = UnivOleDbFactory.ExtractValue(this.originalConnectionString, "data source");
			string str2 = UnivOleDbFactory.ExtractValue(this.originalConnectionString, "initial catalog");
			return str2 + " (" + str + ")";
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000066E8 File Offset: 0x000056E8
		public string GetConcatString(string[] strings)
		{
			string text = "";
			for (int i = 0; i < strings.Length; i++)
			{
				bool flag = i > 0;
				if (flag)
				{
					text += " + ";
				}
				text += strings[i];
			}
			return text;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006738 File Offset: 0x00005738
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006750 File Offset: 0x00005750
		public UnivTransaction Transaction
		{
			get
			{
				return this.myUnivTransaction;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006768 File Offset: 0x00005768
		public UnivDataAdapter CreateDataAdapter()
		{
			return new UnivSqlServer_DataAdapter(this);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006780 File Offset: 0x00005780
		public string OriginalConnectionString
		{
			get
			{
				return this.originalConnectionString;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006798 File Offset: 0x00005798
		public void Open()
		{
			bool flag = this.runThroughClockWorkServer;
			if (!flag)
			{
				SqlConnection sqlConnection = this.myConnection;
				bool flag2 = sqlConnection.State != ConnectionState.Open;
				if (flag2)
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

		// Token: 0x0600012C RID: 300 RVA: 0x000067F8 File Offset: 0x000057F8
		public void Close()
		{
			bool flag = this.runThroughClockWorkServer;
			if (!flag)
			{
				this.isOpen = false;
				SqlConnection sqlConnection = this.myConnection;
				bool flag2 = sqlConnection.State > ConnectionState.Closed;
				if (flag2)
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

		// Token: 0x0600012D RID: 301 RVA: 0x00006854 File Offset: 0x00005854
		public UnivTransaction BeginTransaction()
		{
			bool flag = this.runThroughClockWorkServer;
			UnivTransaction result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SqlTransaction sqlTransaction = this.myConnection.BeginTransaction();
				this.myUnivTransaction = new UnivSqlServer_Transaction(this, sqlTransaction);
				result = this.myUnivTransaction;
			}
			return result;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006898 File Offset: 0x00005898
		public UnivSqlServer_Transaction SqlTransaction
		{
			get
			{
				return this.myUnivTransaction;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000068B0 File Offset: 0x000058B0
		public string ConnectionString
		{
			get
			{
				return this.myConnectionString;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000068C8 File Offset: 0x000058C8
		public SqlConnection Connection
		{
			get
			{
				return this.myConnection;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000068E0 File Offset: 0x000058E0
		public object GetConnection()
		{
			return this.myConnection;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000068F8 File Offset: 0x000058F8
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006910 File Offset: 0x00005910
		public bool RunThroughClockWorkServer
		{
			get
			{
				return this.runThroughClockWorkServer;
			}
			set
			{
				this.runThroughClockWorkServer = value;
			}
		}

		// Token: 0x04000039 RID: 57
		private string myConnectionString;

		// Token: 0x0400003A RID: 58
		private string originalConnectionString;

		// Token: 0x0400003B RID: 59
		private SqlConnection myConnection;

		// Token: 0x0400003C RID: 60
		private const string myWildCardChar = "%";

		// Token: 0x0400003D RID: 61
		private bool isOpen = false;

		// Token: 0x0400003E RID: 62
		private bool disposed = false;

		// Token: 0x0400003F RID: 63
		private UnivSqlServer_Transaction myUnivTransaction;

		// Token: 0x04000040 RID: 64
		private bool runThroughClockWorkServer = false;
	}
}
