using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A0D RID: 2573
	internal class PeerOperationSelectorBehavior : IContractBehavior
	{
		// Token: 0x060065DA RID: 26074 RVA: 0x0017B9B0 File Offset: 0x00179BB0
		internal PeerOperationSelectorBehavior(IPeerNodeMessageHandling messageHandler)
		{
			this.messageHandler = messageHandler;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x0017B9BF File Offset: 0x00179BBF
		void IContractBehavior.AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x0017B9C1 File Offset: 0x00179BC1
		void IContractBehavior.Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x0017B9C3 File Offset: 0x00179BC3
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
		{
			dispatch.OperationSelector = new OperationSelector(this.messageHandler);
			if (dispatch.ClientRuntime != null)
			{
				dispatch.ClientRuntime.OperationSelector = new OperationSelectorBehavior.MethodInfoOperationSelector(description, MessageDirection.Output);
			}
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x0017B9F0 File Offset: 0x00179BF0
		void IContractBehavior.ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
			proxy.OperationSelector = new OperationSelectorBehavior.MethodInfoOperationSelector(description, MessageDirection.Input);
			proxy.CallbackDispatchRuntime.OperationSelector = new OperationSelector(this.messageHandler);
		}

		// Token: 0x04003AC2 RID: 15042
		private IPeerNodeMessageHandling messageHandler;
	}
}
