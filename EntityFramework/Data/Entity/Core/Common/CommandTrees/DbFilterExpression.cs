using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000EE RID: 238
	public sealed class DbFilterExpression : DbExpression
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0002583A File Offset: 0x00023A3A
		internal DbFilterExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression predicate) : base(DbExpressionKind.Filter, resultType, true)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00025854 File Offset: 0x00023A54
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0002585C File Offset: 0x00023A5C
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00025864 File Offset: 0x00023A64
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00025879 File Offset: 0x00023A79
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001D3 RID: 467
		private readonly DbExpressionBinding _input;

		// Token: 0x040001D4 RID: 468
		private readonly DbExpression _predicate;
	}
}
