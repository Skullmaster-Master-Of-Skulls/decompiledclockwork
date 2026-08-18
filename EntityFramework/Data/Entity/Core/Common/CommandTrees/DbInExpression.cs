using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000126 RID: 294
	public class DbInExpression : DbExpression
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0002F226 File Offset: 0x0002D426
		internal DbInExpression(TypeUsage booleanResultType, DbExpression item, DbExpressionList list) : base(DbExpressionKind.In, booleanResultType, true)
		{
			this._item = item;
			this._list = list;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0002F240 File Offset: 0x0002D440
		public DbExpression Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0002F248 File Offset: 0x0002D448
		public IList<DbExpression> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002F250 File Offset: 0x0002D450
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002F265 File Offset: 0x0002D465
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000294 RID: 660
		private readonly DbExpression _item;

		// Token: 0x04000295 RID: 661
		private readonly DbExpressionList _list;
	}
}
