using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtras.Soap
{
	// Token: 0x0200000F RID: 15
	internal class SoapHeaderSvcExtension : IContractBehavior, IServiceContractGenerationExtension
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003554 File Offset: 0x00001754
		public SoapHeaderSvcExtension(Dictionary<string, MessageHeaderDescription> headers)
		{
			this.clientHeaders = headers;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003566 File Offset: 0x00001766
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003569 File Offset: 0x00001769
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000356C File Offset: 0x0000176C
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000356F File Offset: 0x0000176F
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003572 File Offset: 0x00001772
		void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
		{
			context.ContractType.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SoapHeadersAttribute))));
		}

		// Token: 0x0400000F RID: 15
		private Dictionary<string, MessageHeaderDescription> clientHeaders;
	}
}
