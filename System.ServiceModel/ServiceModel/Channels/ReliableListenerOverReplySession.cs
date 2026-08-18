using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000920 RID: 2336
	internal abstract class ReliableListenerOverReplySession<TChannel, TReliableChannel> : ReliableListenerOverSession<TChannel, TReliableChannel, IReplySessionChannel, IInputSession, RequestContext> where TChannel : class, IChannel where TReliableChannel : class, IChannel
	{
		// Token: 0x0600599D RID: 22941 RVA: 0x00147A22 File Offset: 0x00145C22
		protected ReliableListenerOverReplySession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			base.FaultHelper = new ReplyFaultHelper(context.Binding.SendTimeout, context.Binding.CloseTimeout);
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x00147A4D File Offset: 0x00145C4D
		protected override IAsyncResult BeginTryReceiveItem(IReplySessionChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginTryReceiveRequest(TimeSpan.MaxValue, callback, channel);
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x00147A5C File Offset: 0x00145C5C
		protected override void DisposeItem(RequestContext item)
		{
			((IDisposable)item.RequestMessage).Dispose();
			((IDisposable)item).Dispose();
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x00147A6F File Offset: 0x00145C6F
		protected override void EndTryReceiveItem(IReplySessionChannel channel, IAsyncResult result, out RequestContext item)
		{
			channel.EndTryReceiveRequest(result, out item);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x00147A7A File Offset: 0x00145C7A
		protected override Message GetMessage(RequestContext item)
		{
			return item.RequestMessage;
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00147A82 File Offset: 0x00145C82
		protected override void SendReply(Message reply, IReplySessionChannel channel, RequestContext item)
		{
			if (FaultHelper.AddressReply(item.RequestMessage, reply))
			{
				item.Reply(reply);
			}
		}
	}
}
