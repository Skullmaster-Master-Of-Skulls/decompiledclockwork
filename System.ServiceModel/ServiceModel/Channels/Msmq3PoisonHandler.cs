using System;
using System.Collections.Generic;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D5 RID: 2261
	internal sealed class Msmq3PoisonHandler : IPoisonHandlingStrategy, IDisposable
	{
		// Token: 0x06005612 RID: 22034 RVA: 0x0013AEA6 File Offset: 0x001390A6
		internal Msmq3PoisonHandler(MsmqReceiveHelper receiver)
		{
			this.receiver = receiver;
			this.trackedMessages = new SortedList<long, int>(256);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0013AED0 File Offset: 0x001390D0
		public bool CheckAndHandlePoisonMessage(MsmqMessageProperty messageProperty)
		{
			long lookupId = messageProperty.LookupId;
			object obj = this.thisLock;
			int num;
			lock (obj)
			{
				num = this.UpdateSeenCount(lookupId);
				if (num > this.receiver.MsmqReceiveParameters.ReceiveRetryCount + 1 && this.receiver.MsmqReceiveParameters.ReceiveRetryCount != 2147483647)
				{
					if (TD.ReceiveRetryCountReachedIsEnabled())
					{
						TD.ReceiveRetryCountReached(messageProperty.MessageId);
					}
					this.FinalDisposition(messageProperty);
					this.trackedMessages.Remove(lookupId);
					return true;
				}
			}
			messageProperty.AbortCount = num - 1;
			return false;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x0013AF80 File Offset: 0x00139180
		public void FinalDisposition(MsmqMessageProperty messageProperty)
		{
			ReceiveErrorHandling receiveErrorHandling = this.receiver.MsmqReceiveParameters.ReceiveErrorHandling;
			if (receiveErrorHandling != ReceiveErrorHandling.Fault)
			{
				if (receiveErrorHandling == ReceiveErrorHandling.Drop)
				{
					MsmqDefaultLockingQueue msmqDefaultLockingQueue = this.receiver.Queue as MsmqDefaultLockingQueue;
					if (msmqDefaultLockingQueue != null && this.receiver.Transactional)
					{
						msmqDefaultLockingQueue.UnlockMessage(messageProperty.LookupId, TimeSpan.Zero);
					}
					this.receiver.DropOrRejectReceivedMessage(messageProperty, false);
					return;
				}
			}
			else
			{
				MsmqReceiveHelper.TryAbortTransactionCurrent();
				if (this.receiver.ChannelListener != null)
				{
					this.receiver.ChannelListener.FaultListener();
				}
				if (this.receiver.Channel != null)
				{
					this.receiver.Channel.FaultChannel();
				}
			}
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x0013B024 File Offset: 0x00139224
		private int UpdateSeenCount(long lookupId)
		{
			int num;
			if (this.trackedMessages.TryGetValue(lookupId, out num))
			{
				num++;
				this.trackedMessages[lookupId] = num;
				return num;
			}
			if (256 == this.trackedMessages.Count)
			{
				this.trackedMessages.RemoveAt(0);
			}
			this.trackedMessages.Add(lookupId, 1);
			return 1;
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x0013B080 File Offset: 0x00139280
		public void Open()
		{
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0013B082 File Offset: 0x00139282
		public void Dispose()
		{
		}

		// Token: 0x04003537 RID: 13623
		private const int maxTrackedMessages = 256;

		// Token: 0x04003538 RID: 13624
		private MsmqReceiveHelper receiver;

		// Token: 0x04003539 RID: 13625
		private SortedList<long, int> trackedMessages;

		// Token: 0x0400353A RID: 13626
		private object thisLock = new object();
	}
}
