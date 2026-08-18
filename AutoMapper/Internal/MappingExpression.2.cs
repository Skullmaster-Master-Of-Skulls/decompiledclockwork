using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000AD RID: 173
	public class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>, IMemberConfigurationExpression<TSource>
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00013000 File Offset: 0x00011200
		public MappingExpression(TypeMap typeMap, Func<Type, object> serviceCtor, IProfileExpression profile)
		{
			this.TypeMap = typeMap;
			this._serviceCtor = serviceCtor;
			this.Profile = profile;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001301D File Offset: 0x0001121D
		public TypeMap TypeMap { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00013025 File Offset: 0x00011225
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0001302D File Offset: 0x0001122D
		public PropertyMap PropertyMap { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00013036 File Offset: 0x00011236
		public IProfileExpression Profile { get; }

		// Token: 0x060004EF RID: 1263 RVA: 0x00013040 File Offset: 0x00011240
		public IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember, Action<IMemberConfigurationExpression<TSource>> memberOptions)
		{
			IMemberAccessor destinationProperty = ReflectionHelper.FindProperty(destinationMember).ToMemberAccessor();
			this.ForDestinationMember(destinationProperty, memberOptions);
			return this;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00013064 File Offset: 0x00011264
		public IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberConfigurationExpression<TSource>> memberOptions)
		{
			IMemberAccessor memberAccessor = null;
			PropertyInfo property = this.TypeMap.DestinationType.GetProperty(name);
			if (property != null)
			{
				memberAccessor = new PropertyAccessor(property);
			}
			if (memberAccessor == null)
			{
				FieldInfo field = this.TypeMap.DestinationType.GetField(name);
				if (field == null)
				{
					throw new ArgumentOutOfRangeException("name", "Cannot find a field or property named " + name);
				}
				memberAccessor = new FieldAccessor(field);
			}
			this.ForDestinationMember(memberAccessor, memberOptions);
			return this;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000130D8 File Offset: 0x000112D8
		public void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions)
		{
			new TypeDetails(this.TypeMap.DestinationType, this.Profile.ShouldMapProperty, this.Profile.ShouldMapField).PublicWriteAccessors.Each(delegate(MemberInfo acc)
			{
				this.ForDestinationMember(acc.ToMemberAccessor(), memberOptions);
			});
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013138 File Offset: 0x00011338
		public IMappingExpression<TSource, TDestination> IgnoreAllPropertiesWithAnInaccessibleSetter()
		{
			foreach (PropertyInfo propertyInfo in typeof(TDestination).GetDeclaredProperties().Where(new Func<PropertyInfo, bool>(this.HasAnInaccessibleSetter)))
			{
				this.ForMember(propertyInfo.Name, delegate(IMemberConfigurationExpression<TSource> opt)
				{
					opt.Ignore();
				});
			}
			return this;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000131C8 File Offset: 0x000113C8
		public IMappingExpression<TSource, TDestination> IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
		{
			foreach (PropertyInfo propertyInfo in typeof(TSource).GetDeclaredProperties().Where(new Func<PropertyInfo, bool>(this.HasAnInaccessibleSetter)))
			{
				this.ForSourceMember(propertyInfo.Name, delegate(ISourceMemberConfigurationExpression opt)
				{
					opt.Ignore();
				});
			}
			return this;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00013258 File Offset: 0x00011458
		private bool HasAnInaccessibleSetter(PropertyInfo property)
		{
			MethodInfo setMethod = property.GetSetMethod(true);
			return setMethod == null || setMethod.IsPrivate || setMethod.IsFamily;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00013286 File Offset: 0x00011486
		public IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>() where TOtherSource : TSource where TOtherDestination : TDestination
		{
			return this.Include(typeof(TOtherSource), typeof(TOtherDestination));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000132A2 File Offset: 0x000114A2
		public IMappingExpression<TSource, TDestination> Include(Type otherSourceType, Type otherDestinationType)
		{
			this.TypeMap.IncludeDerivedTypes(otherSourceType, otherDestinationType);
			return this;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000132B2 File Offset: 0x000114B2
		public IMappingExpression<TSource, TDestination> IncludeBase<TSourceBase, TDestinationBase>()
		{
			return this.IncludeBase(typeof(TSourceBase), typeof(TDestinationBase));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000132D0 File Offset: 0x000114D0
		public IMappingExpression<TSource, TDestination> IncludeBase(Type sourceBase, Type destinationBase)
		{
			Type typeFromHandle = typeof(object);
			Type type = sourceBase;
			Type type2 = destinationBase;
			while (type != null && type2 != null && type != typeFromHandle && type2 != typeFromHandle)
			{
				TypeMap typeMap = this.Profile.CreateMap(type, type2).TypeMap;
				typeMap.IncludeDerivedTypes(this.TypeMap.SourceType, this.TypeMap.DestinationType);
				this.TypeMap.ApplyInheritedMap(typeMap);
				type = type.BaseType();
				type2 = type2.BaseType();
			}
			return this;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001335C File Offset: 0x0001155C
		public IMappingExpression<TSource, TDestination> WithProfile(string profileName)
		{
			this.TypeMap.Profile = profileName;
			return this;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001336B File Offset: 0x0001156B
		public void ProjectUsing(Expression<Func<TSource, TDestination>> projectionExpression)
		{
			this.TypeMap.UseCustomProjection(projectionExpression);
			this.ConvertUsing(projectionExpression.Compile());
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00013385 File Offset: 0x00011585
		public void NullSubstitute(object nullSubstitute)
		{
			this.PropertyMap.SetNullSubstitute(nullSubstitute);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013394 File Offset: 0x00011594
		public IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver
		{
			DeferredInstantiatedResolver valueResolver = new DeferredInstantiatedResolver(this.BuildCtor<IValueResolver>(typeof(TValueResolver)));
			this.ResolveUsing(valueResolver);
			return new ResolutionExpression<TSource, TValueResolver>(this.TypeMap.SourceType, this.PropertyMap);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000133D8 File Offset: 0x000115D8
		public IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType)
		{
			DeferredInstantiatedResolver valueResolver = new DeferredInstantiatedResolver(this.BuildCtor<IValueResolver>(valueResolverType));
			this.ResolveUsing(valueResolver);
			return new ResolutionExpression<TSource>(this.TypeMap.SourceType, this.PropertyMap);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00013410 File Offset: 0x00011610
		public IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver)
		{
			this.PropertyMap.AssignCustomValueResolver(valueResolver);
			return new ResolutionExpression<TSource>(this.TypeMap.SourceType, this.PropertyMap);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013434 File Offset: 0x00011634
		public void ResolveUsing(Func<TSource, object> resolver)
		{
			this.PropertyMap.AssignCustomValueResolver(new DelegateBasedResolver<TSource>((ResolutionResult r) => resolver((TSource)((object)r.Value))));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001346A File Offset: 0x0001166A
		public void ResolveUsing(Func<ResolutionResult, object> resolver)
		{
			this.PropertyMap.AssignCustomValueResolver(new DelegateBasedResolver<TSource>(resolver));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013480 File Offset: 0x00011680
		public void ResolveUsing(Func<ResolutionResult, TSource, object> resolver)
		{
			this.PropertyMap.AssignCustomValueResolver(new DelegateBasedResolver<TSource>((ResolutionResult r) => resolver(r, (TSource)((object)r.Value))));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000134B6 File Offset: 0x000116B6
		public void MapFrom<TMember>(Expression<Func<TSource, TMember>> sourceMember)
		{
			this.PropertyMap.SetCustomValueResolverExpression<TSource, TMember>(sourceMember);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000134C4 File Offset: 0x000116C4
		public void MapFrom<TMember>(string property)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource));
			Expression<Func<TSource, TMember>> customValueResolverExpression = Expression.Lambda<Func<TSource, TMember>>(Expression.Property(parameterExpression, property), new ParameterExpression[]
			{
				parameterExpression
			});
			this.PropertyMap.SetCustomValueResolverExpression<TSource, TMember>(customValueResolverExpression);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013504 File Offset: 0x00011704
		public void UseValue<TValue>(TValue value)
		{
			this.MapFrom<TValue>((TSource src) => value);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001356C File Offset: 0x0001176C
		public void UseValue(object value)
		{
			this.PropertyMap.AssignCustomValueResolver(new DelegateBasedResolver<TSource>((ResolutionResult src) => value));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000135A4 File Offset: 0x000117A4
		public void Condition(Func<TSource, bool> condition)
		{
			this.Condition((ResolutionContext context) => condition((TSource)((object)context.Parent.SourceValue)));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000135D0 File Offset: 0x000117D0
		public void Condition(Func<ResolutionContext, bool> condition)
		{
			this.PropertyMap.ApplyCondition(condition);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000135E0 File Offset: 0x000117E0
		public void PreCondition(Func<TSource, bool> condition)
		{
			this.PreCondition((ResolutionContext context) => condition((TSource)((object)context.Parent.SourceValue)));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001360C File Offset: 0x0001180C
		public void PreCondition(Func<ResolutionContext, bool> condition)
		{
			this.PropertyMap.ApplyPreCondition(condition);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001361A File Offset: 0x0001181A
		public void ExplicitExpansion()
		{
			this.PropertyMap.ExplicitExpansion = true;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013628 File Offset: 0x00011828
		public IMappingExpression<TSource, TDestination> MaxDepth(int depth)
		{
			this.TypeMap.MaxDepth = depth;
			return this;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013637 File Offset: 0x00011837
		public IMappingExpression<TSource, TDestination> ConstructUsingServiceLocator()
		{
			this.TypeMap.ConstructDestinationUsingServiceLocator = true;
			return this;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013648 File Offset: 0x00011848
		public IMappingExpression<TDestination, TSource> ReverseMap()
		{
			IMappingExpression<TDestination, TSource> mappingExpression = this.Profile.CreateMap<TDestination, TSource>(MemberList.Source);
			return this.ConfigureReverseMap(mappingExpression);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001366C File Offset: 0x0001186C
		protected IMappingExpression<TDestination, TSource> ConfigureReverseMap(IMappingExpression<TDestination, TSource> mappingExpression)
		{
			foreach (PropertyMap propertyMap in from pm in this.TypeMap.GetPropertyMaps()
			where pm.IsIgnored()
			select pm)
			{
				mappingExpression.ForSourceMember(propertyMap.DestinationProperty.Name, delegate(ISourceMemberConfigurationExpression opt)
				{
					opt.Ignore();
				});
			}
			foreach (TypePair typePair in this.TypeMap.IncludedDerivedTypes)
			{
				mappingExpression.Include(typePair.DestinationType, typePair.SourceType);
			}
			return mappingExpression;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001375C File Offset: 0x0001195C
		public IMappingExpression<TSource, TDestination> ForSourceMember(Expression<Func<TSource, object>> sourceMember, Action<ISourceMemberConfigurationExpression> memberOptions)
		{
			MemberInfo sourceMember2 = ReflectionHelper.FindProperty(sourceMember);
			SourceMappingExpression obj = new SourceMappingExpression(this.TypeMap, sourceMember2);
			memberOptions(obj);
			return this;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013788 File Offset: 0x00011988
		public IMappingExpression<TSource, TDestination> ForSourceMember(string sourceMemberName, Action<ISourceMemberConfigurationExpression> memberOptions)
		{
			MemberInfo sourceMember = this.TypeMap.SourceType.GetMember(sourceMemberName).First<MemberInfo>();
			SourceMappingExpression obj = new SourceMappingExpression(this.TypeMap, sourceMember);
			memberOptions(obj);
			return this;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000137C4 File Offset: 0x000119C4
		public IMappingExpression<TSource, TDestination> Substitute(Func<TSource, object> substituteFunc)
		{
			this.TypeMap.Substitution = ((object src) => substituteFunc((TSource)((object)src)));
			return this;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000137F6 File Offset: 0x000119F6
		public void Ignore()
		{
			this.PropertyMap.Ignore();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013803 File Offset: 0x00011A03
		public void UseDestinationValue()
		{
			this.PropertyMap.UseDestinationValue = true;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013811 File Offset: 0x00011A11
		public void DoNotUseDestinationValue()
		{
			this.PropertyMap.UseDestinationValue = false;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001381F File Offset: 0x00011A1F
		public void SetMappingOrder(int mappingOrder)
		{
			this.PropertyMap.SetMappingOrder(mappingOrder);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013830 File Offset: 0x00011A30
		public void ConvertUsing(Func<TSource, TDestination> mappingFunction)
		{
			this.TypeMap.UseCustomMapper((ResolutionContext source) => mappingFunction((TSource)((object)source.SourceValue)));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013864 File Offset: 0x00011A64
		public void ConvertUsing(Func<ResolutionContext, TDestination> mappingFunction)
		{
			this.TypeMap.UseCustomMapper((ResolutionContext context) => mappingFunction(context));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00013898 File Offset: 0x00011A98
		public void ConvertUsing(Func<ResolutionContext, TSource, TDestination> mappingFunction)
		{
			this.TypeMap.UseCustomMapper((ResolutionContext source) => mappingFunction(source, (TSource)((object)source.SourceValue)));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000138C9 File Offset: 0x00011AC9
		public void ConvertUsing(ITypeConverter<TSource, TDestination> converter)
		{
			this.ConvertUsing(new Func<ResolutionContext, TDestination>(converter.Convert));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000138E0 File Offset: 0x00011AE0
		public void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>
		{
			DeferredInstantiatedConverter<TSource, TDestination> @object = new DeferredInstantiatedConverter<TSource, TDestination>(this.BuildCtor<ITypeConverter<TSource, TDestination>>(typeof(TTypeConverter)));
			this.ConvertUsing(new Func<ResolutionContext, TDestination>(@object.Convert));
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00013918 File Offset: 0x00011B18
		public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeFunction)
		{
			this.TypeMap.AddBeforeMapAction(delegate(object src, object dest)
			{
				beforeFunction((TSource)((object)src), (TDestination)((object)dest));
			});
			return this;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001394C File Offset: 0x00011B4C
		public IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
		{
			Action<TSource, TDestination> beforeFunction = delegate(TSource src, TDestination dest)
			{
				TMappingAction tmappingAction = (TMappingAction)((object)this._serviceCtor(typeof(TMappingAction)));
				tmappingAction.Process(src, dest);
			};
			return this.BeforeMap(beforeFunction);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013970 File Offset: 0x00011B70
		public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterFunction)
		{
			this.TypeMap.AddAfterMapAction(delegate(object src, object dest)
			{
				afterFunction((TSource)((object)src), (TDestination)((object)dest));
			});
			return this;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000139A4 File Offset: 0x00011BA4
		public IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
		{
			Action<TSource, TDestination> afterFunction = delegate(TSource src, TDestination dest)
			{
				TMappingAction tmappingAction = (TMappingAction)((object)this._serviceCtor(typeof(TMappingAction)));
				tmappingAction.Process(src, dest);
			};
			return this.AfterMap(afterFunction);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000139C8 File Offset: 0x00011BC8
		public IMappingExpression<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> ctor)
		{
			return this.ConstructUsing((ResolutionContext ctxt) => ctor((TSource)((object)ctxt.SourceValue)));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000139F4 File Offset: 0x00011BF4
		public IMappingExpression<TSource, TDestination> ConstructUsing(Func<ResolutionContext, TDestination> ctor)
		{
			this.TypeMap.DestinationCtor = ((ResolutionContext ctxt) => ctor(ctxt));
			return this;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00013A28 File Offset: 0x00011C28
		public IMappingExpression<TSource, TDestination> ConstructProjectionUsing(Expression<Func<TSource, TDestination>> ctor)
		{
			Func<TSource, TDestination> func = ctor.Compile();
			this.TypeMap.ConstructExpression = ctor;
			return this.ConstructUsing((ResolutionContext ctxt) => func((TSource)((object)ctxt.SourceValue)));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013A65 File Offset: 0x00011C65
		private void ForDestinationMember(IMemberAccessor destinationProperty, Action<IMemberConfigurationExpression<TSource>> memberOptions)
		{
			this.PropertyMap = this.TypeMap.FindOrCreatePropertyMapFor(destinationProperty);
			memberOptions(this);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00013A80 File Offset: 0x00011C80
		public void As<T>()
		{
			this.TypeMap.DestinationTypeOverride = typeof(T);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00013A98 File Offset: 0x00011C98
		public IMappingExpression<TSource, TDestination> ForCtorParam(string ctorParamName, Action<ICtorParamConfigurationExpression<TSource>> paramOptions)
		{
			ConstructorParameterMap constructorParameterMap = this.TypeMap.ConstructorMap.CtorParams.Single((ConstructorParameterMap p) => p.Parameter.Name == ctorParamName);
			CtorParamConfigurationExpression<TSource> obj = new CtorParamConfigurationExpression<TSource>(constructorParameterMap);
			constructorParameterMap.CanResolve = true;
			paramOptions(obj);
			return this;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00013AE8 File Offset: 0x00011CE8
		protected Func<ResolutionContext, TServiceType> BuildCtor<TServiceType>(Type type)
		{
			return delegate(ResolutionContext context)
			{
				if (type.IsGenericTypeDefinition())
				{
					type = type.MakeGenericType(context.SourceType.GetTypeInfo().GenericTypeArguments);
				}
				Func<Type, object> serviceCtor = context.Options.ServiceCtor;
				object obj = (serviceCtor != null) ? serviceCtor(type) : null;
				if (obj != null)
				{
					return (TServiceType)((object)obj);
				}
				return (TServiceType)((object)this._serviceCtor(type));
			};
		}

		// Token: 0x040000E2 RID: 226
		private readonly Func<Type, object> _serviceCtor;
	}
}
