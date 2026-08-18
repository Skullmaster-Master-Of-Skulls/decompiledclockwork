using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x0200012E RID: 302
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeExporter
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x000612FC File Offset: 0x0005F4FC
		internal CodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable exportedMappings)
		{
			if (codeNamespace != null)
			{
				CodeGenerator.ValidateIdentifiers(codeNamespace);
			}
			this.codeNamespace = codeNamespace;
			if (codeCompileUnit != null)
			{
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				}
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.Xml.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				}
			}
			this.codeCompileUnit = codeCompileUnit;
			this.options = options;
			this.exportedMappings = exportedMappings;
			this.codeProvider = codeProvider;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00061391 File Offset: 0x0005F591
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00061399 File Offset: 0x0005F599
		internal CodeNamespace CodeNamespace
		{
			get
			{
				if (this.codeNamespace == null)
				{
					this.codeNamespace = new CodeNamespace();
				}
				return this.codeNamespace;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x000613B4 File Offset: 0x0005F5B4
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000613CF File Offset: 0x0005F5CF
		internal Hashtable ExportedClasses
		{
			get
			{
				if (this.exportedClasses == null)
				{
					this.exportedClasses = new Hashtable();
				}
				return this.exportedClasses;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x000613EA File Offset: 0x0005F5EA
		internal Hashtable ExportedMappings
		{
			get
			{
				if (this.exportedMappings == null)
				{
					this.exportedMappings = new Hashtable();
				}
				return this.exportedMappings;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00061405 File Offset: 0x0005F605
		internal bool GenerateProperties
		{
			get
			{
				return (this.options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00061414 File Offset: 0x0005F614
		internal CodeAttributeDeclaration GeneratedCodeAttribute
		{
			get
			{
				if (this.generatedCodeAttribute == null)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetExecutingAssembly();
						if (assembly == null)
						{
							assembly = typeof(CodeExporter).Assembly;
						}
					}
					AssemblyName name = assembly.GetName();
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
					string productVersion = CodeExporter.GetProductVersion(assembly);
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
					this.generatedCodeAttribute = codeAttributeDeclaration;
				}
				return this.generatedCodeAttribute;
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000614D0 File Offset: 0x0005F6D0
		internal static CodeAttributeDeclaration FindAttributeDeclaration(Type type, CodeAttributeDeclarationCollection metadata)
		{
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == type.FullName || codeAttributeDeclaration.Name == type.Name)
				{
					return codeAttributeDeclaration;
				}
			}
			return null;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0006154C File Offset: 0x0005F74C
		private static string GetProductVersion(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is AssemblyInformationalVersionAttribute)
				{
					AssemblyInformationalVersionAttribute assemblyInformationalVersionAttribute = (AssemblyInformationalVersionAttribute)customAttributes[i];
					return assemblyInformationalVersionAttribute.InformationalVersion;
				}
			}
			return null;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0006158A File Offset: 0x0005F78A
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.includeMetadata;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00061592 File Offset: 0x0005F792
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0006159A File Offset: 0x0005F79A
		internal void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("XmlMappingsScopeMismatch"));
			}
		}

		// Token: 0x06001605 RID: 5637
		internal abstract void ExportDerivedStructs(StructMapping mapping);

		// Token: 0x06001606 RID: 5638
		internal abstract void EnsureTypesExported(Accessor[] accessors, string ns);

		// Token: 0x06001607 RID: 5639 RVA: 0x000615C5 File Offset: 0x0005F7C5
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("XmlCodegenWarningDetails", new object[]
			{
				text
			}), false));
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000615E8 File Offset: 0x0005F7E8
		internal void ExportRoot(StructMapping mapping, Type includeType)
		{
			if (!this.rootExported)
			{
				this.rootExported = true;
				this.ExportDerivedStructs(mapping);
				for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
				{
					if (!structMapping.ReferencedByElement && structMapping.IncludeInSchema && !structMapping.IsAnonymousType)
					{
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(includeType.FullName);
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName)));
						this.includeMetadata.Add(codeAttributeDeclaration);
					}
				}
				Hashtable hashtable = new Hashtable();
				foreach (object obj in this.Scope.TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping is ArrayMapping)
					{
						ArrayMapping arrayMapping = (ArrayMapping)typeMapping;
						if (CodeExporter.ShouldInclude(arrayMapping) && !hashtable.Contains(arrayMapping.TypeDesc.FullName))
						{
							CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(includeType.FullName);
							codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(arrayMapping.TypeDesc.FullName)));
							this.includeMetadata.Add(codeAttributeDeclaration2);
							hashtable.Add(arrayMapping.TypeDesc.FullName, string.Empty);
							Accessor[] elements = arrayMapping.Elements;
							this.EnsureTypesExported(elements, arrayMapping.Namespace);
						}
					}
				}
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00061770 File Offset: 0x0005F970
		private static bool ShouldInclude(ArrayMapping arrayMapping)
		{
			if (arrayMapping.ReferencedByElement)
			{
				return false;
			}
			if (arrayMapping.Next != null)
			{
				return false;
			}
			if (arrayMapping.Elements.Length == 1)
			{
				TypeKind kind = arrayMapping.Elements[0].Mapping.TypeDesc.Kind;
				if (kind == TypeKind.Node)
				{
					return false;
				}
			}
			for (int i = 0; i < arrayMapping.Elements.Length; i++)
			{
				if (arrayMapping.Elements[i].Name != arrayMapping.Elements[i].Mapping.DefaultElementName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000617F8 File Offset: 0x0005F9F8
		internal CodeTypeDeclaration ExportEnum(EnumMapping mapping, Type type)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(mapping.TypeDesc.Name);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			codeTypeDeclaration.IsEnum = true;
			if (mapping.IsFlags && mapping.Constants.Length > 31)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(long)));
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			this.CodeNamespace.Types.Add(codeTypeDeclaration);
			for (int i = 0; i < mapping.Constants.Length; i++)
			{
				CodeExporter.ExportConstant(codeTypeDeclaration, mapping.Constants[i], type, mapping.IsFlags, 1L << i);
			}
			if (mapping.IsFlags)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(FlagsAttribute).FullName);
				codeTypeDeclaration.CustomAttributes.Add(value);
			}
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000618E8 File Offset: 0x0005FAE8
		internal void AddTypeMetadata(CodeAttributeDeclarationCollection metadata, Type type, string defaultName, string name, string ns, bool includeInSchema)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
			if (name == null || name.Length == 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("AnonymousType", new CodePrimitiveExpression(true)));
			}
			else if (defaultName != name)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("TypeName", new CodePrimitiveExpression(name)));
			}
			if (ns != null && ns.Length != 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (!includeInSchema)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IncludeInSchema", new CodePrimitiveExpression(false)));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				metadata.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000619C0 File Offset: 0x0005FBC0
		internal static void AddIncludeMetadata(CodeAttributeDeclarationCollection metadata, StructMapping mapping, Type type)
		{
			if (mapping.IsAnonymousType)
			{
				return;
			}
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				metadata.Add(new CodeAttributeDeclaration(type.FullName)
				{
					Arguments = 
					{
						new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName))
					}
				});
				CodeExporter.AddIncludeMetadata(metadata, structMapping, type);
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00061A28 File Offset: 0x0005FC28
		internal static void ExportConstant(CodeTypeDeclaration codeClass, ConstantMapping constant, Type type, bool init, long enumValue)
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(int).FullName, constant.Name);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			if (init)
			{
				codeMemberField.InitExpression = new CodePrimitiveExpression(enumValue);
			}
			codeClass.Members.Add(codeMemberField);
			if (constant.XmlName != constant.Name)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(constant.XmlName)));
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00061AD8 File Offset: 0x0005FCD8
		internal static object PromoteType(Type type, object value)
		{
			if (type == typeof(sbyte))
			{
				return ((IConvertible)value).ToInt16(null);
			}
			if (type == typeof(ushort))
			{
				return ((IConvertible)value).ToInt32(null);
			}
			if (type == typeof(uint))
			{
				return ((IConvertible)value).ToInt64(null);
			}
			if (type == typeof(ulong))
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return value;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00061B78 File Offset: 0x0005FD78
		internal CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = ((codeMemberProperty.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			CodeExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			CodeExpression right = new CodePropertySetValueReferenceExpression();
			codeAssignStatement.Left = left;
			codeAssignStatement.Right = right;
			if (this.EnableDataBinding)
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
				codeMemberProperty.SetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), CodeExporter.RaisePropertyChangedEventMethod.Name, new CodeExpression[]
				{
					new CodePrimitiveExpression(name)
				}));
			}
			else
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
			}
			return codeMemberProperty;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00061C60 File Offset: 0x0005FE60
		internal static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00061C74 File Offset: 0x0005FE74
		internal void AddPropertyChangedNotifier(CodeTypeDeclaration codeClass)
		{
			if (this.EnableDataBinding && codeClass != null)
			{
				if (codeClass.BaseTypes.Count == 0)
				{
					codeClass.BaseTypes.Add(typeof(object));
				}
				codeClass.BaseTypes.Add(new CodeTypeReference(typeof(INotifyPropertyChanged)));
				codeClass.Members.Add(CodeExporter.PropertyChangedEvent);
				codeClass.Members.Add(CodeExporter.RaisePropertyChangedEventMethod);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00061CEB File Offset: 0x0005FEEB
		private bool EnableDataBinding
		{
			get
			{
				return (this.options & CodeGenerationOptions.EnableDataBinding) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00061CFC File Offset: 0x0005FEFC
		internal static CodeMemberMethod RaisePropertyChangedEventMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "RaisePropertyChanged";
				codeMemberMethod.Attributes = (MemberAttributes)12290;
				CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("propertyName");
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), codeArgumentReferenceExpression.ParameterName));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("propertyChanged");
				codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(PropertyChangedEventHandler), codeVariableReferenceExpression.VariableName, new CodeEventReferenceExpression(new CodeThisReferenceExpression(), CodeExporter.PropertyChangedEvent.Name)));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeStatement[0]);
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeConditionStatement.TrueStatements.Add(new CodeDelegateInvokeExpression(codeVariableReferenceExpression, new CodeExpression[]
				{
					new CodeThisReferenceExpression(),
					new CodeObjectCreateExpression(typeof(PropertyChangedEventArgs), new CodeExpression[]
					{
						codeArgumentReferenceExpression
					})
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00061DF4 File Offset: 0x0005FFF4
		internal static CodeMemberEvent PropertyChangedEvent
		{
			get
			{
				return new CodeMemberEvent
				{
					Attributes = MemberAttributes.Public,
					Name = "PropertyChanged",
					Type = new CodeTypeReference(typeof(PropertyChangedEventHandler)),
					ImplementationTypes = 
					{
						typeof(INotifyPropertyChanged)
					}
				};
			}
		}

		// Token: 0x04000A50 RID: 2640
		private Hashtable exportedMappings;

		// Token: 0x04000A51 RID: 2641
		private Hashtable exportedClasses;

		// Token: 0x04000A52 RID: 2642
		private CodeNamespace codeNamespace;

		// Token: 0x04000A53 RID: 2643
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x04000A54 RID: 2644
		private bool rootExported;

		// Token: 0x04000A55 RID: 2645
		private TypeScope scope;

		// Token: 0x04000A56 RID: 2646
		private CodeAttributeDeclarationCollection includeMetadata = new CodeAttributeDeclarationCollection();

		// Token: 0x04000A57 RID: 2647
		private CodeGenerationOptions options;

		// Token: 0x04000A58 RID: 2648
		private CodeDomProvider codeProvider;

		// Token: 0x04000A59 RID: 2649
		private CodeAttributeDeclaration generatedCodeAttribute;
	}
}
