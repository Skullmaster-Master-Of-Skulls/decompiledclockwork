using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006E9 RID: 1769
	internal static class ExpressionExtensions
	{
		// Token: 0x06004707 RID: 18183 RVA: 0x00150584 File Offset: 0x0014E784
		public static PropertyPath GetSimplePropertyAccess(this LambdaExpression propertyAccessExpression)
		{
			PropertyPath propertyPath = propertyAccessExpression.Parameters.Single<ParameterExpression>().MatchSimplePropertyAccess(propertyAccessExpression.Body);
			if (propertyPath == null)
			{
				throw Error.InvalidPropertyExpression(propertyAccessExpression);
			}
			return propertyPath;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x001505BC File Offset: 0x0014E7BC
		public static PropertyPath GetComplexPropertyAccess(this LambdaExpression propertyAccessExpression)
		{
			PropertyPath propertyPath = propertyAccessExpression.Parameters.Single<ParameterExpression>().MatchComplexPropertyAccess(propertyAccessExpression.Body);
			if (propertyPath == null)
			{
				throw Error.InvalidComplexPropertyExpression(propertyAccessExpression);
			}
			return propertyPath;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001505FC File Offset: 0x0014E7FC
		public static IEnumerable<PropertyPath> GetSimplePropertyAccessList(this LambdaExpression propertyAccessExpression)
		{
			IEnumerable<PropertyPath> enumerable = propertyAccessExpression.MatchPropertyAccessList((Expression p, Expression e) => e.MatchSimplePropertyAccess(p));
			if (enumerable == null)
			{
				throw Error.InvalidPropertiesExpression(propertyAccessExpression);
			}
			return enumerable;
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00150644 File Offset: 0x0014E844
		public static IEnumerable<PropertyPath> GetComplexPropertyAccessList(this LambdaExpression propertyAccessExpression)
		{
			IEnumerable<PropertyPath> enumerable = propertyAccessExpression.MatchPropertyAccessList((Expression p, Expression e) => e.MatchComplexPropertyAccess(p));
			if (enumerable == null)
			{
				throw Error.InvalidComplexPropertiesExpression(propertyAccessExpression);
			}
			return enumerable;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x001506B4 File Offset: 0x0014E8B4
		private static IEnumerable<PropertyPath> MatchPropertyAccessList(this LambdaExpression lambdaExpression, Func<Expression, Expression, PropertyPath> propertyMatcher)
		{
			NewExpression newExpression = lambdaExpression.Body.RemoveConvert() as NewExpression;
			if (newExpression != null)
			{
				ParameterExpression parameterExpression = lambdaExpression.Parameters.Single<ParameterExpression>();
				IEnumerable<PropertyPath> enumerable = from a in newExpression.Arguments
				select propertyMatcher(a, parameterExpression) into p
				where p != null
				select p;
				if (enumerable.Count<PropertyPath>() == newExpression.Arguments.Count<Expression>())
				{
					if (!newExpression.HasDefaultMembersOnly(enumerable))
					{
						return null;
					}
					return enumerable;
				}
			}
			PropertyPath propertyPath = propertyMatcher(lambdaExpression.Body, lambdaExpression.Parameters.Single<ParameterExpression>());
			if (!(propertyPath != null))
			{
				return null;
			}
			return new PropertyPath[]
			{
				propertyPath
			};
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x001507C4 File Offset: 0x0014E9C4
		private static bool HasDefaultMembersOnly(this NewExpression newExpression, IEnumerable<PropertyPath> propertyPaths)
		{
			return !newExpression.Members.Where((MemberInfo t, int i) => !string.Equals(t.Name, propertyPaths.ElementAt(i).Last<PropertyInfo>().Name, StringComparison.Ordinal)).Any<MemberInfo>();
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00150800 File Offset: 0x0014EA00
		private static PropertyPath MatchSimplePropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			PropertyPath propertyPath = parameterExpression.MatchPropertyAccess(propertyAccessExpression);
			if (!(propertyPath != null) || propertyPath.Count != 1)
			{
				return null;
			}
			return propertyPath;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0015082C File Offset: 0x0014EA2C
		private static PropertyPath MatchComplexPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			return parameterExpression.MatchPropertyAccess(propertyAccessExpression);
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00150844 File Offset: 0x0014EA44
		private static PropertyPath MatchPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			for (;;)
			{
				MemberExpression memberExpression = propertyAccessExpression.RemoveConvert() as MemberExpression;
				if (memberExpression == null)
				{
					break;
				}
				PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
				if (propertyInfo == null)
				{
					goto Block_2;
				}
				list.Insert(0, propertyInfo);
				propertyAccessExpression = memberExpression.Expression;
				if (memberExpression.Expression == parameterExpression)
				{
					goto Block_3;
				}
			}
			return null;
			Block_2:
			return null;
			Block_3:
			return new PropertyPath(list);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x0015089E File Offset: 0x0014EA9E
		public static Expression RemoveConvert(this Expression expression)
		{
			while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
			{
				expression = ((UnaryExpression)expression).Operand;
			}
			return expression;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x001508C4 File Offset: 0x0014EAC4
		public static bool IsNullConstant(this Expression expression)
		{
			expression = expression.RemoveConvert();
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value == null;
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x001508E8 File Offset: 0x0014EAE8
		public static bool IsStringAddExpression(this Expression expression)
		{
			BinaryExpression binaryExpression = expression as BinaryExpression;
			return binaryExpression != null && !(binaryExpression.Method == null) && binaryExpression.NodeType == ExpressionType.Add && binaryExpression.Method.DeclaringType == typeof(string) && string.Equals(binaryExpression.Method.Name, "Concat", StringComparison.Ordinal);
		}
	}
}
