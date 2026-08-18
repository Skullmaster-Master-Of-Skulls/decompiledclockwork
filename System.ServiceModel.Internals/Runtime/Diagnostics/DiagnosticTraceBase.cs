using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200003D RID: 61
	internal abstract class DiagnosticTraceBase
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00009484 File Offset: 0x00007684
		public DiagnosticTraceBase(string traceSourceName)
		{
			this.thisLock = new object();
			this.TraceSourceName = traceSourceName;
			this.LastFailure = DateTime.MinValue;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000094B0 File Offset: 0x000076B0
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000094B8 File Offset: 0x000076B8
		protected DateTime LastFailure { get; set; }

		// Token: 0x06000244 RID: 580 RVA: 0x000094C1 File Offset: 0x000076C1
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static void UnsafeRemoveDefaultTraceListener(TraceSource traceSource)
		{
			traceSource.Listeners.Remove("Default");
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000094D3 File Offset: 0x000076D3
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000094DB File Offset: 0x000076DB
		public TraceSource TraceSource
		{
			get
			{
				return this.traceSource;
			}
			set
			{
				this.SetTraceSource(value);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000094E4 File Offset: 0x000076E4
		[SecuritySafeCritical]
		protected void SetTraceSource(TraceSource traceSource)
		{
			if (traceSource != null)
			{
				DiagnosticTraceBase.UnsafeRemoveDefaultTraceListener(traceSource);
				this.traceSource = traceSource;
				this.haveListeners = (this.traceSource.Listeners.Count > 0);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000950F File Offset: 0x0000770F
		public bool HaveListeners
		{
			get
			{
				return this.haveListeners;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009518 File Offset: 0x00007718
		private SourceLevels FixLevel(SourceLevels level)
		{
			if ((level & (SourceLevels)(-16) & SourceLevels.Verbose) != SourceLevels.Off)
			{
				level |= SourceLevels.Verbose;
			}
			else if ((level & (SourceLevels)(-8) & SourceLevels.Information) != SourceLevels.Off)
			{
				level |= SourceLevels.Information;
			}
			else if ((level & (SourceLevels)(-4) & SourceLevels.Warning) != SourceLevels.Off)
			{
				level |= SourceLevels.Warning;
			}
			if ((level & ~SourceLevels.Critical & SourceLevels.Error) != SourceLevels.Off)
			{
				level |= SourceLevels.Error;
			}
			if ((level & SourceLevels.Critical) != SourceLevels.Off)
			{
				level |= SourceLevels.Critical;
			}
			if (level == SourceLevels.ActivityTracing)
			{
				level = SourceLevels.Off;
			}
			return level;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000033BD File Offset: 0x000015BD
		protected virtual void OnSetLevel(SourceLevels level)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009578 File Offset: 0x00007778
		[SecurityCritical]
		private void SetLevel(SourceLevels level)
		{
			SourceLevels sourceLevels = this.FixLevel(level);
			this.level = sourceLevels;
			if (this.TraceSource != null)
			{
				this.haveListeners = (this.TraceSource.Listeners.Count > 0);
				this.OnSetLevel(level);
				this.tracingEnabled = (this.HaveListeners && level > SourceLevels.Off);
				this.TraceSource.Switch.Level = level;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000095E4 File Offset: 0x000077E4
		[SecurityCritical]
		private void SetLevelThreadSafe(SourceLevels level)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.SetLevel(level);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00009628 File Offset: 0x00007828
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00009666 File Offset: 0x00007866
		public SourceLevels Level
		{
			get
			{
				if (this.TraceSource != null && this.TraceSource.Switch.Level != this.level)
				{
					this.level = this.TraceSource.Switch.Level;
				}
				return this.level;
			}
			[SecurityCritical]
			set
			{
				this.SetLevelThreadSafe(value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000966F File Offset: 0x0000786F
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00009677 File Offset: 0x00007877
		protected string EventSourceName
		{
			[SecuritySafeCritical]
			get
			{
				return this.eventSourceName;
			}
			[SecurityCritical]
			set
			{
				this.eventSourceName = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00009680 File Offset: 0x00007880
		public bool TracingEnabled
		{
			get
			{
				return this.tracingEnabled && this.traceSource != null;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00009698 File Offset: 0x00007898
		protected static string ProcessName
		{
			[SecuritySafeCritical]
			get
			{
				string result = null;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = currentProcess.ProcessName;
				}
				return result;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000096D4 File Offset: 0x000078D4
		protected static int ProcessId
		{
			[SecuritySafeCritical]
			get
			{
				int result = -1;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = currentProcess.Id;
				}
				return result;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009710 File Offset: 0x00007910
		public virtual bool ShouldTrace(TraceEventLevel level)
		{
			return this.ShouldTraceToTraceSource(level);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009719 File Offset: 0x00007919
		public bool ShouldTrace(TraceEventType type)
		{
			return this.TracingEnabled && this.HaveListeners && this.TraceSource != null && (type & (TraceEventType)this.Level) > (TraceEventType)0;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009740 File Offset: 0x00007940
		public bool ShouldTraceToTraceSource(TraceEventLevel level)
		{
			return this.ShouldTrace(TraceLevelHelper.GetTraceEventType(level));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009750 File Offset: 0x00007950
		public static string XmlEncode(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length + 8);
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c != '&')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append("&gt;");
						}
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&amp;");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000097D4 File Offset: 0x000079D4
		[SecuritySafeCritical]
		protected void AddDomainEventHandlersForCleanup()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			if (this.TraceSource != null)
			{
				this.haveListeners = (this.TraceSource.Listeners.Count > 0);
			}
			this.tracingEnabled = this.haveListeners;
			if (this.TracingEnabled)
			{
				currentDomain.UnhandledException += this.UnhandledExceptionHandler;
				this.SetLevel(this.TraceSource.Switch.Level);
				currentDomain.DomainUnload += this.ExitOrUnloadEventHandler;
				currentDomain.ProcessExit += this.ExitOrUnloadEventHandler;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009868 File Offset: 0x00007A68
		private void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			this.ShutdownTracing();
		}

		// Token: 0x0600025A RID: 602
		protected abstract void OnUnhandledException(Exception exception);

		// Token: 0x0600025B RID: 603 RVA: 0x00009870 File Offset: 0x00007A70
		protected void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception exception = (Exception)args.ExceptionObject;
			this.OnUnhandledException(exception);
			this.ShutdownTracing();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009898 File Offset: 0x00007A98
		protected static string CreateSourceString(object source)
		{
			ITraceSourceStringProvider traceSourceStringProvider = source as ITraceSourceStringProvider;
			if (traceSourceStringProvider != null)
			{
				return traceSourceStringProvider.GetSourceString();
			}
			return DiagnosticTraceBase.CreateDefaultSourceString(source);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000098BC File Offset: 0x00007ABC
		internal static string CreateDefaultSourceString(object source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", new object[]
			{
				source.GetType().ToString(),
				source.GetHashCode()
			});
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009908 File Offset: 0x00007B08
		protected static void AddExceptionToTraceString(XmlWriter xml, Exception exception)
		{
			xml.WriteElementString("ExceptionType", DiagnosticTraceBase.XmlEncode(exception.GetType().AssemblyQualifiedName));
			xml.WriteElementString("Message", DiagnosticTraceBase.XmlEncode(exception.Message));
			xml.WriteElementString("StackTrace", DiagnosticTraceBase.XmlEncode(DiagnosticTraceBase.StackTraceString(exception)));
			xml.WriteElementString("ExceptionString", DiagnosticTraceBase.XmlEncode(exception.ToString()));
			Win32Exception ex = exception as Win32Exception;
			if (ex != null)
			{
				xml.WriteElementString("NativeErrorCode", ex.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture));
			}
			if (exception.Data != null && exception.Data.Count > 0)
			{
				xml.WriteStartElement("DataItems");
				foreach (object obj in exception.Data.Keys)
				{
					xml.WriteStartElement("Data");
					xml.WriteElementString("Key", DiagnosticTraceBase.XmlEncode(obj.ToString()));
					xml.WriteElementString("Value", DiagnosticTraceBase.XmlEncode(exception.Data[obj].ToString()));
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
			}
			if (exception.InnerException != null)
			{
				xml.WriteStartElement("InnerException");
				DiagnosticTraceBase.AddExceptionToTraceString(xml, exception.InnerException);
				xml.WriteEndElement();
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009A84 File Offset: 0x00007C84
		protected static string StackTraceString(Exception exception)
		{
			string text = exception.StackTrace;
			if (string.IsNullOrEmpty(text))
			{
				StackTrace stackTrace = new StackTrace(false);
				StackFrame[] frames = stackTrace.GetFrames();
				int num = 0;
				bool flag = false;
				foreach (StackFrame stackFrame in frames)
				{
					string name = stackFrame.GetMethod().Name;
					if (name == "StackTraceString" || name == "AddExceptionToTraceString" || name == "BuildTrace" || name == "TraceEvent" || name == "TraceException" || name == "GetAdditionalPayload")
					{
						num++;
					}
					else if (name.StartsWith("ThrowHelper", StringComparison.Ordinal))
					{
						num++;
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						break;
					}
				}
				stackTrace = new StackTrace(num, false);
				text = stackTrace.ToString();
			}
			return text;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009B70 File Offset: 0x00007D70
		[SecuritySafeCritical]
		protected void LogTraceFailure(string traceString, Exception exception)
		{
			TimeSpan t = TimeSpan.FromMinutes(10.0);
			try
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (DateTime.UtcNow.Subtract(this.LastFailure) >= t)
					{
						this.LastFailure = DateTime.UtcNow;
						EventLogger eventLogger = EventLogger.UnsafeCreateEventLogger(this.eventSourceName, this);
						if (exception == null)
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291112U, false, new string[]
							{
								traceString
							});
						}
						else
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291113U, false, new string[]
							{
								traceString,
								exception.ToString()
							});
						}
					}
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
			}
		}

		// Token: 0x06000261 RID: 609
		protected abstract void OnShutdownTracing();

		// Token: 0x06000262 RID: 610 RVA: 0x00009C4C File Offset: 0x00007E4C
		private void ShutdownTracing()
		{
			if (!this.calledShutdown)
			{
				this.calledShutdown = true;
				try
				{
					this.OnShutdownTracing();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.LogTraceFailure(null, exception);
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009C98 File Offset: 0x00007E98
		protected bool CalledShutdown
		{
			get
			{
				return this.calledShutdown;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00009CA0 File Offset: 0x00007EA0
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00009CCC File Offset: 0x00007ECC
		public static Guid ActivityId
		{
			[SecuritySafeCritical]
			get
			{
				object obj = Trace.CorrelationManager.ActivityId;
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			[SecuritySafeCritical]
			set
			{
				Trace.CorrelationManager.ActivityId = value;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009CDC File Offset: 0x00007EDC
		protected static string LookupSeverity(TraceEventType type)
		{
			if (type <= TraceEventType.Verbose)
			{
				switch (type)
				{
				case TraceEventType.Critical:
					return "Critical";
				case TraceEventType.Error:
					return "Error";
				case (TraceEventType)3:
					break;
				case TraceEventType.Warning:
					return "Warning";
				default:
					if (type == TraceEventType.Information)
					{
						return "Information";
					}
					if (type == TraceEventType.Verbose)
					{
						return "Verbose";
					}
					break;
				}
			}
			else if (type <= TraceEventType.Stop)
			{
				if (type == TraceEventType.Start)
				{
					return "Start";
				}
				if (type == TraceEventType.Stop)
				{
					return "Stop";
				}
			}
			else
			{
				if (type == TraceEventType.Suspend)
				{
					return "Suspend";
				}
				if (type == TraceEventType.Transfer)
				{
					return "Transfer";
				}
			}
			return type.ToString();
		}

		// Token: 0x06000267 RID: 615
		public abstract bool IsEnabled();

		// Token: 0x06000268 RID: 616
		public abstract void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord);

		// Token: 0x040000F5 RID: 245
		protected const string DefaultTraceListenerName = "Default";

		// Token: 0x040000F6 RID: 246
		protected const string TraceRecordVersion = "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord";

		// Token: 0x040000F7 RID: 247
		protected static string AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x040000F8 RID: 248
		private const ushort TracingEventLogCategory = 4;

		// Token: 0x040000F9 RID: 249
		private object thisLock;

		// Token: 0x040000FA RID: 250
		private bool tracingEnabled = true;

		// Token: 0x040000FB RID: 251
		private bool calledShutdown;

		// Token: 0x040000FC RID: 252
		private bool haveListeners;

		// Token: 0x040000FD RID: 253
		private SourceLevels level;

		// Token: 0x040000FE RID: 254
		protected string TraceSourceName;

		// Token: 0x040000FF RID: 255
		private TraceSource traceSource;

		// Token: 0x04000100 RID: 256
		[SecurityCritical]
		private string eventSourceName;
	}
}
