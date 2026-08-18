using System;
using System.EnterpriseServices;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000073 RID: 115
	public sealed class TransactionScope : IDisposable
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00035C14 File Offset: 0x00035014
		public TransactionScope() : this(TransactionScopeOption.Required)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00035C34 File Offset: 0x00035034
		public TransactionScope(TransactionScopeOption scopeOption)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption )");
			}
			if (this.NeedToCreateTransaction(scopeOption))
			{
				this.committableTransaction = new CommittableTransaction();
				this.expectedCurrent = this.committableTransaction.Clone();
			}
			if (DiagnosticTrace.Information)
			{
				if (null == this.expectedCurrent)
				{
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty, TransactionScopeResult.NoTransaction);
				}
				else
				{
					TransactionScopeResult txScopeResult;
					if (null == this.committableTransaction)
					{
						txScopeResult = TransactionScopeResult.UsingExistingCurrent;
					}
					else
					{
						txScopeResult = TransactionScopeResult.CreatedTransaction;
					}
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId, txScopeResult);
				}
			}
			this.PushScope();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption )");
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00035D14 File Offset: 0x00035114
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TimeSpan )");
			}
			this.ValidateScopeTimeout("scopeTimeout", scopeTimeout);
			TimeSpan timeout = TransactionManager.ValidateTimeout(scopeTimeout);
			if (this.NeedToCreateTransaction(scopeOption))
			{
				this.committableTransaction = new CommittableTransaction(timeout);
				this.expectedCurrent = this.committableTransaction.Clone();
			}
			if (null != this.expectedCurrent && null == this.committableTransaction && TimeSpan.Zero != scopeTimeout)
			{
				this.scopeTimer = new Timer(new TimerCallback(TransactionScope.TimerCallback), this, scopeTimeout, TimeSpan.Zero);
			}
			if (DiagnosticTrace.Information)
			{
				if (null == this.expectedCurrent)
				{
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty, TransactionScopeResult.NoTransaction);
				}
				else
				{
					TransactionScopeResult txScopeResult;
					if (null == this.committableTransaction)
					{
						txScopeResult = TransactionScopeResult.UsingExistingCurrent;
					}
					else
					{
						txScopeResult = TransactionScopeResult.CreatedTransaction;
					}
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId, txScopeResult);
				}
			}
			this.PushScope();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TimeSpan )");
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00035E54 File Offset: 0x00035254
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TransactionOptions )");
			}
			this.ValidateScopeTimeout("transactionOptions.Timeout", transactionOptions.Timeout);
			TimeSpan timeout = transactionOptions.Timeout;
			transactionOptions.Timeout = TransactionManager.ValidateTimeout(transactionOptions.Timeout);
			TransactionManager.ValidateIsolationLevel(transactionOptions.IsolationLevel);
			if (this.NeedToCreateTransaction(scopeOption))
			{
				this.committableTransaction = new CommittableTransaction(transactionOptions);
				this.expectedCurrent = this.committableTransaction.Clone();
			}
			else if (null != this.expectedCurrent && IsolationLevel.Unspecified != transactionOptions.IsolationLevel && this.expectedCurrent.IsolationLevel != transactionOptions.IsolationLevel)
			{
				throw new ArgumentException(SR.GetString("TransactionScopeIsolationLevelDifferentFromTransaction"), "transactionOptions.IsolationLevel");
			}
			if (null != this.expectedCurrent && null == this.committableTransaction && TimeSpan.Zero != timeout)
			{
				this.scopeTimer = new Timer(new TimerCallback(TransactionScope.TimerCallback), this, timeout, TimeSpan.Zero);
			}
			if (DiagnosticTrace.Information)
			{
				if (null == this.expectedCurrent)
				{
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty, TransactionScopeResult.NoTransaction);
				}
				else
				{
					TransactionScopeResult txScopeResult;
					if (null == this.committableTransaction)
					{
						txScopeResult = TransactionScopeResult.UsingExistingCurrent;
					}
					else
					{
						txScopeResult = TransactionScopeResult.CreatedTransaction;
					}
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId, txScopeResult);
				}
			}
			this.PushScope();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TransactionOptions )");
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00035FF4 File Offset: 0x000353F4
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TransactionOptions, EnterpriseServicesInteropOption )");
			}
			this.ValidateScopeTimeout("transactionOptions.Timeout", transactionOptions.Timeout);
			TimeSpan timeout = transactionOptions.Timeout;
			transactionOptions.Timeout = TransactionManager.ValidateTimeout(transactionOptions.Timeout);
			TransactionManager.ValidateIsolationLevel(transactionOptions.IsolationLevel);
			this.ValidateInteropOption(interopOption);
			this.interopModeSpecified = true;
			this.interopOption = interopOption;
			if (this.NeedToCreateTransaction(scopeOption))
			{
				this.committableTransaction = new CommittableTransaction(transactionOptions);
				this.expectedCurrent = this.committableTransaction.Clone();
			}
			else if (null != this.expectedCurrent && IsolationLevel.Unspecified != transactionOptions.IsolationLevel && this.expectedCurrent.IsolationLevel != transactionOptions.IsolationLevel)
			{
				throw new ArgumentException(SR.GetString("TransactionScopeIsolationLevelDifferentFromTransaction"), "transactionOptions.IsolationLevel");
			}
			if (null != this.expectedCurrent && null == this.committableTransaction && TimeSpan.Zero != timeout)
			{
				this.scopeTimer = new Timer(new TimerCallback(TransactionScope.TimerCallback), this, timeout, TimeSpan.Zero);
			}
			if (DiagnosticTrace.Information)
			{
				if (null == this.expectedCurrent)
				{
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty, TransactionScopeResult.NoTransaction);
				}
				else
				{
					TransactionScopeResult txScopeResult;
					if (null == this.committableTransaction)
					{
						txScopeResult = TransactionScopeResult.UsingExistingCurrent;
					}
					else
					{
						txScopeResult = TransactionScopeResult.CreatedTransaction;
					}
					TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId, txScopeResult);
				}
			}
			this.PushScope();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( TransactionScopeOption, TransactionOptions, EnterpriseServicesInteropOption )");
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000361B4 File Offset: 0x000355B4
		public TransactionScope(Transaction transactionToUse)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction )");
			}
			this.Initialize(transactionToUse, TimeSpan.Zero, false);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction )");
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00036224 File Offset: 0x00035624
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction, TimeSpan )");
			}
			this.Initialize(transactionToUse, scopeTimeout, false);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction, TimeSpan )");
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00036284 File Offset: 0x00035684
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction, TimeSpan, EnterpriseServicesInteropOption )");
			}
			this.ValidateInteropOption(interopOption);
			this.interopOption = interopOption;
			this.Initialize(transactionToUse, scopeTimeout, true);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.ctor( Transaction, TimeSpan, EnterpriseServicesInteropOption )");
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000362F4 File Offset: 0x000356F4
		private bool NeedToCreateTransaction(TransactionScopeOption scopeOption)
		{
			bool result = false;
			this.CommonInitialize();
			switch (scopeOption)
			{
			case TransactionScopeOption.Required:
				this.expectedCurrent = this.savedCurrent;
				if (null == this.expectedCurrent)
				{
					result = true;
				}
				break;
			case TransactionScopeOption.RequiresNew:
				result = true;
				break;
			case TransactionScopeOption.Suppress:
				this.expectedCurrent = null;
				result = false;
				break;
			default:
				throw new ArgumentOutOfRangeException("scopeOption");
			}
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00036364 File Offset: 0x00035764
		private void Initialize(Transaction transactionToUse, TimeSpan scopeTimeout, bool interopModeSpecified)
		{
			if (null == transactionToUse)
			{
				throw new ArgumentNullException("transactionToUse");
			}
			this.ValidateScopeTimeout("scopeTimeout", scopeTimeout);
			this.CommonInitialize();
			if (TimeSpan.Zero != scopeTimeout)
			{
				this.scopeTimer = new Timer(new TimerCallback(TransactionScope.TimerCallback), this, scopeTimeout, TimeSpan.Zero);
			}
			this.expectedCurrent = transactionToUse;
			this.interopModeSpecified = interopModeSpecified;
			if (DiagnosticTrace.Information)
			{
				TransactionScopeCreatedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId, TransactionScopeResult.TransactionPassed);
			}
			this.PushScope();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00036404 File Offset: 0x00035804
		public void Dispose()
		{
			bool flag = false;
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.Dispose");
			}
			if (this.disposed)
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.Dispose");
				}
				return;
			}
			if (this.scopeThread != Thread.CurrentThread)
			{
				if (DiagnosticTrace.Error)
				{
					InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceBase"), SR.GetString("InvalidScopeThread"));
				}
				throw new InvalidOperationException(SR.GetString("InvalidScopeThread"));
			}
			Exception ex = null;
			try
			{
				this.disposed = true;
				TransactionScope currentScope = this.threadContextData.CurrentScope;
				Transaction transaction = null;
				Transaction transaction2 = Transaction.FastGetTransaction(currentScope, this.threadContextData, out transaction);
				if (!this.Equals(currentScope))
				{
					if (currentScope == null)
					{
						Transaction transaction3 = this.committableTransaction;
						if (transaction3 == null)
						{
							transaction3 = this.dependentTransaction;
						}
						transaction3.Rollback();
						flag = true;
						throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceBase"), SR.GetString("TransactionScopeInvalidNesting"), null);
					}
					if (currentScope.interopOption != EnterpriseServicesInteropOption.None || ((!(null != currentScope.expectedCurrent) || currentScope.expectedCurrent.Equals(transaction2)) && (!(null != transaction2) || !(null == currentScope.expectedCurrent))))
					{
						goto IL_252;
					}
					if (DiagnosticTrace.Warning)
					{
						TransactionTraceIdentifier currentTxTraceId;
						if (null == transaction2)
						{
							currentTxTraceId = TransactionTraceIdentifier.Empty;
						}
						else
						{
							currentTxTraceId = transaction2.TransactionTraceId;
						}
						TransactionTraceIdentifier scopeTxTraceId;
						if (null == this.expectedCurrent)
						{
							scopeTxTraceId = TransactionTraceIdentifier.Empty;
						}
						else
						{
							scopeTxTraceId = this.expectedCurrent.TransactionTraceId;
						}
						TransactionScopeCurrentChangedTraceRecord.Trace(SR.GetString("TraceSourceBase"), scopeTxTraceId, currentTxTraceId);
					}
					ex = TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceBase"), SR.GetString("TransactionScopeIncorrectCurrent"), null);
					if (!(null != transaction2))
					{
						goto IL_252;
					}
					try
					{
						transaction2.Rollback();
						goto IL_252;
					}
					catch (TransactionException)
					{
						goto IL_252;
					}
					catch (ObjectDisposedException)
					{
						goto IL_252;
					}
					IL_1CA:
					if (ex == null)
					{
						ex = TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceBase"), SR.GetString("TransactionScopeInvalidNesting"), null);
					}
					if (DiagnosticTrace.Warning)
					{
						if (null == currentScope.expectedCurrent)
						{
							TransactionScopeNestedIncorrectlyTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty);
						}
						else
						{
							TransactionScopeNestedIncorrectlyTraceRecord.Trace(SR.GetString("TraceSourceBase"), currentScope.expectedCurrent.TransactionTraceId);
						}
					}
					currentScope.complete = false;
					try
					{
						currentScope.InternalDispose();
					}
					catch (TransactionException)
					{
					}
					currentScope = this.threadContextData.CurrentScope;
					this.complete = false;
					IL_252:
					if (!this.Equals(currentScope))
					{
						goto IL_1CA;
					}
				}
				else if (this.interopOption == EnterpriseServicesInteropOption.None && ((null != this.expectedCurrent && !this.expectedCurrent.Equals(transaction2)) || (null != transaction2 && null == this.expectedCurrent)))
				{
					if (DiagnosticTrace.Warning)
					{
						TransactionTraceIdentifier currentTxTraceId2;
						if (null == transaction2)
						{
							currentTxTraceId2 = TransactionTraceIdentifier.Empty;
						}
						else
						{
							currentTxTraceId2 = transaction2.TransactionTraceId;
						}
						TransactionTraceIdentifier scopeTxTraceId2;
						if (null == this.expectedCurrent)
						{
							scopeTxTraceId2 = TransactionTraceIdentifier.Empty;
						}
						else
						{
							scopeTxTraceId2 = this.expectedCurrent.TransactionTraceId;
						}
						TransactionScopeCurrentChangedTraceRecord.Trace(SR.GetString("TraceSourceBase"), scopeTxTraceId2, currentTxTraceId2);
					}
					if (ex == null)
					{
						ex = TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceBase"), SR.GetString("TransactionScopeIncorrectCurrent"), null);
					}
					if (null != transaction2)
					{
						try
						{
							transaction2.Rollback();
						}
						catch (TransactionException)
						{
						}
						catch (ObjectDisposedException)
						{
						}
					}
					this.complete = false;
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.PopScope();
				}
			}
			this.InternalDispose();
			if (ex != null)
			{
				throw ex;
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.Dispose");
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00036824 File Offset: 0x00035C24
		private void InternalDispose()
		{
			this.disposed = true;
			try
			{
				this.PopScope();
				if (DiagnosticTrace.Information)
				{
					if (null == this.expectedCurrent)
					{
						TransactionScopeDisposedTraceRecord.Trace(SR.GetString("TraceSourceBase"), TransactionTraceIdentifier.Empty);
					}
					else
					{
						TransactionScopeDisposedTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId);
					}
				}
				if (null != this.expectedCurrent)
				{
					if (!this.complete)
					{
						if (DiagnosticTrace.Warning)
						{
							TransactionScopeIncompleteTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId);
						}
						Transaction transaction = this.committableTransaction;
						if (transaction == null)
						{
							transaction = this.dependentTransaction;
						}
						transaction.Rollback();
					}
					else if (null != this.committableTransaction)
					{
						this.committableTransaction.Commit();
					}
					else
					{
						this.dependentTransaction.Complete();
					}
				}
			}
			finally
			{
				if (this.scopeTimer != null)
				{
					this.scopeTimer.Dispose();
				}
				if (null != this.committableTransaction)
				{
					this.committableTransaction.Dispose();
					this.expectedCurrent.Dispose();
				}
				if (null != this.dependentTransaction)
				{
					this.dependentTransaction.Dispose();
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00036974 File Offset: 0x00035D74
		public void Complete()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.Complete");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("TransactionScope");
			}
			if (this.complete)
			{
				throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceBase"), SR.GetString("DisposeScope"), null);
			}
			this.complete = true;
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceBase"), "TransactionScope.Complete");
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000369F4 File Offset: 0x00035DF4
		private static void TimerCallback(object state)
		{
			TransactionScope transactionScope = state as TransactionScope;
			if (transactionScope == null)
			{
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceBase"), SR.GetString("TransactionScopeTimerObjectInvalid"));
				}
				throw TransactionException.Create(SR.GetString("TraceSourceBase"), SR.GetString("InternalError") + SR.GetString("TransactionScopeTimerObjectInvalid"), null);
			}
			transactionScope.Timeout();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00036A64 File Offset: 0x00035E64
		private void Timeout()
		{
			if (!this.complete && null != this.expectedCurrent)
			{
				if (DiagnosticTrace.Warning)
				{
					TransactionScopeTimeoutTraceRecord.Trace(SR.GetString("TraceSourceBase"), this.expectedCurrent.TransactionTraceId);
				}
				try
				{
					this.expectedCurrent.Rollback();
				}
				catch (ObjectDisposedException exception)
				{
					if (DiagnosticTrace.Verbose)
					{
						ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceBase"), exception);
					}
				}
				catch (TransactionException exception2)
				{
					if (DiagnosticTrace.Verbose)
					{
						ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceBase"), exception2);
					}
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00036B24 File Offset: 0x00035F24
		private void CommonInitialize()
		{
			this.complete = false;
			this.dependentTransaction = null;
			this.disposed = false;
			this.committableTransaction = null;
			this.expectedCurrent = null;
			this.scopeTimer = null;
			this.scopeThread = Thread.CurrentThread;
			Transaction.GetCurrentTransactionAndScope(out this.savedCurrent, out this.savedCurrentScope, out this.threadContextData, out this.contextTransaction);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00036B84 File Offset: 0x00035F84
		private void PushScope()
		{
			if (!this.interopModeSpecified)
			{
				this.interopOption = Transaction.InteropMode(this.savedCurrentScope);
			}
			this.SetCurrent(this.expectedCurrent);
			this.threadContextData.CurrentScope = this;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00036BC4 File Offset: 0x00035FC4
		private void PopScope()
		{
			this.threadContextData.CurrentScope = this.savedCurrentScope;
			this.RestoreCurrent();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00036BF4 File Offset: 0x00035FF4
		private void SetCurrent(Transaction newCurrent)
		{
			if (this.dependentTransaction == null && this.committableTransaction == null && newCurrent != null)
			{
				this.dependentTransaction = newCurrent.DependentClone(DependentCloneOption.RollbackIfNotComplete);
			}
			switch (this.interopOption)
			{
			case EnterpriseServicesInteropOption.None:
				this.threadContextData.CurrentTransaction = newCurrent;
				return;
			case EnterpriseServicesInteropOption.Automatic:
				Transaction.VerifyEnterpriseServicesOk();
				if (Transaction.UseServiceDomainForCurrent())
				{
					this.PushServiceDomain(newCurrent);
					return;
				}
				this.threadContextData.CurrentTransaction = newCurrent;
				return;
			case EnterpriseServicesInteropOption.Full:
				Transaction.VerifyEnterpriseServicesOk();
				this.PushServiceDomain(newCurrent);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00036C94 File Offset: 0x00036094
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PushServiceDomain(Transaction newCurrent)
		{
			if ((newCurrent != null && newCurrent.Equals(ContextUtil.SystemTransaction)) || (newCurrent == null && ContextUtil.SystemTransaction == null))
			{
				return;
			}
			ServiceConfig serviceConfig = new ServiceConfig();
			try
			{
				if (newCurrent != null)
				{
					serviceConfig.Synchronization = SynchronizationOption.RequiresNew;
					ServiceDomain.Enter(serviceConfig);
					this.createdDoubleServiceDomain = true;
					serviceConfig.Synchronization = SynchronizationOption.Required;
					serviceConfig.BringYourOwnSystemTransaction = newCurrent;
				}
				ServiceDomain.Enter(serviceConfig);
				this.createdServiceDomain = true;
			}
			catch (COMException ex)
			{
				if (NativeMethods.XACT_E_NOTRANSACTION == ex.ErrorCode)
				{
					throw TransactionException.Create(SR.GetString("TraceSourceBase"), SR.GetString("TransactionAlreadyOver"), ex);
				}
				throw TransactionException.Create(SR.GetString("TraceSourceBase"), ex.Message, ex);
			}
			finally
			{
				if (!this.createdServiceDomain && this.createdDoubleServiceDomain)
				{
					ServiceDomain.Leave();
				}
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00036DA4 File Offset: 0x000361A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void JitSafeLeaveServiceDomain()
		{
			if (this.createdDoubleServiceDomain)
			{
				ServiceDomain.Leave();
			}
			ServiceDomain.Leave();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00036DD4 File Offset: 0x000361D4
		private void RestoreCurrent()
		{
			if (this.createdServiceDomain)
			{
				this.JitSafeLeaveServiceDomain();
			}
			this.threadContextData.CurrentTransaction = this.contextTransaction;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00036E04 File Offset: 0x00036204
		private void ValidateInteropOption(EnterpriseServicesInteropOption interopOption)
		{
			if (interopOption < EnterpriseServicesInteropOption.None || interopOption > EnterpriseServicesInteropOption.Full)
			{
				throw new ArgumentOutOfRangeException("interopOption");
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00036E24 File Offset: 0x00036224
		private void ValidateScopeTimeout(string paramName, TimeSpan scopeTimeout)
		{
			if (scopeTimeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException(paramName);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00036E54 File Offset: 0x00036254
		internal bool ScopeComplete
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00036E74 File Offset: 0x00036274
		internal EnterpriseServicesInteropOption InteropMode
		{
			get
			{
				return this.interopOption;
			}
		}

		// Token: 0x0400014F RID: 335
		private bool complete;

		// Token: 0x04000150 RID: 336
		private Transaction savedCurrent;

		// Token: 0x04000151 RID: 337
		private Transaction contextTransaction;

		// Token: 0x04000152 RID: 338
		private TransactionScope savedCurrentScope;

		// Token: 0x04000153 RID: 339
		private ContextData threadContextData;

		// Token: 0x04000154 RID: 340
		private Transaction expectedCurrent;

		// Token: 0x04000155 RID: 341
		private CommittableTransaction committableTransaction;

		// Token: 0x04000156 RID: 342
		private DependentTransaction dependentTransaction;

		// Token: 0x04000157 RID: 343
		private bool disposed;

		// Token: 0x04000158 RID: 344
		private Timer scopeTimer;

		// Token: 0x04000159 RID: 345
		private Thread scopeThread;

		// Token: 0x0400015A RID: 346
		private bool createdServiceDomain;

		// Token: 0x0400015B RID: 347
		private bool createdDoubleServiceDomain;

		// Token: 0x0400015C RID: 348
		private bool interopModeSpecified;

		// Token: 0x0400015D RID: 349
		private EnterpriseServicesInteropOption interopOption;
	}
}
