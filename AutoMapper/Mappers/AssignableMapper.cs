using System;

namespace AutoMapper.Mappers
{
	// Token: 0x02000070 RID: 112
	public class AssignableMapper : IObjectMapper
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x000098EA File Offset: 0x00007AEA
		public object Map(ResolutionContext context)
		{
			if (context.SourceValue == null && !context.Engine.ShouldMapSourceValueAsNull(context))
			{
				return context.Engine.CreateObject(context);
			}
			return context.SourceValue;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00009915 File Offset: 0x00007B15
		public bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsAssignableFrom(context.SourceType);
		}
	}
}
