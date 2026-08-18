using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042C RID: 1068
	internal class XmlSerializerOperationGenerator : IOperationBehavior, IOperationContractGenerationExtension
	{
		// Token: 0x06002996 RID: 10646 RVA: 0x0009FB9C File Offset: 0x0009DD9C
		internal XmlSerializerOperationGenerator(XmlSerializerImportOptions options)
		{
			this.operationGenerator = new OperationGenerator();
			this.options = options;
			this.codeNamespace = XmlSerializerOperationGenerator.GetTargetCodeNamespace(options);
			this.partInfoTable = new Dictionary<MessagePartDescription, XmlSerializerOperationGenerator.PartInfo>();
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0009FBD8 File Offset: 0x0009DDD8
		private static CodeNamespace GetTargetCodeNamespace(XmlSerializerImportOptions options)
		{
			CodeNamespace codeNamespace = null;
			string text = options.ClrNamespace ?? string.Empty;
			foreach (object obj in options.CodeCompileUnit.Namespaces)
			{
				CodeNamespace codeNamespace2 = (CodeNamespace)obj;
				if (codeNamespace2.Name == text)
				{
					codeNamespace = codeNamespace2;
				}
			}
			if (codeNamespace == null)
			{
				codeNamespace = new CodeNamespace(text);
				options.CodeCompileUnit.Namespaces.Add(codeNamespace);
			}
			return codeNamespace;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0009FC74 File Offset: 0x0009DE74
		internal void Add(MessagePartDescription part, XmlMemberMapping memberMapping, XmlMembersMapping membersMapping, bool isEncoded)
		{
			XmlSerializerOperationGenerator.PartInfo partInfo = new XmlSerializerOperationGenerator.PartInfo();
			partInfo.MemberMapping = memberMapping;
			partInfo.MembersMapping = membersMapping;
			partInfo.IsEncoded = isEncoded;
			this.partInfoTable[part] = partInfo;
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x0009FCAC File Offset: 0x0009DEAC
		public XmlCodeExporter XmlExporter
		{
			get
			{
				if (this.xmlExporter == null)
				{
					this.xmlExporter = new XmlCodeExporter(this.codeNamespace, this.options.CodeCompileUnit, this.options.CodeProvider, this.options.WebReferenceOptions.CodeGenerationOptions, null);
				}
				return this.xmlExporter;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x0009FD00 File Offset: 0x0009DF00
		public SoapCodeExporter SoapExporter
		{
			get
			{
				if (this.soapExporter == null)
				{
					this.soapExporter = new SoapCodeExporter(this.codeNamespace, this.options.CodeCompileUnit, this.options.CodeProvider, this.options.WebReferenceOptions.CodeGenerationOptions, null);
				}
				return this.soapExporter;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x0009FD53 File Offset: 0x0009DF53
		private OperationGenerator OperationGenerator
		{
			get
			{
				return this.operationGenerator;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x0009FD5B File Offset: 0x0009DF5B
		internal Dictionary<OperationDescription, XmlSerializerFormatAttribute> OperationAttributes
		{
			get
			{
				return this.operationAttributes;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0009FD63 File Offset: 0x0009DF63
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0009FD65 File Offset: 0x0009DF65
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0009FD67 File Offset: 0x0009DF67
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0009FD69 File Offset: 0x0009DF69
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0009FD6C File Offset: 0x0009DF6C
		void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (this.partInfoTable != null && this.partInfoTable.Count > 0)
			{
				Dictionary<XmlMembersMapping, XmlMembersMapping> alreadyExported = new Dictionary<XmlMembersMapping, XmlMembersMapping>();
				foreach (MessageDescription messageDescription in context.Operation.Messages)
				{
					foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
					{
						this.GeneratePartType(alreadyExported, messageHeaderDescription, messageHeaderDescription.Namespace);
					}
					MessageBodyDescription body = messageDescription.Body;
					bool flag = body.WrapperName != null;
					if (OperationFormatter.IsValidReturnValue(body.ReturnValue))
					{
						this.GeneratePartType(alreadyExported, body.ReturnValue, flag ? body.WrapperNamespace : body.ReturnValue.Namespace);
					}
					foreach (MessagePartDescription messagePartDescription in body.Parts)
					{
						this.GeneratePartType(alreadyExported, messagePartDescription, flag ? body.WrapperNamespace : messagePartDescription.Namespace);
					}
				}
			}
			XmlSerializerOperationBehavior xmlSerializerOperationBehavior = context.Operation.Behaviors.Find<XmlSerializerOperationBehavior>();
			if (xmlSerializerOperationBehavior == null)
			{
				return;
			}
			XmlSerializerFormatAttribute xmlSerializerFormatAttribute = (xmlSerializerOperationBehavior == null) ? new XmlSerializerFormatAttribute() : xmlSerializerOperationBehavior.XmlSerializerFormatAttribute;
			OperationFormatStyle style = xmlSerializerFormatAttribute.Style;
			this.operationGenerator.GenerateOperation(context, ref style, xmlSerializerFormatAttribute.IsEncoded, new XmlSerializerOperationGenerator.WrappedBodyTypeGenerator(context), new Dictionary<MessagePartDescription, ICollection<CodeTypeReference>>());
			context.ServiceContractGenerator.AddReferencedAssembly(typeof(XmlTypeAttribute).Assembly);
			xmlSerializerFormatAttribute.Style = style;
			context.SyncMethod.CustomAttributes.Add(OperationGenerator.GenerateAttributeDeclaration(context.Contract.ServiceContractGenerator, xmlSerializerFormatAttribute));
			this.AddKnownTypes(context.SyncMethod.CustomAttributes, xmlSerializerFormatAttribute.IsEncoded ? this.SoapExporter.IncludeMetadata : this.XmlExporter.IncludeMetadata);
			DataContractSerializerOperationGenerator.UpdateTargetCompileUnit(context, this.options.CodeCompileUnit);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0009FFB8 File Offset: 0x0009E1B8
		private void AddKnownTypes(CodeAttributeDeclarationCollection destination, CodeAttributeDeclarationCollection source)
		{
			foreach (object obj in source)
			{
				CodeAttributeDeclaration include = (CodeAttributeDeclaration)obj;
				CodeAttributeDeclaration codeAttributeDeclaration = this.ToKnownType(include);
				if (codeAttributeDeclaration != null)
				{
					destination.Add(codeAttributeDeclaration);
				}
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000A0018 File Offset: 0x0009E218
		private CodeAttributeDeclaration ToKnownType(CodeAttributeDeclaration include)
		{
			if (include.Name == typeof(SoapIncludeAttribute).FullName || include.Name == typeof(XmlIncludeAttribute).FullName)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(ServiceKnownTypeAttribute)));
				foreach (object obj in include.Arguments)
				{
					CodeAttributeArgument value = (CodeAttributeArgument)obj;
					codeAttributeDeclaration.Arguments.Add(value);
				}
				return codeAttributeDeclaration;
			}
			return null;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000A00C8 File Offset: 0x0009E2C8
		private void GeneratePartType(Dictionary<XmlMembersMapping, XmlMembersMapping> alreadyExported, MessagePartDescription part, string partNamespace)
		{
			if (!this.partInfoTable.ContainsKey(part))
			{
				return;
			}
			XmlSerializerOperationGenerator.PartInfo partInfo = this.partInfoTable[part];
			XmlMembersMapping membersMapping = partInfo.MembersMapping;
			XmlMemberMapping memberMapping = partInfo.MemberMapping;
			if (!alreadyExported.ContainsKey(membersMapping))
			{
				if (partInfo.IsEncoded)
				{
					this.SoapExporter.ExportMembersMapping(membersMapping);
				}
				else
				{
					this.XmlExporter.ExportMembersMapping(membersMapping);
				}
				alreadyExported.Add(membersMapping, membersMapping);
			}
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			if (partInfo.IsEncoded)
			{
				this.SoapExporter.AddMappingMetadata(codeAttributeDeclarationCollection, memberMapping, false);
			}
			else
			{
				this.XmlExporter.AddMappingMetadata(codeAttributeDeclarationCollection, memberMapping, partNamespace, false);
			}
			part.BaseType = this.GetTypeName(memberMapping);
			this.operationGenerator.ParameterTypes.Add(part, new CodeTypeReference(part.BaseType));
			this.operationGenerator.ParameterAttributes.Add(part, codeAttributeDeclarationCollection);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000A019C File Offset: 0x0009E39C
		internal string GetTypeName(XmlMemberMapping member)
		{
			string text = member.GenerateTypeName(this.options.CodeProvider);
			string b = text.Replace("[]", null);
			if (this.codeNamespace != null && !string.IsNullOrEmpty(this.codeNamespace.Name))
			{
				foreach (object obj in this.codeNamespace.Types)
				{
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
					if (codeTypeDeclaration.Name == b)
					{
						text = this.codeNamespace.Name + "." + text;
					}
				}
			}
			return text;
		}

		// Token: 0x04002294 RID: 8852
		private OperationGenerator operationGenerator;

		// Token: 0x04002295 RID: 8853
		private Dictionary<MessagePartDescription, XmlSerializerOperationGenerator.PartInfo> partInfoTable;

		// Token: 0x04002296 RID: 8854
		private Dictionary<OperationDescription, XmlSerializerFormatAttribute> operationAttributes = new Dictionary<OperationDescription, XmlSerializerFormatAttribute>();

		// Token: 0x04002297 RID: 8855
		private XmlCodeExporter xmlExporter;

		// Token: 0x04002298 RID: 8856
		private SoapCodeExporter soapExporter;

		// Token: 0x04002299 RID: 8857
		private XmlSerializerImportOptions options;

		// Token: 0x0400229A RID: 8858
		private CodeNamespace codeNamespace;

		// Token: 0x0400229B RID: 8859
		private static object contractMarker = new object();

		// Token: 0x02000C01 RID: 3073
		private class PartInfo
		{
			// Token: 0x040042C8 RID: 17096
			internal XmlMemberMapping MemberMapping;

			// Token: 0x040042C9 RID: 17097
			internal XmlMembersMapping MembersMapping;

			// Token: 0x040042CA RID: 17098
			internal bool IsEncoded;
		}

		// Token: 0x02000C02 RID: 3074
		internal class WrappedBodyTypeGenerator : IWrappedBodyTypeGenerator
		{
			// Token: 0x06007632 RID: 30258 RVA: 0x001BBC7F File Offset: 0x001B9E7F
			public WrappedBodyTypeGenerator(OperationContractGenerationContext context)
			{
				this.context = context;
			}

			// Token: 0x06007633 RID: 30259 RVA: 0x001BBC8E File Offset: 0x001B9E8E
			public void ValidateForParameterMode(OperationDescription operation)
			{
			}

			// Token: 0x06007634 RID: 30260 RVA: 0x001BBC90 File Offset: 0x001B9E90
			public void AddMemberAttributes(XmlName messageName, MessagePartDescription part, CodeAttributeDeclarationCollection importedAttributes, CodeAttributeDeclarationCollection typeAttributes, CodeAttributeDeclarationCollection fieldAttributes)
			{
				if (importedAttributes != null)
				{
					fieldAttributes.AddRange(importedAttributes);
				}
			}

			// Token: 0x06007635 RID: 30261 RVA: 0x001BBCA0 File Offset: 0x001B9EA0
			public void AddTypeAttributes(string messageName, string typeNS, CodeAttributeDeclarationCollection typeAttributes, bool isEncoded)
			{
				if (isEncoded)
				{
					return;
				}
				XmlTypeAttribute xmlTypeAttribute = new XmlTypeAttribute();
				xmlTypeAttribute.Namespace = typeNS;
				typeAttributes.Add(OperationGenerator.GenerateAttributeDeclaration(this.context.Contract.ServiceContractGenerator, xmlTypeAttribute));
			}

			// Token: 0x040042CB RID: 17099
			private OperationContractGenerationContext context;
		}
	}
}
