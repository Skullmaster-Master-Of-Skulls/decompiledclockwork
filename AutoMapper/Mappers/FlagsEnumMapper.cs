using System;
using System.Linq;

namespace AutoMapper.Mappers
{
	// Token: 0x02000080 RID: 128
	public class FlagsEnumMapper : IObjectMapper
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x00011218 File Offset: 0x0000F418
		public object Map(ResolutionContext context)
		{
			Type enumerationType = TypeHelper.GetEnumerationType(context.DestinationType);
			if (context.SourceValue == null)
			{
				return context.Engine.CreateObject(context);
			}
			return Enum.Parse(enumerationType, context.SourceValue.ToString(), true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011258 File Offset: 0x0000F458
		public bool IsMatch(TypePair context)
		{
			Type enumerationType = TypeHelper.GetEnumerationType(context.SourceType);
			Type enumerationType2 = TypeHelper.GetEnumerationType(context.DestinationType);
			return enumerationType != null && enumerationType2 != null && enumerationType.GetCustomAttributes(typeof(FlagsAttribute), false).Any<object>() && enumerationType2.GetCustomAttributes(typeof(FlagsAttribute), false).Any<object>();
		}
	}
}
