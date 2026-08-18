using System;
using log4net.Core;
using log4net.Util.TypeConverters;

namespace log4net.Layout
{
	// Token: 0x020000AB RID: 171
	[TypeConverter(typeof(RawLayoutConverter))]
	public interface IRawLayout
	{
		// Token: 0x06000502 RID: 1282
		object Format(LoggingEvent loggingEvent);
	}
}
