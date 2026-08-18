using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003CB RID: 971
	[__DynamicallyInvokable]
	public interface IOperationBehavior
	{
		// Token: 0x06002485 RID: 9349
		[__DynamicallyInvokable]
		void Validate(OperationDescription operationDescription);

		// Token: 0x06002486 RID: 9350
		[__DynamicallyInvokable]
		void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation);

		// Token: 0x06002487 RID: 9351
		[__DynamicallyInvokable]
		void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation);

		// Token: 0x06002488 RID: 9352
		[__DynamicallyInvokable]
		void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters);
	}
}
