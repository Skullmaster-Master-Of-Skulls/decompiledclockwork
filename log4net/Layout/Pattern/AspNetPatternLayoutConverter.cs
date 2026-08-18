using System;
using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200008D RID: 141
	internal abstract class AspNetPatternLayoutConverter : PatternLayoutConverter
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000ECC6 File Offset: 0x0000CEC6
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (HttpContext.Current == null)
			{
				writer.Write(SystemInfo.NotAvailableText);
				return;
			}
			this.Convert(writer, loggingEvent, HttpContext.Current);
		}

		// Token: 0x060004A2 RID: 1186
		protected abstract void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext);
	}
}
