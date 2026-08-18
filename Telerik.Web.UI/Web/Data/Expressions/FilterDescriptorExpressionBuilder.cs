using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB1 RID: 7089
	internal class FilterDescriptorExpressionBuilder : FilterExpressionBuilder
	{
		// Token: 0x06011239 RID: 70201 RVA: 0x003C78E2 File Offset: 0x003C5AE2
		public FilterDescriptorExpressionBuilder(ParameterExpression parameterExpression, FilterDescriptor descriptor) : base(parameterExpression)
		{
			this.descriptor = descriptor;
		}

		// Token: 0x1700539D RID: 21405
		// (get) Token: 0x0601123A RID: 70202 RVA: 0x003C78F2 File Offset: 0x003C5AF2
		public FilterDescriptor FilterDescriptor
		{
			get
			{
				return this.descriptor;
			}
		}

		// Token: 0x0601123B RID: 70203 RVA: 0x003C78FC File Offset: 0x003C5AFC
		public override Expression CreateBodyExpression()
		{
			Expression expression = this.CreateMemberExpression();
			Type type = expression.Type;
			Expression expression2 = FilterDescriptorExpressionBuilder.CreateValueExpression(type, this.descriptor.Value, CultureInfo.InvariantCulture);
			bool flag = true;
			if (FilterDescriptorExpressionBuilder.TypesAreDifferent(this.descriptor, expression, expression2))
			{
				if (!FilterDescriptorExpressionBuilder.TryConvertExpressionTypes(ref expression, ref expression2))
				{
					flag = false;
				}
			}
			else if (expression.Type.IsEnumType() || expression2.Type.IsEnumType())
			{
				if (!FilterDescriptorExpressionBuilder.TryPromoteNullableEnums(ref expression, ref expression2))
				{
					flag = false;
				}
			}
			else if (type.IsNullableType() && expression.Type != expression2.Type && !FilterDescriptorExpressionBuilder.TryConvertNullableValue(expression, ref expression2))
			{
				flag = false;
			}
			if (!flag)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Operator '{0}' is incompatible with operand types '{1}' and '{2}'", new object[]
				{
					this.descriptor.Operator,
					expression.Type.GetTypeName(),
					expression2.Type.GetTypeName()
				}));
			}
			return this.descriptor.Operator.CreateExpression(expression, expression2, this.descriptor.IsCaseSensitive);
		}

		// Token: 0x0601123C RID: 70204 RVA: 0x003C7A10 File Offset: 0x003C5C10
		public FilterDescription CreateFilterDescription()
		{
			LambdaExpression lambdaExpression = base.CreateFilterExpression();
			Delegate predicate = lambdaExpression.Compile();
			return new PredicateFilterDescription(predicate);
		}

		// Token: 0x0601123D RID: 70205 RVA: 0x003C7A34 File Offset: 0x003C5C34
		protected virtual Expression CreateMemberExpression()
		{
			Type memberType = this.FilterDescriptor.MemberType;
			MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = ExpressionBuilderFactory.MemberAccess(base.ParameterExpression.Type, memberType, this.FilterDescriptor.Member);
			memberAccessExpressionBuilderBase.Options.CopyFrom(base.Options);
			memberAccessExpressionBuilderBase.ParameterExpression = base.ParameterExpression;
			Expression expression = memberAccessExpressionBuilderBase.CreateMemberAccessExpression();
			if (memberType != null && expression.Type.GetNonNullableType() != memberType.GetNonNullableType())
			{
				expression = Expression.Convert(expression, memberType);
			}
			return expression;
		}

		// Token: 0x0601123E RID: 70206 RVA: 0x003C7AB8 File Offset: 0x003C5CB8
		private static Expression CreateConstantExpression(object value)
		{
			if (value == null)
			{
				return ExpressionParser.NullLiteral;
			}
			return Expression.Constant(value);
		}

		// Token: 0x0601123F RID: 70207 RVA: 0x003C7ACC File Offset: 0x003C5CCC
		private static Expression CreateValueExpression(Type targetType, object value, CultureInfo culture)
		{
			if (targetType != typeof(string) && (!targetType.IsValueType || targetType.IsNullableType()) && string.Compare(value as string, "null", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = null;
			}
			if (value != null)
			{
				Type nonNullableType = targetType.GetNonNullableType();
				if (value.GetType() != nonNullableType)
				{
					if (nonNullableType.IsEnum)
					{
						value = Enum.Parse(nonNullableType, value.ToString(), true);
					}
					else if (value is IConvertible)
					{
						value = Convert.ChangeType(value, nonNullableType, culture);
					}
				}
			}
			return FilterDescriptorExpressionBuilder.CreateConstantExpression(value);
		}

		// Token: 0x06011240 RID: 70208 RVA: 0x003C7B5C File Offset: 0x003C5D5C
		private static Expression PromoteExpression(Expression expr, Type type, bool exact)
		{
			if (expr.Type == type)
			{
				return expr;
			}
			ConstantExpression constantExpression = expr as ConstantExpression;
			if (constantExpression != null && constantExpression == ExpressionParser.NullLiteral && (!type.IsValueType || type.IsNullableType()))
			{
				return Expression.Constant(null, type);
			}
			if (!expr.Type.IsCompatibleWith(type))
			{
				return null;
			}
			if (type.IsValueType || exact)
			{
				return Expression.Convert(expr, type);
			}
			return expr;
		}

		// Token: 0x06011241 RID: 70209 RVA: 0x003C7BC8 File Offset: 0x003C5DC8
		private static bool TryConvertExpressionTypes(ref Expression memberExpression, ref Expression valueExpression)
		{
			if (memberExpression.Type != valueExpression.Type)
			{
				if (!memberExpression.Type.IsAssignableFrom(valueExpression.Type))
				{
					if (!valueExpression.Type.IsAssignableFrom(memberExpression.Type))
					{
						return false;
					}
					memberExpression = Expression.Convert(memberExpression, valueExpression.Type);
				}
				else
				{
					valueExpression = Expression.Convert(valueExpression, memberExpression.Type);
				}
			}
			return true;
		}

		// Token: 0x06011242 RID: 70210 RVA: 0x003C7C3C File Offset: 0x003C5E3C
		private static bool TryConvertNullableValue(Expression memberExpression, ref Expression valueExpression)
		{
			ConstantExpression constantExpression = valueExpression as ConstantExpression;
			if (constantExpression != null)
			{
				try
				{
					valueExpression = Expression.Constant(constantExpression.Value, memberExpression.Type);
				}
				catch (ArgumentException)
				{
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06011243 RID: 70211 RVA: 0x003C7C84 File Offset: 0x003C5E84
		private static bool TryPromoteNullableEnums(ref Expression memberExpression, ref Expression valueExpression)
		{
			if (memberExpression.Type != valueExpression.Type)
			{
				Expression expression = FilterDescriptorExpressionBuilder.PromoteExpression(valueExpression, memberExpression.Type, true);
				if (expression == null)
				{
					expression = FilterDescriptorExpressionBuilder.PromoteExpression(memberExpression, valueExpression.Type, true);
					if (expression == null)
					{
						return false;
					}
					memberExpression = expression;
				}
				else
				{
					valueExpression = expression;
				}
			}
			return true;
		}

		// Token: 0x06011244 RID: 70212 RVA: 0x003C7CD8 File Offset: 0x003C5ED8
		private static bool TypesAreDifferent(FilterDescriptor descriptor, Expression memberExpression, Expression valueExpression)
		{
			bool flag = descriptor.Operator == FilterOperator.IsEqualTo || descriptor.Operator == FilterOperator.IsNotEqualTo;
			return flag && !memberExpression.Type.IsValueType && !valueExpression.Type.IsValueType;
		}

		// Token: 0x04004CBA RID: 19642
		private readonly FilterDescriptor descriptor;
	}
}
