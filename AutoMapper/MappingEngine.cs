using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using AutoMapper.Mappers;

namespace AutoMapper
{
	// Token: 0x02000030 RID: 48
	public class MappingEngine : IMappingEngine
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00004823 File Offset: 0x00002A23
		public MappingEngine(IConfigurationProvider configurationProvider, IMapper mapper)
		{
			this.ConfigurationProvider = configurationProvider;
			this.Mapper = mapper;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004844 File Offset: 0x00002A44
		public IConfigurationProvider ConfigurationProvider { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000484C File Offset: 0x00002A4C
		public IMapper Mapper { get; }

		// Token: 0x060001C1 RID: 449 RVA: 0x00004854 File Offset: 0x00002A54
		public object Map(ResolutionContext context)
		{
			object result;
			try
			{
				if (context.TypeMap != null)
				{
					context.TypeMap.Seal();
					ITypeMapObjectMapper typeMapObjectMapper = this.ConfigurationProvider.GetTypeMapMappers().First((ITypeMapObjectMapper objectMapper) => objectMapper.IsMatch(context));
					result = ((!context.TypeMap.ShouldAssignValue(context)) ? null : typeMapObjectMapper.Map(context));
				}
				else
				{
					TypePair contextTypePair = new TypePair(context.SourceType, context.DestinationType);
					Func<IObjectMapper, bool> <>9__2;
					Func<TypePair, IObjectMapper> valueFactory = delegate(TypePair tp)
					{
						IEnumerable<IObjectMapper> mappers = this.ConfigurationProvider.GetMappers();
						Func<IObjectMapper, bool> predicate;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = ((IObjectMapper mapper) => mapper.IsMatch(contextTypePair)));
						}
						return mappers.FirstOrDefault(predicate);
					};
					IObjectMapper orAdd = this._objectMapperCache.GetOrAdd(contextTypePair, valueFactory);
					if (orAdd == null)
					{
						throw new AutoMapperMappingException(context, "Missing type map configuration or unsupported mapping.");
					}
					result = orAdd.Map(context);
				}
			}
			catch (AutoMapperMappingException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new AutoMapperMappingException(context, inner);
			}
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000049C8 File Offset: 0x00002BC8
		public object CreateObject(ResolutionContext context)
		{
			TypeMap typeMap = context.TypeMap;
			Type type = context.DestinationType;
			if (typeMap != null)
			{
				if (typeMap.DestinationCtor != null)
				{
					return typeMap.DestinationCtor(context);
				}
				if (typeMap.ConstructDestinationUsingServiceLocator)
				{
					return context.Options.ServiceCtor(type);
				}
				if (typeMap.ConstructorMap != null)
				{
					if (typeMap.ConstructorMap.CtorParams.All((ConstructorParameterMap p) => p.CanResolve))
					{
						return typeMap.ConstructorMap.ResolveValue(context);
					}
				}
			}
			if (context.DestinationValue != null)
			{
				return context.DestinationValue;
			}
			if (type.IsInterface())
			{
				type = new ProxyGenerator().GetProxyType(type);
			}
			if (this.ConfigurationProvider.AllowNullDestinationValues)
			{
				return ObjectCreator.CreateObject(type);
			}
			return ObjectCreator.CreateNonNullValue(type);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004A98 File Offset: 0x00002C98
		public bool ShouldMapSourceValueAsNull(ResolutionContext context)
		{
			if (context.DestinationType.IsValueType() && !context.DestinationType.IsNullableType())
			{
				return false;
			}
			TypeMap contextTypeMap = context.GetContextTypeMap();
			if (contextTypeMap != null)
			{
				return this.ConfigurationProvider.GetProfileConfiguration(contextTypeMap.Profile).AllowNullDestinationValues;
			}
			return this.ConfigurationProvider.AllowNullDestinationValues;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public bool ShouldMapSourceCollectionAsNull(ResolutionContext context)
		{
			TypeMap contextTypeMap = context.GetContextTypeMap();
			if (contextTypeMap != null)
			{
				return this.ConfigurationProvider.GetProfileConfiguration(contextTypeMap.Profile).AllowNullCollections;
			}
			return this.ConfigurationProvider.AllowNullCollections;
		}

		// Token: 0x0400002D RID: 45
		private readonly ConcurrentDictionary<TypePair, IObjectMapper> _objectMapperCache = new ConcurrentDictionary<TypePair, IObjectMapper>();
	}
}
