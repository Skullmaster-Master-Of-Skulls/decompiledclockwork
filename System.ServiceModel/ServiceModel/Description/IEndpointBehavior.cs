using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E6 RID: 998
	[__DynamicallyInvokable]
	public interface IEndpointBehavior
	{
		// Token: 0x060025AA RID: 9642
		[__DynamicallyInvokable]
		void Validate(ServiceEndpoint endpoint);

		// Token: 0x060025AB RID: 9643
		[__DynamicallyInvokable]
		void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters);

		// Token: 0x060025AC RID: 9644
		[__DynamicallyInvokable]
		void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher);

		// Token: 0x060025AD RID: 9645
		[__DynamicallyInvokable]
		void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime);
	}
}
