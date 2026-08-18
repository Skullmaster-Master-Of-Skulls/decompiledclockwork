using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CDA RID: 3290
	internal class BindingExpressionHelper
	{
		// Token: 0x06007AD5 RID: 31445 RVA: 0x001C2D9C File Offset: 0x001C0F9C
		public static object GetPropertyValue(object componentInstance, string propertyName)
		{
			Type type = componentInstance.GetType();
			PropertyInfo property = type.GetProperty(propertyName);
			object result = null;
			if (property != null)
			{
				result = property.GetValue(componentInstance, null);
			}
			return result;
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x001C2DD0 File Offset: 0x001C0FD0
		public static Func<object, object> CreateGetValueFuncReflection(Type itemType, string propertyPath)
		{
			ParameterExpression parameterExpression = Expression.Parameter(itemType, "item");
			Expression get;
			if (string.IsNullOrEmpty(propertyPath))
			{
				get = parameterExpression;
			}
			else
			{
				try
				{
					ConstantExpression arg = Expression.Constant(propertyPath);
					MethodInfo method = typeof(BindingExpressionHelper).GetMethod("GetPropertyValue");
					MethodCallExpression methodCallExpression = Expression.Call(method, parameterExpression, arg);
					get = methodCallExpression;
				}
				catch (ArgumentException)
				{
					return (object p) => null;
				}
			}
			return BindingExpressionHelper.CompileToUntypedFunc(get, parameterExpression, itemType);
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x001C2E64 File Offset: 0x001C1064
		public static Func<object, object> CreateGetValueFunc(Type itemType, string propertyPath)
		{
			ParameterExpression parameterExpression = Expression.Parameter(itemType, "item");
			Expression get;
			if (string.IsNullOrEmpty(propertyPath))
			{
				get = parameterExpression;
			}
			else
			{
				try
				{
					get = Expression.PropertyOrField(parameterExpression, propertyPath);
				}
				catch (ArgumentException)
				{
					return (object p) => null;
				}
			}
			return BindingExpressionHelper.CompileToUntypedFunc(get, parameterExpression, itemType);
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x001C2ED0 File Offset: 0x001C10D0
		private static Func<object, object> CompileToUntypedFunc(Expression get, ParameterExpression parameter, Type itemType)
		{
			LambdaExpression lambdaExpression = Expression.Lambda(get, new ParameterExpression[]
			{
				parameter
			});
			Delegate @delegate = lambdaExpression.Compile();
			MethodInfo methodInfo = typeof(BindingExpressionHelper).GetMethod("ToUntypedFunc", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
			{
				itemType,
				lambdaExpression.Body.Type
			});
			return (Func<object, object>)methodInfo.Invoke(null, new object[]
			{
				@delegate
			});
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x001C2F6C File Offset: 0x001C116C
		private static Func<object, object> ToUntypedFunc<T, TResult>(Func<T, TResult> func)
		{
			return (object item) => func((T)((object)item));
		}
	}
}
