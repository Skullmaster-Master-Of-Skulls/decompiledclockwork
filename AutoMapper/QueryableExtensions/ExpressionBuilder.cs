using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions.Impl;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000055 RID: 85
	public class ExpressionBuilder : IExpressionBuilder
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00007F29 File Offset: 0x00006129
		public ExpressionBuilder(IConfigurationProvider configurationProvider)
		{
			this._configurationProvider = configurationProvider;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00007F44 File Offset: 0x00006144
		public Expression CreateMapExpression(Type sourceType, Type destinationType, IDictionary<string, object> parameters = null, params MemberInfo[] membersToExpand)
		{
			parameters = (parameters ?? new Dictionary<string, object>());
			LambdaExpression orAdd = this._expressionCache.GetOrAdd(new ExpressionRequest(sourceType, destinationType, membersToExpand), (ExpressionRequest tp) => this.CreateMapExpression(tp, new ConcurrentDictionary<ExpressionRequest, int>()));
			if (!parameters.Any<KeyValuePair<string, object>>())
			{
				return orAdd;
			}
			return new ExpressionBuilder.ConstantExpressionReplacementVisitor(parameters).Visit(orAdd);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00007F94 File Offset: 0x00006194
		public Expression<Func<TSource, TDestination>> CreateMapExpression<TSource, TDestination>(IDictionary<string, object> parameters = null, params MemberInfo[] membersToExpand)
		{
			return (Expression<Func<TSource, TDestination>>)this.CreateMapExpression(typeof(TSource), typeof(TDestination), parameters, membersToExpand);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00007FB8 File Offset: 0x000061B8
		public LambdaExpression CreateMapExpression(ExpressionRequest request, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			ParameterExpression parameterExpression = Expression.Parameter(request.SourceType, "dto");
			Expression body = this.CreateMapExpression(request, parameterExpression, typePairCount);
			return Expression.Lambda(typeof(Func<, >).MakeGenericType(new Type[]
			{
				request.SourceType,
				request.DestinationType
			}), body, new ParameterExpression[]
			{
				parameterExpression
			});
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008018 File Offset: 0x00006218
		public Expression CreateMapExpression(ExpressionRequest request, Expression instanceParameter, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			TypeMap typeMap = this._configurationProvider.ResolveTypeMap(request.SourceType, request.DestinationType);
			if (typeMap == null)
			{
				throw QueryMapperHelper.MissingMapException(request.SourceType, request.DestinationType);
			}
			ParameterReplacementVisitor parameterReplacementVisitor = (instanceParameter is ParameterExpression) ? new ParameterReplacementVisitor(instanceParameter) : null;
			LambdaExpression customProjection = typeMap.CustomProjection;
			if (customProjection == null)
			{
				List<MemberBinding> list = new List<MemberBinding>();
				if (typePairCount.AddOrUpdate(request, 0, (ExpressionRequest tp, int i) => i + 1) >= typeMap.MaxDepth)
				{
					if (this._configurationProvider.AllowNullDestinationValues)
					{
						return null;
					}
				}
				else
				{
					list = this.CreateMemberBindings(request, typeMap, instanceParameter, typePairCount);
				}
				Expression node = typeMap.DestinationConstructorExpression(instanceParameter);
				if (parameterReplacementVisitor != null)
				{
					node = parameterReplacementVisitor.Visit(node);
				}
				ExpressionBuilder.NewFinderVisitor newFinderVisitor = new ExpressionBuilder.NewFinderVisitor();
				newFinderVisitor.Visit(node);
				return Expression.MemberInit(newFinderVisitor.NewExpression, list.ToArray());
			}
			if (parameterReplacementVisitor != null)
			{
				return parameterReplacementVisitor.Visit(customProjection.Body);
			}
			return customProjection.Body;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008108 File Offset: 0x00006308
		private List<MemberBinding> CreateMemberBindings(ExpressionRequest request, TypeMap typeMap, Expression instanceParameter, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			List<MemberBinding> list = new List<MemberBinding>();
			using (IEnumerator<PropertyMap> enumerator = (from pm in typeMap.GetPropertyMaps()
			where pm.CanResolveValue()
			select pm).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExpressionBuilder.<>c__DisplayClass10_0 CS$<>8__locals1 = new ExpressionBuilder.<>c__DisplayClass10_0();
					CS$<>8__locals1.propertyMap = enumerator.Current;
					ExpressionResolutionResult result = ExpressionBuilder.ResolveExpression(CS$<>8__locals1.propertyMap, request.SourceType, instanceParameter);
					if (!CS$<>8__locals1.propertyMap.ExplicitExpansion || request.MembersToExpand.Contains(CS$<>8__locals1.propertyMap.DestinationProperty.MemberInfo))
					{
						TypeMap propertyTypeMap = this._configurationProvider.ResolveTypeMap(result.Type, CS$<>8__locals1.propertyMap.DestinationPropertyType);
						ExpressionRequest request2 = new ExpressionRequest(result.Type, CS$<>8__locals1.propertyMap.DestinationPropertyType, request.MembersToExpand);
						IExpressionBinder expressionBinder = ExpressionBuilder.Binders.FirstOrDefault((IExpressionBinder b) => b.IsMatch(CS$<>8__locals1.propertyMap, propertyTypeMap, result));
						if (expressionBinder == null)
						{
							string format = "Unable to create a map expression from {0}.{1} ({2}) to {3}.{4} ({5})";
							object[] array = new object[6];
							int num = 0;
							MemberInfo sourceMember = CS$<>8__locals1.propertyMap.SourceMember;
							object obj;
							if (sourceMember == null)
							{
								obj = null;
							}
							else
							{
								Type declaringType = sourceMember.DeclaringType;
								obj = ((declaringType != null) ? declaringType.Name : null);
							}
							array[num] = obj;
							int num2 = 1;
							MemberInfo sourceMember2 = CS$<>8__locals1.propertyMap.SourceMember;
							array[num2] = ((sourceMember2 != null) ? sourceMember2.Name : null);
							array[2] = result.Type;
							int num3 = 3;
							Type declaringType2 = CS$<>8__locals1.propertyMap.DestinationProperty.MemberInfo.DeclaringType;
							array[num3] = ((declaringType2 != null) ? declaringType2.Name : null);
							array[4] = CS$<>8__locals1.propertyMap.DestinationProperty.Name;
							array[5] = CS$<>8__locals1.propertyMap.DestinationPropertyType;
							throw new AutoMapperMappingException(string.Format(format, array));
						}
						MemberAssignment memberAssignment = expressionBinder.Build(this._configurationProvider, CS$<>8__locals1.propertyMap, propertyTypeMap, request2, result, typePairCount);
						if (memberAssignment != null)
						{
							list.Add(memberAssignment);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008364 File Offset: 0x00006564
		private static ExpressionResolutionResult ResolveExpression(PropertyMap propertyMap, Type currentType, Expression instanceParameter)
		{
			ExpressionResolutionResult result = new ExpressionResolutionResult(instanceParameter, currentType);
			using (IEnumerator<IValueResolver> enumerator = propertyMap.GetSourceValueResolvers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IValueResolver resolver = enumerator.Current;
					IExpressionResultConverter expressionResultConverter = ExpressionBuilder.ExpressionResultConverters.FirstOrDefault((IExpressionResultConverter c) => c.CanGetExpressionResolutionResult(result, resolver));
					if (expressionResultConverter == null)
					{
						throw new Exception("Can't resolve this to Queryable Expression");
					}
					result = expressionResultConverter.GetExpressionResolutionResult(result, propertyMap, resolver);
				}
			}
			return result;
		}

		// Token: 0x040000A3 RID: 163
		private static readonly IExpressionResultConverter[] ExpressionResultConverters = new IExpressionResultConverter[]
		{
			new MemberGetterExpressionResultConverter(),
			new MemberResolverExpressionResultConverter(),
			new NullSubstitutionExpressionResultConverter()
		};

		// Token: 0x040000A4 RID: 164
		private static readonly IExpressionBinder[] Binders = new IExpressionBinder[]
		{
			new NullableExpressionBinder(),
			new AssignableExpressionBinder(),
			new EnumerableExpressionBinder(),
			new MappedTypeExpressionBinder(),
			new CustomProjectionExpressionBinder(),
			new StringExpressionBinder()
		};

		// Token: 0x040000A5 RID: 165
		private readonly ConcurrentDictionary<ExpressionRequest, LambdaExpression> _expressionCache = new ConcurrentDictionary<ExpressionRequest, LambdaExpression>();

		// Token: 0x040000A6 RID: 166
		private readonly IConfigurationProvider _configurationProvider;

		// Token: 0x02000120 RID: 288
		private class NewFinderVisitor : ExpressionVisitor
		{
			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001707D File Offset: 0x0001527D
			// (set) Token: 0x060006FD RID: 1789 RVA: 0x00017085 File Offset: 0x00015285
			public NewExpression NewExpression { get; private set; }

			// Token: 0x060006FE RID: 1790 RVA: 0x0001708E File Offset: 0x0001528E
			protected override Expression VisitNew(NewExpression node)
			{
				this.NewExpression = node;
				return base.VisitNew(node);
			}
		}

		// Token: 0x02000121 RID: 289
		private class ConstantExpressionReplacementVisitor : ExpressionVisitor
		{
			// Token: 0x06000700 RID: 1792 RVA: 0x000170A6 File Offset: 0x000152A6
			public ConstantExpressionReplacementVisitor(IDictionary<string, object> paramValues)
			{
				this._paramValues = paramValues;
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x000170B8 File Offset: 0x000152B8
			protected override Expression VisitMember(MemberExpression node)
			{
				if (!node.Member.DeclaringType.Name.Contains("<>"))
				{
					return base.VisitMember(node);
				}
				if (!this._paramValues.ContainsKey(node.Member.Name))
				{
					return base.VisitMember(node);
				}
				return Expression.Convert(Expression.Constant(this._paramValues[node.Member.Name]), node.Member.GetMemberType());
			}

			// Token: 0x04000214 RID: 532
			private readonly IDictionary<string, object> _paramValues;
		}
	}
}
