using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000102 RID: 258
	public sealed class DbQuantifierExpression : DbExpression
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x00025E54 File Offset: 0x00024054
		internal DbQuantifierExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpressionBinding input, DbExpression predicate) : base(kind, booleanResultType, true)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00025E6E File Offset: 0x0002406E
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00025E76 File Offset: 0x00024076
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00025E7E File Offset: 0x0002407E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00025E93 File Offset: 0x00024093
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001EE RID: 494
		private readonly DbExpressionBinding _input;

		// Token: 0x040001EF RID: 495
		private readonly DbExpression _predicate;
	}
}
