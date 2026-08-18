using System;

namespace AutoMapper
{
	// Token: 0x02000031 RID: 49
	public class MappingOperationOptions<TSource, TDestination> : MappingOperationOptions, IMappingOperationOptions<TSource, TDestination>, IMappingOperationOptions
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00004B2C File Offset: 0x00002D2C
		public void BeforeMap(Action<TSource, TDestination> beforeFunction)
		{
			base.BeforeMapAction = delegate(object src, object dest)
			{
				beforeFunction((TSource)((object)src), (TDestination)((object)dest));
			};
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004B58 File Offset: 0x00002D58
		public void AfterMap(Action<TSource, TDestination> afterFunction)
		{
			base.AfterMapAction = delegate(object src, object dest)
			{
				afterFunction((TSource)((object)src), (TDestination)((object)dest));
			};
		}
	}
}
