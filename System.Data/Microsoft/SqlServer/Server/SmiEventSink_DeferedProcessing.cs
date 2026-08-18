using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000038 RID: 56
	internal class SmiEventSink_DeferedProcessing : SmiEventSink_Default
	{
		// Token: 0x06000200 RID: 512 RVA: 0x001DD318 File Offset: 0x001DC718
		internal SmiEventSink_DeferedProcessing(SmiEventSink parent) : base(parent)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x001DD338 File Offset: 0x001DC738
		protected override void DispatchMessages(bool ignoreNonFatalMessages)
		{
		}
	}
}
