using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace WCFExtras.Soap
{
	// Token: 0x0200000D RID: 13
	public class SoapHeaderImporter : IWsdlImportExtension
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003190 File Offset: 0x00001390
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003194 File Offset: 0x00001394
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
				foreach (MessageHeaderDescription messageHeaderDescription in list)
				{
					dictionary3[messageHeaderDescription] = dictionary2[messageHeaderDescription.Name];
					dictionary[messageHeaderDescription.Name] = messageHeaderDescription;
				}
				operationDescription.Behaviors.Add(new SoapHeaderOpExtension(dictionary3));
			}
			if (dictionary.Count > 0)
			{
				context.Contract.Behaviors.Add(new SoapHeaderSvcExtension(dictionary));
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000033EC File Offset: 0x000015EC
		private SoapHeaderDirection MessageDirectionToSoapHeaderDirection(MessageDirection messageDirection)
		{
			SoapHeaderDirection result;
			switch (messageDirection)
			{
			case MessageDirection.Input:
				result = SoapHeaderDirection.In;
				break;
			case MessageDirection.Output:
				result = SoapHeaderDirection.Out;
				break;
			default:
				result = SoapHeaderDirection.In;
				break;
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003419 File Offset: 0x00001619
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
		}
	}
}
