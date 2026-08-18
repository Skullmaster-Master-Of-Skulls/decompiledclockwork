using System;
using System.Data.Common.CommandTrees;
using System.Linq.Expressions;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A0 RID: 416
	internal sealed class Binding
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x00068A11 File Offset: 0x00066C11
		internal Binding(Expression linqExpression, DbExpression cqtExpression)
		{
			EntityUtil.CheckArgumentNull<Expression>(linqExpression, "linqExpression");
			EntityUtil.CheckArgumentNull<DbExpression>(cqtExpression, "cqtExpression");
			this.LinqExpression = linqExpression;
			this.CqtExpression = cqtExpression;
		}

		// Token: 0x04000C16 RID: 3094
		internal readonly Expression LinqExpression;

		// Token: 0x04000C17 RID: 3095
		internal readonly DbExpression CqtExpression;
	}
}
