using System;
using System.IO;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000D8 RID: 216
	internal sealed class AppDomainPatternConverter : PatternConverter
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001478C File Offset: 0x0001298C
		protected override void Convert(TextWriter writer, object state)
		{
			writer.Write(SystemInfo.ApplicationFriendlyName);
		}
	}
}
