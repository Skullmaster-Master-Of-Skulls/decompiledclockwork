using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x02000408 RID: 1032
	public class DataContractSerializerMessageContractImporter : IWsdlImportExtension
	{
		// Token: 0x06002740 RID: 10048 RVA: 0x000915B7 File Offset: 0x0008F7B7
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext endpointContext)
		{
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endpointContext"));
			}
			if (this.enabled)
			{
				MessageContractImporter.ImportMessageBinding(importer, endpointContext, typeof(MessageContractImporter.DataContractSerializerSchemaImporter));
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000915EA File Offset: 0x0008F7EA
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext contractContext)
		{
			if (contractContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contractContext"));
			}
			if (this.enabled)
			{
				MessageContractImporter.ImportMessageContract(importer, contractContext, MessageContractImporter.DataContractSerializerSchemaImporter.Get(importer));
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x00091619 File Offset: 0x0008F819
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0009161B File Offset: 0x0008F81B
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x00091623 File Offset: 0x0008F823
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x040021E2 RID: 8674
		private bool enabled = true;

		// Token: 0x040021E3 RID: 8675
		internal const string GenericMessageSchemaTypeName = "MessageBody";

		// Token: 0x040021E4 RID: 8676
		internal const string GenericMessageSchemaTypeNamespace = "http://schemas.microsoft.com/Message";

		// Token: 0x040021E5 RID: 8677
		private const string StreamBodySchemaTypeName = "StreamBody";

		// Token: 0x040021E6 RID: 8678
		private const string StreamBodySchemaTypeNamespace = "http://schemas.microsoft.com/Message";

		// Token: 0x040021E7 RID: 8679
		internal static XmlQualifiedName GenericMessageTypeName = new XmlQualifiedName("MessageBody", "http://schemas.microsoft.com/Message");

		// Token: 0x040021E8 RID: 8680
		internal static XmlQualifiedName StreamBodyTypeName = new XmlQualifiedName("StreamBody", "http://schemas.microsoft.com/Message");
	}
}
