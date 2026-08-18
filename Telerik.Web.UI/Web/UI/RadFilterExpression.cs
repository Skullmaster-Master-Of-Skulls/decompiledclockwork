using System;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x0200187F RID: 6271
	public abstract class RadFilterExpression : StateManager
	{
		// Token: 0x1700493B RID: 18747
		// (get) Token: 0x0600F2EF RID: 62191
		public abstract RadFilterFunction FilterFunction { get; }

		// Token: 0x0600F2F0 RID: 62192 RVA: 0x00375118 File Offset: 0x00373318
		internal static RadFilterExpression CreateExpressionFromTypeName(string expressionTypeName, string expressionFieldType)
		{
			RadFilterExpression result = null;
			if (expressionTypeName.StartsWith("RadFilterGroupExpression"))
			{
				result = new RadFilterGroupExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterEqualToFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterNotEqualToFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterNotEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterContainsFilterExpression"))
			{
				result = new RadFilterContainsFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterDoesNotContainFilterExpression"))
			{
				result = new RadFilterDoesNotContainFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterStartsWithFilterExpression"))
			{
				result = new RadFilterStartsWithFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterEndsWithFilterExpression"))
			{
				result = new RadFilterEndsWithFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterGreaterThanFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterGreaterThanFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterLessThanFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterLessThanFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterGreaterThanOrEqualToFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterGreaterThanOrEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterLessThanOrEqualToFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterLessThanOrEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterBetweenFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterBetweenFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterNotBetweenFilterExpression"))
			{
				result = RadFilterExpression.ConstructExpressionInstance(typeof(RadFilterNotBetweenFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadFilterIsEmptyFilterExpression"))
			{
				result = new RadFilterIsEmptyFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterNotIsEmptyFilterExpression"))
			{
				result = new RadFilterNotIsEmptyFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterIsNullFilterExpression"))
			{
				result = new RadFilterIsNullFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadFilterNotIsNullFilterExpression"))
			{
				result = new RadFilterNotIsNullFilterExpression();
			}
			return result;
		}

		// Token: 0x0600F2F1 RID: 62193 RVA: 0x00375304 File Offset: 0x00373504
		internal static RadFilterExpression CreateExpressionForFilterFunction(RadFilterFunction function, string expressionFieldType)
		{
			Type type = null;
			switch (function)
			{
			case RadFilterFunction.Contains:
				type = typeof(RadFilterContainsFilterExpression);
				break;
			case RadFilterFunction.DoesNotContain:
				type = typeof(RadFilterDoesNotContainFilterExpression);
				break;
			case RadFilterFunction.StartsWith:
				type = typeof(RadFilterStartsWithFilterExpression);
				break;
			case RadFilterFunction.EndsWith:
				type = typeof(RadFilterEndsWithFilterExpression);
				break;
			case RadFilterFunction.EqualTo:
				type = typeof(RadFilterEqualToFilterExpression<>);
				break;
			case RadFilterFunction.NotEqualTo:
				type = typeof(RadFilterNotEqualToFilterExpression<>);
				break;
			case RadFilterFunction.GreaterThan:
				type = typeof(RadFilterGreaterThanFilterExpression<>);
				break;
			case RadFilterFunction.LessThan:
				type = typeof(RadFilterLessThanFilterExpression<>);
				break;
			case RadFilterFunction.GreaterThanOrEqualTo:
				type = typeof(RadFilterGreaterThanOrEqualToFilterExpression<>);
				break;
			case RadFilterFunction.LessThanOrEqualTo:
				type = typeof(RadFilterLessThanOrEqualToFilterExpression<>);
				break;
			case RadFilterFunction.Between:
				type = typeof(RadFilterBetweenFilterExpression<>);
				break;
			case RadFilterFunction.NotBetween:
				type = typeof(RadFilterNotBetweenFilterExpression<>);
				break;
			case RadFilterFunction.IsEmpty:
				type = typeof(RadFilterIsEmptyFilterExpression);
				break;
			case RadFilterFunction.NotIsEmpty:
				type = typeof(RadFilterNotIsEmptyFilterExpression);
				break;
			case RadFilterFunction.IsNull:
				type = typeof(RadFilterIsNullFilterExpression);
				break;
			case RadFilterFunction.NotIsNull:
				type = typeof(RadFilterNotIsNullFilterExpression);
				break;
			case RadFilterFunction.Group:
				type = typeof(RadFilterGroupExpression);
				break;
			}
			return RadFilterExpression.CreateExpressionFromTypeName(type.Name, expressionFieldType);
		}

		// Token: 0x0600F2F2 RID: 62194 RVA: 0x00375460 File Offset: 0x00373660
		protected static RadFilterExpression ConstructExpressionInstance(Type expressionType, string expressionFieldType)
		{
			Type type = Type.GetType(expressionFieldType);
			if (type != null)
			{
				Type type2 = expressionType.MakeGenericType(new Type[]
				{
					type
				});
				return (RadFilterExpression)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			return null;
		}
	}
}
