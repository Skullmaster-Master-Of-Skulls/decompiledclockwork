using System;
using System.Linq.Expressions;
using AutoMapper.Internal;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000065 RID: 101
	public class NullSubstitutionExpressionResultConverter : IExpressionResultConverter
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00008EFC File Offset: 0x000070FC
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, PropertyMap propertyMap, IValueResolver valueResolver)
		{
			Expression expression = expressionResolutionResult.ResolutionExpression;
			Type type = expressionResolutionResult.Type;
			object nullSubstitute = propertyMap.NullSubstitute;
			expression = new NullSubstitutionExpressionResultConverter.NullSubstitutionConversionVisitor(expressionResolutionResult.ResolutionExpression, nullSubstitute).Visit(expression);
			type = type.GetTypeOfNullable();
			return new ExpressionResolutionResult(expression, type);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008F3F File Offset: 0x0000713F
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, ConstructorParameterMap propertyMap, IValueResolver valueResolver)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00008F46 File Offset: 0x00007146
		public bool CanGetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, IValueResolver valueResolver)
		{
			return valueResolver is NullReplacementMethod && expressionResolutionResult.Type.IsNullableType();
		}

		// Token: 0x02000130 RID: 304
		private class NullSubstitutionConversionVisitor : ExpressionVisitor
		{
			// Token: 0x06000728 RID: 1832 RVA: 0x000172FA File Offset: 0x000154FA
			public NullSubstitutionConversionVisitor(Expression newParameter, object nullSubstitute)
			{
				this.newParameter = newParameter;
				this._nullSubstitute = nullSubstitute;
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x00017310 File Offset: 0x00015510
			protected override Expression VisitMember(MemberExpression node)
			{
				if (node == this.newParameter)
				{
					return Expression.Condition(Expression.Property(this.newParameter, "HasValue"), Expression.Property(this.newParameter, "Value"), Expression.Constant(this._nullSubstitute), node.Type.GetTypeOfNullable());
				}
				return node;
			}

			// Token: 0x0400022F RID: 559
			private readonly Expression newParameter;

			// Token: 0x04000230 RID: 560
			private readonly object _nullSubstitute;
		}
	}
}
