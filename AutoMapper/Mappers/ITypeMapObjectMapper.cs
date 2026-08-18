using System;

namespace AutoMapper.Mappers
{
	// Token: 0x02000083 RID: 131
	public interface ITypeMapObjectMapper
	{
		// Token: 0x06000426 RID: 1062
		object Map(ResolutionContext context);

		// Token: 0x06000427 RID: 1063
		bool IsMatch(ResolutionContext context);
	}
}
