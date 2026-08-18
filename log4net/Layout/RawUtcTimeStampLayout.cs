using System;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000B1 RID: 177
	public class RawUtcTimeStampLayout : IRawLayout
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000FFA6 File Offset: 0x0000E1A6
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.TimeStampUtc;
		}
	}
}
