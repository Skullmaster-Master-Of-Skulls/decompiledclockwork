using System;

namespace AutoMapper
{
	// Token: 0x02000015 RID: 21
	public interface IMappingEngine
	{
		// Token: 0x0600007C RID: 124
		bool ShouldMapSourceValueAsNull(ResolutionContext context);

		// Token: 0x0600007D RID: 125
		bool ShouldMapSourceCollectionAsNull(ResolutionContext context);

		// Token: 0x0600007E RID: 126
		object CreateObject(ResolutionContext context);

		// Token: 0x0600007F RID: 127
		object Map(ResolutionContext context);

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128
		IConfigurationProvider ConfigurationProvider { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129
		IMapper Mapper { get; }
	}
}
