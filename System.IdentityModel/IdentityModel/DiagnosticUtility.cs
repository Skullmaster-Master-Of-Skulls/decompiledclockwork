using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Diagnostics.Application;
using System.Runtime.CompilerServices;
using System.Security;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000021 RID: 33
	internal static class DiagnosticUtility
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004A5C File Offset: 0x00002C5C
		private static void UpdateLevel()
		{
			DiagnosticUtility.level = DiagnosticUtility.DiagnosticTrace.Level;
			DiagnosticUtility.tracingEnabled = DiagnosticUtility.DiagnosticTrace.TracingEnabled;
			DiagnosticUtility.shouldTraceCritical = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Critical);
			DiagnosticUtility.shouldTraceError = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Error);
			DiagnosticUtility.shouldTraceInformation = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Information);
			DiagnosticUtility.shouldTraceWarning = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Warning);
			DiagnosticUtility.shouldTraceVerbose = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Verbose);
			DiagnosticUtility.shouldUseActivity = DiagnosticUtility.DiagnosticTrace.ShouldUseActivity;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004AE7 File Offset: 0x00002CE7
		internal static LegacyDiagnosticTrace DiagnosticTrace
		{
			get
			{
				return DiagnosticUtility.diagnosticTrace;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004AEE File Offset: 0x00002CEE
		internal static ExceptionUtility ExceptionUtility
		{
			get
			{
				return DiagnosticUtility.exceptionUtility ?? DiagnosticUtility.GetExceptionUtility();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004B00 File Offset: 0x00002D00
		private static ExceptionUtility GetExceptionUtility()
		{
			object obj = DiagnosticUtility.lockObject;
			lock (obj)
			{
				if (DiagnosticUtility.exceptionUtility == null)
				{
					DiagnosticUtility.exceptionUtility = new ExceptionUtility("System.IdentityModel", "System.IdentityModel 4.0.0.0", DiagnosticUtility.diagnosticTrace, FxTrace.Exception);
				}
			}
			return DiagnosticUtility.exceptionUtility;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004B64 File Offset: 0x00002D64
		internal static Utility Utility
		{
			get
			{
				return DiagnosticUtility.utility ?? DiagnosticUtility.GetUtility();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004B74 File Offset: 0x00002D74
		private static Utility GetUtility()
		{
			object obj = DiagnosticUtility.lockObject;
			lock (obj)
			{
				if (DiagnosticUtility.utility == null)
				{
					DiagnosticUtility.utility = new Utility(DiagnosticUtility.ExceptionUtility);
				}
			}
			return DiagnosticUtility.utility;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004BC8 File Offset: 0x00002DC8
		private static LegacyDiagnosticTrace InitializeTracing()
		{
			DiagnosticUtility.InitDiagnosticTraceImpl(TraceSourceKind.PiiTraceSource, "System.IdentityModel");
			if (!DiagnosticUtility.diagnosticTrace.HaveListeners)
			{
				DiagnosticUtility.diagnosticTrace = null;
			}
			return DiagnosticUtility.diagnosticTrace;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004BEC File Offset: 0x00002DEC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InitDiagnosticTraceImpl(TraceSourceKind sourceType, string traceSourceName)
		{
			DiagnosticUtility.diagnosticTrace = new LegacyDiagnosticTrace(sourceType, traceSourceName, "System.IdentityModel 4.0.0.0");
			DiagnosticUtility.UpdateLevel();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004C04 File Offset: 0x00002E04
		internal static bool TracingEnabled
		{
			get
			{
				return DiagnosticUtility.tracingEnabled;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004C0C File Offset: 0x00002E0C
		internal static bool ShouldTrace(TraceEventType type)
		{
			bool result = false;
			if (DiagnosticUtility.TracingEnabled)
			{
				switch (type)
				{
				case TraceEventType.Critical:
					result = DiagnosticUtility.ShouldTraceCritical;
					break;
				case TraceEventType.Error:
					result = DiagnosticUtility.ShouldTraceError;
					break;
				case (TraceEventType)3:
					break;
				case TraceEventType.Warning:
					result = DiagnosticUtility.ShouldTraceWarning;
					break;
				default:
					if (type != TraceEventType.Information)
					{
						if (type == TraceEventType.Verbose)
						{
							result = DiagnosticUtility.ShouldTraceVerbose;
						}
					}
					else
					{
						result = DiagnosticUtility.ShouldTraceInformation;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004C6C File Offset: 0x00002E6C
		internal static void TraceHandledException(Exception exception, TraceEventType traceEventType)
		{
			FxTrace.Exception.TraceHandledException(exception, traceEventType);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004C7A File Offset: 0x00002E7A
		internal static bool ShouldTraceCritical
		{
			get
			{
				return DiagnosticUtility.shouldTraceCritical;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004C81 File Offset: 0x00002E81
		internal static bool ShouldUseActivity
		{
			get
			{
				return DiagnosticUtility.shouldUseActivity;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004C88 File Offset: 0x00002E88
		internal static bool ShouldTraceError
		{
			get
			{
				return DiagnosticUtility.shouldTraceError;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004C8F File Offset: 0x00002E8F
		internal static bool ShouldTraceWarning
		{
			get
			{
				return DiagnosticUtility.shouldTraceWarning;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004C96 File Offset: 0x00002E96
		internal static bool ShouldTraceInformation
		{
			get
			{
				return DiagnosticUtility.shouldTraceInformation;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004C9D File Offset: 0x00002E9D
		internal static bool ShouldTraceVerbose
		{
			get
			{
				return DiagnosticUtility.shouldTraceVerbose;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004CA4 File Offset: 0x00002EA4
		[Conditional("DEBUG")]
		internal static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000024C1 File Offset: 0x000006C1
		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DebugAssert(string message)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004CA8 File Offset: 0x00002EA8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Exception FailFast(string message)
		{
			try
			{
				try
				{
					DiagnosticUtility.ExceptionUtility.TraceFailFast(message);
				}
				finally
				{
					Environment.FailFast(message);
				}
			}
			catch
			{
			}
			Environment.FailFast(message);
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004CF4 File Offset: 0x00002EF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Exception InvokeFinalHandler(Exception exception)
		{
			try
			{
				try
				{
					DiagnosticUtility.ExceptionUtility.TraceFailFastException(exception);
				}
				finally
				{
					Environment.FailFast(null);
				}
			}
			catch
			{
			}
			Environment.FailFast(null);
			return null;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004D40 File Offset: 0x00002F40
		public static Exception ThrowHelperArgumentNullOrEmptyString(string arg)
		{
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID0006"), arg));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004D5C File Offset: 0x00002F5C
		public static Exception ThrowHelperArgumentOutOfRange(string arg)
		{
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(arg));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004D6E File Offset: 0x00002F6E
		public static Exception ThrowHelperArgumentOutOfRange(string arg, string message)
		{
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(arg, message));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004D81 File Offset: 0x00002F81
		public static Exception ThrowHelperArgumentOutOfRange(string arg, object actualValue, string message)
		{
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(arg, actualValue, message));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004D98 File Offset: 0x00002F98
		public static Exception ThrowHelperConfigurationError(ConfigurationElement configElement, string propertyName, Exception inner)
		{
			if (inner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inner");
			}
			if (configElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configElement");
			}
			if (propertyName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("propertyName");
			}
			if (configElement.ElementInformation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0003", new object[]
				{
					"configElement.ElementInformation"
				}));
			}
			if (configElement.ElementInformation.Properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0003", new object[]
				{
					"configElement.ElementInformation.Properties"
				}));
			}
			if (configElement.ElementInformation.Properties[propertyName] == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0005", new object[]
				{
					"configElement.ElementInformation.Properties",
					propertyName
				}));
			}
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ID1024", new object[]
			{
				propertyName,
				inner.Message
			}), inner, configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004EDC File Offset: 0x000030DC
		public static Exception ThrowHelperConfigurationError(ConfigurationElement configElement, string propertyName, string message)
		{
			if (configElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configElement");
			}
			if (propertyName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("propertyName");
			}
			if (configElement.ElementInformation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0003", new object[]
				{
					"configElement.ElementInformation"
				}));
			}
			if (configElement.ElementInformation.Properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0003", new object[]
				{
					"configElement.ElementInformation.Properties"
				}));
			}
			if (configElement.ElementInformation.Properties[propertyName] == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("configElement", SR.GetString("ID0005", new object[]
				{
					"configElement.ElementInformation.Properties",
					propertyName
				}));
			}
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(message, configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004FEF File Offset: 0x000031EF
		public static Exception ThrowHelperXml(XmlReader reader, string message)
		{
			return DiagnosticUtility.ThrowHelperXml(reader, message, null);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004FFC File Offset: 0x000031FC
		public static Exception ThrowHelperXml(XmlReader reader, string message, Exception inner)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(message, inner, (xmlLineInfo != null) ? xmlLineInfo.LineNumber : 0, (xmlLineInfo != null) ? xmlLineInfo.LinePosition : 0));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005039 File Offset: 0x00003239
		public static Exception ThrowHelperInvalidOperation(string message)
		{
			return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(message));
		}

		// Token: 0x040000C6 RID: 198
		private const string TraceSourceName = "System.IdentityModel";

		// Token: 0x040000C7 RID: 199
		internal const string EventSourceName = "System.IdentityModel 4.0.0.0";

		// Token: 0x040000C8 RID: 200
		internal const string DefaultTraceListenerName = "Default";

		// Token: 0x040000C9 RID: 201
		private static SourceLevels level = SourceLevels.Off;

		// Token: 0x040000CA RID: 202
		private static bool tracingEnabled = false;

		// Token: 0x040000CB RID: 203
		private static bool shouldUseActivity = false;

		// Token: 0x040000CC RID: 204
		private static bool shouldTraceVerbose = false;

		// Token: 0x040000CD RID: 205
		private static bool shouldTraceInformation = false;

		// Token: 0x040000CE RID: 206
		private static bool shouldTraceWarning = false;

		// Token: 0x040000CF RID: 207
		private static bool shouldTraceError = false;

		// Token: 0x040000D0 RID: 208
		private static bool shouldTraceCritical = false;

		// Token: 0x040000D1 RID: 209
		private static LegacyDiagnosticTrace diagnosticTrace = DiagnosticUtility.InitializeTracing();

		// Token: 0x040000D2 RID: 210
		private static object lockObject = new object();

		// Token: 0x040000D3 RID: 211
		private static ExceptionUtility exceptionUtility = null;

		// Token: 0x040000D4 RID: 212
		private static Utility utility = null;
	}
}
