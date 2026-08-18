using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000404 RID: 1028
	public sealed class DbLikeExpression : DbExpression
	{
		// Token: 0x0600368B RID: 13963 RVA: 0x000D0EA6 File Offset: 0x000CF0A6
		internal DbLikeExpression(TypeUsage booleanResultType, DbExpression input, DbExpression pattern, DbExpression escape) : base(DbExpressionKind.Like, booleanResultType)
		{
			this._argument = input;
			this._pattern = pattern;
			this._escape = escape;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600368C RID: 13964 RVA: 0x000D0EC7 File Offset: 0x000CF0C7
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000D0ECF File Offset: 0x000CF0CF
		public DbExpression Pattern
		{
			get
			{
				return this._pattern;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000D0ED7 File Offset: 0x000CF0D7
		public DbExpression Escape
		{
			get
			{
				return this._escape;
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000D0EDF File Offset: 0x000CF0DF
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000D0EF6 File Offset: 0x000CF0F6
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001806 RID: 6150
		private readonly DbExpression _argument;

		// Token: 0x04001807 RID: 6151
		private readonly DbExpression _pattern;

		// Token: 0x04001808 RID: 6152
		private readonly DbExpression _escape;
	}
}
