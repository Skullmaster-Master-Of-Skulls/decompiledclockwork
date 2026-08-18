using System;
using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000091 RID: 145
	internal sealed class AspNetSessionPatternConverter : AspNetPatternLayoutConverter
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000EE1C File Offset: 0x0000D01C
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
		{
			if (httpContext.Session == null)
			{
				writer.Write(SystemInfo.NotAvailableText);
				return;
			}
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Session.Contents[this.Option]);
				return;
			}
			PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Session);
		}
	}
}
