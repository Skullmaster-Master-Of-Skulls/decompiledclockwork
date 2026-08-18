using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022D RID: 557
	internal sealed class MethodExpr : GroupAggregateExpr
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x0005131D File Offset: 0x0004F51D
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args) : this(expr, distinctKind, args, null)
		{
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00051329 File Offset: 0x0004F529
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args, NodeList<RelshipNavigationExpr> relationships) : base(distinctKind)
		{
			this._expr = expr;
			this._args = args;
			this._relationships = relationships;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00051348 File Offset: 0x0004F548
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00051350 File Offset: 0x0004F550
		internal NodeList<Node> Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00051358 File Offset: 0x0004F558
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null && this._relationships.Count > 0;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00051372 File Offset: 0x0004F572
		internal NodeList<RelshipNavigationExpr> Relationships
		{
			get
			{
				return this._relationships;
			}
		}

		// Token: 0x0400061A RID: 1562
		private readonly Node _expr;

		// Token: 0x0400061B RID: 1563
		private readonly NodeList<Node> _args;

		// Token: 0x0400061C RID: 1564
		private readonly NodeList<RelshipNavigationExpr> _relationships;
	}
}
