using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x0200007F RID: 127
	public interface IFilter : IOptionHandler
	{
		// Token: 0x0600045D RID: 1117
		FilterDecision Decide(LoggingEvent loggingEvent);

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600045E RID: 1118
		// (set) Token: 0x0600045F RID: 1119
		IFilter Next { get; set; }
	}
}
