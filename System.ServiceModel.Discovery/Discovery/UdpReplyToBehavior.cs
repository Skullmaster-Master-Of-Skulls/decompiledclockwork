using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000058 RID: 88
	internal class UdpReplyToBehavior : IEndpointBehavior, IDispatchMessageInspector, IClientMessageInspector
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000CB9E File Offset: 0x0000AD9E
		public UdpReplyToBehavior(string scheme)
		{
			this.scheme = scheme;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000CBAD File Offset: 0x0000ADAD
		private static EndpointAddress AnnonymousAddress
		{
			get
			{
				if (UdpReplyToBehavior.annonymousAddress == null)
				{
					UdpReplyToBehavior.annonymousAddress = new EndpointAddress(EndpointAddress.AnonymousUri, new AddressHeader[0]);
				}
				return UdpReplyToBehavior.annonymousAddress;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDispatcher");
			}
			endpointDispatcher.AddressFilter = new UdpDiscoveryMessageFilter(endpointDispatcher.AddressFilter);
			endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
			if (endpointDispatcher.DispatchRuntime.CallbackClientRuntime != null)
			{
				endpointDispatcher.DispatchRuntime.CallbackClientRuntime.MessageInspectors.Add(this);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			object obj = null;
			UdpReplyToBehavior.UdpAddressingState udpAddressingState = null;
			if (OperationContext.Current.IncomingMessageProperties.TryGetValue(RemoteEndpointMessageProperty.Name, out obj))
			{
				RemoteEndpointMessageProperty remoteEndpointMessageProperty = obj as RemoteEndpointMessageProperty;
				if (remoteEndpointMessageProperty != null)
				{
					UriBuilder uriBuilder = new UriBuilder();
					uriBuilder.Scheme = this.scheme;
					uriBuilder.Host = remoteEndpointMessageProperty.Address;
					uriBuilder.Port = remoteEndpointMessageProperty.Port;
					udpAddressingState = new UdpReplyToBehavior.UdpAddressingState();
					udpAddressingState.RemoteEndpointAddress = uriBuilder.Uri;
					OperationContext.Current.IncomingMessageHeaders.ReplyTo = UdpReplyToBehavior.AnnonymousAddress;
				}
			}
			NetworkInterfaceMessageProperty networkInterfaceMessageProperty;
			if (NetworkInterfaceMessageProperty.TryGet(OperationContext.Current.IncomingMessageProperties, out networkInterfaceMessageProperty))
			{
				if (udpAddressingState == null)
				{
					udpAddressingState = new UdpReplyToBehavior.UdpAddressingState();
				}
				udpAddressingState.NetworkInterfaceMessageProperty = networkInterfaceMessageProperty;
			}
			if (udpAddressingState != null)
			{
				DiscoveryMessageProperty value = new DiscoveryMessageProperty(udpAddressingState);
				OperationContext.Current.IncomingMessageProperties["System.ServiceModel.Discovery.DiscoveryMessageProperty"] = value;
			}
			return null;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000030E1 File Offset: 0x000012E1
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000030E1 File Offset: 0x000012E1
		void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CD10 File Offset: 0x0000AF10
		object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			object obj;
			if (OperationContext.Current.OutgoingMessageProperties.TryGetValue("System.ServiceModel.Discovery.DiscoveryMessageProperty", out obj))
			{
				DiscoveryMessageProperty discoveryMessageProperty = obj as DiscoveryMessageProperty;
				if (discoveryMessageProperty != null)
				{
					UdpReplyToBehavior.UdpAddressingState udpAddressingState = discoveryMessageProperty.CorrelationState as UdpReplyToBehavior.UdpAddressingState;
					if (udpAddressingState != null)
					{
						if (udpAddressingState.RemoteEndpointAddress != null)
						{
							UdpReplyToBehavior.AnnonymousAddress.ApplyTo(request);
							request.Properties.Via = udpAddressingState.RemoteEndpointAddress;
						}
						if (udpAddressingState.NetworkInterfaceMessageProperty != null)
						{
							udpAddressingState.NetworkInterfaceMessageProperty.AddTo(request);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000110 RID: 272
		private static EndpointAddress annonymousAddress;

		// Token: 0x04000111 RID: 273
		private string scheme;

		// Token: 0x020000EE RID: 238
		private class UdpAddressingState
		{
			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000850 RID: 2128 RVA: 0x000155A8 File Offset: 0x000137A8
			// (set) Token: 0x06000851 RID: 2129 RVA: 0x000155B0 File Offset: 0x000137B0
			public Uri RemoteEndpointAddress
			{
				get
				{
					return this.remoteEndpontAddress;
				}
				set
				{
					this.remoteEndpontAddress = value;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000852 RID: 2130 RVA: 0x000155B9 File Offset: 0x000137B9
			// (set) Token: 0x06000853 RID: 2131 RVA: 0x000155C1 File Offset: 0x000137C1
			public NetworkInterfaceMessageProperty NetworkInterfaceMessageProperty
			{
				get
				{
					return this.networkInterfaceMessageProperty;
				}
				set
				{
					this.networkInterfaceMessageProperty = value;
				}
			}

			// Token: 0x04000297 RID: 663
			private Uri remoteEndpontAddress;

			// Token: 0x04000298 RID: 664
			private NetworkInterfaceMessageProperty networkInterfaceMessageProperty;
		}
	}
}
