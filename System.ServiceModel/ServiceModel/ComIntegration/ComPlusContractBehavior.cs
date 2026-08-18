using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E5 RID: 485
	internal class ComPlusContractBehavior : IContractBehavior
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x00038218 File Offset: 0x00036418
		public ComPlusContractBehavior(ServiceInfo info)
		{
			this.info = info;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00038227 File Offset: 0x00036427
		public void Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00038229 File Offset: 0x00036429
		public void AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003822C File Offset: 0x0003642C
		public void ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
		{
			dispatch.InstanceProvider = new ComPlusInstanceProvider(this.info);
			dispatch.InstanceContextInitializers.Add(new ComPlusInstanceContextInitializer(this.info));
			foreach (DispatchOperation dispatchOperation in dispatch.Operations)
			{
				dispatchOperation.CallContextInitializers.Add(new ComPlusThreadInitializer(description, dispatchOperation, this.info));
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000382B4 File Offset: 0x000364B4
		public void ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
		}

		// Token: 0x040017DB RID: 6107
		private ServiceInfo info;
	}
}
