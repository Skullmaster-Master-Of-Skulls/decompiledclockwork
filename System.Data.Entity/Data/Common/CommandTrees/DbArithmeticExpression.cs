using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FB RID: 1019
	public sealed class DbArithmeticExpression : DbExpression
	{
		// Token: 0x0600366A RID: 13930 RVA: 0x000D0C45 File Offset: 0x000CEE45
		internal DbArithmeticExpression(DbExpressionKind kind, TypeUsage numericResultType, DbExpressionList args) : base(kind, numericResultType)
		{
			this._args = args;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000D0C56 File Offset: 0x000CEE56
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000D0C5E File Offset: 0x000CEE5E
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D0C75 File Offset: 0x000CEE75
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001800 RID: 6144
		private readonly DbExpressionList _args;
	}
}
