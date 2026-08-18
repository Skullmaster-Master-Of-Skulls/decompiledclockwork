using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F1 RID: 1009
	public abstract class DbUnaryExpression : DbExpression
	{
		// Token: 0x0600360F RID: 13839 RVA: 0x000D0412 File Offset: 0x000CE612
		internal DbUnaryExpression(DbExpressionKind kind, TypeUsage resultType, DbExpression argument) : base(kind, resultType)
		{
			this._argument = argument;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000D0423 File Offset: 0x000CE623
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x040017F5 RID: 6133
		private readonly DbExpression _argument;
	}
}
