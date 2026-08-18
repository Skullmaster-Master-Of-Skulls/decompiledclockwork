using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtras.Soap
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Interface)]
	public class SoapHeadersAttribute : Attribute, IContractBehavior, IWsdlExportExtension
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002138 File Offset: 0x00000338
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000213B File Offset: 0x0000033B
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			SoapHeadersClientHook.Hook(contractDescription, endpoint, clientRuntime);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002147 File Offset: 0x00000347
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000214A File Offset: 0x0000034A
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000214D File Offset: 0x0000034D
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			SoapHeadersAttribute.Export(exporter, context);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002158 File Offset: 0x00000358
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000215C File Offset: 0x0000035C
		internal static void Export(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			foreach (OperationDescription operationDescription in context.Contract.Operations)
			{
				SoapHeaderAttribute[] array = (SoapHeaderAttribute[])operationDescription.SyncMethod.GetCustomAttributes(typeof(SoapHeaderAttribute), false);
				if (array.Length > 0)
				{
					foreach (SoapHeaderAttribute soapHeader in array)
					{
						SoapHeadersAttribute.AddSoapHeader(operationDescription, soapHeader);
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002214 File Offset: 0x00000414
		internal static void AddSoapHeader(OperationDescription op, SoapHeaderAttribute soapHeader)
		{
			string @namespace = SoapHeaderHelper.GetNamespace(soapHeader.Type);
			MessageHeaderDescription messageHeaderDescription = new MessageHeaderDescription(soapHeader.Name, @namespace);
			messageHeaderDescription.Type = soapHeader.Type;
			bool flag = (soapHeader.Direction & SoapHeaderDirection.In) == SoapHeaderDirection.In;
			bool flag2 = (soapHeader.Direction & SoapHeaderDirection.Out) == SoapHeaderDirection.Out;
			foreach (MessageDescription messageDescription in op.Messages)
			{
				if ((messageDescription.Direction == MessageDirection.Input && flag) || (messageDescription.Direction == MessageDirection.Output && flag2))
				{
					messageDescription.Headers.Add(messageHeaderDescription);
				}
			}
		}
	}
}
