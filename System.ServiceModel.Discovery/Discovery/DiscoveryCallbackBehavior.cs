using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000011 RID: 17
	internal class DiscoveryCallbackBehavior : IEndpointBehavior
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003A22 File Offset: 0x00001C22
		public DiscoveryCallbackBehavior()
		{
			this.innerCallbackBehavior = new CallbackBehaviorAttribute();
			this.innerCallbackBehavior.ConcurrencyMode = ConcurrencyMode.Multiple;
			this.innerCallbackBehavior.UseSynchronizationContext = false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A4D File Offset: 0x00001C4D
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
			((IEndpointBehavior)this.innerCallbackBehavior).AddBindingParameters(endpoint, bindingParameters);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A5C File Offset: 0x00001C5C
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			((IEndpointBehavior)this.innerCallbackBehavior).ApplyClientBehavior(endpoint, clientRuntime);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003A6B File Offset: 0x00001C6B
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
			((IEndpointBehavior)this.innerCallbackBehavior).Validate(endpoint);
		}

		// Token: 0x04000036 RID: 54
		private CallbackBehaviorAttribute innerCallbackBehavior;
	}
}
