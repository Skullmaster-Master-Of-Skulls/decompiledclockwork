using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091D RID: 2333
	internal abstract class ReliableListenerOverReply<TChannel, TReliableChannel> : ReliableListenerOverDatagram<TChannel, TReliableChannel, IReplyChannel, RequestContext> where TChannel : class, IChannel where TReliableChannel : class, IChannel
	{
		// Token: 0x06005986 RID: 22918 RVA: 0x001475C6 File Offset: 0x001457C6
		protected ReliableListenerOverReply(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			base.FaultHelper = new ReplyFaultHelper(context.Binding.SendTimeout, context.Binding.CloseTimeout);
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x001475F1 File Offset: 0x001457F1
		protected override IAsyncResult BeginTryReceiveItem(IReplyChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginTryReceiveRequest(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00147600 File Offset: 0x00145800
		protected override void DisposeItem(RequestContext item)
		{
			((IDisposable)item.RequestMessage).Dispose();
			((IDisposable)item).Dispose();
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x00147613 File Offset: 0x00145813
		protected override void EndTryReceiveItem(IReplyChannel channel, IAsyncResult result, out RequestContext item)
		{
			channel.EndTryReceiveRequest(result, out item);
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x0014761E File Offset: 0x0014581E
		protected override Message GetMessage(RequestContext item)
		{
			return item.RequestMessage;
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x00147626 File Offset: 0x00145826
		protected override void SendReply(Message reply, IReplyChannel channel, RequestContext item)
		{
			if (FaultHelper.AddressReply(item.RequestMessage, reply))
			{
				item.Reply(reply);
			}
		}
	}
}
