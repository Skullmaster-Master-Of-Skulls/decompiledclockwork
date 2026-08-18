using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;
using AutoMapper.Mappers;
using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions.Impl;

namespace AutoMapper
{
	// Token: 0x0200002F RID: 47
	public class MapperConfiguration : IConfigurationProvider, IMapperConfiguration, IProfileExpression, IConfiguration
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000367D File Offset: 0x0000187D
		public MapperConfiguration(Action<IMapperConfiguration> configure) : this(configure, MapperRegistry.Mappers, TypeMapObjectMapperRegistry.Mappers)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003690 File Offset: 0x00001890
		public MapperConfiguration(Action<IMapperConfiguration> configure, IEnumerable<IObjectMapper> mappers, IEnumerable<ITypeMapObjectMapper> typeMapObjectMappers)
		{
			this._typeMapFactory = new TypeMapFactory();
			this._mappers = mappers;
			this._typeMapObjectMappers = typeMapObjectMappers;
			this._defaultProfile = this.CreateProfile(this.ProfileName);
			configure(this);
			this.Seal();
			this.ExpressionBuilder = new ExpressionBuilder(this);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003725 File Offset: 0x00001925
		public string ProfileName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000372C File Offset: 0x0000192C
		void IConfiguration.ForAllMaps(string profileName, Action<TypeMap, IMappingExpression> configuration)
		{
			IEnumerable<TypeMap> source = from kv in this._userDefinedTypeMaps
			select kv.Value;
			Func<TypeMap, bool> <>9__1;
			Func<TypeMap, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((TypeMap tm) => tm.Profile == profileName));
			}
			foreach (TypeMap typeMap in source.Where(predicate))
			{
				configuration(typeMap, this.CreateMappingExpression(typeMap));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000037D8 File Offset: 0x000019D8
		IProfileExpression IConfiguration.CreateProfile(string profileName)
		{
			return this.CreateProfile(profileName);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000037E4 File Offset: 0x000019E4
		void IConfiguration.CreateProfile(string profileName, Action<IProfileExpression> profileConfiguration)
		{
			Profile obj = this.CreateProfile(profileName);
			profileConfiguration(obj);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003800 File Offset: 0x00001A00
		void IConfiguration.AddProfile(Profile profile)
		{
			this._profiles.AddOrUpdate(profile.ProfileName, profile, (string s, Profile configuration) => profile);
			profile.Initialize(this);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000384F File Offset: 0x00001A4F
		void IConfiguration.AddProfile<TProfile>()
		{
			((IConfiguration)this).AddProfile(Activator.CreateInstance<TProfile>());
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003861 File Offset: 0x00001A61
		void IConfiguration.ConstructServicesUsing(Func<Type, object> constructor)
		{
			this._serviceCtor = constructor;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000386A File Offset: 0x00001A6A
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003877 File Offset: 0x00001A77
		Func<PropertyInfo, bool> IProfileExpression.ShouldMapProperty
		{
			get
			{
				return this._defaultProfile.ShouldMapProperty;
			}
			set
			{
				this._defaultProfile.ShouldMapProperty = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003885 File Offset: 0x00001A85
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00003892 File Offset: 0x00001A92
		Func<FieldInfo, bool> IProfileExpression.ShouldMapField
		{
			get
			{
				return this._defaultProfile.ShouldMapField;
			}
			set
			{
				this._defaultProfile.ShouldMapField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000038A0 File Offset: 0x00001AA0
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000038AD File Offset: 0x00001AAD
		bool IProfileExpression.CreateMissingTypeMaps
		{
			get
			{
				return this._defaultProfile.CreateMissingTypeMaps;
			}
			set
			{
				this._defaultProfile.CreateMissingTypeMaps = value;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000038BB File Offset: 0x00001ABB
		void IProfileExpression.IncludeSourceExtensionMethods(Assembly assembly)
		{
			this._defaultProfile.IncludeSourceExtensionMethods(assembly);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000038C9 File Offset: 0x00001AC9
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000038D6 File Offset: 0x00001AD6
		INamingConvention IProfileExpression.SourceMemberNamingConvention
		{
			get
			{
				return this._defaultProfile.SourceMemberNamingConvention;
			}
			set
			{
				this._defaultProfile.SourceMemberNamingConvention = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000038F1 File Offset: 0x00001AF1
		INamingConvention IProfileExpression.DestinationMemberNamingConvention
		{
			get
			{
				return this._defaultProfile.DestinationMemberNamingConvention;
			}
			set
			{
				this._defaultProfile.DestinationMemberNamingConvention = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000038FF File Offset: 0x00001AFF
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00003907 File Offset: 0x00001B07
		bool IProfileExpression.AllowNullDestinationValues
		{
			get
			{
				return this.AllowNullDestinationValues;
			}
			set
			{
				this.AllowNullDestinationValues = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003910 File Offset: 0x00001B10
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00003918 File Offset: 0x00001B18
		bool IProfileExpression.AllowNullCollections
		{
			get
			{
				return this.AllowNullCollections;
			}
			set
			{
				this.AllowNullCollections = value;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003921 File Offset: 0x00001B21
		void IProfileExpression.ForAllMaps(Action<TypeMap, IMappingExpression> configuration)
		{
			this._defaultProfile.ForAllMaps(configuration);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000392F File Offset: 0x00001B2F
		IMemberConfiguration IProfileExpression.AddMemberConfiguration()
		{
			return this._defaultProfile.AddMemberConfiguration();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000393C File Offset: 0x00001B3C
		IConditionalObjectMapper IProfileExpression.AddConditionalObjectMapper()
		{
			return this._defaultProfile.AddConditionalObjectMapper();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003949 File Offset: 0x00001B49
		void IProfileExpression.DisableConstructorMapping()
		{
			this._defaultProfile.DisableConstructorMapping();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003956 File Offset: 0x00001B56
		IMappingExpression<TSource, TDestination> IProfileExpression.CreateMap<TSource, TDestination>()
		{
			return this._defaultProfile.CreateMap<TSource, TDestination>();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003963 File Offset: 0x00001B63
		IMappingExpression<TSource, TDestination> IProfileExpression.CreateMap<TSource, TDestination>(MemberList memberList)
		{
			return this._defaultProfile.CreateMap<TSource, TDestination>(memberList);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003971 File Offset: 0x00001B71
		IMappingExpression<TSource, TDestination> IConfiguration.CreateMap<TSource, TDestination>(string profileName)
		{
			return ((IConfiguration)this).CreateMap<TSource, TDestination>(profileName, MemberList.Destination);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000397C File Offset: 0x00001B7C
		IMappingExpression<TSource, TDestination> IConfiguration.CreateMap<TSource, TDestination>(string profileName, MemberList memberList)
		{
			TypeMap typeMap = this.CreateTypeMap(new TypePair(typeof(TSource), typeof(TDestination)), profileName, memberList);
			return this.CreateMappingExpression<TSource, TDestination>(typeMap);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000039B2 File Offset: 0x00001BB2
		IMappingExpression IProfileExpression.CreateMap(Type sourceType, Type destinationType)
		{
			return this._defaultProfile.CreateMap(sourceType, destinationType, MemberList.Destination);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000039C2 File Offset: 0x00001BC2
		IMappingExpression IProfileExpression.CreateMap(Type sourceType, Type destinationType, MemberList memberList)
		{
			return this._defaultProfile.CreateMap(sourceType, destinationType, memberList);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000039D4 File Offset: 0x00001BD4
		IMappingExpression IConfiguration.CreateMap(Type sourceType, Type destinationType, MemberList memberList, string profileName)
		{
			TypePair typePair = new TypePair(sourceType, destinationType);
			if (sourceType.IsGenericTypeDefinition() && destinationType.IsGenericTypeDefinition())
			{
				return this._typeMapExpressionCache.GetOrAdd(typePair, (TypePair tp) => new CreateTypeMapExpression(tp, memberList, profileName));
			}
			TypeMap typeMap = this.CreateTypeMap(typePair, profileName, memberList);
			return this.CreateMappingExpression(typeMap);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003A42 File Offset: 0x00001C42
		void IProfileExpression.ClearPrefixes()
		{
			this._defaultProfile.ClearPrefixes();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003A4F File Offset: 0x00001C4F
		void IProfileExpression.RecognizeAlias(string original, string alias)
		{
			this._defaultProfile.RecognizeAlias(original, alias);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003A5E File Offset: 0x00001C5E
		void IProfileExpression.ReplaceMemberName(string original, string newValue)
		{
			this._defaultProfile.ReplaceMemberName(original, newValue);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003A6D File Offset: 0x00001C6D
		void IProfileExpression.RecognizePrefixes(params string[] prefixes)
		{
			this._defaultProfile.RecognizePrefixes(prefixes);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003A7B File Offset: 0x00001C7B
		void IProfileExpression.RecognizePostfixes(params string[] postfixes)
		{
			this._defaultProfile.RecognizePostfixes(postfixes);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003A89 File Offset: 0x00001C89
		void IProfileExpression.RecognizeDestinationPrefixes(params string[] prefixes)
		{
			this._defaultProfile.RecognizeDestinationPrefixes(prefixes);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003A97 File Offset: 0x00001C97
		void IProfileExpression.RecognizeDestinationPostfixes(params string[] postfixes)
		{
			this._defaultProfile.RecognizeDestinationPostfixes(postfixes);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003AA5 File Offset: 0x00001CA5
		void IProfileExpression.AddGlobalIgnore(string startingwith)
		{
			this._defaultProfile.AddGlobalIgnore(startingwith);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00003AB3 File Offset: 0x00001CB3
		public IExpressionBuilder ExpressionBuilder { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003ABB File Offset: 0x00001CBB
		public Func<Type, object> ServiceCtor
		{
			get
			{
				return this._serviceCtor;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003AC3 File Offset: 0x00001CC3
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public bool AllowNullDestinationValues
		{
			get
			{
				return this._defaultProfile.AllowNullDestinationValues;
			}
			private set
			{
				this._defaultProfile.AllowNullDestinationValues = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003ADE File Offset: 0x00001CDE
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00003AEB File Offset: 0x00001CEB
		public bool AllowNullCollections
		{
			get
			{
				return this._defaultProfile.AllowNullCollections;
			}
			private set
			{
				this._defaultProfile.AllowNullCollections = value;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003AF9 File Offset: 0x00001CF9
		public TypeMap[] GetAllTypeMaps()
		{
			return (from kv in this._userDefinedTypeMaps
			select kv.Value).ToArray<TypeMap>();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003B2C File Offset: 0x00001D2C
		public TypeMap FindTypeMapFor(Type sourceType, Type destinationType)
		{
			TypePair typePair = new TypePair(sourceType, destinationType);
			return this.FindTypeMapFor(typePair);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003B48 File Offset: 0x00001D48
		public TypeMap FindTypeMapFor<TSource, TDestination>()
		{
			TypePair typePair = new TypePair(typeof(TSource), typeof(TDestination));
			return this.FindTypeMapFor(typePair);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003B78 File Offset: 0x00001D78
		public TypeMap FindTypeMapFor(TypePair typePair)
		{
			TypeMap result;
			this._userDefinedTypeMaps.TryGetValue(typePair, out result);
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003B98 File Offset: 0x00001D98
		public TypeMap ResolveTypeMap(Type sourceType, Type destinationType)
		{
			TypePair typePair = new TypePair(sourceType, destinationType);
			return this.ResolveTypeMap(typePair);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public TypeMap ResolveTypeMap(TypePair typePair)
		{
			Func<TypePair, TypeMap> <>9__1;
			return this._typeMapPlanCache.GetOrAdd(typePair, delegate(TypePair _)
			{
				IEnumerable<TypePair> relatedTypePairs = this.GetRelatedTypePairs(_);
				Func<TypePair, TypeMap> selector;
				if ((selector = <>9__1) == null)
				{
					selector = (<>9__1 = delegate(TypePair tp)
					{
						TypeMap result;
						if ((result = this._typeMapPlanCache.GetOrDefault(tp)) == null && (result = this.FindTypeMapFor(tp)) == null)
						{
							if (this.CoveredByObjectMap(typePair))
							{
								return null;
							}
							result = (this.FindConventionTypeMapFor(tp) ?? this.FindClosedGenericTypeMapFor(tp));
						}
						return result;
					});
				}
				return relatedTypePairs.Select(selector).FirstOrDefault((TypeMap tm) => tm != null);
			});
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public TypeMap ResolveTypeMap(object source, object destination, Type sourceType, Type destinationType)
		{
			return this.ResolveTypeMap(((source != null) ? source.GetType() : null) ?? sourceType, ((destination != null) ? destination.GetType() : null) ?? destinationType);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003C1D File Offset: 0x00001E1D
		public TypeMap ResolveTypeMap(ResolutionResult resolutionResult, Type destinationType)
		{
			return this.ResolveTypeMap(resolutionResult.Type, destinationType) ?? this.ResolveTypeMap(resolutionResult.MemberType, destinationType);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003C3D File Offset: 0x00001E3D
		public IProfileConfiguration GetProfileConfiguration(string profileName)
		{
			return this.GetProfile(profileName);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003C46 File Offset: 0x00001E46
		public void AssertConfigurationIsValid(TypeMap typeMap)
		{
			this.AssertConfigurationIsValid(Enumerable.Repeat<TypeMap>(typeMap, 1));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00003C58 File Offset: 0x00001E58
		public void AssertConfigurationIsValid(string profileName)
		{
			this.AssertConfigurationIsValid(from kv in this._userDefinedTypeMaps
			select kv.Value into typeMap
			where typeMap.Profile == profileName
			select typeMap);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public void AssertConfigurationIsValid<TProfile>() where TProfile : Profile, new()
		{
			this.AssertConfigurationIsValid(Activator.CreateInstance<TProfile>().ProfileName);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00003CCA File Offset: 0x00001ECA
		public void AssertConfigurationIsValid()
		{
			this.AssertConfigurationIsValid(from kv in this._userDefinedTypeMaps
			select kv.Value);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00003CFC File Offset: 0x00001EFC
		public IEnumerable<IObjectMapper> GetMappers()
		{
			return this._mappers;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00003D04 File Offset: 0x00001F04
		public IEnumerable<ITypeMapObjectMapper> GetTypeMapMappers()
		{
			return this._typeMapObjectMappers;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00003D0C File Offset: 0x00001F0C
		internal void Seal()
		{
			List<Tuple<TypePair, TypeMap>> list = new List<Tuple<TypePair, TypeMap>>();
			List<Tuple<TypePair, TypePair>> list2 = new List<Tuple<TypePair, TypePair>>();
			using (IEnumerator<TypeMap> enumerator = (from kv in this._userDefinedTypeMaps
			select kv.Value).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TypeMap typeMap = enumerator.Current;
					typeMap.Seal();
					this._typeMapPlanCache.AddOrUpdate(typeMap.Types, typeMap, (TypePair _, TypeMap _2) => typeMap);
					if (typeMap.DestinationTypeOverride != null)
					{
						list2.Add(Tuple.Create<TypePair, TypePair>(typeMap.Types, new TypePair(typeMap.SourceType, typeMap.DestinationTypeOverride)));
					}
					list.AddRange(from derivedMap in this.GetDerivedTypeMaps(typeMap)
					select Tuple.Create<TypePair, TypeMap>(new TypePair(derivedMap.SourceType, typeMap.DestinationType), derivedMap));
				}
			}
			foreach (Tuple<TypePair, TypePair> tuple in list2)
			{
				TypeMap derivedMap = this.FindTypeMapFor(tuple.Item2);
				if (derivedMap != null)
				{
					this._typeMapPlanCache.AddOrUpdate(tuple.Item1, derivedMap, (TypePair _, TypeMap _2) => derivedMap);
				}
			}
			using (List<Tuple<TypePair, TypeMap>>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Tuple<TypePair, TypeMap> derivedMap = enumerator3.Current;
					this._typeMapPlanCache.GetOrAdd(derivedMap.Item1, (TypePair _) => derivedMap.Item2);
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00003F1C File Offset: 0x0000211C
		public IMapper CreateMapper()
		{
			return new Mapper(this);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00003F24 File Offset: 0x00002124
		public IMapper CreateMapper(Func<Type, object> serviceCtor)
		{
			return new Mapper(this, serviceCtor);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00003F2D File Offset: 0x0000212D
		private IEnumerable<TypeMap> GetDerivedTypeMaps(TypeMap typeMap)
		{
			foreach (TypeMap derivedMap in typeMap.IncludedDerivedTypes.Select(new Func<TypePair, TypeMap>(this.FindTypeMapFor)))
			{
				if (derivedMap == null)
				{
					throw QueryMapperHelper.MissingMapException(typeMap.SourceType, typeMap.DestinationType);
				}
				yield return derivedMap;
				foreach (TypeMap typeMap2 in this.GetDerivedTypeMaps(derivedMap))
				{
					yield return typeMap2;
				}
				IEnumerator<TypeMap> enumerator2 = null;
				derivedMap = null;
			}
			IEnumerator<TypeMap> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00003F44 File Offset: 0x00002144
		private Profile CreateProfile(string profileName)
		{
			MapperConfiguration.NamedProfile profileExpression = new MapperConfiguration.NamedProfile(profileName);
			profileExpression.Initialize(this);
			this._profiles.AddOrUpdate(profileExpression.ProfileName, profileExpression, (string s, Profile configuration) => profileExpression);
			return profileExpression;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00003F9E File Offset: 0x0000219E
		private TypeMap CreateTypeMap(TypePair types, string profileName)
		{
			return this.CreateTypeMap(types, profileName, MemberList.Destination);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00003FAC File Offset: 0x000021AC
		private TypeMap CreateTypeMap(TypePair types, string profileName, MemberList memberList)
		{
			return this._userDefinedTypeMaps.GetOrAdd(types, delegate(TypePair tp)
			{
				Profile profile = this.GetProfile(profileName);
				TypeMap typeMap = this._typeMapFactory.CreateTypeMap(types.SourceType, types.DestinationType, profile, memberList);
				typeMap.Profile = profileName;
				typeMap.IgnorePropertiesStartingWith = profile.GlobalIgnores;
				this.IncludeBaseMappings(types, typeMap);
				TypeMap typeMap2;
				this._typeMapPlanCache.TryRemove(tp, out typeMap2);
				return typeMap;
			});
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00003FF8 File Offset: 0x000021F8
		private void IncludeBaseMappings(TypePair types, TypeMap typeMap)
		{
			IEnumerable<TypeMap> source = from kv in this._userDefinedTypeMaps
			select kv.Value;
			Func<TypeMap, bool> <>9__1;
			Func<TypeMap, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((TypeMap t) => t.TypeHasBeenIncluded(types)));
			}
			foreach (TypeMap typeMap2 in source.Where(predicate))
			{
				typeMap.ApplyInheritedMap(typeMap2);
				this.IncludeBaseMappings(typeMap2.Types, typeMap);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000040A8 File Offset: 0x000022A8
		private bool CoveredByObjectMap(TypePair typePair)
		{
			return this.GetMappers().FirstOrDefault((IObjectMapper m) => m.IsMatch(typePair)) != null;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000040DC File Offset: 0x000022DC
		private TypeMap FindConventionTypeMapFor(TypePair typePair)
		{
			IConditionalObjectMapper conditionalObjectMapper = (from kv in this._profiles
			select kv.Value).SelectMany((Profile p) => p.TypeConfigurations).FirstOrDefault((IConditionalObjectMapper tc) => tc.IsMatch(typePair));
			if (conditionalObjectMapper == null)
			{
				return null;
			}
			return this.CreateTypeMap(typePair, conditionalObjectMapper.ProfileName);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00004170 File Offset: 0x00002370
		private TypeMap FindClosedGenericTypeMapFor(TypePair typePair)
		{
			if (!this.HasOpenGenericTypeMapDefined(typePair))
			{
				return null;
			}
			TypePair types = new TypePair(typePair.SourceType, typePair.DestinationType);
			Type genericTypeDefinition = typePair.SourceType.GetGenericTypeDefinition();
			Type genericTypeDefinition2 = typePair.DestinationType.GetGenericTypeDefinition();
			TypePair key = new TypePair(genericTypeDefinition, genericTypeDefinition2);
			CreateTypeMapExpression createTypeMapExpression;
			if (!this._typeMapExpressionCache.TryGetValue(key, out createTypeMapExpression))
			{
				throw new AutoMapperMappingException("Missing type map configuration or unsupported mapping.");
			}
			TypeMap typeMap = this.CreateTypeMap(types, createTypeMapExpression.ProfileName, createTypeMapExpression.MemberList);
			IMappingExpression mappingExpression = this.CreateMappingExpression(typeMap);
			createTypeMapExpression.Accept(mappingExpression);
			return typeMap;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000041FC File Offset: 0x000023FC
		private bool HasOpenGenericTypeMapDefined(TypePair typePair)
		{
			if (!typePair.SourceType.IsGenericType() || !typePair.DestinationType.IsGenericType() || !(typePair.SourceType.GetGenericTypeDefinition() != null) || !(typePair.DestinationType.GetGenericTypeDefinition() != null))
			{
				return false;
			}
			Type genericTypeDefinition = typePair.SourceType.GetGenericTypeDefinition();
			Type genericTypeDefinition2 = typePair.DestinationType.GetGenericTypeDefinition();
			TypePair key = new TypePair(genericTypeDefinition, genericTypeDefinition2);
			return this._typeMapExpressionCache.ContainsKey(key);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004278 File Offset: 0x00002478
		private IEnumerable<TypePair> GetRelatedTypePairs(TypePair root)
		{
			return from destinationType in this.GetAllTypes(root.DestinationType)
			from sourceType in this.GetAllTypes(root.SourceType)
			select new TypePair(sourceType, destinationType);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000042DA File Offset: 0x000024DA
		private IEnumerable<Type> GetAllTypes(Type type)
		{
			yield return type;
			Type baseType = type.BaseType();
			while (baseType != null)
			{
				yield return baseType;
				baseType = baseType.BaseType();
			}
			foreach (Type type2 in type.GetTypeInfo().ImplementedInterfaces)
			{
				yield return type2;
			}
			IEnumerator<Type> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000042EC File Offset: 0x000024EC
		private IMappingExpression<TSource, TDestination> CreateMappingExpression<TSource, TDestination>(TypeMap typeMap)
		{
			Profile profile = this.GetProfile(typeMap.Profile);
			MappingExpression<TSource, TDestination> mappingExp = new MappingExpression<TSource, TDestination>(typeMap, this._serviceCtor, profile);
			Type destinationType = (typeMap.ConfiguredMemberList == MemberList.Destination) ? typeof(TDestination) : typeof(TSource);
			return this.Ignore<TSource, TDestination>(mappingExp, destinationType);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000433C File Offset: 0x0000253C
		private IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(IMappingExpression<TSource, TDestination> mappingExp, Type destinationType)
		{
			foreach (MemberInfo memberInfo in new TypeDetails(destinationType, ((IProfileExpression)this).ShouldMapProperty, ((IProfileExpression)this).ShouldMapField).PublicWriteAccessors)
			{
				if (memberInfo.GetCustomAttributes(true).Any((object x) => x is IgnoreMapAttribute))
				{
					mappingExp = mappingExp.ForMember(memberInfo.Name, delegate(IMemberConfigurationExpression<TSource> y)
					{
						y.Ignore();
					});
				}
				if (this._defaultProfile.GlobalIgnores.Contains(memberInfo.Name))
				{
					mappingExp = mappingExp.ForMember(memberInfo.Name, delegate(IMemberConfigurationExpression<TSource> y)
					{
						y.Ignore();
					});
				}
			}
			return mappingExp;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000443C File Offset: 0x0000263C
		private IMappingExpression CreateMappingExpression(TypeMap typeMap)
		{
			Profile profile = this.GetProfile(typeMap.Profile);
			MappingExpression mappingExp = new MappingExpression(typeMap, this._serviceCtor, profile);
			return (IMappingExpression)this.Ignore<object, object>(mappingExp, typeMap.DestinationType);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004478 File Offset: 0x00002678
		private void AssertConfigurationIsValid(IEnumerable<TypeMap> typeMaps)
		{
			this.Seal();
			TypeMap[] array = (typeMaps as TypeMap[]) ?? typeMaps.ToArray<TypeMap>();
			AutoMapperConfigurationException.TypeMapConfigErrors[] array2 = (from typeMap in array
			where typeMap.ShouldCheckForValid()
			let unmappedPropertyNames = typeMap.GetUnmappedPropertyNames()
			where unmappedPropertyNames.Length != 0
			select new AutoMapperConfigurationException.TypeMapConfigErrors(typeMap, unmappedPropertyNames)).ToArray<AutoMapperConfigurationException.TypeMapConfigErrors>();
			if (array2.Any<AutoMapperConfigurationException.TypeMapConfigErrors>())
			{
				throw new AutoMapperConfigurationException(array2);
			}
			List<TypeMap> typeMapsChecked = new List<TypeMap>();
			List<Exception> list = new List<Exception>();
			MappingEngine engine = new MappingEngine(this, this.CreateMapper());
			foreach (TypeMap typeMap2 in array)
			{
				try
				{
					this.DryRunTypeMap(typeMapsChecked, new ResolutionContext(typeMap2, null, typeMap2.SourceType, typeMap2.DestinationType, new MappingOperationOptions(), engine));
				}
				catch (Exception item)
				{
					list.Add(item);
				}
			}
			if (list.Count > 1)
			{
				throw new AggregateException(list);
			}
			if (list.Count > 0)
			{
				throw list[0];
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000045D8 File Offset: 0x000027D8
		private void DryRunTypeMap(ICollection<TypeMap> typeMapsChecked, ResolutionContext context)
		{
			TypeMap typeMap = context.TypeMap;
			if (typeMap != null)
			{
				typeMapsChecked.Add(typeMap);
				this.CheckPropertyMaps(typeMapsChecked, context);
				return;
			}
			IObjectMapper objectMapper = this.GetMappers().FirstOrDefault((IObjectMapper mapper) => mapper.IsMatch(context.Types));
			if (objectMapper == null && context.SourceType.IsNullableType())
			{
				TypePair nullableTypes = new TypePair(Nullable.GetUnderlyingType(context.SourceType), context.DestinationType);
				objectMapper = this.GetMappers().FirstOrDefault((IObjectMapper mapper) => mapper.IsMatch(nullableTypes));
			}
			if (objectMapper == null)
			{
				throw new AutoMapperConfigurationException(context);
			}
			if (objectMapper is ArrayMapper || objectMapper is EnumerableMapper || objectMapper is CollectionMapper)
			{
				this.CheckElementMaps(typeMapsChecked, context);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000046BC File Offset: 0x000028BC
		private void CheckElementMaps(ICollection<TypeMap> typeMapsChecked, ResolutionContext context)
		{
			Type elementType = TypeHelper.GetElementType(context.SourceType);
			Type elementType2 = TypeHelper.GetElementType(context.DestinationType);
			TypeMap itemTypeMap = ((IConfigurationProvider)this).ResolveTypeMap(elementType, elementType2);
			if (typeMapsChecked.Any((TypeMap typeMap) => object.Equals(typeMap, itemTypeMap)))
			{
				return;
			}
			ResolutionContext context2 = context.CreateElementContext(itemTypeMap, null, elementType, elementType2, 0);
			this.DryRunTypeMap(typeMapsChecked, context2);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004724 File Offset: 0x00002924
		private void CheckPropertyMaps(ICollection<TypeMap> typeMapsChecked, ResolutionContext context)
		{
			foreach (PropertyMap propertyMap in context.TypeMap.GetPropertyMaps())
			{
				if (!propertyMap.IsIgnored())
				{
					IMemberResolver memberResolver = propertyMap.GetSourceValueResolvers().OfType<IMemberResolver>().LastOrDefault<IMemberResolver>();
					if (memberResolver != null)
					{
						Type memberType = memberResolver.MemberType;
						Type memberType2 = propertyMap.DestinationProperty.MemberType;
						TypeMap memberTypeMap = ((IConfigurationProvider)this).ResolveTypeMap(memberType, memberType2);
						if (!typeMapsChecked.Any((TypeMap typeMap) => object.Equals(typeMap, memberTypeMap)))
						{
							ResolutionContext context2 = context.CreateMemberContext(memberTypeMap, null, null, memberType, propertyMap);
							this.DryRunTypeMap(typeMapsChecked, context2);
						}
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000047EC File Offset: 0x000029EC
		private Profile GetProfile(string profileName)
		{
			return this._profiles.GetOrAdd(profileName, (string name) => new MapperConfiguration.NamedProfile(profileName));
		}

		// Token: 0x04000023 RID: 35
		private readonly ITypeMapFactory _typeMapFactory;

		// Token: 0x04000024 RID: 36
		private readonly IEnumerable<IObjectMapper> _mappers;

		// Token: 0x04000025 RID: 37
		private readonly IEnumerable<ITypeMapObjectMapper> _typeMapObjectMappers;

		// Token: 0x04000026 RID: 38
		private readonly Profile _defaultProfile;

		// Token: 0x04000027 RID: 39
		private readonly ConcurrentDictionary<TypePair, TypeMap> _userDefinedTypeMaps = new ConcurrentDictionary<TypePair, TypeMap>();

		// Token: 0x04000028 RID: 40
		private readonly ConcurrentDictionary<TypePair, TypeMap> _typeMapPlanCache = new ConcurrentDictionary<TypePair, TypeMap>();

		// Token: 0x04000029 RID: 41
		private readonly ConcurrentDictionary<TypePair, CreateTypeMapExpression> _typeMapExpressionCache = new ConcurrentDictionary<TypePair, CreateTypeMapExpression>();

		// Token: 0x0400002A RID: 42
		private readonly ConcurrentDictionary<string, Profile> _profiles = new ConcurrentDictionary<string, Profile>();

		// Token: 0x0400002B RID: 43
		private Func<Type, object> _serviceCtor = new Func<Type, object>(ObjectCreator.CreateObject);

		// Token: 0x020000CB RID: 203
		private class NamedProfile : Profile
		{
			// Token: 0x060005C5 RID: 1477 RVA: 0x00015481 File Offset: 0x00013681
			public NamedProfile(string profileName) : base(profileName)
			{
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x000098B0 File Offset: 0x00007AB0
			protected override void Configure()
			{
			}
		}
	}
}
