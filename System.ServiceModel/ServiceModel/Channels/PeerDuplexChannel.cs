using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F6 RID: 2550
	internal class PeerDuplexChannel : DuplexChannel
	{
		// Token: 0x0600652A RID: 25898 RVA: 0x001795E0 File Offset: 0x001777E0
		public PeerDuplexChannel(PeerNodeImplementation peerNode, PeerNodeImplementation.Registration registration, ChannelManagerBase channelManager, EndpointAddress localAddress, Uri via) : base(channelManager, localAddress)
		{
			PeerNodeImplementation.ValidateVia(via);
			if (registration != null)
			{
				peerNode = PeerNodeImplementation.Get(via, registration);
			}
			this.peerNode = new PeerNode(peerNode);
			this.to = localAddress;
			this.via = via;
			this.channelManager = channelManager;
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x0600652B RID: 25899 RVA: 0x0017962D File Offset: 0x0017782D
		public override EndpointAddress RemoteAddress
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x00179635 File Offset: 0x00177835
		public override Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x0600652D RID: 25901 RVA: 0x0017963D File Offset: 0x0017783D
		public PeerNodeImplementation InnerNode
		{
			get
			{
				return this.peerNode.InnerNode;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x0017964A File Offset: 0x0017784A
		// (set) Token: 0x0600652F RID: 25903 RVA: 0x00179652 File Offset: 0x00177852
		internal PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel> Dispatcher
		{
			get
			{
				return this.messageDispatcher;
			}
			set
			{
				this.messageDispatcher = value;
			}
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0017965B File Offset: 0x0017785B
		protected override void AddHeadersTo(Message message)
		{
			base.AddHeadersTo(message);
			if (this.to != null)
			{
				this.to.ApplyTo(message);
			}
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x00179680 File Offset: 0x00177880
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(PeerNode))
			{
				return (T)((object)this.peerNode);
			}
			if (typeof(T) == typeof(PeerNodeImplementation))
			{
				return (T)((object)this.peerNode.InnerNode);
			}
			if (typeof(T) == typeof(IOnlineStatus))
			{
				return (T)((object)this.peerNode);
			}
			if (typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)FaultConverter.GetDefaultFaultConverter(MessageVersion.Soap12WSAddressing10));
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x00179738 File Offset: 0x00177938
		protected override void OnAbort()
		{
			base.OnAbort();
			if (base.State < CommunicationState.Closed)
			{
				try
				{
					this.peerNode.InnerNode.Abort();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x0017978C File Offset: 0x0017798C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginClose(timeout, callback, state);
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x001797A4 File Offset: 0x001779A4
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginOpen(timeout, callback, state, true);
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001797C8 File Offset: 0x001779C8
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.peerNode.InnerNode.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x00179801 File Offset: 0x00177A01
		protected override void OnClosing()
		{
			base.OnClosing();
			this.ReleaseNode();
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x00179810 File Offset: 0x00177A10
		private void ReleaseNode()
		{
			if (!this.released)
			{
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.released)
					{
						flag = (this.released = true);
					}
				}
				if (flag && this.peerNode != null)
				{
					if (this.messageDispatcher != null)
					{
						this.messageDispatcher.Unregister(false);
					}
					this.peerNode.InnerNode.Release();
				}
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00179898 File Offset: 0x00177A98
		protected override void OnEndClose(IAsyncResult result)
		{
			PeerNodeImplementation.EndClose(result);
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x001798A0 File Offset: 0x00177AA0
		protected override void OnEndOpen(IAsyncResult result)
		{
			PeerNodeImplementation.EndOpen(result);
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x001798A8 File Offset: 0x00177AA8
		protected override void OnEnqueueItem(Message message)
		{
			message.Properties.Via = this.via;
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262205, SR.GetString("TraceCodePeerChannelMessageReceived"), this, message);
			}
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x001798D9 File Offset: 0x00177AD9
		protected override void OnOpen(TimeSpan timeout)
		{
			this.peerNode.OnOpen();
			this.peerNode.InnerNode.Open(timeout, true);
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x001798F8 File Offset: 0x00177AF8
		protected override void OnFaulted()
		{
			base.OnFaulted();
			this.ReleaseNode();
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x00179906 File Offset: 0x00177B06
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			base.EndSend(base.BeginSend(message, timeout, null, null));
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x00179918 File Offset: 0x00177B18
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.securityProtocol == null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.securityProtocol == null)
					{
						this.securityProtocol = ((IPeerFactory)this.channelManager).SecurityManager.CreateSecurityProtocol<IDuplexChannel>(this.to, timeoutHelper.RemainingTime());
					}
				}
			}
			return this.peerNode.InnerNode.BeginSend(this, message, this.via, (ITransportFactorySettings)base.Manager, timeoutHelper.RemainingTime(), callback, state, this.securityProtocol);
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x001799C8 File Offset: 0x00177BC8
		protected override void OnEndSend(IAsyncResult result)
		{
			PeerNodeImplementation.EndSend(result);
		}

		// Token: 0x04003A0D RID: 14861
		private EndpointAddress to;

		// Token: 0x04003A0E RID: 14862
		private Uri via;

		// Token: 0x04003A0F RID: 14863
		private PeerNode peerNode;

		// Token: 0x04003A10 RID: 14864
		private bool released;

		// Token: 0x04003A11 RID: 14865
		private SecurityProtocol securityProtocol;

		// Token: 0x04003A12 RID: 14866
		private ChannelManagerBase channelManager;

		// Token: 0x04003A13 RID: 14867
		private PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel> messageDispatcher;
	}
}
