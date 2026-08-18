using System;

namespace AutoMapper
{
	// Token: 0x02000014 RID: 20
	public interface IMapper
	{
		// Token: 0x06000071 RID: 113
		TDestination Map<TDestination>(object source);

		// Token: 0x06000072 RID: 114
		TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts);

		// Token: 0x06000073 RID: 115
		TDestination Map<TSource, TDestination>(TSource source);

		// Token: 0x06000074 RID: 116
		TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts);

		// Token: 0x06000075 RID: 117
		TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

		// Token: 0x06000076 RID: 118
		TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts);

		// Token: 0x06000077 RID: 119
		object Map(object source, Type sourceType, Type destinationType);

		// Token: 0x06000078 RID: 120
		object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts);

		// Token: 0x06000079 RID: 121
		object Map(object source, object destination, Type sourceType, Type destinationType);

		// Token: 0x0600007A RID: 122
		object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts);

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123
		IConfigurationProvider ConfigurationProvider { get; }
	}
}
