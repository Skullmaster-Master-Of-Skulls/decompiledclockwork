using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000411 RID: 1041
	public sealed class DbProjectExpression : DbExpression
	{
		// Token: 0x060036C5 RID: 14021 RVA: 0x000D12B7 File Offset: 0x000CF4B7
		internal DbProjectExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression projection) : base(DbExpressionKind.Project, resultType)
		{
			this._input = input;
			this._projection = projection;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000D12D0 File Offset: 0x000CF4D0
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000D12D8 File Offset: 0x000CF4D8
		public DbExpression Projection
		{
			get
			{
				return this._projection;
			}
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000D12E0 File Offset: 0x000CF4E0
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000D12F7 File Offset: 0x000CF4F7
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001818 RID: 6168
		private readonly DbExpressionBinding _input;

		// Token: 0x04001819 RID: 6169
		private readonly DbExpression _projection;
	}
}
