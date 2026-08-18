using System;

namespace AutoMapper
{
	// Token: 0x02000019 RID: 25
	public interface IMappingOperationOptions<TSource, TDestination> : IMappingOperationOptions
	{
		// Token: 0x060000C0 RID: 192
		void BeforeMap(Action<TSource, TDestination> beforeFunction);

		// Token: 0x060000C1 RID: 193
		void AfterMap(Action<TSource, TDestination> afterFunction);
	}
}
