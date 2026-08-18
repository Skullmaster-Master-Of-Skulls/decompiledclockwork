using System;
using System.Data.OleDb;
using System.Diagnostics;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000022 RID: 34
	public class UnivMSAccess_Command : UnivCommand, IDisposable
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007E7C File Offset: 0x00006E7C
		public UnivMSAccess_Command(string commandText, UnivMSAccess_Connection univConnection, UnivMSAccess_Transaction univTransaction)
		{
			this.myUnivConnection = univConnection;
			this.myCommand = new OleDbCommand(commandText, this.myUnivConnection.Connection, univTransaction.Transaction);
			this.myUnivParameters = new UnivMSAccess_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007EDC File Offset: 0x00006EDC
		public UnivMSAccess_Command(string commandText, UnivMSAccess_Connection univConnection, UnivMSAccess_Transaction univTransaction, UnivMSAccess_ParameterCollection univParameters)
		{
			this.myUnivConnection = univConnection;
			this.myUnivParameters = univParameters;
			UnivMSAccess_Transaction univMSAccess_Transaction = (univTransaction != null) ? univTransaction : null;
			bool flag = univMSAccess_Transaction == null || univMSAccess_Transaction.Transaction == null;
			if (flag)
			{
				this.myCommand = new OleDbCommand(commandText, this.myUnivConnection.Connection);
			}
			else
			{
				this.myCommand = new OleDbCommand(commandText, this.myUnivConnection.Connection, univMSAccess_Transaction.Transaction);
			}
			this.myUnivParameters = new UnivMSAccess_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007F7C File Offset: 0x00006F7C
		public UnivMSAccess_Command(UnivMSAccess_Connection univConnection, OleDbCommand command)
		{
			this.myUnivConnection = univConnection;
			bool flag = command == null;
			if (flag)
			{
				this.myCommand = new OleDbCommand("", this.myUnivConnection.Connection);
			}
			else
			{
				this.myCommand = command;
			}
			this.myUnivParameters = new UnivMSAccess_ParameterCollection(this.myUnivConnection, this, this.myCommand.Parameters);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007FE8 File Offset: 0x00006FE8
		~UnivMSAccess_Command()
		{
			this.Dispose(false);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000801C File Offset: 0x0000701C
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.myCommand.Dispose();
				this.myCommand = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000804C File Offset: 0x0000704C
		public string ToStringParametersExpanded()
		{
			return UnivOleDbFactory.ToStringParametersExpanded(this);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008064 File Offset: 0x00007064
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008078 File Offset: 0x00007078
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00008095 File Offset: 0x00007095
		public string CommandText
		{
			get
			{
				return this.myCommand.CommandText;
			}
			set
			{
				this.myCommand.CommandText = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000080A8 File Offset: 0x000070A8
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000080C5 File Offset: 0x000070C5
		public int CommandTimeout
		{
			get
			{
				return this.myCommand.CommandTimeout;
			}
			set
			{
				this.myCommand.CommandTimeout = value;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600019B RID: 411 RVA: 0x000080D8 File Offset: 0x000070D8
		// (remove) Token: 0x0600019C RID: 412 RVA: 0x00008110 File Offset: 0x00007110
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessStarted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600019D RID: 413 RVA: 0x00008148 File Offset: 0x00007148
		// (remove) Token: 0x0600019E RID: 414 RVA: 0x00008180 File Offset: 0x00007180
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DatabaseAccessStartedEnded databaseAccessEnded;

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000081B8 File Offset: 0x000071B8
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000081D8 File Offset: 0x000071D8
		public UnivTransaction Transaction
		{
			get
			{
				return this.myUnivConnection.Transaction;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.myCommand.Transaction = null;
				}
				else
				{
					this.myCommand.Transaction = ((UnivMSAccess_Transaction)value).Transaction;
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008214 File Offset: 0x00007214
		public UnivParameterCollection Parameters
		{
			get
			{
				return this.myUnivParameters;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000822C File Offset: 0x0000722C
		public UnivDataReader ExecuteReader2()
		{
			this.OnDatabaseAccessStarted();
			OleDbDataReader reader = this.myCommand.ExecuteReader();
			return new UnivMSAccess_DataReader(reader);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008258 File Offset: 0x00007258
		public int ExecuteNonQuery(out string emsg)
		{
			int result;
			try
			{
				int num = this.ExecuteNonQuery();
				emsg = null;
				result = num;
			}
			catch (Exception ex)
			{
				emsg = ex.ToString();
				result = 0;
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008294 File Offset: 0x00007294
		public int ExecuteNonQuery()
		{
			this.OnDatabaseAccessStarted();
			this.myUnivConnection.Open();
			int result = this.myCommand.ExecuteNonQuery();
			this.myUnivConnection.Close();
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000082DC File Offset: 0x000072DC
		public int ExecuteNonQuery2(out string emsg)
		{
			this.OnDatabaseAccessStarted();
			int result;
			try
			{
				result = this.myCommand.ExecuteNonQuery();
				emsg = null;
			}
			catch (Exception ex)
			{
				result = 0;
				emsg = ex.ToString();
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008330 File Offset: 0x00007330
		public int ExecuteNonQuery2()
		{
			this.OnDatabaseAccessStarted();
			int result;
			try
			{
				result = this.myCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				result = 0;
			}
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008378 File Offset: 0x00007378
		public object ExecuteScalar()
		{
			this.OnDatabaseAccessStarted();
			object result = this.myCommand.ExecuteScalar();
			this.OnDatabaseAccessEnded();
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006527 File Offset: 0x00005527
		public void OnDatabaseAccessStarted()
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006527 File Offset: 0x00005527
		public void OnDatabaseAccessEnded()
		{
		}

		// Token: 0x04000053 RID: 83
		private UnivMSAccess_Connection myUnivConnection;

		// Token: 0x04000054 RID: 84
		private OleDbCommand myCommand;

		// Token: 0x04000055 RID: 85
		private UnivMSAccess_ParameterCollection myUnivParameters;

		// Token: 0x04000056 RID: 86
		private bool disposed = false;
	}
}
