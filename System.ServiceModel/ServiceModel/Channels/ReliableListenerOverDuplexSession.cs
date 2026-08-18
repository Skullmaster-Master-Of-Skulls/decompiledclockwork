using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091F RID: 2335
	internal abstract class ReliableListenerOverDuplexSession<TChannel, TReliableChannel> : ReliableListenerOverSession<TChannel, TReliableChannel, IDuplexSessionChannel, IDuplexSession, Message> where TChannel : class, IChannel where TReliableChannel : class, IChannel
	{
		// Token: 0x06005997 RID: 22935 RVA: 0x001479C0 File Offset: 0x00145BC0
		protected ReliableListenerOverDuplexSession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			base.FaultHelper = new SendFaultHelper(context.Binding.SendTimeout, context.Binding.CloseTimeout);
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x001479EB File Offset: 0x00145BEB
		protected override IAsyncResult BeginTryReceiveItem(IDuplexSessionChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginTryReceive(TimeSpan.MaxValue, callback, channel);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x001479FA File Offset: 0x00145BFA
		protected override void DisposeItem(Message item)
		{
			((IDisposable)item).Dispose();
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x00147A02 File Offset: 0x00145C02
		protected override void EndTryReceiveItem(IDuplexSessionChannel channel, IAsyncResult result, out Message item)
		{
			channel.EndTryReceive(result, out item);
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x00147A0D File Offset: 0x00145C0D
		protected override Message GetMessage(Message item)
		{
			return item;
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x00147A10 File Offset: 0x00145C10
		protected override void SendReply(Message reply, IDuplexSessionChannel channel, Message item)
		{
			if (FaultHelper.AddressReply(item, reply))
			{
				channel.Send(reply);
			}
		}
	}
}
