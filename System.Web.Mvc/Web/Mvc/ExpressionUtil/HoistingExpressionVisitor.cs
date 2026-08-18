using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B1 RID: 177
	internal sealed class HoistingExpressionVisitor<TIn, TOut> : ExpressionVisitor
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000DB85 File Offset: 0x0000BD85
		private HoistingExpressionVisitor()
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public static Expression<Hoisted<TIn, TOut>> Hoist(Expression<Func<TIn, TOut>> expr)
		{
			HoistingExpressionVisitor<TIn, TOut> hoistingExpressionVisitor = new HoistingExpressionVisitor<TIn, TOut>();
			Expression body = hoistingExpressionVisitor.Visit(expr.Body);
			return Expression.Lambda<Hoisted<TIn, TOut>>(body, new ParameterExpression[]
			{
				expr.Parameters[0],
				HoistingExpressionVisitor<TIn, TOut>._hoistedConstantsParamExpr
			});
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		protected override Expression VisitConstant(ConstantExpression node)
		{
			return Expression.Convert(Expression.Property(HoistingExpressionVisitor<TIn, TOut>._hoistedConstantsParamExpr, "Item", new Expression[]
			{
				Expression.Constant(this._numConstantsProcessed++)
			}), node.Type);
		}

		// Token: 0x04000150 RID: 336
		private static readonly ParameterExpression _hoistedConstantsParamExpr = Expression.Parameter(typeof(List<object>), "hoistedConstants");

		// Token: 0x04000151 RID: 337
		private int _numConstantsProcessed;
	}
}
