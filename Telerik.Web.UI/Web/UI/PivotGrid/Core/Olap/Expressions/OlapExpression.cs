using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006F9 RID: 1785
	internal abstract class OlapExpression
	{
		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06003F7E RID: 16254
		public abstract OlapExpressionType NodeType { get; }

		// Token: 0x06003F7F RID: 16255 RVA: 0x000C95C3 File Offset: 0x000C77C3
		protected internal virtual OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitExtension(this);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000C95CC File Offset: 0x000C77CC
		protected internal virtual OlapExpression VisitChildren(OlapExpressionVisitor visitor)
		{
			return this;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000C95CF File Offset: 0x000C77CF
		public override string ToString()
		{
			return OlapExpressionStringBuilder.ExpressionNodeToString(this);
		}
	}
}
