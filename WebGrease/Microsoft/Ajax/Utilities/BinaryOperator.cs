using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000067 RID: 103
	public class BinaryOperator : Expression
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0002163D File Offset: 0x0001F83D
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00021687 File Offset: 0x0001F887
		public AstNode Operand1
		{
			get
			{
				return this.m_operand1;
			}
			set
			{
				this.m_operand1.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_operand1 = value;
				this.m_operand1.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x000216C0 File Offset: 0x0001F8C0
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x00021707 File Offset: 0x0001F907
		public AstNode Operand2
		{
			get
			{
				return this.m_operand2;
			}
			set
			{
				this.m_operand2.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_operand2 = value;
				this.m_operand2.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00021740 File Offset: 0x0001F940
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00021748 File Offset: 0x0001F948
		public JSToken OperatorToken { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00021751 File Offset: 0x0001F951
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00021759 File Offset: 0x0001F959
		public Context OperatorContext { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0002176A File Offset: 0x0001F96A
		public override Context TerminatingContext
		{
			get
			{
				Context result;
				if ((result = base.TerminatingContext) == null)
				{
					result = this.Operand2.IfNotNull((AstNode n) => n.TerminatingContext);
				}
				return result;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002179E File Offset: 0x0001F99E
		public BinaryOperator(Context context) : base(context)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000217A8 File Offset: 0x0001F9A8
		public override OperatorPrecedence Precedence
		{
			get
			{
				switch (this.OperatorToken)
				{
				case JSToken.FirstBinaryOperator:
				case JSToken.Minus:
					return OperatorPrecedence.Additive;
				case JSToken.Multiply:
				case JSToken.Divide:
				case JSToken.Modulo:
					return OperatorPrecedence.Multiplicative;
				case JSToken.BitwiseAnd:
					return OperatorPrecedence.BitwiseAnd;
				case JSToken.BitwiseOr:
					return OperatorPrecedence.BitwiseOr;
				case JSToken.BitwiseXor:
					return OperatorPrecedence.BitwiseXor;
				case JSToken.LeftShift:
				case JSToken.RightShift:
				case JSToken.UnsignedRightShift:
					return OperatorPrecedence.Shift;
				case JSToken.Equal:
				case JSToken.NotEqual:
				case JSToken.StrictEqual:
				case JSToken.StrictNotEqual:
					return OperatorPrecedence.Equality;
				case JSToken.LessThan:
				case JSToken.LessThanEqual:
				case JSToken.GreaterThan:
				case JSToken.GreaterThanEqual:
				case JSToken.InstanceOf:
				case JSToken.In:
					return OperatorPrecedence.Relational;
				case JSToken.LogicalAnd:
					return OperatorPrecedence.LogicalAnd;
				case JSToken.LogicalOr:
					return OperatorPrecedence.LogicalOr;
				case JSToken.Comma:
					return OperatorPrecedence.Comma;
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
					return OperatorPrecedence.Assignment;
				default:
					return OperatorPrecedence.None;
				}
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00021878 File Offset: 0x0001FA78
		public override PrimitiveType FindPrimitiveType()
		{
			switch (this.OperatorToken)
			{
			case JSToken.FirstBinaryOperator:
			case JSToken.PlusAssign:
			{
				PrimitiveType primitiveType = this.Operand1.FindPrimitiveType();
				PrimitiveType primitiveType2 = this.Operand2.FindPrimitiveType();
				if (primitiveType == PrimitiveType.String || primitiveType2 == PrimitiveType.String)
				{
					return PrimitiveType.String;
				}
				if (primitiveType == PrimitiveType.Other || primitiveType2 == PrimitiveType.Other)
				{
					return PrimitiveType.Other;
				}
				return PrimitiveType.Number;
			}
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
				return PrimitiveType.Number;
			case JSToken.Equal:
			case JSToken.NotEqual:
			case JSToken.StrictEqual:
			case JSToken.StrictNotEqual:
			case JSToken.LessThan:
			case JSToken.LessThanEqual:
			case JSToken.GreaterThan:
			case JSToken.GreaterThanEqual:
			case JSToken.InstanceOf:
			case JSToken.In:
				return PrimitiveType.Boolean;
			case JSToken.LogicalAnd:
			case JSToken.LogicalOr:
			{
				PrimitiveType primitiveType = this.Operand1.FindPrimitiveType();
				if (primitiveType != PrimitiveType.Other && primitiveType == this.Operand2.FindPrimitiveType())
				{
					return primitiveType;
				}
				return PrimitiveType.Other;
			}
			case JSToken.Comma:
			case JSToken.Assign:
				return this.Operand2.FindPrimitiveType();
			default:
				return PrimitiveType.Other;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00021988 File Offset: 0x0001FB88
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Operand1, this.Operand2, null, null);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0002199D File Offset: 0x0001FB9D
		public override void Accept(IVisitor visitor)
		{
			if (visitor == null)
			{
				return;
			}
			visitor.Visit(this);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000219AA File Offset: 0x0001FBAA
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Operand1 == oldNode)
			{
				this.Operand1 = newNode;
				return true;
			}
			if (this.Operand2 == oldNode)
			{
				this.Operand2 = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000219D4 File Offset: 0x0001FBD4
		public override AstNode LeftHandSide
		{
			get
			{
				if (this.OperatorToken != JSToken.Comma)
				{
					return this.Operand1.LeftHandSide;
				}
				AstNodeList astNodeList = this.Operand2 as AstNodeList;
				if (astNodeList != null && astNodeList.Count > 0)
				{
					return astNodeList[astNodeList.Count - 1].LeftHandSide;
				}
				return this.Operand2.LeftHandSide;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00021A30 File Offset: 0x0001FC30
		public void SwapOperands()
		{
			AstNode operand = this.m_operand1;
			this.m_operand1 = this.m_operand2;
			this.m_operand2 = operand;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00021A58 File Offset: 0x0001FC58
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			BinaryOperator binaryOperator = otherNode as BinaryOperator;
			return binaryOperator != null && this.OperatorToken == binaryOperator.OperatorToken && this.Operand1.IsEquivalentTo(binaryOperator.Operand1) && this.Operand2.IsEquivalentTo(binaryOperator.Operand2);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		public bool IsAssign
		{
			get
			{
				switch (this.OperatorToken)
				{
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
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00021AF6 File Offset: 0x0001FCF6
		internal override string GetFunctionGuess(AstNode target)
		{
			if (this.Operand2 != target)
			{
				return string.Empty;
			}
			if (!this.IsAssign)
			{
				return base.Parent.GetFunctionGuess(this);
			}
			return this.Operand1.GetFunctionGuess(this);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00021B28 File Offset: 0x0001FD28
		public override bool ContainsInOperator
		{
			get
			{
				return this.OperatorToken == JSToken.In || this.Operand1.ContainsInOperator || this.Operand2.ContainsInOperator;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00021B60 File Offset: 0x0001FD60
		public override bool IsConstant
		{
			get
			{
				if (this.Operand1.IfNotNull((AstNode o) => o.IsConstant))
				{
					return this.Operand2.IfNotNull((AstNode o) => o.IsConstant);
				}
				return false;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00021BC4 File Offset: 0x0001FDC4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				(this.Operand1 == null) ? "<null>" : this.Operand1.ToString(),
				' ',
				OutputVisitor.OperatorString(this.OperatorToken),
				' ',
				(this.Operand2 == null) ? "<null>" : this.Operand2.ToString()
			});
		}

		// Token: 0x04000265 RID: 613
		private AstNode m_operand1;

		// Token: 0x04000266 RID: 614
		private AstNode m_operand2;
	}
}
