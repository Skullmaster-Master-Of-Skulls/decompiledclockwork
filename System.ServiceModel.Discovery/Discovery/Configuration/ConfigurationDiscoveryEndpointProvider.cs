using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B0 RID: 176
	internal class ConfigurationDiscoveryEndpointProvider : DiscoveryEndpointProvider
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x00012969 File Offset: 0x00010B69
		public ConfigurationDiscoveryEndpointProvider()
		{
			this.channelEndpointElement = ConfigurationUtility.GetDefaultDiscoveryEndpointElement();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001297C File Offset: 0x00010B7C
		public ConfigurationDiscoveryEndpointProvider(ChannelEndpointElement channelEndpointElement)
		{
			ConfigurationDiscoveryEndpointProvider.ValidateAndGetDiscoveryEndpoint(channelEndpointElement);
			this.channelEndpointElement = channelEndpointElement;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00012992 File Offset: 0x00010B92
		public override DiscoveryEndpoint GetDiscoveryEndpoint()
		{
			return ConfigurationDiscoveryEndpointProvider.ValidateAndGetDiscoveryEndpoint(this.channelEndpointElement);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000129A0 File Offset: 0x00010BA0
		private static DiscoveryEndpoint ValidateAndGetDiscoveryEndpoint(ChannelEndpointElement channelEndpointElement)
		{
			if (string.IsNullOrEmpty(channelEndpointElement.Kind))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigDiscoveryEndpointMissingKind(typeof(DiscoveryEndpoint).FullName)));
			}
			ServiceEndpoint serviceEndpoint = ConfigLoader.LookupEndpoint(channelEndpointElement, null);
			if (serviceEndpoint == null)
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigInvalidEndpointConfiguration(channelEndpointElement.Kind)));
			}
			DiscoveryEndpoint discoveryEndpoint = serviceEndpoint as DiscoveryEndpoint;
			if (discoveryEndpoint == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigInvalidDiscoveryEndpoint(typeof(DiscoveryEndpoint).FullName, channelEndpointElement.Kind, serviceEndpoint.GetType().FullName)));
			}
			return discoveryEndpoint;
		}

		// Token: 0x040001C7 RID: 455
		private readonly ChannelEndpointElement channelEndpointElement;
	}
}
