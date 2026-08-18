using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F2 RID: 2546
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal abstract class PeerChannelListenerBase : TransportChannelListener, IPeerFactory, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x060064CB RID: 25803 RVA: 0x0017818C File Offset: 0x0017638C
		internal PeerChannelListenerBase(PeerTransportBindingElement bindingElement, BindingContext context, PeerResolver peerResolver) : base(bindingElement, context)
		{
			this.listenIPAddress = bindingElement.ListenIPAddress;
			this.port = bindingElement.Port;
			this.resolver = peerResolver;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = context.Binding.Elements.Find<BinaryMessageEncodingBindingElement>();
			if (binaryMessageEncodingBindingElement != null)
			{
				binaryMessageEncodingBindingElement.ReaderQuotas.CopyTo(this.readerQuotas);
			}
			else
			{
				EncoderDefaults.ReaderQuotas.CopyTo(this.readerQuotas);
			}
			this.securityManager = PeerSecurityManager.Create(bindingElement.Security, context, this.readerQuotas);
			this.securityCapabilities = bindingElement.GetProperty<ISecurityCapabilities>(context);
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x00178227 File Offset: 0x00176427
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.listenIPAddress;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x0017822F File Offset: 0x0017642F
		internal PeerNodeImplementation InnerNode
		{
			get
			{
				if (this.peerNode == null)
				{
					return null;
				}
				return this.peerNode.InnerNode;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x00178246 File Offset: 0x00176446
		internal PeerNodeImplementation.Registration Registration
		{
			get
			{
				return this.registration;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x0017824E File Offset: 0x0017644E
		// (set) Token: 0x060064D0 RID: 25808 RVA: 0x00178256 File Offset: 0x00176456
		public PeerNodeImplementation PrivatePeerNode
		{
			get
			{
				return this.privatePeerNode;
			}
			set
			{
				this.privatePeerNode = value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0017825F File Offset: 0x0017645F
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x00178267 File Offset: 0x00176467
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x060064D3 RID: 25811 RVA: 0x0017826F File Offset: 0x0017646F
		public PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x00178277 File Offset: 0x00176477
		// (set) Token: 0x060064D5 RID: 25813 RVA: 0x0017827F File Offset: 0x0017647F
		public PeerSecurityManager SecurityManager
		{
			get
			{
				return this.securityManager;
			}
			set
			{
				this.securityManager = value;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x00178288 File Offset: 0x00176488
		// (set) Token: 0x060064D7 RID: 25815 RVA: 0x00178290 File Offset: 0x00176490
		protected SecurityProtocol SecurityProtocol
		{
			get
			{
				return this.securityProtocol;
			}
			set
			{
				this.securityProtocol = value;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x00178299 File Offset: 0x00176499
		public override string Scheme
		{
			get
			{
				return "net.p2p";
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x001782A0 File Offset: 0x001764A0
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return PeerChannelListenerBase.transportManagerTable;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x001782A7 File Offset: 0x001764A7
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return PeerChannelListenerBase.transportManagerTable;
			}
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x001782B0 File Offset: 0x001764B0
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(PeerNode))
			{
				return this.peerNode as T;
			}
			if (typeof(T) == typeof(IOnlineStatus))
			{
				return this.peerNode as T;
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.securityCapabilities);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x00178344 File Offset: 0x00176544
		protected override void OnAbort()
		{
			base.OnAbort();
			if (base.State < CommunicationState.Closed && this.peerNode != null)
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

		// Token: 0x060064DD RID: 25821 RVA: 0x001783A0 File Offset: 0x001765A0
		private void OnCloseCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.peerNode.OnClose();
			this.peerNode.InnerNode.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001783E4 File Offset: 0x001765E4
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(timeout);
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001783F0 File Offset: 0x001765F0
		protected override void OnClosing()
		{
			base.OnClosing();
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

		// Token: 0x060064E0 RID: 25824 RVA: 0x00178468 File Offset: 0x00176668
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper data = new TimeoutHelper(timeout);
			return new CompletedAsyncResult<TimeoutHelper>(data, callback, state);
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x00178488 File Offset: 0x00176688
		protected override void OnEndClose(IAsyncResult result)
		{
			this.OnCloseCore(CompletedAsyncResult<TimeoutHelper>.End(result).RemainingTime());
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x001784AC File Offset: 0x001766AC
		private void OnOpenCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.peerNode.OnOpen();
			this.peerNode.InnerNode.Open(timeoutHelper.RemainingTime(), false);
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x001784F1 File Offset: 0x001766F1
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnOpenCore(timeout);
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x001784FC File Offset: 0x001766FC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper data = new TimeoutHelper(timeout);
			return new CompletedAsyncResult<TimeoutHelper>(data, callback, state);
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x0017851C File Offset: 0x0017671C
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.OnOpenCore(CompletedAsyncResult<TimeoutHelper>.End(result).RemainingTime());
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x0017853D File Offset: 0x0017673D
		protected override void OnFaulted()
		{
			this.OnAbort();
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x00178548 File Offset: 0x00176748
		internal override IList<TransportManager> SelectTransportManagers()
		{
			if (this.peerNode == null)
			{
				PeerNodeImplementation peerNodeImplementation;
				if (this.privatePeerNode != null && this.Uri.Host == this.privatePeerNode.MeshId)
				{
					peerNodeImplementation = this.privatePeerNode;
					this.registration = null;
				}
				else
				{
					this.registration = new PeerNodeImplementation.Registration(this.Uri, this);
					peerNodeImplementation = PeerNodeImplementation.Get(this.Uri, this.registration);
				}
				if (peerNodeImplementation.MaxReceivedMessageSize < this.MaxReceivedMessageSize)
				{
					peerNodeImplementation.Release();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerMaxReceivedMessageSizeConflict", new object[]
					{
						this.MaxReceivedMessageSize,
						peerNodeImplementation.MaxReceivedMessageSize,
						this.Uri
					})));
				}
				this.peerNode = new PeerNode(peerNodeImplementation);
			}
			return null;
		}

		// Token: 0x040039F0 RID: 14832
		private IPAddress listenIPAddress;

		// Token: 0x040039F1 RID: 14833
		private int port;

		// Token: 0x040039F2 RID: 14834
		private PeerResolver resolver;

		// Token: 0x040039F3 RID: 14835
		private PeerNode peerNode;

		// Token: 0x040039F4 RID: 14836
		private PeerNodeImplementation privatePeerNode;

		// Token: 0x040039F5 RID: 14837
		private PeerNodeImplementation.Registration registration;

		// Token: 0x040039F6 RID: 14838
		private bool released;

		// Token: 0x040039F7 RID: 14839
		private PeerSecurityManager securityManager;

		// Token: 0x040039F8 RID: 14840
		private SecurityProtocol securityProtocol;

		// Token: 0x040039F9 RID: 14841
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039FA RID: 14842
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x040039FB RID: 14843
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>(true);
	}
}
