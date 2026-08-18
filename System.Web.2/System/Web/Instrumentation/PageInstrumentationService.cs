using System;
using System.Collections.Generic;

namespace System.Web.Instrumentation
{
	// Token: 0x020001B1 RID: 433
	public sealed class PageInstrumentationService
	{
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0004743D File Offset: 0x0004563D
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x00047444 File Offset: 0x00045644
		public static bool IsEnabled { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0004744C File Offset: 0x0004564C
		public IList<PageExecutionListener> ExecutionListeners
		{
			get
			{
				return this._executionListeners;
			}
		}

		// Token: 0x040016A2 RID: 5794
		private IList<PageExecutionListener> _executionListeners = new List<PageExecutionListener>();
	}
}
