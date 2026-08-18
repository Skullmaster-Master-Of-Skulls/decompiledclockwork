using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E2 RID: 226
	public abstract class DbUnaryExpression : DbExpression
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x0002553F File Offset: 0x0002373F
		internal DbUnaryExpression()
		{
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00025547 File Offset: 0x00023747
		internal DbUnaryExpression(DbExpressionKind kind, TypeUsage resultType, DbExpression argument) : base(kind, resultType, true)
		{
			this._argument = argument;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00025559 File Offset: 0x00023759
		public virtual DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x040001C6 RID: 454
		private readonly DbExpression _argument;
	}
}
