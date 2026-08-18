using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000061 RID: 97
	public class TreeVisitor : IVisitor
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001A6EA File Offset: 0x000188EA
		public virtual void Visit(ArrayLiteral node)
		{
			if (node != null && node.Elements != null)
			{
				node.Elements.Accept(this);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A703 File Offset: 0x00018903
		public virtual void Visit(AspNetBlockNode node)
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A708 File Offset: 0x00018908
		public virtual void Visit(AstNodeList node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A75C File Offset: 0x0001895C
		public virtual void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				if (node.Operand1 != null)
				{
					node.Operand1.Accept(this);
				}
				if (node.Operand2 != null)
				{
					node.Operand2.Accept(this);
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A789 File Offset: 0x00018989
		public virtual void Visit(BindingIdentifier node)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A78C File Offset: 0x0001898C
		public virtual void Visit(Block node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A7E0 File Offset: 0x000189E0
		public virtual void Visit(Break node)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A7E2 File Offset: 0x000189E2
		public virtual void Visit(CallNode node)
		{
			if (node != null)
			{
				if (node.Arguments != null)
				{
					node.Arguments.Accept(this);
				}
				if (node.Function != null)
				{
					node.Function.Accept(this);
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A810 File Offset: 0x00018A10
		public virtual void Visit(ClassNode node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Heritage != null)
				{
					node.Heritage.Accept(this);
				}
				if (node.Elements != null)
				{
					node.Elements.Accept(this);
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001A85C File Offset: 0x00018A5C
		public virtual void Visit(ComprehensionNode node)
		{
			if (node != null)
			{
				if (node.Clauses != null)
				{
					node.Clauses.Accept(this);
				}
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001A889 File Offset: 0x00018A89
		public virtual void Visit(ComprehensionForClause node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001A8B6 File Offset: 0x00018AB6
		public virtual void Visit(ComprehensionIfClause node)
		{
			if (node != null && node.Condition != null)
			{
				node.Condition.Accept(this);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A8CF File Offset: 0x00018ACF
		public virtual void Visit(ConditionalCompilationComment node)
		{
			if (node != null && node.Statements != null)
			{
				node.Statements.Accept(this);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public virtual void Visit(ConditionalCompilationElse node)
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001A8EA File Offset: 0x00018AEA
		public virtual void Visit(ConditionalCompilationElseIf node)
		{
			if (node != null && node.Condition != null)
			{
				node.Condition.Accept(this);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A903 File Offset: 0x00018B03
		public virtual void Visit(ConditionalCompilationEnd node)
		{
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A905 File Offset: 0x00018B05
		public virtual void Visit(ConditionalCompilationIf node)
		{
			if (node != null && node.Condition != null)
			{
				node.Condition.Accept(this);
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001A91E File Offset: 0x00018B1E
		public virtual void Visit(ConditionalCompilationOn node)
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001A920 File Offset: 0x00018B20
		public virtual void Visit(ConditionalCompilationSet node)
		{
			if (node != null && node.Value != null)
			{
				node.Value.Accept(this);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001A93C File Offset: 0x00018B3C
		public virtual void Visit(Conditional node)
		{
			if (node != null)
			{
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				if (node.TrueExpression != null)
				{
					node.TrueExpression.Accept(this);
				}
				if (node.FalseExpression != null)
				{
					node.FalseExpression.Accept(this);
				}
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A988 File Offset: 0x00018B88
		public virtual void Visit(ConstantWrapper node)
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A98A File Offset: 0x00018B8A
		public virtual void Visit(ConstantWrapperPP node)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A98C File Offset: 0x00018B8C
		public virtual void Visit(ConstStatement node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		public virtual void Visit(ContinueNode node)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public virtual void Visit(CustomNode node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001AA38 File Offset: 0x00018C38
		public virtual void Visit(DebuggerNode node)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001AA3A File Offset: 0x00018C3A
		public virtual void Visit(DirectivePrologue node)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public virtual void Visit(DoWhile node)
		{
			if (node != null)
			{
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001AA69 File Offset: 0x00018C69
		public virtual void Visit(EmptyStatement node)
		{
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public virtual void Visit(ExportNode node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					astNode.Accept(this);
				}
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001AABC File Offset: 0x00018CBC
		public virtual void Visit(ForIn node)
		{
			if (node != null)
			{
				if (node.Variable != null)
				{
					node.Variable.Accept(this);
				}
				if (node.Collection != null)
				{
					node.Collection.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001AB08 File Offset: 0x00018D08
		public virtual void Visit(ForNode node)
		{
			if (node != null)
			{
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				if (node.Incrementer != null)
				{
					node.Incrementer.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001AB68 File Offset: 0x00018D68
		public virtual void Visit(FunctionObject node)
		{
			if (node != null && node.Body != null)
			{
				node.Body.Accept(this);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001AB81 File Offset: 0x00018D81
		public virtual void Visit(GetterSetter node)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001AB83 File Offset: 0x00018D83
		public virtual void Visit(GroupingOperator node)
		{
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public virtual void Visit(IfNode node)
		{
			if (node != null)
			{
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				if (node.TrueBlock != null)
				{
					node.TrueBlock.Accept(this);
				}
				if (node.FalseBlock != null)
				{
					node.FalseBlock.Accept(this);
				}
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public virtual void Visit(ImportantComment node)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001ABEA File Offset: 0x00018DEA
		public virtual void Visit(ImportExportSpecifier node)
		{
			if (node != null && node.LocalIdentifier != null)
			{
				node.LocalIdentifier.Accept(this);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001AC04 File Offset: 0x00018E04
		public virtual void Visit(ImportNode node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					astNode.Accept(this);
				}
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001AC54 File Offset: 0x00018E54
		public virtual void Visit(InitializerNode node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001AC81 File Offset: 0x00018E81
		public virtual void Visit(LabeledStatement node)
		{
			if (node != null && node.Statement != null)
			{
				node.Statement.Accept(this);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public virtual void Visit(LexicalDeclaration node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		public virtual void Visit(Lookup node)
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001ACF2 File Offset: 0x00018EF2
		public virtual void Visit(Member node)
		{
			if (node != null && node.Root != null)
			{
				node.Root.Accept(this);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001AD0B File Offset: 0x00018F0B
		public virtual void Visit(ModuleDeclaration node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001AD38 File Offset: 0x00018F38
		public virtual void Visit(ObjectLiteral node)
		{
			if (node != null && node.Properties != null)
			{
				node.Properties.Accept(this);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001AD51 File Offset: 0x00018F51
		public virtual void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001AD53 File Offset: 0x00018F53
		public virtual void Visit(ObjectLiteralProperty node)
		{
			if (node != null)
			{
				if (node.Name != null)
				{
					node.Name.Accept(this);
				}
				if (node.Value != null)
				{
					node.Value.Accept(this);
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001AD80 File Offset: 0x00018F80
		public virtual void Visit(ParameterDeclaration node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001ADAD File Offset: 0x00018FAD
		public virtual void Visit(RegExpLiteral node)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001ADAF File Offset: 0x00018FAF
		public virtual void Visit(ReturnNode node)
		{
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public virtual void Visit(Switch node)
		{
			if (node != null)
			{
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				if (node.Cases != null)
				{
					node.Cases.Accept(this);
				}
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001ADF5 File Offset: 0x00018FF5
		public virtual void Visit(SwitchCase node)
		{
			if (node != null)
			{
				if (node.CaseValue != null)
				{
					node.CaseValue.Accept(this);
				}
				if (node.Statements != null)
				{
					node.Statements.Accept(this);
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001AE22 File Offset: 0x00019022
		public virtual void Visit(TemplateLiteral node)
		{
			if (node != null)
			{
				if (node.Function != null)
				{
					node.Function.Accept(this);
				}
				if (node.Expressions != null)
				{
					node.Expressions.Accept(this);
				}
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001AE4F File Offset: 0x0001904F
		public virtual void Visit(TemplateLiteralExpression node)
		{
			if (node != null && node.Expression != null)
			{
				node.Expression.Accept(this);
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001AE68 File Offset: 0x00019068
		public virtual void Visit(ThisLiteral node)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001AE6A File Offset: 0x0001906A
		public virtual void Visit(ThrowNode node)
		{
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001AE84 File Offset: 0x00019084
		public virtual void Visit(TryNode node)
		{
			if (node != null)
			{
				if (node.TryBlock != null)
				{
					node.TryBlock.Accept(this);
				}
				if (node.CatchParameter != null)
				{
					node.CatchParameter.Accept(this);
				}
				if (node.CatchBlock != null)
				{
					node.CatchBlock.Accept(this);
				}
				if (node.FinallyBlock != null)
				{
					node.FinallyBlock.Accept(this);
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001AEE4 File Offset: 0x000190E4
		public virtual void Visit(Var node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node.Children)
				{
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001AF38 File Offset: 0x00019138
		public virtual void Visit(VariableDeclaration node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001AF65 File Offset: 0x00019165
		public virtual void Visit(UnaryOperator node)
		{
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001AF7E File Offset: 0x0001917E
		public virtual void Visit(WhileNode node)
		{
			if (node != null)
			{
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001AFAB File Offset: 0x000191AB
		public virtual void Visit(WithNode node)
		{
			if (node != null)
			{
				if (node.WithObject != null)
				{
					node.WithObject.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}
	}
}
