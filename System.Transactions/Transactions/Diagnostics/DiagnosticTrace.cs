using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace System.Transactions.Diagnostics
{
	// Token: 0x0200009F RID: 159
	internal static class DiagnosticTrace
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0003EB54 File Offset: 0x0003DF54
		private static string ProcessName
		{
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0003EBA4 File Offset: 0x0003DFA4
		private static int ProcessId
		{
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0003EBF4 File Offset: 0x0003DFF4
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0003EC14 File Offset: 0x0003E014
		private static TraceSource TraceSource
		{
			get
			{
				return DiagnosticTrace.traceSource;
			}
			set
			{
				DiagnosticTrace.traceSource = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0003EC34 File Offset: 0x0003E034
		private static Dictionary<int, string> TraceEventTypeNames
		{
			get
			{
				return DiagnosticTrace.traceEventTypeNames;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0003EC54 File Offset: 0x0003E054
		private static SourceLevels FixLevel(SourceLevels level)
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
			if ((level & (SourceLevels)(-8)) == SourceLevels.Off)
			{
				return level;
			}
			return level | SourceLevels.ActivityTracing;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0003ECC4 File Offset: 0x0003E0C4
		private static void SetLevel(SourceLevels level)
		{
			SourceLevels sourceLevels = DiagnosticTrace.FixLevel(level);
			DiagnosticTrace.level = sourceLevels;
			if (DiagnosticTrace.TraceSource != null)
			{
				DiagnosticTrace.TraceSource.Switch.Level = sourceLevels;
				DiagnosticTrace.shouldCorrelate = DiagnosticTrace.ShouldTrace(TraceEventType.Transfer);
				DiagnosticTrace.shouldTraceVerbose = DiagnosticTrace.ShouldTrace(TraceEventType.Verbose);
				DiagnosticTrace.shouldTraceInformation = DiagnosticTrace.ShouldTrace(TraceEventType.Information);
				DiagnosticTrace.shouldTraceWarning = DiagnosticTrace.ShouldTrace(TraceEventType.Warning);
				DiagnosticTrace.shouldTraceError = DiagnosticTrace.ShouldTrace(TraceEventType.Error);
				DiagnosticTrace.shouldTraceCritical = DiagnosticTrace.ShouldTrace(TraceEventType.Critical);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0003ED44 File Offset: 0x0003E144
		private static void SetLevelThreadSafe(SourceLevels level)
		{
			if (DiagnosticTrace.TracingEnabled && level != DiagnosticTrace.Level)
			{
				lock (DiagnosticTrace.localSyncObject)
				{
					DiagnosticTrace.SetLevel(level);
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0003EDA4 File Offset: 0x0003E1A4
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0003EDF4 File Offset: 0x0003E1F4
		internal static SourceLevels Level
		{
			get
			{
				if (DiagnosticTrace.TraceSource != null && DiagnosticTrace.TraceSource.Switch.Level != DiagnosticTrace.level)
				{
					DiagnosticTrace.level = DiagnosticTrace.TraceSource.Switch.Level;
				}
				return DiagnosticTrace.level;
			}
			set
			{
				DiagnosticTrace.SetLevelThreadSafe(value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0003EE14 File Offset: 0x0003E214
		internal static bool HaveListeners
		{
			get
			{
				return DiagnosticTrace.haveListeners;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0003EE34 File Offset: 0x0003E234
		internal static bool TracingEnabled
		{
			get
			{
				return DiagnosticTrace.tracingEnabled && DiagnosticTrace.traceSource != null;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0003EE64 File Offset: 0x0003E264
		static DiagnosticTrace()
		{
			DiagnosticTrace.AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;
			DiagnosticTrace.traceEventTypeNames = new Dictionary<int, string>();
			DiagnosticTrace.traceEventTypeNames[1] = "Critical";
			DiagnosticTrace.traceEventTypeNames[2] = "Error";
			DiagnosticTrace.traceEventTypeNames[4] = "Warning";
			DiagnosticTrace.traceEventTypeNames[8] = "Information";
			DiagnosticTrace.traceEventTypeNames[16] = "Verbose";
			DiagnosticTrace.traceEventTypeNames[2048] = "Resume";
			DiagnosticTrace.traceEventTypeNames[256] = "Start";
			DiagnosticTrace.traceEventTypeNames[512] = "Stop";
			DiagnosticTrace.traceEventTypeNames[1024] = "Suspend";
			DiagnosticTrace.traceEventTypeNames[4096] = "Transfer";
			DiagnosticTrace.TraceFailureThreshold = 10;
			DiagnosticTrace.TraceFailureCount = DiagnosticTrace.TraceFailureThreshold + 1;
			try
			{
				DiagnosticTrace.traceSource = new TraceSource("System.Transactions", SourceLevels.Critical);
				AppDomain currentDomain = AppDomain.CurrentDomain;
				if (DiagnosticTrace.TraceSource.Switch.ShouldTrace(TraceEventType.Critical))
				{
					currentDomain.UnhandledException += DiagnosticTrace.UnhandledExceptionHandler;
				}
				currentDomain.DomainUnload += DiagnosticTrace.ExitOrUnloadEventHandler;
				currentDomain.ProcessExit += DiagnosticTrace.ExitOrUnloadEventHandler;
				DiagnosticTrace.haveListeners = (DiagnosticTrace.TraceSource.Listeners.Count > 0);
				DiagnosticTrace.SetLevel(DiagnosticTrace.TraceSource.Switch.Level);
			}
			catch (ConfigurationErrorsException)
			{
				throw;
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (StackOverflowException)
			{
				throw;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (DiagnosticTrace.TraceSource == null)
				{
					DiagnosticTrace.LogEvent(TraceEventType.Error, string.Format(CultureInfo.CurrentCulture, SR.GetString("FailedToCreateTraceSource"), new object[]
					{
						ex
					}), true);
				}
				else
				{
					DiagnosticTrace.TraceSource = null;
					DiagnosticTrace.LogEvent(TraceEventType.Error, string.Format(CultureInfo.CurrentCulture, SR.GetString("FailedToInitializeTraceSource"), new object[]
					{
						ex
					}), true);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0003F154 File Offset: 0x0003E554
		internal static bool ShouldTrace(TraceEventType type)
		{
			return (type & (TraceEventType)DiagnosticTrace.Level) != (TraceEventType)0 && DiagnosticTrace.TraceSource != null && DiagnosticTrace.HaveListeners;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0003F184 File Offset: 0x0003E584
		internal static bool ShouldCorrelate
		{
			get
			{
				return DiagnosticTrace.shouldCorrelate;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0003F1A4 File Offset: 0x0003E5A4
		internal static bool Critical
		{
			get
			{
				return DiagnosticTrace.shouldTraceCritical;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0003F1C4 File Offset: 0x0003E5C4
		internal static bool Error
		{
			get
			{
				return DiagnosticTrace.shouldTraceError;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0003F1E4 File Offset: 0x0003E5E4
		internal static bool Warning
		{
			get
			{
				return DiagnosticTrace.shouldTraceWarning;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0003F204 File Offset: 0x0003E604
		internal static bool Information
		{
			get
			{
				return DiagnosticTrace.shouldTraceInformation;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0003F224 File Offset: 0x0003E624
		internal static bool Verbose
		{
			get
			{
				return DiagnosticTrace.shouldTraceVerbose;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0003F244 File Offset: 0x0003E644
		internal static void TraceEvent(TraceEventType type, string code, string description)
		{
			DiagnosticTrace.TraceEvent(type, code, description, null, null, ref DiagnosticTrace.EmptyGuid, false, null);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0003F264 File Offset: 0x0003E664
		internal static void TraceEvent(TraceEventType type, string code, string description, TraceRecord trace)
		{
			DiagnosticTrace.TraceEvent(type, code, description, trace, null, ref DiagnosticTrace.EmptyGuid, false, null);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0003F284 File Offset: 0x0003E684
		internal static void TraceEvent(TraceEventType type, string code, string description, TraceRecord trace, Exception exception)
		{
			DiagnosticTrace.TraceEvent(type, code, description, trace, exception, ref DiagnosticTrace.EmptyGuid, false, null);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0003F2A4 File Offset: 0x0003E6A4
		internal static void TraceEvent(TraceEventType type, string code, string description, TraceRecord trace, Exception exception, ref Guid activityId, bool emitTransfer, object source)
		{
			if (DiagnosticTrace.ShouldTrace(type))
			{
				using (Activity.CreateActivity(activityId, emitTransfer))
				{
					XPathNavigator data = DiagnosticTrace.BuildTraceString(type, code, description, trace, exception, source);
					try
					{
						DiagnosticTrace.TraceSource.TraceData(type, 0, data);
						if (DiagnosticTrace.calledShutdown)
						{
							DiagnosticTrace.TraceSource.Flush();
						}
					}
					catch (OutOfMemoryException)
					{
						throw;
					}
					catch (StackOverflowException)
					{
						throw;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception e)
					{
						string @string = SR.GetString("TraceFailure", new object[]
						{
							type.ToString(),
							code,
							description,
							(source == null) ? string.Empty : DiagnosticTrace.CreateSourceString(source)
						});
						DiagnosticTrace.LogTraceFailure(@string, e);
					}
					catch
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0003F404 File Offset: 0x0003E804
		internal static void TraceAndLogEvent(TraceEventType type, string code, string description, TraceRecord trace, Exception exception, ref Guid activityId, object source)
		{
			bool flag = DiagnosticTrace.ShouldTrace(type);
			string traceString = null;
			try
			{
				DiagnosticTrace.LogEvent(type, code, description, trace, exception, source);
				if (flag)
				{
					DiagnosticTrace.TraceEvent(type, code, description, trace, exception, ref activityId, false, source);
				}
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (StackOverflowException)
			{
				throw;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception e)
			{
				DiagnosticTrace.LogTraceFailure(traceString, e);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0003F4E4 File Offset: 0x0003E8E4
		internal static void TraceTransfer(Guid newId)
		{
			Guid activityId = DiagnosticTrace.GetActivityId();
			if (DiagnosticTrace.ShouldCorrelate && newId != activityId && DiagnosticTrace.HaveListeners)
			{
				try
				{
					if (newId != activityId)
					{
						DiagnosticTrace.TraceSource.TraceTransfer(0, null, newId);
					}
				}
				catch (OutOfMemoryException)
				{
					throw;
				}
				catch (StackOverflowException)
				{
					throw;
				}
				catch (ThreadAbortException)
				{
					throw;
				}
				catch (Exception e)
				{
					DiagnosticTrace.LogTraceFailure(null, e);
				}
				catch
				{
					throw;
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0003F5C4 File Offset: 0x0003E9C4
		internal static Guid GetActivityId()
		{
			object obj = Trace.CorrelationManager.ActivityId;
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0003F5F4 File Offset: 0x0003E9F4
		internal static void GetActivityId(ref Guid guid)
		{
			if (DiagnosticTrace.ShouldCorrelate)
			{
				guid = DiagnosticTrace.GetActivityId();
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0003F614 File Offset: 0x0003EA14
		internal static void SetActivityId(Guid id)
		{
			Trace.CorrelationManager.ActivityId = id;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0003F634 File Offset: 0x0003EA34
		private static string CreateSourceString(object source)
		{
			return source.GetType().ToString() + "/" + source.GetHashCode().ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0003F674 File Offset: 0x0003EA74
		private static void LogEvent(TraceEventType type, string code, string description, TraceRecord trace, Exception exception, object source)
		{
			StringBuilder stringBuilder = new StringBuilder(SR.GetString("EventLogValue", new object[]
			{
				DiagnosticTrace.ProcessName,
				DiagnosticTrace.ProcessId.ToString(CultureInfo.CurrentCulture),
				code,
				description
			}));
			if (source != null)
			{
				stringBuilder.AppendLine(SR.GetString("EventLogSourceValue", new object[]
				{
					DiagnosticTrace.CreateSourceString(source)
				}));
			}
			if (exception != null)
			{
				stringBuilder.AppendLine(SR.GetString("EventLogExceptionValue", new object[]
				{
					exception.ToString()
				}));
			}
			if (trace != null)
			{
				stringBuilder.AppendLine(SR.GetString("EventLogEventIdValue", new object[]
				{
					trace.EventId
				}));
				stringBuilder.AppendLine(SR.GetString("EventLogTraceValue", new object[]
				{
					trace.ToString()
				}));
			}
			DiagnosticTrace.LogEvent(type, stringBuilder.ToString(), false);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0003F774 File Offset: 0x0003EB74
		internal static void LogEvent(TraceEventType type, string message, bool addProcessInfo)
		{
			if (addProcessInfo)
			{
				message = string.Format(CultureInfo.CurrentCulture, "{0}: {1}\n{2}: {3}\n{4}", new object[]
				{
					"ProcessName",
					DiagnosticTrace.ProcessName,
					"ProcessId",
					DiagnosticTrace.ProcessId,
					message
				});
			}
			DiagnosticTrace.LogEvent(type, message);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0003F7D4 File Offset: 0x0003EBD4
		internal static void LogEvent(TraceEventType type, string message)
		{
			try
			{
				if (!string.IsNullOrEmpty(message) && message.Length >= 8192)
				{
					message = message.Substring(0, 8191);
				}
				EventLog.WriteEntry(".NET Runtime", message, DiagnosticTrace.EventLogEntryTypeFromEventType(type));
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (StackOverflowException)
			{
				throw;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch
			{
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0003F894 File Offset: 0x0003EC94
		private static string LookupSeverity(TraceEventType type)
		{
			int num = (int)(type & (TraceEventType)31);
			if ((type & (TraceEventType)768) != (TraceEventType)0)
			{
				num = (int)type;
			}
			else if (num == 0)
			{
				num = 16;
			}
			return DiagnosticTrace.TraceEventTypeNames[num];
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0003F8C4 File Offset: 0x0003ECC4
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0003F8E4 File Offset: 0x0003ECE4
		private static int TraceFailureCount
		{
			get
			{
				return DiagnosticTrace.traceFailureCount;
			}
			set
			{
				DiagnosticTrace.traceFailureCount = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0003F904 File Offset: 0x0003ED04
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0003F924 File Offset: 0x0003ED24
		private static int TraceFailureThreshold
		{
			get
			{
				return DiagnosticTrace.traceFailureThreshold;
			}
			set
			{
				DiagnosticTrace.traceFailureThreshold = value;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0003F944 File Offset: 0x0003ED44
		private static void LogTraceFailure(string traceString, Exception e)
		{
			if (e != null)
			{
				traceString = string.Format(CultureInfo.CurrentCulture, SR.GetString("FailedToTraceEvent"), new object[]
				{
					e,
					(traceString != null) ? traceString : ""
				});
			}
			lock (DiagnosticTrace.localSyncObject)
			{
				if (DiagnosticTrace.TraceFailureCount > DiagnosticTrace.TraceFailureThreshold)
				{
					DiagnosticTrace.TraceFailureCount = 1;
					DiagnosticTrace.TraceFailureThreshold *= 2;
					DiagnosticTrace.LogEvent(TraceEventType.Error, traceString, true);
				}
				else
				{
					DiagnosticTrace.TraceFailureCount++;
				}
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0003F9F4 File Offset: 0x0003EDF4
		private static void ShutdownTracing()
		{
			if (DiagnosticTrace.TraceSource != null)
			{
				try
				{
					if (DiagnosticTrace.Level != SourceLevels.Off)
					{
						if (DiagnosticTrace.Information)
						{
							Dictionary<string, string> dictionary = new Dictionary<string, string>(3);
							dictionary["AppDomain.FriendlyName"] = AppDomain.CurrentDomain.FriendlyName;
							dictionary["ProcessName"] = DiagnosticTrace.ProcessName;
							dictionary["ProcessId"] = DiagnosticTrace.ProcessId.ToString(CultureInfo.CurrentCulture);
							DiagnosticTrace.TraceEvent(TraceEventType.Information, "http://msdn.microsoft.com/TraceCodes/System/ActivityTracing/2004/07/Diagnostics/AppDomainUnload", SR.GetString("TraceCodeAppDomainUnloading"), new DictionaryTraceRecord(dictionary), null, ref DiagnosticTrace.EmptyGuid, false, null);
						}
						DiagnosticTrace.calledShutdown = true;
						DiagnosticTrace.TraceSource.Flush();
					}
				}
				catch (OutOfMemoryException)
				{
					throw;
				}
				catch (StackOverflowException)
				{
					throw;
				}
				catch (ThreadAbortException)
				{
					throw;
				}
				catch (Exception e)
				{
					DiagnosticTrace.LogTraceFailure(null, e);
				}
				catch
				{
					throw;
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0003FB34 File Offset: 0x0003EF34
		private static void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			DiagnosticTrace.ShutdownTracing();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0003FB54 File Offset: 0x0003EF54
		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception exception = (Exception)args.ExceptionObject;
			DiagnosticTrace.TraceEvent(TraceEventType.Critical, "http://msdn.microsoft.com/TraceCodes/System/ActivityTracing/2004/07/Reliability/Exception/Unhandled", SR.GetString("UnhandledException"), null, exception, ref DiagnosticTrace.EmptyGuid, false, null);
			DiagnosticTrace.ShutdownTracing();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0003FB94 File Offset: 0x0003EF94
		private static XPathNavigator BuildTraceString(TraceEventType type, string code, string description, TraceRecord trace, Exception exception, object source)
		{
			return DiagnosticTrace.BuildTraceString(new PlainXmlWriter(), type, code, description, trace, exception, source);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0003FBB4 File Offset: 0x0003EFB4
		private static XPathNavigator BuildTraceString(PlainXmlWriter xml, TraceEventType type, string code, string description, TraceRecord trace, Exception exception, object source)
		{
			xml.WriteStartElement("TraceRecord");
			xml.WriteAttributeString("xmlns", "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord");
			xml.WriteAttributeString("Severity", DiagnosticTrace.LookupSeverity(type));
			xml.WriteElementString("TraceIdentifier", code);
			xml.WriteElementString("Description", description);
			xml.WriteElementString("AppDomain", DiagnosticTrace.AppDomainFriendlyName);
			if (source != null)
			{
				xml.WriteElementString("Source", DiagnosticTrace.CreateSourceString(source));
			}
			if (trace != null)
			{
				xml.WriteStartElement("ExtendedData");
				xml.WriteAttributeString("xmlns", trace.EventId);
				trace.WriteTo(xml);
				xml.WriteEndElement();
			}
			if (exception != null)
			{
				xml.WriteStartElement("Exception");
				DiagnosticTrace.AddExceptionToTraceString(xml, exception);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();
			return xml.ToNavigator();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0003FC84 File Offset: 0x0003F084
		private static void AddExceptionToTraceString(XmlWriter xml, Exception exception)
		{
			xml.WriteElementString("ExceptionType", DiagnosticTrace.XmlEncode(exception.GetType().AssemblyQualifiedName));
			xml.WriteElementString("Message", DiagnosticTrace.XmlEncode(exception.Message));
			xml.WriteElementString("StackTrace", DiagnosticTrace.XmlEncode(DiagnosticTrace.StackTraceString(exception)));
			xml.WriteElementString("ExceptionString", DiagnosticTrace.XmlEncode(exception.ToString()));
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
					xml.WriteElementString("Key", DiagnosticTrace.XmlEncode(obj.ToString()));
					xml.WriteElementString("Value", DiagnosticTrace.XmlEncode(exception.Data[obj].ToString()));
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
			}
			if (exception.InnerException != null)
			{
				xml.WriteStartElement("InnerException");
				DiagnosticTrace.AddExceptionToTraceString(xml, exception.InnerException);
				xml.WriteEndElement();
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0003FE14 File Offset: 0x0003F214
		private static string StackTraceString(Exception exception)
		{
			string text = exception.StackTrace;
			if (string.IsNullOrEmpty(text))
			{
				StackTrace stackTrace = new StackTrace(true);
				StackFrame[] frames = stackTrace.GetFrames();
				int num = 0;
				foreach (StackFrame stackFrame in frames)
				{
					Type declaringType = stackFrame.GetMethod().DeclaringType;
					if (declaringType != typeof(DiagnosticTrace))
					{
						break;
					}
					num++;
				}
				stackTrace = new StackTrace(num);
				text = stackTrace.ToString();
			}
			return text;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0003FE94 File Offset: 0x0003F294
		internal static string XmlEncode(string text)
		{
			if (text == null)
			{
				return null;
			}
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length + 8);
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				char c2 = c;
				if (c2 != '&')
				{
					switch (c2)
					{
					case '<':
						stringBuilder.Append("&lt;");
						goto IL_73;
					case '>':
						stringBuilder.Append("&gt;");
						goto IL_73;
					}
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append("&amp;");
				}
				IL_73:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0003FF24 File Offset: 0x0003F324
		private static EventLogEntryType EventLogEntryTypeFromEventType(TraceEventType type)
		{
			EventLogEntryType result = EventLogEntryType.Information;
			switch (type)
			{
			case TraceEventType.Critical:
			case TraceEventType.Error:
				result = EventLogEntryType.Error;
				break;
			case TraceEventType.Warning:
				result = EventLogEntryType.Warning;
				break;
			}
			return result;
		}

		// Token: 0x04000253 RID: 595
		internal const string DefaultTraceListenerName = "Default";

		// Token: 0x04000254 RID: 596
		private const string subType = "";

		// Token: 0x04000255 RID: 597
		private const string version = "1";

		// Token: 0x04000256 RID: 598
		private const int traceFailureLogThreshold = 10;

		// Token: 0x04000257 RID: 599
		private const string EventLogSourceName = ".NET Runtime";

		// Token: 0x04000258 RID: 600
		private const string TraceSourceName = "System.Transactions";

		// Token: 0x04000259 RID: 601
		private const string TraceRecordVersion = "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord";

		// Token: 0x0400025A RID: 602
		private static TraceSource traceSource = null;

		// Token: 0x0400025B RID: 603
		private static bool tracingEnabled = true;

		// Token: 0x0400025C RID: 604
		private static bool haveListeners = false;

		// Token: 0x0400025D RID: 605
		private static Dictionary<int, string> traceEventTypeNames;

		// Token: 0x0400025E RID: 606
		private static object localSyncObject = new object();

		// Token: 0x0400025F RID: 607
		private static int traceFailureCount = 0;

		// Token: 0x04000260 RID: 608
		private static int traceFailureThreshold = 0;

		// Token: 0x04000261 RID: 609
		private static SourceLevels level;

		// Token: 0x04000262 RID: 610
		private static bool calledShutdown = false;

		// Token: 0x04000263 RID: 611
		private static bool shouldCorrelate = false;

		// Token: 0x04000264 RID: 612
		private static bool shouldTraceVerbose = false;

		// Token: 0x04000265 RID: 613
		private static bool shouldTraceInformation = false;

		// Token: 0x04000266 RID: 614
		private static bool shouldTraceWarning = false;

		// Token: 0x04000267 RID: 615
		private static bool shouldTraceError = false;

		// Token: 0x04000268 RID: 616
		private static bool shouldTraceCritical = false;

		// Token: 0x04000269 RID: 617
		internal static Guid EmptyGuid = Guid.Empty;

		// Token: 0x0400026A RID: 618
		private static string AppDomainFriendlyName = null;
	}
}
