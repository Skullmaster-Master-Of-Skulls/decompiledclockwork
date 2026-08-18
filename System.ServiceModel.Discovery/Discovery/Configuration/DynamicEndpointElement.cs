using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B7 RID: 183
	public sealed class DynamicEndpointElement : StandardEndpointElement
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00012F0F File Offset: 0x0001110F
		[ConfigurationProperty("discoveryClientSettings")]
		public DiscoveryClientSettingsElement DiscoveryClientSettings
		{
			get
			{
				return (DiscoveryClientSettingsElement)base["discoveryClientSettings"];
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00012F21 File Offset: 0x00011121
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(DynamicEndpoint);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00012F30 File Offset: 0x00011130
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("discoveryClientSettings", typeof(DiscoveryClientSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00012FAC File Offset: 0x000111AC
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new DynamicEndpoint(contractDescription);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00012FB4 File Offset: 0x000111B4
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			if (string.IsNullOrEmpty(channelEndpointElement.Contract))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigContractNotSpecified(channelEndpointElement.Kind)));
			}
			if (channelEndpointElement.Address != null && !channelEndpointElement.Address.Equals(DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryEndpointAddressIncorrect("address", channelEndpointElement.Address, DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri)));
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00013038 File Offset: 0x00011238
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigDynamicEndpointInService(serviceEndpointElement.Kind)));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000030E1 File Offset: 0x000012E1
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00013054 File Offset: 0x00011254
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
			DynamicEndpoint dynamicEndpoint = (DynamicEndpoint)endpoint;
			if (!dynamicEndpoint.ValidateAndInsertDiscoveryClientBindingElement(dynamicEndpoint.Binding))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryClientBindingElementPresentInDynamicEndpoint));
			}
			if (this.DiscoveryClientSettings.ElementInformation.Properties["endpoint"].ValueOrigin == PropertyValueOrigin.Default)
			{
				dynamicEndpoint.DiscoveryEndpointProvider = new ConfigurationDiscoveryEndpointProvider();
			}
			else
			{
				dynamicEndpoint.DiscoveryEndpointProvider = new ConfigurationDiscoveryEndpointProvider(this.DiscoveryClientSettings.DiscoveryEndpoint);
			}
			this.DiscoveryClientSettings.FindCriteria.ApplyConfiguration(dynamicEndpoint.FindCriteria);
			if (dynamicEndpoint.FindCriteria.ContractTypeNames.Count == 0)
			{
				dynamicEndpoint.FindCriteria.ContractTypeNames.Add(new XmlQualifiedName(dynamicEndpoint.Contract.Name, dynamicEndpoint.Contract.Namespace));
			}
		}

		// Token: 0x040001CB RID: 459
		private ConfigurationPropertyCollection properties;
	}
}
