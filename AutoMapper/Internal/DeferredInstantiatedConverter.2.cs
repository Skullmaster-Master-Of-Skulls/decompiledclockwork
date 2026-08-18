using System;

namespace AutoMapper.Internal
{
	// Token: 0x02000097 RID: 151
	public class DeferredInstantiatedConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x00012649 File Offset: 0x00010849
		public DeferredInstantiatedConverter(Func<ResolutionContext, ITypeConverter<TSource, TDestination>> instantiator)
		{
			this._instantiator = instantiator;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00012658 File Offset: 0x00010858
		public TDestination Convert(ResolutionContext context)
		{
			return this._instantiator(context).Convert(context);
		}

		// Token: 0x040000D7 RID: 215
		private readonly Func<ResolutionContext, ITypeConverter<TSource, TDestination>> _instantiator;
	}
}
