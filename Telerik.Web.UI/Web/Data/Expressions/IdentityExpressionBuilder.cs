using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB5 RID: 7093
	internal class IdentityExpressionBuilder : ExpressionBuilderBase
	{
		// Token: 0x0601126E RID: 70254 RVA: 0x003C84C7 File Offset: 0x003C66C7
		public IdentityExpressionBuilder(Type itemType) : base(itemType)
		{
		}

		// Token: 0x0601126F RID: 70255 RVA: 0x003C84D0 File Offset: 0x003C66D0
		internal LambdaExpression CreateLambdaExpression()
		{
			return Expression.Lambda(base.ParameterExpression, new ParameterExpression[]
			{
				base.ParameterExpression
			});
		}
	}
}
