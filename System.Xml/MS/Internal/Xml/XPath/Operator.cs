using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000152 RID: 338
	internal class Operator : AstNode
	{
		// Token: 0x060012B4 RID: 4788 RVA: 0x0005133A File Offset: 0x0005033A
		public Operator(Operator.Op op, AstNode opnd1, AstNode opnd2)
		{
			this.opType = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00051357 File Offset: 0x00050357
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Operator;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0005135A File Offset: 0x0005035A
		public override XPathResultType ReturnType
		{
			get
			{
				if (this.opType < Operator.Op.PLUS)
				{
					return XPathResultType.Boolean;
				}
				if (this.opType < Operator.Op.UNION)
				{
					return XPathResultType.Number;
				}
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00051374 File Offset: 0x00050374
		public Operator.Op OperatorType
		{
			get
			{
				return this.opType;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0005137C File Offset: 0x0005037C
		public AstNode Operand1
		{
			get
			{
				return this.opnd1;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00051384 File Offset: 0x00050384
		public AstNode Operand2
		{
			get
			{
				return this.opnd2;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		private Operator.Op opType;

		// Token: 0x04000BA9 RID: 2985
		private AstNode opnd1;

		// Token: 0x04000BAA RID: 2986
		private AstNode opnd2;

		// Token: 0x02000153 RID: 339
		public enum Op
		{
			// Token: 0x04000BAC RID: 2988
			LT,
			// Token: 0x04000BAD RID: 2989
			GT,
			// Token: 0x04000BAE RID: 2990
			LE,
			// Token: 0x04000BAF RID: 2991
			GE,
			// Token: 0x04000BB0 RID: 2992
			EQ,
			// Token: 0x04000BB1 RID: 2993
			NE,
			// Token: 0x04000BB2 RID: 2994
			OR,
			// Token: 0x04000BB3 RID: 2995
			AND,
			// Token: 0x04000BB4 RID: 2996
			PLUS,
			// Token: 0x04000BB5 RID: 2997
			MINUS,
			// Token: 0x04000BB6 RID: 2998
			MUL,
			// Token: 0x04000BB7 RID: 2999
			MOD,
			// Token: 0x04000BB8 RID: 3000
			DIV,
			// Token: 0x04000BB9 RID: 3001
			UNION,
			// Token: 0x04000BBA RID: 3002
			INVALID
		}
	}
}
