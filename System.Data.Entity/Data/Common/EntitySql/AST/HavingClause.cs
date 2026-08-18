using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200037F RID: 895
	internal sealed class HavingClause : Node
	{
		// Token: 0x0600324B RID: 12875 RVA: 0x000C509F File Offset: 0x000C329F
		internal HavingClause(Node havingExpr, uint methodCallCounter)
		{
			this._havingExpr = havingExpr;
			this._methodCallCount = methodCallCounter;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000C50B5 File Offset: 0x000C32B5
		internal Node HavingPredicate
		{
			get
			{
				return this._havingExpr;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000C50BD File Offset: 0x000C32BD
		internal bool HasMethodCall
		{
			get
			{
				return this._methodCallCount > 0U;
			}
		}

		// Token: 0x04001637 RID: 5687
		private readonly Node _havingExpr;

		// Token: 0x04001638 RID: 5688
		private readonly uint _methodCallCount;
	}
}
