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
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000019 RID: 25
	public class XmlCommentsImporter : IServiceBehavior, IWsdlImportExtension
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004652 File Offset: 0x00002852
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
			this.wsdlDocuments = wsdlDocuments;
			this.xmlSchemas = xmlSchemas;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000466C File Offset: 0x0000286C
		internal static void AddXmlComment(CodeTypeMember member, string documentation, ImportOptions options)
		{
			IEnumerable<string> source = XmlCommentsUtils.ParseAndReformatComment(documentation, options.Format, options.WrapLongLines);
			CodeCommentStatement[] value = (from s in source
			select new CodeCommentStatement(s, true)).ToArray<CodeCommentStatement>();
			member.Comments.AddRange(value);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000046C1 File Offset: 0x000028C1
		private static string GetDocumentation(DocumentableItem item)
		{
			if (item.DocumentationElement != null)
			{
				return item.DocumentationElement.InnerText;
			}
			return item.Documentation;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000046E0 File Offset: 0x000028E0
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

		// Token: 0x06000087 RID: 135 RVA: 0x00004798 File Offset: 0x00002998
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000479A File Offset: 0x0000299A
		void IServiceBehavior.AddBindingParameters(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000479C File Offset: 0x0000299C
		void IServiceBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000479E File Offset: 0x0000299E
		void IServiceBehavior.Validate(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x04000023 RID: 35
		internal static ImportOptions options = new ImportOptions();

		// Token: 0x04000024 RID: 36
		internal ServiceDescriptionCollection wsdlDocuments;

		// Token: 0x04000025 RID: 37
		internal XmlSchemaSet xmlSchemas;
	}
}
