using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000587 RID: 1415
	internal static class ListenerBinder
	{
		// Token: 0x06003676 RID: 13942 RVA: 0x000D1BE8 File Offset: 0x000CFDE8
		internal static IListenerBinder GetBinder(IChannelListener listener, MessageVersion messageVersion)
		{
			IChannelListener<IInputChannel> channelListener = listener as IChannelListener<IInputChannel>;
			if (channelListener != null)
			{
				return new ListenerBinder.InputListenerBinder(channelListener, messageVersion);
			}
			IChannelListener<IInputSessionChannel> channelListener2 = listener as IChannelListener<IInputSessionChannel>;
			if (channelListener2 != null)
			{
				return new ListenerBinder.InputSessionListenerBinder(channelListener2, messageVersion);
			}
			IChannelListener<IReplyChannel> channelListener3 = listener as IChannelListener<IReplyChannel>;
			if (channelListener3 != null)
			{
				return new ListenerBinder.ReplyListenerBinder(channelListener3, messageVersion);
			}
			IChannelListener<IReplySessionChannel> channelListener4 = listener as IChannelListener<IReplySessionChannel>;
			if (channelListener4 != null)
			{
				return new ListenerBinder.ReplySessionListenerBinder(channelListener4, messageVersion);
			}
			IChannelListener<IDuplexChannel> channelListener5 = listener as IChannelListener<IDuplexChannel>;
			if (channelListener5 != null)
			{
				return new ListenerBinder.DuplexListenerBinder(channelListener5, messageVersion);
			}
			IChannelListener<IDuplexSessionChannel> channelListener6 = listener as IChannelListener<IDuplexSessionChannel>;
			if (channelListener6 != null)
			{
				return new ListenerBinder.DuplexSessionListenerBinder(channelListener6, messageVersion);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownListenerType1", new object[]
			{
				listener.Uri.AbsoluteUri
			})));
		}

		// Token: 0x02000C8D RID: 3213
		private class DuplexListenerBinder : IListenerBinder
		{
			// Token: 0x060078B8 RID: 30904 RVA: 0x001C307E File Offset: 0x001C127E
			internal DuplexListenerBinder(IChannelListener<IDuplexChannel> listener, MessageVersion messageVersion)
			{
				this.correlator = new RequestReplyCorrelator();
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B69 RID: 7017
			// (get) Token: 0x060078B9 RID: 30905 RVA: 0x001C309F File Offset: 0x001C129F
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B6A RID: 7018
			// (get) Token: 0x060078BA RID: 30906 RVA: 0x001C30A7 File Offset: 0x001C12A7
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078BB RID: 30907 RVA: 0x001C30B0 File Offset: 0x001C12B0
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IDuplexChannel duplexChannel = this.listener.AcceptChannel(timeout);
				if (duplexChannel == null)
				{
					return null;
				}
				return new DuplexChannelBinder(duplexChannel, this.correlator, this.listener.Uri);
			}

			// Token: 0x060078BC RID: 30908 RVA: 0x001C30E6 File Offset: 0x001C12E6
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078BD RID: 30909 RVA: 0x001C30F8 File Offset: 0x001C12F8
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IDuplexChannel duplexChannel = this.listener.EndAcceptChannel(result);
				if (duplexChannel == null)
				{
					return null;
				}
				return new DuplexChannelBinder(duplexChannel, this.correlator, this.listener.Uri);
			}

			// Token: 0x040044C6 RID: 17606
			private IRequestReplyCorrelator correlator;

			// Token: 0x040044C7 RID: 17607
			private IChannelListener<IDuplexChannel> listener;

			// Token: 0x040044C8 RID: 17608
			private MessageVersion messageVersion;
		}

		// Token: 0x02000C8E RID: 3214
		private class DuplexSessionListenerBinder : IListenerBinder
		{
			// Token: 0x060078BE RID: 30910 RVA: 0x001C312E File Offset: 0x001C132E
			internal DuplexSessionListenerBinder(IChannelListener<IDuplexSessionChannel> listener, MessageVersion messageVersion)
			{
				this.correlator = new RequestReplyCorrelator();
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B6B RID: 7019
			// (get) Token: 0x060078BF RID: 30911 RVA: 0x001C314F File Offset: 0x001C134F
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B6C RID: 7020
			// (get) Token: 0x060078C0 RID: 30912 RVA: 0x001C3157 File Offset: 0x001C1357
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078C1 RID: 30913 RVA: 0x001C3160 File Offset: 0x001C1360
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IDuplexSessionChannel duplexSessionChannel = this.listener.AcceptChannel(timeout);
				if (duplexSessionChannel == null)
				{
					return null;
				}
				return new DuplexChannelBinder(duplexSessionChannel, this.correlator, this.listener.Uri);
			}

			// Token: 0x060078C2 RID: 30914 RVA: 0x001C3196 File Offset: 0x001C1396
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078C3 RID: 30915 RVA: 0x001C31A8 File Offset: 0x001C13A8
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IDuplexSessionChannel duplexSessionChannel = this.listener.EndAcceptChannel(result);
				if (duplexSessionChannel == null)
				{
					return null;
				}
				return new DuplexChannelBinder(duplexSessionChannel, this.correlator, this.listener.Uri);
			}

			// Token: 0x040044C9 RID: 17609
			private IRequestReplyCorrelator correlator;

			// Token: 0x040044CA RID: 17610
			private IChannelListener<IDuplexSessionChannel> listener;

			// Token: 0x040044CB RID: 17611
			private MessageVersion messageVersion;
		}

		// Token: 0x02000C8F RID: 3215
		private class InputListenerBinder : IListenerBinder
		{
			// Token: 0x060078C4 RID: 30916 RVA: 0x001C31DE File Offset: 0x001C13DE
			internal InputListenerBinder(IChannelListener<IInputChannel> listener, MessageVersion messageVersion)
			{
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B6D RID: 7021
			// (get) Token: 0x060078C5 RID: 30917 RVA: 0x001C31F4 File Offset: 0x001C13F4
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B6E RID: 7022
			// (get) Token: 0x060078C6 RID: 30918 RVA: 0x001C31FC File Offset: 0x001C13FC
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078C7 RID: 30919 RVA: 0x001C3204 File Offset: 0x001C1404
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IInputChannel inputChannel = this.listener.AcceptChannel(timeout);
				if (inputChannel == null)
				{
					return null;
				}
				return new InputChannelBinder(inputChannel, this.listener.Uri);
			}

			// Token: 0x060078C8 RID: 30920 RVA: 0x001C3234 File Offset: 0x001C1434
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078C9 RID: 30921 RVA: 0x001C3244 File Offset: 0x001C1444
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IInputChannel inputChannel = this.listener.EndAcceptChannel(result);
				if (inputChannel == null)
				{
					return null;
				}
				return new InputChannelBinder(inputChannel, this.listener.Uri);
			}

			// Token: 0x040044CC RID: 17612
			private IChannelListener<IInputChannel> listener;

			// Token: 0x040044CD RID: 17613
			private MessageVersion messageVersion;
		}

		// Token: 0x02000C90 RID: 3216
		private class InputSessionListenerBinder : IListenerBinder
		{
			// Token: 0x060078CA RID: 30922 RVA: 0x001C3274 File Offset: 0x001C1474
			internal InputSessionListenerBinder(IChannelListener<IInputSessionChannel> listener, MessageVersion messageVersion)
			{
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B6F RID: 7023
			// (get) Token: 0x060078CB RID: 30923 RVA: 0x001C328A File Offset: 0x001C148A
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B70 RID: 7024
			// (get) Token: 0x060078CC RID: 30924 RVA: 0x001C3292 File Offset: 0x001C1492
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078CD RID: 30925 RVA: 0x001C329C File Offset: 0x001C149C
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IInputSessionChannel inputSessionChannel = this.listener.AcceptChannel(timeout);
				if (inputSessionChannel == null)
				{
					return null;
				}
				return new InputChannelBinder(inputSessionChannel, this.listener.Uri);
			}

			// Token: 0x060078CE RID: 30926 RVA: 0x001C32CC File Offset: 0x001C14CC
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078CF RID: 30927 RVA: 0x001C32DC File Offset: 0x001C14DC
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IInputSessionChannel inputSessionChannel = this.listener.EndAcceptChannel(result);
				if (inputSessionChannel == null)
				{
					return null;
				}
				return new InputChannelBinder(inputSessionChannel, this.listener.Uri);
			}

			// Token: 0x040044CE RID: 17614
			private IChannelListener<IInputSessionChannel> listener;

			// Token: 0x040044CF RID: 17615
			private MessageVersion messageVersion;
		}

		// Token: 0x02000C91 RID: 3217
		private class ReplyListenerBinder : IListenerBinder
		{
			// Token: 0x060078D0 RID: 30928 RVA: 0x001C330C File Offset: 0x001C150C
			internal ReplyListenerBinder(IChannelListener<IReplyChannel> listener, MessageVersion messageVersion)
			{
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B71 RID: 7025
			// (get) Token: 0x060078D1 RID: 30929 RVA: 0x001C3322 File Offset: 0x001C1522
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B72 RID: 7026
			// (get) Token: 0x060078D2 RID: 30930 RVA: 0x001C332A File Offset: 0x001C152A
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078D3 RID: 30931 RVA: 0x001C3334 File Offset: 0x001C1534
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IReplyChannel replyChannel = this.listener.AcceptChannel(timeout);
				if (replyChannel == null)
				{
					return null;
				}
				return new ReplyChannelBinder(replyChannel, this.listener.Uri);
			}

			// Token: 0x060078D4 RID: 30932 RVA: 0x001C3364 File Offset: 0x001C1564
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078D5 RID: 30933 RVA: 0x001C3374 File Offset: 0x001C1574
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IReplyChannel replyChannel = this.listener.EndAcceptChannel(result);
				if (replyChannel == null)
				{
					return null;
				}
				return new ReplyChannelBinder(replyChannel, this.listener.Uri);
			}

			// Token: 0x040044D0 RID: 17616
			private IChannelListener<IReplyChannel> listener;

			// Token: 0x040044D1 RID: 17617
			private MessageVersion messageVersion;
		}

		// Token: 0x02000C92 RID: 3218
		private class ReplySessionListenerBinder : IListenerBinder
		{
			// Token: 0x060078D6 RID: 30934 RVA: 0x001C33A4 File Offset: 0x001C15A4
			internal ReplySessionListenerBinder(IChannelListener<IReplySessionChannel> listener, MessageVersion messageVersion)
			{
				this.listener = listener;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001B73 RID: 7027
			// (get) Token: 0x060078D7 RID: 30935 RVA: 0x001C33BA File Offset: 0x001C15BA
			public IChannelListener Listener
			{
				get
				{
					return this.listener;
				}
			}

			// Token: 0x17001B74 RID: 7028
			// (get) Token: 0x060078D8 RID: 30936 RVA: 0x001C33C2 File Offset: 0x001C15C2
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x060078D9 RID: 30937 RVA: 0x001C33CC File Offset: 0x001C15CC
			public IChannelBinder Accept(TimeSpan timeout)
			{
				IReplySessionChannel replySessionChannel = this.listener.AcceptChannel(timeout);
				if (replySessionChannel == null)
				{
					return null;
				}
				return new ReplyChannelBinder(replySessionChannel, this.listener.Uri);
			}

			// Token: 0x060078DA RID: 30938 RVA: 0x001C33FC File Offset: 0x001C15FC
			public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.listener.BeginAcceptChannel(timeout, callback, state);
			}

			// Token: 0x060078DB RID: 30939 RVA: 0x001C340C File Offset: 0x001C160C
			public IChannelBinder EndAccept(IAsyncResult result)
			{
				IReplySessionChannel replySessionChannel = this.listener.EndAcceptChannel(result);
				if (replySessionChannel == null)
				{
					return null;
				}
				return new ReplyChannelBinder(replySessionChannel, this.listener.Uri);
			}

			// Token: 0x040044D2 RID: 17618
			private IChannelListener<IReplySessionChannel> listener;

			// Token: 0x040044D3 RID: 17619
			private MessageVersion messageVersion;
		}
	}
}
