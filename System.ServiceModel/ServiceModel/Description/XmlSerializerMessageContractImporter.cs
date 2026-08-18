using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x02000409 RID: 1033
	public class XmlSerializerMessageContractImporter : IWsdlImportExtension
	{
		// Token: 0x06002747 RID: 10055 RVA: 0x00091665 File Offset: 0x0008F865
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext endpointContext)
		{
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endpointContext"));
			}
			MessageContractImporter.ImportMessageBinding(importer, endpointContext, typeof(MessageContractImporter.XmlSerializerSchemaImporter));
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00091690 File Offset: 0x0008F890
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext contractContext)
		{
			if (contractContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contractContext"));
			}
			MessageContractImporter.ImportMessageContract(importer, contractContext, MessageContractImporter.XmlSerializerSchemaImporter.Get(importer));
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000916B7 File Offset: 0x0008F8B7
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}
	}
}
