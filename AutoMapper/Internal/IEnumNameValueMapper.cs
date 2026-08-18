using System;

namespace AutoMapper.Internal
{
	// Token: 0x020000A8 RID: 168
	public interface IEnumNameValueMapper
	{
		// Token: 0x060004CA RID: 1226
		bool IsMatch(Type enumDestinationType, string sourceValue);

		// Token: 0x060004CB RID: 1227
		object Convert(Type enumSourceType, Type enumDestinationType, ResolutionContext context);
	}
}
