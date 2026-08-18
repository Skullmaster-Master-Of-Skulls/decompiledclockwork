using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000AD RID: 173
	internal class ConfigurationUtility
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x000125F8 File Offset: 0x000107F8
		public static ChannelEndpointElement GetDefaultDiscoveryEndpointElement()
		{
			return new ChannelEndpointElement
			{
				Kind = "udpDiscoveryEndpoint"
			};
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001260A File Offset: 0x0001080A
		public static T LookupEndpoint<T>(ChannelEndpointElement channelEndpointElement) where T : ServiceEndpoint
		{
			return ConfigLoader.LookupEndpoint(channelEndpointElement, null) as T;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00012620 File Offset: 0x00010820
		internal static void InitializeAndValidateUdpChannelEndpointElement(ChannelEndpointElement channelEndpointElement)
		{
			if (!(channelEndpointElement.Address == null) && !string.IsNullOrEmpty(channelEndpointElement.Address.ToString()))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigAddressSpecifiedForUdpDiscoveryEndpoint(channelEndpointElement.Kind)));
			}
			channelEndpointElement.Address = null;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00012670 File Offset: 0x00010870
		internal static void InitializeAndValidateUdpServiceEndpointElement(ServiceEndpointElement serviceEndpointElement)
		{
			if (!(serviceEndpointElement.Address == null) && !string.IsNullOrEmpty(serviceEndpointElement.Address.ToString()))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigAddressSpecifiedForUdpDiscoveryEndpoint(serviceEndpointElement.Kind)));
			}
			serviceEndpointElement.Address = null;
			if (serviceEndpointElement.ListenUri != null)
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigListenUriSpecifiedForUdpDiscoveryEndpoint(serviceEndpointElement.Kind)));
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000126E8 File Offset: 0x000108E8
		internal static TEndpoint LookupEndpointFromClientSection<TEndpoint>(string endpointConfigurationName) where TEndpoint : ServiceEndpoint
		{
			TEndpoint tendpoint = default(TEndpoint);
			bool flag = string.Equals(endpointConfigurationName, "*", StringComparison.Ordinal);
			ClientSection section = ClientSection.GetSection();
			foreach (object obj in section.Endpoints)
			{
				ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
				if (!string.IsNullOrEmpty(channelEndpointElement.Kind) && (endpointConfigurationName == channelEndpointElement.Name || flag))
				{
					TEndpoint tendpoint2 = ConfigurationUtility.LookupEndpoint<TEndpoint>(channelEndpointElement);
					if (tendpoint2 != null)
					{
						if (tendpoint != null)
						{
							if (flag)
							{
								throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigMultipleEndpointsMatchWildcard(typeof(TEndpoint).FullName, section.SectionInformation.SectionName)));
							}
							throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigMultipleEndpointsMatch(typeof(TEndpoint).FullName, endpointConfigurationName, section.SectionInformation.SectionName)));
						}
						else
						{
							tendpoint = tendpoint2;
						}
					}
				}
			}
			if (tendpoint != null)
			{
				return tendpoint;
			}
			if (flag)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigNoEndpointsMatchWildcard(typeof(TEndpoint).FullName, section.SectionInformation.SectionName)));
			}
			throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigNoEndpointsMatch(typeof(TEndpoint).FullName, endpointConfigurationName, section.SectionInformation.SectionName)));
		}
	}
}
