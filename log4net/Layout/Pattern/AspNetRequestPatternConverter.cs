using System;
using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000090 RID: 144
	internal sealed class AspNetRequestPatternConverter : AspNetPatternLayoutConverter
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000ED98 File Offset: 0x0000CF98
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
		{
			HttpRequest httpRequest = null;
			try
			{
				httpRequest = httpContext.Request;
			}
			catch (HttpException)
			{
			}
			if (httpRequest == null)
			{
				writer.Write(SystemInfo.NotAvailableText);
				return;
			}
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Request.Params[this.Option]);
				return;
			}
			PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Request.Params);
		}
	}
}
