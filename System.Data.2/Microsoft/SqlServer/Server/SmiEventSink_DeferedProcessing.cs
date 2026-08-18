using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003E RID: 62
	internal class SmiEventSink_DeferedProcessing : SmiEventSink_Default
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0003A45C File Offset: 0x0003985C
		internal SmiEventSink_DeferedProcessing(SmiEventSink parent) : base(parent)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0003A470 File Offset: 0x00039870
		protected override void DispatchMessages(bool ignoreNonFatalMessages)
		{
		}
	}
}
