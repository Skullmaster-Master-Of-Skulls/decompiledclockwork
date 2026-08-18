using System;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000088 RID: 136
	public class NullableMapper : IObjectMapper
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00011760 File Offset: 0x0000F960
		public object Map(ResolutionContext context)
		{
			return context.SourceValue;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011768 File Offset: 0x0000F968
		public bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsNullableType();
		}
	}
}
