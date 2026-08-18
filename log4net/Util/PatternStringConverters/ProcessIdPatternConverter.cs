using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000E0 RID: 224
	internal sealed class ProcessIdPatternConverter : PatternConverter
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00014C34 File Offset: 0x00012E34
		[SecuritySafeCritical]
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				writer.Write(Process.GetCurrentProcess().Id);
			}
			catch (SecurityException)
			{
				LogLog.Debug(ProcessIdPatternConverter.declaringType, "Security exception while trying to get current process id. Error Ignored.");
				writer.Write(SystemInfo.NotAvailableText);
			}
		}

		// Token: 0x04000290 RID: 656
		private static readonly Type declaringType = typeof(ProcessIdPatternConverter);
	}
}
