using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200073B RID: 1851
	internal class QueryableExpressionHelper
	{
		// Token: 0x060041D7 RID: 16855 RVA: 0x000CEAD8 File Offset: 0x000CCCD8
		public static Expression MakeMemberAccess(ParameterExpression itemExpression, string memberName)
		{
			return QueryableExpressionHelper.MakeMemberAccess(itemExpression, memberName, null);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000CEAE4 File Offset: 0x000CCCE4
		public static Expression MakeMemberAccess(ParameterExpression itemExpression, string memberName, Expression defaultExpression)
		{
			Expression expression = itemExpression;
			string[] array = memberName.Split(new string[]
			{
				"."
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				Type nonNullableType = PivotTypeExtensions.GetNonNullableType(expression.Type);
				PropertyInfo property = nonNullableType.GetProperty(text);
				if (property == null)
				{
					QueryableExpressionHelper.SubmitPropertyErrorTraceEvent(text, itemExpression);
					return QueryableExpressionHelper.GetExpressionForMissingProperty(nonNullableType, defaultExpression);
				}
				expression = QueryableExpressionHelper.AddValueAccessForNullable(expression);
				expression = Expression.MakeMemberAccess(expression, property);
			}
			return expression;
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000CEB70 File Offset: 0x000CCD70
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We need to work always.")]
		internal static Expression GetValueExpression(object value, Type desiredType)
		{
			if (value != null && value.GetType().Equals(desiredType))
			{
				return Expression.Constant(value);
			}
			TypeConverter converter = TypeDescriptor.GetConverter(desiredType);
			try
			{
				object value2 = converter.ConvertFrom(null, CultureInfo.CurrentCulture, value.ToString());
				return Expression.Constant(value2);
			}
			catch
			{
			}
			try
			{
				object value3 = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value.ToString());
				return Expression.Constant(value3);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000CEBFC File Offset: 0x000CCDFC
		private static Expression GetExpressionForMissingProperty(Type type, Expression defaultExpression)
		{
			Expression result = QueryableExpressionHelper.DefaltValueExpression(type);
			if (defaultExpression != null)
			{
				result = defaultExpression;
			}
			return result;
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000CEC18 File Offset: 0x000CCE18
		private static Expression AddValueAccessForNullable(Expression access)
		{
			bool flag = PivotTypeExtensions.IsNullableType(access.Type);
			if (flag)
			{
				PropertyInfo property = access.Type.GetProperty("Value");
				return Expression.MakeMemberAccess(access, property);
			}
			return access;
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000CEC50 File Offset: 0x000CCE50
		public static Expression LiftMemberAccessToNull(Expression memberAccess)
		{
			if (memberAccess != null)
			{
				Type nonNullableType = PivotTypeExtensions.GetNonNullableType(memberAccess.Type);
				PivotTypeExtensions.IsNullableType(memberAccess.Type);
				Expression defaultValue = QueryableExpressionHelper.DefaltValueExpression(nonNullableType);
				Expression conditionalExpression = QueryableExpressionHelper.AddValueAccessForNullable(memberAccess);
				return QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess, conditionalExpression, defaultValue);
			}
			return null;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000CEC94 File Offset: 0x000CCE94
		public static Expression LiftMemberAccessToNull(Expression memberAccess, Expression conditionalExpression, Expression defaultValue)
		{
			for (Expression expression = memberAccess as MemberExpression; expression != null; expression = QueryableExpressionHelper.GetInstanceExpressionFromExpression(expression))
			{
				conditionalExpression = QueryableExpressionHelper.CreateIfNullExpression(expression, conditionalExpression, defaultValue);
			}
			return conditionalExpression;
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000CECBF File Offset: 0x000CCEBF
		public static Expression DefaltValueExpression(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type == typeof(DateTime))
			{
				return Expression.Constant(DateTime.Today, type);
			}
			return Expression.Constant(PivotTypeExtensions.DefaultValue(type), type);
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000CECFB File Offset: 0x000CCEFB
		private static Expression CreateIfNullExpression(Expression instance, Expression memberAccess, Expression defaultValue)
		{
			if (QueryableExpressionHelper.ShouldGenerateCondition(instance.Type))
			{
				return QueryableExpressionHelper.CreateConditionExpression(instance, memberAccess, defaultValue);
			}
			return memberAccess;
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000CED14 File Offset: 0x000CCF14
		private static bool ShouldGenerateCondition(Type type)
		{
			return !type.IsValueType || PivotTypeExtensions.IsNullableType(type);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000CED28 File Offset: 0x000CCF28
		private static Expression CreateConditionExpression(Expression instance, Expression memberAccess, Expression defaultValue)
		{
			Expression right = QueryableExpressionHelper.DefaltValueExpression(instance.Type);
			BinaryExpression test = Expression.NotEqual(instance, right);
			return Expression.Condition(test, memberAccess, defaultValue);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000CED54 File Offset: 0x000CCF54
		private static Expression GetInstanceExpressionFromExpression(Expression memberAccess)
		{
			MemberExpression memberExpression = memberAccess as MemberExpression;
			if (memberExpression != null)
			{
				return memberExpression.Expression;
			}
			MethodCallExpression methodCallExpression = memberAccess as MethodCallExpression;
			if (methodCallExpression != null)
			{
				return methodCallExpression.Object;
			}
			return null;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000CED84 File Offset: 0x000CCF84
		private static void SubmitPropertyErrorTraceEvent(string propertyName, ParameterExpression itemExpression)
		{
			TelerikPivotTraceSources.DataProviderSource.TraceEvent(TraceEventType.Error, 1, "Error: property '{0}' not found on item type '{1}'", new object[]
			{
				propertyName,
				itemExpression.Type
			});
		}
	}
}
