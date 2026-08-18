using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A21 RID: 2593
	internal class PeerOutputChannel : TransportOutputChannel
	{
		// Token: 0x06006717 RID: 26391 RVA: 0x0018147C File Offset: 0x0017F67C
		public PeerOutputChannel(PeerNodeImplementation peerNode, PeerNodeImplementation.Registration registration, ChannelManagerBase channelManager, EndpointAddress localAddress, Uri via, MessageVersion messageVersion) : base(channelManager, localAddress, via, false, messageVersion)
		{
			PeerNodeImplementation.ValidateVia(via);
			if (registration != null)
			{
				peerNode = PeerNodeImplementation.Get(via, registration);
			}
			this.peerNode = new PeerNode(peerNode);
			this.via = via;
			this.channelManager = channelManager;
			this.to = localAddress;
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x001814D0 File Offset: 0x0017F6D0
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

		// Token: 0x06006719 RID: 26393 RVA: 0x00181588 File Offset: 0x0017F788
		protected override void OnAbort()
		{
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

		// Token: 0x0600671A RID: 26394 RVA: 0x001815D4 File Offset: 0x0017F7D4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginClose(timeout, callback, state);
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x001815EC File Offset: 0x0017F7EC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginOpen(timeout, callback, state, true);
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x0018160F File Offset: 0x0017F80F
		protected override void OnClose(TimeSpan timeout)
		{
			this.peerNode.InnerNode.Close(timeout);
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x00181622 File Offset: 0x0017F822
		protected override void OnClosing()
		{
			base.OnClosing();
			this.ReleaseNode();
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x00181630 File Offset: 0x0017F830
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
					this.peerNode.InnerNode.Release();
				}
			}
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x001816A4 File Offset: 0x0017F8A4
		protected override void OnOpen(TimeSpan timeout)
		{
			this.peerNode.OnOpen();
			this.peerNode.InnerNode.Open(timeout, true);
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x001816C3 File Offset: 0x0017F8C3
		protected override void OnFaulted()
		{
			base.OnFaulted();
			this.ReleaseNode();
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x001816D1 File Offset: 0x0017F8D1
		protected override void OnEndClose(IAsyncResult result)
		{
			PeerNodeImplementation.EndClose(result);
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x001816D9 File Offset: 0x0017F8D9
		protected override void OnEndOpen(IAsyncResult result)
		{
			PeerNodeImplementation.EndOpen(result);
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x001816E1 File Offset: 0x0017F8E1
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			base.EndSend(base.BeginSend(message, timeout, null, null));
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x001816F4 File Offset: 0x0017F8F4
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
						this.securityProtocol = ((IPeerFactory)this.channelManager).SecurityManager.CreateSecurityProtocol<IOutputChannel>(this.to, timeoutHelper.RemainingTime());
					}
				}
			}
			return this.peerNode.InnerNode.BeginSend(this, message, this.via, (ITransportFactorySettings)base.Manager, timeoutHelper.RemainingTime(), callback, state, this.securityProtocol);
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x001817A4 File Offset: 0x0017F9A4
		protected override void OnEndSend(IAsyncResult result)
		{
			PeerNodeImplementation.EndSend(result);
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x001817AC File Offset: 0x0017F9AC
		protected override void AddHeadersTo(Message message)
		{
			this.RemoteAddress.ApplyTo(message);
		}

		// Token: 0x04003B3D RID: 15165
		private PeerNode peerNode;

		// Token: 0x04003B3E RID: 15166
		private Uri via;

		// Token: 0x04003B3F RID: 15167
		private EndpointAddress to;

		// Token: 0x04003B40 RID: 15168
		private SecurityProtocol securityProtocol;

		// Token: 0x04003B41 RID: 15169
		private bool released;

		// Token: 0x04003B42 RID: 15170
		private ChannelManagerBase channelManager;
	}
}
