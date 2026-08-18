using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200065C RID: 1628
	public class ServiceMetadataEndpointElement : StandardEndpointElement
	{
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000EE8F8 File Offset: 0x000ECAF8
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(ServiceMetadataEndpoint);
			}
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000EE904 File Offset: 0x000ECB04
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new ServiceMetadataEndpoint();
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000EE90B File Offset: 0x000ECB0B
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			if (string.IsNullOrEmpty(channelEndpointElement.Binding))
			{
				channelEndpointElement.Binding = "mexHttpBinding";
			}
			channelEndpointElement.Contract = "IMetadataExchange";
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000EE930 File Offset: 0x000ECB30
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			if (string.IsNullOrEmpty(serviceEndpointElement.Binding))
			{
				serviceEndpointElement.Binding = "mexHttpBinding";
			}
			serviceEndpointElement.Contract = "IMetadataExchange";
			serviceEndpointElement.IsSystemEndpoint = true;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000EE95C File Offset: 0x000ECB5C
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000EE95E File Offset: 0x000ECB5E
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
		}
	}
}
