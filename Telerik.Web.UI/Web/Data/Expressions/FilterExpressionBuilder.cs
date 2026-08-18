using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAE RID: 7086
	internal abstract class FilterExpressionBuilder : ExpressionBuilderBase
	{
		// Token: 0x0601122B RID: 70187 RVA: 0x003C7709 File Offset: 0x003C5909
		protected FilterExpressionBuilder(ParameterExpression parameterExpression) : base(parameterExpression.Type)
		{
			base.ParameterExpression = parameterExpression;
		}

		// Token: 0x0601122C RID: 70188
		public abstract Expression CreateBodyExpression();

		// Token: 0x0601122D RID: 70189 RVA: 0x003C7720 File Offset: 0x003C5920
		public LambdaExpression CreateFilterExpression()
		{
			Expression body = this.CreateBodyExpression();
			return Expression.Lambda(body, new ParameterExpression[]
			{
				base.ParameterExpression
			});
		}
	}
}
