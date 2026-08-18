using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A12 RID: 2578
	internal class PeerInputChannel : InputChannel
	{
		// Token: 0x060065FB RID: 26107 RVA: 0x0017BD4D File Offset: 0x00179F4D
		public PeerInputChannel(PeerNodeImplementation peerNode, PeerNodeImplementation.Registration registration, ChannelManagerBase channelManager, EndpointAddress localAddress, Uri via) : base(channelManager, localAddress)
		{
			PeerNodeImplementation.ValidateVia(via);
			if (registration != null)
			{
				peerNode = PeerNodeImplementation.Get(via, registration);
			}
			this.peerNode = new PeerNode(peerNode);
			this.to = localAddress;
			this.via = via;
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x0017BD88 File Offset: 0x00179F88
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

		// Token: 0x060065FD RID: 26109 RVA: 0x0017BE40 File Offset: 0x0017A040
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

		// Token: 0x060065FE RID: 26110 RVA: 0x0017BE94 File Offset: 0x0017A094
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginCloseNode), new ChainedEndHandler(this.OnEndCloseNode), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x0017BECE File Offset: 0x0017A0CE
		private IAsyncResult OnBeginCloseNode(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginClose(timeout, callback, state);
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x0017BEE3 File Offset: 0x0017A0E3
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ChainedBeginHandler(this.OnBeginOpenNode), new ChainedEndHandler(this.OnEndOpenNode));
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x0017BF20 File Offset: 0x0017A120
		private IAsyncResult OnBeginOpenNode(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.peerNode.InnerNode.BeginOpen(timeout, callback, state, true);
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x0017BF44 File Offset: 0x0017A144
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.peerNode.InnerNode.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x0017BF7D File Offset: 0x0017A17D
		protected override void OnClosing()
		{
			base.OnClosing();
			this.ReleaseNode();
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x0017BF8C File Offset: 0x0017A18C
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
				if (flag)
				{
					this.peerNode.InnerNode.Release();
				}
			}
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x0017BFF8 File Offset: 0x0017A1F8
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x0017C000 File Offset: 0x0017A200
		private void OnEndCloseNode(IAsyncResult result)
		{
			PeerNodeImplementation.EndClose(result);
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x0017C008 File Offset: 0x0017A208
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x0017C010 File Offset: 0x0017A210
		private void OnEndOpenNode(IAsyncResult result)
		{
			PeerNodeImplementation.EndOpen(result);
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x0017C018 File Offset: 0x0017A218
		protected override void OnEnqueueItem(Message message)
		{
			message.Properties.Via = this.via;
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262205, SR.GetString("TraceCodePeerChannelMessageReceived"), this, message);
			}
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x0017C04C File Offset: 0x0017A24C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.peerNode.OnOpen();
			this.peerNode.InnerNode.Open(timeoutHelper.RemainingTime(), true);
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x0017C091 File Offset: 0x0017A291
		protected override void OnFaulted()
		{
			base.OnFaulted();
			this.ReleaseNode();
		}

		// Token: 0x04003AD5 RID: 15061
		private EndpointAddress to;

		// Token: 0x04003AD6 RID: 15062
		private Uri via;

		// Token: 0x04003AD7 RID: 15063
		private PeerNode peerNode;

		// Token: 0x04003AD8 RID: 15064
		private bool released;
	}
}
