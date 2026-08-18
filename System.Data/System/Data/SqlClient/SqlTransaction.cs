using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000311 RID: 785
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x060028F4 RID: 10484 RVA: 0x002B2FE8 File Offset: 0x002B23E8
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

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x002B3058 File Offset: 0x002B2458
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

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x002B3078 File Offset: 0x002B2478
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x002B3098 File Offset: 0x002B2498
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x002B30B8 File Offset: 0x002B24B8
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x002B30D8 File Offset: 0x002B24D8
		private bool IsYukonPartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x002B3108 File Offset: 0x002B2508
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x002B3138 File Offset: 0x002B2538
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x002B3158 File Offset: 0x002B2558
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

		// Token: 0x060028FD RID: 10493 RVA: 0x002B3188 File Offset: 0x002B2588
		public override void Commit()
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Commit|API> %d#", this.ObjectID);
			SNIHandle target = null;
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

		// Token: 0x060028FE RID: 10494 RVA: 0x002B32A8 File Offset: 0x002B26A8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				SNIHandle target = null;
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

		// Token: 0x060028FF RID: 10495 RVA: 0x002B3378 File Offset: 0x002B2778
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
			SNIHandle target = null;
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

		// Token: 0x06002900 RID: 10496 RVA: 0x002B34B8 File Offset: 0x002B28B8
		public void Rollback(string transactionName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Rollback|API> %d# transactionName='%ls'", this.ObjectID, transactionName);
			SNIHandle target = null;
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

		// Token: 0x06002901 RID: 10497 RVA: 0x002B35D8 File Offset: 0x002B29D8
		public void Save(string savePointName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlTransaction.Save|API> %d# savePointName='%ls'", this.ObjectID, savePointName);
			SNIHandle target = null;
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

		// Token: 0x06002902 RID: 10498 RVA: 0x002B36E8 File Offset: 0x002B2AE8
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

		// Token: 0x06002903 RID: 10499 RVA: 0x002B3738 File Offset: 0x002B2B38
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

		// Token: 0x040019AB RID: 6571
		private static int _objectTypeCount;

		// Token: 0x040019AC RID: 6572
		internal readonly int _objectID = Interlocked.Increment(ref SqlTransaction._objectTypeCount);

		// Token: 0x040019AD RID: 6573
		internal readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

		// Token: 0x040019AE RID: 6574
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x040019AF RID: 6575
		private SqlConnection _connection;

		// Token: 0x040019B0 RID: 6576
		private bool _isFromAPI;
	}
}
