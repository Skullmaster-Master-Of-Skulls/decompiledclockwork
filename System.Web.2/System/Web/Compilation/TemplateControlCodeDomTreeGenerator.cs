using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000865 RID: 2149
	internal abstract class TemplateControlCodeDomTreeGenerator : BaseTemplateCodeDomTreeGenerator
	{
		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x00164EEA File Offset: 0x001630EA
		private TemplateControlParser Parser
		{
			get
			{
				return this._tcParser;
			}
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x00164EF2 File Offset: 0x001630F2
		internal TemplateControlCodeDomTreeGenerator(TemplateControlParser tcParser) : base(tcParser)
		{
			this._tcParser = tcParser;
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x00164F04 File Offset: 0x00163104
		protected override void BuildInitStatements(CodeStatementCollection trueStatements, CodeStatementCollection topLevelStatements)
		{
			base.BuildInitStatements(trueStatements, topLevelStatements);
			if (this._stringResourceBuilder.HasStrings)
			{
				CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__stringResource");
				codeMemberField.Attributes |= MemberAttributes.Static;
				this._sourceDataClass.Members.Add(codeMemberField);
				trueStatements.Add(new CodeAssignStatement
				{
					Left = new CodeFieldReferenceExpression(this._classTypeExpr, "__stringResource"),
					Right = new CodeMethodInvokeExpression
					{
						Method = 
						{
							TargetObject = new CodeThisReferenceExpression(),
							MethodName = "ReadStringResource"
						}
					}
				});
			}
			CodeTypeReference targetType = CodeDomUtility.BuildGlobalCodeTypeReference(this.Parser.BaseType);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeCastExpression(targetType, new CodeThisReferenceExpression()), "AppRelativeVirtualPath"), new CodePrimitiveExpression(this.Parser.CurrentVirtualPath.AppRelativeVirtualPathString));
			if (!this._designerMode && this.Parser.CodeFileVirtualPath != null)
			{
				codeAssignStatement.LinePragma = BaseCodeDomTreeGenerator.CreateCodeLinePragmaHelper(this.Parser.CodeFileVirtualPath.VirtualPathString, 912304);
			}
			topLevelStatements.Add(codeAssignStatement);
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x00165038 File Offset: 0x00163238
		protected override void BuildMiscClassMembers()
		{
			base.BuildMiscClassMembers();
			if (!this._designerMode)
			{
				this.BuildAutomaticEventHookup();
			}
			this.BuildApplicationInstanceProperty();
			if (this._designerMode)
			{
				this.GenerateDummyBindMethodsAtDesignTime();
			}
			this.BuildSourceDataTreeFromBuilder(this.Parser.RootBuilder, false, false, null);
			if (!this._designerMode)
			{
				this.BuildFrameworkInitializeMethod();
			}
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x00165090 File Offset: 0x00163290
		internal void BuildStronglyTypedProperty(string propertyName, Type propertyType)
		{
			if (this._usingVJSCompiler)
			{
				return;
			}
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes &= (MemberAttributes)(-61441);
			codeMemberProperty.Attributes &= (MemberAttributes)(-16);
			codeMemberProperty.Attributes |= (MemberAttributes)24594;
			codeMemberProperty.Name = propertyName;
			codeMemberProperty.Type = new CodeTypeReference(propertyType);
			CodePropertyReferenceExpression expression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), propertyName);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(propertyType, expression)));
			this._intermediateClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x00165128 File Offset: 0x00163328
		private void GenerateDummyBindMethodsAtDesignTime()
		{
			this.GenerateBindMethod(true);
			this.GenerateBindMethod(false);
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x00165138 File Offset: 0x00163338
		private void GenerateBindMethod(bool addFormatParameter)
		{
			if (this._sourceDataClass == null)
			{
				return;
			}
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "Bind";
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "expression"));
			if (addFormatParameter)
			{
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "format"));
			}
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(string));
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(string.Empty)));
			this._sourceDataClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x001651E4 File Offset: 0x001633E4
		private void BuildFrameworkInitializeMethod()
		{
			if (this._sourceDataClass == null)
			{
				return;
			}
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= (MemberAttributes)12292;
			codeMemberMethod.Name = "FrameworkInitialize";
			this.BuildFrameworkInitializeMethodContents(codeMemberMethod);
			if (!this._designerMode && this.Parser.CodeFileVirtualPath != null)
			{
				codeMemberMethod.LinePragma = BaseCodeDomTreeGenerator.CreateCodeLinePragmaHelper(this.Parser.CodeFileVirtualPath.VirtualPathString, 912304);
			}
			this._sourceDataClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x0016529C File Offset: 0x0016349C
		protected virtual void BuildFrameworkInitializeMethodContents(CodeMemberMethod method)
		{
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), method.Name, new CodeExpression[0]);
			method.Statements.Add(new CodeExpressionStatement(expression));
			if (this._stringResourceBuilder.HasStrings)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetStringResourcePointer", new CodeExpression[0]);
				codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(this._classTypeExpr, "__stringResource"));
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(0));
				method.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression2.Method.MethodName = "__BuildControlTree";
			codeMethodInvokeExpression2.Parameters.Add(new CodeThisReferenceExpression());
			method.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression2));
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x00165384 File Offset: 0x00163584
		private void BuildAutomaticEventHookup()
		{
			if (this._sourceDataClass == null)
			{
				return;
			}
			if (!this.Parser.FAutoEventWireup)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Attributes &= (MemberAttributes)(-61441);
				codeMemberProperty.Attributes &= (MemberAttributes)(-16);
				codeMemberProperty.Attributes |= (MemberAttributes)12292;
				codeMemberProperty.Name = "SupportAutoEvents";
				codeMemberProperty.Type = new CodeTypeReference(typeof(bool));
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
				this._sourceDataClass.Members.Add(codeMemberProperty);
				return;
			}
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x00165434 File Offset: 0x00163634
		private void BuildApplicationInstanceProperty()
		{
			Type globalAsaxType = BuildManager.GetGlobalAsaxType();
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Attributes &= (MemberAttributes)(-61441);
			codeMemberProperty.Attributes &= (MemberAttributes)(-16);
			codeMemberProperty.Attributes |= (MemberAttributes)12290;
			if (this._designerMode)
			{
				base.ApplyEditorBrowsableCustomAttribute(codeMemberProperty);
			}
			codeMemberProperty.Name = "ApplicationInstance";
			codeMemberProperty.Type = new CodeTypeReference(globalAsaxType);
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Context");
			codePropertyReferenceExpression = new CodePropertyReferenceExpression(codePropertyReferenceExpression, "ApplicationInstance");
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(globalAsaxType, codePropertyReferenceExpression)));
			this._intermediateClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x001654EC File Offset: 0x001636EC
		protected override void BuildDefaultConstructor()
		{
			base.BuildDefaultConstructor();
			if (BinaryCompatibility.Current.TargetsAtLeastFramework472 && !this._designerMode && this._sourceDataClass != null)
			{
				foreach (ConstructorInfo constructorInfo in this.Parser.BaseType.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
				{
					if (constructorInfo.GetParameters().Length != 0)
					{
						this.AddConstructorToSource(constructorInfo);
					}
				}
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x00165550 File Offset: 0x00163750
		private void AddConstructorToSource(ConstructorInfo ctor)
		{
			CodeConstructor codeConstructor = new CodeConstructor();
			base.AddDebuggerNonUserCodeAttribute(codeConstructor);
			codeConstructor.Attributes &= (MemberAttributes)(-61441);
			codeConstructor.Attributes |= MemberAttributes.Public;
			foreach (ParameterInfo parameterInfo in ctor.GetParameters())
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(parameterInfo.ParameterType, parameterInfo.Name);
				foreach (CustomAttributeData customAttributeData in parameterInfo.CustomAttributes)
				{
					List<CodeAttributeArgument> list = new List<CodeAttributeArgument>();
					foreach (CustomAttributeTypedArgument customAttributeTypedArgument in customAttributeData.ConstructorArguments)
					{
						list.Add(new CodeAttributeArgument(new CodePrimitiveExpression(customAttributeTypedArgument.Value)));
					}
					foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
					{
						list.Add(new CodeAttributeArgument(customAttributeNamedArgument.MemberName, new CodePrimitiveExpression(customAttributeNamedArgument.TypedValue.Value)));
					}
					CodeAttributeDeclaration value = new CodeAttributeDeclaration(new CodeTypeReference(customAttributeData.AttributeType), list.ToArray());
					codeParameterDeclarationExpression.CustomAttributes.Add(value);
				}
				if (parameterInfo.HasDefaultValue)
				{
					CodeAttributeDeclaration value2 = new CodeAttributeDeclaration(new CodeTypeReference(typeof(DefaultParameterValueAttribute)), new CodeAttributeArgument[]
					{
						new CodeAttributeArgument(new CodePrimitiveExpression(parameterInfo.DefaultValue))
					});
					codeParameterDeclarationExpression.CustomAttributes.Add(value2);
				}
				codeConstructor.Parameters.Add(codeParameterDeclarationExpression);
				codeConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(parameterInfo.Name));
			}
			codeConstructor.Statements.Add(BaseCodeDomTreeGenerator.CreateInitInvoke());
			this._sourceDataClass.Members.Add(codeConstructor);
		}

		// Token: 0x04003438 RID: 13368
		private const string stringResourcePointerName = "__stringResource";

		// Token: 0x04003439 RID: 13369
		private TemplateControlParser _tcParser;

		// Token: 0x0400343A RID: 13370
		private const string literalMemoryBlockName = "__literals";

		// Token: 0x0400343B RID: 13371
		internal const int badBaseClassLineMarker = 912304;
	}
}
