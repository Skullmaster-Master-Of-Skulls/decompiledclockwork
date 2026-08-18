using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000513 RID: 1299
	internal class XPathRelationExpr : XPathConjunctExpr
	{
		// Token: 0x0600316E RID: 12654 RVA: 0x000BE21B File Offset: 0x000BC41B
		internal XPathRelationExpr(RelationOperator op, XPathExpr left, XPathExpr right) : base(XPathExprType.Relational, ValueDataType.Boolean, left, right)
		{
			this.op = op;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000BE22E File Offset: 0x000BC42E
		// (set) Token: 0x06003170 RID: 12656 RVA: 0x000BE236 File Offset: 0x000BC436
		internal RelationOperator Op
		{
			get
			{
				return this.op;
			}
			set
			{
				this.op = value;
			}
		}

		// Token: 0x04002660 RID: 9824
		private RelationOperator op;
	}
}
