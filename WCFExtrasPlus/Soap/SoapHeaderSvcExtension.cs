using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000005 RID: 5
	internal class SoapHeaderSvcExtension : IContractBehavior, IServiceContractGenerationExtension
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002470 File Offset: 0x00000670
		public SoapHeaderSvcExtension(Dictionary<string, MessageHeaderDescription> headers)
		{
			this.clientHeaders = headers;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000247F File Offset: 0x0000067F
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002481 File Offset: 0x00000681
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002483 File Offset: 0x00000683
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002485 File Offset: 0x00000685
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002487 File Offset: 0x00000687
		void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
		{
			context.ContractType.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SoapHeadersAttribute))));
		}

		// Token: 0x04000002 RID: 2
		private Dictionary<string, MessageHeaderDescription> clientHeaders;
	}
}
