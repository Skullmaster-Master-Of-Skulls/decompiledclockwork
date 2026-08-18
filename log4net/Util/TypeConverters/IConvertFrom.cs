using System;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000AD RID: 173
	public interface IConvertFrom
	{
		// Token: 0x06000505 RID: 1285
		bool CanConvertFrom(Type sourceType);

		// Token: 0x06000506 RID: 1286
		object ConvertFrom(object source);
	}
}
