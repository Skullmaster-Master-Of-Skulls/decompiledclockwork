using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000A6 RID: 166
	public interface ILayout
	{
		// Token: 0x060004DF RID: 1247
		void Format(TextWriter writer, LoggingEvent loggingEvent);

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004E0 RID: 1248
		string ContentType { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004E1 RID: 1249
		string Header { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E2 RID: 1250
		string Footer { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004E3 RID: 1251
		bool IgnoresException { get; }
	}
}
