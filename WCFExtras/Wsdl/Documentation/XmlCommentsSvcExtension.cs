using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtras.Utils;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000012 RID: 18
	public class XmlCommentsSvcExtension : IContractBehavior, IServiceContractGenerationExtension
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003BB0 File Offset: 0x00001DB0
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003BB3 File Offset: 0x00001DB3
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BB6 File Offset: 0x00001DB6
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003BB9 File Offset: 0x00001DB9
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003BBC File Offset: 0x00001DBC
		public XmlCommentsSvcExtension(XmlCommentsImporter importer, string documentation)
		{
			this.documentation = documentation;
			this.importer = importer;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003BD8 File Offset: 0x00001DD8
		void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
		{
			this.ReadConfiguration(context.ServiceContractGenerator.Configuration);
			if (!string.IsNullOrEmpty(this.documentation))
			{
				XmlCommentsImporter.AddXmlComment(context.ContractType, this.documentation, XmlCommentsImporter.options);
			}
			this.AddXmlCommentsToDataContracts(context);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C28 File Offset: 0x00001E28
		private void AddXmlCommentsToDataContracts(ServiceContractGenerationContext context)
		{
			Dictionary<string, CodeTypeMember> dictionary = CodeDomUtils.EnumerareCodeMembers(context.ServiceContractGenerator.TargetCompileUnit);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			WsdlUtils.EnumerateDocumentedItems(this.importer.wsdlDocuments, dictionary2);
			WsdlUtils.EnumerateDocumentedItems(this.importer.xmlSchemas, dictionary2);
			foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
			{
				CodeTypeMember member;
				if (dictionary.TryGetValue(keyValuePair.Key, out member))
				{
					XmlCommentsImporter.AddXmlComment(member, keyValuePair.Value, XmlCommentsImporter.options);
				}
			}
			this.PostProcessCodeMembers(context, dictionary.Values);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CF0 File Offset: 0x00001EF0
		protected virtual void PostProcessCodeMembers(ServiceContractGenerationContext context, IEnumerable<CodeTypeMember> members)
		{
			if (XmlCommentsImporter.options.Documentable)
			{
				context.ServiceContractGenerator.Options = ServiceContractGenerationOptions.None;
				this.RemoveIExtensibleDataObjectFromDeclaration(members);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D28 File Offset: 0x00001F28
		private void RemoveIExtensibleDataObjectFromDeclaration(IEnumerable<CodeTypeMember> members)
		{
			foreach (CodeTypeMember codeTypeMember in members)
			{
				if (codeTypeMember is CodeTypeDeclaration)
				{
					this.RemoveIExtensibleDataObjectFromDeclaration((CodeTypeDeclaration)codeTypeMember);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D94 File Offset: 0x00001F94
		private void RemoveIExtensibleDataObjectFromDeclaration(CodeTypeDeclaration codeTypeDeclaration)
		{
			int num = codeTypeDeclaration.BaseTypes.IndexOf("System.Runtime.Serialization.IExtensibleDataObject");
			if (num >= 0)
			{
				codeTypeDeclaration.BaseTypes.RemoveAt(num);
				num = codeTypeDeclaration.Members.IndexOf("ExtensionData");
				if (num >= 0)
				{
					codeTypeDeclaration.Members.RemoveAt(num);
				}
				num = codeTypeDeclaration.Members.IndexOf("extensionDataField");
				if (num >= 0)
				{
					codeTypeDeclaration.Members.RemoveAt(num);
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E18 File Offset: 0x00002018
		private void ReadConfiguration(Configuration configuration)
		{
			XmlCommentsConfig configuration2 = XmlCommentsConfig.GetConfiguration(configuration);
			if (configuration2 != null)
			{
				XmlCommentsImporter.options.Documentable = configuration2.Documentable;
				XmlCommentsImporter.options.Format = configuration2.Format;
				XmlCommentsImporter.options.WrapLongLines = configuration2.WrapLongLines;
			}
		}

		// Token: 0x04000014 RID: 20
		private string documentation;

		// Token: 0x04000015 RID: 21
		private XmlCommentsImporter importer;
	}
}
