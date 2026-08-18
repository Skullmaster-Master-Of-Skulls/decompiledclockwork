using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001FE RID: 510
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x06001F91 RID: 8081 RVA: 0x000D9F7C File Offset: 0x000D937C
		internal SqlTransaction(SqlInternalConnection internalConnection, SqlConnection con, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			this._isolationLevel = iso;
			this._connection = con;
			if (internalTransaction == null)
			{
				this._internalTransaction = new SqlInternalTransaction(internalConnection, TransactionType.LocalFromAPI, this);
				return;
			}
			this._internalTransaction = internalTransaction;
			this._internalTransaction.InitParent(this);
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x000D9FE0 File Offset: 0x000D93E0
		public new SqlConnection Connection
		{
			get
			{
				if (this.IsZombied)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000DA000 File Offset: 0x000D9400
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000DA014 File Offset: 0x000D9414
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000DA028 File Offset: 0x000D9428
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x000DA044 File Offset: 0x000D9444
		private bool IsYukonPartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x000DA068 File Offset: 0x000D9468
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x000DA08C File Offset: 0x000D948C
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000DA0A0 File Offset: 0x000D94A0
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000DA0D0 File Offset: 0x000D94D0
		public override void Commit()
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Commit|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlTransaction.Commit|API|Correlation> ObjectID%d#, ActivityID %ls", this.ObjectID);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Commit();
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000DA1FC File Offset: 0x000D95FC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				TdsParser target = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					if (!this.IsZombied && !this.IsYukonPartialZombie)
					{
						this._internalTransaction.Dispose();
					}
				}
				catch (OutOfMemoryException e)
				{
					this._connection.Abort(e);
					throw;
				}
				catch (StackOverflowException e2)
				{
					this._connection.Abort(e2);
					throw;
				}
				catch (ThreadAbortException e3)
				{
					this._connection.Abort(e3);
					SqlInternalConnection.BestEffortCleanup(target);
					throw;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000DA2C8 File Offset: 0x000D96C8
		public override void Rollback()
		{
			if (this.IsYukonPartialZombie)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlTransaction.Rollback|ADV> %d# partial zombie no rollback required\n", this.ObjectID);
				}
				this._internalTransaction = null;
				return;
			}
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Rollback|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlTransaction.Rollback|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback();
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000DA410 File Offset: 0x000D9810
		public void Rollback(string transactionName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Rollback|API> %d# transactionName='%ls'", this.ObjectID, transactionName);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback(transactionName);
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				this._isFromAPI = false;
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000DA52C File Offset: 0x000D992C
		public void Save(string savePointName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Save|API> %d# savePointName='%ls'", this.ObjectID, savePointName);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._internalTransaction.Save(savePointName);
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000DA63C File Offset: 0x000D9A3C
		internal void Zombie()
		{
			SqlInternalConnection sqlInternalConnection = this._connection.InnerConnection as SqlInternalConnection;
			if (sqlInternalConnection != null && sqlInternalConnection.IsYukonOrNewer && !this._isFromAPI)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlTransaction.Zombie|ADV> %d# yukon deferred zombie\n", this.ObjectID);
					return;
				}
			}
			else
			{
				this._internalTransaction = null;
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000DA68C File Offset: 0x000D9A8C
		private void ZombieCheck()
		{
			if (this.IsZombied)
			{
				if (this.IsYukonPartialZombie)
				{
					this._internalTransaction = null;
				}
				throw ADP.TransactionZombied(this);
			}
		}

		// Token: 0x040011E2 RID: 4578
		private static int _objectTypeCount;

		// Token: 0x040011E3 RID: 4579
		internal readonly int _objectID = Interlocked.Increment(ref SqlTransaction._objectTypeCount);

		// Token: 0x040011E4 RID: 4580
		internal readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

		// Token: 0x040011E5 RID: 4581
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x040011E6 RID: 4582
		private SqlConnection _connection;

		// Token: 0x040011E7 RID: 4583
		private bool _isFromAPI;
	}
}
