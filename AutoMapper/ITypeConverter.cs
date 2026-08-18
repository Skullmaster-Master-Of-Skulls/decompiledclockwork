using System;

namespace AutoMapper
{
	// Token: 0x0200002A RID: 42
	public interface ITypeConverter<in TSource, out TDestination>
	{
		// Token: 0x06000123 RID: 291
		TDestination Convert(ResolutionContext context);
	}
}
