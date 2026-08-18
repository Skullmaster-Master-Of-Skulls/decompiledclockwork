using System;

namespace AutoMapper.Mappers
{
	// Token: 0x02000090 RID: 144
	public class StringMapper : IObjectMapper
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		public object Map(ResolutionContext context)
		{
			object sourceValue = context.SourceValue;
			if (sourceValue == null)
			{
				return null;
			}
			return sourceValue.ToString();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011CCB File Offset: 0x0000FECB
		public bool IsMatch(TypePair context)
		{
			return context.DestinationType == typeof(string) && context.SourceType != typeof(string);
		}
	}
}
