using System;

namespace AutoMapper.Internal
{
	// Token: 0x02000095 RID: 149
	public class DefaultResolver : IValueResolver
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x000125A1 File Offset: 0x000107A1
		public ResolutionResult Resolve(ResolutionResult source)
		{
			return source.New(source.Value);
		}
	}
}
