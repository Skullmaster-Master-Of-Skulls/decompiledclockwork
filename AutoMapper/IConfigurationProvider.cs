using System;
using System.Collections.Generic;
using AutoMapper.Mappers;
using AutoMapper.QueryableExtensions;

namespace AutoMapper
{
	// Token: 0x0200000E RID: 14
	public interface IConfigurationProvider
	{
		// Token: 0x06000054 RID: 84
		TypeMap[] GetAllTypeMaps();

		// Token: 0x06000055 RID: 85
		TypeMap FindTypeMapFor(Type sourceType, Type destinationType);

		// Token: 0x06000056 RID: 86
		TypeMap FindTypeMapFor(TypePair typePair);

		// Token: 0x06000057 RID: 87
		TypeMap FindTypeMapFor<TSource, TDestination>();

		// Token: 0x06000058 RID: 88
		TypeMap ResolveTypeMap(object source, object destination, Type sourceType, Type destinationType);

		// Token: 0x06000059 RID: 89
		TypeMap ResolveTypeMap(Type sourceType, Type destinationType);

		// Token: 0x0600005A RID: 90
		TypeMap ResolveTypeMap(TypePair typePair);

		// Token: 0x0600005B RID: 91
		TypeMap ResolveTypeMap(ResolutionResult resolutionResult, Type destinationType);

		// Token: 0x0600005C RID: 92
		IProfileConfiguration GetProfileConfiguration(string profileName);

		// Token: 0x0600005D RID: 93
		void AssertConfigurationIsValid();

		// Token: 0x0600005E RID: 94
		void AssertConfigurationIsValid(TypeMap typeMap);

		// Token: 0x0600005F RID: 95
		void AssertConfigurationIsValid(string profileName);

		// Token: 0x06000060 RID: 96
		void AssertConfigurationIsValid<TProfile>() where TProfile : Profile, new();

		// Token: 0x06000061 RID: 97
		IEnumerable<IObjectMapper> GetMappers();

		// Token: 0x06000062 RID: 98
		IEnumerable<ITypeMapObjectMapper> GetTypeMapMappers();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99
		Func<Type, object> ServiceCtor { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100
		bool AllowNullDestinationValues { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101
		bool AllowNullCollections { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102
		IExpressionBuilder ExpressionBuilder { get; }
	}
}
