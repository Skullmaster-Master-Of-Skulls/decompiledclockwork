using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F6 RID: 2294
	internal sealed class MsmqReceiveHelper
	{
		// Token: 0x06005770 RID: 22384 RVA: 0x00140F84 File Offset: 0x0013F184
		internal MsmqReceiveHelper(MsmqReceiveParameters receiveParameters, Uri uri, IMsmqMessagePool messagePool, MsmqInputChannelBase channel, MsmqChannelListenerBase listener)
		{
			this.queueName = receiveParameters.AddressTranslator.UriToFormatName(uri);
			this.receiveParameters = receiveParameters;
			this.uri = uri;
			this.instanceId = uri.ToString().ToUpperInvariant();
			this.pool = messagePool;
			this.poisonHandler = Msmq.CreatePoisonHandler(this);
			this.channel = channel;
			this.listener = listener;
			this.queue = Msmq.CreateMsmqQueue(this);
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x00140FF7 File Offset: 0x0013F1F7
		internal ServiceModelActivity Activity
		{
			get
			{
				return this.activity;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06005772 RID: 22386 RVA: 0x00140FFF File Offset: 0x0013F1FF
		private IPoisonHandlingStrategy PoisonHandler
		{
			get
			{
				return this.poisonHandler;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x00141007 File Offset: 0x0013F207
		internal MsmqReceiveParameters MsmqReceiveParameters
		{
			get
			{
				return this.receiveParameters;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06005774 RID: 22388 RVA: 0x0014100F File Offset: 0x0013F20F
		internal MsmqInputChannelBase Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06005775 RID: 22389 RVA: 0x00141017 File Offset: 0x0013F217
		internal MsmqChannelListenerBase ChannelListener
		{
			get
			{
				return this.listener;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06005776 RID: 22390 RVA: 0x0014101F File Offset: 0x0013F21F
		internal Uri ListenUri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x00141027 File Offset: 0x0013F227
		internal string InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06005778 RID: 22392 RVA: 0x0014102F File Offset: 0x0013F22F
		internal MsmqQueue Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x00141037 File Offset: 0x0013F237
		internal bool Transactional
		{
			get
			{
				return this.receiveParameters.ExactlyOnce;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x0600577A RID: 22394 RVA: 0x00141044 File Offset: 0x0013F244
		internal string MsmqRuntimeNativeLibrary
		{
			get
			{
				if (this.msmqRuntimeNativeLibrary == null)
				{
					this.msmqRuntimeNativeLibrary = Environment.SystemDirectory + "\\mqrt.dll";
				}
				return this.msmqRuntimeNativeLibrary;
			}
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x0014106C File Offset: 0x0013F26C
		internal void Open()
		{
			this.activity = MsmqDiagnostics.StartListenAtActivity(this);
			using (MsmqDiagnostics.BoundOpenOperation(this))
			{
				this.queue.EnsureOpen();
				this.poisonHandler.Open();
			}
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001410C0 File Offset: 0x0013F2C0
		internal void Close()
		{
			using (ServiceModelActivity.BoundOperation(this.Activity))
			{
				this.poisonHandler.Dispose();
				this.queue.Dispose();
			}
			ServiceModelActivity.Stop(this.activity);
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00141118 File Offset: 0x0013F318
		internal MsmqInputMessage TakeMessage()
		{
			return this.pool.TakeMessage();
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00141125 File Offset: 0x0013F325
		internal void ReturnMessage(MsmqInputMessage message)
		{
			this.pool.ReturnMessage(message);
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x00141134 File Offset: 0x0013F334
		internal static void TryAbortTransactionCurrent()
		{
			if (null != Transaction.Current)
			{
				try
				{
					Transaction.Current.Rollback();
				}
				catch (TransactionAbortedException ex)
				{
					MsmqDiagnostics.ExpectedException(ex);
				}
				catch (ObjectDisposedException ex2)
				{
					MsmqDiagnostics.ExpectedException(ex2);
				}
			}
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00141188 File Offset: 0x0013F388
		internal void DropOrRejectReceivedMessage(MsmqMessageProperty messageProperty, bool reject)
		{
			this.DropOrRejectReceivedMessage(this.Queue, messageProperty, reject);
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00141198 File Offset: 0x0013F398
		internal void DropOrRejectReceivedMessage(MsmqQueue queue, MsmqMessageProperty messageProperty, bool reject)
		{
			if (this.Transactional)
			{
				MsmqReceiveHelper.TryAbortTransactionCurrent();
				IPostRollbackErrorStrategy postRollbackErrorStrategy = new SimplePostRollbackErrorStrategy(messageProperty.LookupId);
				MsmqQueue.MoveReceiveResult moveReceiveResult = MsmqQueue.MoveReceiveResult.Unknown;
				do
				{
					using (MsmqEmptyMessage msmqEmptyMessage = new MsmqEmptyMessage())
					{
						using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
						{
							moveReceiveResult = queue.TryReceiveByLookupId(messageProperty.LookupId, msmqEmptyMessage, MsmqTransactionMode.CurrentOrThrow);
							if (MsmqQueue.MoveReceiveResult.Succeeded == moveReceiveResult && reject)
							{
								queue.MarkMessageRejected(messageProperty.LookupId);
							}
							transactionScope.Complete();
						}
					}
					if (moveReceiveResult == MsmqQueue.MoveReceiveResult.Succeeded)
					{
						MsmqDiagnostics.MessageConsumed(this.instanceId, messageProperty.MessageId, Msmq.IsRejectMessageSupported && reject);
					}
					if (moveReceiveResult != MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction)
					{
						return;
					}
				}
				while (postRollbackErrorStrategy.AnotherTryNeeded());
				return;
			}
			MsmqDiagnostics.MessageConsumed(this.instanceId, messageProperty.MessageId, false);
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00141268 File Offset: 0x0013F468
		internal static void MoveReceivedMessage(MsmqQueue queueFrom, MsmqQueue queueTo, long lookupId)
		{
			MsmqReceiveHelper.TryAbortTransactionCurrent();
			IPostRollbackErrorStrategy postRollbackErrorStrategy = new SimplePostRollbackErrorStrategy(lookupId);
			MsmqQueue.MoveReceiveResult moveReceiveResult;
			do
			{
				moveReceiveResult = queueFrom.TryMoveMessage(lookupId, queueTo, MsmqTransactionMode.Single);
			}
			while (moveReceiveResult == MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction && postRollbackErrorStrategy.AnotherTryNeeded());
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x00141299 File Offset: 0x0013F499
		internal void FinalDisposition(MsmqMessageProperty messageProperty)
		{
			this.poisonHandler.FinalDisposition(messageProperty);
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001412A8 File Offset: 0x0013F4A8
		internal bool WaitForMessage(TimeSpan timeout)
		{
			bool result;
			using (MsmqEmptyMessage msmqEmptyMessage = new MsmqEmptyMessage())
			{
				result = (MsmqQueue.ReceiveResult.Timeout != this.queue.TryPeek(msmqEmptyMessage, timeout));
			}
			return result;
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x001412EC File Offset: 0x0013F4EC
		internal IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new MsmqReceiveHelper.WaitForMessageAsyncResult(this.queue, timeout, callback, state);
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x001412FC File Offset: 0x0013F4FC
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return MsmqReceiveHelper.WaitForMessageAsyncResult.End(result);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x00141304 File Offset: 0x0013F504
		internal bool TryReceive(MsmqInputMessage msmqMessage, TimeSpan timeout, MsmqTransactionMode transactionMode, out MsmqMessageProperty property)
		{
			property = null;
			MsmqQueue.ReceiveResult receiveResult = this.Queue.TryReceive(msmqMessage, timeout, transactionMode);
			if (MsmqQueue.ReceiveResult.OperationCancelled == receiveResult)
			{
				return true;
			}
			if (MsmqQueue.ReceiveResult.Timeout == receiveResult)
			{
				return false;
			}
			property = new MsmqMessageProperty(msmqMessage);
			if (this.Transactional && this.PoisonHandler.CheckAndHandlePoisonMessage(property))
			{
				long lookupId = property.LookupId;
				property = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new MsmqPoisonMessageException(lookupId));
			}
			return true;
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00141370 File Offset: 0x0013F570
		internal IAsyncResult BeginTryReceive(MsmqInputMessage msmqMessage, TimeSpan timeout, MsmqTransactionMode transactionMode, AsyncCallback callback, object state)
		{
			if (this.receiveParameters.ExactlyOnce || this.queue is ILockingQueue)
			{
				return new MsmqReceiveHelper.TryTransactedReceiveAsyncResult(this, msmqMessage, timeout, transactionMode, callback, state);
			}
			return new MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult(this, msmqMessage, timeout, callback, state);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x001413A8 File Offset: 0x0013F5A8
		internal bool EndTryReceive(IAsyncResult result, out MsmqInputMessage msmqMessage, out MsmqMessageProperty msmqProperty)
		{
			msmqMessage = null;
			msmqProperty = null;
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			if (this.receiveParameters.ExactlyOnce)
			{
				MsmqReceiveHelper.TryTransactedReceiveAsyncResult tryTransactedReceiveAsyncResult = result as MsmqReceiveHelper.TryTransactedReceiveAsyncResult;
				if (tryTransactedReceiveAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("InvalidAsyncResult"));
				}
				return MsmqReceiveHelper.TryTransactedReceiveAsyncResult.End(tryTransactedReceiveAsyncResult, out msmqMessage, out msmqProperty);
			}
			else
			{
				MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult tryNonTransactedReceiveAsyncResult = result as MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult;
				if (tryNonTransactedReceiveAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("InvalidAsyncResult"));
				}
				return MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult.End(tryNonTransactedReceiveAsyncResult, out msmqMessage, out msmqProperty);
			}
		}

		// Token: 0x040035C9 RID: 13769
		private IPoisonHandlingStrategy poisonHandler;

		// Token: 0x040035CA RID: 13770
		private string queueName;

		// Token: 0x040035CB RID: 13771
		private MsmqQueue queue;

		// Token: 0x040035CC RID: 13772
		private MsmqReceiveParameters receiveParameters;

		// Token: 0x040035CD RID: 13773
		private Uri uri;

		// Token: 0x040035CE RID: 13774
		private string instanceId;

		// Token: 0x040035CF RID: 13775
		private IMsmqMessagePool pool;

		// Token: 0x040035D0 RID: 13776
		private MsmqInputChannelBase channel;

		// Token: 0x040035D1 RID: 13777
		private MsmqChannelListenerBase listener;

		// Token: 0x040035D2 RID: 13778
		private ServiceModelActivity activity;

		// Token: 0x040035D3 RID: 13779
		private string msmqRuntimeNativeLibrary;

		// Token: 0x02000D95 RID: 3477
		private class TryTransactedReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007EB9 RID: 32441 RVA: 0x001D80F4 File Offset: 0x001D62F4
			internal TryTransactedReceiveAsyncResult(MsmqReceiveHelper receiver, MsmqInputMessage msmqMessage, TimeSpan timeout, MsmqTransactionMode transactionMode, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.txCurrent = Transaction.Current;
				this.receiver = receiver;
				this.msmqMessage = msmqMessage;
				this.transactionMode = transactionMode;
				ActionItem.Schedule(MsmqReceiveHelper.TryTransactedReceiveAsyncResult.onComplete, this);
			}

			// Token: 0x06007EBA RID: 32442 RVA: 0x001D8144 File Offset: 0x001D6344
			private static void OnComplete(object parameter)
			{
				MsmqReceiveHelper.TryTransactedReceiveAsyncResult tryTransactedReceiveAsyncResult = parameter as MsmqReceiveHelper.TryTransactedReceiveAsyncResult;
				Transaction value = Transaction.Current;
				Transaction.Current = tryTransactedReceiveAsyncResult.txCurrent;
				try
				{
					Exception exception = null;
					try
					{
						tryTransactedReceiveAsyncResult.expired = !tryTransactedReceiveAsyncResult.receiver.TryReceive(tryTransactedReceiveAsyncResult.msmqMessage, tryTransactedReceiveAsyncResult.timeoutHelper.RemainingTime(), tryTransactedReceiveAsyncResult.transactionMode, out tryTransactedReceiveAsyncResult.messageProperty);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					tryTransactedReceiveAsyncResult.Complete(false, exception);
				}
				finally
				{
					Transaction.Current = value;
				}
			}

			// Token: 0x06007EBB RID: 32443 RVA: 0x001D81DC File Offset: 0x001D63DC
			internal static bool End(IAsyncResult result, out MsmqInputMessage msmqMessage, out MsmqMessageProperty property)
			{
				MsmqReceiveHelper.TryTransactedReceiveAsyncResult tryTransactedReceiveAsyncResult = AsyncResult.End<MsmqReceiveHelper.TryTransactedReceiveAsyncResult>(result);
				msmqMessage = tryTransactedReceiveAsyncResult.msmqMessage;
				property = tryTransactedReceiveAsyncResult.messageProperty;
				return !tryTransactedReceiveAsyncResult.expired;
			}

			// Token: 0x040048B4 RID: 18612
			private bool expired;

			// Token: 0x040048B5 RID: 18613
			private MsmqReceiveHelper receiver;

			// Token: 0x040048B6 RID: 18614
			private TimeoutHelper timeoutHelper;

			// Token: 0x040048B7 RID: 18615
			private Transaction txCurrent;

			// Token: 0x040048B8 RID: 18616
			private MsmqInputMessage msmqMessage;

			// Token: 0x040048B9 RID: 18617
			private MsmqMessageProperty messageProperty;

			// Token: 0x040048BA RID: 18618
			private MsmqTransactionMode transactionMode;

			// Token: 0x040048BB RID: 18619
			private static Action<object> onComplete = new Action<object>(MsmqReceiveHelper.TryTransactedReceiveAsyncResult.OnComplete);
		}

		// Token: 0x02000D96 RID: 3478
		private class TryNonTransactedReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007EBD RID: 32445 RVA: 0x001D821C File Offset: 0x001D641C
			internal TryNonTransactedReceiveAsyncResult(MsmqReceiveHelper receiver, MsmqInputMessage msmqMessage, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.receiver = receiver;
				this.msmqMessage = msmqMessage;
				receiver.Queue.BeginTryReceive(msmqMessage, timeout, MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult.onCompleteStatic, this);
			}

			// Token: 0x06007EBE RID: 32446 RVA: 0x001D824A File Offset: 0x001D644A
			private static void OnCompleteStatic(IAsyncResult result)
			{
				(result.AsyncState as MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult).OnComplete(result);
			}

			// Token: 0x06007EBF RID: 32447 RVA: 0x001D8260 File Offset: 0x001D6460
			private void OnComplete(IAsyncResult result)
			{
				Exception exception = null;
				try
				{
					this.receiveResult = this.receiver.Queue.EndTryReceive(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(result.CompletedSynchronously, exception);
			}

			// Token: 0x06007EC0 RID: 32448 RVA: 0x001D82B4 File Offset: 0x001D64B4
			internal static bool End(IAsyncResult result, out MsmqInputMessage msmqMessage, out MsmqMessageProperty property)
			{
				MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult tryNonTransactedReceiveAsyncResult = AsyncResult.End<MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult>(result);
				msmqMessage = tryNonTransactedReceiveAsyncResult.msmqMessage;
				property = null;
				if (MsmqQueue.ReceiveResult.Timeout == tryNonTransactedReceiveAsyncResult.receiveResult)
				{
					return false;
				}
				if (MsmqQueue.ReceiveResult.OperationCancelled == tryNonTransactedReceiveAsyncResult.receiveResult)
				{
					return true;
				}
				property = new MsmqMessageProperty(msmqMessage);
				return true;
			}

			// Token: 0x040048BC RID: 18620
			private MsmqQueue.ReceiveResult receiveResult;

			// Token: 0x040048BD RID: 18621
			private MsmqReceiveHelper receiver;

			// Token: 0x040048BE RID: 18622
			private MsmqInputMessage msmqMessage;

			// Token: 0x040048BF RID: 18623
			private static AsyncCallback onCompleteStatic = Fx.ThunkCallback(new AsyncCallback(MsmqReceiveHelper.TryNonTransactedReceiveAsyncResult.OnCompleteStatic));
		}

		// Token: 0x02000D97 RID: 3479
		private class WaitForMessageAsyncResult : AsyncResult
		{
			// Token: 0x06007EC2 RID: 32450 RVA: 0x001D830B File Offset: 0x001D650B
			public WaitForMessageAsyncResult(MsmqQueue msmqQueue, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.msmqMessage = new MsmqEmptyMessage();
				this.msmqQueue = msmqQueue;
				this.msmqQueue.BeginPeek(this.msmqMessage, timeout, MsmqReceiveHelper.WaitForMessageAsyncResult.onCompleteStatic, this);
			}

			// Token: 0x06007EC3 RID: 32451 RVA: 0x001D8341 File Offset: 0x001D6541
			private static void OnCompleteStatic(IAsyncResult result)
			{
				((MsmqReceiveHelper.WaitForMessageAsyncResult)result.AsyncState).OnComplete(result);
			}

			// Token: 0x06007EC4 RID: 32452 RVA: 0x001D8354 File Offset: 0x001D6554
			private void OnComplete(IAsyncResult result)
			{
				this.msmqMessage.Dispose();
				MsmqQueue.ReceiveResult receiveResult = MsmqQueue.ReceiveResult.Unknown;
				Exception exception = null;
				try
				{
					receiveResult = this.msmqQueue.EndPeek(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				this.successResult = (receiveResult != MsmqQueue.ReceiveResult.Timeout);
				base.Complete(result.CompletedSynchronously, exception);
			}

			// Token: 0x06007EC5 RID: 32453 RVA: 0x001D83B8 File Offset: 0x001D65B8
			public static bool End(IAsyncResult result)
			{
				MsmqReceiveHelper.WaitForMessageAsyncResult waitForMessageAsyncResult = AsyncResult.End<MsmqReceiveHelper.WaitForMessageAsyncResult>(result);
				return waitForMessageAsyncResult.successResult;
			}

			// Token: 0x040048C0 RID: 18624
			private MsmqQueue msmqQueue;

			// Token: 0x040048C1 RID: 18625
			private MsmqEmptyMessage msmqMessage;

			// Token: 0x040048C2 RID: 18626
			private bool successResult;

			// Token: 0x040048C3 RID: 18627
			private static AsyncCallback onCompleteStatic = Fx.ThunkCallback(new AsyncCallback(MsmqReceiveHelper.WaitForMessageAsyncResult.OnCompleteStatic));
		}
	}
}
