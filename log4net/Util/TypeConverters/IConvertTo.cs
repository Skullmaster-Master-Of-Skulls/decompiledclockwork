using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000E9 RID: 233
	public interface IConvertTo
	{
		// Token: 0x0600069D RID: 1693
		bool CanConvertTo(Type targetType);

		// Token: 0x0600069E RID: 1694
		object ConvertTo(object source, Type targetType);
	}
}
