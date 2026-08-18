using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	internal class OletxTransaction : ISerializable, IObjectReference
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000376E4 File Offset: 0x00036AE4
		internal RealOletxTransaction RealTransaction
		{
			get
			{
				return this.realOletxTransaction;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00037704 File Offset: 0x00036B04
		internal Guid Identifier
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.get_Identifier");
				}
				Guid identifier = this.realOletxTransaction.Identifier;
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.get_Identifier");
				}
				return identifier;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00037754 File Offset: 0x00036B54
		internal TransactionStatus Status
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.get_Status");
				}
				TransactionStatus status = this.realOletxTransaction.Status;
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.get_Status");
				}
				return status;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000377A4 File Offset: 0x00036BA4
		internal Exception InnerException
		{
			get
			{
				return this.realOletxTransaction.innerException;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000377C4 File Offset: 0x00036BC4
		internal OletxTransaction(RealOletxTransaction realOletxTransaction)
		{
			this.realOletxTransaction = realOletxTransaction;
			this.realOletxTransaction.OletxTransactionCreated();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000377F4 File Offset: 0x00036BF4
		protected OletxTransaction(SerializationInfo serializationInfo, StreamingContext context)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			this.propagationTokenForDeserialize = (byte[])serializationInfo.GetValue("OletxTransactionPropagationToken", typeof(byte[]));
			if (this.propagationTokenForDeserialize.Length < 24)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "serializationInfo");
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00037864 File Offset: 0x00036C64
		public object GetRealObject(StreamingContext context)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "IObjectReference.GetRealObject");
			}
			if (this.propagationTokenForDeserialize == null)
			{
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), SR.GetString("UnableToDeserializeTransaction"));
				}
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("UnableToDeserializeTransactionInternalError"), null);
			}
			if (null != this.savedLtmPromotedTransaction)
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "IObjectReference.GetRealObject");
				}
				return this.savedLtmPromotedTransaction;
			}
			Transaction transactionFromTransmitterPropagationToken = TransactionInterop.GetTransactionFromTransmitterPropagationToken(this.propagationTokenForDeserialize);
			this.savedLtmPromotedTransaction = transactionFromTransmitterPropagationToken;
			if (DiagnosticTrace.Verbose)
			{
				TransactionDeserializedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), transactionFromTransmitterPropagationToken.internalTransaction.PromotedTransaction.TransactionTraceId);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "IObjectReference.GetRealObject");
			}
			return transactionFromTransmitterPropagationToken;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00037954 File Offset: 0x00036D54
		internal void Dispose()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "IDisposable.Dispose");
			}
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) == 0)
			{
				this.realOletxTransaction.OletxTransactionDisposed();
			}
			GC.SuppressFinalize(this);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "IDisposable.Dispose");
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000379C4 File Offset: 0x00036DC4
		internal void Rollback()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.Rollback");
			}
			if (DiagnosticTrace.Warning)
			{
				TransactionRollbackCalledTraceRecord.Trace(SR.GetString("TraceSourceOletx"), this.TransactionTraceId);
			}
			this.realOletxTransaction.Rollback();
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.Rollback");
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00037A34 File Offset: 0x00036E34
		internal IPromotedEnlistment EnlistVolatile(ISinglePhaseNotificationInternal singlePhaseNotification, EnlistmentOptions enlistmentOptions)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistVolatile( ISinglePhaseNotificationInternal )");
			}
			if (this.realOletxTransaction == null || this.realOletxTransaction.TooLateForEnlistments)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TooLate"), null);
			}
			IPromotedEnlistment result = this.realOletxTransaction.EnlistVolatile(singlePhaseNotification, enlistmentOptions, this);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistVolatile( ISinglePhaseNotificationInternal )");
			}
			return result;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00037AC4 File Offset: 0x00036EC4
		internal IPromotedEnlistment EnlistVolatile(IEnlistmentNotificationInternal enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistVolatile( IEnlistmentNotificationInternal )");
			}
			if (this.realOletxTransaction == null || this.realOletxTransaction.TooLateForEnlistments)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TooLate"), null);
			}
			IPromotedEnlistment result = this.realOletxTransaction.EnlistVolatile(enlistmentNotification, enlistmentOptions, this);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistVolatile( IEnlistmentNotificationInternal )");
			}
			return result;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00037B54 File Offset: 0x00036F54
		internal IPromotedEnlistment EnlistDurable(Guid resourceManagerIdentifier, ISinglePhaseNotificationInternal singlePhaseNotification, bool canDoSinglePhase, EnlistmentOptions enlistmentOptions)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistDurable( ISinglePhaseNotificationInternal )");
			}
			if (this.realOletxTransaction == null || this.realOletxTransaction.TooLateForEnlistments)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TooLate"), null);
			}
			OletxTransactionManager oletxTransactionManagerInstance = this.realOletxTransaction.OletxTransactionManagerInstance;
			OletxResourceManager oletxResourceManager = oletxTransactionManagerInstance.FindOrRegisterResourceManager(resourceManagerIdentifier);
			OletxEnlistment result = oletxResourceManager.EnlistDurable(this, canDoSinglePhase, singlePhaseNotification, enlistmentOptions);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.EnlistDurable( ISinglePhaseNotificationInternal )");
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00037BF4 File Offset: 0x00036FF4
		internal OletxDependentTransaction DependentClone(bool delayCommit)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.DependentClone");
			}
			if (TransactionStatus.Aborted == this.Status)
			{
				throw TransactionAbortedException.Create(SR.GetString("TraceSourceOletx"), this.realOletxTransaction.innerException);
			}
			if (TransactionStatus.InDoubt == this.Status)
			{
				throw TransactionInDoubtException.Create(SR.GetString("TraceSourceOletx"), this.realOletxTransaction.innerException);
			}
			if (this.Status != TransactionStatus.Active)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TransactionAlreadyOver"), null);
			}
			OletxDependentTransaction result = new OletxDependentTransaction(this.realOletxTransaction, delayCommit);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.DependentClone");
			}
			return result;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00037CB4 File Offset: 0x000370B4
		internal TransactionTraceIdentifier TransactionTraceId
		{
			get
			{
				if (TransactionTraceIdentifier.Empty == this.traceIdentifier)
				{
					lock (this.realOletxTransaction)
					{
						if (TransactionTraceIdentifier.Empty == this.traceIdentifier)
						{
							try
							{
								TransactionTraceIdentifier transactionTraceIdentifier = new TransactionTraceIdentifier(this.realOletxTransaction.Identifier.ToString(), 0);
								Thread.MemoryBarrier();
								this.traceIdentifier = transactionTraceIdentifier;
							}
							catch (TransactionException exception)
							{
								if (DiagnosticTrace.Verbose)
								{
									ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), exception);
								}
							}
						}
					}
				}
				return this.traceIdentifier;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00037D84 File Offset: 0x00037184
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.GetObjectData");
			}
			byte[] transmitterPropagationToken = TransactionInterop.GetTransmitterPropagationToken(this);
			serializationInfo.SetType(typeof(OletxTransaction));
			serializationInfo.AddValue("OletxTransactionPropagationToken", transmitterPropagationToken);
			if (DiagnosticTrace.Information)
			{
				TransactionSerializedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), this.TransactionTraceId);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransaction.GetObjectData");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00037E24 File Offset: 0x00037224
		public virtual IsolationLevel IsolationLevel
		{
			get
			{
				return this.realOletxTransaction.TransactionIsolationLevel;
			}
		}

		// Token: 0x040001CF RID: 463
		protected const string propagationTokenString = "OletxTransactionPropagationToken";

		// Token: 0x040001D0 RID: 464
		internal RealOletxTransaction realOletxTransaction;

		// Token: 0x040001D1 RID: 465
		private byte[] propagationTokenForDeserialize;

		// Token: 0x040001D2 RID: 466
		protected int disposed;

		// Token: 0x040001D3 RID: 467
		internal Transaction savedLtmPromotedTransaction;

		// Token: 0x040001D4 RID: 468
		private TransactionTraceIdentifier traceIdentifier = TransactionTraceIdentifier.Empty;
	}
}
