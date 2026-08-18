using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F8 RID: 1016
	public class SynchronousReceiveBehavior : IEndpointBehavior
	{
		// Token: 0x06002683 RID: 9859 RVA: 0x0008AB64 File Offset: 0x00088D64
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0008AB66 File Offset: 0x00088D66
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0008AB68 File Offset: 0x00088D68
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointDispatcher");
			}
			endpointDispatcher.ChannelDispatcher.ReceiveSynchronously = true;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008AB89 File Offset: 0x00088D89
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}
	}
}
