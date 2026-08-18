using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002B RID: 43
	internal class DiscoveryViaBehavior : IEndpointBehavior
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00007365 File Offset: 0x00005565
		public DiscoveryViaBehavior(Uri via)
		{
			if (via == null)
			{
				throw FxTrace.Exception.ArgumentNull("via");
			}
			this.via = via;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000738D File Offset: 0x0000558D
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00007395 File Offset: 0x00005595
		public Uri Via
		{
			get
			{
				return this.via;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.via = value;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000073B7 File Offset: 0x000055B7
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			if (clientRuntime == null)
			{
				throw FxTrace.Exception.ArgumentNull("clientRuntime");
			}
			clientRuntime.Via = this.Via;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000030E1 File Offset: 0x000012E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x04000084 RID: 132
		private Uri via;
	}
}
