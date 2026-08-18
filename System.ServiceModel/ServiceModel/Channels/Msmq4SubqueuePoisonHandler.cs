using System;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D7 RID: 2263
	internal sealed class Msmq4SubqueuePoisonHandler : IPoisonHandlingStrategy, IDisposable
	{
		// Token: 0x06005626 RID: 22054 RVA: 0x0013B7A9 File Offset: 0x001399A9
		public Msmq4SubqueuePoisonHandler(MsmqReceiveHelper receiver)
		{
			this.receiver = receiver;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0013B7B8 File Offset: 0x001399B8
		public void Open()
		{
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0013B7BA File Offset: 0x001399BA
		public bool CheckAndHandlePoisonMessage(MsmqMessageProperty messageProperty)
		{
			if (messageProperty.AbortCount > this.receiver.MsmqReceiveParameters.ReceiveRetryCount)
			{
				if (TD.ReceiveRetryCountReachedIsEnabled())
				{
					TD.ReceiveRetryCountReached(messageProperty.MessageId);
				}
				this.FinalDisposition(messageProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x0013B7F0 File Offset: 0x001399F0
		public void FinalDisposition(MsmqMessageProperty messageProperty)
		{
			switch (this.receiver.MsmqReceiveParameters.ReceiveErrorHandling)
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
				this.receiver.DropOrRejectReceivedMessage(messageProperty, false);
				return;
			case ReceiveErrorHandling.Reject:
				this.receiver.DropOrRejectReceivedMessage(messageProperty, true);
				MsmqDiagnostics.PoisonMessageRejected(messageProperty.MessageId, this.receiver.InstanceId);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x0013B892 File Offset: 0x00139A92
		public void Dispose()
		{
		}

		// Token: 0x0400354A RID: 13642
		private MsmqReceiveHelper receiver;
	}
}
