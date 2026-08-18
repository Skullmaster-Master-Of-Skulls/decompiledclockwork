using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x02000407 RID: 1031
	public interface IWsdlImportExtension
	{
		// Token: 0x0600273D RID: 10045
		void BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy);

		// Token: 0x0600273E RID: 10046
		void ImportContract(WsdlImporter importer, WsdlContractConversionContext context);

		// Token: 0x0600273F RID: 10047
		void ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context);
	}
}
