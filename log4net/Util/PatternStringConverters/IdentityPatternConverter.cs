using System;
using System.IO;
using System.Security;
using System.Threading;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DD RID: 221
	internal sealed class IdentityPatternConverter : PatternConverter
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x00014AE8 File Offset: 0x00012CE8
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null && Thread.CurrentPrincipal.Identity.Name != null)
				{
					writer.Write(Thread.CurrentPrincipal.Identity.Name);
				}
			}
			catch (SecurityException)
			{
				LogLog.Debug(IdentityPatternConverter.declaringType, "Security exception while trying to get current thread principal. Error Ignored.");
				writer.Write(SystemInfo.NotAvailableText);
			}
		}

		// Token: 0x0400028F RID: 655
		private static readonly Type declaringType = typeof(IdentityPatternConverter);
	}
}
