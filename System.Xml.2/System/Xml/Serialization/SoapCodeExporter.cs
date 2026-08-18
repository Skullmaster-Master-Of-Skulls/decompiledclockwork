using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x02000171 RID: 369
	public class SoapCodeExporter : CodeExporter
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x0006C1FE File Offset: 0x0006A3FE
		public SoapCodeExporter(CodeNamespace codeNamespace) : base(codeNamespace, null, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006C20B File Offset: 0x0006A40B
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit) : base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0006C218 File Offset: 0x0006A418
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options) : base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0006C225 File Offset: 0x0006A425
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0006C233 File Offset: 0x0006A433
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0006C242 File Offset: 0x0006A442
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			xmlTypeMapping.CheckShallow();
			base.CheckScope(xmlTypeMapping.Scope);
			this.ExportElement(xmlTypeMapping.Accessor);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0006C264 File Offset: 0x0006A464
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			xmlMembersMapping.CheckShallow();
			base.CheckScope(xmlMembersMapping.Scope);
			for (int i = 0; i < xmlMembersMapping.Count; i++)
			{
				this.ExportElement((ElementAccessor)xmlMembersMapping[i].Accessor);
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0006C2AB File Offset: 0x0006A4AB
		private void ExportElement(ElementAccessor element)
		{
			this.ExportType(element.Mapping);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0006C2BC File Offset: 0x0006A4BC
		private void ExportType(TypeMapping mapping)
		{
			if (mapping.IsReference)
			{
				return;
			}
			if (base.ExportedMappings[mapping] == null)
			{
				CodeTypeDeclaration codeTypeDeclaration = null;
				base.ExportedMappings.Add(mapping, mapping);
				if (mapping is EnumMapping)
				{
					codeTypeDeclaration = base.ExportEnum((EnumMapping)mapping, typeof(SoapEnumAttribute));
				}
				else if (mapping is StructMapping)
				{
					codeTypeDeclaration = this.ExportStruct((StructMapping)mapping);
				}
				else if (mapping is ArrayMapping)
				{
					Accessor[] elements = ((ArrayMapping)mapping).Elements;
					this.EnsureTypesExported(elements, null);
				}
				if (codeTypeDeclaration != null)
				{
					codeTypeDeclaration.CustomAttributes.Add(base.GeneratedCodeAttribute);
					codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(SerializableAttribute).FullName));
					if (!codeTypeDeclaration.IsEnum)
					{
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument[]
						{
							new CodeAttributeArgument(new CodePrimitiveExpression("code"))
						}));
					}
					base.AddTypeMetadata(codeTypeDeclaration.CustomAttributes, typeof(SoapTypeAttribute), mapping.TypeDesc.Name, Accessor.UnescapeName(mapping.TypeName), mapping.Namespace, mapping.IncludeInSchema);
					base.ExportedClasses.Add(mapping, codeTypeDeclaration);
				}
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0006C424 File Offset: 0x0006A624
		private CodeTypeDeclaration ExportStruct(StructMapping mapping)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				base.ExportRoot(mapping, typeof(SoapIncludeAttribute));
				return null;
			}
			if (!mapping.IncludeInSchema)
			{
				return null;
			}
			string name = mapping.TypeDesc.Name;
			string text = (mapping.TypeDesc.BaseTypeDesc == null) ? string.Empty : mapping.TypeDesc.BaseTypeDesc.Name;
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.IsPartial = base.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			base.CodeNamespace.Types.Add(codeTypeDeclaration);
			if (text != null && text.Length > 0)
			{
				codeTypeDeclaration.BaseTypes.Add(text);
			}
			else
			{
				base.AddPropertyChangedNotifier(codeTypeDeclaration);
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			if (mapping.TypeDesc.IsAbstract)
			{
				codeTypeDeclaration.TypeAttributes |= TypeAttributes.Abstract;
			}
			CodeExporter.AddIncludeMetadata(codeTypeDeclaration.CustomAttributes, mapping, typeof(SoapIncludeAttribute));
			if (base.GenerateProperties)
			{
				for (int i = 0; i < mapping.Members.Length; i++)
				{
					this.ExportProperty(codeTypeDeclaration, mapping.Members[i], mapping.Scope);
				}
			}
			else
			{
				for (int j = 0; j < mapping.Members.Length; j++)
				{
					this.ExportMember(codeTypeDeclaration, mapping.Members[j]);
				}
			}
			for (int k = 0; k < mapping.Members.Length; k++)
			{
				Accessor[] elements = mapping.Members[k].Elements;
				this.EnsureTypesExported(elements, null);
			}
			if (mapping.BaseMapping != null)
			{
				this.ExportType(mapping.BaseMapping);
			}
			this.ExportDerivedStructs(mapping);
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0006C5E8 File Offset: 0x0006A7E8
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		internal override void ExportDerivedStructs(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				this.ExportType(structMapping);
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006C60F File Offset: 0x0006A80F
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, bool forceUseMemberName)
		{
			this.AddMemberMetadata(metadata, member.Mapping, forceUseMemberName);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006C61F File Offset: 0x0006A81F
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member)
		{
			this.AddMemberMetadata(metadata, member.Mapping, false);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0006C630 File Offset: 0x0006A830
		private void AddElementMetadata(CodeAttributeDeclarationCollection metadata, string elementName, TypeDesc typeDesc, bool isNullable)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(SoapElementAttribute).FullName);
			if (elementName != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(elementName)));
			}
			if (typeDesc != null && typeDesc.IsAmbiguousDataType)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(typeDesc.DataType.Name)));
			}
			if (isNullable)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(true)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0006C6CC File Offset: 0x0006A8CC
		private void AddMemberMetadata(CodeAttributeDeclarationCollection metadata, MemberMapping member, bool forceUseMemberName)
		{
			if (member.Elements.Length == 0)
			{
				return;
			}
			ElementAccessor elementAccessor = member.Elements[0];
			TypeMapping mapping = elementAccessor.Mapping;
			string text = Accessor.UnescapeName(elementAccessor.Name);
			bool flag = text == member.Name && !forceUseMemberName;
			if (!flag || mapping.TypeDesc.IsAmbiguousDataType || elementAccessor.IsNullable)
			{
				this.AddElementMetadata(metadata, flag ? null : text, mapping.TypeDesc.IsAmbiguousDataType ? mapping.TypeDesc : null, elementAccessor.IsNullable);
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006C758 File Offset: 0x0006A958
		private void ExportMember(CodeTypeDeclaration codeClass, MemberMapping member)
		{
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, member.Name);
			codeMemberField.Attributes = ((codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			codeClass.Members.Add(codeMemberField);
			this.AddMemberMetadata(codeMemberField.CustomAttributes, member, false);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, member.Name + "Specified");
				codeMemberField.Attributes = ((codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
				codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(SoapIgnoreAttribute).FullName);
				codeMemberField.CustomAttributes.Add(value);
				codeClass.Members.Add(codeMemberField);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006C864 File Offset: 0x0006AA64
		private void ExportProperty(CodeTypeDeclaration codeClass, MemberMapping member, CodeIdentifiers memberScope)
		{
			string text = memberScope.AddUnique(CodeExporter.MakeFieldName(member.Name), member);
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			codeClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name, typeName);
			codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			this.AddMemberMetadata(codeMemberProperty.CustomAttributes, member, false);
			codeClass.Members.Add(codeMemberProperty);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, text + "Specified");
				codeMemberField.Attributes = MemberAttributes.Private;
				codeClass.Members.Add(codeMemberField);
				codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name + "Specified", typeof(bool).FullName);
				codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(SoapIgnoreAttribute).FullName);
				codeMemberProperty.CustomAttributes.Add(value);
				codeClass.Members.Add(codeMemberProperty);
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0006C9B0 File Offset: 0x0006ABB0
		internal override void EnsureTypesExported(Accessor[] accessors, string ns)
		{
			if (accessors == null)
			{
				return;
			}
			for (int i = 0; i < accessors.Length; i++)
			{
				this.ExportType(accessors[i].Mapping);
			}
		}
	}
}
