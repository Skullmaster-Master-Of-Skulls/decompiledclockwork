using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200078C RID: 1932
	internal class DbQueryVisitor : ExpressionVisitor
	{
		// Token: 0x0600578B RID: 22411 RVA: 0x0017958C File Offset: 0x0017778C
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			Check.NotNull<MethodCallExpression>(node, "node");
			if (typeof(DbContext).IsAssignableFrom(node.Method.DeclaringType))
			{
				MemberExpression memberExpression = node.Object as MemberExpression;
				if (memberExpression != null)
				{
					DbContext contextFromConstantExpression = DbQueryVisitor.GetContextFromConstantExpression(memberExpression.Expression, memberExpression.Member);
					if (contextFromConstantExpression != null && !node.Method.GetCustomAttributes(false).Any<DbFunctionAttribute>() && node.Method.GetParameters().Length == 0)
					{
						Expression expression = DbQueryVisitor.CreateObjectQueryConstant(node.Method.Invoke(contextFromConstantExpression, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null));
						if (expression != null)
						{
							return expression;
						}
					}
				}
			}
			return base.VisitMethodCall(node);
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0017962C File Offset: 0x0017782C
		protected override Expression VisitMember(MemberExpression node)
		{
			Check.NotNull<MemberExpression>(node, "node");
			PropertyInfo propertyInfo = node.Member as PropertyInfo;
			MemberExpression memberExpression = node.Expression as MemberExpression;
			if (propertyInfo != null && memberExpression != null && typeof(IQueryable).IsAssignableFrom(propertyInfo.PropertyType) && typeof(DbContext).IsAssignableFrom(node.Member.DeclaringType))
			{
				DbContext contextFromConstantExpression = DbQueryVisitor.GetContextFromConstantExpression(memberExpression.Expression, memberExpression.Member);
				if (contextFromConstantExpression != null)
				{
					Expression expression = DbQueryVisitor.CreateObjectQueryConstant(propertyInfo.GetValue(contextFromConstantExpression, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null));
					if (expression != null)
					{
						return expression;
					}
				}
			}
			return base.VisitMember(node);
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x001796D0 File Offset: 0x001778D0
		private static DbContext GetContextFromConstantExpression(Expression expression, MemberInfo member)
		{
			if (expression == null)
			{
				return DbQueryVisitor.GetContextFromMember(member, null);
			}
			ConstantExpression constantExpression = expression as ConstantExpression;
			if (constantExpression != null)
			{
				object value = constantExpression.Value;
				if (value != null)
				{
					return DbQueryVisitor.GetContextFromMember(member, value);
				}
			}
			return null;
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00179708 File Offset: 0x00177908
		private static DbContext GetContextFromMember(MemberInfo member, object value)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.GetValue(value) as DbContext;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(value, null) as DbContext;
			}
			return null;
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x00179754 File Offset: 0x00177954
		private static Expression CreateObjectQueryConstant(object dbQuery)
		{
			ObjectQuery objectQuery = DbQueryVisitor.ExtractObjectQuery(dbQuery);
			if (objectQuery != null)
			{
				Type type = objectQuery.GetType().GetGenericArguments().Single<Type>();
				Func<ObjectQuery, object> func;
				if (!DbQueryVisitor._wrapperFactories.TryGetValue(type, out func))
				{
					Type type2 = typeof(ReplacementDbQueryWrapper<>).MakeGenericType(new Type[]
					{
						type
					});
					MethodInfo declaredMethod = type2.GetDeclaredMethod("Create", new Type[]
					{
						typeof(ObjectQuery)
					});
					func = (Func<ObjectQuery, object>)Delegate.CreateDelegate(typeof(Func<ObjectQuery, object>), declaredMethod);
					DbQueryVisitor._wrapperFactories.TryAdd(type, func);
				}
				object obj = func(objectQuery);
				ConstantExpression expression = Expression.Constant(obj, obj.GetType());
				return Expression.Property(expression, "Query");
			}
			return null;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x00179820 File Offset: 0x00177A20
		private static ObjectQuery ExtractObjectQuery(object dbQuery)
		{
			IInternalQueryAdapter internalQueryAdapter = dbQuery as IInternalQueryAdapter;
			if (internalQueryAdapter != null)
			{
				return internalQueryAdapter.InternalQuery.ObjectQuery;
			}
			return null;
		}

		// Token: 0x04002346 RID: 9030
		private const BindingFlags SetAccessBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002347 RID: 9031
		private static readonly ConcurrentDictionary<Type, Func<ObjectQuery, object>> _wrapperFactories = new ConcurrentDictionary<Type, Func<ObjectQuery, object>>();
	}
}
