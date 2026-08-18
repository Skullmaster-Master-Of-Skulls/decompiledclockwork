using System;

namespace AutoMapper
{
	// Token: 0x0200000D RID: 13
	public interface IConfiguration
	{
		// Token: 0x0600004B RID: 75
		IProfileExpression CreateProfile(string profileName);

		// Token: 0x0600004C RID: 76
		void CreateProfile(string profileName, Action<IProfileExpression> profileConfiguration);

		// Token: 0x0600004D RID: 77
		void AddProfile(Profile profile);

		// Token: 0x0600004E RID: 78
		void AddProfile<TProfile>() where TProfile : Profile, new();

		// Token: 0x0600004F RID: 79
		void ConstructServicesUsing(Func<Type, object> constructor);

		// Token: 0x06000050 RID: 80
		IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(string profileName);

		// Token: 0x06000051 RID: 81
		IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(string profileName, MemberList memberList);

		// Token: 0x06000052 RID: 82
		void ForAllMaps(string profileName, Action<TypeMap, IMappingExpression> configuration);

		// Token: 0x06000053 RID: 83
		IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList memberList, string profileName);
	}
}
