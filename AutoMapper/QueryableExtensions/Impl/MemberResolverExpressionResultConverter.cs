using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000063 RID: 99
	public class MemberResolverExpressionResultConverter : IExpressionResultConverter
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00008DA0 File Offset: 0x00006FA0
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, PropertyMap propertyMap, IValueResolver valueResolver)
		{
			return MemberResolverExpressionResultConverter.ExpressionResolutionResult(expressionResolutionResult, propertyMap.CustomExpression);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008DB0 File Offset: 0x00006FB0
		private static ExpressionResolutionResult ExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, LambdaExpression lambdaExpression)
		{
			ParameterExpression oldParameter = lambdaExpression.Parameters.Single<ParameterExpression>();
			Expression expression = new MemberResolverExpressionResultConverter.ParameterConversionVisitor(expressionResolutionResult.ResolutionExpression, oldParameter).Visit(lambdaExpression.Body);
			Type type = expression.Type;
			return new ExpressionResolutionResult(expression, type);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008DED File Offset: 0x00006FED
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, ConstructorParameterMap propertyMap, IValueResolver valueResolver)
		{
			return MemberResolverExpressionResultConverter.ExpressionResolutionResult(expressionResolutionResult, null);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008DF6 File Offset: 0x00006FF6
		public bool CanGetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, IValueResolver valueResolver)
		{
			return valueResolver is IMemberResolver;
		}

		// Token: 0x0200012F RID: 303
		private class ParameterConversionVisitor : ExpressionVisitor
		{
			// Token: 0x06000725 RID: 1829 RVA: 0x0001727B File Offset: 0x0001547B
			public ParameterConversionVisitor(Expression newParameter, ParameterExpression oldParameter)
			{
				this.newParameter = newParameter;
				this.oldParameter = oldParameter;
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x00017291 File Offset: 0x00015491
			protected override Expression VisitParameter(ParameterExpression node)
			{
				if (node != this.oldParameter)
				{
					return node;
				}
				return this.newParameter;
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x000172A4 File Offset: 0x000154A4
			protected override Expression VisitMember(MemberExpression node)
			{
				if (node.Expression != this.oldParameter)
				{
					return base.VisitMember(node);
				}
				Expression expression = this.Visit(node.Expression);
				MemberInfo member = this.newParameter.Type.GetMember(node.Member.Name).First<MemberInfo>();
				return Expression.MakeMemberAccess(expression, member);
			}

			// Token: 0x0400022D RID: 557
			private readonly Expression newParameter;

			// Token: 0x0400022E RID: 558
			private readonly ParameterExpression oldParameter;
		}
	}
}
