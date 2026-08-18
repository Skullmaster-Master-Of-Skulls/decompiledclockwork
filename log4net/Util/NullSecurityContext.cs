using System;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x0200010A RID: 266
	public sealed class NullSecurityContext : SecurityContext
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00017C3F File Offset: 0x00015E3F
		private NullSecurityContext()
		{
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00017C47 File Offset: 0x00015E47
		public override IDisposable Impersonate(object state)
		{
			return null;
		}

		// Token: 0x040002D5 RID: 725
		public static readonly NullSecurityContext Instance = new NullSecurityContext();
	}
}
