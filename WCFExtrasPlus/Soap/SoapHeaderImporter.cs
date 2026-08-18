using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000003 RID: 3
	public class SoapHeaderImporter : IWsdlImportExtension
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002106 File Offset: 0x00000306
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00000308
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
			Dictionary<string, MessageHeaderDescription> dictionary = new Dictionary<string, MessageHeaderDescription>();
			foreach (OperationDescription operationDescription in context.Contract.Operations)
			{
				Dictionary<string, SoapHeaderDirection> dictionary2 = new Dictionary<string, SoapHeaderDirection>();
				List<MessageHeaderDescription> list = new List<MessageHeaderDescription>();
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
					{
						SoapHeaderDirection soapHeaderDirection = this.MessageDirectionToSoapHeaderDirection(messageDescription.Direction);
						SoapHeaderDirection soapHeaderDirection2;
						if (dictionary2.TryGetValue(messageHeaderDescription.Name, out soapHeaderDirection2))
						{
							dictionary2[messageHeaderDescription.Name] = (soapHeaderDirection2 | soapHeaderDirection);
						}
						else
						{
							list.Add(messageHeaderDescription);
							dictionary2[messageHeaderDescription.Name] = soapHeaderDirection;
						}
					}
					messageDescription.Headers.Clear();
				}
				Dictionary<MessageHeaderDescription, SoapHeaderDirection> dictionary3 = new Dictionary<MessageHeaderDescription, SoapHeaderDirection>();
				foreach (MessageHeaderDescription messageHeaderDescription2 in list)
				{
					dictionary3[messageHeaderDescription2] = dictionary2[messageHeaderDescription2.Name];
					dictionary[messageHeaderDescription2.Name] = messageHeaderDescription2;
				}
				operationDescription.Behaviors.Add(new SoapHeaderOpExtension(dictionary3));
			}
			if (dictionary.Count > 0)
			{
				context.Contract.Behaviors.Add(new SoapHeaderSvcExtension(dictionary));
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000230C File Offset: 0x0000050C
		private SoapHeaderDirection MessageDirectionToSoapHeaderDirection(MessageDirection messageDirection)
		{
			switch (messageDirection)
			{
			case MessageDirection.Input:
				return SoapHeaderDirection.In;
			case MessageDirection.Output:
				return SoapHeaderDirection.Out;
			default:
				return SoapHeaderDirection.In;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002330 File Offset: 0x00000530
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
		}
	}
}
