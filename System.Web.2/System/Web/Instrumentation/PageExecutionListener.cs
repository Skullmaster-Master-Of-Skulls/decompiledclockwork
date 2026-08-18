using System;

namespace System.Web.Instrumentation
{
	// Token: 0x020001B3 RID: 435
	public abstract class PageExecutionListener
	{
		// Token: 0x06001678 RID: 5752
		public abstract void BeginContext(PageExecutionContext context);

		// Token: 0x06001679 RID: 5753
		public abstract void EndContext(PageExecutionContext context);
	}
}
