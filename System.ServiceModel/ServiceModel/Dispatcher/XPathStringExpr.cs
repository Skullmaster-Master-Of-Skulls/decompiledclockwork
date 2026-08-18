using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051A RID: 1306
	internal class XPathStringExpr : XPathLiteralExpr
	{
		// Token: 0x06003183 RID: 12675 RVA: 0x000BE37E File Offset: 0x000BC57E
		internal XPathStringExpr(string literal) : base(XPathExprType.String, ValueDataType.String)
		{
			this.literal = literal;
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000BE390 File Offset: 0x000BC590
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000BE398 File Offset: 0x000BC598
		internal string String
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x04002668 RID: 9832
		private string literal;
	}
}
