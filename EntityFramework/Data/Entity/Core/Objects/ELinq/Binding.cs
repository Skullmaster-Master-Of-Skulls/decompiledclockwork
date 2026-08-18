using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200054D RID: 1357
	internal sealed class Binding
	{
		// Token: 0x060034AB RID: 13483 RVA: 0x000F908C File Offset: 0x000F728C
		internal Binding(Expression linqExpression, DbExpression cqtExpression)
		{
			this.LinqExpression = linqExpression;
			this.CqtExpression = cqtExpression;
		}

		// Token: 0x040013B4 RID: 5044
		internal readonly Expression LinqExpression;

		// Token: 0x040013B5 RID: 5045
		internal readonly DbExpression CqtExpression;
	}
}
