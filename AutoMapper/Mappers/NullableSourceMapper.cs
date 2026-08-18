using System;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000089 RID: 137
	public class NullableSourceMapper : IObjectMapper
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00011775 File Offset: 0x0000F975
		public object Map(ResolutionContext context)
		{
			return context.SourceValue ?? context.Engine.CreateObject(context);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001178D File Offset: 0x0000F98D
		public bool IsMatch(TypePair context)
		{
			return context.SourceType.IsNullableType() && !context.DestinationType.IsNullableType();
		}
	}
}
