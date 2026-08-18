using System;

namespace AutoMapper
{
	// Token: 0x02000040 RID: 64
	public abstract class ValueResolver<TSource, TDestination> : IValueResolver
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00007578 File Offset: 0x00005778
		public ResolutionResult Resolve(ResolutionResult source)
		{
			if (source.Value != null && !(source.Value is TSource))
			{
				throw new AutoMapperMappingException(string.Format("Value supplied is of type {0} but expected {1}.\nChange the value resolver source type, or redirect the source value supplied to the value resolver using FromMember.", source.Value.GetType(), typeof(TSource)));
			}
			return source.New(this.ResolveCore((TSource)((object)source.Value)), typeof(TDestination));
		}

		// Token: 0x060002E2 RID: 738
		protected abstract TDestination ResolveCore(TSource source);
	}
}
