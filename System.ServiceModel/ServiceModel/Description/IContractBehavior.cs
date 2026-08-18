using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C9 RID: 969
	[__DynamicallyInvokable]
	public interface IContractBehavior
	{
		// Token: 0x06002480 RID: 9344
		[__DynamicallyInvokable]
		void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint);

		// Token: 0x06002481 RID: 9345
		[__DynamicallyInvokable]
		void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime);

		// Token: 0x06002482 RID: 9346
		[__DynamicallyInvokable]
		void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime);

		// Token: 0x06002483 RID: 9347
		[__DynamicallyInvokable]
		void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters);
	}
}
