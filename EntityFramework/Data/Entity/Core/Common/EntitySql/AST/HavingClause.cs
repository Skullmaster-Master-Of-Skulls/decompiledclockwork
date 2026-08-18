using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000227 RID: 551
	internal sealed class HavingClause : Node
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x00050772 File Offset: 0x0004E972
		internal HavingClause(Node havingExpr, uint methodCallCounter)
		{
			this._havingExpr = havingExpr;
			this._methodCallCount = methodCallCounter;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00050788 File Offset: 0x0004E988
		internal Node HavingPredicate
		{
			get
			{
				return this._havingExpr;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00050790 File Offset: 0x0004E990
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x040005F7 RID: 1527
		private readonly Node _havingExpr;

		// Token: 0x040005F8 RID: 1528
		private readonly uint _methodCallCount;
	}
}
