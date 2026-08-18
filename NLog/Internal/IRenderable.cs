using System;

namespace NLog.Internal
{
	// Token: 0x02000092 RID: 146
	internal interface IRenderable
	{
		// Token: 0x060004B2 RID: 1202
		string Render(LogEventInfo logEvent);
	}
}
