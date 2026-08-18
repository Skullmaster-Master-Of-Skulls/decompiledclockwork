using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001B99 RID: 7065
	internal static class FilterOperatorExtensions
	{
		// Token: 0x06011197 RID: 70039 RVA: 0x003C59D3 File Offset: 0x003C3BD3
		internal static Expression CreateExpression(this FilterOperator filterOperator, Expression left, Expression right)
		{
			return filterOperator.CreateExpression(left, right, false);
		}

		// Token: 0x06011198 RID: 70040 RVA: 0x003C59E0 File Offset: 0x003C3BE0
		internal static Expression CreateExpression(this FilterOperator filterOperator, Expression left, Expression right, bool isCaseSensitive)
		{
			if (left.Type == typeof(string) && !isCaseSensitive)
			{
				left = FilterOperatorExtensions.GenerateToLowerCall(left);
				right = FilterOperatorExtensions.GenerateToLowerCall(right);
			}
			switch (filterOperator)
			{
			case FilterOperator.IsLessThan:
				return FilterOperatorExtensions.GenerateLessThan(left, right);
			case FilterOperator.IsLessThanOrEqualTo:
				return FilterOperatorExtensions.GenerateLessThanEqual(left, right);
			case FilterOperator.IsEqualTo:
				return FilterOperatorExtensions.GenerateEqual(left, right);
			case FilterOperator.IsNotEqualTo:
				return FilterOperatorExtensions.GenerateNotEqual(left, right);
			case FilterOperator.IsGreaterThanOrEqualTo:
				return FilterOperatorExtensions.GenerateGreaterThanEqual(left, right);
			case FilterOperator.IsGreaterThan:
				return FilterOperatorExtensions.GenerateGreaterThan(left, right);
			case FilterOperator.StartsWith:
				return FilterOperatorExtensions.GenerateStartsWith(left, right);
			case FilterOperator.EndsWith:
				return FilterOperatorExtensions.GenerateEndsWith(left, right);
			case FilterOperator.Contains:
				return FilterOperatorExtensions.GenerateContains(left, right);
			case FilterOperator.IsContainedIn:
				return FilterOperatorExtensions.GenerateIsContainedIn(left, right);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06011199 RID: 70041 RVA: 0x003C5A9E File Offset: 0x003C3C9E
		private static Expression GenerateEqual(Expression left, Expression right)
		{
			return Expression.Equal(left, right);
		}

		// Token: 0x0601119A RID: 70042 RVA: 0x003C5AA7 File Offset: 0x003C3CA7
		private static Expression GenerateNotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right);
		}

		// Token: 0x0601119B RID: 70043 RVA: 0x003C5AB0 File Offset: 0x003C3CB0
		private static Expression GenerateContains(Expression left, Expression right)
		{
			return Expression.Equal(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringContainsMethodInfo, left, right), ExpressionParser.TrueLiteral);
		}

		// Token: 0x0601119C RID: 70044 RVA: 0x003C5AC8 File Offset: 0x003C3CC8
		private static Expression GenerateIsContainedIn(Expression left, Expression right)
		{
			return Expression.Equal(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringContainsMethodInfo, right, left), ExpressionParser.TrueLiteral);
		}

		// Token: 0x0601119D RID: 70045 RVA: 0x003C5AE0 File Offset: 0x003C3CE0
		private static Expression GenerateStartsWith(Expression left, Expression right)
		{
			return Expression.Equal(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringStartsWithMethodInfo, left, right), ExpressionParser.TrueLiteral);
		}

		// Token: 0x0601119E RID: 70046 RVA: 0x003C5AF8 File Offset: 0x003C3CF8
		private static Expression GenerateEndsWith(Expression left, Expression right)
		{
			return Expression.Equal(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringEndsWithMethodInfo, left, right), ExpressionParser.TrueLiteral);
		}

		// Token: 0x0601119F RID: 70047 RVA: 0x003C5B10 File Offset: 0x003C3D10
		private static Expression GenerateGreaterThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThan(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringCompareMethodInfo, left, right), ExpressionFactory.ZeroExpression);
			}
			return Expression.GreaterThan(left, right);
		}

		// Token: 0x060111A0 RID: 70048 RVA: 0x003C5B47 File Offset: 0x003C3D47
		private static Expression GenerateGreaterThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThanOrEqual(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringCompareMethodInfo, left, right), ExpressionFactory.ZeroExpression);
			}
			return Expression.GreaterThanOrEqual(left, right);
		}

		// Token: 0x060111A1 RID: 70049 RVA: 0x003C5B7E File Offset: 0x003C3D7E
		private static Expression GenerateLessThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThan(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringCompareMethodInfo, left, right), ExpressionFactory.ZeroExpression);
			}
			return Expression.LessThan(left, right);
		}

		// Token: 0x060111A2 RID: 70050 RVA: 0x003C5BB5 File Offset: 0x003C3DB5
		private static Expression GenerateLessThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThanOrEqual(FilterOperatorExtensions.GenerateStringMethodCall(FilterOperatorExtensions.StringCompareMethodInfo, left, right), ExpressionFactory.ZeroExpression);
			}
			return Expression.LessThanOrEqual(left, right);
		}

		// Token: 0x060111A3 RID: 70051 RVA: 0x003C5BEC File Offset: 0x003C3DEC
		private static Expression GenerateStringMethodCall(MethodInfo methodInfo, Expression left, Expression right)
		{
			if (methodInfo.IsStatic)
			{
				return Expression.Call(methodInfo, new Expression[]
				{
					left,
					right
				});
			}
			return Expression.Call(left, methodInfo, new Expression[]
			{
				right
			});
		}

		// Token: 0x060111A4 RID: 70052 RVA: 0x003C5C2C File Offset: 0x003C3E2C
		private static Expression GenerateToLowerCall(Expression stringExpression)
		{
			Expression instance = ExpressionFactory.LiftStringExpressionToEmpty(stringExpression);
			return Expression.Call(instance, FilterOperatorExtensions.StringToLowerMethodInfo);
		}

		// Token: 0x04004C92 RID: 19602
		internal static readonly MethodInfo StringToLowerMethodInfo = typeof(string).GetMethod("ToLower", new Type[0]);

		// Token: 0x04004C93 RID: 19603
		internal static readonly MethodInfo StringStartsWithMethodInfo = typeof(string).GetMethod("StartsWith", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04004C94 RID: 19604
		internal static readonly MethodInfo StringEndsWithMethodInfo = typeof(string).GetMethod("EndsWith", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04004C95 RID: 19605
		internal static readonly MethodInfo StringCompareMethodInfo = typeof(string).GetMethod("Compare", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04004C96 RID: 19606
		internal static readonly MethodInfo StringContainsMethodInfo = typeof(string).GetMethod("Contains", new Type[]
		{
			typeof(string)
		});
	}
}
