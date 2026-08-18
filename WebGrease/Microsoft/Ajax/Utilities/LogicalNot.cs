using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B2 RID: 178
	public class LogicalNot : TreeVisitor
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00036DA2 File Offset: 0x00034FA2
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00036DAA File Offset: 0x00034FAA
		public bool MinifyBooleans { get; set; }

		// Token: 0x06000B6C RID: 2924 RVA: 0x00036DB3 File Offset: 0x00034FB3
		public LogicalNot(AstNode node) : this(node, null)
		{
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00036DD8 File Offset: 0x00034FD8
		public LogicalNot(AstNode node, CodeSettings codeSettings)
		{
			this.m_expression = node;
			this.MinifyBooleans = codeSettings.IfNotNull((CodeSettings settings) => settings.MinifyCode && settings.IsModificationAllowed(TreeModifications.BooleanLiteralsToNotOperators));
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00036E10 File Offset: 0x00035010
		public int Measure()
		{
			this.m_measure = true;
			this.m_delta = 0;
			this.m_expression.Accept(this);
			return this.m_delta;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00036E32 File Offset: 0x00035032
		public void Apply()
		{
			this.m_measure = false;
			this.m_expression.Accept(this);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00036E48 File Offset: 0x00035048
		public static void Apply(AstNode node, CodeSettings codeSettings)
		{
			LogicalNot logicalNot = new LogicalNot(node, codeSettings);
			logicalNot.Apply();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00036E64 File Offset: 0x00035064
		private static void WrapWithLogicalNot(AstNode operand)
		{
			operand.Parent.ReplaceChild(operand, new UnaryOperator(operand.Context)
			{
				Operand = operand,
				OperatorToken = JSToken.LogicalNot
			});
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00036E9A File Offset: 0x0003509A
		private void TypicalHandler(AstNode node)
		{
			if (node != null)
			{
				if (this.m_measure)
				{
					this.m_delta++;
					return;
				}
				LogicalNot.WrapWithLogicalNot(node);
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00036EBC File Offset: 0x000350BC
		public override void Visit(AstNodeList node)
		{
			if (node != null && node.Count > 0)
			{
				node[node.Count - 1].Accept(this);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00036EDE File Offset: 0x000350DE
		public override void Visit(ArrayLiteral node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00036EE7 File Offset: 0x000350E7
		public override void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				if (this.m_measure)
				{
					this.MeasureBinaryOperator(node);
					return;
				}
				this.ConvertBinaryOperator(node);
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00036F04 File Offset: 0x00035104
		private void MeasureBinaryOperator(BinaryOperator node)
		{
			switch (node.OperatorToken)
			{
			case JSToken.FirstBinaryOperator:
			case JSToken.Minus:
			case JSToken.Multiply:
			case JSToken.Divide:
			case JSToken.Modulo:
			case JSToken.BitwiseAnd:
			case JSToken.BitwiseOr:
			case JSToken.BitwiseXor:
			case JSToken.LeftShift:
			case JSToken.RightShift:
			case JSToken.UnsignedRightShift:
			case JSToken.LessThan:
			case JSToken.LessThanEqual:
			case JSToken.GreaterThan:
			case JSToken.GreaterThanEqual:
			case JSToken.InstanceOf:
			case JSToken.In:
			case JSToken.Assign:
			case JSToken.PlusAssign:
			case JSToken.MinusAssign:
			case JSToken.MultiplyAssign:
			case JSToken.DivideAssign:
			case JSToken.ModuloAssign:
			case JSToken.BitwiseAndAssign:
			case JSToken.BitwiseOrAssign:
			case JSToken.BitwiseXorAssign:
			case JSToken.LeftShiftAssign:
			case JSToken.RightShiftAssign:
			case JSToken.UnsignedRightShiftAssign:
				this.m_delta += 3;
				return;
			case JSToken.Equal:
			case JSToken.NotEqual:
			case JSToken.StrictEqual:
			case JSToken.StrictNotEqual:
				break;
			case JSToken.LogicalAnd:
			case JSToken.LogicalOr:
				if (node.Parent is Block || (node.Parent is CommaOperator && node.Parent.Parent is Block))
				{
					if (node.Operand1 != null)
					{
						node.Operand1.Accept(this);
						return;
					}
				}
				else
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
				break;
			case JSToken.Comma:
				node.Operand2.Accept(this);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00037038 File Offset: 0x00035238
		private void ConvertBinaryOperator(BinaryOperator node)
		{
			switch (node.OperatorToken)
			{
			case JSToken.FirstBinaryOperator:
			case JSToken.Minus:
			case JSToken.Multiply:
			case JSToken.Divide:
			case JSToken.Modulo:
			case JSToken.BitwiseAnd:
			case JSToken.BitwiseOr:
			case JSToken.BitwiseXor:
			case JSToken.LeftShift:
			case JSToken.RightShift:
			case JSToken.UnsignedRightShift:
			case JSToken.LessThan:
			case JSToken.LessThanEqual:
			case JSToken.GreaterThan:
			case JSToken.GreaterThanEqual:
			case JSToken.InstanceOf:
			case JSToken.In:
			case JSToken.Assign:
			case JSToken.PlusAssign:
			case JSToken.MinusAssign:
			case JSToken.MultiplyAssign:
			case JSToken.DivideAssign:
			case JSToken.ModuloAssign:
			case JSToken.BitwiseAndAssign:
			case JSToken.BitwiseOrAssign:
			case JSToken.BitwiseXorAssign:
			case JSToken.LeftShiftAssign:
			case JSToken.RightShiftAssign:
			case JSToken.UnsignedRightShiftAssign:
				LogicalNot.WrapWithLogicalNot(node);
				return;
			case JSToken.Equal:
				node.OperatorToken = JSToken.NotEqual;
				return;
			case JSToken.NotEqual:
				node.OperatorToken = JSToken.Equal;
				return;
			case JSToken.StrictEqual:
				node.OperatorToken = JSToken.StrictNotEqual;
				return;
			case JSToken.StrictNotEqual:
				node.OperatorToken = JSToken.StrictEqual;
				return;
			case JSToken.LogicalAnd:
			case JSToken.LogicalOr:
				if (node.Parent is Block || (node.Parent is CommaOperator && node.Parent.Parent is Block))
				{
					if (node.Operand1 != null)
					{
						node.Operand1.Accept(this);
					}
				}
				else
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
				node.OperatorToken = ((node.OperatorToken == JSToken.LogicalAnd) ? JSToken.LogicalOr : JSToken.LogicalAnd);
				return;
			case JSToken.Comma:
				node.Operand2.Accept(this);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003719E File Offset: 0x0003539E
		public override void Visit(CallNode node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000371A8 File Offset: 0x000353A8
		public override void Visit(Conditional node)
		{
			if (node != null)
			{
				LogicalNot logicalNot = new LogicalNot(node.TrueExpression)
				{
					MinifyBooleans = this.MinifyBooleans
				};
				LogicalNot logicalNot2 = new LogicalNot(node.FalseExpression)
				{
					MinifyBooleans = this.MinifyBooleans
				};
				int num = logicalNot.Measure() + logicalNot2.Measure();
				if (this.m_measure)
				{
					this.m_delta += ((num > 3) ? 3 : num);
					return;
				}
				if (num > 3)
				{
					LogicalNot.WrapWithLogicalNot(node);
					return;
				}
				node.TrueExpression.Accept(this);
				node.FalseExpression.Accept(this);
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00037244 File Offset: 0x00035444
		public override void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				if (node.PrimitiveType == PrimitiveType.Boolean)
				{
					if (!this.m_measure)
					{
						node.Value = !node.ToBoolean();
						return;
					}
					if (!this.MinifyBooleans)
					{
						this.m_delta += (node.ToBoolean() ? 1 : -1);
						return;
					}
				}
				else
				{
					this.TypicalHandler(node);
				}
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000372A4 File Offset: 0x000354A4
		public override void Visit(GroupingOperator node)
		{
			if (node != null)
			{
				if (this.m_measure)
				{
					int num = this.m_delta + 1;
					node.Operand.Accept(this);
					if (this.m_delta > num)
					{
						this.m_delta = num;
						return;
					}
				}
				else
				{
					this.m_measure = true;
					this.m_delta = 0;
					node.Operand.Accept(this);
					this.m_measure = false;
					if (this.m_delta > 1)
					{
						LogicalNot.WrapWithLogicalNot(node);
						return;
					}
					node.Parent.ReplaceChild(node, node.Operand);
					node.Operand.Accept(this);
				}
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00037332 File Offset: 0x00035532
		public override void Visit(Lookup node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003733B File Offset: 0x0003553B
		public override void Visit(Member node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00037344 File Offset: 0x00035544
		public override void Visit(ObjectLiteral node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003734D File Offset: 0x0003554D
		public override void Visit(RegExpLiteral node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00037356 File Offset: 0x00035556
		public override void Visit(ThisLiteral node)
		{
			this.TypicalHandler(node);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00037360 File Offset: 0x00035560
		public override void Visit(UnaryOperator node)
		{
			if (node != null && !node.OperatorInConditionalCompilationComment)
			{
				if (node.OperatorToken == JSToken.LogicalNot)
				{
					if (this.m_measure)
					{
						this.m_delta--;
						if (node.Operand is BinaryOperator || node.Operand is Conditional || node.Operand is GroupingOperator)
						{
							this.m_delta -= 2;
							return;
						}
					}
					else
					{
						GroupingOperator groupingOperator = node.Operand as GroupingOperator;
						if (groupingOperator != null)
						{
							node.Parent.ReplaceChild(node, groupingOperator.Operand);
							return;
						}
						node.Parent.ReplaceChild(node, node.Operand);
						return;
					}
				}
				else
				{
					this.TypicalHandler(node);
				}
			}
		}

		// Token: 0x040004C1 RID: 1217
		private AstNode m_expression;

		// Token: 0x040004C2 RID: 1218
		private bool m_measure;

		// Token: 0x040004C3 RID: 1219
		private int m_delta;
	}
}
