using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A35 RID: 2613
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
	internal class PeerService : IPeerService, IPeerServiceContract, IServiceBehavior, IChannelInitializer
	{
		// Token: 0x060067B6 RID: 26550 RVA: 0x00183892 File Offset: 0x00181A92
		public PeerService(PeerNodeConfig config, PeerService.ChannelCallback channelCallback, PeerService.GetNeighborCallback getNeighborCallback, Dictionary<Type, object> services) : this(config, channelCallback, getNeighborCallback, services, null)
		{
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x001838A0 File Offset: 0x00181AA0
		public PeerService(PeerNodeConfig config, PeerService.ChannelCallback channelCallback, PeerService.GetNeighborCallback getNeighborCallback, Dictionary<Type, object> services, IPeerNodeMessageHandling messageHandler)
		{
			this.config = config;
			this.newChannelCallback = channelCallback;
			this.getNeighborCallback = getNeighborCallback;
			this.messageHandler = messageHandler;
			if (services != null)
			{
				object obj = null;
				services.TryGetValue(typeof(IPeerConnectorContract), out obj);
				this.connector = (obj as IPeerConnectorContract);
				obj = null;
				services.TryGetValue(typeof(IPeerFlooderContract<Message, UtilityInfo>), out obj);
				this.flooder = (obj as IPeerFlooderContract<Message, UtilityInfo>);
			}
			this.serviceHost = new ServiceHost(this, new Uri[0]);
			ServiceThrottlingBehavior serviceThrottlingBehavior = new ServiceThrottlingBehavior();
			serviceThrottlingBehavior.MaxConcurrentCalls = this.config.MaxPendingIncomingCalls;
			serviceThrottlingBehavior.MaxConcurrentSessions = this.config.MaxConcurrentSessions;
			this.serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x00183968 File Offset: 0x00181B68
		public void Abort()
		{
			this.serviceHost.Abort();
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x00183975 File Offset: 0x00181B75
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00183980 File Offset: 0x00181B80
		private void CreateBinding()
		{
			Collection<BindingElement> collection = new Collection<BindingElement>();
			BindingElement securityBindingElement = this.config.SecurityManager.GetSecurityBindingElement();
			if (securityBindingElement != null)
			{
				collection.Add(securityBindingElement);
			}
			TcpTransportBindingElement tcpTransportBindingElement = new TcpTransportBindingElement();
			tcpTransportBindingElement.MaxReceivedMessageSize = this.config.MaxReceivedMessageSize;
			tcpTransportBindingElement.MaxBufferPoolSize = this.config.MaxBufferPoolSize;
			tcpTransportBindingElement.TeredoEnabled = true;
			MessageEncodingBindingElement messageEncodingBindingElement = null;
			if (this.messageHandler != null)
			{
				messageEncodingBindingElement = this.messageHandler.EncodingBindingElement;
			}
			if (messageEncodingBindingElement == null)
			{
				BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
				this.config.ReaderQuotas.CopyTo(binaryMessageEncodingBindingElement.ReaderQuotas);
				collection.Add(binaryMessageEncodingBindingElement);
			}
			else
			{
				collection.Add(messageEncodingBindingElement);
			}
			collection.Add(tcpTransportBindingElement);
			this.binding = new CustomBinding(collection);
			this.binding.ReceiveTimeout = TimeSpan.MaxValue;
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x00183A48 File Offset: 0x00181C48
		public EndpointAddress GetListenAddress()
		{
			IChannelListener listener = this.serviceHost.ChannelDispatchers[0].Listener;
			return new EndpointAddress(listener.Uri, listener.GetProperty<EndpointIdentity>(), new AddressHeader[0]);
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x00183A84 File Offset: 0x00181C84
		private IPeerNeighbor GetNeighbor()
		{
			IPeerNeighbor peerNeighbor = this.getNeighborCallback(OperationContext.Current.GetCallbackChannel<IPeerProxy>());
			if (peerNeighbor == null || peerNeighbor.State == PeerNeighborState.Closed)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 262198, SR.GetString("TraceCodePeerNeighborNotFound"), new PeerNodeTraceRecord(this.config.NodeId), OperationContext.Current.IncomingMessage);
				}
				return null;
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				PeerNeighborState state = peerNeighbor.State;
				PeerNodeAddress listenAddress = null;
				IPAddress connectIPAddress = null;
				if (state >= PeerNeighborState.Opened && state <= PeerNeighborState.Connected)
				{
					listenAddress = this.config.GetListenAddress(true);
					connectIPAddress = this.config.ListenIPAddress;
				}
				PeerNeighborTraceRecord extendedData = new PeerNeighborTraceRecord(peerNeighbor.NodeId, this.config.NodeId, listenAddress, connectIPAddress, peerNeighbor.GetHashCode(), peerNeighbor.IsInitiator, state.ToString(), null, null, OperationContext.Current.IncomingMessage.Headers.Action);
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262202, SR.GetString("TraceCodePeerNeighborMessageReceived"), extendedData, this, null);
			}
			return peerNeighbor;
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x00183B88 File Offset: 0x00181D88
		public void Open(TimeSpan timeout)
		{
			this.CreateBinding();
			this.serviceHost.Description.Endpoints.Clear();
			ServiceEndpoint serviceEndpoint = this.serviceHost.AddServiceEndpoint(typeof(IPeerService), this.binding, this.config.GetMeshUri());
			serviceEndpoint.ListenUri = this.config.GetSelfUri();
			serviceEndpoint.ListenUriMode = ((this.config.Port > 0) ? ListenUriMode.Explicit : ListenUriMode.Unique);
			this.config.SecurityManager.ApplyServiceSecurity(this.serviceHost.Description);
			this.serviceHost.Open(timeout);
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262224, SR.GetString("TraceCodePeerServiceOpened", new object[]
				{
					this.GetListenAddress()
				}), this);
			}
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x00183C53 File Offset: 0x00181E53
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHost)
		{
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00183C55 File Offset: 0x00181E55
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHost, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x00183C58 File Offset: 0x00181E58
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHost)
		{
			for (int i = 0; i < serviceHost.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHost.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					bool flag = false;
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						if (!endpointDispatcher.IsSystemEndpoint)
						{
							if (!flag)
							{
								channelDispatcher.ChannelInitializers.Add(this);
								flag = true;
							}
							endpointDispatcher.DispatchRuntime.OperationSelector = new OperationSelector(this.messageHandler);
						}
					}
				}
			}
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x00183D00 File Offset: 0x00181F00
		void IChannelInitializer.Initialize(IClientChannel channel)
		{
			this.newChannelCallback(channel);
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x00183D10 File Offset: 0x00181F10
		void IPeerServiceContract.Connect(ConnectInfo connectInfo)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				this.connector.Connect(neighbor, connectInfo);
			}
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x00183D34 File Offset: 0x00181F34
		void IPeerServiceContract.Disconnect(DisconnectInfo disconnectInfo)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				this.connector.Disconnect(neighbor, disconnectInfo);
			}
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x00183D58 File Offset: 0x00181F58
		void IPeerServiceContract.Refuse(RefuseInfo refuseInfo)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				this.connector.Refuse(neighbor, refuseInfo);
			}
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x00183D7C File Offset: 0x00181F7C
		void IPeerServiceContract.Welcome(WelcomeInfo welcomeInfo)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				this.connector.Welcome(neighbor, welcomeInfo);
			}
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x00183DA0 File Offset: 0x00181FA0
		IAsyncResult IPeerServiceContract.BeginFloodMessage(Message floodedInfo, AsyncCallback callback, object state)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				return this.flooder.OnFloodedMessage(neighbor, floodedInfo, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x00183DCE File Offset: 0x00181FCE
		void IPeerServiceContract.EndFloodMessage(IAsyncResult result)
		{
			this.flooder.EndFloodMessage(result);
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x00183DDC File Offset: 0x00181FDC
		void IPeerServiceContract.LinkUtility(UtilityInfo utilityInfo)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor != null)
			{
				this.flooder.ProcessLinkUtility(neighbor, utilityInfo);
			}
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x00183E00 File Offset: 0x00182000
		Message IPeerServiceContract.ProcessRequestSecurityToken(Message message)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(typeof(IPeerNeighbor).ToString()));
			}
			Message message2 = this.config.SecurityManager.ProcessRequest(neighbor, message);
			if (message2 == null)
			{
				OperationContext operationContext = OperationContext.Current;
				operationContext.RequestContext.Close();
				operationContext.RequestContext = null;
			}
			return message2;
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x00183E68 File Offset: 0x00182068
		void IPeerServiceContract.Fault(Message message)
		{
			IPeerNeighbor neighbor = this.GetNeighbor();
			if (neighbor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(typeof(IPeerNeighbor).ToString()));
			}
			neighbor.Abort(PeerCloseReason.Faulted, PeerCloseInitiator.RemoteNode);
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x00183EA6 File Offset: 0x001820A6
		void IPeerServiceContract.Ping(Message message)
		{
		}

		// Token: 0x04003B86 RID: 15238
		private Binding binding;

		// Token: 0x04003B87 RID: 15239
		private PeerNodeConfig config;

		// Token: 0x04003B88 RID: 15240
		private PeerService.ChannelCallback newChannelCallback;

		// Token: 0x04003B89 RID: 15241
		private PeerService.GetNeighborCallback getNeighborCallback;

		// Token: 0x04003B8A RID: 15242
		private ServiceHost serviceHost;

		// Token: 0x04003B8B RID: 15243
		private IPeerConnectorContract connector;

		// Token: 0x04003B8C RID: 15244
		private IPeerFlooderContract<Message, UtilityInfo> flooder;

		// Token: 0x04003B8D RID: 15245
		private IPeerNodeMessageHandling messageHandler;

		// Token: 0x02000E72 RID: 3698
		// (Invoke) Token: 0x060083E3 RID: 33763
		public delegate bool ChannelCallback(IClientChannel channel);

		// Token: 0x02000E73 RID: 3699
		// (Invoke) Token: 0x060083E7 RID: 33767
		public delegate IPeerNeighbor GetNeighborCallback(IPeerProxy channel);
	}
}
