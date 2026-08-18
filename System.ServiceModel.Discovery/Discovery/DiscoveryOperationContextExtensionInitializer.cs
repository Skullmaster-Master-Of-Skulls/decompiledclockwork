using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000025 RID: 37
	internal class DiscoveryOperationContextExtensionInitializer : IEndpointBehavior, IDispatchMessageInspector
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00006A6F File Offset: 0x00004C6F
		public DiscoveryOperationContextExtensionInitializer(DiscoveryOperationContextExtension discoveryOperationContextExtension)
		{
			this.discoveryOperationContextExtension = discoveryOperationContextExtension;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006A7E File Offset: 0x00004C7E
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDispatcher");
			}
			endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006AA4 File Offset: 0x00004CA4
		object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			OperationContext.Current.Extensions.Add(this.discoveryOperationContextExtension);
			return null;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x04000075 RID: 117
		private DiscoveryOperationContextExtension discoveryOperationContextExtension;
	}
}
