using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B6 RID: 1462
	internal class TransactionRpcFacet
	{
		// Token: 0x06003928 RID: 14632 RVA: 0x000DD8E6 File Offset: 0x000DBAE6
		internal TransactionRpcFacet()
		{
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000DD8F5 File Offset: 0x000DBAF5
		internal TransactionRpcFacet(ref MessageRpc rpc)
		{
			this.rpc = rpc;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000DD910 File Offset: 0x000DBB10
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void Complete(Exception error)
		{
			if (this.Current != null)
			{
				TransactedBatchContext transactedBatchContext = this.rpc.TransactedBatchContext;
				if (transactedBatchContext != null)
				{
					if (error == null)
					{
						transactedBatchContext.Complete();
					}
					else
					{
						transactedBatchContext.ForceRollback();
					}
					transactedBatchContext.InDispatch = false;
				}
				else if (this.transactionSetComplete)
				{
					this.rpc.InstanceContext.Transaction.CompletePendingTransaction(this.Current, null);
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917511, SR.GetString("TraceCodeTxCompletionStatusCompletedForSetComplete", new object[]
						{
							this.Current.TransactionInformation.LocalIdentifier,
							this.rpc.Operation.Name
						}));
					}
				}
				else if (this.IsCompleted || error != null)
				{
					this.rpc.InstanceContext.Transaction.CompletePendingTransaction(this.Current, error);
				}
				if (this.rpc.Operation.IsInsideTransactedReceiveScope)
				{
					this.CompleteDependentClone();
				}
				this.Current = null;
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000DDA08 File Offset: 0x000DBC08
		internal void SetIncomplete()
		{
			this.IsCompleted = false;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000DDA14 File Offset: 0x000DBC14
		internal void Completed()
		{
			if (this.scope == null)
			{
				return;
			}
			if (this.rpc.Operation.TransactionAutoComplete)
			{
				try
				{
					this.Current.Rollback();
				}
				catch (ObjectDisposedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxTransactionInvalidSetTransactionComplete", new object[]
				{
					this.rpc.Operation.Name,
					this.rpc.Host.Description.Name
				})));
			}
			if (this.transactionSetComplete)
			{
				try
				{
					this.Current.Rollback();
				}
				catch (ObjectDisposedException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMultiSetTransactionComplete", new object[]
				{
					this.rpc.Operation.Name,
					this.rpc.Host.Description.Name
				})));
			}
			this.transactionSetComplete = true;
			this.IsCompleted = true;
			this.scope.Complete();
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000DDB40 File Offset: 0x000DBD40
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void ThreadEnter(ref Exception error)
		{
			Transaction clone = this.Clone;
			if (clone != null && error == null)
			{
				if (TD.TransactionScopeCreateIsEnabled() && clone != null && clone.TransactionInformation != null)
				{
					TD.TransactionScopeCreate(this.rpc.EventTraceActivity, clone.TransactionInformation.LocalIdentifier, clone.TransactionInformation.DistributedIdentifier);
				}
				this.scope = this.rpc.InstanceContext.Transaction.CreateTransactionScope(clone);
				this.transactionSetComplete = false;
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000DDBC4 File Offset: 0x000DBDC4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void ThreadLeave()
		{
			if (this.scope != null)
			{
				if (!this.transactionSetComplete)
				{
					this.scope.Complete();
				}
				try
				{
					this.scope.Dispose();
					this.scope = null;
				}
				catch (TransactionException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
				}
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000DDC3C File Offset: 0x000DBE3C
		internal void CreateDependentClone()
		{
			if (this.dependentClone == null && this.Clone != null)
			{
				this.dependentClone = this.Clone.DependentClone(DependentCloneOption.BlockCommitUntilComplete);
			}
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000DDC6C File Offset: 0x000DBE6C
		internal void CompleteDependentClone()
		{
			if (this.dependentClone != null)
			{
				this.dependentClone.Complete();
			}
		}

		// Token: 0x040029CA RID: 10698
		internal Transaction Current;

		// Token: 0x040029CB RID: 10699
		internal Transaction Clone;

		// Token: 0x040029CC RID: 10700
		internal DependentTransaction dependentClone;

		// Token: 0x040029CD RID: 10701
		internal bool IsCompleted = true;

		// Token: 0x040029CE RID: 10702
		internal MessageRpc rpc;

		// Token: 0x040029CF RID: 10703
		private TransactionScope scope;

		// Token: 0x040029D0 RID: 10704
		private bool transactionSetComplete;
	}
}
