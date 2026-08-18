using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B8F RID: 7055
	internal class ExpressionParser
	{
		// Token: 0x04004C78 RID: 19576
		internal static readonly Expression TrueLiteral = Expression.Constant(true);

		// Token: 0x04004C79 RID: 19577
		internal static readonly Expression FalseLiteral = Expression.Constant(false);

		// Token: 0x04004C7A RID: 19578
		internal static readonly Expression NullLiteral = Expression.Constant(null);
	}
}
