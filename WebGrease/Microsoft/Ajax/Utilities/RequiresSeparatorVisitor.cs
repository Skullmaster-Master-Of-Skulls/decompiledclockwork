using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001E RID: 30
	public class RequiresSeparatorVisitor : IVisitor
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006343 File Offset: 0x00004543
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000634B File Offset: 0x0000454B
		public bool DoesRequire { get; private set; }

		// Token: 0x0600021F RID: 543 RVA: 0x00006354 File Offset: 0x00004554
		public RequiresSeparatorVisitor(CodeSettings settings)
		{
			this.DoesRequire = true;
			this.m_settings = (settings ?? new CodeSettings());
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000637C File Offset: 0x0000457C
		public bool Query(AstNode node)
		{
			this.DoesRequire = (node != null);
			node.IfNotNull(delegate(AstNode n)
			{
				n.Accept(this);
			});
			return this.DoesRequire;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000063A3 File Offset: 0x000045A3
		public void Visit(ArrayLiteral node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000063AF File Offset: 0x000045AF
		public void Visit(AspNetBlockNode node)
		{
			if (node != null)
			{
				this.DoesRequire = node.IsTerminatedByExplicitSemicolon;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000063C0 File Offset: 0x000045C0
		public void Visit(AstNodeList node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000063CC File Offset: 0x000045CC
		public void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000063D8 File Offset: 0x000045D8
		public void Visit(BindingIdentifier node)
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000063DC File Offset: 0x000045DC
		public void Visit(Block node)
		{
			if (node != null)
			{
				if (node.ForceBraces || node.Count > 1)
				{
					this.DoesRequire = false;
					return;
				}
				if (node.Count == 0)
				{
					this.DoesRequire = true;
					return;
				}
				if (node[0] == null)
				{
					this.DoesRequire = true;
					return;
				}
				node[0].Accept(this);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006433 File Offset: 0x00004633
		public void Visit(Break node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000643F File Offset: 0x0000463F
		public void Visit(CallNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000644B File Offset: 0x0000464B
		public void Visit(ClassNode node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006457 File Offset: 0x00004657
		public void Visit(ComprehensionNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006463 File Offset: 0x00004663
		public void Visit(ComprehensionForClause node)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006465 File Offset: 0x00004665
		public void Visit(ComprehensionIfClause node)
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006474 File Offset: 0x00004674
		public void Visit(ConditionalCompilationComment node)
		{
			if (node != null)
			{
				if (node.Statements.IfNotNull((Block s) => s.Count > 0))
				{
					node.Statements[node.Statements.Count - 1].Accept(this);
					return;
				}
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000064D4 File Offset: 0x000046D4
		public void Visit(ConditionalCompilationElse node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000064E0 File Offset: 0x000046E0
		public void Visit(ConditionalCompilationElseIf node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000064EC File Offset: 0x000046EC
		public void Visit(ConditionalCompilationEnd node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000064F8 File Offset: 0x000046F8
		public void Visit(ConditionalCompilationIf node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006504 File Offset: 0x00004704
		public void Visit(ConditionalCompilationOn node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006510 File Offset: 0x00004710
		public void Visit(ConditionalCompilationSet node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000651C File Offset: 0x0000471C
		public void Visit(Conditional node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006528 File Offset: 0x00004728
		public void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006534 File Offset: 0x00004734
		public void Visit(ConstantWrapperPP node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006540 File Offset: 0x00004740
		public void Visit(ConstStatement node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000654C File Offset: 0x0000474C
		public void Visit(ContinueNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006558 File Offset: 0x00004758
		public void Visit(CustomNode node)
		{
			if (node != null)
			{
				this.DoesRequire = node.RequiresSeparator;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006569 File Offset: 0x00004769
		public void Visit(DebuggerNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006575 File Offset: 0x00004775
		public void Visit(DirectivePrologue node)
		{
			if (node != null)
			{
				this.DoesRequire = !node.IsRedundant;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006589 File Offset: 0x00004789
		public void Visit(DoWhile node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006595 File Offset: 0x00004795
		public void Visit(EmptyStatement node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000065A1 File Offset: 0x000047A1
		public void Visit(ExportNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
				if (!node.IsDefault && node.Count == 1 && (node[0] is FunctionObject || node[0] is ClassNode))
				{
					this.DoesRequire = false;
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000065E1 File Offset: 0x000047E1
		public void Visit(ForIn node)
		{
			if (node != null)
			{
				if (node.Body == null || node.Body.Count == 0)
				{
					this.DoesRequire = false;
					return;
				}
				node.Body.Accept(this);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000660F File Offset: 0x0000480F
		public void Visit(ForNode node)
		{
			if (node != null)
			{
				if (node.Body == null)
				{
					this.DoesRequire = false;
					return;
				}
				node.Body.Accept(this);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006650 File Offset: 0x00004850
		public void Visit(FunctionObject node)
		{
			if (node != null)
			{
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					if (node.Body.IfNotNull((Block b) => b.Count == 1 && !(b[0] is ReturnNode)))
					{
						node.Body[0].Accept(this);
						return;
					}
				}
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000066AD File Offset: 0x000048AD
		public void Visit(GetterSetter node)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000066AF File Offset: 0x000048AF
		public void Visit(GroupingOperator node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000066BC File Offset: 0x000048BC
		public void Visit(IfNode node)
		{
			if (node != null)
			{
				if (node.FalseBlock != null && node.FalseBlock.Count > 0)
				{
					node.FalseBlock.Accept(this);
					return;
				}
				if (node.TrueBlock != null && node.TrueBlock.Count > 0)
				{
					node.TrueBlock.Accept(this);
					return;
				}
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006719 File Offset: 0x00004919
		public void Visit(ImportantComment node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006725 File Offset: 0x00004925
		public void Visit(ImportExportSpecifier node)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006727 File Offset: 0x00004927
		public void Visit(ImportNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006733 File Offset: 0x00004933
		public void Visit(InitializerNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000673F File Offset: 0x0000493F
		public void Visit(LabeledStatement node)
		{
			if (node != null)
			{
				if (node.Statement != null)
				{
					node.Statement.Accept(this);
					return;
				}
				this.DoesRequire = false;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006760 File Offset: 0x00004960
		public void Visit(LexicalDeclaration node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000676C File Offset: 0x0000496C
		public void Visit(Lookup node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006778 File Offset: 0x00004978
		public void Visit(Member node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006784 File Offset: 0x00004984
		public void Visit(ModuleDeclaration node)
		{
			if (node != null)
			{
				this.DoesRequire = (node.Binding != null);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000679B File Offset: 0x0000499B
		public void Visit(ObjectLiteral node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000067A7 File Offset: 0x000049A7
		public void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000067A9 File Offset: 0x000049A9
		public void Visit(ObjectLiteralProperty node)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000067AB File Offset: 0x000049AB
		public void Visit(ParameterDeclaration node)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000067AD File Offset: 0x000049AD
		public void Visit(RegExpLiteral node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000067B9 File Offset: 0x000049B9
		public void Visit(ReturnNode node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000067C5 File Offset: 0x000049C5
		public void Visit(Switch node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000067D1 File Offset: 0x000049D1
		public void Visit(SwitchCase node)
		{
			if (node != null)
			{
				if (node.Statements == null || node.Statements.Count == 0)
				{
					this.DoesRequire = false;
					return;
				}
				node.Statements[node.Statements.Count - 1].Accept(this);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006811 File Offset: 0x00004A11
		public void Visit(TemplateLiteral node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000681D File Offset: 0x00004A1D
		public void Visit(TemplateLiteralExpression node)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000681F File Offset: 0x00004A1F
		public void Visit(ThisLiteral node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000682B File Offset: 0x00004A2B
		public void Visit(ThrowNode node)
		{
			if (node != null)
			{
				this.DoesRequire = !this.m_settings.MacSafariQuirks;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006844 File Offset: 0x00004A44
		public void Visit(TryNode node)
		{
			if (node != null)
			{
				this.DoesRequire = false;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006850 File Offset: 0x00004A50
		public void Visit(Var node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000685C File Offset: 0x00004A5C
		public void Visit(VariableDeclaration node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00006868 File Offset: 0x00004A68
		public void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				this.DoesRequire = true;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006874 File Offset: 0x00004A74
		public void Visit(WhileNode node)
		{
			if (node != null)
			{
				if (node.Body == null || node.Body.Count == 0)
				{
					this.DoesRequire = false;
					return;
				}
				node.Body.Accept(this);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000068A2 File Offset: 0x00004AA2
		public void Visit(WithNode node)
		{
			if (node != null)
			{
				if (node.Body == null || node.Body.Count == 0)
				{
					this.DoesRequire = false;
					return;
				}
				node.Body.Accept(this);
			}
		}

		// Token: 0x04000071 RID: 113
		private CodeSettings m_settings;
	}
}
