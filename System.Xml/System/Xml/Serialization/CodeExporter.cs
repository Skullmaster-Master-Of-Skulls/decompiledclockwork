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
	// Token: 0x020002AD RID: 685
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeExporter
	{
		// Token: 0x060020E3 RID: 8419 RVA: 0x0009B5C0 File Offset: 0x0009A5C0
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

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0009B655 File Offset: 0x0009A655
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0009B65D File Offset: 0x0009A65D
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0009B678 File Offset: 0x0009A678
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0009B693 File Offset: 0x0009A693
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

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0009B6AE File Offset: 0x0009A6AE
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

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0009B6C9 File Offset: 0x0009A6C9
		internal bool GenerateProperties
		{
			get
			{
				return (this.options & CodeGenerationOptions.GenerateProperties) != CodeGenerationOptions.None;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x0009B6DC File Offset: 0x0009A6DC
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

		// Token: 0x060020EB RID: 8427 RVA: 0x0009B78C File Offset: 0x0009A78C
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

		// Token: 0x060020EC RID: 8428 RVA: 0x0009B808 File Offset: 0x0009A808
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0009B846 File Offset: 0x0009A846
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.includeMetadata;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x0009B84E File Offset: 0x0009A84E
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0009B856 File Offset: 0x0009A856
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

		// Token: 0x060020F0 RID: 8432
		internal abstract void ExportDerivedStructs(StructMapping mapping);

		// Token: 0x060020F1 RID: 8433
		internal abstract void EnsureTypesExported(Accessor[] accessors, string ns);

		// Token: 0x060020F2 RID: 8434 RVA: 0x0009B884 File Offset: 0x0009A884
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("XmlCodegenWarningDetails", new object[]
			{
				text
			}), false));
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0009B8B4 File Offset: 0x0009A8B4
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
							this.EnsureTypesExported(arrayMapping.Elements, arrayMapping.Namespace);
						}
					}
				}
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0009BA38 File Offset: 0x0009AA38
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

		// Token: 0x060020F5 RID: 8437 RVA: 0x0009BAC0 File Offset: 0x0009AAC0
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

		// Token: 0x060020F6 RID: 8438 RVA: 0x0009BBB0 File Offset: 0x0009ABB0
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

		// Token: 0x060020F7 RID: 8439 RVA: 0x0009BC88 File Offset: 0x0009AC88
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

		// Token: 0x060020F8 RID: 8440 RVA: 0x0009BCF0 File Offset: 0x0009ACF0
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

		// Token: 0x060020F9 RID: 8441 RVA: 0x0009BDA0 File Offset: 0x0009ADA0
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

		// Token: 0x060020FA RID: 8442 RVA: 0x0009BE2C File Offset: 0x0009AE2C
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

		// Token: 0x060020FB RID: 8443 RVA: 0x0009BF19 File Offset: 0x0009AF19
		internal static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0009BF2C File Offset: 0x0009AF2C
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

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0009BFA3 File Offset: 0x0009AFA3
		private bool EnableDataBinding
		{
			get
			{
				return (this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0009BFB4 File Offset: 0x0009AFB4
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

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0009C0B8 File Offset: 0x0009B0B8
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

		// Token: 0x0400141C RID: 5148
		private Hashtable exportedMappings;

		// Token: 0x0400141D RID: 5149
		private Hashtable exportedClasses;

		// Token: 0x0400141E RID: 5150
		private CodeNamespace codeNamespace;

		// Token: 0x0400141F RID: 5151
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x04001420 RID: 5152
		private bool rootExported;

		// Token: 0x04001421 RID: 5153
		private TypeScope scope;

		// Token: 0x04001422 RID: 5154
		private CodeAttributeDeclarationCollection includeMetadata = new CodeAttributeDeclarationCollection();

		// Token: 0x04001423 RID: 5155
		private CodeGenerationOptions options;

		// Token: 0x04001424 RID: 5156
		private CodeDomProvider codeProvider;

		// Token: 0x04001425 RID: 5157
		private CodeAttributeDeclaration generatedCodeAttribute;
	}
}
