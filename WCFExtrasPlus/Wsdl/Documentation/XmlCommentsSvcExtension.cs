using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x0200001A RID: 26
	public class XmlCommentsSvcExtension : IContractBehavior, IServiceContractGenerationExtension
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000047B4 File Offset: 0x000029B4
		void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047B6 File Offset: 0x000029B6
		void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000047B8 File Offset: 0x000029B8
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000047BA File Offset: 0x000029BA
		void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000047BC File Offset: 0x000029BC
		public XmlCommentsSvcExtension(XmlCommentsImporter importer, string documentation)
		{
			this.documentation = documentation;
			this.importer = importer;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000047D2 File Offset: 0x000029D2
		void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
		{
			this.ReadConfiguration(context.ServiceContractGenerator.Configuration);
			if (!string.IsNullOrEmpty(this.documentation))
			{
				XmlCommentsImporter.AddXmlComment(context.ContractType, this.documentation, XmlCommentsImporter.options);
			}
			this.AddXmlCommentsToDataContracts(context);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004810 File Offset: 0x00002A10
		private void AddXmlCommentsToDataContracts(ServiceContractGenerationContext context)
		{
			Dictionary<string, CodeTypeMember> dictionary = CodeDomUtils.EnumerateCodeMembers(context.ServiceContractGenerator.TargetCompileUnit);
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

		// Token: 0x06000095 RID: 149 RVA: 0x000048C4 File Offset: 0x00002AC4
		protected virtual void PostProcessCodeMembers(ServiceContractGenerationContext context, IEnumerable<CodeTypeMember> members)
		{
			if (XmlCommentsImporter.options.Documentable)
			{
				context.ServiceContractGenerator.Options = ServiceContractGenerationOptions.None;
				this.RemoveIExtensibleDataObjectFromDeclaration(members);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000048E8 File Offset: 0x00002AE8
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

		// Token: 0x06000097 RID: 151 RVA: 0x00004940 File Offset: 0x00002B40
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

		// Token: 0x06000098 RID: 152 RVA: 0x000049B0 File Offset: 0x00002BB0
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

		// Token: 0x04000027 RID: 39
		private string documentation;

		// Token: 0x04000028 RID: 40
		private XmlCommentsImporter importer;
	}
}
