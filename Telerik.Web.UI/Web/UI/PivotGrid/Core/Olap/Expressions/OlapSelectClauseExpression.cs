using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000704 RID: 1796
	internal class OlapSelectClauseExpression : OlapExpression
	{
		// Token: 0x06003FC9 RID: 16329 RVA: 0x000C9D3F File Offset: 0x000C7F3F
		internal OlapSelectClauseExpression(IEnumerable<OlapSelectQueryAxisClauseExpression> queryAxisClauses, OlapExpression from)
		{
			if (queryAxisClauses == null)
			{
				throw new ArgumentNullException("queryAxisClauses");
			}
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			this.queryAxisClauses = queryAxisClauses.ToList<OlapSelectQueryAxisClauseExpression>();
			this.From = from;
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000C9D76 File Offset: 0x000C7F76
		public IEnumerable<OlapSelectQueryAxisClauseExpression> QueryAxisClauses
		{
			get
			{
				return this.queryAxisClauses;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000C9D7E File Offset: 0x000C7F7E
		// (set) Token: 0x06003FCC RID: 16332 RVA: 0x000C9D86 File Offset: 0x000C7F86
		public OlapExpression From { get; private set; }

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x000C9D8F File Offset: 0x000C7F8F
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.SelectClause;
			}
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000C9D92 File Offset: 0x000C7F92
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitSelectClause(this);
		}

		// Token: 0x040010F0 RID: 4336
		private readonly IEnumerable<OlapSelectQueryAxisClauseExpression> queryAxisClauses;
	}
}
