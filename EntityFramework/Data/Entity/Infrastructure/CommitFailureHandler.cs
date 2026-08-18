using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200019A RID: 410
	public class CommitFailureHandler : TransactionHandler
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003D931 File Offset: 0x0003BB31
		public CommitFailureHandler() : this((DbConnection c) => new TransactionContext(c))
		{
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003D956 File Offset: 0x0003BB56
		public CommitFailureHandler(Func<DbConnection, TransactionContext> transactionContextFactory)
		{
			Check.NotNull<Func<DbConnection, TransactionContext>>(transactionContextFactory, "transactionContextFactory");
			this._transactionContextFactory = transactionContextFactory;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003D987 File Offset: 0x0003BB87
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0003D98F File Offset: 0x0003BB8F
		protected internal TransactionContext TransactionContext { get; private set; }

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003D998 File Offset: 0x0003BB98
		public override void Initialize(ObjectContext context)
		{
			base.Initialize(context);
			DbConnection storeConnection = ((EntityConnection)base.ObjectContext.Connection).StoreConnection;
			this.Initialize(storeConnection);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003D9C9 File Offset: 0x0003BBC9
		public override void Initialize(DbContext context, DbConnection connection)
		{
			base.Initialize(context, connection);
			this.Initialize(connection);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003D9DA File Offset: 0x0003BBDA
		private void Initialize(DbConnection connection)
		{
			this.TransactionContext = this._transactionContextFactory(connection);
			if (this.TransactionContext != null)
			{
				this.TransactionContext.Configuration.LazyLoadingEnabled = false;
				this.TransactionContext.Configuration.AutoDetectChangesEnabled = false;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003DA18 File Offset: 0x0003BC18
		protected virtual int PruningLimit
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003DA1C File Offset: 0x0003BC1C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed && disposing && this.TransactionContext != null)
			{
				if (this._rowsToDelete.Any<TransactionRow>())
				{
					try
					{
						this.PruneTransactionHistory(true, false);
					}
					catch (Exception)
					{
					}
				}
				this.TransactionContext.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003DA78 File Offset: 0x0003BC78
		public override string BuildDatabaseInitializationScript()
		{
			if (this.TransactionContext != null)
			{
				IEnumerable<MigrationStatement> migrationStatements = TransactionContextInitializer<TransactionContext>.GenerateMigrationStatements(this.TransactionContext);
				StringBuilder stringBuilder = new StringBuilder();
				MigratorScriptingDecorator.BuildSqlScript(migrationStatements, stringBuilder);
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		public override void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			if (this.TransactionContext == null || !this.MatchesParentContext(connection, interceptionContext) || interceptionContext.Result == null)
			{
				return;
			}
			Guid transactionId = Guid.NewGuid();
			bool flag = false;
			bool flag2 = false;
			ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
			((EntityConnection)objectContext.Connection).UseStoreTransaction(interceptionContext.Result);
			while (!flag)
			{
				TransactionRow transactionRow = new TransactionRow
				{
					Id = transactionId,
					CreationTime = DateTime.Now
				};
				this._transactions.Add(interceptionContext.Result, transactionRow);
				this.TransactionContext.Transactions.Add(transactionRow);
				try
				{
					objectContext.SaveChangesInternal(SaveOptions.AcceptAllChangesAfterSave, true);
					flag = true;
				}
				catch (UpdateException)
				{
					this._transactions.Remove(interceptionContext.Result);
					this.TransactionContext.Entry<TransactionRow>(transactionRow).State = EntityState.Detached;
					if (flag2)
					{
						throw;
					}
					try
					{
						TransactionRow transactionRow2 = this.TransactionContext.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(new DefaultExecutionStrategy()).FirstOrDefault((TransactionRow t) => t.Id == transactionId);
						if (transactionRow2 == null)
						{
							throw;
						}
						transactionId = Guid.NewGuid();
					}
					catch (EntityCommandExecutionException)
					{
						this.TransactionContext.Database.Initialize(true);
						flag2 = true;
					}
				}
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003DC88 File Offset: 0x0003BE88
		public override void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			TransactionRow transactionRow;
			if (this.TransactionContext == null || (interceptionContext.Connection != null && !this.MatchesParentContext(interceptionContext.Connection, interceptionContext)) || !this._transactions.TryGetValue(transaction, out transactionRow))
			{
				return;
			}
			this._transactions.Remove(transaction);
			if (interceptionContext.Exception == null)
			{
				this.PruneTransactionHistory(transactionRow);
				return;
			}
			TransactionRow transactionRow2 = null;
			try
			{
				transactionRow2 = this.TransactionContext.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(new DefaultExecutionStrategy()).SingleOrDefault((TransactionRow t) => t.Id == transactionRow.Id);
			}
			catch (EntityCommandExecutionException)
			{
			}
			if (transactionRow2 != null)
			{
				interceptionContext.Exception = null;
				this.PruneTransactionHistory(transactionRow);
				return;
			}
			this.TransactionContext.Entry<TransactionRow>(transactionRow).State = EntityState.Detached;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003DDD4 File Offset: 0x0003BFD4
		public override void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			TransactionRow entity;
			if (this.TransactionContext == null || (interceptionContext.Connection != null && !this.MatchesParentContext(interceptionContext.Connection, interceptionContext)) || !this._transactions.TryGetValue(transaction, out entity))
			{
				return;
			}
			this._transactions.Remove(transaction);
			this.TransactionContext.Entry<TransactionRow>(entity).State = EntityState.Detached;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003DE30 File Offset: 0x0003C030
		public override void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			this.RolledBack(transaction, interceptionContext);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public virtual void ClearTransactionHistory()
		{
			foreach (TransactionRow transaction in this.TransactionContext.Transactions)
			{
				this.MarkTransactionForPruning(transaction);
			}
			this.PruneTransactionHistory(true, true);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003DE98 File Offset: 0x0003C098
		public Task ClearTransactionHistoryAsync()
		{
			return this.ClearTransactionHistoryAsync(CancellationToken.None);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003E038 File Offset: 0x0003C238
		public virtual async Task ClearTransactionHistoryAsync(CancellationToken cancellationToken)
		{
			await this.TransactionContext.Transactions.ForEachAsync(new Action<TransactionRow>(this.MarkTransactionForPruning), cancellationToken).WithCurrentCulture();
			await this.PruneTransactionHistoryAsync(true, true, cancellationToken).WithCurrentCulture();
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003E086 File Offset: 0x0003C286
		protected virtual void MarkTransactionForPruning(TransactionRow transaction)
		{
			Check.NotNull<TransactionRow>(transaction, "transaction");
			if (!this._rowsToDelete.Contains(transaction))
			{
				this._rowsToDelete.Add(transaction);
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003E0AF File Offset: 0x0003C2AF
		public void PruneTransactionHistory()
		{
			this.PruneTransactionHistory(true, true);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003E0B9 File Offset: 0x0003C2B9
		public Task PruneTransactionHistoryAsync()
		{
			return this.PruneTransactionHistoryAsync(CancellationToken.None);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003E0C6 File Offset: 0x0003C2C6
		public Task PruneTransactionHistoryAsync(CancellationToken cancellationToken)
		{
			return this.PruneTransactionHistoryAsync(true, true, cancellationToken);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		protected virtual void PruneTransactionHistory(bool force, bool useExecutionStrategy)
		{
			if (this._rowsToDelete.Count > 0 && (force || this._rowsToDelete.Count > this.PruningLimit))
			{
				foreach (TransactionRow transactionRow in this.TransactionContext.Transactions.ToList<TransactionRow>())
				{
					if (this._rowsToDelete.Contains(transactionRow))
					{
						this.TransactionContext.Transactions.Remove(transactionRow);
					}
				}
				ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
				try
				{
					objectContext.SaveChangesInternal(SaveOptions.None, !useExecutionStrategy);
					this._rowsToDelete.Clear();
				}
				finally
				{
					objectContext.AcceptAllChanges();
				}
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		protected virtual async Task PruneTransactionHistoryAsync(bool force, bool useExecutionStrategy, CancellationToken cancellationToken)
		{
			if (this._rowsToDelete.Count > 0 && (force || this._rowsToDelete.Count > this.PruningLimit))
			{
				foreach (TransactionRow transactionRow in this.TransactionContext.Transactions.ToList<TransactionRow>())
				{
					if (this._rowsToDelete.Contains(transactionRow))
					{
						this.TransactionContext.Transactions.Remove(transactionRow);
					}
				}
				ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
				try
				{
					await((IObjectContextAdapter)this.TransactionContext).ObjectContext.SaveChangesInternalAsync(SaveOptions.None, !useExecutionStrategy, cancellationToken).WithCurrentCulture<int>();
					this._rowsToDelete.Clear();
				}
				finally
				{
					objectContext.AcceptAllChanges();
				}
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003E430 File Offset: 0x0003C630
		private void PruneTransactionHistory(TransactionRow transaction)
		{
			this.MarkTransactionForPruning(transaction);
			try
			{
				this.PruneTransactionHistory(false, false);
			}
			catch (DataException)
			{
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003E464 File Offset: 0x0003C664
		public static CommitFailureHandler FromContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return CommitFailureHandler.FromContext(((IObjectContextAdapter)context).ObjectContext);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003E47D File Offset: 0x0003C67D
		public static CommitFailureHandler FromContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return context.TransactionHandler as CommitFailureHandler;
		}

		// Token: 0x040003BA RID: 954
		private readonly Dictionary<DbTransaction, TransactionRow> _transactions = new Dictionary<DbTransaction, TransactionRow>();

		// Token: 0x040003BB RID: 955
		private readonly HashSet<TransactionRow> _rowsToDelete = new HashSet<TransactionRow>();

		// Token: 0x040003BC RID: 956
		private readonly Func<DbConnection, TransactionContext> _transactionContextFactory;
	}
}
