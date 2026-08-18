using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000409 RID: 1033
	public sealed class DbElementExpression : DbUnaryExpression
	{
		// Token: 0x0600369F RID: 13983 RVA: 0x000D1012 File Offset: 0x000CF212
		internal DbElementExpression(TypeUsage resultType, DbExpression argument) : base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = false;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000D1025 File Offset: 0x000CF225
		internal DbElementExpression(TypeUsage resultType, DbExpression argument, bool unwrapSingleProperty) : base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = unwrapSingleProperty;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000D1038 File Offset: 0x000CF238
		internal bool IsSinglePropertyUnwrapped
		{
			get
			{
				return this._singlePropertyUnwrapped;
			}
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000D1040 File Offset: 0x000CF240
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000D1057 File Offset: 0x000CF257
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400180B RID: 6155
		private bool _singlePropertyUnwrapped;
	}
}
