using System;
using System.IO;
using System.Web;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200008F RID: 143
	internal sealed class AspNetContextPatternConverter : AspNetPatternLayoutConverter
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000ED53 File Offset: 0x0000CF53
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
		{
			if (this.Option != null)
			{
				PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Items[this.Option]);
				return;
			}
			PatternConverter.WriteObject(writer, loggingEvent.Repository, httpContext.Items);
		}
	}
}
