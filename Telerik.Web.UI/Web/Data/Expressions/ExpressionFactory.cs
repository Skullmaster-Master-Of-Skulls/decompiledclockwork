using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAD RID: 7085
	internal static class ExpressionFactory
	{
		// Token: 0x0601121D RID: 70173 RVA: 0x003C74C0 File Offset: 0x003C56C0
		public static Expression DefaltValueExpression(Type type)
		{
			return Expression.Constant(type.DefaultValue(), type);
		}

		// Token: 0x0601121E RID: 70174 RVA: 0x003C74D0 File Offset: 0x003C56D0
		public static Expression MakeMemberAccess(Expression instance, string memberName)
		{
			foreach (IMemberAccessToken token in MemberAccessTokenizer.GetTokens(memberName))
			{
				instance = token.CreateMemberAccessExpression(instance);
			}
			return instance;
		}

		// Token: 0x0601121F RID: 70175 RVA: 0x003C7520 File Offset: 0x003C5720
		public static Expression MakeMemberAccess(Expression instance, string memberName, bool liftMemberAccessToNull)
		{
			Expression expression = ExpressionFactory.MakeMemberAccess(instance, memberName);
			if (liftMemberAccessToNull)
			{
				return ExpressionFactory.LiftMemberAccessToNull(expression);
			}
			return expression;
		}

		// Token: 0x06011220 RID: 70176 RVA: 0x003C7540 File Offset: 0x003C5740
		public static Expression LiftMemberAccessToNull(Expression memberAccess)
		{
			Expression defaultValue = ExpressionFactory.DefaltValueExpression(memberAccess.Type);
			return ExpressionFactory.LiftMemberAccessToNullRecursive(memberAccess, memberAccess, defaultValue);
		}

		// Token: 0x06011221 RID: 70177 RVA: 0x003C7564 File Offset: 0x003C5764
		public static Expression LiftMethodCallToNull(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			Expression instance2 = ExpressionFactory.ExtractMemberAccessExpressionFromLiftedExpression(instance);
			MethodCallExpression memberAccess = Expression.Call(instance2, method, arguments);
			return ExpressionFactory.LiftMemberAccessToNull(memberAccess);
		}

		// Token: 0x06011222 RID: 70178 RVA: 0x003C7588 File Offset: 0x003C5788
		private static Expression LiftMemberAccessToNullRecursive(Expression memberAccess, Expression conditionalExpression, Expression defaultValue)
		{
			Expression instanceExpressionFromExpression = ExpressionFactory.GetInstanceExpressionFromExpression(memberAccess);
			if (instanceExpressionFromExpression == null)
			{
				return conditionalExpression;
			}
			conditionalExpression = ExpressionFactory.CreateIfNullExpression(instanceExpressionFromExpression, conditionalExpression, defaultValue);
			return ExpressionFactory.LiftMemberAccessToNullRecursive(instanceExpressionFromExpression, conditionalExpression, defaultValue);
		}

		// Token: 0x06011223 RID: 70179 RVA: 0x003C75B4 File Offset: 0x003C57B4
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

		// Token: 0x06011224 RID: 70180 RVA: 0x003C75E4 File Offset: 0x003C57E4
		private static Expression CreateIfNullExpression(Expression instance, Expression memberAccess, Expression defaultValue)
		{
			if (ExpressionFactory.ShouldGenerateCondition(instance.Type))
			{
				return ExpressionFactory.CreateConditionExpression(instance, memberAccess, defaultValue);
			}
			return memberAccess;
		}

		// Token: 0x06011225 RID: 70181 RVA: 0x003C75FD File Offset: 0x003C57FD
		private static bool ShouldGenerateCondition(Type type)
		{
			return !type.IsValueType || type.IsNullableType();
		}

		// Token: 0x06011226 RID: 70182 RVA: 0x003C7610 File Offset: 0x003C5810
		private static Expression CreateConditionExpression(Expression instance, Expression memberAccess, Expression defaultValue)
		{
			Expression right = ExpressionFactory.DefaltValueExpression(instance.Type);
			BinaryExpression test = Expression.NotEqual(instance, right);
			return Expression.Condition(test, memberAccess, defaultValue);
		}

		// Token: 0x06011227 RID: 70183 RVA: 0x003C763C File Offset: 0x003C583C
		private static Expression ExtractMemberAccessExpressionFromLiftedExpression(Expression liftedToNullExpression)
		{
			while (liftedToNullExpression.NodeType == ExpressionType.Conditional)
			{
				ConditionalExpression conditionalExpression = (ConditionalExpression)liftedToNullExpression;
				if (conditionalExpression.Test.NodeType == ExpressionType.NotEqual)
				{
					liftedToNullExpression = conditionalExpression.IfTrue;
				}
				else
				{
					liftedToNullExpression = conditionalExpression.IfFalse;
				}
			}
			return liftedToNullExpression;
		}

		// Token: 0x06011228 RID: 70184 RVA: 0x003C767D File Offset: 0x003C587D
		internal static Expression LiftStringExpressionToEmpty(Expression stringExpression)
		{
			if (stringExpression.Type != typeof(string))
			{
				throw new ArgumentException("Provided expression should have string type", "stringExpression");
			}
			if (ExpressionFactory.IsNotNullConstantExpression(stringExpression))
			{
				return stringExpression;
			}
			return Expression.Coalesce(stringExpression, ExpressionFactory.EmptyStringExpression);
		}

		// Token: 0x06011229 RID: 70185 RVA: 0x003C76BC File Offset: 0x003C58BC
		internal static bool IsNotNullConstantExpression(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Constant)
			{
				ConstantExpression constantExpression = (ConstantExpression)expression;
				return constantExpression.Value != null;
			}
			return false;
		}

		// Token: 0x04004CB5 RID: 19637
		public static readonly ConstantExpression ZeroExpression = Expression.Constant(0);

		// Token: 0x04004CB6 RID: 19638
		public static readonly ConstantExpression EmptyStringExpression = Expression.Constant(string.Empty);
	}
}
