using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x0200016D RID: 365
	public sealed class PeerNode : IOnlineStatus
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002831A File Offset: 0x0002651A
		internal PeerNode(PeerNodeImplementation peerNode)
		{
			this.innerNode = peerNode;
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000AC2 RID: 2754 RVA: 0x0002832C File Offset: 0x0002652C
		// (remove) Token: 0x06000AC3 RID: 2755 RVA: 0x00028364 File Offset: 0x00026564
		public event EventHandler Offline;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000AC4 RID: 2756 RVA: 0x0002839C File Offset: 0x0002659C
		// (remove) Token: 0x06000AC5 RID: 2757 RVA: 0x000283D4 File Offset: 0x000265D4
		public event EventHandler Online;

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00028409 File Offset: 0x00026609
		internal void FireOffline(object source, EventArgs args)
		{
			this.FireEvent(this.Offline, source, args);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00028419 File Offset: 0x00026619
		internal void FireOnline(object source, EventArgs args)
		{
			this.FireEvent(this.Online, source, args);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002842C File Offset: 0x0002662C
		private void FireEvent(EventHandler handler, object source, EventArgs args)
		{
			if (handler != null)
			{
				try
				{
					SynchronizationContext synchronizationContext = this.synchronizationContext;
					if (synchronizationContext != null)
					{
						synchronizationContext.Send(delegate(object state)
						{
							handler(source, args);
						}, null);
					}
					else
					{
						handler(source, args);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("NotificationException"), ex);
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000284C8 File Offset: 0x000266C8
		public bool IsOnline
		{
			get
			{
				return this.InnerNode.IsOnline;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000284D5 File Offset: 0x000266D5
		internal bool IsOpen
		{
			get
			{
				return this.InnerNode.IsOpen;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000284E2 File Offset: 0x000266E2
		public int Port
		{
			get
			{
				return this.InnerNode.ListenerPort;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000284EF File Offset: 0x000266EF
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x000284FC File Offset: 0x000266FC
		public PeerMessagePropagationFilter MessagePropagationFilter
		{
			get
			{
				return this.InnerNode.MessagePropagationFilter;
			}
			set
			{
				this.InnerNode.MessagePropagationFilter = value;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002850C File Offset: 0x0002670C
		internal void OnOpen()
		{
			this.synchronizationContext = ThreadBehavior.GetCurrentSynchronizationContext();
			this.innerNode.Offline += this.FireOffline;
			this.innerNode.Online += this.FireOnline;
			this.innerNode.EncodingElement = this.encoderElement;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00028563 File Offset: 0x00026763
		internal void OnClose()
		{
			this.innerNode.Offline -= this.FireOffline;
			this.innerNode.Online -= this.FireOnline;
			this.synchronizationContext = null;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002859A File Offset: 0x0002679A
		internal PeerNodeImplementation InnerNode
		{
			get
			{
				return this.innerNode;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000285A4 File Offset: 0x000267A4
		public void RefreshConnection()
		{
			PeerNodeImplementation peerNodeImplementation = this.InnerNode;
			if (peerNodeImplementation != null)
			{
				peerNodeImplementation.RefreshConnection();
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000285C4 File Offset: 0x000267C4
		public override string ToString()
		{
			if (this.IsOpen)
			{
				return SR.GetString("PeerNodeToStringFormat", new object[]
				{
					this.InnerNode.MeshId,
					this.InnerNode.NodeId,
					this.IsOnline,
					this.IsOpen,
					this.Port
				});
			}
			return SR.GetString("PeerNodeToStringFormat", new object[]
			{
				"",
				-1,
				this.IsOnline,
				this.IsOpen,
				-1
			});
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002867B File Offset: 0x0002687B
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00028683 File Offset: 0x00026883
		private MessageEncodingBindingElement EncodingElement
		{
			get
			{
				return this.encoderElement;
			}
			set
			{
				this.encoderElement = value;
			}
		}

		// Token: 0x04000BDD RID: 3037
		private PeerNodeImplementation innerNode;

		// Token: 0x04000BDE RID: 3038
		private SynchronizationContext synchronizationContext;

		// Token: 0x04000BDF RID: 3039
		private MessageEncodingBindingElement encoderElement;
	}
}
