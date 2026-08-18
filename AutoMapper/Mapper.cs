using System;
using AutoMapper.Mappers;

namespace AutoMapper
{
	// Token: 0x0200002E RID: 46
	public class Mapper : IMapper, IDynamicMapper
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003018 File Offset: 0x00001218
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003024 File Offset: 0x00001224
		[Obsolete("The static API will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed. Use CreateMapper to create a mapper instance.")]
		public static bool AllowNullDestinationValues
		{
			get
			{
				return Mapper.Configuration.AllowNullDestinationValues;
			}
			set
			{
				Mapper.Configuration.AllowNullDestinationValues = value;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003031 File Offset: 0x00001231
		public static TDestination Map<TDestination>(object source)
		{
			return Mapper.Instance.Map<TDestination>(source);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000303E File Offset: 0x0000123E
		public static TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
		{
			return Mapper.Instance.Map<TDestination>(source, opts);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000304C File Offset: 0x0000124C
		public static TDestination Map<TSource, TDestination>(TSource source)
		{
			return Mapper.Instance.Map<TSource, TDestination>(source);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003059 File Offset: 0x00001259
		public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			return Mapper.Instance.Map<TSource, TDestination>(source, destination);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003067 File Offset: 0x00001267
		public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
		{
			return Mapper.Instance.Map<TSource, TDestination>(source, destination, opts);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003076 File Offset: 0x00001276
		public static TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
		{
			return Mapper.Instance.Map<TSource, TDestination>(source, opts);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003084 File Offset: 0x00001284
		public static object Map(object source, Type sourceType, Type destinationType)
		{
			return Mapper.Instance.Map(source, sourceType, destinationType);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003093 File Offset: 0x00001293
		public static object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
		{
			return Mapper.Instance.Map(source, sourceType, destinationType, opts);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000030A3 File Offset: 0x000012A3
		public static object Map(object source, object destination, Type sourceType, Type destinationType)
		{
			return Mapper.Instance.Map(source, destination, sourceType, destinationType);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000030B3 File Offset: 0x000012B3
		public static object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
		{
			return Mapper.Instance.Map(source, destination, sourceType, destinationType, opts);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000030C5 File Offset: 0x000012C5
		[Obsolete("Set the CreateMissingTypeMaps property on Mapper.ConfigurationProvider or your Profile instead")]
		public static TDestination DynamicMap<TSource, TDestination>(TSource source)
		{
			return Mapper.DynamicInstance.DynamicMap<TSource, TDestination>(source);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000030D2 File Offset: 0x000012D2
		[Obsolete("Set the CreateMissingTypeMaps property on Mapper.ConfigurationProvider or your Profile instead")]
		public static void DynamicMap<TSource, TDestination>(TSource source, TDestination destination)
		{
			Mapper.DynamicInstance.DynamicMap<TSource, TDestination>(source, destination);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000030E0 File Offset: 0x000012E0
		[Obsolete("Set the CreateMissingTypeMaps property on Mapper.ConfigurationProvider or your Profile instead")]
		public static TDestination DynamicMap<TDestination>(object source)
		{
			return Mapper.DynamicInstance.DynamicMap<TDestination>(source);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000030ED File Offset: 0x000012ED
		[Obsolete("Set the CreateMissingTypeMaps property on Mapper.ConfigurationProvider or your Profile instead")]
		public static object DynamicMap(object source, Type sourceType, Type destinationType)
		{
			return Mapper.DynamicInstance.DynamicMap(source, sourceType, destinationType);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000030FC File Offset: 0x000012FC
		[Obsolete("Set the CreateMissingTypeMaps property on Mapper.ConfigurationProvider or your Profile instead")]
		public static void DynamicMap(object source, object destination, Type sourceType, Type destinationType)
		{
			Mapper.DynamicInstance.DynamicMap(source, destination, sourceType, destinationType);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000310C File Offset: 0x0000130C
		public static void Initialize(Action<IMapperConfiguration> action)
		{
			Mapper.Reset();
			action(Mapper.Configuration);
			((MapperConfiguration)Mapper.Configuration).Seal();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000312D File Offset: 0x0000132D
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
		{
			return Mapper.Configuration.CreateMap<TSource, TDestination>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003139 File Offset: 0x00001339
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList memberList)
		{
			return Mapper.Configuration.CreateMap<TSource, TDestination>(memberList);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003146 File Offset: 0x00001346
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IMappingExpression CreateMap(Type sourceType, Type destinationType)
		{
			return Mapper.Configuration.CreateMap(sourceType, destinationType);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003154 File Offset: 0x00001354
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList memberList)
		{
			return Mapper.Configuration.CreateMap(sourceType, destinationType, memberList);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003163 File Offset: 0x00001363
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IProfileExpression CreateProfile(string profileName)
		{
			return Mapper.Configuration.CreateProfile(profileName);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003170 File Offset: 0x00001370
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static void CreateProfile(string profileName, Action<IProfileExpression> profileConfiguration)
		{
			Mapper.Configuration.CreateProfile(profileName, profileConfiguration);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000317E File Offset: 0x0000137E
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static void AddProfile(Profile profile)
		{
			Mapper.Configuration.AddProfile(profile);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000318B File Offset: 0x0000138B
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static void AddProfile<TProfile>() where TProfile : Profile, new()
		{
			Mapper.Configuration.AddProfile<TProfile>();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003197 File Offset: 0x00001397
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static TypeMap FindTypeMapFor(Type sourceType, Type destinationType)
		{
			return Mapper.ConfigurationProvider.FindTypeMapFor(sourceType, destinationType);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000031A5 File Offset: 0x000013A5
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static TypeMap FindTypeMapFor<TSource, TDestination>()
		{
			return Mapper.ConfigurationProvider.FindTypeMapFor(typeof(TSource), typeof(TDestination));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000031C5 File Offset: 0x000013C5
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static TypeMap[] GetAllTypeMaps()
		{
			return Mapper.ConfigurationProvider.GetAllTypeMaps();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000031D1 File Offset: 0x000013D1
		public static void AssertConfigurationIsValid()
		{
			Mapper.ConfigurationProvider.AssertConfigurationIsValid();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000031DD File Offset: 0x000013DD
		public static void AssertConfigurationIsValid(TypeMap typeMap)
		{
			Mapper.ConfigurationProvider.AssertConfigurationIsValid(typeMap);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000031EA File Offset: 0x000013EA
		public static void AssertConfigurationIsValid(string profileName)
		{
			Mapper.ConfigurationProvider.AssertConfigurationIsValid(profileName);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000031F7 File Offset: 0x000013F7
		public static void AssertConfigurationIsValid<TProfile>() where TProfile : Profile, new()
		{
			Mapper.ConfigurationProvider.AssertConfigurationIsValid<TProfile>();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003203 File Offset: 0x00001403
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static void Reset()
		{
			MapperRegistry.Reset();
			Mapper._configuration = new Lazy<MapperConfiguration>(Mapper._configurationInit);
			Mapper._mappingEngine = new Lazy<Mapper>(Mapper._mappingEngineInit);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003228 File Offset: 0x00001428
		public static IMappingEngine Engine
		{
			get
			{
				return Mapper._mappingEngine.Value._engine;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003239 File Offset: 0x00001439
		public static IMapper Instance
		{
			get
			{
				return Mapper._mappingEngine.Value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003245 File Offset: 0x00001445
		internal static IConfigurationProvider ConfigurationProvider
		{
			get
			{
				return Mapper._configuration.Value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003239 File Offset: 0x00001439
		private static IDynamicMapper DynamicInstance
		{
			get
			{
				return Mapper._mappingEngine.Value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003245 File Offset: 0x00001445
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static IMapperConfiguration Configuration
		{
			get
			{
				return Mapper._configuration.Value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003251 File Offset: 0x00001451
		[Obsolete("Dynamically creating maps will be removed in version 5.0. Use a MapperConfiguration instance and store statically as needed, or Mapper.Initialize. Use CreateMapper to create a mapper instance.")]
		public static void AddGlobalIgnore(string startingwith)
		{
			Mapper.Configuration.AddGlobalIgnore(startingwith);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000325E File Offset: 0x0000145E
		internal Mapper(IConfigurationProvider configurationProvider) : this(configurationProvider, configurationProvider.ServiceCtor)
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000326D File Offset: 0x0000146D
		internal Mapper(IConfigurationProvider configurationProvider, Func<Type, object> serviceCtor)
		{
			this._configurationProvider = configurationProvider;
			this._serviceCtor = serviceCtor;
			this._engine = new MappingEngine(configurationProvider, this);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003290 File Offset: 0x00001490
		TDestination IMapper.Map<TDestination>(object source)
		{
			return ((IMapper)this).Map<TDestination>(source, new Action<IMappingOperationOptions>(this.DefaultMappingOptions));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000032A8 File Offset: 0x000014A8
		TDestination IMapper.Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
		{
			TDestination result = default(TDestination);
			if (source != null)
			{
				Type type = source.GetType();
				Type typeFromHandle = typeof(TDestination);
				result = (TDestination)((object)((IMapper)this).Map(source, type, typeFromHandle, opts));
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000032E4 File Offset: 0x000014E4
		TDestination IMapper.Map<TSource, TDestination>(TSource source)
		{
			Type typeFromHandle = typeof(TSource);
			Type typeFromHandle2 = typeof(TDestination);
			return (TDestination)((object)((IMapper)this).Map(source, typeFromHandle, typeFromHandle2, new Action<IMappingOperationOptions>(this.DefaultMappingOptions)));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003328 File Offset: 0x00001528
		TDestination IMapper.Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
		{
			Type typeFromHandle = typeof(TSource);
			Type typeFromHandle2 = typeof(TDestination);
			MappingOperationOptions<TSource, TDestination> mappingOperationOptions = new MappingOperationOptions<TSource, TDestination>();
			opts(mappingOperationOptions);
			return (TDestination)((object)this.MapCore(source, typeFromHandle, typeFromHandle2, mappingOperationOptions));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000336C File Offset: 0x0000156C
		TDestination IMapper.Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			return ((IMapper)this).Map<TSource, TDestination>(source, destination, new Action<IMappingOperationOptions<TSource, TDestination>>(this.DefaultMappingOptions));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003384 File Offset: 0x00001584
		TDestination IMapper.Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
		{
			Type typeFromHandle = typeof(TSource);
			Type typeFromHandle2 = typeof(TDestination);
			MappingOperationOptions<TSource, TDestination> mappingOperationOptions = new MappingOperationOptions<TSource, TDestination>();
			opts(mappingOperationOptions);
			return (TDestination)((object)this.MapCore(source, destination, typeFromHandle, typeFromHandle2, mappingOperationOptions));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000033CE File Offset: 0x000015CE
		object IMapper.Map(object source, Type sourceType, Type destinationType)
		{
			return ((IMapper)this).Map(source, sourceType, destinationType, new Action<IMappingOperationOptions>(this.DefaultMappingOptions));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000033E8 File Offset: 0x000015E8
		object IMapper.Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
		{
			MappingOperationOptions mappingOperationOptions = new MappingOperationOptions();
			opts(mappingOperationOptions);
			return this.MapCore(source, sourceType, destinationType, mappingOperationOptions);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000340D File Offset: 0x0000160D
		object IMapper.Map(object source, object destination, Type sourceType, Type destinationType)
		{
			return ((IMapper)this).Map(source, destination, sourceType, destinationType, new Action<IMappingOperationOptions>(this.DefaultMappingOptions));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003428 File Offset: 0x00001628
		object IMapper.Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
		{
			MappingOperationOptions mappingOperationOptions = new MappingOperationOptions();
			opts(mappingOperationOptions);
			return this.MapCore(source, destination, sourceType, destinationType, mappingOperationOptions);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003450 File Offset: 0x00001650
		TDestination IDynamicMapper.DynamicMap<TSource, TDestination>(TSource source)
		{
			Type typeFromHandle = typeof(TSource);
			Type typeFromHandle2 = typeof(TDestination);
			return (TDestination)((object)((IDynamicMapper)this).DynamicMap(source, typeFromHandle, typeFromHandle2));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003488 File Offset: 0x00001688
		void IDynamicMapper.DynamicMap<TSource, TDestination>(TSource source, TDestination destination)
		{
			Type typeFromHandle = typeof(TSource);
			Type typeFromHandle2 = typeof(TDestination);
			((IDynamicMapper)this).DynamicMap(source, destination, typeFromHandle, typeFromHandle2);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000034C0 File Offset: 0x000016C0
		TDestination IDynamicMapper.DynamicMap<TDestination>(object source)
		{
			Type sourceType = ((source != null) ? source.GetType() : null) ?? typeof(object);
			Type typeFromHandle = typeof(TDestination);
			return (TDestination)((object)((IDynamicMapper)this).DynamicMap(source, sourceType, typeFromHandle));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003504 File Offset: 0x00001704
		object IDynamicMapper.DynamicMap(object source, Type sourceType, Type destinationType)
		{
			Mapper.Configuration.CreateMissingTypeMaps = true;
			ResolutionContext context = new ResolutionContext(this._configurationProvider.ResolveTypeMap(source, null, sourceType, destinationType), source, sourceType, destinationType, new MappingOperationOptions(), this._engine);
			return this._engine.Map(context);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000354C File Offset: 0x0000174C
		void IDynamicMapper.DynamicMap(object source, object destination, Type sourceType, Type destinationType)
		{
			Mapper.Configuration.CreateMissingTypeMaps = true;
			ResolutionContext context = new ResolutionContext(this._configurationProvider.ResolveTypeMap(source, destination, sourceType, destinationType), source, destination, sourceType, destinationType, new MappingOperationOptions(), this._engine);
			this._engine.Map(context);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003598 File Offset: 0x00001798
		private object MapCore(object source, Type sourceType, Type destinationType, MappingOperationOptions options)
		{
			ResolutionContext context = new ResolutionContext(this._configurationProvider.ResolveTypeMap(source, null, sourceType, destinationType), source, sourceType, destinationType, options, this._engine);
			return this._engine.Map(context);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000035D4 File Offset: 0x000017D4
		private object MapCore(object source, object destination, Type sourceType, Type destinationType, MappingOperationOptions options)
		{
			ResolutionContext context = new ResolutionContext(this._configurationProvider.ResolveTypeMap(source, destination, sourceType, destinationType), source, destination, sourceType, destinationType, options, this._engine);
			return this._engine.Map(context);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003610 File Offset: 0x00001810
		private void DefaultMappingOptions(IMappingOperationOptions opts)
		{
			opts.ConstructServicesUsing(this._serviceCtor);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000361E File Offset: 0x0000181E
		IConfigurationProvider IMapper.ConfigurationProvider
		{
			get
			{
				return this._configurationProvider;
			}
		}

		// Token: 0x0400001C RID: 28
		private static readonly Func<MapperConfiguration> _configurationInit = () => new MapperConfiguration(delegate(IMapperConfiguration cfg)
		{
		}, MapperRegistry.Mappers, TypeMapObjectMapperRegistry.Mappers);

		// Token: 0x0400001D RID: 29
		private static Lazy<MapperConfiguration> _configuration = new Lazy<MapperConfiguration>(Mapper._configurationInit);

		// Token: 0x0400001E RID: 30
		private static readonly Func<Mapper> _mappingEngineInit = () => new Mapper(Mapper._configuration.Value);

		// Token: 0x0400001F RID: 31
		private static Lazy<Mapper> _mappingEngine = new Lazy<Mapper>(Mapper._mappingEngineInit);

		// Token: 0x04000020 RID: 32
		private readonly IMappingEngine _engine;

		// Token: 0x04000021 RID: 33
		private readonly IConfigurationProvider _configurationProvider;

		// Token: 0x04000022 RID: 34
		private readonly Func<Type, object> _serviceCtor;
	}
}
