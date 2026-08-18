using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001F RID: 31
	public class StatementStartVisitor : IVisitor
	{
		// Token: 0x06000264 RID: 612 RVA: 0x000068E1 File Offset: 0x00004AE1
		public bool IsSafe(AstNode node)
		{
			this.m_isSafe = true;
			node.IfNotNull(delegate(AstNode n)
			{
				n.Accept(this);
			});
			return this.m_isSafe;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00006902 File Offset: 0x00004B02
		public void Visit(BinaryOperator node)
		{
			if (node != null && node.Operand1 != null)
			{
				node.Operand1.Accept(this);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000691B File Offset: 0x00004B1B
		public void Visit(CallNode node)
		{
			if (node != null && node.Function != null)
			{
				node.Function.Accept(this);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006934 File Offset: 0x00004B34
		public void Visit(Conditional node)
		{
			if (node != null && node.Condition != null)
			{
				node.Condition.Accept(this);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000694D File Offset: 0x00004B4D
		public void Visit(Member node)
		{
			if (node != null && node.Root != null)
			{
				node.Root.Accept(this);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006966 File Offset: 0x00004B66
		public void Visit(UnaryOperator node)
		{
			if (node != null && node.IsPostfix && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006992 File Offset: 0x00004B92
		public void Visit(ClassNode node)
		{
			this.m_isSafe = node.IfNotNull((ClassNode n) => n.ClassType == ClassType.Declaration);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000069BD File Offset: 0x00004BBD
		public void Visit(CustomNode node)
		{
			this.m_isSafe = false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000069D1 File Offset: 0x00004BD1
		public void Visit(FunctionObject node)
		{
			this.m_isSafe = node.IfNotNull((FunctionObject n) => n.FunctionType == FunctionType.ArrowFunction);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000069FC File Offset: 0x00004BFC
		public void Visit(ObjectLiteral node)
		{
			this.m_isSafe = false;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006A05 File Offset: 0x00004C05
		public void Visit(ArrayLiteral node)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006A07 File Offset: 0x00004C07
		public void Visit(AspNetBlockNode node)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006A09 File Offset: 0x00004C09
		public void Visit(BindingIdentifier node)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00006A0B File Offset: 0x00004C0B
		public void Visit(Block node)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006A0D File Offset: 0x00004C0D
		public void Visit(Break node)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006A0F File Offset: 0x00004C0F
		public void Visit(ComprehensionNode node)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006A11 File Offset: 0x00004C11
		public void Visit(ConditionalCompilationComment node)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00006A13 File Offset: 0x00004C13
		public void Visit(ConditionalCompilationElse node)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006A15 File Offset: 0x00004C15
		public void Visit(ConditionalCompilationElseIf node)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006A17 File Offset: 0x00004C17
		public void Visit(ConditionalCompilationEnd node)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00006A19 File Offset: 0x00004C19
		public void Visit(ConditionalCompilationIf node)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006A1B File Offset: 0x00004C1B
		public void Visit(ConditionalCompilationOn node)
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006A1D File Offset: 0x00004C1D
		public void Visit(ConditionalCompilationSet node)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006A1F File Offset: 0x00004C1F
		public void Visit(ConstantWrapper node)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006A21 File Offset: 0x00004C21
		public void Visit(ConstantWrapperPP node)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006A23 File Offset: 0x00004C23
		public void Visit(ConstStatement node)
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006A25 File Offset: 0x00004C25
		public void Visit(ContinueNode node)
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006A27 File Offset: 0x00004C27
		public void Visit(DebuggerNode node)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006A29 File Offset: 0x00004C29
		public void Visit(DirectivePrologue node)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006A2B File Offset: 0x00004C2B
		public void Visit(DoWhile node)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006A2D File Offset: 0x00004C2D
		public void Visit(EmptyStatement node)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00006A2F File Offset: 0x00004C2F
		public void Visit(ExportNode node)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00006A31 File Offset: 0x00004C31
		public void Visit(ForIn node)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006A33 File Offset: 0x00004C33
		public void Visit(ForNode node)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006A35 File Offset: 0x00004C35
		public void Visit(GetterSetter node)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006A37 File Offset: 0x00004C37
		public void Visit(GroupingOperator node)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00006A39 File Offset: 0x00004C39
		public void Visit(IfNode node)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006A3B File Offset: 0x00004C3B
		public void Visit(ImportantComment node)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006A3D File Offset: 0x00004C3D
		public void Visit(ImportNode node)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006A3F File Offset: 0x00004C3F
		public void Visit(LabeledStatement node)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006A41 File Offset: 0x00004C41
		public void Visit(LexicalDeclaration node)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00006A43 File Offset: 0x00004C43
		public void Visit(Lookup node)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006A45 File Offset: 0x00004C45
		public void Visit(ModuleDeclaration node)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006A47 File Offset: 0x00004C47
		public void Visit(RegExpLiteral node)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006A49 File Offset: 0x00004C49
		public void Visit(ReturnNode node)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006A4B File Offset: 0x00004C4B
		public void Visit(Switch node)
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006A4D File Offset: 0x00004C4D
		public void Visit(TemplateLiteral node)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006A4F File Offset: 0x00004C4F
		public void Visit(ThisLiteral node)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006A51 File Offset: 0x00004C51
		public void Visit(ThrowNode node)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006A53 File Offset: 0x00004C53
		public void Visit(TryNode node)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006A55 File Offset: 0x00004C55
		public void Visit(Var node)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00006A57 File Offset: 0x00004C57
		public void Visit(WhileNode node)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00006A59 File Offset: 0x00004C59
		public void Visit(WithNode node)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00006A5B File Offset: 0x00004C5B
		public void Visit(AstNodeList node)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006A5D File Offset: 0x00004C5D
		public void Visit(ComprehensionForClause node)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00006A5F File Offset: 0x00004C5F
		public void Visit(ComprehensionIfClause node)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00006A61 File Offset: 0x00004C61
		public void Visit(InitializerNode node)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006A63 File Offset: 0x00004C63
		public void Visit(ImportExportSpecifier node)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006A65 File Offset: 0x00004C65
		public void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00006A67 File Offset: 0x00004C67
		public void Visit(ObjectLiteralProperty node)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00006A69 File Offset: 0x00004C69
		public void Visit(ParameterDeclaration node)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006A6B File Offset: 0x00004C6B
		public void Visit(SwitchCase node)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006A6D File Offset: 0x00004C6D
		public void Visit(TemplateLiteralExpression node)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006A6F File Offset: 0x00004C6F
		public void Visit(VariableDeclaration node)
		{
		}

		// Token: 0x04000075 RID: 117
		private bool m_isSafe;
	}
}
