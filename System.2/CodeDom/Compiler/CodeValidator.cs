using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000674 RID: 1652
	internal class CodeValidator
	{
		// Token: 0x06003C75 RID: 15477 RVA: 0x000F9960 File Offset: 0x000F7B60
		internal void ValidateIdentifiers(CodeObject e)
		{
			if (e is CodeCompileUnit)
			{
				this.ValidateCodeCompileUnit((CodeCompileUnit)e);
				return;
			}
			if (e is CodeComment)
			{
				this.ValidateComment((CodeComment)e);
				return;
			}
			if (e is CodeExpression)
			{
				this.ValidateExpression((CodeExpression)e);
				return;
			}
			if (e is CodeNamespace)
			{
				this.ValidateNamespace((CodeNamespace)e);
				return;
			}
			if (e is CodeNamespaceImport)
			{
				CodeValidator.ValidateNamespaceImport((CodeNamespaceImport)e);
				return;
			}
			if (e is CodeStatement)
			{
				this.ValidateStatement((CodeStatement)e);
				return;
			}
			if (e is CodeTypeMember)
			{
				this.ValidateTypeMember((CodeTypeMember)e);
				return;
			}
			if (e is CodeTypeReference)
			{
				CodeValidator.ValidateTypeReference((CodeTypeReference)e);
				return;
			}
			if (e is CodeDirective)
			{
				CodeValidator.ValidateCodeDirective((CodeDirective)e);
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000F9A50 File Offset: 0x000F7C50
		private void ValidateTypeMember(CodeTypeMember e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeMemberEvent)
			{
				this.ValidateEvent((CodeMemberEvent)e);
				return;
			}
			if (e is CodeMemberField)
			{
				this.ValidateField((CodeMemberField)e);
				return;
			}
			if (e is CodeMemberMethod)
			{
				this.ValidateMemberMethod((CodeMemberMethod)e);
				return;
			}
			if (e is CodeMemberProperty)
			{
				this.ValidateProperty((CodeMemberProperty)e);
				return;
			}
			if (e is CodeSnippetTypeMember)
			{
				this.ValidateSnippetMember((CodeSnippetTypeMember)e);
				return;
			}
			if (e is CodeTypeDeclaration)
			{
				this.ValidateTypeDeclaration((CodeTypeDeclaration)e);
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000F9B3C File Offset: 0x000F7D3C
		private void ValidateCodeCompileUnit(CodeCompileUnit e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeSnippetCompileUnit)
			{
				this.ValidateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				return;
			}
			this.ValidateCompileUnitStart(e);
			this.ValidateNamespaces(e);
			this.ValidateCompileUnitEnd(e);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000F9B89 File Offset: 0x000F7D89
		private void ValidateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000F9B9F File Offset: 0x000F7D9F
		private void ValidateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.AssemblyCustomAttributes);
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000F9BBB File Offset: 0x000F7DBB
		private void ValidateCompileUnitEnd(CodeCompileUnit e)
		{
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000F9BC0 File Offset: 0x000F7DC0
		private void ValidateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace e2 = (CodeNamespace)obj;
				this.ValidateNamespace(e2);
			}
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x000F9C1C File Offset: 0x000F7E1C
		private void ValidateNamespace(CodeNamespace e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateNamespaceStart(e);
			this.ValidateNamespaceImports(e);
			this.ValidateTypes(e);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x000F9C3E File Offset: 0x000F7E3E
		private static void ValidateNamespaceStart(CodeNamespace e)
		{
			if (e.Name != null && e.Name.Length > 0)
			{
				CodeValidator.ValidateTypeName(e, "Name", e.Name);
			}
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x000F9C68 File Offset: 0x000F7E68
		private void ValidateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.ValidateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				CodeValidator.ValidateNamespaceImport(codeNamespaceImport);
			}
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x000F9CB1 File Offset: 0x000F7EB1
		private static void ValidateNamespaceImport(CodeNamespaceImport e)
		{
			CodeValidator.ValidateTypeName(e, "Namespace", e.Namespace);
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000F9CC4 File Offset: 0x000F7EC4
		private void ValidateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				CodeValidator.ValidateTypeName(codeAttributeDeclaration, "Name", codeAttributeDeclaration.Name);
				CodeValidator.ValidateTypeReference(codeAttributeDeclaration.AttributeType);
				foreach (object obj2 in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument arg = (CodeAttributeArgument)obj2;
					this.ValidateAttributeArgument(arg);
				}
			}
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x000F9D64 File Offset: 0x000F7F64
		private void ValidateAttributeArgument(CodeAttributeArgument arg)
		{
			if (arg.Name != null && arg.Name.Length > 0)
			{
				CodeValidator.ValidateIdentifier(arg, "Name", arg.Name);
			}
			this.ValidateExpression(arg.Value);
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x000F9D9C File Offset: 0x000F7F9C
		private void ValidateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration e2 = (CodeTypeDeclaration)obj;
				this.ValidateTypeDeclaration(e2);
			}
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000F9DF8 File Offset: 0x000F7FF8
		private void ValidateTypeDeclaration(CodeTypeDeclaration e)
		{
			CodeTypeDeclaration codeTypeDeclaration = this.currentClass;
			this.currentClass = e;
			this.ValidateTypeStart(e);
			this.ValidateTypeParameters(e.TypeParameters);
			this.ValidateTypeMembers(e);
			CodeValidator.ValidateTypeReferences(e.BaseTypes);
			this.currentClass = codeTypeDeclaration;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x000F9E40 File Offset: 0x000F8040
		private void ValidateTypeMembers(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember e2 = (CodeTypeMember)obj;
				this.ValidateTypeMember(e2);
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000F9E9C File Offset: 0x000F809C
		private void ValidateTypeParameters(CodeTypeParameterCollection parameters)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				this.ValidateTypeParameter(parameters[i]);
			}
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x000F9EC7 File Offset: 0x000F80C7
		private void ValidateTypeParameter(CodeTypeParameter e)
		{
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			CodeValidator.ValidateTypeReferences(e.Constraints);
			this.ValidateAttributes(e.CustomAttributes);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000F9EF4 File Offset: 0x000F80F4
		private void ValidateField(CodeMemberField e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (!this.IsCurrentEnum)
			{
				CodeValidator.ValidateTypeReference(e.Type);
			}
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000F9F54 File Offset: 0x000F8154
		private void ValidateConstructor(CodeConstructor e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			this.ValidateParameters(e.Parameters);
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			if (baseConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(baseConstructorArgs);
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(chainedConstructorArgs);
			}
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x000F9FC4 File Offset: 0x000F81C4
		private void ValidateProperty(CodeMemberProperty e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			if (e.Parameters.Count > 0 && string.Compare(e.Name, "Item", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.ValidateParameters(e.Parameters);
			}
			else
			{
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			if (e.HasGet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.GetStatements);
			}
			if (e.HasSet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.SetStatements);
			}
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000FA0AC File Offset: 0x000F82AC
		private void ValidateMemberMethod(CodeMemberMethod e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			this.ValidateTypeParameters(e.TypeParameters);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e is CodeEntryPointMethod)
			{
				this.ValidateStatements(((CodeEntryPointMethod)e).Statements);
				return;
			}
			if (e is CodeConstructor)
			{
				this.ValidateConstructor((CodeConstructor)e);
				return;
			}
			if (e is CodeTypeConstructor)
			{
				this.ValidateTypeConstructor((CodeTypeConstructor)e);
				return;
			}
			this.ValidateMethod(e);
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000FA13B File Offset: 0x000F833B
		private void ValidateTypeConstructor(CodeTypeConstructor e)
		{
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000FA14C File Offset: 0x000F834C
		private void ValidateMethod(CodeMemberMethod e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.ReturnTypeCustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.ReturnType);
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			this.ValidateParameters(e.Parameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.Statements);
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000FA1E8 File Offset: 0x000F83E8
		private void ValidateSnippetMember(CodeSnippetTypeMember e)
		{
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x000FA1EC File Offset: 0x000F83EC
		private void ValidateTypeStart(CodeTypeDeclaration e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (this.IsCurrentDelegate)
			{
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				CodeValidator.ValidateTypeReference(codeTypeDelegate.ReturnType);
				this.ValidateParameters(codeTypeDelegate.Parameters);
				return;
			}
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference e2 = (CodeTypeReference)obj;
				CodeValidator.ValidateTypeReference(e2);
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000FA2A4 File Offset: 0x000F84A4
		private void ValidateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement e2 = (CodeCommentStatement)obj;
				this.ValidateCommentStatement(e2);
			}
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000FA2F8 File Offset: 0x000F84F8
		private void ValidateCommentStatement(CodeCommentStatement e)
		{
			this.ValidateComment(e.Comment);
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000FA306 File Offset: 0x000F8506
		private void ValidateComment(CodeComment e)
		{
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000FA308 File Offset: 0x000F8508
		private void ValidateStatement(CodeStatement e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeCommentStatement)
			{
				this.ValidateCommentStatement((CodeCommentStatement)e);
				return;
			}
			if (e is CodeMethodReturnStatement)
			{
				this.ValidateMethodReturnStatement((CodeMethodReturnStatement)e);
				return;
			}
			if (e is CodeConditionStatement)
			{
				this.ValidateConditionStatement((CodeConditionStatement)e);
				return;
			}
			if (e is CodeTryCatchFinallyStatement)
			{
				this.ValidateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
				return;
			}
			if (e is CodeAssignStatement)
			{
				this.ValidateAssignStatement((CodeAssignStatement)e);
				return;
			}
			if (e is CodeExpressionStatement)
			{
				this.ValidateExpressionStatement((CodeExpressionStatement)e);
				return;
			}
			if (e is CodeIterationStatement)
			{
				this.ValidateIterationStatement((CodeIterationStatement)e);
				return;
			}
			if (e is CodeThrowExceptionStatement)
			{
				this.ValidateThrowExceptionStatement((CodeThrowExceptionStatement)e);
				return;
			}
			if (e is CodeSnippetStatement)
			{
				this.ValidateSnippetStatement((CodeSnippetStatement)e);
				return;
			}
			if (e is CodeVariableDeclarationStatement)
			{
				this.ValidateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
				return;
			}
			if (e is CodeAttachEventStatement)
			{
				this.ValidateAttachEventStatement((CodeAttachEventStatement)e);
				return;
			}
			if (e is CodeRemoveEventStatement)
			{
				this.ValidateRemoveEventStatement((CodeRemoveEventStatement)e);
				return;
			}
			if (e is CodeGotoStatement)
			{
				CodeValidator.ValidateGotoStatement((CodeGotoStatement)e);
				return;
			}
			if (e is CodeLabeledStatement)
			{
				this.ValidateLabeledStatement((CodeLabeledStatement)e);
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000FA478 File Offset: 0x000F8678
		private void ValidateStatements(CodeStatementCollection stms)
		{
			foreach (object obj in stms)
			{
				this.ValidateStatement((CodeStatement)obj);
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000FA4A7 File Offset: 0x000F86A7
		private void ValidateExpressionStatement(CodeExpressionStatement e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000FA4B5 File Offset: 0x000F86B5
		private void ValidateIterationStatement(CodeIterationStatement e)
		{
			this.ValidateStatement(e.InitStatement);
			this.ValidateExpression(e.TestExpression);
			this.ValidateStatement(e.IncrementStatement);
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000FA4E7 File Offset: 0x000F86E7
		private void ValidateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			if (e.ToThrow != null)
			{
				this.ValidateExpression(e.ToThrow);
			}
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000FA4FD File Offset: 0x000F86FD
		private void ValidateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			if (e.Expression != null)
			{
				this.ValidateExpression(e.Expression);
			}
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000FA514 File Offset: 0x000F8714
		private void ValidateConditionStatement(CodeConditionStatement e)
		{
			this.ValidateExpression(e.Condition);
			this.ValidateStatements(e.TrueStatements);
			CodeStatementCollection falseStatements = e.FalseStatements;
			if (falseStatements.Count > 0)
			{
				this.ValidateStatements(e.FalseStatements);
			}
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000FA558 File Offset: 0x000F8758
		private void ValidateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.ValidateStatements(e.TryStatements);
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					CodeValidator.ValidateTypeReference(codeCatchClause.CatchExceptionType);
					CodeValidator.ValidateIdentifier(codeCatchClause, "LocalName", codeCatchClause.LocalName);
					this.ValidateStatements(codeCatchClause.Statements);
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.ValidateStatements(finallyStatements);
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000FA5DD File Offset: 0x000F87DD
		private void ValidateAssignStatement(CodeAssignStatement e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000FA5F7 File Offset: 0x000F87F7
		private void ValidateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000FA611 File Offset: 0x000F8811
		private void ValidateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000FA62B File Offset: 0x000F882B
		private static void ValidateGotoStatement(CodeGotoStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000FA63E File Offset: 0x000F883E
		private void ValidateLabeledStatement(CodeLabeledStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
			if (e.Statement != null)
			{
				this.ValidateStatement(e.Statement);
			}
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000FA665 File Offset: 0x000F8865
		private void ValidateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000FA697 File Offset: 0x000F8897
		private void ValidateLinePragmaStart(CodeLinePragma e)
		{
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000FA69C File Offset: 0x000F889C
		private void ValidateEvent(CodeMemberEvent e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.Type);
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000FA6F4 File Offset: 0x000F88F4
		private void ValidateParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression e = (CodeParameterDeclarationExpression)obj;
				this.ValidateParameterDeclarationExpression(e);
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000FA725 File Offset: 0x000F8925
		private void ValidateSnippetStatement(CodeSnippetStatement e)
		{
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000FA728 File Offset: 0x000F8928
		private void ValidateExpressionList(CodeExpressionCollection expressions)
		{
			foreach (object obj in expressions)
			{
				this.ValidateExpression((CodeExpression)obj);
			}
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000FA758 File Offset: 0x000F8958
		private static void ValidateTypeReference(CodeTypeReference e)
		{
			string baseType = e.BaseType;
			CodeValidator.ValidateTypeName(e, "BaseType", baseType);
			CodeValidator.ValidateArity(e);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000FA78C File Offset: 0x000F898C
		private static void ValidateTypeReferences(CodeTypeReferenceCollection refs)
		{
			for (int i = 0; i < refs.Count; i++)
			{
				CodeValidator.ValidateTypeReference(refs[i]);
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000FA7B8 File Offset: 0x000F89B8
		private static void ValidateArity(CodeTypeReference e)
		{
			string baseType = e.BaseType;
			int num = 0;
			for (int i = 0; i < baseType.Length; i++)
			{
				if (baseType[i] == '`')
				{
					i++;
					int num2 = 0;
					while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
					{
						num2 = num2 * 10 + (int)(baseType[i] - '0');
						i++;
					}
					num += num2;
				}
			}
			if (num != e.TypeArguments.Count && e.TypeArguments.Count != 0)
			{
				throw new ArgumentException(SR.GetString("ArityDoesntMatch", new object[]
				{
					baseType,
					e.TypeArguments.Count
				}));
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000FA874 File Offset: 0x000F8A74
		private static void ValidateTypeName(object e, string propertyName, string typeName)
		{
			if (!CodeGenerator.IsValidLanguageIndependentTypeName(typeName))
			{
				string @string = SR.GetString("InvalidTypeName", new object[]
				{
					typeName,
					propertyName,
					e.GetType().FullName
				});
				throw new ArgumentException(@string, "typeName");
			}
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000FA8BC File Offset: 0x000F8ABC
		private static void ValidateIdentifier(object e, string propertyName, string identifier)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(identifier))
			{
				string @string = SR.GetString("InvalidLanguageIdentifier", new object[]
				{
					identifier,
					propertyName,
					e.GetType().FullName
				});
				throw new ArgumentException(@string, "identifier");
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000FA904 File Offset: 0x000F8B04
		private void ValidateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.ValidateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.ValidateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.ValidateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.ValidateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				CodeValidator.ValidateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.ValidateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.ValidateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				CodeValidator.ValidateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				CodeValidator.ValidateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.ValidateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.ValidateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.ValidateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.ValidateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.ValidateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.ValidateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.ValidateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.ValidateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.ValidateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.ValidateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.ValidatePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.ValidatePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.ValidatePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.ValidateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				CodeValidator.ValidateTypeReference(((CodeTypeReferenceExpression)e).Type);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				CodeValidator.ValidateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000FAB54 File Offset: 0x000F8D54
		private void ValidateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.ValidateExpressionList(initializers);
				return;
			}
			if (e.SizeExpression != null)
			{
				this.ValidateExpression(e.SizeExpression);
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000FAB98 File Offset: 0x000F8D98
		private void ValidateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000FAB9A File Offset: 0x000F8D9A
		private void ValidateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000FABB4 File Offset: 0x000F8DB4
		private void ValidateCastExpression(CodeCastExpression e)
		{
			CodeValidator.ValidateTypeReference(e.TargetType);
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000FABCD File Offset: 0x000F8DCD
		private static void ValidateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000FABDA File Offset: 0x000F8DDA
		private void ValidateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.DelegateType);
			this.ValidateExpression(e.TargetObject);
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000FAC04 File Offset: 0x000F8E04
		private void ValidateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "FieldName", e.FieldName);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000FAC2B File Offset: 0x000F8E2B
		private static void ValidateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "ParameterName", e.ParameterName);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000FAC3E File Offset: 0x000F8E3E
		private static void ValidateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "VariableName", e.VariableName);
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000FAC54 File Offset: 0x000F8E54
		private void ValidateIndexerExpression(CodeIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression e2 = (CodeExpression)obj;
				this.ValidateExpression(e2);
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000FACBC File Offset: 0x000F8EBC
		private void ValidateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression e2 = (CodeExpression)obj;
				this.ValidateExpression(e2);
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000FAD24 File Offset: 0x000F8F24
		private void ValidateSnippetExpression(CodeSnippetExpression e)
		{
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000FAD26 File Offset: 0x000F8F26
		private void ValidateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.ValidateMethodReferenceExpression(e.Method);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000FAD40 File Offset: 0x000F8F40
		private void ValidateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000FAD72 File Offset: 0x000F8F72
		private void ValidateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "EventName", e.EventName);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000FAD99 File Offset: 0x000F8F99
		private void ValidateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000FADBB File Offset: 0x000F8FBB
		private void ValidateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x000FADD4 File Offset: 0x000F8FD4
		private void ValidateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x000FAE0C File Offset: 0x000F900C
		private void ValidateDirectionExpression(CodeDirectionExpression e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000FAE1A File Offset: 0x000F901A
		private void ValidatePrimitiveExpression(CodePrimitiveExpression e)
		{
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x000FAE1C File Offset: 0x000F901C
		private void ValidatePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "PropertyName", e.PropertyName);
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000FAE43 File Offset: 0x000F9043
		private void ValidatePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x000FAE45 File Offset: 0x000F9045
		private void ValidateThisReferenceExpression(CodeThisReferenceExpression e)
		{
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000FAE47 File Offset: 0x000F9047
		private static void ValidateTypeOfExpression(CodeTypeOfExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x000FAE54 File Offset: 0x000F9054
		private static void ValidateCodeDirectives(CodeDirectiveCollection e)
		{
			for (int i = 0; i < e.Count; i++)
			{
				CodeValidator.ValidateCodeDirective(e[i]);
			}
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000FAE80 File Offset: 0x000F9080
		private static void ValidateCodeDirective(CodeDirective e)
		{
			if (e is CodeChecksumPragma)
			{
				CodeValidator.ValidateChecksumPragma((CodeChecksumPragma)e);
				return;
			}
			if (e is CodeRegionDirective)
			{
				CodeValidator.ValidateRegionDirective((CodeRegionDirective)e);
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[]
			{
				e.GetType().FullName
			}), "e");
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000FAEDD File Offset: 0x000F90DD
		private static void ValidateChecksumPragma(CodeChecksumPragma e)
		{
			if (e.FileName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				throw new ArgumentException(SR.GetString("InvalidPathCharsInChecksum", new object[]
				{
					e.FileName
				}));
			}
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x000FAF11 File Offset: 0x000F9111
		private static void ValidateRegionDirective(CodeRegionDirective e)
		{
			if (e.RegionText.IndexOfAny(CodeValidator.newLineChars) != -1)
			{
				throw new ArgumentException(SR.GetString("InvalidRegion", new object[]
				{
					e.RegionText
				}));
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000FAF45 File Offset: 0x000F9145
		private bool IsCurrentInterface
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsInterface;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000FAF69 File Offset: 0x000F9169
		private bool IsCurrentEnum
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsEnum;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000FAF8D File Offset: 0x000F918D
		private bool IsCurrentDelegate
		{
			get
			{
				return this.currentClass != null && this.currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x04002C76 RID: 11382
		private static readonly char[] newLineChars = new char[]
		{
			'\r',
			'\n',
			'\u2028',
			'\u2029',
			'\u0085'
		};

		// Token: 0x04002C77 RID: 11383
		private CodeTypeDeclaration currentClass;
	}
}
