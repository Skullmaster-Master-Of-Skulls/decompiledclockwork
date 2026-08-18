using System;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008E RID: 142
	internal class EvaluateLiteralVisitor : TreeVisitor
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00025916 File Offset: 0x00023B16
		public EvaluateLiteralVisitor(JSParser parser)
		{
			this.m_parser = parser;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00025928 File Offset: 0x00023B28
		private bool ReplaceMemberBracketWithDot(BinaryOperator node, ConstantWrapper newLiteral)
		{
			if (newLiteral.IsStringLiteral)
			{
				CallNode callNode = (node.Parent is AstNodeList) ? (node.Parent.Parent as CallNode) : null;
				if (callNode != null && callNode.InBrackets)
				{
					string text = newLiteral.ToString();
					string newName;
					if (this.m_parser.Settings.HasRenamePairs && this.m_parser.Settings.ManualRenamesProperties && this.m_parser.Settings.IsModificationAllowed((TreeModifications)((ulong)-2147483648)) && !string.IsNullOrEmpty(newName = this.m_parser.Settings.GetNewName(text)))
					{
						if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.BracketMemberToDotMember) && JSScanner.IsSafeIdentifier(newName) && !JSScanner.IsKeyword(newName, (callNode.EnclosingScope ?? this.m_parser.GlobalScope).UseStrict))
						{
							Member newNode = new Member(callNode.Context)
							{
								Root = callNode.Function,
								Name = newName,
								NameContext = callNode.Arguments[0].Context
							};
							callNode.Parent.ReplaceChild(callNode, newNode);
							return true;
						}
						newLiteral.Value = newName;
						newLiteral.PrimitiveType = PrimitiveType.String;
					}
					else if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.BracketMemberToDotMember) && JSScanner.IsSafeIdentifier(text) && !JSScanner.IsKeyword(text, (callNode.EnclosingScope ?? this.m_parser.GlobalScope).UseStrict))
					{
						Member newNode2 = new Member(callNode.Context)
						{
							Root = callNode.Function,
							Name = text,
							NameContext = callNode.Arguments[0].Context
						};
						callNode.Parent.ReplaceChild(callNode, newNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00025B08 File Offset: 0x00023D08
		private static void ReplaceNodeWithLiteral(AstNode node, ConstantWrapper newLiteral)
		{
			GroupingOperator groupingOperator = node.Parent as GroupingOperator;
			if (groupingOperator != null)
			{
				groupingOperator.Parent.ReplaceChild(groupingOperator, newLiteral);
				return;
			}
			node.Parent.ReplaceChild(node, newLiteral);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00025B44 File Offset: 0x00023D44
		private static void ReplaceNodeCheckParens(AstNode oldNode, AstNode newNode)
		{
			GroupingOperator groupingOperator = oldNode.Parent as GroupingOperator;
			if (groupingOperator == null)
			{
				oldNode.Parent.ReplaceChild(oldNode, newNode);
				return;
			}
			if (newNode == null)
			{
				groupingOperator.Parent.ReplaceChild(groupingOperator, null);
				return;
			}
			OperatorPrecedence operatorPrecedence = groupingOperator.Parent.Precedence;
			Conditional conditional = groupingOperator.Parent as Conditional;
			if (conditional != null)
			{
				operatorPrecedence = ((conditional.Condition == groupingOperator) ? OperatorPrecedence.LogicalOr : OperatorPrecedence.Assignment);
			}
			if (newNode.Precedence >= operatorPrecedence)
			{
				groupingOperator.Parent.ReplaceChild(groupingOperator, newNode);
				return;
			}
			oldNode.Parent.ReplaceChild(oldNode, newNode);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00025BD0 File Offset: 0x00023DD0
		private void EvalThisOperator(BinaryOperator node, ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper constantWrapper = null;
			switch (node.OperatorToken)
			{
			case JSToken.FirstBinaryOperator:
				constantWrapper = this.Plus(left, right);
				break;
			case JSToken.Minus:
				constantWrapper = this.Minus(left, right);
				break;
			case JSToken.Multiply:
				constantWrapper = this.Multiply(left, right);
				break;
			case JSToken.Divide:
				constantWrapper = this.Divide(left, right);
				if (constantWrapper != null && this.NodeLength(constantWrapper) > this.NodeLength(node))
				{
					constantWrapper = null;
				}
				break;
			case JSToken.Modulo:
				constantWrapper = this.Modulo(left, right);
				if (constantWrapper != null && this.NodeLength(constantWrapper) > this.NodeLength(node))
				{
					constantWrapper = null;
				}
				break;
			case JSToken.BitwiseAnd:
				constantWrapper = this.BitwiseAnd(left, right);
				break;
			case JSToken.BitwiseOr:
				constantWrapper = this.BitwiseOr(left, right);
				break;
			case JSToken.BitwiseXor:
				constantWrapper = this.BitwiseXor(left, right);
				break;
			case JSToken.LeftShift:
				constantWrapper = this.LeftShift(left, right);
				break;
			case JSToken.RightShift:
				constantWrapper = this.RightShift(left, right);
				break;
			case JSToken.UnsignedRightShift:
				constantWrapper = this.UnsignedRightShift(left, right);
				break;
			case JSToken.Equal:
				constantWrapper = this.Equal(left, right);
				break;
			case JSToken.NotEqual:
				constantWrapper = this.NotEqual(left, right);
				break;
			case JSToken.StrictEqual:
				constantWrapper = this.StrictEqual(left, right);
				break;
			case JSToken.StrictNotEqual:
				constantWrapper = this.StrictNotEqual(left, right);
				break;
			case JSToken.LessThan:
				constantWrapper = this.LessThan(left, right);
				break;
			case JSToken.LessThanEqual:
				constantWrapper = this.LessThanOrEqual(left, right);
				break;
			case JSToken.GreaterThan:
				constantWrapper = this.GreaterThan(left, right);
				break;
			case JSToken.GreaterThanEqual:
				constantWrapper = this.GreaterThanOrEqual(left, right);
				break;
			case JSToken.LogicalAnd:
				constantWrapper = this.LogicalAnd(left, right);
				break;
			case JSToken.LogicalOr:
				constantWrapper = this.LogicalOr(left, right);
				break;
			}
			if (constantWrapper != null && !this.ReplaceMemberBracketWithDot(node, constantWrapper))
			{
				EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, constantWrapper);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00025D94 File Offset: 0x00023F94
		private void RotateFromLeft(BinaryOperator node, BinaryOperator binaryOp, ConstantWrapper newLiteral)
		{
			binaryOp.Operand2 = newLiteral;
			node.Parent.ReplaceChild(node, binaryOp);
			ConstantWrapper constantWrapper = binaryOp.Operand1 as ConstantWrapper;
			if (constantWrapper != null)
			{
				this.EvalThisOperator(binaryOp, constantWrapper, newLiteral);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025DD0 File Offset: 0x00023FD0
		private void RotateFromRight(BinaryOperator node, BinaryOperator binaryOp, ConstantWrapper newLiteral)
		{
			binaryOp.Operand1 = newLiteral;
			node.Parent.ReplaceChild(node, binaryOp);
			ConstantWrapper constantWrapper = binaryOp.Operand2 as ConstantWrapper;
			if (constantWrapper != null)
			{
				this.EvalThisOperator(binaryOp, newLiteral, constantWrapper);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00025E0C File Offset: 0x0002400C
		private static bool NoMultiplicativeOverOrUnderFlow(ConstantWrapper left, ConstantWrapper right, ConstantWrapper result)
		{
			bool flag = !result.IsInfinity;
			if (flag)
			{
				flag = (!result.IsZero || left.IsZero || right.IsZero);
			}
			return flag;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00025E44 File Offset: 0x00024044
		private static bool NoOverflow(ConstantWrapper result)
		{
			return !result.IsInfinity;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00025E50 File Offset: 0x00024050
		private void EvalToTheLeft(BinaryOperator node, ConstantWrapper thisConstant, ConstantWrapper otherConstant, BinaryOperator leftOperator)
		{
			if (leftOperator.OperatorToken == JSToken.FirstBinaryOperator && node.OperatorToken == JSToken.FirstBinaryOperator)
			{
				if (otherConstant.IsStringLiteral)
				{
					ConstantWrapper constantWrapper = this.StringConcat(otherConstant, thisConstant);
					if (constantWrapper != null)
					{
						this.RotateFromLeft(node, leftOperator, constantWrapper);
						return;
					}
				}
			}
			else if (leftOperator.OperatorToken == JSToken.Minus)
			{
				if (node.OperatorToken == JSToken.FirstBinaryOperator)
				{
					if (!thisConstant.IsStringLiteral)
					{
						ConstantWrapper constantWrapper2 = this.Minus(otherConstant, thisConstant);
						if (constantWrapper2 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper2))
						{
							this.RotateFromLeft(node, leftOperator, constantWrapper2);
							return;
						}
						ConstantWrapper constantWrapper3 = leftOperator.Operand1 as ConstantWrapper;
						if (constantWrapper3 != null)
						{
							this.EvalFarToTheLeft(node, thisConstant, constantWrapper3, leftOperator);
							return;
						}
					}
				}
				else if (node.OperatorToken == JSToken.Minus)
				{
					ConstantWrapper constantWrapper4 = this.NumericAddition(otherConstant, thisConstant);
					if (constantWrapper4 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper4))
					{
						this.RotateFromLeft(node, leftOperator, constantWrapper4);
						return;
					}
					ConstantWrapper constantWrapper5 = leftOperator.Operand1 as ConstantWrapper;
					if (constantWrapper5 != null)
					{
						this.EvalFarToTheLeft(node, thisConstant, constantWrapper5, leftOperator);
						return;
					}
				}
			}
			else if (leftOperator.OperatorToken == node.OperatorToken && (node.OperatorToken == JSToken.Multiply || node.OperatorToken == JSToken.Divide))
			{
				ConstantWrapper constantWrapper6 = this.Multiply(otherConstant, thisConstant);
				if (constantWrapper6 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper6))
				{
					this.RotateFromLeft(node, leftOperator, constantWrapper6);
					return;
				}
			}
			else if ((leftOperator.OperatorToken == JSToken.Multiply && node.OperatorToken == JSToken.Divide) || (leftOperator.OperatorToken == JSToken.Divide && node.OperatorToken == JSToken.Multiply))
			{
				if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
				{
					ConstantWrapper constantWrapper7 = this.Divide(otherConstant, thisConstant);
					ConstantWrapper constantWrapper8 = this.Divide(thisConstant, otherConstant);
					int num = (constantWrapper7 != null) ? this.NodeLength(constantWrapper7) : int.MaxValue;
					int num2 = (constantWrapper8 != null) ? this.NodeLength(constantWrapper8) : int.MaxValue;
					if (constantWrapper7 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper7) && (constantWrapper8 == null || num < num2))
					{
						if (num <= this.NodeLength(otherConstant) + this.NodeLength(thisConstant) + 1)
						{
							this.RotateFromLeft(node, leftOperator, constantWrapper7);
							return;
						}
					}
					else if (constantWrapper8 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper8) && num2 <= this.NodeLength(otherConstant) + this.NodeLength(thisConstant) + 1)
					{
						leftOperator.OperatorToken = ((leftOperator.OperatorToken == JSToken.Multiply) ? JSToken.Divide : JSToken.Multiply);
						this.RotateFromLeft(node, leftOperator, constantWrapper8);
						return;
					}
				}
			}
			else if (node.OperatorToken == leftOperator.OperatorToken && (node.OperatorToken == JSToken.BitwiseAnd || node.OperatorToken == JSToken.BitwiseOr || node.OperatorToken == JSToken.BitwiseXor))
			{
				ConstantWrapper constantWrapper9 = null;
				switch (node.OperatorToken)
				{
				case JSToken.BitwiseAnd:
					constantWrapper9 = this.BitwiseAnd(otherConstant, thisConstant);
					break;
				case JSToken.BitwiseOr:
					constantWrapper9 = this.BitwiseOr(otherConstant, thisConstant);
					break;
				case JSToken.BitwiseXor:
					constantWrapper9 = this.BitwiseXor(otherConstant, thisConstant);
					break;
				}
				if (constantWrapper9 != null)
				{
					this.RotateFromLeft(node, leftOperator, constantWrapper9);
				}
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002612C File Offset: 0x0002432C
		private void EvalFarToTheLeft(BinaryOperator node, ConstantWrapper thisConstant, ConstantWrapper otherConstant, BinaryOperator leftOperator)
		{
			if (leftOperator.OperatorToken == JSToken.Minus)
			{
				if (node.OperatorToken == JSToken.FirstBinaryOperator)
				{
					if (thisConstant.PrimitiveType != PrimitiveType.String && thisConstant.PrimitiveType != PrimitiveType.Other)
					{
						ConstantWrapper constantWrapper = this.NumericAddition(otherConstant, thisConstant);
						if (constantWrapper != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper))
						{
							this.RotateFromRight(node, leftOperator, constantWrapper);
							return;
						}
					}
				}
				else if (node.OperatorToken == JSToken.Minus)
				{
					ConstantWrapper constantWrapper2 = this.Minus(otherConstant, thisConstant);
					if (constantWrapper2 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper2))
					{
						this.RotateFromRight(node, leftOperator, constantWrapper2);
						return;
					}
				}
			}
			else if (node.OperatorToken == JSToken.Multiply)
			{
				if (leftOperator.OperatorToken == JSToken.Multiply || leftOperator.OperatorToken == JSToken.Divide)
				{
					ConstantWrapper constantWrapper3 = this.Multiply(otherConstant, thisConstant);
					if (constantWrapper3 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper3))
					{
						this.RotateFromRight(node, leftOperator, constantWrapper3);
						return;
					}
				}
			}
			else if (node.OperatorToken == JSToken.Divide)
			{
				if (leftOperator.OperatorToken == JSToken.Divide)
				{
					ConstantWrapper constantWrapper4 = this.Divide(otherConstant, thisConstant);
					if (constantWrapper4 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper4) && this.NodeLength(constantWrapper4) <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						this.RotateFromRight(node, leftOperator, constantWrapper4);
						return;
					}
				}
				else if (leftOperator.OperatorToken == JSToken.Multiply)
				{
					ConstantWrapper constantWrapper5 = this.Divide(otherConstant, thisConstant);
					ConstantWrapper constantWrapper6 = this.Divide(thisConstant, otherConstant);
					int num = (constantWrapper5 != null) ? this.NodeLength(constantWrapper5) : int.MaxValue;
					int num2 = (constantWrapper6 != null) ? this.NodeLength(constantWrapper6) : int.MaxValue;
					if (constantWrapper5 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper5) && (constantWrapper6 == null || num < num2))
					{
						if (num <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
						{
							this.RotateFromRight(node, leftOperator, constantWrapper5);
							return;
						}
					}
					else if (constantWrapper6 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper6) && num2 <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						leftOperator.SwapOperands();
						leftOperator.OperatorToken = JSToken.Divide;
						this.RotateFromLeft(node, leftOperator, constantWrapper6);
					}
				}
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00026328 File Offset: 0x00024528
		private void EvalToTheRight(BinaryOperator node, ConstantWrapper thisConstant, ConstantWrapper otherConstant, BinaryOperator rightOperator)
		{
			if (node.OperatorToken == JSToken.FirstBinaryOperator)
			{
				if (rightOperator.OperatorToken == JSToken.FirstBinaryOperator && otherConstant.IsStringLiteral)
				{
					ConstantWrapper constantWrapper = this.StringConcat(thisConstant, otherConstant);
					if (constantWrapper != null)
					{
						this.RotateFromRight(node, rightOperator, constantWrapper);
						return;
					}
				}
				else if (rightOperator.OperatorToken == JSToken.Minus && !thisConstant.IsStringLiteral)
				{
					ConstantWrapper constantWrapper2 = this.NumericAddition(thisConstant, otherConstant);
					if (constantWrapper2 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper2))
					{
						this.RotateFromRight(node, rightOperator, constantWrapper2);
						return;
					}
					ConstantWrapper constantWrapper3 = rightOperator.Operand2 as ConstantWrapper;
					if (constantWrapper3 != null)
					{
						this.EvalFarToTheRight(node, thisConstant, constantWrapper3, rightOperator);
						return;
					}
				}
			}
			else if (node.OperatorToken == JSToken.Minus && rightOperator.OperatorToken == JSToken.Minus)
			{
				ConstantWrapper constantWrapper4 = this.Minus(otherConstant, thisConstant);
				if (constantWrapper4 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper4))
				{
					rightOperator.SwapOperands();
					this.RotateFromLeft(node, rightOperator, constantWrapper4);
					return;
				}
				ConstantWrapper constantWrapper5 = rightOperator.Operand2 as ConstantWrapper;
				if (constantWrapper5 != null)
				{
					this.EvalFarToTheRight(node, thisConstant, constantWrapper5, rightOperator);
					return;
				}
			}
			else if (node.OperatorToken == JSToken.Multiply && (rightOperator.OperatorToken == JSToken.Multiply || rightOperator.OperatorToken == JSToken.Divide))
			{
				ConstantWrapper constantWrapper6 = this.Multiply(thisConstant, otherConstant);
				if (constantWrapper6 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper6))
				{
					this.RotateFromRight(node, rightOperator, constantWrapper6);
					return;
				}
			}
			else if (node.OperatorToken == JSToken.Divide)
			{
				if (rightOperator.OperatorToken == JSToken.Multiply)
				{
					ConstantWrapper constantWrapper7 = this.Divide(thisConstant, otherConstant);
					if (constantWrapper7 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper7) && this.NodeLength(constantWrapper7) < this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						rightOperator.OperatorToken = JSToken.Divide;
						this.RotateFromRight(node, rightOperator, constantWrapper7);
						return;
					}
				}
				else if (rightOperator.OperatorToken == JSToken.Divide)
				{
					ConstantWrapper constantWrapper8 = this.Divide(thisConstant, otherConstant);
					ConstantWrapper constantWrapper9 = this.Divide(otherConstant, thisConstant);
					int num = (constantWrapper8 != null) ? this.NodeLength(constantWrapper8) : int.MaxValue;
					int num2 = (constantWrapper9 != null) ? this.NodeLength(constantWrapper9) : int.MaxValue;
					if (constantWrapper8 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper8) && (constantWrapper9 == null || num < num2))
					{
						if (num <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
						{
							rightOperator.OperatorToken = JSToken.Multiply;
							this.RotateFromRight(node, rightOperator, constantWrapper8);
							return;
						}
					}
					else if (constantWrapper9 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper9) && num2 <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						rightOperator.SwapOperands();
						this.RotateFromLeft(node, rightOperator, constantWrapper9);
					}
				}
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000265A0 File Offset: 0x000247A0
		private void EvalFarToTheRight(BinaryOperator node, ConstantWrapper thisConstant, ConstantWrapper otherConstant, BinaryOperator rightOperator)
		{
			if (rightOperator.OperatorToken == JSToken.Minus)
			{
				if (node.OperatorToken == JSToken.FirstBinaryOperator)
				{
					if (!thisConstant.IsStringLiteral)
					{
						ConstantWrapper constantWrapper = this.Minus(otherConstant, thisConstant);
						if (constantWrapper != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper))
						{
							this.RotateFromLeft(node, rightOperator, constantWrapper);
							return;
						}
					}
				}
				else if (node.OperatorToken == JSToken.Minus)
				{
					ConstantWrapper constantWrapper2 = this.NumericAddition(thisConstant, otherConstant);
					if (constantWrapper2 != null && EvaluateLiteralVisitor.NoOverflow(constantWrapper2))
					{
						rightOperator.SwapOperands();
						this.RotateFromRight(node, rightOperator, constantWrapper2);
						return;
					}
				}
			}
			else if (node.OperatorToken == JSToken.Multiply)
			{
				if (rightOperator.OperatorToken == JSToken.Multiply)
				{
					ConstantWrapper constantWrapper3 = this.Multiply(thisConstant, otherConstant);
					if (constantWrapper3 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper3))
					{
						this.RotateFromLeft(node, rightOperator, constantWrapper3);
						return;
					}
				}
				else if (rightOperator.OperatorToken == JSToken.Divide)
				{
					ConstantWrapper constantWrapper4 = this.Divide(otherConstant, thisConstant);
					ConstantWrapper constantWrapper5 = this.Divide(thisConstant, otherConstant);
					int num = (constantWrapper4 != null) ? this.NodeLength(constantWrapper4) : int.MaxValue;
					int num2 = (constantWrapper5 != null) ? this.NodeLength(constantWrapper5) : int.MaxValue;
					if (constantWrapper4 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(otherConstant, thisConstant, constantWrapper4) && (constantWrapper5 == null || num < num2))
					{
						if (num <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
						{
							this.RotateFromLeft(node, rightOperator, constantWrapper4);
							return;
						}
					}
					else if (constantWrapper5 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper5) && num2 <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						rightOperator.SwapOperands();
						rightOperator.OperatorToken = JSToken.Multiply;
						this.RotateFromRight(node, rightOperator, constantWrapper5);
						return;
					}
				}
			}
			else if (node.OperatorToken == JSToken.Divide)
			{
				if (rightOperator.OperatorToken == JSToken.Multiply)
				{
					ConstantWrapper constantWrapper6 = this.Divide(thisConstant, otherConstant);
					if (constantWrapper6 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper6) && this.NodeLength(constantWrapper6) <= this.NodeLength(thisConstant) + this.NodeLength(otherConstant) + 1)
					{
						rightOperator.SwapOperands();
						rightOperator.OperatorToken = JSToken.Divide;
						this.RotateFromRight(node, rightOperator, constantWrapper6);
						return;
					}
				}
				else if (rightOperator.OperatorToken == JSToken.Divide)
				{
					ConstantWrapper constantWrapper7 = this.Multiply(thisConstant, otherConstant);
					if (constantWrapper7 != null && EvaluateLiteralVisitor.NoMultiplicativeOverOrUnderFlow(thisConstant, otherConstant, constantWrapper7))
					{
						rightOperator.SwapOperands();
						this.RotateFromRight(node, rightOperator, constantWrapper7);
					}
				}
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000267D4 File Offset: 0x000249D4
		private ConstantWrapper Multiply(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (left.IsOkayToCombine && right.IsOkayToCombine && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					double num = left.ToNumber();
					double num2 = right.ToNumber();
					double num3 = num * num2;
					if (ConstantWrapper.NumberIsOkayToCombine(num3))
					{
						result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
					}
					else
					{
						if (!left.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num))
						{
							left.Parent.ReplaceChild(left, new ConstantWrapper(num, PrimitiveType.Number, left.Context));
						}
						if (!right.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num2))
						{
							right.Parent.ReplaceChild(right, new ConstantWrapper(num2, PrimitiveType.Number, right.Context));
						}
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000268BC File Offset: 0x00024ABC
		private ConstantWrapper Divide(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (left.IsOkayToCombine && right.IsOkayToCombine && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					double num = left.ToNumber();
					double num2 = right.ToNumber();
					double num3 = num / num2;
					if (ConstantWrapper.NumberIsOkayToCombine(num3))
					{
						result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
					}
					else
					{
						if (!left.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num))
						{
							left.Parent.ReplaceChild(left, new ConstantWrapper(num, PrimitiveType.Number, left.Context));
						}
						if (!right.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num2))
						{
							right.Parent.ReplaceChild(right, new ConstantWrapper(num2, PrimitiveType.Number, right.Context));
						}
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000269A4 File Offset: 0x00024BA4
		private ConstantWrapper Modulo(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (left.IsOkayToCombine && right.IsOkayToCombine && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					double num = left.ToNumber();
					double num2 = right.ToNumber();
					double num3 = num % num2;
					if (ConstantWrapper.NumberIsOkayToCombine(num3))
					{
						result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
					}
					else
					{
						if (!left.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num))
						{
							left.Parent.ReplaceChild(left, new ConstantWrapper(num, PrimitiveType.Number, left.Context));
						}
						if (!right.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num2))
						{
							right.Parent.ReplaceChild(right, new ConstantWrapper(num2, PrimitiveType.Number, right.Context));
						}
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00026A8C File Offset: 0x00024C8C
		private ConstantWrapper Plus(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result;
			if (left.IsStringLiteral || right.IsStringLiteral)
			{
				result = this.StringConcat(left, right);
			}
			else
			{
				result = this.NumericAddition(left, right);
			}
			return result;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00026AC0 File Offset: 0x00024CC0
		private ConstantWrapper NumericAddition(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (left.IsOkayToCombine && right.IsOkayToCombine && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					double num = left.ToNumber();
					double num2 = right.ToNumber();
					double num3 = num + num2;
					if (ConstantWrapper.NumberIsOkayToCombine(num3))
					{
						result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
					}
					else
					{
						if (!left.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num))
						{
							left.Parent.ReplaceChild(left, new ConstantWrapper(num, PrimitiveType.Number, left.Context));
						}
						if (!right.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num2))
						{
							right.Parent.ReplaceChild(right, new ConstantWrapper(num2, PrimitiveType.Number, right.Context));
						}
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00026BA8 File Offset: 0x00024DA8
		private ConstantWrapper StringConcat(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.CombineAdjacentStringLiterals) && ((left.IsStringLiteral && right.IsStringLiteral) || this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions)) && left.IsOkayToCombine && right.IsOkayToCombine)
			{
				result = new ConstantWrapper(left.ToString() + right.ToString(), PrimitiveType.String, left.Context.FlattenToStart());
			}
			return result;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00026C2C File Offset: 0x00024E2C
		private ConstantWrapper Minus(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (left.IsOkayToCombine && right.IsOkayToCombine && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					double num = left.ToNumber();
					double num2 = right.ToNumber();
					double num3 = num - num2;
					if (ConstantWrapper.NumberIsOkayToCombine(num3))
					{
						result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
					}
					else
					{
						if (!left.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num))
						{
							left.Parent.ReplaceChild(left, new ConstantWrapper(num, PrimitiveType.Number, left.Context));
						}
						if (!right.IsNumericLiteral && ConstantWrapper.NumberIsOkayToCombine(num2))
						{
							right.Parent.ReplaceChild(right, new ConstantWrapper(num2, PrimitiveType.Number, right.Context));
						}
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00026D14 File Offset: 0x00024F14
		private ConstantWrapper LeftShift(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					int num = left.ToInt32();
					int num2 = (int)(right.ToUInt32() & 31U);
					double num3 = Convert.ToDouble(num << num2);
					result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00026D88 File Offset: 0x00024F88
		private ConstantWrapper RightShift(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					int num = left.ToInt32();
					int num2 = (int)(right.ToUInt32() & 31U);
					double num3 = Convert.ToDouble(num >> num2);
					result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00026DFC File Offset: 0x00024FFC
		private ConstantWrapper UnsignedRightShift(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					uint num = left.ToUInt32();
					int num2 = (int)(right.ToUInt32() & 31U);
					double num3 = Convert.ToDouble(num >> num2);
					result = new ConstantWrapper(num3, PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00026E70 File Offset: 0x00025070
		private ConstantWrapper LessThan(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				if (left.IsStringLiteral && right.IsStringLiteral)
				{
					if (left.IsOkayToCombine && right.IsOkayToCombine)
					{
						result = new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) < 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
				}
				else
				{
					try
					{
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							result = new ConstantWrapper(left.ToNumber() < right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00026F34 File Offset: 0x00025134
		private ConstantWrapper LessThanOrEqual(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				if (left.IsStringLiteral && right.IsStringLiteral)
				{
					if (left.IsOkayToCombine && right.IsOkayToCombine)
					{
						result = new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) <= 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
				}
				else
				{
					try
					{
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							result = new ConstantWrapper(left.ToNumber() <= right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00026FFC File Offset: 0x000251FC
		private ConstantWrapper GreaterThan(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				if (left.IsStringLiteral && right.IsStringLiteral)
				{
					if (left.IsOkayToCombine && right.IsOkayToCombine)
					{
						result = new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) > 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
				}
				else
				{
					try
					{
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							result = new ConstantWrapper(left.ToNumber() > right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000270C0 File Offset: 0x000252C0
		private ConstantWrapper GreaterThanOrEqual(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				if (left.IsStringLiteral && right.IsStringLiteral)
				{
					if (left.IsOkayToCombine && right.IsOkayToCombine)
					{
						result = new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) >= 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
				}
				else
				{
					try
					{
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							result = new ConstantWrapper(left.ToNumber() >= right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00027188 File Offset: 0x00025388
		private ConstantWrapper Equal(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				PrimitiveType primitiveType = left.PrimitiveType;
				if (primitiveType == right.PrimitiveType)
				{
					switch (primitiveType)
					{
					case PrimitiveType.Null:
						return new ConstantWrapper(true, PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Boolean:
						return new ConstantWrapper(left.ToBoolean() == right.ToBoolean(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Number:
						try
						{
							if (left.IsOkayToCombine && right.IsOkayToCombine)
							{
								result = new ConstantWrapper(left.ToNumber() == right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
							}
							return result;
						}
						catch (InvalidCastException)
						{
							return result;
						}
						break;
					case PrimitiveType.String:
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							return new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) == 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
						return result;
					default:
						return result;
					}
				}
				if (left.IsOkayToCombine && right.IsOkayToCombine)
				{
					try
					{
						result = new ConstantWrapper(left.ToNumber() == right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00027300 File Offset: 0x00025500
		private ConstantWrapper NotEqual(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				PrimitiveType primitiveType = left.PrimitiveType;
				if (primitiveType == right.PrimitiveType)
				{
					switch (primitiveType)
					{
					case PrimitiveType.Null:
						return new ConstantWrapper(false, PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Boolean:
						return new ConstantWrapper(left.ToBoolean() != right.ToBoolean(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Number:
						try
						{
							if (left.IsOkayToCombine && right.IsOkayToCombine)
							{
								result = new ConstantWrapper(left.ToNumber() != right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
							}
							return result;
						}
						catch (InvalidCastException)
						{
							return result;
						}
						break;
					case PrimitiveType.String:
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							return new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) != 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
						return result;
					default:
						return result;
					}
				}
				if (left.IsOkayToCombine && right.IsOkayToCombine)
				{
					try
					{
						result = new ConstantWrapper(left.ToNumber() != right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					}
					catch (InvalidCastException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00027484 File Offset: 0x00025684
		private ConstantWrapper StrictEqual(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				PrimitiveType primitiveType = left.PrimitiveType;
				if (primitiveType == right.PrimitiveType)
				{
					switch (primitiveType)
					{
					case PrimitiveType.Null:
						return new ConstantWrapper(true, PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Boolean:
						return new ConstantWrapper(left.ToBoolean() == right.ToBoolean(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Number:
						try
						{
							if (left.IsOkayToCombine && right.IsOkayToCombine)
							{
								result = new ConstantWrapper(left.ToNumber() == right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
							}
							return result;
						}
						catch (InvalidCastException)
						{
							return result;
						}
						break;
					case PrimitiveType.String:
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							return new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) == 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
						return result;
					default:
						return result;
					}
				}
				result = new ConstantWrapper(false, PrimitiveType.Boolean, left.Context.FlattenToStart());
			}
			return result;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000275C8 File Offset: 0x000257C8
		private ConstantWrapper StrictNotEqual(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				PrimitiveType primitiveType = left.PrimitiveType;
				if (primitiveType == right.PrimitiveType)
				{
					switch (primitiveType)
					{
					case PrimitiveType.Null:
						return new ConstantWrapper(false, PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Boolean:
						return new ConstantWrapper(left.ToBoolean() != right.ToBoolean(), PrimitiveType.Boolean, left.Context.FlattenToStart());
					case PrimitiveType.Number:
						try
						{
							if (left.IsOkayToCombine && right.IsOkayToCombine)
							{
								result = new ConstantWrapper(left.ToNumber() != right.ToNumber(), PrimitiveType.Boolean, left.Context.FlattenToStart());
							}
							return result;
						}
						catch (InvalidCastException)
						{
							return result;
						}
						break;
					case PrimitiveType.String:
						if (left.IsOkayToCombine && right.IsOkayToCombine)
						{
							return new ConstantWrapper(string.CompareOrdinal(left.ToString(), right.ToString()) != 0, PrimitiveType.Boolean, left.Context.FlattenToStart());
						}
						return result;
					default:
						return result;
					}
				}
				result = new ConstantWrapper(true, PrimitiveType.Boolean, left.Context.FlattenToStart());
			}
			return result;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00027718 File Offset: 0x00025918
		private ConstantWrapper BitwiseAnd(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					int num = left.ToInt32();
					int num2 = right.ToInt32();
					result = new ConstantWrapper(Convert.ToDouble(num & num2), PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00027784 File Offset: 0x00025984
		private ConstantWrapper BitwiseOr(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					int num = left.ToInt32();
					int num2 = right.ToInt32();
					result = new ConstantWrapper(Convert.ToDouble(num | num2), PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000277F0 File Offset: 0x000259F0
		private ConstantWrapper BitwiseXor(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					int num = left.ToInt32();
					int num2 = right.ToInt32();
					result = new ConstantWrapper(Convert.ToDouble(num ^ num2), PrimitiveType.Number, left.Context.FlattenToStart());
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002785C File Offset: 0x00025A5C
		private ConstantWrapper LogicalAnd(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					result = (left.ToBoolean() ? right : left);
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000278A8 File Offset: 0x00025AA8
		private ConstantWrapper LogicalOr(ConstantWrapper left, ConstantWrapper right)
		{
			ConstantWrapper result = null;
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				try
				{
					result = (left.ToBoolean() ? left : right);
				}
				catch (InvalidCastException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000278F4 File Offset: 0x00025AF4
		private static bool OnlyHasConstantItems(ArrayLiteral arrayLiteral)
		{
			int count = arrayLiteral.Elements.Count;
			for (int i = 0; i < count; i++)
			{
				ConstantWrapper constantWrapper = arrayLiteral.Elements[i] as ConstantWrapper;
				if (constantWrapper == null || !constantWrapper.IsOkayToCombine)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002793C File Offset: 0x00025B3C
		private static string ComputeJoin(ArrayLiteral arrayLiteral, ConstantWrapper separatorNode)
		{
			string value = (separatorNode == null) ? "," : separatorNode.ToString();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < arrayLiteral.Elements.Count; i++)
			{
				if (i > 0 && !string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(arrayLiteral.Elements[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000279B0 File Offset: 0x00025BB0
		private int NodeLength(AstNode node)
		{
			string obj = OutputVisitor.Apply(node, this.m_parser.Settings);
			return obj.IfNotNull((string c) => c.Length);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000279F4 File Offset: 0x00025BF4
		public override void Visit(AstNodeList node)
		{
			if (node != null)
			{
				CommaOperator commaOperator = node.Parent as CommaOperator;
				AstNodeList astNodeList;
				if (commaOperator != null && (astNodeList = (commaOperator.Operand2 as AstNodeList)) != null)
				{
					for (int i = astNodeList.Count - ((node.Parent is Block) ? 1 : 2); i >= 0; i--)
					{
						if (astNodeList[i] is ConstantWrapper)
						{
							astNodeList.RemoveAt(i);
						}
					}
				}
				base.Visit(node);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00027A61 File Offset: 0x00025C61
		public override void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoBinaryOperator(node);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00027A74 File Offset: 0x00025C74
		private void DoBinaryOperator(BinaryOperator node)
		{
			if (this.m_parser.Settings.EvalLiteralExpressions && !node.IsAssign && node.OperatorToken != JSToken.In && node.OperatorToken != JSToken.InstanceOf)
			{
				if (node.OperatorToken == JSToken.StrictEqual || node.OperatorToken == JSToken.StrictNotEqual)
				{
					PrimitiveType primitiveType = node.Operand1.FindPrimitiveType();
					if (primitiveType != PrimitiveType.Other)
					{
						PrimitiveType primitiveType2 = node.Operand2.FindPrimitiveType();
						if (primitiveType2 != PrimitiveType.Other)
						{
							if (primitiveType != primitiveType2)
							{
								EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(node.OperatorToken != JSToken.StrictEqual, PrimitiveType.Boolean, node.Context));
								return;
							}
							node.OperatorToken = ((node.OperatorToken == JSToken.StrictEqual) ? JSToken.Equal : JSToken.NotEqual);
						}
					}
				}
				ConstantWrapper constantWrapper = node.Operand1 as ConstantWrapper;
				if (constantWrapper != null)
				{
					if (node.OperatorToken == JSToken.Comma)
					{
						ConstantWrapper constantWrapper2 = node.Operand2 as ConstantWrapper;
						if (constantWrapper2 != null)
						{
							if (!this.ReplaceMemberBracketWithDot(node, constantWrapper2))
							{
								EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, constantWrapper2);
								return;
							}
						}
						else
						{
							if (!(node is CommaOperator))
							{
								EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, node.Operand2);
								return;
							}
							AstNodeList astNodeList = node.Operand2 as AstNodeList;
							if (astNodeList == null)
							{
								EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, node.Operand2);
								return;
							}
							if (astNodeList.Count == 1)
							{
								EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, astNodeList[0]);
								return;
							}
							if (astNodeList.Count == 0)
							{
								EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, null);
								return;
							}
							AstNode astNode = astNodeList[0];
							astNodeList.RemoveAt(0);
							node.Operand1 = astNode;
							if (astNodeList.Count == 1)
							{
								astNode = astNodeList[0];
								astNodeList.RemoveAt(0);
								node.Operand2 = astNode;
								return;
							}
						}
					}
					else
					{
						ConstantWrapper constantWrapper3 = node.Operand2 as ConstantWrapper;
						if (constantWrapper3 != null)
						{
							this.EvalThisOperator(node, constantWrapper, constantWrapper3);
							return;
						}
						BinaryOperator binaryOperator = node.Operand2 as BinaryOperator;
						if (binaryOperator != null)
						{
							ConstantWrapper constantWrapper4 = binaryOperator.Operand1 as ConstantWrapper;
							if (constantWrapper4 != null)
							{
								this.EvalToTheRight(node, constantWrapper, constantWrapper4, binaryOperator);
								return;
							}
							ConstantWrapper constantWrapper5 = binaryOperator.Operand2 as ConstantWrapper;
							if (constantWrapper5 != null)
							{
								this.EvalFarToTheRight(node, constantWrapper, constantWrapper5, binaryOperator);
								return;
							}
						}
					}
				}
				else
				{
					ConstantWrapper constantWrapper6 = node.Operand2 as ConstantWrapper;
					if (constantWrapper6 != null)
					{
						BinaryOperator binaryOperator2 = node.Operand1 as BinaryOperator;
						if (binaryOperator2 != null)
						{
							ConstantWrapper constantWrapper7 = binaryOperator2.Operand2 as ConstantWrapper;
							if (constantWrapper7 != null)
							{
								this.EvalToTheLeft(node, constantWrapper6, constantWrapper7, binaryOperator2);
								return;
							}
							ConstantWrapper constantWrapper8 = binaryOperator2.Operand1 as ConstantWrapper;
							if (constantWrapper8 != null)
							{
								this.EvalFarToTheLeft(node, constantWrapper6, constantWrapper8, binaryOperator2);
								return;
							}
						}
						else if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.SimplifyStringToNumericConversion))
						{
							Lookup lookup = node.Operand1 as Lookup;
							if (lookup != null && node.OperatorToken == JSToken.Minus && constantWrapper6.IsIntegerLiteral && constantWrapper6.ToNumber() == 0.0)
							{
								UnaryOperator newNode = new UnaryOperator(node.Context)
								{
									Operand = lookup,
									OperatorToken = JSToken.FirstBinaryOperator
								};
								EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, newNode);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00027D68 File Offset: 0x00025F68
		public override void Visit(CallNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (!node.IsConstructor && !node.InBrackets)
				{
					Member member = node.Function as Member;
					if (member != null && string.CompareOrdinal(member.Name, "join") == 0 && node.Arguments.Count <= 1 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateLiteralJoins))
					{
						ArrayLiteral arrayLiteral = member.Root as ArrayLiteral;
						if (arrayLiteral != null && !arrayLiteral.MayHaveIssues)
						{
							ConstantWrapper separatorNode = null;
							if ((node.Arguments.Count == 0 || (separatorNode = (node.Arguments[0] as ConstantWrapper)) != null) && EvaluateLiteralVisitor.OnlyHasConstantItems(arrayLiteral))
							{
								string text = EvaluateLiteralVisitor.ComputeJoin(arrayLiteral, separatorNode);
								if (text.Length + 2 < this.NodeLength(node))
								{
									EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(text, PrimitiveType.String, node.Context));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00027E5A File Offset: 0x0002605A
		public override void Visit(Conditional node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoConditional(node);
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00027E70 File Offset: 0x00026070
		private void DoConditional(Conditional node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null)
				{
					try
					{
						EvaluateLiteralVisitor.ReplaceNodeCheckParens(node, constantWrapper.ToBoolean() ? node.TrueExpression : node.FalseExpression);
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00027ED8 File Offset: 0x000260D8
		public override void Visit(ConditionalCompilationElseIf node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoConditionalCompilationElseIf(node);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00027EEC File Offset: 0x000260EC
		private void DoConditionalCompilationElseIf(ConditionalCompilationElseIf node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null && constantWrapper.IsNotOneOrPositiveZero)
				{
					try
					{
						node.Condition = new ConstantWrapper(constantWrapper.ToBoolean() ? 1 : 0, PrimitiveType.Number, node.Condition.Context);
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00027F68 File Offset: 0x00026168
		public override void Visit(ConditionalCompilationIf node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoConditionalCompilationIf(node);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00027F7C File Offset: 0x0002617C
		private void DoConditionalCompilationIf(ConditionalCompilationIf node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null && constantWrapper.IsNotOneOrPositiveZero)
				{
					try
					{
						node.Condition = new ConstantWrapper(constantWrapper.ToBoolean() ? 1 : 0, PrimitiveType.Number, node.Condition.Context);
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00027FF8 File Offset: 0x000261F8
		public override void Visit(DoWhile node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoDoWhile(node);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002800C File Offset: 0x0002620C
		private void DoDoWhile(DoWhile node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null && constantWrapper.IsNotOneOrPositiveZero)
				{
					try
					{
						node.Condition = new ConstantWrapper(constantWrapper.ToBoolean() ? 1 : 0, PrimitiveType.Number, node.Condition.Context);
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00028088 File Offset: 0x00026288
		public override void Visit(ForNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoForNode(node);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002809C File Offset: 0x0002629C
		private void DoForNode(ForNode node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null)
				{
					try
					{
						if (constantWrapper.ToBoolean())
						{
							node.Condition = null;
						}
						else if (constantWrapper.IsNotOneOrPositiveZero)
						{
							node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002811C File Offset: 0x0002631C
		public override void Visit(IfNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoIfNode(node);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00028130 File Offset: 0x00026330
		private void DoIfNode(IfNode node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null && constantWrapper.IsNotOneOrPositiveZero)
				{
					try
					{
						node.Condition = new ConstantWrapper(constantWrapper.ToBoolean() ? 1 : 0, PrimitiveType.Number, node.Condition.Context);
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000281AC File Offset: 0x000263AC
		public override void Visit(Member node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (string.CompareOrdinal(node.Name, "length") == 0 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateLiteralLengths))
				{
					ConstantWrapper constantWrapper = null;
					ConstantWrapper constantWrapper2 = node.Root as ConstantWrapper;
					ArrayLiteral arrayLiteral;
					if (constantWrapper2 != null)
					{
						if (constantWrapper2.PrimitiveType == PrimitiveType.String && !constantWrapper2.MayHaveIssues)
						{
							constantWrapper = new ConstantWrapper(constantWrapper2.ToString().Length, PrimitiveType.Number, node.Context);
						}
					}
					else if ((arrayLiteral = (node.Root as ArrayLiteral)) != null && !arrayLiteral.MayHaveIssues)
					{
						int length = arrayLiteral.Length;
						if (length >= 0)
						{
							constantWrapper = new ConstantWrapper(length, PrimitiveType.Number, node.Context);
						}
					}
					if (constantWrapper != null)
					{
						node.Parent.ReplaceChild(node, constantWrapper);
					}
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00028280 File Offset: 0x00026480
		public override void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoUnaryNode(node);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00028294 File Offset: 0x00026494
		private void DoUnaryNode(UnaryOperator node)
		{
			if (!node.OperatorInConditionalCompilationComment && this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Operand as ConstantWrapper;
				switch (node.OperatorToken)
				{
				case JSToken.Void:
					if (constantWrapper != null)
					{
						node.Operand = new ConstantWrapper(0, PrimitiveType.Number, node.Context);
						return;
					}
					return;
				case JSToken.TypeOf:
					if (constantWrapper != null)
					{
						string value = null;
						if (constantWrapper.IsStringLiteral)
						{
							value = "string";
						}
						else if (constantWrapper.IsNumericLiteral)
						{
							value = "number";
						}
						else if (constantWrapper.IsBooleanLiteral)
						{
							value = "boolean";
						}
						else if (constantWrapper.Value == null)
						{
							value = "object";
						}
						if (!string.IsNullOrEmpty(value))
						{
							EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(value, PrimitiveType.String, node.Context));
							return;
						}
						return;
					}
					else
					{
						if (node.Operand is ObjectLiteral)
						{
							EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper("object", PrimitiveType.String, node.Context));
							return;
						}
						return;
					}
					break;
				case JSToken.LogicalNot:
					goto IL_17D;
				case JSToken.BitwiseNot:
					goto IL_152;
				case JSToken.FirstBinaryOperator:
					if (constantWrapper == null)
					{
						return;
					}
					try
					{
						EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(constantWrapper.ToNumber(), PrimitiveType.Number, node.Context));
						return;
					}
					catch (InvalidCastException)
					{
						return;
					}
					break;
				case JSToken.Minus:
					break;
				default:
					return;
				}
				if (constantWrapper == null)
				{
					return;
				}
				try
				{
					EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(-constantWrapper.ToNumber(), PrimitiveType.Number, node.Context));
					return;
				}
				catch (InvalidCastException)
				{
					return;
				}
				IL_152:
				if (constantWrapper == null)
				{
					return;
				}
				try
				{
					EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(Convert.ToDouble(~constantWrapper.ToInt32()), PrimitiveType.Number, node.Context));
					return;
				}
				catch (InvalidCastException)
				{
					return;
				}
				IL_17D:
				if (constantWrapper != null)
				{
					try
					{
						EvaluateLiteralVisitor.ReplaceNodeWithLiteral(node, new ConstantWrapper(!constantWrapper.ToBoolean(), PrimitiveType.Boolean, node.Context));
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002847C File Offset: 0x0002667C
		public override void Visit(WhileNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.DoWhileNode(node);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00028490 File Offset: 0x00026690
		private void DoWhileNode(WhileNode node)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.EvaluateNumericExpressions))
			{
				ConstantWrapper constantWrapper = node.Condition as ConstantWrapper;
				if (constantWrapper != null)
				{
					try
					{
						bool flag = constantWrapper.ToBoolean();
						if (flag)
						{
							if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.ChangeWhileToFor))
							{
								AstNode initializer = null;
								if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.MoveVarIntoFor))
								{
									Block block = node.Parent as Block;
									if (block != null)
									{
										int num = block.IndexOf(node);
										if (num > 0)
										{
											Var var = block[num - 1] as Var;
											if (var != null)
											{
												initializer = var;
												block.RemoveAt(num - 1);
											}
										}
									}
								}
								ForNode newNode = new ForNode(node.Context)
								{
									Initializer = initializer,
									Body = node.Body
								};
								node.Parent.ReplaceChild(node, newNode);
							}
							else
							{
								node.Condition = new ConstantWrapper(1, PrimitiveType.Number, node.Condition.Context);
							}
						}
						else if (constantWrapper.IsNotOneOrPositiveZero)
						{
							node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
						}
					}
					catch (InvalidCastException)
					{
					}
				}
			}
		}

		// Token: 0x0400031C RID: 796
		private JSParser m_parser;
	}
}
