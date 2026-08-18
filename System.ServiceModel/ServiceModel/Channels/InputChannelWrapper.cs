using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072F RID: 1839
	internal class InputChannelWrapper : ChannelWrapper<IInputChannel, Message>, IInputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x00105A4A File Offset: 0x00103C4A
		public InputChannelWrapper(ChannelManagerBase channelManager, IInputChannel innerChannel, Message firstMessage) : base(channelManager, innerChannel, firstMessage)
		{
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x00105A55 File Offset: 0x00103C55
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00105A62 File Offset: 0x00103C62
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00105A6B File Offset: 0x00103C6B
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00105A73 File Offset: 0x00103C73
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00105A78 File Offset: 0x00103C78
		protected override void CloseFirstItem(TimeSpan timeout)
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				TypedChannelDemuxer.AbortMessage(firstItem);
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00105A98 File Offset: 0x00103C98
		public Message Receive()
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return firstItem;
			}
			return base.InnerChannel.Receive();
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00105ABC File Offset: 0x00103CBC
		public Message Receive(TimeSpan timeout)
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return firstItem;
			}
			return base.InnerChannel.Receive(timeout);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00105AE4 File Offset: 0x00103CE4
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginReceive(callback, state);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x00105B14 File Offset: 0x00103D14
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00105B42 File Offset: 0x00103D42
		public Message EndReceive(IAsyncResult result)
		{
			if (result is ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult)
			{
				return ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult.End(result);
			}
			return base.InnerChannel.EndReceive(result);
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x00105B5F File Offset: 0x00103D5F
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			message = base.GetFirstItem();
			return message != null || base.InnerChannel.TryReceive(timeout, out message);
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x00105B7C File Offset: 0x00103D7C
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			Message firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x00105BAA File Offset: 0x00103DAA
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			if (result is ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult)
			{
				message = ChannelWrapper<IInputChannel, Message>.ReceiveAsyncResult.End(result);
				return true;
			}
			return base.InnerChannel.EndTryReceive(result, out message);
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00105BCB File Offset: 0x00103DCB
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.HaveFirstItem() || base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00105BE3 File Offset: 0x00103DE3
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.HaveFirstItem())
			{
				return new ChannelWrapper<IInputChannel, Message>.WaitAsyncResult(callback, state);
			}
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00105C03 File Offset: 0x00103E03
		public bool EndWaitForMessage(IAsyncResult result)
		{
			if (result is ChannelWrapper<IInputChannel, Message>.WaitAsyncResult)
			{
				return ChannelWrapper<IInputChannel, Message>.WaitAsyncResult.End(result);
			}
			return base.InnerChannel.EndWaitForMessage(result);
		}
	}
}
