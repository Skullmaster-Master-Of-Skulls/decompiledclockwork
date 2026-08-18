using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AutoMapper.Internal
{
	// Token: 0x0200009B RID: 155
	public class DelegateFactory
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x000127BC File Offset: 0x000109BC
		public LateBoundMethod CreateGet(MethodInfo method)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "target");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "arguments");
			MethodCallExpression expression;
			if (!method.IsDefined(typeof(ExtensionAttribute), false))
			{
				expression = Expression.Call(Expression.Convert(parameterExpression, method.DeclaringType), method, DelegateFactory.CreateParameterExpressions(method, parameterExpression, parameterExpression2));
			}
			else
			{
				expression = Expression.Call(method, DelegateFactory.CreateParameterExpressions(method, parameterExpression, parameterExpression2));
			}
			return Expression.Lambda<LateBoundMethod>(Expression.Convert(expression, typeof(object)), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001285C File Offset: 0x00010A5C
		public LateBoundPropertyGet CreateGet(PropertyInfo property)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<LateBoundPropertyGet>(Expression.Convert(Expression.Property(Expression.Convert(parameterExpression, property.DeclaringType), property), typeof(object)), new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000128B4 File Offset: 0x00010AB4
		public LateBoundFieldGet CreateGet(FieldInfo field)
		{
			ParameterExpression parameterExpression;
			return Expression.Lambda<LateBoundFieldGet>(Expression.Convert(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), typeof(object)), new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001290C File Offset: 0x00010B0C
		public LateBoundFieldSet CreateSet(FieldInfo field)
		{
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			return Expression.Lambda<LateBoundFieldSet>(Expression.Assign(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), Expression.Convert(parameterExpression2, field.FieldType)), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012980 File Offset: 0x00010B80
		public LateBoundPropertySet CreateSet(PropertyInfo property)
		{
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			return Expression.Lambda<LateBoundPropertySet>(Expression.Assign(Expression.Property(Expression.Convert(parameterExpression, property.DeclaringType), property), Expression.Convert(parameterExpression2, property.PropertyType)), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000129F4 File Offset: 0x00010BF4
		public LateBoundCtor CreateCtor(Type type)
		{
			return this._ctorCache.GetOrAdd(type, delegate(Type t)
			{
				if (!type.IsClass())
				{
					return Expression.Lambda<LateBoundCtor>(Expression.Convert(Expression.New(type), typeof(object)), new ParameterExpression[0]).Compile();
				}
				ConstructorInfo constructorInfo = (from ci in type.GetDeclaredConstructors()
				where !ci.IsStatic
				select ci).FirstOrDefault((ConstructorInfo c) => c.GetParameters().All((ParameterInfo p) => p.IsOptional));
				if (constructorInfo == null)
				{
					throw new ArgumentException(type + " needs to have a constructor with 0 args or only optional args", "type");
				}
				ConstantExpression[] arguments = (from p in constructorInfo.GetParameters()
				select Expression.Constant(p.DefaultValue, p.ParameterType)).ToArray<ConstantExpression>();
				return Expression.Lambda<LateBoundCtor>(Expression.Convert(Expression.New(constructorInfo, arguments), typeof(object)), new ParameterExpression[0]).Compile();
			});
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00012A2C File Offset: 0x00010C2C
		private static Expression[] CreateParameterExpressions(MethodInfo method, Expression instanceParameter, Expression argumentsParameter)
		{
			List<UnaryExpression> list = new List<UnaryExpression>();
			ParameterInfo[] source = method.GetParameters();
			if (method.IsDefined(typeof(ExtensionAttribute), false))
			{
				Type parameterType = method.GetParameters()[0].ParameterType;
				list.Add(Expression.Convert(instanceParameter, parameterType));
				source = source.Skip(1).ToArray<ParameterInfo>();
			}
			list.AddRange(source.Select((ParameterInfo parameter, int index) => Expression.Convert(Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)), parameter.ParameterType)));
			return list.ToArray();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00012AAC File Offset: 0x00010CAC
		public LateBoundParamsCtor CreateCtor(ConstructorInfo constructorInfo, IEnumerable<ConstructorParameterMap> ctorParams)
		{
			ParameterExpression paramsExpr = Expression.Parameter(typeof(object[]), "parameters");
			UnaryExpression[] arguments = ctorParams.Select((ConstructorParameterMap ctorParam, int i) => Expression.Convert(Expression.ArrayIndex(paramsExpr, Expression.Constant(i)), ctorParam.Parameter.ParameterType)).ToArray<UnaryExpression>();
			return Expression.Lambda<LateBoundParamsCtor>(Expression.New(constructorInfo, arguments), new ParameterExpression[]
			{
				paramsExpr
			}).Compile();
		}

		// Token: 0x040000DB RID: 219
		private readonly ConcurrentDictionary<Type, LateBoundCtor> _ctorCache = new ConcurrentDictionary<Type, LateBoundCtor>();
	}
}
