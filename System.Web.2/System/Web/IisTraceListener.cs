using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x020000D2 RID: 210
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public sealed class IisTraceListener : TraceListener
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00027348 File Offset: 0x00025548
		public IisTraceListener()
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null && !HttpRuntime.UseIntegratedPipeline && !(httpContext.WorkerRequest is ISAPIWorkerRequestInProcForIIS7))
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_7"));
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00027388 File Offset: 0x00025588
		public override void Write(string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(IntegratedTraceType.TraceWrite, message);
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x000273D0 File Offset: 0x000255D0
		public override void Write(string message, string category)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(IntegratedTraceType.TraceWrite, message);
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00027418 File Offset: 0x00025618
		public override void WriteLine(string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(IntegratedTraceType.TraceWrite, message);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00027460 File Offset: 0x00025660
		public override void WriteLine(string message, string category)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(null, string.Empty, TraceEventType.Verbose, 0, message, null, null, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(IntegratedTraceType.TraceWrite, message);
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000274A8 File Offset: 0x000256A8
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				string message = string.Empty;
				if (data != null)
				{
					message = data.ToString();
				}
				httpContext.WorkerRequest.RaiseTraceEvent(this.Convert(eventType), this.AppendTraceOptions(eventCache, message));
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002750C File Offset: 0x0002570C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			if (httpContext != null)
			{
				httpContext.WorkerRequest.RaiseTraceEvent(this.Convert(eventType), this.AppendTraceOptions(eventCache, stringBuilder.ToString()));
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000275A4 File Offset: 0x000257A4
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
			httpContext.WorkerRequest.RaiseTraceEvent(this.Convert(severity), this.AppendTraceOptions(eventCache, message));
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000275F6 File Offset: 0x000257F6
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00027614 File Offset: 0x00025814
		private string AppendTraceOptions(TraceEventCache eventCache, string message)
		{
			if (eventCache == null || base.TraceOutputOptions == TraceOptions.None)
			{
				return message;
			}
			StringBuilder stringBuilder = new StringBuilder(message, 1024);
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				stringBuilder.Append("\r\nProcessId=");
				stringBuilder.Append(eventCache.ProcessId);
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				stringBuilder.Append("\r\nLogicalOperationStack=");
				bool flag = true;
				foreach (object value in eventCache.LogicalOperationStack)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						flag = false;
					}
					stringBuilder.Append(value);
				}
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				stringBuilder.Append("\r\nThreadId=");
				stringBuilder.Append(eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				stringBuilder.Append("\r\nDateTime=");
				stringBuilder.Append(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				stringBuilder.Append("\r\nTimestamp=");
				stringBuilder.Append(eventCache.Timestamp);
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				stringBuilder.Append("\r\nCallstack=");
				stringBuilder.Append(eventCache.Callstack);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00027774 File Offset: 0x00025974
		private bool IsEnabled(TraceOptions opts)
		{
			return (opts & base.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00027784 File Offset: 0x00025984
		private IntegratedTraceType Convert(TraceEventType tet)
		{
			if (tet <= TraceEventType.Start)
			{
				if (tet <= TraceEventType.Information)
				{
					switch (tet)
					{
					case TraceEventType.Critical:
						return IntegratedTraceType.DiagCritical;
					case TraceEventType.Error:
						return IntegratedTraceType.DiagError;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						return IntegratedTraceType.DiagWarning;
					default:
						if (tet == TraceEventType.Information)
						{
							return IntegratedTraceType.DiagInfo;
						}
						break;
					}
				}
				else
				{
					if (tet == TraceEventType.Verbose)
					{
						return IntegratedTraceType.DiagVerbose;
					}
					if (tet == TraceEventType.Start)
					{
						return IntegratedTraceType.DiagStart;
					}
				}
			}
			else if (tet <= TraceEventType.Suspend)
			{
				if (tet == TraceEventType.Stop)
				{
					return IntegratedTraceType.DiagStop;
				}
				if (tet == TraceEventType.Suspend)
				{
					return IntegratedTraceType.DiagSuspend;
				}
			}
			else
			{
				if (tet == TraceEventType.Resume)
				{
					return IntegratedTraceType.DiagResume;
				}
				if (tet == TraceEventType.Transfer)
				{
					return IntegratedTraceType.DiagTransfer;
				}
			}
			return IntegratedTraceType.DiagVerbose;
		}
	}
}
