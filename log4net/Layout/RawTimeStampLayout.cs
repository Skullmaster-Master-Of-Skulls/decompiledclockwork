using System;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000B0 RID: 176
	public class RawTimeStampLayout : IRawLayout
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000FF91 File Offset: 0x0000E191
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.TimeStamp;
		}
	}
}
