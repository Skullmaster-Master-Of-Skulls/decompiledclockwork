using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B7 RID: 1463
	internal sealed class TransactionInstanceContextFacet
	{
		// Token: 0x06003931 RID: 14641 RVA: 0x000DDC87 File Offset: 0x000DBE87
		internal TransactionInstanceContextFacet(InstanceContext instanceContext)
		{
			this.instanceContext = instanceContext;
			this.mutex = instanceContext.ThisLock;
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x000DDCA2 File Offset: 0x000DBEA2
		// (set) Token: 0x06003933 RID: 14643 RVA: 0x000DDCAA File Offset: 0x000DBEAA
		internal bool ShouldReleaseInstance
		{
			get
			{
				return this.shouldReleaseInstance;
			}
			set
			{
				this.shouldReleaseInstance = value;
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000DDCB4 File Offset: 0x000DBEB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void CheckIfTxCompletedAndUpdateAttached(ref MessageRpc rpc, bool isConcurrent)
		{
			if (rpc.Transaction.Current == null)
			{
				return;
			}
			object obj = this.mutex;
			lock (obj)
			{
				if (!isConcurrent)
				{
					if (this.shouldReleaseInstance)
					{
						this.shouldReleaseInstance = false;
						if (rpc.Error == null)
						{
							rpc.Error = TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true);
							DiagnosticUtility.TraceHandledException(rpc.Error, TraceEventType.Error);
							if (DiagnosticUtility.ShouldTraceInformation)
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 917513, SR.GetString("TraceCodeTxCompletionStatusCompletedForAsyncAbort", new object[]
								{
									rpc.Transaction.Current.TransactionInformation.LocalIdentifier,
									rpc.Operation.Name
								}));
							}
						}
					}
					if (rpc.Transaction.IsCompleted || rpc.Error != null)
					{
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							if (rpc.Error != null)
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 917510, SR.GetString("TraceCodeTxCompletionStatusCompletedForError", new object[]
								{
									rpc.Transaction.Current.TransactionInformation.LocalIdentifier,
									rpc.Operation.Name
								}));
							}
							else
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 917509, SR.GetString("TraceCodeTxCompletionStatusCompletedForAutocomplete", new object[]
								{
									rpc.Transaction.Current.TransactionInformation.LocalIdentifier,
									rpc.Operation.Name
								}));
							}
						}
						this.Attached = null;
						if (!(this.waiting == null))
						{
							DiagnosticUtility.FailFast("waiting should be null when resetting current");
						}
						this.current = null;
					}
					else
					{
						this.Attached = rpc.Transaction.Current;
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 917514, SR.GetString("TraceCodeTxCompletionStatusRemainsAttached", new object[]
							{
								rpc.Transaction.Current.TransactionInformation.LocalIdentifier,
								rpc.Operation.Name
							}));
						}
					}
				}
				else if (!this.pending.ContainsKey(rpc.Transaction.Current) && rpc.Error == null)
				{
					rpc.Error = TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true);
					DiagnosticUtility.TraceHandledException(rpc.Error, TraceEventType.Error);
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917513, SR.GetString("TraceCodeTxCompletionStatusCompletedForAsyncAbort", new object[]
						{
							rpc.Transaction.Current.TransactionInformation.LocalIdentifier,
							rpc.Operation.Name
						}));
					}
				}
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000DDF64 File Offset: 0x000DC164
		internal void CompletePendingTransaction(Transaction transaction, Exception error)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (this.pending.ContainsKey(transaction))
				{
					TransactionInstanceContextFacet.Complete(transaction, error);
				}
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000DDFB4 File Offset: 0x000DC1B4
		internal static void Complete(Transaction transaction, Exception error)
		{
			try
			{
				if (error == null)
				{
					CommittableTransaction committableTransaction = transaction as CommittableTransaction;
					if (committableTransaction != null)
					{
						committableTransaction.Commit();
					}
					else
					{
						DependentTransaction dependentTransaction = transaction as DependentTransaction;
						if (dependentTransaction != null)
						{
							dependentTransaction.Complete();
						}
					}
				}
				else
				{
					transaction.Rollback();
				}
			}
			catch (TransactionException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000DE034 File Offset: 0x000DC234
		internal TransactionScope CreateTransactionScope(Transaction transaction)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (this.pending.ContainsKey(transaction))
				{
					try
					{
						return new TransactionScope(transaction);
					}
					catch (TransactionException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000DE0BC File Offset: 0x000DC2BC
		internal void SetCurrent(ref MessageRpc rpc)
		{
			Transaction transaction = rpc.Transaction.Current;
			if (!(transaction != null))
			{
				DiagnosticUtility.FailFast("we should never get here with a requestTransaction null");
			}
			object obj = this.mutex;
			lock (obj)
			{
				if (this.current == null)
				{
					this.current = transaction;
				}
				else if (this.current != transaction)
				{
					this.waiting = transaction;
					this.paused = rpc.Pause();
				}
				else
				{
					rpc.Transaction.Current = this.current;
				}
			}
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000DE164 File Offset: 0x000DC364
		internal void AddReference(ref MessageRpc rpc, Transaction tx, bool updateCallCount)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (this.pending == null)
				{
					this.pending = new Dictionary<Transaction, TransactionInstanceContextFacet.RemoveReferenceRM>();
				}
				if (tx != null)
				{
					if (this.pending == null)
					{
						this.pending = new Dictionary<Transaction, TransactionInstanceContextFacet.RemoveReferenceRM>();
					}
					TransactionInstanceContextFacet.RemoveReferenceRM removeReferenceRM;
					if (!this.pending.TryGetValue(tx, out removeReferenceRM))
					{
						TransactionInstanceContextFacet.RemoveReferenceRM removeReferenceRM2 = new TransactionInstanceContextFacet.RemoveReferenceRM(this.instanceContext, tx, rpc.Operation.Name);
						removeReferenceRM2.CallCount = 1L;
						this.pending.Add(tx, removeReferenceRM2);
					}
					else if (updateCallCount)
					{
						removeReferenceRM.CallCount += 1L;
					}
				}
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000DE220 File Offset: 0x000DC420
		internal void RemoveReference(Transaction tx)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (tx.Equals(this.current))
				{
					if (this.waiting != null)
					{
						this.current = this.waiting;
						this.waiting = null;
						if (this.instanceContext.Behavior.ReleaseServiceInstanceOnTransactionComplete)
						{
							this.instanceContext.ReleaseServiceInstance();
							if (DiagnosticUtility.ShouldTraceInformation)
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 917516, SR.GetString("TraceCodeTxReleaseServiceInstanceOnCompletion", new object[]
								{
									tx.TransactionInformation.LocalIdentifier
								}));
							}
						}
						bool flag2;
						this.paused.Resume(out flag2);
						if (flag2)
						{
						}
					}
					else
					{
						this.shouldReleaseInstance = true;
						this.current = null;
					}
				}
				if (this.pending != null && this.pending.ContainsKey(tx))
				{
					this.pending.Remove(tx);
				}
			}
		}

		// Token: 0x040029D1 RID: 10705
		internal Transaction waiting;

		// Token: 0x040029D2 RID: 10706
		internal Transaction Attached;

		// Token: 0x040029D3 RID: 10707
		private IResumeMessageRpc paused;

		// Token: 0x040029D4 RID: 10708
		private object mutex;

		// Token: 0x040029D5 RID: 10709
		private Transaction current;

		// Token: 0x040029D6 RID: 10710
		private InstanceContext instanceContext;

		// Token: 0x040029D7 RID: 10711
		private Dictionary<Transaction, TransactionInstanceContextFacet.RemoveReferenceRM> pending;

		// Token: 0x040029D8 RID: 10712
		private bool shouldReleaseInstance;

		// Token: 0x02000CB4 RID: 3252
		private abstract class VolatileBase : ISinglePhaseNotification, IEnlistmentNotification
		{
			// Token: 0x0600796D RID: 31085 RVA: 0x001C5585 File Offset: 0x001C3785
			protected VolatileBase(InstanceContext instanceContext, Transaction transaction)
			{
				this.InstanceContext = instanceContext;
				this.Transaction = transaction;
				this.Transaction.EnlistVolatile(this, EnlistmentOptions.None);
			}

			// Token: 0x0600796E RID: 31086
			protected abstract void Completed();

			// Token: 0x0600796F RID: 31087 RVA: 0x001C55A9 File Offset: 0x001C37A9
			public virtual void Commit(Enlistment enlistment)
			{
				this.Completed();
			}

			// Token: 0x06007970 RID: 31088 RVA: 0x001C55B1 File Offset: 0x001C37B1
			public virtual void InDoubt(Enlistment enlistment)
			{
				this.Completed();
			}

			// Token: 0x06007971 RID: 31089 RVA: 0x001C55B9 File Offset: 0x001C37B9
			public virtual void Rollback(Enlistment enlistment)
			{
				this.Completed();
			}

			// Token: 0x06007972 RID: 31090 RVA: 0x001C55C1 File Offset: 0x001C37C1
			public virtual void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
			{
				enlistment.Committed();
				this.Completed();
			}

			// Token: 0x06007973 RID: 31091 RVA: 0x001C55CF File Offset: 0x001C37CF
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				preparingEnlistment.Prepared();
			}

			// Token: 0x0400453A RID: 17722
			protected InstanceContext InstanceContext;

			// Token: 0x0400453B RID: 17723
			protected Transaction Transaction;
		}

		// Token: 0x02000CB5 RID: 3253
		private sealed class RemoveReferenceRM : TransactionInstanceContextFacet.VolatileBase
		{
			// Token: 0x06007974 RID: 31092 RVA: 0x001C55D8 File Offset: 0x001C37D8
			internal RemoveReferenceRM(InstanceContext instanceContext, Transaction tx, string operation) : base(instanceContext, tx)
			{
				this.operation = operation;
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					this.endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
				}
				AspNetEnvironment.Current.IncrementBusyCount();
				if (AspNetEnvironment.Current.TraceIncrementBusyCountIsEnabled())
				{
					AspNetEnvironment.Current.TraceIncrementBusyCount(base.GetType().FullName);
				}
			}

			// Token: 0x17001B95 RID: 7061
			// (get) Token: 0x06007975 RID: 31093 RVA: 0x001C5631 File Offset: 0x001C3831
			// (set) Token: 0x06007976 RID: 31094 RVA: 0x001C5639 File Offset: 0x001C3839
			internal long CallCount
			{
				get
				{
					return this.callCount;
				}
				set
				{
					this.callCount = value;
				}
			}

			// Token: 0x06007977 RID: 31095 RVA: 0x001C5644 File Offset: 0x001C3844
			protected override void Completed()
			{
				this.InstanceContext.Transaction.RemoveReference(this.Transaction);
				AspNetEnvironment.Current.DecrementBusyCount();
				if (AspNetEnvironment.Current.TraceDecrementBusyCountIsEnabled())
				{
					AspNetEnvironment.Current.TraceDecrementBusyCount(base.GetType().FullName);
				}
			}

			// Token: 0x06007978 RID: 31096 RVA: 0x001C5692 File Offset: 0x001C3892
			public override void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
			{
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.TxCommitted(this.endpointDispatcher, this.CallCount);
				}
				base.SinglePhaseCommit(enlistment);
			}

			// Token: 0x06007979 RID: 31097 RVA: 0x001C56B3 File Offset: 0x001C38B3
			public override void Commit(Enlistment enlistment)
			{
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.TxCommitted(this.endpointDispatcher, this.CallCount);
				}
				base.Commit(enlistment);
			}

			// Token: 0x0600797A RID: 31098 RVA: 0x001C56D4 File Offset: 0x001C38D4
			public override void Rollback(Enlistment enlistment)
			{
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.TxAborted(this.endpointDispatcher, this.CallCount);
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 917517, SR.GetString("TraceCodeTxAsyncAbort", new object[]
					{
						this.Transaction.TransactionInformation.LocalIdentifier
					}));
				}
				base.Rollback(enlistment);
			}

			// Token: 0x0600797B RID: 31099 RVA: 0x001C5735 File Offset: 0x001C3935
			public override void InDoubt(Enlistment enlistment)
			{
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.TxInDoubt(this.endpointDispatcher, this.CallCount);
				}
				base.InDoubt(enlistment);
			}

			// Token: 0x0400453C RID: 17724
			private string operation;

			// Token: 0x0400453D RID: 17725
			private long callCount;

			// Token: 0x0400453E RID: 17726
			private EndpointDispatcher endpointDispatcher;
		}
	}
}
