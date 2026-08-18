using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000371 RID: 881
	internal sealed class MethodExpr : GroupAggregateExpr
	{
		// Token: 0x06003224 RID: 12836 RVA: 0x000C4E12 File Offset: 0x000C3012
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args) : this(expr, distinctKind, args, null)
		{
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000C4E1E File Offset: 0x000C301E
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args, NodeList<RelshipNavigationExpr> relationships) : base(distinctKind)
		{
			this._expr = expr;
			this._args = args;
			this._relationships = relationships;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000C4E3D File Offset: 0x000C303D
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000C4E45 File Offset: 0x000C3045
		internal NodeList<Node> Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000C4E4D File Offset: 0x000C304D
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null && this._relationships.Count > 0;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000C4E67 File Offset: 0x000C3067
		internal NodeList<RelshipNavigationExpr> Relationships
		{
			get
			{
				return this._relationships;
			}
		}

		// Token: 0x04001607 RID: 5639
		private readonly Node _expr;

		// Token: 0x04001608 RID: 5640
		private readonly NodeList<Node> _args;

		// Token: 0x04001609 RID: 5641
		private readonly NodeList<RelshipNavigationExpr> _relationships;
	}
}
