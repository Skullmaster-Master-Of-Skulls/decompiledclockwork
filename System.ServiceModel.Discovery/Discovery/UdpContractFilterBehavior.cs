using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000054 RID: 84
	internal class UdpContractFilterBehavior : IEndpointBehavior
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x000030E1 File Offset: 0x000012E1
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C866 File Offset: 0x0000AA66
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			if (clientRuntime != null && clientRuntime.CallbackDispatchRuntime != null && clientRuntime.CallbackDispatchRuntime.UnhandledDispatchOperation != null)
			{
				clientRuntime.CallbackDispatchRuntime.UnhandledDispatchOperation.Invoker = new UdpContractFilterBehavior.UnhandledActionOperationInvoker();
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000C895 File Offset: 0x0000AA95
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDispatcher");
			}
			endpointDispatcher.ContractFilter = new MatchAllMessageFilter();
			endpointDispatcher.DispatchRuntime.UnhandledDispatchOperation.Invoker = new UdpContractFilterBehavior.UnhandledActionOperationInvoker();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000030E1 File Offset: 0x000012E1
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x020000ED RID: 237
		private class UnhandledActionOperationInvoker : IOperationInvoker
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600084A RID: 2122 RVA: 0x0000C68B File Offset: 0x0000A88B
			public bool IsSynchronous
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x00015580 File Offset: 0x00013780
			public object[] AllocateInputs()
			{
				return EmptyArray.Allocate(1);
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x00015588 File Offset: 0x00013788
			public object Invoke(object instance, object[] inputs, out object[] outputs)
			{
				outputs = EmptyArray.Allocate(0);
				return new NullMessage();
			}

			// Token: 0x0600084D RID: 2125 RVA: 0x00015597 File Offset: 0x00013797
			public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
			{
				throw FxTrace.Exception.AsError(new NotImplementedException());
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x00015597 File Offset: 0x00013797
			public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
			{
				throw FxTrace.Exception.AsError(new NotImplementedException());
			}
		}
	}
}
