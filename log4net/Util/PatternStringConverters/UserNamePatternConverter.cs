using System;
using System.IO;
using System.Security;
using System.Security.Principal;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000E3 RID: 227
	internal sealed class UserNamePatternConverter : PatternConverter
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00014E48 File Offset: 0x00013048
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null && current.Name != null)
				{
					writer.Write(current.Name);
				}
			}
			catch (SecurityException)
			{
				LogLog.Debug(UserNamePatternConverter.declaringType, "Security exception while trying to get current windows identity. Error Ignored.");
				writer.Write(SystemInfo.NotAvailableText);
			}
		}

		// Token: 0x04000294 RID: 660
		private static readonly Type declaringType = typeof(UserNamePatternConverter);
	}
}
