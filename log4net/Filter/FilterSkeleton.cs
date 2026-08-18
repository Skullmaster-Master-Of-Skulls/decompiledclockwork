using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000080 RID: 128
	public abstract class FilterSkeleton : IFilter, IOptionHandler
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000E5D7 File Offset: 0x0000C7D7
		public virtual void ActivateOptions()
		{
		}

		// Token: 0x06000461 RID: 1121
		public abstract FilterDecision Decide(LoggingEvent loggingEvent);

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000E5E1 File Offset: 0x0000C7E1
		public IFilter Next
		{
			get
			{
				return this.m_next;
			}
			set
			{
				this.m_next = value;
			}
		}

		// Token: 0x040001E2 RID: 482
		private IFilter m_next;
	}
}
