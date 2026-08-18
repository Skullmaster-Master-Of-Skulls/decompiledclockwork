using System;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Interface)]
	public class SoapHeadersAttribute : Attribute, IContractBehavior, IWsdlExportExtension
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002692 File Offset: 0x00000892
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002694 File Offset: 0x00000894
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			SoapHeadersClientHook.Hook(contractDescription, endpoint, clientRuntime);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000269E File Offset: 0x0000089E
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A0 File Offset: 0x000008A0
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026A2 File Offset: 0x000008A2
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			SoapHeadersAttribute.Export(exporter, context);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026AB File Offset: 0x000008AB
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026B0 File Offset: 0x000008B0
		internal static void Export(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			foreach (OperationDescription operationDescription in context.Contract.Operations)
			{
				MethodInfo methodInfo = operationDescription.SyncMethod ?? operationDescription.BeginMethod;
				if (methodInfo == null)
				{
					methodInfo = operationDescription.TaskMethod;
				}
				SoapHeaderAttribute[] array = (SoapHeaderAttribute[])methodInfo.GetCustomAttributes(typeof(SoapHeaderAttribute), false);
				if (array.Length > 0)
				{
					foreach (SoapHeaderAttribute soapHeader in array)
					{
						SoapHeadersAttribute.AddSoapHeader(operationDescription, soapHeader);
					}
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002764 File Offset: 0x00000964
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
