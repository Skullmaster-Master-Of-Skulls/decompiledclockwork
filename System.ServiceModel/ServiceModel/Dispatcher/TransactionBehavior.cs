using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B5 RID: 1461
	internal class TransactionBehavior
	{
		// Token: 0x0600391A RID: 14618 RVA: 0x000DD14F File Offset: 0x000DB34F
		internal TransactionBehavior()
		{
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000DD170 File Offset: 0x000DB370
		internal TransactionBehavior(DispatchRuntime dispatch)
		{
			this.isConcurrent = (dispatch.ConcurrencyMode == ConcurrencyMode.Multiple || dispatch.ConcurrencyMode == ConcurrencyMode.Reentrant);
			this.dispatch = dispatch;
			this.isTransactedReceiveChannelDispatcher = dispatch.ChannelDispatcher.IsTransactedReceive;
			if (dispatch.ChannelDispatcher.TransactionIsolationLevelSet)
			{
				this.InitializeIsolationLevel(dispatch);
			}
			this.timeout = TransactionBehavior.NormalizeTimeout(dispatch.ChannelDispatcher.TransactionTimeout);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000DD1F8 File Offset: 0x000DB3F8
		internal static Exception CreateFault(string reasonText, string codeString, bool isNetDispatcherFault)
		{
			string ns;
			string action;
			if (isNetDispatcherFault)
			{
				ns = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher";
				action = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault";
			}
			else
			{
				ns = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions";
				action = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault";
			}
			FaultReason reason = new FaultReason(reasonText, CultureInfo.CurrentCulture);
			FaultCode code = FaultCode.CreateSenderFaultCode(codeString, ns);
			return new FaultException(reason, code, action);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000DD23E File Offset: 0x000DB43E
		internal static TransactionBehavior CreateIfNeeded(DispatchRuntime dispatch)
		{
			if (TransactionBehavior.NeedsTransactionBehavior(dispatch))
			{
				return new TransactionBehavior(dispatch);
			}
			return null;
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000DD250 File Offset: 0x000DB450
		internal static TimeSpan NormalizeTimeout(TimeSpan timeout)
		{
			if (TimeSpan.Zero == timeout)
			{
				timeout = TransactionManager.DefaultTimeout;
			}
			else if (TimeSpan.Zero != TransactionManager.MaximumTimeout && timeout > TransactionManager.MaximumTimeout)
			{
				timeout = TransactionManager.MaximumTimeout;
			}
			return timeout;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000DD290 File Offset: 0x000DB490
		internal static CommittableTransaction CreateTransaction(IsolationLevel isolation, TimeSpan timeout)
		{
			return new CommittableTransaction(new TransactionOptions
			{
				IsolationLevel = isolation,
				Timeout = timeout
			});
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000DD2BB File Offset: 0x000DB4BB
		internal void SetCurrent(ref MessageRpc rpc)
		{
			if (!this.isConcurrent)
			{
				rpc.InstanceContext.Transaction.SetCurrent(ref rpc);
			}
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000DD2D8 File Offset: 0x000DB4D8
		internal void ResolveOutcome(ref MessageRpc rpc)
		{
			if (rpc.InstanceContext != null && rpc.transaction != null)
			{
				TransactionInstanceContextFacet transaction = rpc.InstanceContext.Transaction;
				if (transaction != null)
				{
					transaction.CheckIfTxCompletedAndUpdateAttached(ref rpc, this.isConcurrent);
				}
				rpc.Transaction.Complete(rpc.Error);
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000DD322 File Offset: 0x000DB522
		private Transaction GetInstanceContextTransaction(ref MessageRpc rpc)
		{
			return rpc.InstanceContext.Transaction.Attached;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000DD334 File Offset: 0x000DB534
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeIsolationLevel(DispatchRuntime dispatch)
		{
			this.isolation = dispatch.ChannelDispatcher.TransactionIsolationLevel;
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000DD348 File Offset: 0x000DB548
		private static bool NeedsTransactionBehavior(DispatchRuntime dispatch)
		{
			DispatchOperation unhandledDispatchOperation = dispatch.UnhandledDispatchOperation;
			if (unhandledDispatchOperation != null && unhandledDispatchOperation.TransactionRequired)
			{
				return true;
			}
			if (dispatch.ChannelDispatcher.IsTransactedReceive)
			{
				return true;
			}
			for (int i = 0; i < dispatch.Operations.Count; i++)
			{
				DispatchOperation dispatchOperation = dispatch.Operations[i];
				if (dispatchOperation.TransactionRequired)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000DD3A8 File Offset: 0x000DB5A8
		internal void ResolveTransaction(ref MessageRpc rpc)
		{
			if (rpc.Operation.HasDefaultUnhandledActionInvoker)
			{
				return;
			}
			Transaction transaction = null;
			if (rpc.Operation.IsInsideTransactedReceiveScope)
			{
				IInstanceTransaction instanceTransaction = rpc.Operation.Invoker as IInstanceTransaction;
				if (instanceTransaction != null)
				{
					transaction = instanceTransaction.GetTransactionForInstance(rpc.OperationContext);
				}
				if (transaction != null && DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 917519, SR.GetString("TraceCodeTxSourceTxScopeRequiredUsingExistingTransaction", new object[]
					{
						transaction.TransactionInformation.LocalIdentifier,
						rpc.Operation.Name
					}));
				}
			}
			else
			{
				transaction = this.GetInstanceContextTransaction(ref rpc);
			}
			Transaction transaction2 = null;
			try
			{
				transaction2 = TransactionMessageProperty.TryGetTransaction(rpc.Request);
			}
			catch (TransactionException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionUnmarshalFailed", new object[]
				{
					ex.Message
				}), "TransactionUnmarshalingFailed", false));
			}
			if (rpc.Operation.TransactionRequired)
			{
				if (!(transaction2 != null))
				{
					goto IL_238;
				}
				if (this.isTransactedReceiveChannelDispatcher)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917505, SR.GetString("TraceCodeTxSourceTxScopeRequiredIsTransactedTransport", new object[]
						{
							transaction2.TransactionInformation.LocalIdentifier,
							rpc.Operation.Name
						}));
						goto IL_238;
					}
					goto IL_238;
				}
				else
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917506, SR.GetString("TraceCodeTxSourceTxScopeRequiredIsTransactionFlow", new object[]
						{
							transaction2.TransactionInformation.LocalIdentifier,
							rpc.Operation.Name
						}));
					}
					if (PerformanceCounters.PerformanceCountersEnabled)
					{
						PerformanceCounters.TxFlowed(PerformanceCounters.GetEndpointDispatcher(), rpc.Operation.Name);
					}
					bool flag;
					if (rpc.Operation.IsInsideTransactedReceiveScope)
					{
						flag = transaction2.Equals(transaction);
					}
					else
					{
						flag = (transaction2 == transaction);
					}
					if (flag)
					{
						goto IL_238;
					}
					try
					{
						transaction2 = transaction2.DependentClone(DependentCloneOption.RollbackIfNotComplete);
						goto IL_238;
					}
					catch (TransactionException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
					}
				}
			}
			if (transaction2 != null && this.isTransactedReceiveChannelDispatcher)
			{
				try
				{
					if (rpc.TransactedBatchContext != null)
					{
						rpc.TransactedBatchContext.ForceCommit();
						rpc.TransactedBatchContext = null;
					}
					else
					{
						TransactionInstanceContextFacet.Complete(transaction2, null);
					}
				}
				finally
				{
					transaction2.Dispose();
					transaction2 = null;
				}
			}
			IL_238:
			InstanceContext instanceContext = rpc.InstanceContext;
			if (instanceContext.Transaction.ShouldReleaseInstance && !this.isConcurrent)
			{
				if (instanceContext.Behavior.ReleaseServiceInstanceOnTransactionComplete)
				{
					instanceContext.ReleaseServiceInstance();
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917516, SR.GetString("TraceCodeTxReleaseServiceInstanceOnCompletion", new object[]
						{
							transaction.TransactionInformation.LocalIdentifier
						}));
					}
				}
				instanceContext.Transaction.ShouldReleaseInstance = false;
				if (transaction2 == null || transaction2 == transaction)
				{
					rpc.Transaction.Current = transaction;
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
				}
				transaction = null;
			}
			if (rpc.Operation.TransactionRequired)
			{
				if (transaction2 == null)
				{
					if (transaction != null)
					{
						transaction2 = transaction;
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 917507, SR.GetString("TraceCodeTxSourceTxScopeRequiredIsAttachedTransaction", new object[]
							{
								transaction2.TransactionInformation.LocalIdentifier,
								rpc.Operation.Name
							}));
						}
					}
					else
					{
						transaction2 = TransactionBehavior.CreateTransaction(this.isolation, this.timeout);
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 917508, SR.GetString("TraceCodeTxSourceTxScopeRequiredIsCreateNewTransaction", new object[]
							{
								transaction2.TransactionInformation.LocalIdentifier,
								rpc.Operation.Name
							}));
						}
					}
				}
				if (this.isolation != IsolationLevel.Unspecified && transaction2.IsolationLevel != this.isolation)
				{
					throw TraceUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("IsolationLevelMismatch2", new object[]
					{
						transaction2.IsolationLevel,
						this.isolation
					}), "TransactionIsolationLevelMismatch", false), rpc.Request);
				}
				rpc.Transaction.Current = transaction2;
				rpc.InstanceContext.Transaction.AddReference(ref rpc, rpc.Transaction.Current, true);
				try
				{
					rpc.Transaction.Clone = transaction2.Clone();
					if (rpc.Operation.IsInsideTransactedReceiveScope)
					{
						rpc.Transaction.CreateDependentClone();
					}
				}
				catch (ObjectDisposedException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(TransactionBehavior.CreateFault(SR.GetString("SFxTransactionAsyncAborted"), "TransactionAborted", true));
				}
				rpc.InstanceContext.Transaction.AddReference(ref rpc, rpc.Transaction.Clone, false);
				rpc.OperationContext.TransactionFacet = rpc.Transaction;
				if (!rpc.Operation.TransactionAutoComplete)
				{
					rpc.Transaction.SetIncomplete();
				}
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000DD8AC File Offset: 0x000DBAAC
		internal void InitializeCallContext(ref MessageRpc rpc)
		{
			if (rpc.Operation.TransactionRequired)
			{
				rpc.Transaction.ThreadEnter(ref rpc.Error);
			}
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000DD8CC File Offset: 0x000DBACC
		internal void ClearCallContext(ref MessageRpc rpc)
		{
			if (rpc.Operation.TransactionRequired)
			{
				rpc.Transaction.ThreadLeave();
			}
		}

		// Token: 0x040029C5 RID: 10693
		private bool isConcurrent;

		// Token: 0x040029C6 RID: 10694
		private IsolationLevel isolation = ServiceBehaviorAttribute.DefaultIsolationLevel;

		// Token: 0x040029C7 RID: 10695
		private DispatchRuntime dispatch;

		// Token: 0x040029C8 RID: 10696
		private TimeSpan timeout = TimeSpan.Zero;

		// Token: 0x040029C9 RID: 10697
		private bool isTransactedReceiveChannelDispatcher;
	}
}
