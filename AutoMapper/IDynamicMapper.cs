using System;

namespace AutoMapper
{
	// Token: 0x02000013 RID: 19
	[Obsolete("Use CreateMissingTypeMaps instead.")]
	public interface IDynamicMapper
	{
		// Token: 0x0600006C RID: 108
		TDestination DynamicMap<TSource, TDestination>(TSource source);

		// Token: 0x0600006D RID: 109
		TDestination DynamicMap<TDestination>(object source);

		// Token: 0x0600006E RID: 110
		object DynamicMap(object source, Type sourceType, Type destinationType);

		// Token: 0x0600006F RID: 111
		void DynamicMap<TSource, TDestination>(TSource source, TDestination destination);

		// Token: 0x06000070 RID: 112
		void DynamicMap(object source, object destination, Type sourceType, Type destinationType);
	}
}
