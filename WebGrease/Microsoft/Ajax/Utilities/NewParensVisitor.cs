using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B8 RID: 184
	internal class NewParensVisitor : IVisitor
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000379B0 File Offset: 0x00035BB0
		public static bool NeedsParens(AstNode expression, bool outerHasNoArguments)
		{
			NewParensVisitor newParensVisitor = new NewParensVisitor(outerHasNoArguments);
			expression.Accept(newParensVisitor);
			return newParensVisitor.m_needsParens;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000379D1 File Offset: 0x00035BD1
		private NewParensVisitor(bool outerHasNoArguments)
		{
			this.m_outerHasNoArguments = outerHasNoArguments;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000379E0 File Offset: 0x00035BE0
		public void Visit(ArrayLiteral node)
		{
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000379E2 File Offset: 0x00035BE2
		public void Visit(AspNetBlockNode node)
		{
			this.m_needsParens = true;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000379EB File Offset: 0x00035BEB
		public void Visit(BinaryOperator node)
		{
			this.m_needsParens = true;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000379F4 File Offset: 0x00035BF4
		public void Visit(BindingIdentifier node)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000379F8 File Offset: 0x00035BF8
		public void Visit(CallNode node)
		{
			if (node != null)
			{
				if (node.InBrackets)
				{
					node.Function.Accept(this);
					return;
				}
				if (!node.IsConstructor)
				{
					this.m_needsParens = true;
					return;
				}
				if (node.Arguments == null || node.Arguments.Count == 0)
				{
					this.m_needsParens = !this.m_outerHasNoArguments;
					return;
				}
			}
			else
			{
				this.m_needsParens = true;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00037A59 File Offset: 0x00035C59
		public void Visit(ClassNode node)
		{
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00037A5B File Offset: 0x00035C5B
		public void Visit(ComprehensionNode node)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00037A60 File Offset: 0x00035C60
		public void Visit(ConditionalCompilationComment node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					astNode.Accept(this);
					if (this.m_needsParens)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00037AB8 File Offset: 0x00035CB8
		public void Visit(ConditionalCompilationElse node)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00037ABA File Offset: 0x00035CBA
		public void Visit(ConditionalCompilationElseIf node)
		{
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00037ABC File Offset: 0x00035CBC
		public void Visit(ConditionalCompilationEnd node)
		{
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00037ABE File Offset: 0x00035CBE
		public void Visit(ConditionalCompilationIf node)
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00037AC0 File Offset: 0x00035CC0
		public void Visit(ConditionalCompilationOn node)
		{
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00037AC2 File Offset: 0x00035CC2
		public void Visit(ConditionalCompilationSet node)
		{
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00037AC4 File Offset: 0x00035CC4
		public void Visit(Conditional node)
		{
			this.m_needsParens = true;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00037ACD File Offset: 0x00035CCD
		public void Visit(ConstantWrapper node)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00037ACF File Offset: 0x00035CCF
		public void Visit(ConstantWrapperPP node)
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00037AD1 File Offset: 0x00035CD1
		public void Visit(CustomNode node)
		{
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00037AD3 File Offset: 0x00035CD3
		public void Visit(FunctionObject node)
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00037AD5 File Offset: 0x00035CD5
		public virtual void Visit(GroupingOperator node)
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00037AD7 File Offset: 0x00035CD7
		public void Visit(ImportantComment node)
		{
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00037AD9 File Offset: 0x00035CD9
		public void Visit(Lookup node)
		{
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00037ADB File Offset: 0x00035CDB
		public void Visit(Member node)
		{
			if (node != null)
			{
				node.Root.Accept(this);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00037AEC File Offset: 0x00035CEC
		public void Visit(ObjectLiteral node)
		{
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00037AEE File Offset: 0x00035CEE
		public void Visit(ParameterDeclaration node)
		{
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00037AF0 File Offset: 0x00035CF0
		public void Visit(RegExpLiteral node)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00037AF2 File Offset: 0x00035CF2
		public void Visit(TemplateLiteral node)
		{
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00037AF4 File Offset: 0x00035CF4
		public void Visit(ThisLiteral node)
		{
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00037AF6 File Offset: 0x00035CF6
		public void Visit(UnaryOperator node)
		{
			this.m_needsParens = true;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00037AFF File Offset: 0x00035CFF
		public void Visit(AstNodeList node)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00037B01 File Offset: 0x00035D01
		public void Visit(GetterSetter node)
		{
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00037B03 File Offset: 0x00035D03
		public void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00037B05 File Offset: 0x00035D05
		public void Visit(ObjectLiteralProperty node)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00037B07 File Offset: 0x00035D07
		public void Visit(Block node)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00037B09 File Offset: 0x00035D09
		public void Visit(Break node)
		{
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00037B0B File Offset: 0x00035D0B
		public void Visit(ComprehensionForClause node)
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00037B0D File Offset: 0x00035D0D
		public void Visit(ComprehensionIfClause node)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00037B0F File Offset: 0x00035D0F
		public void Visit(ConstStatement node)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00037B11 File Offset: 0x00035D11
		public void Visit(ContinueNode node)
		{
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00037B13 File Offset: 0x00035D13
		public void Visit(DebuggerNode node)
		{
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00037B15 File Offset: 0x00035D15
		public void Visit(DirectivePrologue node)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00037B17 File Offset: 0x00035D17
		public void Visit(DoWhile node)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00037B19 File Offset: 0x00035D19
		public void Visit(EmptyStatement node)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00037B1B File Offset: 0x00035D1B
		public void Visit(ExportNode node)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00037B1D File Offset: 0x00035D1D
		public void Visit(ForIn node)
		{
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00037B1F File Offset: 0x00035D1F
		public void Visit(ForNode node)
		{
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00037B21 File Offset: 0x00035D21
		public void Visit(IfNode node)
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00037B23 File Offset: 0x00035D23
		public void Visit(ImportExportSpecifier node)
		{
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00037B25 File Offset: 0x00035D25
		public void Visit(ImportNode node)
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00037B27 File Offset: 0x00035D27
		public void Visit(InitializerNode node)
		{
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00037B29 File Offset: 0x00035D29
		public void Visit(LabeledStatement node)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00037B2B File Offset: 0x00035D2B
		public void Visit(LexicalDeclaration node)
		{
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00037B2D File Offset: 0x00035D2D
		public void Visit(ModuleDeclaration node)
		{
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00037B2F File Offset: 0x00035D2F
		public void Visit(ReturnNode node)
		{
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00037B31 File Offset: 0x00035D31
		public void Visit(Switch node)
		{
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00037B33 File Offset: 0x00035D33
		public void Visit(SwitchCase node)
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00037B35 File Offset: 0x00035D35
		public void Visit(TemplateLiteralExpression node)
		{
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00037B37 File Offset: 0x00035D37
		public void Visit(ThrowNode node)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00037B39 File Offset: 0x00035D39
		public void Visit(TryNode node)
		{
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00037B3B File Offset: 0x00035D3B
		public void Visit(Var node)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00037B3D File Offset: 0x00035D3D
		public void Visit(VariableDeclaration node)
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00037B3F File Offset: 0x00035D3F
		public void Visit(WhileNode node)
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00037B41 File Offset: 0x00035D41
		public void Visit(WithNode node)
		{
		}

		// Token: 0x040004D6 RID: 1238
		private bool m_needsParens;

		// Token: 0x040004D7 RID: 1239
		private bool m_outerHasNoArguments;
	}
}
