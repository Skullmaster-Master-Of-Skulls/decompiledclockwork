using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000023 RID: 35
	public class UnivMSAccess_Connection : UnivConnection, IDisposable
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000083A5 File Offset: 0x000073A5
		public UnivMSAccess_Connection(string connectionString)
		{
			this.myConnectionString = connectionString;
			this.originalConnectionString = connectionString;
			this.myConnection = new OleDbConnection(connectionString);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000083E0 File Offset: 0x000073E0
		~UnivMSAccess_Connection()
		{
			this.Dispose(false);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008414 File Offset: 0x00007414
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.myConnection.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000843C File Offset: 0x0000743C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008450 File Offset: 0x00007450
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00008468 File Offset: 0x00007468
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

		// Token: 0x060001B0 RID: 432 RVA: 0x00008474 File Offset: 0x00007474
		public string GetTempTablePrefix()
		{
			return "#";
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000848C File Offset: 0x0000748C
		public string GetDatabaseName()
		{
			return "MSAccess";
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000084A4 File Offset: 0x000074A4
		public string GetDatabaseDescription()
		{
			string path = UnivOleDbFactory.ExtractValue(this.originalConnectionString, "data source");
			string fileName = Path.GetFileName(path);
			string directoryName = Path.GetDirectoryName(path);
			return fileName + " (" + directoryName + ")";
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000084E8 File Offset: 0x000074E8
		public string GetConcatString(string[] strings)
		{
			string text = "";
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008544 File Offset: 0x00007544
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000855C File Offset: 0x0000755C
		public UnivTransaction Transaction
		{
			get
			{
				return this.myUnivTransaction;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008574 File Offset: 0x00007574
		public UnivDataAdapter CreateDataAdapter()
		{
			return new UnivMSAccess_DataAdapter(this);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000858C File Offset: 0x0000758C
		public string OriginalConnectionString
		{
			get
			{
				return this.originalConnectionString;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000085A4 File Offset: 0x000075A4
		public void Open()
		{
			OleDbConnection oleDbConnection = this.myConnection;
			bool flag = oleDbConnection.State != ConnectionState.Open;
			if (flag)
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

		// Token: 0x060001B9 RID: 441 RVA: 0x000085F4 File Offset: 0x000075F4
		public void Close()
		{
			this.isOpen = false;
			OleDbConnection oleDbConnection = this.myConnection;
			bool flag = oleDbConnection.State > ConnectionState.Closed;
			if (flag)
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

		// Token: 0x060001BA RID: 442 RVA: 0x00008640 File Offset: 0x00007640
		public UnivTransaction BeginTransaction()
		{
			OleDbTransaction oleDbTransaction = this.myConnection.BeginTransaction();
			this.myUnivTransaction = new UnivMSAccess_Transaction(this, oleDbTransaction);
			return this.myUnivTransaction;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008674 File Offset: 0x00007674
		public UnivMSAccess_Transaction AccessTransaction
		{
			get
			{
				return this.myUnivTransaction;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000868C File Offset: 0x0000768C
		public string ConnectionString
		{
			get
			{
				return this.myConnectionString;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000086A4 File Offset: 0x000076A4
		public OleDbConnection Connection
		{
			get
			{
				return this.myConnection;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000086BC File Offset: 0x000076BC
		public object GetConnection()
		{
			return this.myConnection;
		}

		// Token: 0x04000059 RID: 89
		private string myConnectionString;

		// Token: 0x0400005A RID: 90
		private string originalConnectionString;

		// Token: 0x0400005B RID: 91
		private OleDbConnection myConnection;

		// Token: 0x0400005C RID: 92
		private const string myWildCardChar = "%";

		// Token: 0x0400005D RID: 93
		private bool isOpen = false;

		// Token: 0x0400005E RID: 94
		private bool disposed = false;

		// Token: 0x0400005F RID: 95
		private UnivMSAccess_Transaction myUnivTransaction;

		// Token: 0x04000060 RID: 96
		private bool runThroughClockWorkServer = false;
	}
}
