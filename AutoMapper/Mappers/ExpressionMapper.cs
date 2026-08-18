using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions.Impl;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007F RID: 127
	public class ExpressionMapper : IObjectMapper
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00011060 File Offset: 0x0000F260
		public object Map(ResolutionContext context)
		{
			Type type = context.SourceType.GetTypeInfo().GenericTypeArguments[0];
			Type type2 = context.DestinationType.GetTypeInfo().GenericTypeArguments[0];
			LambdaExpression lambdaExpression = (LambdaExpression)context.SourceValue;
			if (type.GetGenericTypeDefinition() != type2.GetGenericTypeDefinition())
			{
				throw new AutoMapperMappingException("Source and destination expressions must be of the same type.");
			}
			Type type3 = type2.GetTypeInfo().GenericTypeArguments[0];
			if (type3.IsGenericType())
			{
				type3 = type3.GetTypeInfo().GenericTypeArguments[0];
			}
			Type type4 = type.GetTypeInfo().GenericTypeArguments[0];
			if (type4.IsGenericType())
			{
				type4 = type4.GetTypeInfo().GenericTypeArguments[0];
			}
			TypeMap typeMap = context.ConfigurationProvider.ResolveTypeMap(type3, type4);
			ExpressionMapper.MappingVisitor parentMappingVisitor = new ExpressionMapper.MappingVisitor(context.ConfigurationProvider, type2.GetTypeInfo().GenericTypeArguments);
			ExpressionMapper.MappingVisitor mappingVisitor = new ExpressionMapper.MappingVisitor(context.ConfigurationProvider, typeMap, lambdaExpression.Parameters[0], Expression.Parameter(type2.GetTypeInfo().GenericTypeArguments[0], lambdaExpression.Parameters[0].Name), parentMappingVisitor, type2.GetTypeInfo().GenericTypeArguments);
			IEnumerable<ParameterExpression> parameters = lambdaExpression.Parameters.Select(new Func<ParameterExpression, Expression>(mappingVisitor.Visit)).OfType<ParameterExpression>();
			return Expression.Lambda(mappingVisitor.Visit(lambdaExpression.Body), parameters);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000111AC File Offset: 0x0000F3AC
		public bool IsMatch(TypePair context)
		{
			return typeof(LambdaExpression).IsAssignableFrom(context.SourceType) && context.SourceType != typeof(LambdaExpression) && typeof(LambdaExpression).IsAssignableFrom(context.DestinationType) && context.DestinationType != typeof(LambdaExpression);
		}

		// Token: 0x0200013A RID: 314
		internal class MappingVisitor : ExpressionVisitor
		{
			// Token: 0x060008F7 RID: 2295 RVA: 0x00018435 File Offset: 0x00016635
			public MappingVisitor(IConfigurationProvider configurationProvider, IList<Type> destSubTypes) : this(configurationProvider, null, Expression.Parameter(typeof(Nullable)), Expression.Parameter(typeof(Nullable)), null, destSubTypes)
			{
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x00018460 File Offset: 0x00016660
			internal MappingVisitor(IConfigurationProvider configurationProvider, TypeMap typeMap, Expression oldParam, Expression newParam, ExpressionMapper.MappingVisitor parentMappingVisitor = null, IList<Type> destSubTypes = null)
			{
				this._configurationProvider = configurationProvider;
				this._typeMap = typeMap;
				this._oldParam = oldParam;
				this._newParam = newParam;
				this._parentMappingVisitor = parentMappingVisitor;
				if (destSubTypes != null)
				{
					this._destSubTypes = destSubTypes;
				}
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x000184B0 File Offset: 0x000166B0
			protected override Expression VisitConstant(ConstantExpression node)
			{
				if (node == this._oldParam)
				{
					return this._newParam;
				}
				return node;
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x000184B0 File Offset: 0x000166B0
			protected override Expression VisitParameter(ParameterExpression node)
			{
				if (node == this._oldParam)
				{
					return this._newParam;
				}
				return node;
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x000184C3 File Offset: 0x000166C3
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				return base.VisitMethodCall(this.GetConvertedMethodCall(node));
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x000184D2 File Offset: 0x000166D2
			protected override Expression VisitExtension(Expression node)
			{
				if (node.NodeType == (ExpressionType)10000)
				{
					return node;
				}
				return base.VisitExtension(node);
			}

			// Token: 0x060008FD RID: 2301 RVA: 0x000184EC File Offset: 0x000166EC
			private MethodCallExpression GetConvertedMethodCall(MethodCallExpression node)
			{
				if (!node.Method.IsGenericMethod)
				{
					return node;
				}
				ReadOnlyCollection<Expression> convertedArguments = base.Visit(node.Arguments);
				Type[] typeArguments = (from t in node.Method.GetGenericArguments()
				select ExpressionMapper.MappingVisitor.GetConvertingTypeIfExists(node.Arguments, t, convertedArguments)).ToArray<Type>();
				return Expression.Call(node.Method.GetGenericMethodDefinition().MakeGenericMethod(typeArguments), convertedArguments);
			}

			// Token: 0x060008FE RID: 2302 RVA: 0x00018580 File Offset: 0x00016780
			private static Type GetConvertingTypeIfExists(IList<Expression> args, Type t, IList<Expression> arguments)
			{
				Expression expression = (from a in args
				where !a.Type.IsGenericType()
				select a).FirstOrDefault((Expression a) => a.Type == t);
				if (expression != null)
				{
					int num = args.IndexOf(expression);
					if (num < 0)
					{
						return t;
					}
					return arguments[num].Type;
				}
				else
				{
					Expression item = (from a in args
					where a.Type.IsGenericType()
					select a).FirstOrDefault((Expression a) => a.Type.GetTypeInfo().GenericTypeArguments[0] == t);
					int num2 = args.IndexOf(item);
					if (num2 < 0)
					{
						return t;
					}
					return arguments[num2].Type.GetTypeInfo().GenericTypeArguments[0];
				}
			}

			// Token: 0x060008FF RID: 2303 RVA: 0x0001865C File Offset: 0x0001685C
			protected override Expression VisitBinary(BinaryExpression node)
			{
				Expression left = base.Visit(node.Left);
				Expression right = base.Visit(node.Right);
				ExpressionMapper.MappingVisitor.CheckNullableToNonNullableChanges(node.Left, node.Right, ref left, ref right);
				ExpressionMapper.MappingVisitor.CheckNullableToNonNullableChanges(node.Right, node.Left, ref right, ref left);
				return Expression.MakeBinary(node.NodeType, left, right);
			}

			// Token: 0x06000900 RID: 2304 RVA: 0x000186BC File Offset: 0x000168BC
			private static void CheckNullableToNonNullableChanges(Expression left, Expression right, ref Expression newLeft, ref Expression newRight)
			{
				if (ExpressionMapper.MappingVisitor.GoingFromNonNullableToNullable(left, newLeft))
				{
					if (ExpressionMapper.MappingVisitor.BothAreNonNullable(right, newRight))
					{
						ExpressionMapper.MappingVisitor.UpdateToNullableExpression(right, out newRight);
					}
					else if (ExpressionMapper.MappingVisitor.BothAreNullable(right, newRight))
					{
						ExpressionMapper.MappingVisitor.UpdateToNonNullableExpression(right, out newRight);
					}
				}
				if (ExpressionMapper.MappingVisitor.GoingFromNonNullableToNullable(newLeft, left))
				{
					if (ExpressionMapper.MappingVisitor.BothAreNonNullable(right, newRight))
					{
						ExpressionMapper.MappingVisitor.UpdateToNullableExpression(right, out newRight);
						return;
					}
					if (ExpressionMapper.MappingVisitor.BothAreNullable(right, newRight))
					{
						ExpressionMapper.MappingVisitor.UpdateToNonNullableExpression(right, out newRight);
					}
				}
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x00018724 File Offset: 0x00016924
			private static void UpdateToNullableExpression(Expression right, out Expression newRight)
			{
				if (right is ConstantExpression)
				{
					newRight = Expression.Constant((right as ConstantExpression).Value, typeof(Nullable<>).MakeGenericType(new Type[]
					{
						right.Type
					}));
					return;
				}
				throw new AutoMapperMappingException("Mapping a BinaryExpression where one side is nullable and the other isn't");
			}

			// Token: 0x06000902 RID: 2306 RVA: 0x00018774 File Offset: 0x00016974
			private static void UpdateToNonNullableExpression(Expression right, out Expression newRight)
			{
				if (right is ConstantExpression)
				{
					newRight = Expression.Constant((right as ConstantExpression).Value, typeof(Nullable<>).MakeGenericType(new Type[]
					{
						right.Type
					}));
					return;
				}
				if (right is UnaryExpression)
				{
					newRight = (right as UnaryExpression).Operand;
					return;
				}
				throw new AutoMapperMappingException("Mapping a BinaryExpression where one side is nullable and the other isn't");
			}

			// Token: 0x06000903 RID: 2307 RVA: 0x000187DA File Offset: 0x000169DA
			private static bool GoingFromNonNullableToNullable(Expression node, Expression newLeft)
			{
				return !node.Type.IsNullableType() && newLeft.Type.IsNullableType();
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x000187F6 File Offset: 0x000169F6
			private static bool BothAreNullable(Expression node, Expression newLeft)
			{
				return node.Type.IsNullableType() && newLeft.Type.IsNullableType();
			}

			// Token: 0x06000905 RID: 2309 RVA: 0x00018812 File Offset: 0x00016A12
			private static bool BothAreNonNullable(Expression node, Expression newLeft)
			{
				return !node.Type.IsNullableType() && !newLeft.Type.IsNullableType();
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x00018831 File Offset: 0x00016A31
			protected override Expression VisitLambda<T>(Expression<T> expression)
			{
				if (expression.Parameters.Any((ParameterExpression b) => b.Type == this._oldParam.Type))
				{
					return this.VisitLambdaExpression<T>(expression);
				}
				return this.VisitAllParametersExpression<T>(expression);
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x0001885C File Offset: 0x00016A5C
			private Expression VisitLambdaExpression<T>(Expression<T> expression)
			{
				Expression body = base.Visit(expression.Body);
				List<ParameterExpression> parameters = (from e in expression.Parameters
				select base.Visit(e) as ParameterExpression).ToList<ParameterExpression>();
				return Expression.Lambda(body, parameters);
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x00018898 File Offset: 0x00016A98
			private Expression VisitAllParametersExpression<T>(Expression<T> expression)
			{
				List<ExpressionVisitor> list = new List<ExpressionVisitor>();
				for (int i = 0; i < expression.Parameters.Count; i++)
				{
					Type sourceParamType = expression.Parameters[i].Type;
					IEnumerable<Type> destSubTypes = this._destSubTypes;
					Func<Type, bool> predicate;
					Func<Type, bool> <>9__0;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((Type dt) => dt != sourceParamType));
					}
					foreach (Type type in destSubTypes.Where(predicate))
					{
						Type type2 = type.IsGenericType() ? type.GetTypeInfo().GenericTypeArguments[0] : type;
						TypeMap typeMap = this._configurationProvider.FindTypeMapFor(type2, sourceParamType);
						if (typeMap != null)
						{
							ParameterExpression parameterExpression = expression.Parameters[i];
							ParameterExpression newParam = Expression.Parameter(type2, parameterExpression.Name);
							list.Add(new ExpressionMapper.MappingVisitor(this._configurationProvider, typeMap, parameterExpression, newParam, this, null));
						}
					}
				}
				return list.Aggregate(expression, (Expression e, ExpressionVisitor v) => v.Visit(e));
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x000189D8 File Offset: 0x00016BD8
			protected override Expression VisitMember(MemberExpression node)
			{
				if (node == this._oldParam)
				{
					return this._newParam;
				}
				PropertyMap propertyMap = this.PropertyMap(node);
				if (propertyMap == null)
				{
					if (node.Expression is MemberExpression)
					{
						return this.GetConvertedSubMemberCall(node);
					}
					return node;
				}
				else
				{
					this.SetSorceSubTypes(propertyMap);
					Expression expression = this.Visit(node.Expression);
					if (expression == node.Expression)
					{
						expression = this._parentMappingVisitor.Visit(node.Expression);
					}
					if (propertyMap.CustomExpression != null)
					{
						return this.ConvertCustomExpression(expression, propertyMap);
					}
					Func<Expression, IMemberGetter, Expression> func = (Expression current, IMemberGetter memberGetter) => Expression.MakeMemberAccess(current, memberGetter.MemberInfo);
					return propertyMap.GetSourceValueResolvers().OfType<IMemberGetter>().Aggregate(expression, func);
				}
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x00018A8C File Offset: 0x00016C8C
			private Expression GetConvertedSubMemberCall(MemberExpression node)
			{
				Expression expression = this.Visit(node.Expression);
				PropertyMap propertyMap = this.FindPropertyMapOfExpression(node.Expression as MemberExpression);
				if (propertyMap == null)
				{
					return node;
				}
				Type memberType = propertyMap.SourceMember.GetMemberType();
				Type destinationPropertyType = propertyMap.DestinationPropertyType;
				if (memberType == destinationPropertyType)
				{
					return Expression.MakeMemberAccess(expression, node.Member);
				}
				TypeMap typeMap = this._configurationProvider.FindTypeMapFor(memberType, destinationPropertyType);
				ExpressionMapper.MappingVisitor mappingVisitor = new ExpressionMapper.MappingVisitor(this._configurationProvider, typeMap, node.Expression, expression, this, null);
				Expression result = mappingVisitor.Visit(node);
				this._destSubTypes = this._destSubTypes.Concat(mappingVisitor._destSubTypes).ToArray<Type>();
				return result;
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x00018B34 File Offset: 0x00016D34
			private PropertyMap FindPropertyMapOfExpression(MemberExpression expression)
			{
				PropertyMap propertyMap = this.PropertyMap(expression);
				if (propertyMap == null && expression.Expression is MemberExpression)
				{
					return this.FindPropertyMapOfExpression(expression.Expression as MemberExpression);
				}
				return propertyMap;
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x00018B6C File Offset: 0x00016D6C
			private PropertyMap PropertyMap(MemberExpression node)
			{
				if (node.Member.IsStatic())
				{
					return null;
				}
				IMemberAccessor destinationProperty = node.Member.ToMemberAccessor();
				return this._typeMap.GetExistingPropertyMapFor(destinationProperty);
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x00018BA0 File Offset: 0x00016DA0
			private void SetSorceSubTypes(PropertyMap propertyMap)
			{
				if (propertyMap.SourceMember is PropertyInfo)
				{
					this._destSubTypes = (propertyMap.SourceMember as PropertyInfo).PropertyType.GetTypeInfo().GenericTypeArguments.Concat(new Type[]
					{
						(propertyMap.SourceMember as PropertyInfo).PropertyType
					}).ToList<Type>();
					return;
				}
				if (propertyMap.SourceMember is FieldInfo)
				{
					this._destSubTypes = (propertyMap.SourceMember as FieldInfo).FieldType.GetTypeInfo().GenericTypeArguments;
				}
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x00018C2B File Offset: 0x00016E2B
			private Expression ConvertCustomExpression(Expression node, PropertyMap propertyMap)
			{
				return new ParameterReplacementVisitor(node).Visit(propertyMap.CustomExpression.Body);
			}

			// Token: 0x040003F1 RID: 1009
			private IList<Type> _destSubTypes = new Type[0];

			// Token: 0x040003F2 RID: 1010
			private readonly IConfigurationProvider _configurationProvider;

			// Token: 0x040003F3 RID: 1011
			private readonly TypeMap _typeMap;

			// Token: 0x040003F4 RID: 1012
			private readonly Expression _oldParam;

			// Token: 0x040003F5 RID: 1013
			private readonly Expression _newParam;

			// Token: 0x040003F6 RID: 1014
			private readonly ExpressionMapper.MappingVisitor _parentMappingVisitor;
		}
	}
}
