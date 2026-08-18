using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051B RID: 1307
	internal class XPathNumberExpr : XPathLiteralExpr
	{
		// Token: 0x06003186 RID: 12678 RVA: 0x000BE3A0 File Offset: 0x000BC5A0
		internal XPathNumberExpr(double literal) : base(XPathExprType.Number, ValueDataType.Double)
		{
			this.literal = literal;
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000BE3B2 File Offset: 0x000BC5B2
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000BE3BF File Offset: 0x000BC5BF
		internal double Number
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x04002669 RID: 9833
		private double literal;
	}
}
