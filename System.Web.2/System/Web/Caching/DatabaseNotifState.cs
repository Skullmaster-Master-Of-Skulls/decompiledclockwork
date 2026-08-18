using System;
using System.Collections;
using System.Data.SqlClient;
using System.Threading;
using System.Web.DataAccess;

namespace System.Web.Caching
{
	// Token: 0x02000891 RID: 2193
	internal class DatabaseNotifState : IDisposable
	{
		// Token: 0x06006701 RID: 26369 RVA: 0x0016AF82 File Offset: 0x00169182
		public void Dispose()
		{
			if (this._sqlConn != null)
			{
				this._sqlConn.Close();
				this._sqlConn = null;
			}
			if (this._timer != null)
			{
				this._timer.Dispose();
				this._timer = null;
			}
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x0016AFB8 File Offset: 0x001691B8
		internal DatabaseNotifState(string database, string connection, int polltime)
		{
			this._database = database;
			this._connectionString = connection;
			this._timer = null;
			this._tables = new Hashtable();
			this._pollExpt = null;
			this._utcTablesUpdated = DateTime.MinValue;
			if (polltime <= 5000)
			{
				this._poolConn = true;
			}
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x0016B00C File Offset: 0x0016920C
		internal void GetConnection(out SqlConnection sqlConn, out SqlCommand sqlCmd)
		{
			sqlConn = null;
			sqlCmd = null;
			if (this._sqlConn != null)
			{
				sqlConn = this._sqlConn;
				sqlCmd = this._sqlCmd;
				this._sqlConn = null;
				this._sqlCmd = null;
				return;
			}
			SqlConnectionHolder sqlConnectionHolder = null;
			try
			{
				sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._connectionString, true);
				sqlCmd = new SqlCommand("dbo.AspNet_SqlCachePollingStoredProcedure", sqlConnectionHolder.Connection);
				sqlConn = sqlConnectionHolder.Connection;
			}
			catch
			{
				if (sqlConnectionHolder != null)
				{
					sqlConnectionHolder.Close();
					sqlConnectionHolder = null;
				}
				sqlCmd = null;
				throw;
			}
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x0016B094 File Offset: 0x00169294
		internal void ReleaseConnection(ref SqlConnection sqlConn, ref SqlCommand sqlCmd, bool error)
		{
			if (sqlConn == null)
			{
				return;
			}
			if (this._poolConn && !error)
			{
				this._sqlConn = sqlConn;
				this._sqlCmd = sqlCmd;
			}
			else
			{
				sqlConn.Close();
			}
			sqlConn = null;
			sqlCmd = null;
		}

		// Token: 0x0400350E RID: 13582
		internal string _database;

		// Token: 0x0400350F RID: 13583
		internal string _connectionString;

		// Token: 0x04003510 RID: 13584
		internal int _rqInCallback;

		// Token: 0x04003511 RID: 13585
		internal bool _notifEnabled;

		// Token: 0x04003512 RID: 13586
		internal bool _init;

		// Token: 0x04003513 RID: 13587
		internal Timer _timer;

		// Token: 0x04003514 RID: 13588
		internal Hashtable _tables;

		// Token: 0x04003515 RID: 13589
		internal Exception _pollExpt;

		// Token: 0x04003516 RID: 13590
		internal int _pollSqlError;

		// Token: 0x04003517 RID: 13591
		internal SqlConnection _sqlConn;

		// Token: 0x04003518 RID: 13592
		internal SqlCommand _sqlCmd;

		// Token: 0x04003519 RID: 13593
		internal bool _poolConn;

		// Token: 0x0400351A RID: 13594
		internal DateTime _utcTablesUpdated;

		// Token: 0x0400351B RID: 13595
		internal int _refCount;
	}
}
