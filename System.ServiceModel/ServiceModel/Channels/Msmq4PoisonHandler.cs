using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D6 RID: 2262
	internal sealed class Msmq4PoisonHandler : IPoisonHandlingStrategy, IDisposable
	{
		// Token: 0x06005618 RID: 22040 RVA: 0x0013B084 File Offset: 0x00139284
		public Msmq4PoisonHandler(MsmqReceiveHelper receiver)
		{
			this.receiver = receiver;
			this.timer = new IOThreadTimer(new Action<object>(this.OnTimer), null, false);
			this.disposed = false;
			this.mainQueueName = this.ReceiveParameters.AddressTranslator.UriToFormatName(this.ListenUri);
			this.poisonQueueName = this.ReceiveParameters.AddressTranslator.UriToFormatName(new Uri(this.ListenUri.AbsoluteUri + ";poison"));
			this.retryQueueName = this.ReceiveParameters.AddressTranslator.UriToFormatName(new Uri(this.ListenUri.AbsoluteUri + ";retry"));
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06005619 RID: 22041 RVA: 0x0013B13A File Offset: 0x0013933A
		private MsmqReceiveParameters ReceiveParameters
		{
			get
			{
				return this.receiver.MsmqReceiveParameters;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x0600561A RID: 22042 RVA: 0x0013B147 File Offset: 0x00139347
		private Uri ListenUri
		{
			get
			{
				return this.receiver.ListenUri;
			}
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x0013B154 File Offset: 0x00139354
		public void Open()
		{
			if (this.ReceiveParameters.ReceiveContextSettings.Enabled)
			{
				this.lockQueueForReceive = ((MsmqSubqueueLockingQueue)this.receiver.Queue).LockQueueForReceive;
			}
			this.mainQueue = this.receiver.Queue;
			this.mainQueueForMove = new MsmqQueue(this.mainQueueName, 4);
			this.poisonQueue = new MsmqQueue(this.poisonQueueName, 4);
			this.retryQueueForMove = new MsmqQueue(this.retryQueueName, 4);
			this.retryQueueForPeek = new MsmqQueue(this.retryQueueName, 1);
			this.retryQueueMessage = new Msmq4PoisonHandler.MsmqRetryQueueMessage();
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				Msmq4PoisonHandler.StartPeek(this);
				return;
			}
			ActionItem.Schedule(Msmq4PoisonHandler.onStartPeek, this);
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x0013B210 File Offset: 0x00139410
		private static void StartPeek(object state)
		{
			Msmq4PoisonHandler msmq4PoisonHandler = state as Msmq4PoisonHandler;
			Msmq4PoisonHandler obj = msmq4PoisonHandler;
			lock (obj)
			{
				if (!msmq4PoisonHandler.disposed)
				{
					msmq4PoisonHandler.retryQueueForPeek.BeginPeek(msmq4PoisonHandler.retryQueueMessage, TimeSpan.MaxValue, Msmq4PoisonHandler.onPeekCompleted, msmq4PoisonHandler);
				}
			}
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0013B274 File Offset: 0x00139474
		public bool CheckAndHandlePoisonMessage(MsmqMessageProperty messageProperty)
		{
			if (this.ReceiveParameters.ReceiveContextSettings.Enabled)
			{
				return this.ReceiveContextPoisonHandling(messageProperty);
			}
			return this.NonReceiveContextPoisonHandling(messageProperty);
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x0013B298 File Offset: 0x00139498
		public bool ReceiveContextPoisonHandling(MsmqMessageProperty messageProperty)
		{
			int num = this.ReceiveParameters.ReceiveRetryCount + 1;
			int maxRetryCycles = this.ReceiveParameters.MaxRetryCycles;
			int num2 = 2 * num + 1;
			int num3 = messageProperty.MoveCount / (num2 + 2);
			int num4 = num3 * (num2 + 2);
			int num5 = messageProperty.MoveCount - num4;
			bool result;
			lock (this)
			{
				if (this.disposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
				}
				if (num3 > maxRetryCycles)
				{
					this.FinalDisposition(messageProperty);
					result = true;
				}
				else if (num5 >= num2)
				{
					if (TD.ReceiveRetryCountReachedIsEnabled())
					{
						TD.ReceiveRetryCountReached(messageProperty.MessageId);
					}
					if (num3 < maxRetryCycles)
					{
						MsmqReceiveHelper.MoveReceivedMessage(this.lockQueueForReceive, this.retryQueueForMove, messageProperty.LookupId);
						MsmqDiagnostics.PoisonMessageMoved(messageProperty.MessageId, false, this.receiver.InstanceId);
					}
					else
					{
						if (TD.MaxRetryCyclesExceededMsmqIsEnabled())
						{
							TD.MaxRetryCyclesExceededMsmq(messageProperty.MessageId);
						}
						this.FinalDisposition(messageProperty);
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x0013B3B0 File Offset: 0x001395B0
		public bool NonReceiveContextPoisonHandling(MsmqMessageProperty messageProperty)
		{
			if (messageProperty.AbortCount <= this.ReceiveParameters.ReceiveRetryCount)
			{
				return false;
			}
			int num = messageProperty.MoveCount / 2;
			lock (this)
			{
				if (this.disposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
				}
				if (num >= this.ReceiveParameters.MaxRetryCycles)
				{
					if (TD.MaxRetryCyclesExceededMsmqIsEnabled())
					{
						TD.MaxRetryCyclesExceededMsmq(messageProperty.MessageId);
					}
					this.FinalDisposition(messageProperty);
				}
				else
				{
					MsmqReceiveHelper.MoveReceivedMessage(this.mainQueue, this.retryQueueForMove, messageProperty.LookupId);
					MsmqDiagnostics.PoisonMessageMoved(messageProperty.MessageId, false, this.receiver.InstanceId);
				}
			}
			return true;
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x0013B480 File Offset: 0x00139680
		public void FinalDisposition(MsmqMessageProperty messageProperty)
		{
			if (this.ReceiveParameters.ReceiveContextSettings.Enabled)
			{
				this.InternalFinalDisposition(this.lockQueueForReceive, messageProperty);
				return;
			}
			this.InternalFinalDisposition(this.mainQueue, messageProperty);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x0013B4B0 File Offset: 0x001396B0
		private void InternalFinalDisposition(MsmqQueue disposeFromQueue, MsmqMessageProperty messageProperty)
		{
			switch (this.ReceiveParameters.ReceiveErrorHandling)
			{
			case ReceiveErrorHandling.Fault:
				MsmqReceiveHelper.TryAbortTransactionCurrent();
				if (this.receiver.ChannelListener != null)
				{
					this.receiver.ChannelListener.FaultListener();
				}
				if (this.receiver.Channel != null)
				{
					this.receiver.Channel.FaultChannel();
					return;
				}
				break;
			case ReceiveErrorHandling.Drop:
				this.receiver.DropOrRejectReceivedMessage(disposeFromQueue, messageProperty, false);
				return;
			case ReceiveErrorHandling.Reject:
				this.receiver.DropOrRejectReceivedMessage(disposeFromQueue, messageProperty, true);
				MsmqDiagnostics.PoisonMessageRejected(messageProperty.MessageId, this.receiver.InstanceId);
				return;
			case ReceiveErrorHandling.Move:
				MsmqReceiveHelper.MoveReceivedMessage(disposeFromQueue, this.poisonQueue, messageProperty.LookupId);
				MsmqDiagnostics.PoisonMessageMoved(messageProperty.MessageId, true, this.receiver.InstanceId);
				break;
			default:
				return;
			}
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0013B580 File Offset: 0x00139780
		public void Dispose()
		{
			lock (this)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.timer.Cancel();
					if (this.retryQueueForPeek != null)
					{
						this.retryQueueForPeek.Dispose();
					}
					if (this.retryQueueForMove != null)
					{
						this.retryQueueForMove.Dispose();
					}
					if (this.poisonQueue != null)
					{
						this.poisonQueue.Dispose();
					}
					if (this.mainQueueForMove != null)
					{
						this.mainQueueForMove.Dispose();
					}
				}
			}
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0013B61C File Offset: 0x0013981C
		private static void OnPeekCompleted(IAsyncResult result)
		{
			Msmq4PoisonHandler msmq4PoisonHandler = result.AsyncState as Msmq4PoisonHandler;
			MsmqQueue.ReceiveResult receiveResult = MsmqQueue.ReceiveResult.Unknown;
			try
			{
				receiveResult = msmq4PoisonHandler.retryQueueForPeek.EndPeek(result);
			}
			catch (MsmqException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			if (MsmqQueue.ReceiveResult.MessageReceived == receiveResult)
			{
				Msmq4PoisonHandler obj = msmq4PoisonHandler;
				lock (obj)
				{
					if (!msmq4PoisonHandler.disposed)
					{
						DateTime d = MsmqDateTime.ToDateTime(msmq4PoisonHandler.retryQueueMessage.LastMoveTime.Value);
						TimeSpan timeSpan = d + msmq4PoisonHandler.ReceiveParameters.RetryCycleDelay - DateTime.UtcNow;
						if (timeSpan < TimeSpan.Zero)
						{
							msmq4PoisonHandler.OnTimer(msmq4PoisonHandler);
						}
						else
						{
							msmq4PoisonHandler.timer.Set(timeSpan);
						}
					}
				}
			}
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x0013B6EC File Offset: 0x001398EC
		private void OnTimer(object state)
		{
			lock (this)
			{
				if (!this.disposed)
				{
					try
					{
						this.retryQueueForPeek.TryMoveMessage(this.retryQueueMessage.LookupId.Value, this.mainQueueForMove, MsmqTransactionMode.Single);
					}
					catch (MsmqException ex)
					{
						MsmqDiagnostics.ExpectedException(ex);
					}
					this.retryQueueForPeek.BeginPeek(this.retryQueueMessage, TimeSpan.MaxValue, Msmq4PoisonHandler.onPeekCompleted, this);
				}
			}
		}

		// Token: 0x0400353B RID: 13627
		private MsmqQueue mainQueue;

		// Token: 0x0400353C RID: 13628
		private MsmqQueue mainQueueForMove;

		// Token: 0x0400353D RID: 13629
		private MsmqQueue retryQueueForPeek;

		// Token: 0x0400353E RID: 13630
		private MsmqQueue retryQueueForMove;

		// Token: 0x0400353F RID: 13631
		private MsmqQueue poisonQueue;

		// Token: 0x04003540 RID: 13632
		private MsmqQueue lockQueueForReceive;

		// Token: 0x04003541 RID: 13633
		private IOThreadTimer timer;

		// Token: 0x04003542 RID: 13634
		private MsmqReceiveHelper receiver;

		// Token: 0x04003543 RID: 13635
		private bool disposed;

		// Token: 0x04003544 RID: 13636
		private string poisonQueueName;

		// Token: 0x04003545 RID: 13637
		private string retryQueueName;

		// Token: 0x04003546 RID: 13638
		private string mainQueueName;

		// Token: 0x04003547 RID: 13639
		private Msmq4PoisonHandler.MsmqRetryQueueMessage retryQueueMessage;

		// Token: 0x04003548 RID: 13640
		private static Action<object> onStartPeek = new Action<object>(Msmq4PoisonHandler.StartPeek);

		// Token: 0x04003549 RID: 13641
		private static AsyncCallback onPeekCompleted = Fx.ThunkCallback(new AsyncCallback(Msmq4PoisonHandler.OnPeekCompleted));

		// Token: 0x02000D89 RID: 3465
		private class MsmqRetryQueueMessage : NativeMsmqMessage
		{
			// Token: 0x06007E85 RID: 32389 RVA: 0x001D7B38 File Offset: 0x001D5D38
			public MsmqRetryQueueMessage() : base(2)
			{
				this.lookupId = new NativeMsmqMessage.LongProperty(this, 60);
				this.lastMoveTime = new NativeMsmqMessage.IntProperty(this, 75);
			}

			// Token: 0x17001C30 RID: 7216
			// (get) Token: 0x06007E86 RID: 32390 RVA: 0x001D7B5D File Offset: 0x001D5D5D
			public NativeMsmqMessage.LongProperty LookupId
			{
				get
				{
					return this.lookupId;
				}
			}

			// Token: 0x17001C31 RID: 7217
			// (get) Token: 0x06007E87 RID: 32391 RVA: 0x001D7B65 File Offset: 0x001D5D65
			public NativeMsmqMessage.IntProperty LastMoveTime
			{
				get
				{
					return this.lastMoveTime;
				}
			}

			// Token: 0x04004899 RID: 18585
			private NativeMsmqMessage.LongProperty lookupId;

			// Token: 0x0400489A RID: 18586
			private NativeMsmqMessage.IntProperty lastMoveTime;
		}
	}
}
