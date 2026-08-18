using System;
using System.Runtime;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E7 RID: 2023
	internal abstract class FramingDuplexSessionChannel : TransportDuplexSessionChannel
	{
		// Token: 0x06004C86 RID: 19590 RVA: 0x00117350 File Offset: 0x00115550
		private FramingDuplexSessionChannel(ChannelManagerBase manager, IConnectionOrientedTransportFactorySettings settings, EndpointAddress localAddress, Uri localVia, EndpointAddress remoteAddresss, Uri via, bool exposeConnectionProperty) : base(manager, settings, localAddress, localVia, remoteAddresss, via)
		{
			this.exposeConnectionProperty = exposeConnectionProperty;
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x00117369 File Offset: 0x00115569
		protected FramingDuplexSessionChannel(ChannelManagerBase factory, IConnectionOrientedTransportFactorySettings settings, EndpointAddress remoteAddresss, Uri via, bool exposeConnectionProperty) : this(factory, settings, EndpointAddress.AnonymousAddress, settings.MessageVersion.Addressing.AnonymousUri, remoteAddresss, via, exposeConnectionProperty)
		{
			base.Session = FramingDuplexSessionChannel.FramingConnectionDuplexSession.CreateSession(this, settings.Upgrade);
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x0011739F File Offset: 0x0011559F
		protected FramingDuplexSessionChannel(ConnectionOrientedTransportChannelListener channelListener, EndpointAddress localAddress, Uri localVia, bool exposeConnectionProperty) : this(channelListener, channelListener, localAddress, localVia, EndpointAddress.AnonymousAddress, channelListener.MessageVersion.Addressing.AnonymousUri, exposeConnectionProperty)
		{
			base.Session = FramingDuplexSessionChannel.FramingConnectionDuplexSession.CreateSession(this, channelListener.Upgrade);
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x001173D4 File Offset: 0x001155D4
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x001173DC File Offset: 0x001155DC
		protected IConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x001173E5 File Offset: 0x001155E5
		protected override bool IsStreamedOutput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x001173E8 File Offset: 0x001155E8
		protected override void CloseOutputSessionCore(TimeSpan timeout)
		{
			this.Connection.Write(SessionEncoder.EndBytes, 0, SessionEncoder.EndBytes.Length, true, timeout);
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x00117404 File Offset: 0x00115604
		protected override void CompleteClose(TimeSpan timeout)
		{
			this.ReturnConnectionIfNecessary(false, timeout);
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0011740E File Offset: 0x0011560E
		protected override void PrepareMessage(Message message)
		{
			if (this.exposeConnectionProperty)
			{
				message.Properties[ConnectionMessageProperty.Name] = this.connection;
			}
			base.PrepareMessage(message);
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x00117438 File Offset: 0x00115638
		protected override void OnSendCore(Message message, TimeSpan timeout)
		{
			bool allowOutputBatching = message.Properties.AllowOutputBatching;
			ArraySegment<byte> arraySegment = this.EncodeMessage(message);
			this.Connection.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count, !allowOutputBatching, timeout, base.BufferManager);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x00117484 File Offset: 0x00115684
		protected override AsyncCompletionResult BeginCloseOutput(TimeSpan timeout, WaitCallback callback, object state)
		{
			return this.Connection.BeginWrite(SessionEncoder.EndBytes, 0, SessionEncoder.EndBytes.Length, true, timeout, callback, state);
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x001174A2 File Offset: 0x001156A2
		protected override void FinishWritingMessage()
		{
			this.Connection.EndWrite();
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x001174AF File Offset: 0x001156AF
		protected override AsyncCompletionResult StartWritingBufferedMessage(Message message, ArraySegment<byte> messageData, bool allowOutputBatching, TimeSpan timeout, WaitCallback callback, object state)
		{
			return this.Connection.BeginWrite(messageData.Array, messageData.Offset, messageData.Count, !allowOutputBatching, timeout, callback, state);
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x001174DB File Offset: 0x001156DB
		protected override AsyncCompletionResult StartWritingStreamedMessage(Message message, TimeSpan timeout, WaitCallback callback, object state)
		{
			throw FxTrace.Exception.AsError(new InvalidOperationException());
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x001174EC File Offset: 0x001156EC
		protected override ArraySegment<byte> EncodeMessage(Message message)
		{
			ArraySegment<byte> messageFrame = base.MessageEncoder.WriteMessage(message, int.MaxValue, base.BufferManager, 6);
			return SessionEncoder.EncodeMessageFrame(messageFrame);
		}

		// Token: 0x04002FB6 RID: 12214
		private IConnection connection;

		// Token: 0x04002FB7 RID: 12215
		private bool exposeConnectionProperty;

		// Token: 0x02000D08 RID: 3336
		private class FramingConnectionDuplexSession : TransportDuplexSessionChannel.ConnectionDuplexSession
		{
			// Token: 0x06007B04 RID: 31492 RVA: 0x001CA398 File Offset: 0x001C8598
			private FramingConnectionDuplexSession(FramingDuplexSessionChannel channel) : base(channel)
			{
			}

			// Token: 0x06007B05 RID: 31493 RVA: 0x001CA3A4 File Offset: 0x001C85A4
			public static FramingDuplexSessionChannel.FramingConnectionDuplexSession CreateSession(FramingDuplexSessionChannel channel, StreamUpgradeProvider upgrade)
			{
				if (!(upgrade is StreamSecurityUpgradeProvider))
				{
					return new FramingDuplexSessionChannel.FramingConnectionDuplexSession(channel);
				}
				return new FramingDuplexSessionChannel.FramingConnectionDuplexSession.SecureConnectionDuplexSession(channel);
			}

			// Token: 0x02000F44 RID: 3908
			private class SecureConnectionDuplexSession : FramingDuplexSessionChannel.FramingConnectionDuplexSession, ISecuritySession, ISession
			{
				// Token: 0x060086C1 RID: 34497 RVA: 0x001F321A File Offset: 0x001F141A
				public SecureConnectionDuplexSession(FramingDuplexSessionChannel channel) : base(channel)
				{
				}

				// Token: 0x17001D8F RID: 7567
				// (get) Token: 0x060086C2 RID: 34498 RVA: 0x001F3224 File Offset: 0x001F1424
				EndpointIdentity ISecuritySession.RemoteIdentity
				{
					get
					{
						if (this.remoteIdentity == null)
						{
							SecurityMessageProperty remoteSecurity = base.Channel.RemoteSecurity;
							if (remoteSecurity != null && remoteSecurity.ServiceSecurityContext != null && remoteSecurity.ServiceSecurityContext.IdentityClaim != null && remoteSecurity.ServiceSecurityContext.PrimaryIdentity != null)
							{
								this.remoteIdentity = EndpointIdentity.CreateIdentity(remoteSecurity.ServiceSecurityContext.IdentityClaim);
							}
						}
						return this.remoteIdentity;
					}
				}

				// Token: 0x04004E4B RID: 20043
				private EndpointIdentity remoteIdentity;
			}
		}
	}
}
