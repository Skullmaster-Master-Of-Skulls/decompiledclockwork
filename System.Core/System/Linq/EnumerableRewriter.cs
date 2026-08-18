using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x02000167 RID: 359
	internal class EnumerableRewriter : OldExpressionVisitor
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x0002D7E3 File Offset: 0x0002B9E3
		internal EnumerableRewriter()
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(m.Arguments);
			if (expression == m.Object && readOnlyCollection == m.Arguments)
			{
				return m;
			}
			Expression[] array = readOnlyCollection.ToArray<Expression>();
			Type[] typeArgs = m.Method.IsGenericMethod ? m.Method.GetGenericArguments() : null;
			if ((m.Method.IsStatic || m.Method.DeclaringType.IsAssignableFrom(expression.Type)) && EnumerableRewriter.ArgsMatch(m.Method, readOnlyCollection, typeArgs))
			{
				return Expression.Call(expression, m.Method, readOnlyCollection);
			}
			if (m.Method.DeclaringType == typeof(Queryable))
			{
				MethodInfo methodInfo = EnumerableRewriter.FindEnumerableMethod(m.Method.Name, readOnlyCollection, typeArgs);
				readOnlyCollection = this.FixupQuotedArgs(methodInfo, readOnlyCollection);
				return Expression.Call(expression, methodInfo, readOnlyCollection);
			}
			BindingFlags flags = BindingFlags.Static | (m.Method.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			MethodInfo methodInfo2 = EnumerableRewriter.FindMethod(m.Method.DeclaringType, m.Method.Name, readOnlyCollection, typeArgs, flags);
			readOnlyCollection = this.FixupQuotedArgs(methodInfo2, readOnlyCollection);
			return Expression.Call(expression, methodInfo2, readOnlyCollection);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002D920 File Offset: 0x0002BB20
		private ReadOnlyCollection<Expression> FixupQuotedArgs(MethodInfo mi, ReadOnlyCollection<Expression> argList)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			if (parameters.Length != 0)
			{
				List<Expression> list = null;
				int i = 0;
				int num = parameters.Length;
				while (i < num)
				{
					Expression expression = argList[i];
					ParameterInfo parameterInfo = parameters[i];
					expression = this.FixupQuotedExpression(parameterInfo.ParameterType, expression);
					if (list == null && expression != argList[i])
					{
						list = new List<Expression>(argList.Count);
						for (int j = 0; j < i; j++)
						{
							list.Add(argList[j]);
						}
					}
					if (list != null)
					{
						list.Add(expression);
					}
					i++;
				}
				if (list != null)
				{
					argList = list.ToReadOnlyCollection<Expression>();
				}
			}
			return argList;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		private Expression FixupQuotedExpression(Type type, Expression expression)
		{
			Expression expression2 = expression;
			while (!type.IsAssignableFrom(expression2.Type))
			{
				if (expression2.NodeType != ExpressionType.Quote)
				{
					if (!type.IsAssignableFrom(expression2.Type) && type.IsArray && expression2.NodeType == ExpressionType.NewArrayInit)
					{
						Type c = EnumerableRewriter.StripExpression(expression2.Type);
						if (type.IsAssignableFrom(c))
						{
							Type elementType = type.GetElementType();
							NewArrayExpression newArrayExpression = (NewArrayExpression)expression2;
							List<Expression> list = new List<Expression>(newArrayExpression.Expressions.Count);
							int i = 0;
							int count = newArrayExpression.Expressions.Count;
							while (i < count)
							{
								list.Add(this.FixupQuotedExpression(elementType, newArrayExpression.Expressions[i]));
								i++;
							}
							expression = Expression.NewArrayInit(elementType, list);
						}
					}
					return expression;
				}
				expression2 = ((UnaryExpression)expression2).Operand;
			}
			return expression2;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002DA8E File Offset: 0x0002BC8E
		internal override Expression VisitLambda(LambdaExpression lambda)
		{
			return lambda;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002DA94 File Offset: 0x0002BC94
		private static Type GetPublicType(Type t)
		{
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Lookup<, >.Grouping))
			{
				return typeof(IGrouping<, >).MakeGenericType(t.GetGenericArguments());
			}
			if (!t.IsNestedPrivate)
			{
				return t;
			}
			foreach (Type type in t.GetInterfaces())
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type;
				}
			}
			if (typeof(IEnumerable).IsAssignableFrom(t))
			{
				return typeof(IEnumerable);
			}
			return t;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002DB38 File Offset: 0x0002BD38
		internal override Expression VisitConstant(ConstantExpression c)
		{
			EnumerableQuery enumerableQuery = c.Value as EnumerableQuery;
			if (enumerableQuery == null)
			{
				return c;
			}
			if (enumerableQuery.Enumerable != null)
			{
				Type publicType = EnumerableRewriter.GetPublicType(enumerableQuery.Enumerable.GetType());
				return Expression.Constant(enumerableQuery.Enumerable, publicType);
			}
			return this.Visit(enumerableQuery.Expression);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002DB88 File Offset: 0x0002BD88
		internal override Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002DB8C File Offset: 0x0002BD8C
		private static MethodInfo FindEnumerableMethod(string name, ReadOnlyCollection<Expression> args, params Type[] typeArgs)
		{
			if (EnumerableRewriter._seqMethods == null)
			{
				EnumerableRewriter._seqMethods = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).ToLookup((MethodInfo m) => m.Name);
			}
			MethodInfo methodInfo = EnumerableRewriter._seqMethods[name].FirstOrDefault((MethodInfo m) => EnumerableRewriter.ArgsMatch(m, args, typeArgs));
			if (methodInfo == null)
			{
				throw Error.NoMethodOnTypeMatchingArguments(name, typeof(Enumerable));
			}
			if (typeArgs != null)
			{
				return methodInfo.MakeGenericMethod(typeArgs);
			}
			return methodInfo;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002DC44 File Offset: 0x0002BE44
		internal static MethodInfo FindMethod(Type type, string name, ReadOnlyCollection<Expression> args, Type[] typeArgs, BindingFlags flags)
		{
			MethodInfo[] array = (from m in type.GetMethods(flags)
			where m.Name == name
			select m).ToArray<MethodInfo>();
			if (array.Length == 0)
			{
				throw Error.NoMethodOnType(name, type);
			}
			MethodInfo methodInfo = array.FirstOrDefault((MethodInfo m) => EnumerableRewriter.ArgsMatch(m, args, typeArgs));
			if (methodInfo == null)
			{
				throw Error.NoMethodOnTypeMatchingArguments(name, type);
			}
			if (typeArgs != null)
			{
				return methodInfo.MakeGenericMethod(typeArgs);
			}
			return methodInfo;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002DCDC File Offset: 0x0002BEDC
		private static bool ArgsMatch(MethodInfo m, ReadOnlyCollection<Expression> args, Type[] typeArgs)
		{
			ParameterInfo[] parameters = m.GetParameters();
			if (parameters.Length != args.Count)
			{
				return false;
			}
			if (!m.IsGenericMethod && typeArgs != null && typeArgs.Length != 0)
			{
				return false;
			}
			if (!m.IsGenericMethodDefinition && m.IsGenericMethod && m.ContainsGenericParameters)
			{
				m = m.GetGenericMethodDefinition();
			}
			if (m.IsGenericMethodDefinition)
			{
				if (typeArgs == null || typeArgs.Length == 0)
				{
					return false;
				}
				if (m.GetGenericArguments().Length != typeArgs.Length)
				{
					return false;
				}
				m = m.MakeGenericMethod(typeArgs);
				parameters = m.GetParameters();
			}
			int i = 0;
			int count = args.Count;
			while (i < count)
			{
				Type type = parameters[i].ParameterType;
				if (type == null)
				{
					return false;
				}
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				Expression expression = args[i];
				if (!type.IsAssignableFrom(expression.Type))
				{
					if (expression.NodeType == ExpressionType.Quote)
					{
						expression = ((UnaryExpression)expression).Operand;
					}
					if (!type.IsAssignableFrom(expression.Type) && !type.IsAssignableFrom(EnumerableRewriter.StripExpression(expression.Type)))
					{
						return false;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		private static Type StripExpression(Type type)
		{
			bool isArray = type.IsArray;
			Type type2 = isArray ? type.GetElementType() : type;
			Type type3 = TypeHelper.FindGenericType(typeof(Expression<>), type2);
			if (type3 != null)
			{
				type2 = type3.GetGenericArguments()[0];
			}
			if (!isArray)
			{
				return type;
			}
			int arrayRank = type.GetArrayRank();
			if (arrayRank != 1)
			{
				return type2.MakeArrayType(arrayRank);
			}
			return type2.MakeArrayType();
		}

		// Token: 0x040007A6 RID: 1958
		private static volatile ILookup<string, MethodInfo> _seqMethods;
	}
}
