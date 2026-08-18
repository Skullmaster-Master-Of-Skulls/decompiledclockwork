using System;

namespace System.Configuration
{
	// Token: 0x02000057 RID: 87
	internal class EmptyImpersonationContext : IDisposable
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00013390 File Offset: 0x00011590
		internal static IDisposable GetStaticInstance()
		{
			if (EmptyImpersonationContext.s_emptyImpersonationContext == null)
			{
				EmptyImpersonationContext.s_emptyImpersonationContext = new EmptyImpersonationContext();
			}
			return EmptyImpersonationContext.s_emptyImpersonationContext;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00005E74 File Offset: 0x00004074
		public void Dispose()
		{
		}

		// Token: 0x0400025C RID: 604
		private static volatile IDisposable s_emptyImpersonationContext;
	}
}
