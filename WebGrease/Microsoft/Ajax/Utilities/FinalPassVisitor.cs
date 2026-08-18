using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008F RID: 143
	internal class FinalPassVisitor : TreeVisitor
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x000285EC File Offset: 0x000267EC
		private FinalPassVisitor(CodeSettings codeSettings)
		{
			this.m_settings = codeSettings;
			this.m_statementStart = new StatementStartVisitor();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00028608 File Offset: 0x00026808
		public static void Apply(AstNode node, CodeSettings codeSettings)
		{
			FinalPassVisitor visitor = new FinalPassVisitor(codeSettings);
			node.Accept(visitor);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028624 File Offset: 0x00026824
		public override void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				Block block;
				FunctionObject functionObject;
				if (node.OperatorToken == JSToken.Comma && this.m_settings.IsModificationAllowed(TreeModifications.UnfoldCommaExpressionStatements) && (block = (node.Parent as Block)) != null && (block.Parent == null || ((functionObject = (block.Parent as FunctionObject)) != null && (functionObject.FunctionType != FunctionType.ArrowFunction || block.Count > 1)) || block.Parent is TryNode || block.Parent is SwitchCase || block.Count > 1))
				{
					this.PossiblyBreakExpressionStatement(node, block);
					return;
				}
				base.Visit(node);
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000286C4 File Offset: 0x000268C4
		private void PossiblyBreakExpressionStatement(BinaryOperator node, Block parentBlock)
		{
			AstNodeList astNodeList = node.Operand2 as AstNodeList;
			if (astNodeList != null)
			{
				this.PossiblyBreakExpressionList(node, parentBlock, astNodeList);
				return;
			}
			if (this.CanBeBroken(node.Operand2))
			{
				AstNode operand = node.Operand1;
				parentBlock.ReplaceChild(node, operand);
				parentBlock.InsertAfter(operand, node.Operand2);
				operand.Accept(this);
				return;
			}
			base.Visit(node);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00028724 File Offset: 0x00026924
		private void PossiblyBreakExpressionList(BinaryOperator node, Block parentBlock, AstNodeList nodeList)
		{
			if (this.CanBeBroken(nodeList[0]))
			{
				int index = parentBlock.IndexOf(node);
				AstNode operand = node.Operand1;
				FinalPassVisitor.RotateOpeator(node, nodeList);
				parentBlock.Insert(index, operand);
				operand.Accept(this);
				return;
			}
			int i = 1;
			while (i < nodeList.Count)
			{
				if (this.CanBeBroken(nodeList[i]))
				{
					if (i != 1)
					{
						parentBlock.InsertAfter(node, FinalPassVisitor.CreateSplitNodeFromEnd(nodeList, i));
						break;
					}
					AstNode operand2 = nodeList[0];
					nodeList.RemoveAt(0);
					node.Operand2 = operand2;
					if (nodeList.Count > 0)
					{
						parentBlock.InsertAfter(node, FinalPassVisitor.CreateSplitNodeFromEnd(nodeList, 0));
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			base.Visit(node);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000287D0 File Offset: 0x000269D0
		private static AstNode CreateSplitNodeFromEnd(AstNodeList nodeList, int ndx)
		{
			AstNode result;
			if (ndx == nodeList.Count - 1)
			{
				result = nodeList[ndx];
				nodeList.RemoveAt(ndx);
			}
			else if (ndx == nodeList.Count - 2)
			{
				AstNode astNode = nodeList[ndx];
				nodeList.RemoveAt(ndx);
				AstNode operand = nodeList[ndx];
				nodeList.RemoveAt(ndx);
				result = new CommaOperator(astNode.Context.FlattenToStart())
				{
					Operand1 = astNode,
					Operand2 = operand
				};
			}
			else
			{
				AstNode astNode2 = nodeList[ndx];
				nodeList.RemoveAt(ndx);
				AstNodeList astNodeList;
				if (ndx == 0)
				{
					astNodeList = nodeList;
				}
				else
				{
					astNodeList = new AstNodeList(nodeList[ndx].Context.FlattenToStart());
					while (ndx < nodeList.Count)
					{
						AstNode node = nodeList[ndx];
						nodeList.RemoveAt(ndx);
						astNodeList.Append(node);
					}
				}
				result = new CommaOperator(astNode2.Context.FlattenToStart())
				{
					Operand1 = astNode2,
					Operand2 = astNodeList
				};
			}
			return result;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000288C8 File Offset: 0x00026AC8
		private static void RotateOpeator(BinaryOperator node, AstNodeList rightSide)
		{
			if (rightSide.Count == 0)
			{
				node.Parent.ReplaceChild(node, null);
				return;
			}
			if (rightSide.Count == 1)
			{
				node.Parent.ReplaceChild(node, rightSide[0]);
				return;
			}
			if (rightSide.Count == 2)
			{
				node.Operand1 = rightSide[0];
				node.Operand2 = rightSide[1];
				return;
			}
			AstNode operand = rightSide[0];
			rightSide.RemoveAt(0);
			node.Operand1 = operand;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00028944 File Offset: 0x00026B44
		private bool CanBeBroken(AstNode node)
		{
			AstNodeList astNodeList;
			return this.m_statementStart.IsSafe(node) && ((astNodeList = (node as AstNodeList)) == null || astNodeList.Count == 0 || this.CanBeBroken(astNodeList[0]));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028984 File Offset: 0x00026B84
		public override void Visit(ConstantWrapper node)
		{
			if (node != null && node.PrimitiveType == PrimitiveType.Boolean && this.m_settings.IsModificationAllowed(TreeModifications.BooleanLiteralsToNotOperators))
			{
				node.Parent.ReplaceChild(node, new UnaryOperator(node.Context)
				{
					Operand = new ConstantWrapper(node.ToBoolean() ? 0 : 1, PrimitiveType.Number, node.Context),
					OperatorToken = JSToken.LogicalNot
				});
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000289F8 File Offset: 0x00026BF8
		public override void Visit(ImportExportSpecifier node)
		{
			if (node != null && node.LocalIdentifier != null && node.ExternalName.IsNullOrWhiteSpace())
			{
				IRenameable renameable = node.LocalIdentifier as IRenameable;
				if (renameable.WasRenamed)
				{
					node.ExternalName = renameable.OriginalName;
				}
			}
		}

		// Token: 0x0400031E RID: 798
		private CodeSettings m_settings;

		// Token: 0x0400031F RID: 799
		private StatementStartVisitor m_statementStart;
	}
}
