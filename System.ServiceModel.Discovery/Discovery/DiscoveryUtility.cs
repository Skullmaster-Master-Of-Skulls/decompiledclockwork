using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery.Version11;
using System.ServiceModel.Discovery.VersionApril2005;
using System.ServiceModel.Discovery.VersionCD1;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000029 RID: 41
	internal class DiscoveryUtility
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00006F58 File Offset: 0x00005158
		public static Collection<EndpointDiscoveryMetadata> ToEndpointDiscoveryMetadataCollection(Collection<EndpointDiscoveryMetadataApril2005> endpointDiscoveryMetadataApril2005Collection)
		{
			Collection<EndpointDiscoveryMetadata> collection = new Collection<EndpointDiscoveryMetadata>();
			foreach (EndpointDiscoveryMetadataApril2005 endpointDiscoveryMetadataApril in endpointDiscoveryMetadataApril2005Collection)
			{
				collection.Add(endpointDiscoveryMetadataApril.ToEndpointDiscoveryMetadata());
			}
			return collection;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006FAC File Offset: 0x000051AC
		public static Collection<EndpointDiscoveryMetadata> ToEndpointDiscoveryMetadataCollection(Collection<EndpointDiscoveryMetadataCD1> endpointDiscoveryMetadataCD1Collection)
		{
			Collection<EndpointDiscoveryMetadata> collection = new Collection<EndpointDiscoveryMetadata>();
			foreach (EndpointDiscoveryMetadataCD1 endpointDiscoveryMetadataCD in endpointDiscoveryMetadataCD1Collection)
			{
				collection.Add(endpointDiscoveryMetadataCD.ToEndpointDiscoveryMetadata());
			}
			return collection;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007000 File Offset: 0x00005200
		public static Collection<EndpointDiscoveryMetadata> ToEndpointDiscoveryMetadataCollection(Collection<EndpointDiscoveryMetadata11> endpointDiscoveryMetadata11Collection)
		{
			Collection<EndpointDiscoveryMetadata> collection = new Collection<EndpointDiscoveryMetadata>();
			foreach (EndpointDiscoveryMetadata11 endpointDiscoveryMetadata in endpointDiscoveryMetadata11Collection)
			{
				collection.Add(endpointDiscoveryMetadata.ToEndpointDiscoveryMetadata());
			}
			return collection;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007054 File Offset: 0x00005254
		public static ContractDescription GetContract(Type contractType)
		{
			ContractDescription contract = ContractDescription.GetContract(contractType);
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				OperationBehaviorAttribute operationBehaviorAttribute = operationDescription.Behaviors.Find<OperationBehaviorAttribute>();
				if (operationBehaviorAttribute == null)
				{
					operationBehaviorAttribute = new OperationBehaviorAttribute();
					operationDescription.Behaviors.Add(operationBehaviorAttribute);
				}
				operationBehaviorAttribute.PreferAsyncInvocation = true;
			}
			return contract;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000070CC File Offset: 0x000052CC
		public static DiscoveryMessageSequence ToDiscoveryMessageSequenceOrNull(DiscoveryMessageSequenceApril2005 messageSequence)
		{
			if (messageSequence == null)
			{
				return null;
			}
			return messageSequence.ToDiscoveryMessageSequence();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000070D9 File Offset: 0x000052D9
		public static DiscoveryMessageSequence ToDiscoveryMessageSequenceOrNull(DiscoveryMessageSequenceCD1 messageSequence)
		{
			if (messageSequence == null)
			{
				return null;
			}
			return messageSequence.ToDiscoveryMessageSequence();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000070E6 File Offset: 0x000052E6
		public static DiscoveryMessageSequence ToDiscoveryMessageSequenceOrNull(DiscoveryMessageSequence11 messageSequence)
		{
			if (messageSequence == null)
			{
				return null;
			}
			return messageSequence.ToDiscoveryMessageSequence();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000070F3 File Offset: 0x000052F3
		public static bool IsCompatible(OperationContext context, IContextChannel channel)
		{
			return context != null && context.InternalServiceChannel != null && context.InternalServiceChannel.Proxy == channel;
		}
	}
}
