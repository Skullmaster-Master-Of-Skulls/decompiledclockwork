using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.ServiceModel.Internals;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200003F RID: 63
	internal sealed class EtwDiagnosticTrace : DiagnosticTraceBase
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00009E60 File Offset: 0x00008060
		[SecurityCritical]
		static EtwDiagnosticTrace()
		{
			if (!PartialTrustHelpers.HasEtwPermissions())
			{
				EtwDiagnosticTrace.defaultEtwProviderId = Guid.Empty;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009EDC File Offset: 0x000080DC
		[SecurityCritical]
		public EtwDiagnosticTrace(string traceSourceName, Guid etwProviderId) : base(traceSourceName)
		{
			try
			{
				this.TraceSourceName = traceSourceName;
				base.EventSourceName = this.TraceSourceName + " " + "4.0.0.0";
				this.CreateTraceSource();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				EventLogger eventLogger = new EventLogger(base.EventSourceName, null);
				eventLogger.LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[]
				{
					ex.ToString()
				});
			}
			try
			{
				this.CreateEtwProvider(etwProviderId);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				this.etwProvider = null;
				EventLogger eventLogger2 = new EventLogger(base.EventSourceName, null);
				eventLogger2.LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[]
				{
					ex2.ToString()
				});
			}
			if (base.TracingEnabled || this.EtwTracingEnabled)
			{
				base.AddDomainEventHandlersForCleanup();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00009FCC File Offset: 0x000081CC
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00009FD3 File Offset: 0x000081D3
		public static Guid DefaultEtwProviderId
		{
			[SecuritySafeCritical]
			get
			{
				return EtwDiagnosticTrace.defaultEtwProviderId;
			}
			[SecurityCritical]
			set
			{
				EtwDiagnosticTrace.defaultEtwProviderId = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009FDB File Offset: 0x000081DB
		public EtwProvider EtwProvider
		{
			[SecurityCritical]
			get
			{
				return this.etwProvider;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00009FE3 File Offset: 0x000081E3
		public bool IsEtwProviderEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEnabled();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009FFA File Offset: 0x000081FA
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000A007 File Offset: 0x00008207
		public Action RefreshState
		{
			[SecuritySafeCritical]
			get
			{
				return this.EtwProvider.ControllerCallBack;
			}
			[SecuritySafeCritical]
			set
			{
				this.EtwProvider.ControllerCallBack = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A015 File Offset: 0x00008215
		public bool IsEnd2EndActivityTracingEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.IsEtwProviderEnabled && this.EtwProvider.IsEnd2EndActivityTracingEnabled;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A02C File Offset: 0x0000822C
		private bool EtwTracingEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.etwProvider != null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A037 File Offset: 0x00008237
		[SecuritySafeCritical]
		public void SetEnd2EndActivityTracingEnabled(bool isEnd2EndTracingEnabled)
		{
			this.EtwProvider.SetEnd2EndActivityTracingEnabled(isEnd2EndTracingEnabled);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A045 File Offset: 0x00008245
		public void SetAnnotation(Func<string> annotation)
		{
			EtwDiagnosticTrace.traceAnnotation = annotation;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A04D File Offset: 0x0000824D
		public override bool ShouldTrace(TraceEventLevel level)
		{
			return base.ShouldTrace(level) || this.ShouldTraceToEtw(level);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A061 File Offset: 0x00008261
		[SecuritySafeCritical]
		public bool ShouldTraceToEtw(TraceEventLevel level)
		{
			return this.EtwProvider != null && this.EtwProvider.IsEnabled((byte)level, 0L);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A07C File Offset: 0x0000827C
		[SecuritySafeCritical]
		public void Event(int eventId, TraceEventLevel traceEventLevel, TraceChannel channel, string description)
		{
			if (base.TracingEnabled)
			{
				EventDescriptor eventDescriptor = EtwDiagnosticTrace.GetEventDescriptor(eventId, channel, traceEventLevel);
				this.Event(ref eventDescriptor, description);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A0A4 File Offset: 0x000082A4
		[SecurityCritical]
		public void Event(ref EventDescriptor eventDescriptor, string description)
		{
			if (base.TracingEnabled)
			{
				TracePayload serializedPayload = this.GetSerializedPayload(null, null, null);
				this.WriteTraceSource(ref eventDescriptor, description, serializedPayload);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A0CC File Offset: 0x000082CC
		public void SetAndTraceTransfer(Guid newId, bool emitTransfer)
		{
			if (emitTransfer)
			{
				this.TraceTransfer(newId);
			}
			DiagnosticTraceBase.ActivityId = newId;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A0E0 File Offset: 0x000082E0
		[SecuritySafeCritical]
		public void TraceTransfer(Guid newId)
		{
			Guid activityId = DiagnosticTraceBase.ActivityId;
			if (newId != activityId)
			{
				try
				{
					if (base.HaveListeners)
					{
						base.TraceSource.TraceTransfer(0, null, newId);
					}
					if (this.IsEtwEventEnabled(ref EtwDiagnosticTrace.transferEventDescriptor, false))
					{
						this.etwProvider.WriteTransferEvent(ref EtwDiagnosticTrace.transferEventDescriptor, new EventTraceActivity(activityId, false), newId, (EtwDiagnosticTrace.traceAnnotation == null) ? string.Empty : EtwDiagnosticTrace.traceAnnotation(), DiagnosticTraceBase.AppDomainFriendlyName);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.LogTraceFailure(null, exception);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A180 File Offset: 0x00008380
		[SecurityCritical]
		public void WriteTraceSource(ref EventDescriptor eventDescriptor, string description, TracePayload payload)
		{
			if (base.TracingEnabled)
			{
				XPathNavigator xpathNavigator = null;
				try
				{
					string msdnTraceCode;
					int id;
					EtwDiagnosticTrace.GenerateLegacyTraceCode(ref eventDescriptor, out msdnTraceCode, out id);
					string xml = EtwDiagnosticTrace.BuildTrace(ref eventDescriptor, description, payload, msdnTraceCode);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xml);
					xpathNavigator = xmlDocument.CreateNavigator();
					base.TraceSource.TraceData(TraceLevelHelper.GetTraceEventType(eventDescriptor.Level, eventDescriptor.Opcode), id, xpathNavigator);
					if (base.CalledShutdown)
					{
						base.TraceSource.Flush();
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.LogTraceFailure((xpathNavigator == null) ? string.Empty : xpathNavigator.ToString(), exception);
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A230 File Offset: 0x00008430
		[SecurityCritical]
		private static string BuildTrace(ref EventDescriptor eventDescriptor, string description, TracePayload payload, string msdnTraceCode)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("TraceRecord");
						xmlTextWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord");
						xmlTextWriter.WriteAttributeString("Severity", TraceLevelHelper.LookupSeverity((TraceEventLevel)eventDescriptor.Level, (TraceEventOpcode)eventDescriptor.Opcode));
						xmlTextWriter.WriteAttributeString("Channel", EtwDiagnosticTrace.LookupChannel((TraceChannel)eventDescriptor.Channel));
						xmlTextWriter.WriteElementString("TraceIdentifier", msdnTraceCode);
						xmlTextWriter.WriteElementString("Description", description);
						xmlTextWriter.WriteElementString("AppDomain", payload.AppDomainFriendlyName);
						if (!string.IsNullOrEmpty(payload.EventSource))
						{
							xmlTextWriter.WriteElementString("Source", payload.EventSource);
						}
						if (!string.IsNullOrEmpty(payload.ExtendedData))
						{
							xmlTextWriter.WriteRaw(payload.ExtendedData);
						}
						if (!string.IsNullOrEmpty(payload.SerializedException))
						{
							xmlTextWriter.WriteRaw(payload.SerializedException);
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A3A8 File Offset: 0x000085A8
		[SecurityCritical]
		private static void GenerateLegacyTraceCode(ref EventDescriptor eventDescriptor, out string msdnTraceCode, out int legacyEventId)
		{
			switch (eventDescriptor.EventId)
			{
			case 57393:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "AppDomainUnload");
				legacyEventId = 131073;
				return;
			case 57394:
			case 57404:
			case 57405:
			case 57406:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "TraceHandledException");
				legacyEventId = 131076;
				return;
			case 57396:
			case 57407:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "ThrowingException");
				legacyEventId = 131075;
				return;
			case 57397:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "UnhandledException");
				legacyEventId = 131077;
				return;
			}
			msdnTraceCode = eventDescriptor.EventId.ToString(CultureInfo.InvariantCulture);
			legacyEventId = eventDescriptor.EventId;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A487 File Offset: 0x00008687
		private static string GenerateMsdnTraceCode(string traceSource, string traceCodeString)
		{
			return string.Format(CultureInfo.InvariantCulture, "https://docs.microsoft.com/dotnet/framework/wcf/diagnostics/tracing/{0}-{1}", new object[]
			{
				traceSource.Replace('.', '-'),
				traceCodeString
			});
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A4B0 File Offset: 0x000086B0
		private static string LookupChannel(TraceChannel traceChannel)
		{
			string result;
			if (traceChannel != TraceChannel.Application)
			{
				switch (traceChannel)
				{
				case TraceChannel.Admin:
					result = "Admin";
					break;
				case TraceChannel.Operational:
					result = "Operational";
					break;
				case TraceChannel.Analytic:
					result = "Analytic";
					break;
				case TraceChannel.Debug:
					result = "Debug";
					break;
				case TraceChannel.Perf:
					result = "Perf";
					break;
				default:
					result = traceChannel.ToString();
					break;
				}
			}
			else
			{
				result = "Application";
			}
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A520 File Offset: 0x00008720
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception)
		{
			return this.GetSerializedPayload(source, traceRecord, exception, false);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A52C File Offset: 0x0000872C
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception, bool getServiceReference)
		{
			string eventSource = null;
			string extendedData = null;
			string serializedException = null;
			if (source != null)
			{
				eventSource = DiagnosticTraceBase.CreateSourceString(source);
			}
			if (traceRecord != null)
			{
				StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
				try
				{
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
						{
							xmlTextWriter.WriteStartElement("ExtendedData");
							traceRecord.WriteTo(xmlTextWriter);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							extendedData = stringBuilder.ToString();
						}
					}
				}
				finally
				{
					EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
				}
			}
			if (exception != null)
			{
				serializedException = EtwDiagnosticTrace.ExceptionToTraceString(exception, 28672);
			}
			if (getServiceReference && EtwDiagnosticTrace.traceAnnotation != null)
			{
				return new TracePayload(serializedException, eventSource, DiagnosticTraceBase.AppDomainFriendlyName, extendedData, EtwDiagnosticTrace.traceAnnotation());
			}
			return new TracePayload(serializedException, eventSource, DiagnosticTraceBase.AppDomainFriendlyName, extendedData, string.Empty);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A628 File Offset: 0x00008828
		[SecuritySafeCritical]
		public bool IsEtwEventEnabled(ref EventDescriptor eventDescriptor)
		{
			return this.IsEtwEventEnabled(ref eventDescriptor, true);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A632 File Offset: 0x00008832
		[SecuritySafeCritical]
		public bool IsEtwEventEnabled(ref EventDescriptor eventDescriptor, bool fullCheck)
		{
			if (fullCheck)
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEventEnabled(ref eventDescriptor);
			}
			return this.EtwTracingEnabled && this.etwProvider.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A66F File Offset: 0x0000886F
		[SecuritySafeCritical]
		private void CreateTraceSource()
		{
			if (!string.IsNullOrEmpty(this.TraceSourceName))
			{
				base.SetTraceSource(new DiagnosticTraceSource(this.TraceSourceName));
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A690 File Offset: 0x00008890
		[SecurityCritical]
		private void CreateEtwProvider(Guid etwProviderId)
		{
			if (etwProviderId != Guid.Empty && EtwDiagnosticTrace.isVistaOrGreater)
			{
				this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
				if (this.etwProvider == null)
				{
					Hashtable obj = EtwDiagnosticTrace.etwProviderCache;
					lock (obj)
					{
						this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
						if (this.etwProvider == null)
						{
							this.etwProvider = new EtwProvider(etwProviderId);
							EtwDiagnosticTrace.etwProviderCache.Add(etwProviderId, this.etwProvider);
						}
					}
				}
				this.etwProviderId = etwProviderId;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A754 File Offset: 0x00008954
		[SecurityCritical]
		private static EventDescriptor GetEventDescriptor(int eventId, TraceChannel channel, TraceEventLevel traceEventLevel)
		{
			long num = 0L;
			if (channel == TraceChannel.Admin)
			{
				num |= long.MinValue;
			}
			else if (channel == TraceChannel.Operational)
			{
				num |= 4611686018427387904L;
			}
			else if (channel == TraceChannel.Analytic)
			{
				num |= 2305843009213693952L;
			}
			else if (channel == TraceChannel.Debug)
			{
				num |= 72057594037927936L;
			}
			else if (channel == TraceChannel.Perf)
			{
				num |= 576460752303423488L;
			}
			return new EventDescriptor(eventId, 0, (byte)channel, (byte)traceEventLevel, 0, 0, num);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A7CF File Offset: 0x000089CF
		protected override void OnShutdownTracing()
		{
			this.ShutdownTraceSource();
			this.ShutdownEtwProvider();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A7E0 File Offset: 0x000089E0
		private void ShutdownTraceSource()
		{
			try
			{
				if (TraceCore.AppDomainUnloadIsEnabled(this))
				{
					TraceCore.AppDomainUnload(this, AppDomain.CurrentDomain.FriendlyName, DiagnosticTraceBase.ProcessName, DiagnosticTraceBase.ProcessId.ToString(CultureInfo.CurrentCulture));
				}
				base.TraceSource.Flush();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.LogTraceFailure(null, exception);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A850 File Offset: 0x00008A50
		[SecuritySafeCritical]
		private void ShutdownEtwProvider()
		{
			try
			{
				if (this.etwProvider != null)
				{
					this.etwProvider.Dispose();
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.LogTraceFailure(null, exception);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A898 File Offset: 0x00008A98
		public override bool IsEnabled()
		{
			return TraceCore.TraceCodeEventLogCriticalIsEnabled(this) || TraceCore.TraceCodeEventLogVerboseIsEnabled(this) || TraceCore.TraceCodeEventLogInfoIsEnabled(this) || TraceCore.TraceCodeEventLogWarningIsEnabled(this) || TraceCore.TraceCodeEventLogErrorIsEnabled(this);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		public override void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord)
		{
			switch (type)
			{
			case TraceEventType.Critical:
				if (TraceCore.TraceCodeEventLogCriticalIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogCritical(this, traceRecord);
					return;
				}
				break;
			case TraceEventType.Error:
				if (TraceCore.TraceCodeEventLogErrorIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogError(this, traceRecord);
				}
				break;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				if (TraceCore.TraceCodeEventLogWarningIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogWarning(this, traceRecord);
					return;
				}
				break;
			default:
				if (type != TraceEventType.Information)
				{
					if (type != TraceEventType.Verbose)
					{
						return;
					}
					if (TraceCore.TraceCodeEventLogVerboseIsEnabled(this))
					{
						TraceCore.TraceCodeEventLogVerbose(this, traceRecord);
						return;
					}
				}
				else if (TraceCore.TraceCodeEventLogInfoIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogInfo(this, traceRecord);
					return;
				}
				break;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A942 File Offset: 0x00008B42
		protected override void OnUnhandledException(Exception exception)
		{
			if (TraceCore.UnhandledExceptionIsEnabled(this))
			{
				TraceCore.UnhandledException(this, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A964 File Offset: 0x00008B64
		internal static string ExceptionToTraceString(Exception exception, int maxTraceStringLength)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception, maxTraceStringLength, 64);
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		private static void WriteExceptionToTraceString(XmlTextWriter xml, Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return;
			}
			if (!EtwDiagnosticTrace.WriteStartElement(xml, "Exception", ref remainingLength))
			{
				return;
			}
			try
			{
				IList<Tuple<string, string>> list = new List<Tuple<string, string>>
				{
					new Tuple<string, string>("ExceptionType", DiagnosticTraceBase.XmlEncode(exception.GetType().AssemblyQualifiedName)),
					new Tuple<string, string>("Message", DiagnosticTraceBase.XmlEncode(exception.Message)),
					new Tuple<string, string>("StackTrace", DiagnosticTraceBase.XmlEncode(DiagnosticTraceBase.StackTraceString(exception))),
					new Tuple<string, string>("ExceptionString", DiagnosticTraceBase.XmlEncode(exception.ToString()))
				};
				Win32Exception ex = exception as Win32Exception;
				if (ex != null)
				{
					list.Add(new Tuple<string, string>("NativeErrorCode", ex.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture)));
				}
				foreach (Tuple<string, string> tuple in list)
				{
					if (!EtwDiagnosticTrace.WriteXmlElementString(xml, tuple.Item1, tuple.Item2, ref remainingLength))
					{
						return;
					}
				}
				if (exception.Data != null && exception.Data.Count > 0)
				{
					string exceptionData = EtwDiagnosticTrace.GetExceptionData(exception);
					if (exceptionData.Length < remainingLength)
					{
						xml.WriteRaw(exceptionData);
						remainingLength -= exceptionData.Length;
					}
				}
				if (exception.InnerException != null)
				{
					string innerException = EtwDiagnosticTrace.GetInnerException(exception, remainingLength, remainingAllowedRecursionDepth - 1);
					if (!string.IsNullOrEmpty(innerException) && innerException.Length < remainingLength)
					{
						xml.WriteRaw(innerException);
					}
				}
			}
			finally
			{
				xml.WriteEndElement();
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		private static string GetInnerException(Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return null;
			}
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						if (!EtwDiagnosticTrace.WriteStartElement(xmlTextWriter, "InnerException", ref remainingLength))
						{
							result = null;
						}
						else
						{
							EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception.InnerException, remainingLength, remainingAllowedRecursionDepth);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							result = stringBuilder.ToString();
						}
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AC54 File Offset: 0x00008E54
		private static string GetExceptionData(Exception exception)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("DataItems");
						foreach (object obj in exception.Data.Keys)
						{
							xmlTextWriter.WriteStartElement("Data");
							xmlTextWriter.WriteElementString("Key", DiagnosticTraceBase.XmlEncode(obj.ToString()));
							if (exception.Data[obj] == null)
							{
								xmlTextWriter.WriteElementString("Value", string.Empty);
							}
							else
							{
								xmlTextWriter.WriteElementString("Value", DiagnosticTraceBase.XmlEncode(exception.Data[obj].ToString()));
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AD98 File Offset: 0x00008F98
		private static bool WriteStartElement(XmlTextWriter xml, string localName, ref int remainingLength)
		{
			int num = localName.Length * 2 + 5;
			if (num <= remainingLength)
			{
				xml.WriteStartElement(localName);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		private static bool WriteXmlElementString(XmlTextWriter xml, string localName, string value, ref int remainingLength)
		{
			int num;
			if (string.IsNullOrEmpty(value) && !LocalAppContextSwitches.IncludeNullExceptionMessageInETWTrace)
			{
				num = localName.Length + 4;
			}
			else
			{
				num = localName.Length * 2 + 5 + value.Length;
			}
			if (num <= remainingLength)
			{
				xml.WriteElementString(localName, value);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x04000104 RID: 260
		private const int WindowsVistaMajorNumber = 6;

		// Token: 0x04000105 RID: 261
		private const string EventSourceVersion = "4.0.0.0";

		// Token: 0x04000106 RID: 262
		private const ushort TracingEventLogCategory = 4;

		// Token: 0x04000107 RID: 263
		private const int MaxExceptionStringLength = 28672;

		// Token: 0x04000108 RID: 264
		private const int MaxExceptionDepth = 64;

		// Token: 0x04000109 RID: 265
		private const string DiagnosticTraceSource = "System.ServiceModel.Diagnostics";

		// Token: 0x0400010A RID: 266
		private const int XmlBracketsLength = 5;

		// Token: 0x0400010B RID: 267
		private const int XmlBracketsLengthForNullValue = 4;

		// Token: 0x0400010C RID: 268
		public static readonly Guid ImmutableDefaultEtwProviderId = new Guid("{c651f5f6-1c0d-492e-8ae1-b4efd7c9d503}");

		// Token: 0x0400010D RID: 269
		[SecurityCritical]
		private static Guid defaultEtwProviderId = EtwDiagnosticTrace.ImmutableDefaultEtwProviderId;

		// Token: 0x0400010E RID: 270
		private static Hashtable etwProviderCache = new Hashtable();

		// Token: 0x0400010F RID: 271
		private static bool isVistaOrGreater = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x04000110 RID: 272
		private static Func<string> traceAnnotation;

		// Token: 0x04000111 RID: 273
		[SecurityCritical]
		private EtwProvider etwProvider;

		// Token: 0x04000112 RID: 274
		private Guid etwProviderId;

		// Token: 0x04000113 RID: 275
		[SecurityCritical]
		private static EventDescriptor transferEventDescriptor = new EventDescriptor(499, 0, 18, 0, 0, 0, 2305843009215397989L);

		// Token: 0x0200008D RID: 141
		private static class TraceCodes
		{
			// Token: 0x0400029D RID: 669
			public const string AppDomainUnload = "AppDomainUnload";

			// Token: 0x0400029E RID: 670
			public const string TraceHandledException = "TraceHandledException";

			// Token: 0x0400029F RID: 671
			public const string ThrowingException = "ThrowingException";

			// Token: 0x040002A0 RID: 672
			public const string UnhandledException = "UnhandledException";
		}

		// Token: 0x0200008E RID: 142
		private static class EventIdsWithMsdnTraceCode
		{
			// Token: 0x040002A1 RID: 673
			public const int AppDomainUnload = 57393;

			// Token: 0x040002A2 RID: 674
			public const int ThrowingExceptionWarning = 57396;

			// Token: 0x040002A3 RID: 675
			public const int ThrowingExceptionVerbose = 57407;

			// Token: 0x040002A4 RID: 676
			public const int HandledExceptionInfo = 57394;

			// Token: 0x040002A5 RID: 677
			public const int HandledExceptionWarning = 57404;

			// Token: 0x040002A6 RID: 678
			public const int HandledExceptionError = 57405;

			// Token: 0x040002A7 RID: 679
			public const int HandledExceptionVerbose = 57406;

			// Token: 0x040002A8 RID: 680
			public const int UnhandledException = 57397;
		}

		// Token: 0x0200008F RID: 143
		private static class LegacyTraceEventIds
		{
			// Token: 0x040002A9 RID: 681
			public const int Diagnostics = 131072;

			// Token: 0x040002AA RID: 682
			public const int AppDomainUnload = 131073;

			// Token: 0x040002AB RID: 683
			public const int EventLog = 131074;

			// Token: 0x040002AC RID: 684
			public const int ThrowingException = 131075;

			// Token: 0x040002AD RID: 685
			public const int TraceHandledException = 131076;

			// Token: 0x040002AE RID: 686
			public const int UnhandledException = 131077;
		}

		// Token: 0x02000090 RID: 144
		private static class StringBuilderPool
		{
			// Token: 0x06000434 RID: 1076 RVA: 0x000139EC File Offset: 0x00011BEC
			public static StringBuilder Take()
			{
				StringBuilder result = null;
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.TryDequeue(out result))
				{
					return result;
				}
				return new StringBuilder();
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00013A10 File Offset: 0x00011C10
			public static void Return(StringBuilder sb)
			{
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Count <= 64)
				{
					sb.Clear();
					EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Enqueue(sb);
				}
			}

			// Token: 0x040002AF RID: 687
			private const int maxPooledStringBuilders = 64;

			// Token: 0x040002B0 RID: 688
			private static readonly ConcurrentQueue<StringBuilder> freeStringBuilders = new ConcurrentQueue<StringBuilder>();
		}
	}
}
