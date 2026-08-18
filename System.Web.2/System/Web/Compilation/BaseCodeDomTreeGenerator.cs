using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Profile;
using System.Web.UI;
using System.Web.Util;
using Microsoft.VisualBasic;

namespace System.Web.Compilation
{
	// Token: 0x020007FB RID: 2043
	internal abstract class BaseCodeDomTreeGenerator
	{
		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x06006168 RID: 24936 RVA: 0x00150D8F File Offset: 0x0014EF8F
		private TemplateParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x00150D97 File Offset: 0x0014EF97
		internal void SetDesignerMode()
		{
			this._designerMode = true;
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x0600616A RID: 24938 RVA: 0x00150DA0 File Offset: 0x0014EFA0
		internal IDictionary LinePragmasTable
		{
			get
			{
				return this._linePragmasTable;
			}
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x00150DA8 File Offset: 0x0014EFA8
		static BaseCodeDomTreeGenerator()
		{
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			BaseCodeDomTreeGenerator._urlLinePragmas = compilationAppConfig.UrlLinePragmas;
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x00150DC8 File Offset: 0x0014EFC8
		internal CodeCompileUnit GetCodeDomTree(CodeDomProvider codeDomProvider, StringResourceBuilder stringResourceBuilder, VirtualPath virtualPath)
		{
			this._codeDomProvider = codeDomProvider;
			this._stringResourceBuilder = stringResourceBuilder;
			this._virtualPath = virtualPath;
			if (!this.BuildSourceDataTree())
			{
				return null;
			}
			if (this.Parser.RootBuilder != null)
			{
				this.Parser.RootBuilder.OnCodeGenerationComplete();
			}
			return this._codeCompileUnit;
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x0600616D RID: 24941 RVA: 0x00150E17 File Offset: 0x0014F017
		protected CompilerParameters CompilParams
		{
			get
			{
				return this._compilParams;
			}
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x00150E1F File Offset: 0x0014F01F
		internal string GetInstantiatableFullTypeName()
		{
			if (this.PrecompilingForUpdatableDeployment)
			{
				return null;
			}
			return Util.MakeFullTypeName(this._sourceDataNamespace.Name, this._sourceDataClass.Name);
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x00150E46 File Offset: 0x0014F046
		internal string GetIntermediateFullTypeName()
		{
			return Util.MakeFullTypeName(this.Parser.BaseTypeNamespace, this._intermediateClass.Name);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x00150E63 File Offset: 0x0014F063
		protected BaseCodeDomTreeGenerator(TemplateParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x00150E7C File Offset: 0x0014F07C
		protected void ApplyEditorBrowsableCustomAttribute(CodeTypeMember member)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration();
			codeAttributeDeclaration.Name = typeof(EditorBrowsableAttribute).FullName;
			codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EditorBrowsableState)), "Never")));
			member.CustomAttributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x00150EDC File Offset: 0x0014F0DC
		protected virtual string GetGeneratedClassName()
		{
			if (this.Parser.GeneratedClassName != null)
			{
				return this.Parser.GeneratedClassName;
			}
			string text = this._virtualPath.FileName;
			string appRelativeVirtualPathStringOrNull = this._virtualPath.Parent.AppRelativeVirtualPathStringOrNull;
			if (appRelativeVirtualPathStringOrNull != null)
			{
				text = appRelativeVirtualPathStringOrNull.Substring(2) + text;
			}
			text = Util.MakeValidTypeNameFromString(text);
			text = text.ToLowerInvariant();
			string s = (this.Parser.BaseTypeName != null) ? this.Parser.BaseTypeName : this.Parser.BaseType.Name;
			if (StringUtil.EqualsIgnoreCase(text, s))
			{
				text = "_" + text;
			}
			return text;
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x00150F7F File Offset: 0x0014F17F
		internal static bool IsAspNetNamespace(string ns)
		{
			return ns == "ASP";
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x00150F8C File Offset: 0x0014F18C
		private bool PrecompilingForUpdatableDeployment
		{
			get
			{
				return !this.IsGlobalAsaxGenerator && BuildManager.PrecompilingForUpdatableDeployment;
			}
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x00150FA0 File Offset: 0x0014F1A0
		private bool BuildSourceDataTree()
		{
			this._compilParams = this.Parser.CompilParams;
			this._codeCompileUnit = new CodeCompileUnit();
			this._codeCompileUnit.UserData["AllowLateBound"] = !this.Parser.FStrict;
			this._codeCompileUnit.UserData["RequireVariableDeclaration"] = this.Parser.FExplicit;
			this._usingVJSCompiler = (this._codeDomProvider.FileExtension == ".jsl");
			this._sourceDataNamespace = new CodeNamespace(this.Parser.GeneratedNamespace);
			string generatedClassName = this.GetGeneratedClassName();
			if (this.Parser.BaseTypeName != null)
			{
				CodeNamespace codeNamespace = new CodeNamespace(this.Parser.BaseTypeNamespace);
				this._codeCompileUnit.Namespaces.Add(codeNamespace);
				this._intermediateClass = new CodeTypeDeclaration(this.Parser.BaseTypeName);
				if (this._designerMode)
				{
					this._intermediateClass.UserData["BaseClassDefinition"] = this.Parser.DefaultBaseType;
				}
				else
				{
					this._intermediateClass.UserData["BaseClassDefinition"] = this.Parser.BaseType;
				}
				codeNamespace.Types.Add(this._intermediateClass);
				this._intermediateClass.IsPartial = true;
				if (!this.PrecompilingForUpdatableDeployment)
				{
					this._sourceDataClass = new CodeTypeDeclaration(generatedClassName);
					this._sourceDataClass.BaseTypes.Add(CodeDomUtility.BuildGlobalCodeTypeReference(Util.MakeFullTypeName(this.Parser.BaseTypeNamespace, this.Parser.BaseTypeName)));
					this._sourceDataNamespace.Types.Add(this._sourceDataClass);
				}
			}
			else
			{
				this._intermediateClass = new CodeTypeDeclaration(generatedClassName);
				this._intermediateClass.BaseTypes.Add(CodeDomUtility.BuildGlobalCodeTypeReference(this.Parser.BaseType));
				this._sourceDataNamespace.Types.Add(this._intermediateClass);
				this._sourceDataClass = this._intermediateClass;
			}
			this._codeCompileUnit.Namespaces.Add(this._sourceDataNamespace);
			if (this.PrecompilingForUpdatableDeployment && this.Parser.CodeFileVirtualPath == null)
			{
				return false;
			}
			this.GenerateClassAttributes();
			if (this._codeDomProvider is VBCodeProvider)
			{
				this._sourceDataNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
			}
			if (this.Parser.NamespaceEntries != null)
			{
				foreach (object obj in this.Parser.NamespaceEntries.Values)
				{
					NamespaceEntry namespaceEntry = (NamespaceEntry)obj;
					CodeLinePragma linePragma;
					if (namespaceEntry.VirtualPath != null)
					{
						linePragma = this.CreateCodeLinePragma(namespaceEntry.VirtualPath, namespaceEntry.Line);
					}
					else
					{
						linePragma = null;
					}
					CodeNamespaceImport codeNamespaceImport = new CodeNamespaceImport(namespaceEntry.Namespace);
					codeNamespaceImport.LinePragma = linePragma;
					this._sourceDataNamespace.Imports.Add(codeNamespaceImport);
				}
			}
			if (this._sourceDataClass != null)
			{
				string typeName = Util.MakeFullTypeName(this._sourceDataNamespace.Name, this._sourceDataClass.Name);
				CodeTypeReference type = CodeDomUtility.BuildGlobalCodeTypeReference(typeName);
				this._classTypeExpr = new CodeTypeReferenceExpression(type);
			}
			this.GenerateInterfaces();
			this.BuildMiscClassMembers();
			if (!this._designerMode && this._sourceDataClass != null)
			{
				this._ctor = new CodeConstructor();
				this.AddDebuggerNonUserCodeAttribute(this._ctor);
				this._sourceDataClass.Members.Add(this._ctor);
				this._ctor.Attributes &= (MemberAttributes)(-61441);
				this._ctor.Attributes |= MemberAttributes.Public;
				this.BuildDefaultConstructor();
			}
			return true;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x00151378 File Offset: 0x0014F578
		private void SetInitMethod()
		{
			if (BinaryCompatibility.Current.TargetsAtLeastFramework472)
			{
				if (this.Parser.BaseType.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any((ConstructorInfo c) => c.GetParameters().Length != 0))
				{
					CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
					this.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
					codeMemberMethod.Name = "__Init";
					codeMemberMethod.Attributes = (MemberAttributes)20482;
					codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
					if (this.Parser.BaseType.GetConstructor(Type.EmptyTypes) != null)
					{
						this._ctor.Statements.Add(BaseCodeDomTreeGenerator.CreateInitInvoke());
					}
					else
					{
						this._sourceDataClass.Members.Remove(this._ctor);
					}
					this._sourceDataClass.Members.Add(codeMemberMethod);
					this._initMethod = codeMemberMethod;
				}
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06006177 RID: 24951 RVA: 0x0015146B File Offset: 0x0014F66B
		protected CodeMemberMethod InitMethod
		{
			get
			{
				if (!this._initMethodSet)
				{
					this.SetInitMethod();
					this._initMethodSet = true;
				}
				return this._initMethod ?? this._ctor;
			}
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x00151494 File Offset: 0x0014F694
		protected static CodeMethodInvokeExpression CreateInitInvoke()
		{
			return new CodeMethodInvokeExpression
			{
				Method = 
				{
					TargetObject = new CodeThisReferenceExpression(),
					MethodName = "__Init"
				}
			};
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x001514C8 File Offset: 0x0014F6C8
		protected virtual void GenerateClassAttributes()
		{
			if (this.CompilParams.IncludeDebugInformation && this._sourceDataClass != null)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration("System.Runtime.CompilerServices.CompilerGlobalScopeAttribute");
				this._sourceDataClass.CustomAttributes.Add(value);
			}
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x00151508 File Offset: 0x0014F708
		protected virtual void GenerateInterfaces()
		{
			if (this.Parser.ImplementedInterfaces != null)
			{
				foreach (object obj in this.Parser.ImplementedInterfaces)
				{
					Type type = (Type)obj;
					this._intermediateClass.BaseTypes.Add(new CodeTypeReference(type));
				}
			}
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void BuildInitStatements(CodeStatementCollection trueStatements, CodeStatementCollection topLevelStatements)
		{
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x00151584 File Offset: 0x0014F784
		protected virtual void BuildDefaultConstructor()
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(bool), "__initialized");
			codeMemberField.Attributes |= MemberAttributes.Static;
			this._sourceDataClass.Members.Add(codeMemberField);
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(this._classTypeExpr, "__initialized"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
			this.BuildInitStatements(codeConditionStatement.TrueStatements, this.InitMethod.Statements);
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(this._classTypeExpr, "__initialized"), new CodePrimitiveExpression(true)));
			this.InitMethod.Statements.Add(codeConditionStatement);
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x00151648 File Offset: 0x0014F848
		protected virtual void BuildMiscClassMembers()
		{
			if (this.NeedProfileProperty)
			{
				this.BuildProfileProperty();
			}
			if (this._sourceDataClass == null)
			{
				return;
			}
			this.BuildApplicationObjectProperties();
			this.BuildSessionObjectProperties();
			this.BuildPageObjectProperties();
			foreach (object obj in this.Parser.ScriptList)
			{
				ScriptBlockData scriptBlockData = (ScriptBlockData)obj;
				string text = scriptBlockData.Script;
				text = text.PadLeft(text.Length + scriptBlockData.Column - 1);
				CodeSnippetTypeMember codeSnippetTypeMember = new CodeSnippetTypeMember(text);
				codeSnippetTypeMember.LinePragma = this.CreateCodeLinePragma(scriptBlockData.VirtualPath, scriptBlockData.Line, scriptBlockData.Column, scriptBlockData.Column, scriptBlockData.Script.Length, false);
				this._sourceDataClass.Members.Add(codeSnippetTypeMember);
			}
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x00151734 File Offset: 0x0014F934
		private void BuildProfileProperty()
		{
			if (!ProfileManager.Enabled)
			{
				return;
			}
			string profileClassName = ProfileBase.GetProfileClassName();
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes &= (MemberAttributes)(-61441);
			codeMemberProperty.Attributes &= (MemberAttributes)(-16);
			codeMemberProperty.Attributes |= (MemberAttributes)12290;
			codeMemberProperty.Name = "Profile";
			if (this._designerMode)
			{
				this.ApplyEditorBrowsableCustomAttribute(codeMemberProperty);
			}
			codeMemberProperty.Type = new CodeTypeReference(profileClassName);
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Context");
			codePropertyReferenceExpression = new CodePropertyReferenceExpression(codePropertyReferenceExpression, "Profile");
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(profileClassName, codePropertyReferenceExpression)));
			this._intermediateClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x0600617F RID: 24959 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool NeedProfileProperty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x001517F4 File Offset: 0x0014F9F4
		protected void BuildAccessorProperty(string propName, CodeFieldReferenceExpression fieldRef, Type propType, MemberAttributes attributes, CodeAttributeDeclarationCollection attrDeclarations)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes = attributes;
			codeMemberProperty.Name = propName;
			codeMemberProperty.Type = new CodeTypeReference(propType);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(fieldRef));
			codeMemberProperty.SetStatements.Add(new CodeAssignStatement(fieldRef, new CodePropertySetValueReferenceExpression()));
			if (attrDeclarations != null)
			{
				codeMemberProperty.CustomAttributes = attrDeclarations;
			}
			this._sourceDataClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0015186C File Offset: 0x0014FA6C
		protected void BuildFieldAndAccessorProperty(string propName, string fieldName, Type propType, bool fStatic, CodeAttributeDeclarationCollection attrDeclarations)
		{
			CodeMemberField codeMemberField = new CodeMemberField(propType, fieldName);
			if (fStatic)
			{
				codeMemberField.Attributes |= MemberAttributes.Static;
			}
			this._sourceDataClass.Members.Add(codeMemberField);
			CodeFieldReferenceExpression fieldRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
			this.BuildAccessorProperty(propName, fieldRef, propType, MemberAttributes.Public, attrDeclarations);
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x001518C4 File Offset: 0x0014FAC4
		private void BuildInjectedGetPropertyMethod(string propName, Type propType, CodeExpression propertyInitExpression, bool fPublicProp)
		{
			string text = "cached" + propName;
			CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), text);
			this._sourceDataClass.Members.Add(new CodeMemberField(propType, text));
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			if (fPublicProp)
			{
				codeMemberProperty.Attributes &= (MemberAttributes)(-61441);
				codeMemberProperty.Attributes |= MemberAttributes.Public;
			}
			codeMemberProperty.Name = propName;
			codeMemberProperty.Type = new CodeTypeReference(propType);
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(codeExpression, propertyInitExpression));
			codeMemberProperty.GetStatements.Add(codeConditionStatement);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
			this._sourceDataClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x001519A0 File Offset: 0x0014FBA0
		private void BuildObjectPropertiesHelper(IDictionary objects, bool useApplicationState)
		{
			IDictionaryEnumerator enumerator = objects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)enumerator.Value;
				CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), useApplicationState ? "Application" : "Session"), "StaticObjects");
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(targetObject, "GetObject", new CodeExpression[0]);
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(httpStaticObjectsEntry.Name));
				Type declaredType = httpStaticObjectsEntry.DeclaredType;
				if (useApplicationState)
				{
					this.BuildInjectedGetPropertyMethod(httpStaticObjectsEntry.Name, declaredType, new CodeCastExpression(declaredType, codeMethodInvokeExpression), false);
				}
				else
				{
					CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
					codeMemberProperty.Name = httpStaticObjectsEntry.Name;
					codeMemberProperty.Type = new CodeTypeReference(declaredType);
					codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(declaredType, codeMethodInvokeExpression)));
					this._sourceDataClass.Members.Add(codeMemberProperty);
				}
			}
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x00151A90 File Offset: 0x0014FC90
		private void BuildApplicationObjectProperties()
		{
			if (this.Parser.ApplicationObjects != null)
			{
				this.BuildObjectPropertiesHelper(this.Parser.ApplicationObjects.Objects, true);
			}
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x00151AB6 File Offset: 0x0014FCB6
		private void BuildSessionObjectProperties()
		{
			if (this.Parser.SessionObjects != null)
			{
				this.BuildObjectPropertiesHelper(this.Parser.SessionObjects.Objects, false);
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06006186 RID: 24966 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool IsGlobalAsaxGenerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x00151ADC File Offset: 0x0014FCDC
		private void BuildPageObjectProperties()
		{
			if (this.Parser.PageObjectList == null)
			{
				return;
			}
			foreach (object obj in this.Parser.PageObjectList)
			{
				ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)obj;
				CodeExpression propertyInitExpression;
				if (objectTagBuilder.Progid != null)
				{
					propertyInitExpression = new CodeMethodInvokeExpression
					{
						Method = 
						{
							TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Server"),
							MethodName = "CreateObject"
						},
						Parameters = 
						{
							new CodePrimitiveExpression(objectTagBuilder.Progid)
						}
					};
				}
				else if (objectTagBuilder.Clsid != null)
				{
					propertyInitExpression = new CodeMethodInvokeExpression
					{
						Method = 
						{
							TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Server"),
							MethodName = "CreateObjectFromClsid"
						},
						Parameters = 
						{
							new CodePrimitiveExpression(objectTagBuilder.Clsid)
						}
					};
				}
				else
				{
					propertyInitExpression = new CodeObjectCreateExpression(objectTagBuilder.ObjectType, new CodeExpression[0]);
				}
				this.BuildInjectedGetPropertyMethod(objectTagBuilder.ID, objectTagBuilder.DeclaredType, propertyInitExpression, this.IsGlobalAsaxGenerator);
			}
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x00151C20 File Offset: 0x0014FE20
		protected CodeLinePragma CreateCodeLinePragma(ControlBuilder builder)
		{
			string pageVirtualPath = builder.PageVirtualPath;
			int line = builder.Line;
			int num = 1;
			int generatedColumn = 1;
			int codeLength = -1;
			CodeBlockBuilder codeBlockBuilder = builder as CodeBlockBuilder;
			if (codeBlockBuilder != null)
			{
				num = codeBlockBuilder.Column;
				codeLength = codeBlockBuilder.Content.Length;
				if (codeBlockBuilder.BlockType == CodeBlockType.Code)
				{
					generatedColumn = num;
				}
				else
				{
					generatedColumn = "__o".Length + BaseCodeDomTreeGenerator.GetGeneratedColumnOffset(this._codeDomProvider);
				}
			}
			return this.CreateCodeLinePragma(pageVirtualPath, line, num, generatedColumn, codeLength);
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00151C94 File Offset: 0x0014FE94
		internal static int GetGeneratedColumnOffset(CodeDomProvider codeDomProvider)
		{
			object obj = null;
			if (BaseCodeDomTreeGenerator._generatedColumnOffsetDictionary == null)
			{
				BaseCodeDomTreeGenerator._generatedColumnOffsetDictionary = new ListDictionary();
			}
			else
			{
				obj = BaseCodeDomTreeGenerator._generatedColumnOffsetDictionary[codeDomProvider.GetType()];
			}
			if (obj == null)
			{
				CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
				CodeNamespace codeNamespace = new CodeNamespace("ASP");
				codeCompileUnit.Namespaces.Add(codeNamespace);
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration("ColumnOffsetCalculator");
				codeTypeDeclaration.IsClass = true;
				codeNamespace.Types.Add(codeTypeDeclaration);
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
				codeMemberMethod.Name = "GenerateMethod";
				codeTypeDeclaration.Members.Add(codeMemberMethod);
				CodeStatement value = new CodeAssignStatement(new CodeVariableReferenceExpression("__o"), new CodeSnippetExpression("__dummyVar"));
				codeMemberMethod.Statements.Add(value);
				StringBuilder stringBuilder = new StringBuilder();
				StringWriter writer = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
				codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, writer, null);
				StringReader stringReader = new StringReader(stringBuilder.ToString());
				int num = 4;
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					text = text.TrimStart(new char[0]);
					int num2;
					if ((num2 = text.IndexOf("__dummyVar", StringComparison.Ordinal)) != -1)
					{
						num = num2 - "__o".Length + 1;
					}
				}
				BaseCodeDomTreeGenerator._generatedColumnOffsetDictionary[codeDomProvider.GetType()] = num;
				return num;
			}
			return (int)obj;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x00151E02 File Offset: 0x00150002
		protected CodeLinePragma CreateCodeLinePragma(string virtualPath, int lineNumber)
		{
			return this.CreateCodeLinePragma(virtualPath, lineNumber, 1, 1, -1, true);
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x00151E10 File Offset: 0x00150010
		protected CodeLinePragma CreateCodeLinePragma(string virtualPath, int lineNumber, int column, int generatedColumn, int codeLength)
		{
			return this.CreateCodeLinePragma(virtualPath, lineNumber, column, generatedColumn, codeLength, true);
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x00151E20 File Offset: 0x00150020
		protected CodeLinePragma CreateCodeLinePragma(string virtualPath, int lineNumber, int column, int generatedColumn, int codeLength, bool isCodeNugget)
		{
			if (!this.Parser.FLinePragmas)
			{
				return null;
			}
			if (string.IsNullOrEmpty(virtualPath))
			{
				return null;
			}
			if (this._designerMode)
			{
				if (codeLength < 0)
				{
					return null;
				}
				LinePragmaCodeInfo linePragmaCodeInfo = new LinePragmaCodeInfo();
				linePragmaCodeInfo._startLine = lineNumber;
				linePragmaCodeInfo._startColumn = column;
				linePragmaCodeInfo._startGeneratedColumn = generatedColumn;
				linePragmaCodeInfo._codeLength = codeLength;
				linePragmaCodeInfo._isCodeNugget = isCodeNugget;
				int pragmaIdGenerator = this._pragmaIdGenerator;
				this._pragmaIdGenerator = pragmaIdGenerator + 1;
				lineNumber = pragmaIdGenerator;
				if (this._linePragmasTable == null)
				{
					this._linePragmasTable = new Hashtable();
				}
				this._linePragmasTable[lineNumber] = linePragmaCodeInfo;
			}
			return BaseCodeDomTreeGenerator.CreateCodeLinePragmaHelper(virtualPath, lineNumber);
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x00151EC0 File Offset: 0x001500C0
		internal static CodeLinePragma CreateCodeLinePragmaHelper(string virtualPath, int lineNumber)
		{
			string text = null;
			if (UrlPath.IsAbsolutePhysicalPath(virtualPath))
			{
				text = virtualPath;
			}
			else if (BaseCodeDomTreeGenerator._urlLinePragmas)
			{
				text = ErrorFormatter.MakeHttpLinePragma(virtualPath);
			}
			else
			{
				try
				{
					text = HostingEnvironment.MapPathInternal(virtualPath);
					if (!File.Exists(text))
					{
						text = ErrorFormatter.MakeHttpLinePragma(virtualPath);
					}
				}
				catch
				{
					text = ErrorFormatter.MakeHttpLinePragma(virtualPath);
				}
			}
			return new CodeLinePragma(text, lineNumber);
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x00151F28 File Offset: 0x00150128
		protected void AddDebuggerNonUserCodeAttribute(CodeMemberMethod method)
		{
			if (method == null)
			{
				return;
			}
			if (!this.Parser.FLinePragmas)
			{
				return;
			}
			CodeAttributeDeclaration value = new CodeAttributeDeclaration(new CodeTypeReference(typeof(DebuggerNonUserCodeAttribute)));
			method.CustomAttributes.Add(value);
		}

		// Token: 0x040032A0 RID: 12960
		protected CodeDomProvider _codeDomProvider;

		// Token: 0x040032A1 RID: 12961
		protected CodeCompileUnit _codeCompileUnit;

		// Token: 0x040032A2 RID: 12962
		private CodeNamespace _sourceDataNamespace;

		// Token: 0x040032A3 RID: 12963
		protected CodeTypeDeclaration _sourceDataClass;

		// Token: 0x040032A4 RID: 12964
		protected CodeTypeDeclaration _intermediateClass;

		// Token: 0x040032A5 RID: 12965
		private CompilerParameters _compilParams;

		// Token: 0x040032A6 RID: 12966
		protected StringResourceBuilder _stringResourceBuilder;

		// Token: 0x040032A7 RID: 12967
		protected bool _usingVJSCompiler;

		// Token: 0x040032A8 RID: 12968
		private static IDictionary _generatedColumnOffsetDictionary;

		// Token: 0x040032A9 RID: 12969
		private CodeMemberMethod _initMethod;

		// Token: 0x040032AA RID: 12970
		private VirtualPath _virtualPath;

		// Token: 0x040032AB RID: 12971
		private CodeConstructor _ctor;

		// Token: 0x040032AC RID: 12972
		protected CodeTypeReferenceExpression _classTypeExpr;

		// Token: 0x040032AD RID: 12973
		internal const string defaultNamespace = "ASP";

		// Token: 0x040032AE RID: 12974
		internal const string internalAspNamespace = "__ASP";

		// Token: 0x040032AF RID: 12975
		private const string initializedFieldName = "__initialized";

		// Token: 0x040032B0 RID: 12976
		private const string _dummyVariable = "__dummyVar";

		// Token: 0x040032B1 RID: 12977
		private const int _defaultColumnOffset = 4;

		// Token: 0x040032B2 RID: 12978
		private const string InitMethodName = "__Init";

		// Token: 0x040032B3 RID: 12979
		private TemplateParser _parser;

		// Token: 0x040032B4 RID: 12980
		protected bool _designerMode;

		// Token: 0x040032B5 RID: 12981
		private IDictionary _linePragmasTable;

		// Token: 0x040032B6 RID: 12982
		private int _pragmaIdGenerator = 1;

		// Token: 0x040032B7 RID: 12983
		private static bool _urlLinePragmas;

		// Token: 0x040032B8 RID: 12984
		private bool _initMethodSet;
	}
}
