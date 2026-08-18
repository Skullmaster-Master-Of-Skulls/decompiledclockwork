using System;

namespace log4net.Core
{
	// Token: 0x02000074 RID: 116
	public abstract class SecurityContext
	{
		// Token: 0x06000435 RID: 1077
		public abstract IDisposable Impersonate(object state);
	}
}
