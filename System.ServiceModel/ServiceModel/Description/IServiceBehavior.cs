using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x020003CC RID: 972
	public interface IServiceBehavior
	{
		// Token: 0x06002489 RID: 9353
		void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase);

		// Token: 0x0600248A RID: 9354
		void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters);

		// Token: 0x0600248B RID: 9355
		void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase);
	}
}
