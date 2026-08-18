using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000093 RID: 147
	internal sealed class ExceptionPatternConverter : PatternLayoutConverter
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000EF91 File Offset: 0x0000D191
		public ExceptionPatternConverter()
		{
			this.IgnoresException = false;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent.ExceptionObject != null && this.Option != null && this.Option.Length > 0)
			{
				string a;
				if ((a = this.Option.ToLower()) != null)
				{
					if (a == "message")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Message);
						return;
					}
					if (a == "source")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Source);
						return;
					}
					if (a == "stacktrace")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.StackTrace);
						return;
					}
					if (a == "targetsite")
					{
						PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.TargetSite);
						return;
					}
					if (!(a == "helplink"))
					{
						return;
					}
					PatternConverter.WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.HelpLink);
					return;
				}
			}
			else
			{
				string exceptionString = loggingEvent.GetExceptionString();
				if (exceptionString != null && exceptionString.Length > 0)
				{
					writer.WriteLine(exceptionString);
				}
			}
		}
	}
}
