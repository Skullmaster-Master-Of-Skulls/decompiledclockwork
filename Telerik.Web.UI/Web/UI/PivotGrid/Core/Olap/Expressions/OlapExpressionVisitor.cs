using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006FE RID: 1790
	internal class OlapExpressionVisitor
	{
		// Token: 0x06003F96 RID: 16278 RVA: 0x000C96B0 File Offset: 0x000C78B0
		public virtual OlapExpression Visit(OlapExpression node)
		{
			if (node != null)
			{
				return node.Accept(this);
			}
			return null;
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000C96BE File Offset: 0x000C78BE
		protected internal virtual OlapExpression VisitExtension(OlapExpression node)
		{
			return node.VisitChildren(this);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000C96C7 File Offset: 0x000C78C7
		protected internal virtual OlapExpression VisitConstant(OlapConstantExpression node)
		{
			return node;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000C96CA File Offset: 0x000C78CA
		protected internal virtual OlapExpression VisitIdentifier(OlapIdentifierExpression node)
		{
			return node;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000C96CD File Offset: 0x000C78CD
		protected internal virtual OlapExpression VisitWrapper(OlapWrapperExpression node)
		{
			return node;
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000C96D0 File Offset: 0x000C78D0
		protected internal virtual OlapExpression VisitBinary(OlapBinaryExpression node)
		{
			return node;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000C96D3 File Offset: 0x000C78D3
		protected internal virtual OlapExpression VisitSelectQueryAxisClause(OlapSelectQueryAxisClauseExpression node)
		{
			return node;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000C96D6 File Offset: 0x000C78D6
		protected internal virtual OlapExpression VisitSelectClause(OlapSelectClauseExpression node)
		{
			return node;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000C96D9 File Offset: 0x000C78D9
		protected internal virtual OlapExpression VisitMemberFunction(OlapMemberFuntionExpression node)
		{
			return node;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000C96DC File Offset: 0x000C78DC
		protected internal virtual OlapExpression VisitFunction(OlapFunctionExpression node)
		{
			return node;
		}
	}
}
