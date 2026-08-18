using System;

namespace AutoMapper.Internal
{
	// Token: 0x02000098 RID: 152
	public class DeferredInstantiatedResolver : IValueResolver
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0001266C File Offset: 0x0001086C
		public DeferredInstantiatedResolver(Func<ResolutionContext, IValueResolver> constructor)
		{
			this._constructor = constructor;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001267B File Offset: 0x0001087B
		public ResolutionResult Resolve(ResolutionResult source)
		{
			return this._constructor(source.Context).Resolve(source);
		}

		// Token: 0x040000D8 RID: 216
		private readonly Func<ResolutionContext, IValueResolver> _constructor;
	}
}
