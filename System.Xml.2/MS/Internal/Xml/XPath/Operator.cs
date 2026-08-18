using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000030 RID: 48
	internal class Operator : AstNode
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public static Operator.Op InvertOperator(Operator.Op op)
		{
			return Operator.invertOp[(int)op];
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005CDC File Offset: 0x00003EDC
		public Operator(Operator.Op op, AstNode opnd1, AstNode opnd2)
		{
			this.opType = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005CF9 File Offset: 0x00003EF9
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Operator;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005CFC File Offset: 0x00003EFC
		public override XPathResultType ReturnType
		{
			get
			{
				if (this.opType <= Operator.Op.GE)
				{
					return XPathResultType.Boolean;
				}
				if (this.opType <= Operator.Op.MOD)
				{
					return XPathResultType.Number;
				}
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005D16 File Offset: 0x00003F16
		public Operator.Op OperatorType
		{
			get
			{
				return this.opType;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005D1E File Offset: 0x00003F1E
		public AstNode Operand1
		{
			get
			{
				return this.opnd1;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005D26 File Offset: 0x00003F26
		public AstNode Operand2
		{
			get
			{
				return this.opnd2;
			}
		}

		// Token: 0x040000AF RID: 175
		private static Operator.Op[] invertOp = new Operator.Op[]
		{
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.EQ,
			Operator.Op.NE,
			Operator.Op.GT,
			Operator.Op.GE,
			Operator.Op.LT,
			Operator.Op.LE
		};

		// Token: 0x040000B0 RID: 176
		private Operator.Op opType;

		// Token: 0x040000B1 RID: 177
		private AstNode opnd1;

		// Token: 0x040000B2 RID: 178
		private AstNode opnd2;

		// Token: 0x020002FF RID: 767
		public enum Op
		{
			// Token: 0x040013FE RID: 5118
			INVALID,
			// Token: 0x040013FF RID: 5119
			OR,
			// Token: 0x04001400 RID: 5120
			AND,
			// Token: 0x04001401 RID: 5121
			EQ,
			// Token: 0x04001402 RID: 5122
			NE,
			// Token: 0x04001403 RID: 5123
			LT,
			// Token: 0x04001404 RID: 5124
			LE,
			// Token: 0x04001405 RID: 5125
			GT,
			// Token: 0x04001406 RID: 5126
			GE,
			// Token: 0x04001407 RID: 5127
			PLUS,
			// Token: 0x04001408 RID: 5128
			MINUS,
			// Token: 0x04001409 RID: 5129
			MUL,
			// Token: 0x0400140A RID: 5130
			DIV,
			// Token: 0x0400140B RID: 5131
			MOD,
			// Token: 0x0400140C RID: 5132
			UNION
		}
	}
}
