using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000011 RID: 17
	internal static class Expressions
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000455A File Offset: 0x0000275A
		internal static Expression Null<TNullType>()
		{
			return Expression.Constant(null, typeof(TNullType));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000456C File Offset: 0x0000276C
		internal static Expression Null(Type nullType)
		{
			return Expression.Constant(null, nullType);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004578 File Offset: 0x00002778
		internal static Expression<Func<TArg, TResult>> Lambda<TArg, TResult>(string argumentName, Func<ParameterExpression, Expression> createLambdaBodyGivenParameter)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TArg), argumentName);
			Expression body = createLambdaBodyGivenParameter(parameterExpression);
			return Expression.Lambda<Func<TArg, TResult>>(body, new ParameterExpression[]
			{
				parameterExpression
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000045B0 File Offset: 0x000027B0
		internal static Expression Call(this Expression exp, string methodName)
		{
			return Expression.Call(exp, methodName, Type.EmptyTypes, new Expression[0]);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000045C4 File Offset: 0x000027C4
		internal static Expression ConvertTo(this Expression exp, Type convertToType)
		{
			return Expression.Convert(exp, convertToType);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000045CD File Offset: 0x000027CD
		internal static Expression ConvertTo<TConvertToType>(this Expression exp)
		{
			return Expression.Convert(exp, typeof(TConvertToType));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000045DF File Offset: 0x000027DF
		internal static Expressions.ConditionalExpressionBuilder IfTrueThen(this Expression conditionExp, Expression resultIfTrue)
		{
			return new Expressions.ConditionalExpressionBuilder(conditionExp, resultIfTrue);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000045E8 File Offset: 0x000027E8
		internal static Expression Property<TPropertyType>(this Expression exp, string propertyName)
		{
			PropertyInfo runtimeProperty = exp.Type.GetRuntimeProperty(propertyName);
			return Expression.Property(exp, runtimeProperty);
		}

		// Token: 0x02000012 RID: 18
		internal sealed class ConditionalExpressionBuilder
		{
			// Token: 0x060000B8 RID: 184 RVA: 0x00004609 File Offset: 0x00002809
			internal ConditionalExpressionBuilder(Expression conditionExpression, Expression ifTrueExpression)
			{
				this.condition = conditionExpression;
				this.ifTrueThen = ifTrueExpression;
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x0000461F File Offset: 0x0000281F
			internal Expression Else(Expression resultIfFalse)
			{
				return Expression.Condition(this.condition, this.ifTrueThen, resultIfFalse);
			}

			// Token: 0x04000018 RID: 24
			private readonly Expression condition;

			// Token: 0x04000019 RID: 25
			private readonly Expression ifTrueThen;
		}
	}
}
