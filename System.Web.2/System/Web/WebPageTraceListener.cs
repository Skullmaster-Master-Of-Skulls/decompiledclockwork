using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x0200010C RID: 268
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class WebPageTraceListener : TraceListener
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		public override void Write(string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.Trace.WriteInternal(message, false);
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002E238 File Offset: 0x0002C438
		public override void Write(string message, string category)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.Trace.WriteInternal(category, message, false);
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002E280 File Offset: 0x0002C480
		public override void WriteLine(string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.Trace.WriteInternal(message, false);
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002E2C8 File Offset: 0x0002C4C8
		public override void WriteLine(string message, string category)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.Trace.WriteInternal(category, message, false);
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002E310 File Offset: 0x0002C510
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return;
			}
			string message2 = string.Concat(new string[]
			{
				SR.GetString("WebPageTraceListener_Event"),
				" ",
				id.ToString(),
				": ",
				message
			});
			if (severity <= TraceEventType.Warning)
			{
				httpContext.Trace.WarnInternal(source, message2, false);
				return;
			}
			httpContext.Trace.WriteInternal(source, message2, false);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000275F6 File Offset: 0x000257F6
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, string.Format(CultureInfo.InvariantCulture, format, args));
		}
	}
}
