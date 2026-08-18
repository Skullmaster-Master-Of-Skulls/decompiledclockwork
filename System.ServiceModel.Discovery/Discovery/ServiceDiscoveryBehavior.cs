using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004F RID: 79
	public class ServiceDiscoveryBehavior : IServiceBehavior
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public ServiceDiscoveryBehavior()
		{
			this.announcementEndpoints = new NonNullItemCollection<AnnouncementEndpoint>();
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000C3D7 File Offset: 0x0000A5D7
		public Collection<AnnouncementEndpoint> AnnouncementEndpoints
		{
			get
			{
				return this.announcementEndpoints;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000030E1 File Offset: 0x000012E1
		void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			if (serviceDescription == null)
			{
				throw FxTrace.Exception.ArgumentNull("serviceDescription");
			}
			if (serviceHostBase == null)
			{
				throw FxTrace.Exception.ArgumentNull("serviceHostBase");
			}
			List<ServiceEndpoint> applicationEndpoints = this.GetApplicationEndpoints(serviceDescription);
			DiscoveryServiceExtension discoveryServiceExtension = serviceHostBase.Extensions.Find<DiscoveryServiceExtension>();
			if (discoveryServiceExtension == null)
			{
				if (serviceDescription.Endpoints.Count > applicationEndpoints.Count)
				{
					discoveryServiceExtension = new DefaultDiscoveryServiceExtension(2056);
				}
				else
				{
					discoveryServiceExtension = new DefaultDiscoveryServiceExtension(0);
				}
				serviceHostBase.Extensions.Add(discoveryServiceExtension);
			}
			for (int i = 0; i < applicationEndpoints.Count; i++)
			{
				applicationEndpoints[i].Behaviors.Add(new ServiceDiscoveryBehavior.EndpointDiscoveryMetadataInitializer(discoveryServiceExtension.InternalPublishedEndpoints));
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C48C File Offset: 0x0000A68C
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			if (serviceDescription == null)
			{
				throw FxTrace.Exception.ArgumentNull("serviceDescription");
			}
			if (serviceHostBase == null)
			{
				throw FxTrace.Exception.ArgumentNull("serviceHostBase");
			}
			DiscoveryServiceExtension discoveryServiceExtension = serviceHostBase.Extensions.Find<DiscoveryServiceExtension>();
			if (discoveryServiceExtension != null)
			{
				DiscoveryService discoveryService = discoveryServiceExtension.ValidateAndGetDiscoveryService();
				ServiceDiscoveryBehavior.SetDiscoveryImplementation(serviceHostBase, discoveryService);
				if (this.announcementEndpoints.Count > 0)
				{
					serviceHostBase.ChannelDispatchers.Add(new OnlineAnnouncementChannelDispatcher(serviceHostBase, this.announcementEndpoints, discoveryServiceExtension.InternalPublishedEndpoints, discoveryService.MessageSequenceGenerator));
					serviceHostBase.ChannelDispatchers.Insert(0, new OfflineAnnouncementChannelDispatcher(serviceHostBase, this.announcementEndpoints, discoveryServiceExtension.InternalPublishedEndpoints, discoveryService.MessageSequenceGenerator));
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C534 File Offset: 0x0000A734
		private static void SetDiscoveryImplementation(ServiceHostBase host, DiscoveryService discoveryService)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in host.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						if (endpointDispatcher != null && EndpointDiscoveryMetadata.IsDiscoverySystemEndpoint(endpointDispatcher))
						{
							ServiceDiscoveryBehavior.SetDiscoveryImplementation(endpointDispatcher, discoveryService);
						}
					}
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		private static void SetDiscoveryImplementation(EndpointDispatcher endpointDispatcher, DiscoveryService discoveryService)
		{
			DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
			dispatchRuntime.SynchronizationContext = null;
			dispatchRuntime.ConcurrencyMode = ConcurrencyMode.Multiple;
			ServiceDiscoveryInstanceContextProvider serviceDiscoveryInstanceContextProvider = new ServiceDiscoveryInstanceContextProvider(discoveryService);
			dispatchRuntime.InstanceContextProvider = serviceDiscoveryInstanceContextProvider;
			dispatchRuntime.InstanceProvider = serviceDiscoveryInstanceContextProvider;
			dispatchRuntime.Type = discoveryService.GetType();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C614 File Offset: 0x0000A814
		private List<ServiceEndpoint> GetApplicationEndpoints(ServiceDescription serviceDescription)
		{
			List<ServiceEndpoint> list = new List<ServiceEndpoint>(serviceDescription.Endpoints.Count);
			foreach (ServiceEndpoint serviceEndpoint in serviceDescription.Endpoints)
			{
				if (!EndpointDiscoveryMetadata.IsDiscoverySystemEndpoint(serviceEndpoint))
				{
					list.Add(serviceEndpoint);
				}
			}
			return list;
		}

		// Token: 0x04000100 RID: 256
		private NonNullItemCollection<AnnouncementEndpoint> announcementEndpoints;

		// Token: 0x020000EC RID: 236
		private class EndpointDiscoveryMetadataInitializer : IEndpointBehavior
		{
			// Token: 0x06000845 RID: 2117 RVA: 0x0001554A File Offset: 0x0001374A
			internal EndpointDiscoveryMetadataInitializer(Collection<EndpointDiscoveryMetadata> publishedEndpointCollection)
			{
				this.publishedEndpointCollection = publishedEndpointCollection;
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x000030E1 File Offset: 0x000012E1
			void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
			{
			}

			// Token: 0x06000847 RID: 2119 RVA: 0x000030E1 File Offset: 0x000012E1
			void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
			{
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x0001555C File Offset: 0x0001375C
			void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
			{
				EndpointDiscoveryMetadata endpointDiscoveryMetadata = EndpointDiscoveryMetadata.FromServiceEndpoint(endpoint, endpointDispatcher);
				if (endpointDiscoveryMetadata != null)
				{
					this.publishedEndpointCollection.Add(endpointDiscoveryMetadata);
				}
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x000030E1 File Offset: 0x000012E1
			void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
			{
			}

			// Token: 0x04000296 RID: 662
			private Collection<EndpointDiscoveryMetadata> publishedEndpointCollection;
		}
	}
}
