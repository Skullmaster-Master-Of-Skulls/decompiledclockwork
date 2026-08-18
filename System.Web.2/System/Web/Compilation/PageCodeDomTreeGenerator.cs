using System;
using System.CodeDom;
using System.Collections;
using System.Reflection;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000851 RID: 2129
	internal class PageCodeDomTreeGenerator : TemplateControlCodeDomTreeGenerator
	{
		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x00161CD9 File Offset: 0x0015FED9
		private PageParser Parser
		{
			get
			{
				return this._pageParser;
			}
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x00161CE1 File Offset: 0x0015FEE1
		internal PageCodeDomTreeGenerator(PageParser pageParser) : base(pageParser)
		{
			this._pageParser = pageParser;
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x00161CF4 File Offset: 0x0015FEF4
		protected override void GenerateInterfaces()
		{
			base.GenerateInterfaces();
			if (this.Parser.FRequiresSessionState)
			{
				this._intermediateClass.BaseTypes.Add(new CodeTypeReference(typeof(IRequiresSessionState)));
			}
			if (this.Parser.FReadOnlySessionState)
			{
				this._intermediateClass.BaseTypes.Add(new CodeTypeReference(typeof(IReadOnlySessionState)));
			}
			if (!this._designerMode && this._sourceDataClass != null && (this.Parser.AspCompatMode || this.Parser.AsyncMode))
			{
				this._sourceDataClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpAsyncHandler)));
			}
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x00161DAC File Offset: 0x0015FFAC
		protected override void BuildInitStatements(CodeStatementCollection trueStatements, CodeStatementCollection topLevelStatements)
		{
			base.BuildInitStatements(trueStatements, topLevelStatements);
			CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__fileDependencies");
			codeMemberField.Attributes |= MemberAttributes.Static;
			this._sourceDataClass.Members.Add(codeMemberField);
			topLevelStatements.Insert(0, new CodeVariableDeclarationStatement
			{
				Type = new CodeTypeReference(typeof(string[])),
				Name = "dependencies"
			});
			StringSet stringSet = new StringSet();
			stringSet.AddCollection(this.Parser.SourceDependencies);
			trueStatements.Add(new CodeAssignStatement
			{
				Left = new CodeVariableReferenceExpression("dependencies"),
				Right = new CodeArrayCreateExpression(typeof(string), stringSet.Count)
			});
			int num = 0;
			foreach (object obj in ((IEnumerable)stringSet))
			{
				string virtualPath = (string)obj;
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("dependencies"), new CodeExpression[]
				{
					new CodePrimitiveExpression(num++)
				});
				string value = UrlPath.MakeVirtualPathAppRelative(virtualPath);
				codeAssignStatement.Right = new CodePrimitiveExpression(value);
				trueStatements.Add(codeAssignStatement);
			}
			trueStatements.Add(new CodeAssignStatement
			{
				Left = new CodeFieldReferenceExpression(this._classTypeExpr, "__fileDependencies"),
				Right = new CodeMethodInvokeExpression
				{
					Method = 
					{
						TargetObject = new CodeThisReferenceExpression(),
						MethodName = "GetWrappedFileDependencies"
					},
					Parameters = 
					{
						new CodeVariableReferenceExpression("dependencies")
					}
				}
			});
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x00161F88 File Offset: 0x00160188
		protected override void BuildDefaultConstructor()
		{
			base.BuildDefaultConstructor();
			if (base.CompilParams.IncludeDebugInformation)
			{
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Server"), "ScriptTimeout");
				codeAssignStatement.Right = new CodePrimitiveExpression(30000000);
				base.InitMethod.Statements.Add(codeAssignStatement);
			}
			if (this.Parser.TransactionMode != 0)
			{
				base.InitMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TransactionMode"), new CodePrimitiveExpression(this.Parser.TransactionMode)));
			}
			if (this.Parser.AspCompatMode)
			{
				base.InitMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "AspCompatMode"), new CodePrimitiveExpression(this.Parser.AspCompatMode)));
			}
			if (this.Parser.AsyncMode)
			{
				base.InitMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "AsyncMode"), new CodePrimitiveExpression(this.Parser.AsyncMode)));
			}
			if (this.Parser.OutputCacheParameters != null)
			{
				OutputCacheParameters outputCacheParameters = this.Parser.OutputCacheParameters;
				if ((outputCacheParameters.CacheProfile != null && outputCacheParameters.CacheProfile.Length != 0) || outputCacheParameters.Duration != 0 || outputCacheParameters.Location == OutputCacheLocation.None)
				{
					CodeMemberField codeMemberField = new CodeMemberField(typeof(OutputCacheParameters), "__outputCacheSettings");
					codeMemberField.Attributes |= MemberAttributes.Static;
					codeMemberField.InitExpression = new CodePrimitiveExpression(null);
					this._sourceDataClass.Members.Add(codeMemberField);
					CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
					codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(this._classTypeExpr, "__outputCacheSettings"), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
					CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
					codeVariableDeclarationStatement.Type = new CodeTypeReference(typeof(OutputCacheParameters));
					codeVariableDeclarationStatement.Name = "outputCacheSettings";
					codeConditionStatement.TrueStatements.Insert(0, codeVariableDeclarationStatement);
					CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
					codeObjectCreateExpression.CreateType = new CodeTypeReference(typeof(OutputCacheParameters));
					CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("outputCacheSettings");
					CodeAssignStatement value = new CodeAssignStatement(codeVariableReferenceExpression, codeObjectCreateExpression);
					codeConditionStatement.TrueStatements.Add(value);
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.CacheProfile))
					{
						CodeAssignStatement value2 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "CacheProfile"), new CodePrimitiveExpression(outputCacheParameters.CacheProfile));
						codeConditionStatement.TrueStatements.Add(value2);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.Duration))
					{
						CodeAssignStatement value3 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Duration"), new CodePrimitiveExpression(outputCacheParameters.Duration));
						codeConditionStatement.TrueStatements.Add(value3);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.Enabled))
					{
						CodeAssignStatement value4 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Enabled"), new CodePrimitiveExpression(outputCacheParameters.Enabled));
						codeConditionStatement.TrueStatements.Add(value4);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.Location))
					{
						CodeAssignStatement value5 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "Location"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(OutputCacheLocation)), outputCacheParameters.Location.ToString()));
						codeConditionStatement.TrueStatements.Add(value5);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.NoStore))
					{
						CodeAssignStatement value6 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "NoStore"), new CodePrimitiveExpression(outputCacheParameters.NoStore));
						codeConditionStatement.TrueStatements.Add(value6);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.SqlDependency))
					{
						CodeAssignStatement value7 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "SqlDependency"), new CodePrimitiveExpression(outputCacheParameters.SqlDependency));
						codeConditionStatement.TrueStatements.Add(value7);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.VaryByControl))
					{
						CodeAssignStatement value8 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "VaryByControl"), new CodePrimitiveExpression(outputCacheParameters.VaryByControl));
						codeConditionStatement.TrueStatements.Add(value8);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.VaryByCustom))
					{
						CodeAssignStatement value9 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "VaryByCustom"), new CodePrimitiveExpression(outputCacheParameters.VaryByCustom));
						codeConditionStatement.TrueStatements.Add(value9);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.VaryByContentEncoding))
					{
						CodeAssignStatement value10 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "VaryByContentEncoding"), new CodePrimitiveExpression(outputCacheParameters.VaryByContentEncoding));
						codeConditionStatement.TrueStatements.Add(value10);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.VaryByHeader))
					{
						CodeAssignStatement value11 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "VaryByHeader"), new CodePrimitiveExpression(outputCacheParameters.VaryByHeader));
						codeConditionStatement.TrueStatements.Add(value11);
					}
					if (outputCacheParameters.IsParameterSet(OutputCacheParameter.VaryByParam))
					{
						CodeAssignStatement value12 = new CodeAssignStatement(new CodePropertyReferenceExpression(codeVariableReferenceExpression, "VaryByParam"), new CodePrimitiveExpression(outputCacheParameters.VaryByParam));
						codeConditionStatement.TrueStatements.Add(value12);
					}
					CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(this._classTypeExpr, "__outputCacheSettings");
					CodeAssignStatement value13 = new CodeAssignStatement(left, codeVariableReferenceExpression);
					codeConditionStatement.TrueStatements.Add(value13);
					base.InitMethod.Statements.Add(codeConditionStatement);
				}
			}
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x001624B4 File Offset: 0x001606B4
		protected override void BuildMiscClassMembers()
		{
			base.BuildMiscClassMembers();
			if (!this._designerMode && this._sourceDataClass != null)
			{
				this.BuildGetTypeHashCodeMethod();
				if (this.Parser.AspCompatMode)
				{
					this.BuildAspCompatMethods();
				}
				if (this.Parser.AsyncMode)
				{
					this.BuildAsyncPageMethods();
				}
				this.BuildProcessRequestOverride();
			}
			if (this.Parser.PreviousPageType != null)
			{
				base.BuildStronglyTypedProperty("PreviousPage", this.Parser.PreviousPageType);
			}
			if (this.Parser.MasterPageType != null)
			{
				base.BuildStronglyTypedProperty("Master", this.Parser.MasterPageType);
			}
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x0016255C File Offset: 0x0016075C
		private void BuildGetTypeHashCodeMethod()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "GetTypeHashCode";
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(int));
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= (MemberAttributes)24580;
			this._sourceDataClass.Members.Add(codeMemberMethod);
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(this.Parser.TypeHashCode)));
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x00162601 File Offset: 0x00160801
		internal override CodeExpression BuildPagePropertyReferenceExpression()
		{
			return new CodeThisReferenceExpression();
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x00162608 File Offset: 0x00160808
		protected override void BuildFrameworkInitializeMethodContents(CodeMemberMethod method)
		{
			if (this.Parser.StyleSheetTheme != null)
			{
				CodeExpression right = new CodePrimitiveExpression(this.Parser.StyleSheetTheme);
				CodeExpression left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "StyleSheetTheme");
				CodeAssignStatement value = new CodeAssignStatement(left, right);
				method.Statements.Add(value);
			}
			base.BuildFrameworkInitializeMethodContents(method);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "AddWrappedFileDependencies";
			codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(this._classTypeExpr, "__fileDependencies"));
			method.Statements.Add(codeMethodInvokeExpression);
			if (this.Parser.OutputCacheParameters != null)
			{
				OutputCacheParameters outputCacheParameters = this.Parser.OutputCacheParameters;
				if ((outputCacheParameters.CacheProfile != null && outputCacheParameters.CacheProfile.Length != 0) || outputCacheParameters.Duration != 0 || outputCacheParameters.Location == OutputCacheLocation.None)
				{
					CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
					codeMethodInvokeExpression2.Method.TargetObject = new CodeThisReferenceExpression();
					codeMethodInvokeExpression2.Method.MethodName = "InitOutputCache";
					codeMethodInvokeExpression2.Parameters.Add(new CodeFieldReferenceExpression(this._classTypeExpr, "__outputCacheSettings"));
					method.Statements.Add(codeMethodInvokeExpression2);
				}
			}
			if (this.Parser.TraceEnabled != TraceEnable.Default)
			{
				method.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TraceEnabled"), new CodePrimitiveExpression(this.Parser.TraceEnabled == TraceEnable.Enable)));
			}
			if (this.Parser.TraceMode != TraceMode.Default)
			{
				method.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TraceModeValue"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(TraceMode)), this.Parser.TraceMode.ToString())));
			}
			if (this.Parser.ValidateRequest)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression3 = new CodeMethodInvokeExpression();
				codeMethodInvokeExpression3.Method.TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Request");
				codeMethodInvokeExpression3.Method.MethodName = "ValidateInput";
				method.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression3));
				return;
			}
			if (MultiTargetingUtil.TargetFrameworkVersion >= VersionUtil.Framework45)
			{
				CodePropertyReferenceExpression left2 = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "ValidateRequestMode");
				CodeFieldReferenceExpression right2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ValidateRequestMode)), "Disabled");
				CodeAssignStatement value2 = new CodeAssignStatement(left2, right2);
				method.Statements.Add(value2);
			}
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x00162894 File Offset: 0x00160A94
		private void BuildAspCompatMethods()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "BeginProcessRequest";
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= MemberAttributes.Public;
			codeMemberMethod.ImplementationTypes.Add(new CodeTypeReference(typeof(IHttpAsyncHandler)));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(HttpContext), "context"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(AsyncCallback), "cb"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "data"));
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IAsyncResult));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "AspCompatBeginProcessRequest";
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("cb"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("data"));
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
			this._sourceDataClass.Members.Add(codeMemberMethod);
			codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "EndProcessRequest";
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= MemberAttributes.Public;
			codeMemberMethod.ImplementationTypes.Add(typeof(IHttpAsyncHandler));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IAsyncResult), "ar"));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression2.Method.MethodName = "AspCompatEndProcessRequest";
			codeMethodInvokeExpression2.Parameters.Add(new CodeArgumentReferenceExpression("ar"));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression2);
			this._sourceDataClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x00162AE4 File Offset: 0x00160CE4
		private void BuildAsyncPageMethods()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "BeginProcessRequest";
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= MemberAttributes.Public;
			codeMemberMethod.ImplementationTypes.Add(new CodeTypeReference(typeof(IHttpAsyncHandler)));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(HttpContext), "context"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(AsyncCallback), "cb"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "data"));
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IAsyncResult));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "AsyncPageBeginProcessRequest";
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("cb"));
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("data"));
			codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
			this._sourceDataClass.Members.Add(codeMemberMethod);
			codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "EndProcessRequest";
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			codeMemberMethod.Attributes |= MemberAttributes.Public;
			codeMemberMethod.ImplementationTypes.Add(typeof(IHttpAsyncHandler));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IAsyncResult), "ar"));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression2.Method.MethodName = "AsyncPageEndProcessRequest";
			codeMethodInvokeExpression2.Parameters.Add(new CodeArgumentReferenceExpression("ar"));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression2);
			this._sourceDataClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x00162D34 File Offset: 0x00160F34
		private void BuildProcessRequestOverride()
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = "ProcessRequest";
			codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
			codeMemberMethod.Attributes &= (MemberAttributes)(-16);
			MethodInfo methodInfo = null;
			if (this.Parser.BaseType != typeof(Page))
			{
				methodInfo = this.Parser.BaseType.GetMethod("ProcessRequest", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
				{
					typeof(HttpContext)
				}, null);
			}
			this._sourceDataClass.BaseTypes.Add(new CodeTypeReference(typeof(IHttpHandler)));
			if (methodInfo != null && methodInfo.DeclaringType != typeof(Page))
			{
				codeMemberMethod.Attributes |= (MemberAttributes)24592;
			}
			else
			{
				codeMemberMethod.Attributes |= (MemberAttributes)24580;
			}
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(HttpContext), "context"));
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeBaseReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "ProcessRequest";
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("context"));
			codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
			this._sourceDataClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x04003410 RID: 13328
		private PageParser _pageParser;

		// Token: 0x04003411 RID: 13329
		private const string fileDependenciesName = "__fileDependencies";

		// Token: 0x04003412 RID: 13330
		private const string dependenciesLocalName = "dependencies";

		// Token: 0x04003413 RID: 13331
		private const string outputCacheSettingsLocalName = "outputCacheSettings";

		// Token: 0x04003414 RID: 13332
		private const string _previousPagePropertyName = "PreviousPage";

		// Token: 0x04003415 RID: 13333
		private const string _masterPropertyName = "Master";

		// Token: 0x04003416 RID: 13334
		private const string _styleSheetThemePropertyName = "StyleSheetTheme";

		// Token: 0x04003417 RID: 13335
		private const string outputCacheSettingsFieldName = "__outputCacheSettings";

		// Token: 0x04003418 RID: 13336
		internal const int DebugScriptTimeout = 30000000;
	}
}
