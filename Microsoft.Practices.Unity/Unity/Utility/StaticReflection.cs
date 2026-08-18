using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AF RID: 175
	public static class StaticReflection
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000B22D File Offset: 0x0000942D
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Lambda inference at the call site doesn't work without the derived type.")]
		public static MethodInfo GetMethodInfo(Expression<Action> expression)
		{
			return StaticReflection.GetMethodInfo(expression);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B235 File Offset: 0x00009435
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Lambda inference at the call site doesn't work without the derived type.")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Expressions require nested generics")]
		public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
		{
			return StaticReflection.GetMethodInfo(expression);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B240 File Offset: 0x00009440
		private static MethodInfo GetMethodInfo(LambdaExpression lambda)
		{
			StaticReflection.GuardProperExpressionForm(lambda.Body);
			MethodCallExpression methodCallExpression = (MethodCallExpression)lambda.Body;
			return methodCallExpression.Method;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B26C File Offset: 0x0000946C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Expressions require nested generics")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Lambda inference at the call site doesn't work without the derived type.")]
		public static MethodInfo GetPropertyGetMethodInfo<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			PropertyInfo propertyInfo = StaticReflection.GetPropertyInfo<T, TProperty>(expression);
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			return getMethod;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B298 File Offset: 0x00009498
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Expressions require nested generics")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Lambda inference at the call site doesn't work without the derived type.")]
		public static MethodInfo GetPropertySetMethodInfo<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			PropertyInfo propertyInfo = StaticReflection.GetPropertyInfo<T, TProperty>(expression);
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			return setMethod;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B2C4 File Offset: 0x000094C4
		private static PropertyInfo GetPropertyInfo<T, TProperty>(LambdaExpression lambda)
		{
			MemberExpression memberExpression = lambda.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			return propertyInfo;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B308 File Offset: 0x00009508
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "As designed for setting usage expectations")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Expressions require nested generics")]
		public static MemberInfo GetMemberInfo<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			Guard.ArgumentNotNull(expression, "expression");
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new InvalidOperationException("invalid expression form passed");
			}
			MemberInfo member = memberExpression.Member;
			if (member == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			return member;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B350 File Offset: 0x00009550
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Expressions require nested generics")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Lambda inference at the call site doesn't work without the derived type.")]
		public static ConstructorInfo GetConstructorInfo<T>(Expression<Func<T>> expression)
		{
			Guard.ArgumentNotNull(expression, "expression");
			NewExpression newExpression = expression.Body as NewExpression;
			if (newExpression == null)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
			return newExpression.Constructor;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000B388 File Offset: 0x00009588
		private static void GuardProperExpressionForm(Expression expression)
		{
			if (expression.NodeType != ExpressionType.Call)
			{
				throw new InvalidOperationException("Invalid expression form passed");
			}
		}
	}
}
