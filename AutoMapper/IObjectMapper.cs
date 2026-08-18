using System;

namespace AutoMapper
{
	// Token: 0x02000021 RID: 33
	public interface IObjectMapper
	{
		// Token: 0x060000EB RID: 235
		object Map(ResolutionContext context);

		// Token: 0x060000EC RID: 236
		bool IsMatch(TypePair context);
	}
}
