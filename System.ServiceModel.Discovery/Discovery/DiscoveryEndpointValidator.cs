using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200001F RID: 31
	internal class DiscoveryEndpointValidator : IEndpointBehavior
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000635C File Offset: 0x0000455C
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpoint");
			}
			if (endpointDispatcher == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDispatcher");
			}
			if (endpoint.IsSystemEndpoint && endpointDispatcher.ChannelDispatcher.Host.Description.Behaviors.Find<ServiceDiscoveryBehavior>() == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryEndpointWithoutBehavior(endpoint.Name)));
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}
	}
}
