using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using WCFExtras.Utils;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000011 RID: 17
	public class XmlCommentsImporter : IServiceBehavior, IWsdlImportExtension
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003A04 File Offset: 0x00001C04
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
			this.wsdlDocuments = wsdlDocuments;
			this.xmlSchemas = xmlSchemas;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003A30 File Offset: 0x00001C30
		internal static void AddXmlComment(CodeTypeMember member, string documentation, ImportOptions options)
		{
			IEnumerable<string> enumerable = XmlCommentsUtils.ParseAndReformatComment(documentation, options.Format, options.WrapLongLines);
			CodeCommentStatement[] value = Enumerable.Select<string, CodeCommentStatement>(enumerable, (string s) => new CodeCommentStatement(s, true)).ToArray<CodeCommentStatement>();
			member.Comments.AddRange(value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003A8C File Offset: 0x00001C8C
		private static string GetDocumentation(DocumentableItem item)
		{
			string result;
			if (item.DocumentationElement != null)
			{
				result = item.DocumentationElement.InnerText;
			}
			else
			{
				result = item.Documentation;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003AC0 File Offset: 0x00001CC0
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
			string documentation = XmlCommentsImporter.GetDocumentation(context.WsdlPortType);
			context.Contract.Behaviors.Add(new XmlCommentsSvcExtension(this, documentation));
			foreach (object obj in context.WsdlPortType.Operations)
			{
				Operation operation = (Operation)obj;
				documentation = XmlCommentsImporter.GetDocumentation(operation);
				if (!string.IsNullOrEmpty(documentation))
				{
					OperationDescription operationDescription = context.Contract.Operations.Find(operation.Name);
					operationDescription.Behaviors.Add(new XmlCommentsOpExtension(this, documentation));
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B90 File Offset: 0x00001D90
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B93 File Offset: 0x00001D93
		void IServiceBehavior.AddBindingParameters(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003B96 File Offset: 0x00001D96
		void IServiceBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003B99 File Offset: 0x00001D99
		void IServiceBehavior.Validate(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x04000010 RID: 16
		internal static ImportOptions options = new ImportOptions();

		// Token: 0x04000011 RID: 17
		internal ServiceDescriptionCollection wsdlDocuments;

		// Token: 0x04000012 RID: 18
		internal XmlSchemaSet xmlSchemas;
	}
}
