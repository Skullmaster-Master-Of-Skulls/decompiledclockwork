using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x02000027 RID: 39
	internal static class Expressions
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000CFB1 File Offset: 0x0000B1B1
		internal static Expression Null<TNullType>()
		{
			return Expression.Constant(null, typeof(TNullType));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CFC3 File Offset: 0x0000B1C3
		internal static Expression Null(Type nullType)
		{
			return Expression.Constant(null, nullType);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		internal static Expression<Func<TArg, TResult>> Lambda<TArg, TResult>(string argumentName, Func<ParameterExpression, Expression> createLambdaBodyGivenParameter)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TArg), argumentName);
			Expression body = createLambdaBodyGivenParameter(parameterExpression);
			return Expression.Lambda<Func<TArg, TResult>>(body, new ParameterExpression[]
			{
				parameterExpression
			});
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D002 File Offset: 0x0000B202
		internal static Expression Call(this Expression exp, string methodName)
		{
			return Expression.Call(exp, methodName, Type.EmptyTypes, new Expression[0]);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D016 File Offset: 0x0000B216
		internal static Expression ConvertTo(this Expression exp, Type convertToType)
		{
			return Expression.Convert(exp, convertToType);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D01F File Offset: 0x0000B21F
		internal static Expression ConvertTo<TConvertToType>(this Expression exp)
		{
			return Expression.Convert(exp, typeof(TConvertToType));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000D031 File Offset: 0x0000B231
		internal static Expressions.ConditionalExpressionBuilder IfTrueThen(this Expression conditionExp, Expression resultIfTrue)
		{
			return new Expressions.ConditionalExpressionBuilder(conditionExp, resultIfTrue);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D03C File Offset: 0x0000B23C
		internal static Expression Property<TPropertyType>(this Expression exp, string propertyName)
		{
			PropertyInfo property = exp.Type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
			return Expression.Property(exp, property);
		}

		// Token: 0x02000448 RID: 1096
		internal sealed class ConditionalExpressionBuilder
		{
			// Token: 0x06003A54 RID: 14932 RVA: 0x000DE1FE File Offset: 0x000DC3FE
			internal ConditionalExpressionBuilder(Expression conditionExpression, Expression ifTrueExpression)
			{
				this.condition = conditionExpression;
				this.ifTrueThen = ifTrueExpression;
			}

			// Token: 0x06003A55 RID: 14933 RVA: 0x000DE214 File Offset: 0x000DC414
			internal Expression Else(Expression resultIfFalse)
			{
				return Expression.Condition(this.condition, this.ifTrueThen, resultIfFalse);
			}

			// Token: 0x040018D8 RID: 6360
			private readonly Expression condition;

			// Token: 0x040018D9 RID: 6361
			private readonly Expression ifTrueThen;
		}
	}
}
