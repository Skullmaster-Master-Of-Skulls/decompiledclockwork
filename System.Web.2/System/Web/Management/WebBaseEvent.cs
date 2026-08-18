using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x0200018A RID: 394
	public class WebBaseEvent
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00040C0C File Offset: 0x0003EE0C
		static WebBaseEvent()
		{
			for (int i = 0; i < WebBaseEvent.s_eventCodeToSystemEventTypeMappings.GetLength(0); i++)
			{
				for (int j = 0; j < WebBaseEvent.s_eventCodeToSystemEventTypeMappings.GetLength(1); j++)
				{
					WebBaseEvent.s_eventCodeToSystemEventTypeMappings[i, j] = WebBaseEvent.SystemEventType.Unknown;
				}
			}
			for (int k = 0; k < WebBaseEvent.s_eventCodeOccurrence.GetLength(0); k++)
			{
				for (int l = 0; l < WebBaseEvent.s_eventCodeOccurrence.GetLength(1); l++)
				{
					WebBaseEvent.s_eventCodeOccurrence[k, l] = 0L;
				}
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00040CE0 File Offset: 0x0003EEE0
		private void Init(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			if (eventCode < 0)
			{
				throw new ArgumentOutOfRangeException("eventCode", SR.GetString("Invalid_eventCode_error"));
			}
			if (eventDetailCode < 0)
			{
				throw new ArgumentOutOfRangeException("eventDetailCode", SR.GetString("Invalid_eventDetailCode_error"));
			}
			this._code = eventCode;
			this._detailCode = eventDetailCode;
			this._source = eventSource;
			this._eventTimeUtc = DateTime.UtcNow;
			this._message = message;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00040D48 File Offset: 0x0003EF48
		protected internal WebBaseEvent(string message, object eventSource, int eventCode)
		{
			this.Init(message, eventSource, eventCode, 0);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00040D65 File Offset: 0x0003EF65
		protected internal WebBaseEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			this.Init(message, eventSource, eventCode, eventDetailCode);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00040D83 File Offset: 0x0003EF83
		internal WebBaseEvent()
		{
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00040D96 File Offset: 0x0003EF96
		internal bool IsSystemEvent
		{
			get
			{
				return this._code < 100000;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00040DA5 File Offset: 0x0003EFA5
		public DateTime EventTime
		{
			get
			{
				return this._eventTimeUtc.ToLocalTime();
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00040DB2 File Offset: 0x0003EFB2
		public DateTime EventTimeUtc
		{
			get
			{
				return this._eventTimeUtc;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00040DBA File Offset: 0x0003EFBA
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00040DC2 File Offset: 0x0003EFC2
		public object EventSource
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00040DCA File Offset: 0x0003EFCA
		public long EventSequence
		{
			get
			{
				return this._sequenceNumber;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00040DD2 File Offset: 0x0003EFD2
		public long EventOccurrence
		{
			get
			{
				return this._occurrenceNumber;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00040DDA File Offset: 0x0003EFDA
		public int EventCode
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00040DE2 File Offset: 0x0003EFE2
		public int EventDetailCode
		{
			get
			{
				return this._detailCode;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00040DEC File Offset: 0x0003EFEC
		public Guid EventID
		{
			get
			{
				if (this._id == Guid.Empty)
				{
					lock (this)
					{
						if (this._id == Guid.Empty)
						{
							this._id = Guid.NewGuid();
						}
					}
				}
				return this._id;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00040E58 File Offset: 0x0003F058
		public static WebApplicationInformation ApplicationInformation
		{
			get
			{
				return WebBaseEvent.s_applicationInfo;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00040E60 File Offset: 0x0003F060
		internal virtual void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_code", this.EventCode.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_message", this.Message));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_time", this.EventTime.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_time_Utc", this.EventTimeUtc.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_id", this.EventID.ToString("N", CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_sequence", this.EventSequence.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_occurrence", this.EventOccurrence.ToString(CultureInfo.InstalledUICulture)));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_detail_code", this.EventDetailCode.ToString(CultureInfo.InstalledUICulture)));
			if (includeAppInfo)
			{
				formatter.AppendLine(string.Empty);
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_application_information"));
				formatter.IndentationLevel++;
				WebBaseEvent.ApplicationInformation.FormatToString(formatter);
				formatter.IndentationLevel--;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00040FC2 File Offset: 0x0003F1C2
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00040FCC File Offset: 0x0003F1CC
		public virtual string ToString(bool includeAppInfo, bool includeCustomEventDetails)
		{
			WebEventFormatter webEventFormatter = new WebEventFormatter();
			this.FormatToString(webEventFormatter, includeAppInfo);
			if (!this.IsSystemEvent && includeCustomEventDetails)
			{
				webEventFormatter.AppendLine(string.Empty);
				webEventFormatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_custom_event_details"));
				webEventFormatter.IndentationLevel++;
				this.FormatCustomEventDetails(webEventFormatter);
				webEventFormatter.IndentationLevel--;
			}
			return webEventFormatter.ToString();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void FormatCustomEventDetails(WebEventFormatter formatter)
		{
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00041038 File Offset: 0x0003F238
		internal int InferEtwTraceVerbosity()
		{
			switch (WebBaseEvent.WebEventTypeFromWebEvent(this))
			{
			case WebEventType.WEBEVENT_BASE_ERROR_EVENT:
			case WebEventType.WEBEVENT_REQUEST_ERROR_EVENT:
			case WebEventType.WEBEVENT_ERROR_EVENT:
			case WebEventType.WEBEVENT_FAILURE_AUDIT_EVENT:
			case WebEventType.WEBEVENT_AUTHENTICATION_FAILURE_AUDIT_EVENT:
			case WebEventType.WEBEVENT_VIEWSTATE_FAILURE_AUDIT_EVENT:
				return 3;
			case WebEventType.WEBEVENT_AUDIT_EVENT:
			case WebEventType.WEBEVENT_SUCCESS_AUDIT_EVENT:
			case WebEventType.WEBEVENT_AUTHENTICATION_SUCCESS_AUDIT_EVENT:
				return 4;
			}
			return 5;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00041094 File Offset: 0x0003F294
		internal void DeconstructWebEvent(out int eventType, out int fieldCount, out string[] fieldNames, out int[] fieldTypes, out string[] fieldData)
		{
			List<WebEventFieldData> list = new List<WebEventFieldData>();
			eventType = (int)WebBaseEvent.WebEventTypeFromWebEvent(this);
			this.GenerateFieldsForMarshal(list);
			fieldCount = list.Count;
			fieldNames = new string[fieldCount];
			fieldData = new string[fieldCount];
			fieldTypes = new int[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				fieldNames[i] = list[i].Name;
				fieldData[i] = list[i].Data;
				fieldTypes[i] = (int)list[i].Type;
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0004111C File Offset: 0x0003F31C
		internal virtual void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			fields.Add(new WebEventFieldData("EventTime", this.EventTimeUtc.ToString(), WebEventFieldType.String));
			fields.Add(new WebEventFieldData("EventID", this.EventID.ToString(), WebEventFieldType.String));
			fields.Add(new WebEventFieldData("EventMessage", this.Message, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ApplicationDomain", WebBaseEvent.ApplicationInformation.ApplicationDomain, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("TrustLevel", WebBaseEvent.ApplicationInformation.TrustLevel, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ApplicationVirtualPath", WebBaseEvent.ApplicationInformation.ApplicationVirtualPath, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ApplicationPath", WebBaseEvent.ApplicationInformation.ApplicationPath, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("MachineName", WebBaseEvent.ApplicationInformation.MachineName, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("EventCode", this.EventCode.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
			fields.Add(new WebEventFieldData("EventDetailCode", this.EventDetailCode.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
			fields.Add(new WebEventFieldData("SequenceNumber", this.EventSequence.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Long));
			fields.Add(new WebEventFieldData("Occurrence", this.EventOccurrence.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Long));
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void PreProcessEventInit()
		{
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004129C File Offset: 0x0003F49C
		private static void FindEventCode(Exception e, ref int eventCode, ref int eventDetailsCode, ref Exception eStack)
		{
			eventDetailsCode = 0;
			if (e is ConfigurationException)
			{
				eventCode = 3008;
			}
			else if (e is HttpRequestValidationException)
			{
				eventCode = 3003;
			}
			else if (e is HttpCompileException)
			{
				eventCode = 3007;
			}
			else if (e is SecurityException)
			{
				eventCode = 4010;
			}
			else if (e is UnauthorizedAccessException)
			{
				eventCode = 4011;
			}
			else if (e is HttpParseException)
			{
				eventCode = 3006;
			}
			else if (e is HttpException && e.InnerException is ViewStateException)
			{
				ViewStateException ex = (ViewStateException)e.InnerException;
				eventCode = 4009;
				if (ex._macValidationError)
				{
					eventDetailsCode = 50203;
				}
				else
				{
					eventDetailsCode = 50204;
				}
				eStack = ex;
			}
			else if (e is HttpException && ((HttpException)e).WebEventCode != 0)
			{
				eventCode = ((HttpException)e).WebEventCode;
			}
			else if (e.InnerException != null)
			{
				if (eStack == null)
				{
					eStack = e.InnerException;
				}
				WebBaseEvent.FindEventCode(e.InnerException, ref eventCode, ref eventDetailsCode, ref eStack);
			}
			else
			{
				eventCode = 3005;
			}
			if (eStack == null)
			{
				eStack = e;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000413C0 File Offset: 0x0003F5C0
		internal static void RaiseRuntimeError(Exception e, object source)
		{
			if (!HealthMonitoringManager.Enabled)
			{
				return;
			}
			try
			{
				int eventCode = 0;
				int eventDetailCode = 0;
				HttpContext httpContext = HttpContext.Current;
				Exception exception = null;
				if (httpContext != null)
				{
					Page page = httpContext.Handler as Page;
					if (page != null && page.IsTransacted && e.GetType() == typeof(HttpException) && e.InnerException != null)
					{
						e = e.InnerException;
					}
				}
				WebBaseEvent.FindEventCode(e, ref eventCode, ref eventDetailCode, ref exception);
				WebBaseEvent.RaiseSystemEvent(source, eventCode, eventDetailCode, exception);
			}
			catch
			{
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00041450 File Offset: 0x0003F650
		protected internal virtual void IncrementPerfCounters()
		{
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_TOTAL);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0004145C File Offset: 0x0003F65C
		internal void IncrementTotalCounters(int index0, int index1)
		{
			this._sequenceNumber = Interlocked.Increment(ref WebBaseEvent.s_globalSequenceNumber);
			if (index0 != -1)
			{
				this._occurrenceNumber = Interlocked.Increment(ref WebBaseEvent.s_eventCodeOccurrence[index0, index1]);
				return;
			}
			WebBaseEvent.CustomEventCodeOccurrence customEventCodeOccurrence = (WebBaseEvent.CustomEventCodeOccurrence)WebBaseEvent.s_customEventCodeOccurrence[this._code];
			if (customEventCodeOccurrence == null)
			{
				WebBaseEvent.s_lockCustomEventCodeOccurrence.AcquireWriterLock();
				try
				{
					customEventCodeOccurrence = (WebBaseEvent.CustomEventCodeOccurrence)WebBaseEvent.s_customEventCodeOccurrence[this._code];
					if (customEventCodeOccurrence == null)
					{
						customEventCodeOccurrence = new WebBaseEvent.CustomEventCodeOccurrence();
						WebBaseEvent.s_customEventCodeOccurrence[this._code] = customEventCodeOccurrence;
					}
				}
				finally
				{
					WebBaseEvent.s_lockCustomEventCodeOccurrence.ReleaseWriterLock();
				}
			}
			this._occurrenceNumber = Interlocked.Increment(ref customEventCodeOccurrence._occurrence);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00041528 File Offset: 0x0003F728
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public virtual void Raise()
		{
			WebBaseEvent.Raise(this);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00041530 File Offset: 0x0003F730
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public static void Raise(WebBaseEvent eventRaised)
		{
			if (eventRaised.EventCode < 100000)
			{
				throw new HttpException(SR.GetString("System_eventCode_not_allowed", new object[]
				{
					eventRaised.EventCode.ToString(CultureInfo.CurrentCulture),
					100000.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!HealthMonitoringManager.Enabled)
			{
				return;
			}
			WebBaseEvent.RaiseInternal(eventRaised, null, -1, -1);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0004159C File Offset: 0x0003F79C
		internal static void RaiseInternal(WebBaseEvent eventRaised, ArrayList firingRuleInfos, int index0, int index1)
		{
			bool flag = false;
			bool flag2 = false;
			ProcessImpersonationContext processImpersonationContext = null;
			HttpContext httpContext = HttpContext.Current;
			object data = CallContext.GetData("_WEvtRIP");
			if (data != null && (bool)data)
			{
				return;
			}
			eventRaised.IncrementPerfCounters();
			eventRaised.IncrementTotalCounters(index0, index1);
			if (firingRuleInfos == null)
			{
				HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
				firingRuleInfos = healthMonitoringManager._sectionHelper.FindFiringRuleInfos(eventRaised.GetType(), eventRaised.EventCode);
			}
			if (firingRuleInfos.Count == 0)
			{
				return;
			}
			try
			{
				bool[] array = null;
				if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_WEB_EVENT_RAISE_START, httpContext.WorkerRequest, eventRaised.GetType().FullName, eventRaised.EventCode.ToString(CultureInfo.InstalledUICulture), eventRaised.EventDetailCode.ToString(CultureInfo.InstalledUICulture), null);
				}
				try
				{
					foreach (object obj in firingRuleInfos)
					{
						HealthMonitoringSectionHelper.FiringRuleInfo firingRuleInfo = (HealthMonitoringSectionHelper.FiringRuleInfo)obj;
						HealthMonitoringSectionHelper.RuleInfo ruleInfo = firingRuleInfo._ruleInfo;
						RuleFiringRecord ruleFiringRecord = ruleInfo._ruleFiringRecord;
						if (ruleFiringRecord.CheckAndUpdate(eventRaised) && ruleInfo._referencedProvider != null)
						{
							if (!flag)
							{
								eventRaised.PreProcessEventInit();
								flag = true;
							}
							if (firingRuleInfo._indexOfFirstRuleInfoWithSameProvider != -1)
							{
								if (array == null)
								{
									array = new bool[firingRuleInfos.Count];
								}
								if (array[firingRuleInfo._indexOfFirstRuleInfoWithSameProvider])
								{
									continue;
								}
								array[firingRuleInfo._indexOfFirstRuleInfoWithSameProvider] = true;
							}
							if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
							{
								EtwTrace.Trace(EtwTraceType.ETW_TYPE_WEB_EVENT_DELIVER_START, httpContext.WorkerRequest, ruleInfo._ruleSettings.Provider, ruleInfo._ruleSettings.Name, ruleInfo._ruleSettings.EventName, null);
							}
							try
							{
								if (processImpersonationContext == null)
								{
									processImpersonationContext = new ProcessImpersonationContext();
								}
								if (!flag2)
								{
									CallContext.SetData("_WEvtRIP", true);
									flag2 = true;
								}
								ruleInfo._referencedProvider.ProcessEvent(eventRaised);
							}
							catch (Exception e)
							{
								try
								{
									ruleInfo._referencedProvider.LogException(e);
								}
								catch
								{
								}
							}
							finally
							{
								if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
								{
									EtwTrace.Trace(EtwTraceType.ETW_TYPE_WEB_EVENT_DELIVER_END, httpContext.WorkerRequest);
								}
							}
						}
					}
				}
				finally
				{
					if (processImpersonationContext != null)
					{
						processImpersonationContext.Undo();
					}
					if (flag2)
					{
						CallContext.FreeNamedDataSlot("_WEvtRIP");
					}
					if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_WEB_EVENT_RAISE_END, httpContext.WorkerRequest);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00041878 File Offset: 0x0003FA78
		internal static void RaiseSystemEvent(string message, object source, int eventCode, int eventDetailCode, Exception exception)
		{
			WebBaseEvent.RaiseSystemEventInternal(message, source, eventCode, eventDetailCode, exception, null);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00041886 File Offset: 0x0003FA86
		internal static void RaiseSystemEvent(object source, int eventCode)
		{
			WebBaseEvent.RaiseSystemEventInternal(null, source, eventCode, 0, null, null);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00041893 File Offset: 0x0003FA93
		internal static void RaiseSystemEvent(object source, int eventCode, int eventDetailCode)
		{
			WebBaseEvent.RaiseSystemEventInternal(null, source, eventCode, eventDetailCode, null, null);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000418A0 File Offset: 0x0003FAA0
		internal static void RaiseSystemEvent(object source, int eventCode, int eventDetailCode, Exception exception)
		{
			WebBaseEvent.RaiseSystemEventInternal(null, source, eventCode, eventDetailCode, exception, null);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000418AD File Offset: 0x0003FAAD
		internal static void RaiseSystemEvent(object source, int eventCode, string nameToAuthenticate)
		{
			WebBaseEvent.RaiseSystemEventInternal(null, source, eventCode, 0, null, nameToAuthenticate);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000418BC File Offset: 0x0003FABC
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static void RaiseSystemEventInternal(string message, object source, int eventCode, int eventDetailCode, Exception exception, string nameToAuthenticate)
		{
			if (!HealthMonitoringManager.Enabled)
			{
				return;
			}
			int index;
			int index2;
			WebEventCodes.GetEventArrayIndexsFromEventCode(eventCode, out index, out index2);
			WebBaseEvent.SystemEventTypeInfo systemEventTypeInfo;
			WebBaseEvent.SystemEventType systemEventType;
			WebBaseEvent.GetSystemEventTypeInfo(eventCode, index, index2, out systemEventTypeInfo, out systemEventType);
			if (systemEventTypeInfo == null)
			{
				return;
			}
			HealthMonitoringManager healthMonitoringManager = HealthMonitoringManager.Manager();
			ArrayList arrayList = healthMonitoringManager._sectionHelper.FindFiringRuleInfos(systemEventTypeInfo._type, eventCode);
			if (arrayList.Count == 0)
			{
				systemEventTypeInfo._dummyEvent.IncrementPerfCounters();
				systemEventTypeInfo._dummyEvent.IncrementTotalCounters(index, index2);
				return;
			}
			WebBaseEvent.RaiseInternal(WebBaseEvent.NewEventFromSystemEventType(false, systemEventType, message, source, eventCode, eventDetailCode, exception, nameToAuthenticate), arrayList, index, index2);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00041944 File Offset: 0x0003FB44
		private static void GetSystemEventTypeInfo(int eventCode, int index0, int index1, out WebBaseEvent.SystemEventTypeInfo info, out WebBaseEvent.SystemEventType systemEventType)
		{
			systemEventType = WebBaseEvent.s_eventCodeToSystemEventTypeMappings[index0, index1];
			if (systemEventType == WebBaseEvent.SystemEventType.Unknown)
			{
				systemEventType = WebBaseEvent.SystemEventTypeFromEventCode(eventCode);
				WebBaseEvent.s_eventCodeToSystemEventTypeMappings[index0, index1] = systemEventType;
			}
			info = WebBaseEvent.s_systemEventTypeInfos[(int)systemEventType];
			if (info != null)
			{
				return;
			}
			info = new WebBaseEvent.SystemEventTypeInfo(WebBaseEvent.CreateDummySystemEvent(systemEventType));
			WebBaseEvent.s_systemEventTypeInfos[(int)systemEventType] = info;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000419A8 File Offset: 0x0003FBA8
		private static WebBaseEvent.SystemEventType SystemEventTypeFromEventCode(int eventCode)
		{
			if (eventCode >= 1000 && eventCode <= 1005)
			{
				if (eventCode - 1001 <= 3)
				{
					return WebBaseEvent.SystemEventType.WebApplicationLifetimeEvent;
				}
				if (eventCode == 1005)
				{
					return WebBaseEvent.SystemEventType.WebHeartbeatEvent;
				}
			}
			if (eventCode >= 2000 && eventCode <= 2002 && eventCode - 2001 <= 1)
			{
				return WebBaseEvent.SystemEventType.WebRequestEvent;
			}
			if (eventCode >= 3000 && eventCode <= 3012)
			{
				switch (eventCode)
				{
				case 3001:
				case 3002:
				case 3003:
				case 3004:
				case 3005:
				case 3012:
					return WebBaseEvent.SystemEventType.WebRequestErrorEvent;
				case 3006:
				case 3007:
				case 3008:
				case 3009:
				case 3010:
				case 3011:
					return WebBaseEvent.SystemEventType.WebErrorEvent;
				}
			}
			if (eventCode >= 4000 && eventCode <= 4011)
			{
				switch (eventCode)
				{
				case 4001:
				case 4002:
					return WebBaseEvent.SystemEventType.WebAuthenticationSuccessAuditEvent;
				case 4003:
				case 4004:
					return WebBaseEvent.SystemEventType.WebSuccessAuditEvent;
				case 4005:
				case 4006:
					return WebBaseEvent.SystemEventType.WebAuthenticationFailureAuditEvent;
				case 4007:
				case 4008:
				case 4010:
				case 4011:
					return WebBaseEvent.SystemEventType.WebFailureAuditEvent;
				case 4009:
					return WebBaseEvent.SystemEventType.WebViewStateFailureAuditEvent;
				}
			}
			if (eventCode >= 6000 && eventCode <= 6001)
			{
				return WebBaseEvent.SystemEventType.Unknown;
			}
			return WebBaseEvent.SystemEventType.Unknown;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00041ABB File Offset: 0x0003FCBB
		private static WebBaseEvent CreateDummySystemEvent(WebBaseEvent.SystemEventType systemEventType)
		{
			return WebBaseEvent.NewEventFromSystemEventType(true, systemEventType, null, null, 0, 0, null, null);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00041ACC File Offset: 0x0003FCCC
		private static WebBaseEvent NewEventFromSystemEventType(bool createDummy, WebBaseEvent.SystemEventType systemEventType, string message, object source, int eventCode, int eventDetailCode, Exception exception, string nameToAuthenticate)
		{
			if (!createDummy && message == null)
			{
				message = WebEventCodes.MessageFromEventCode(eventCode, eventDetailCode);
			}
			switch (systemEventType)
			{
			case WebBaseEvent.SystemEventType.WebApplicationLifetimeEvent:
				if (!createDummy)
				{
					return new WebApplicationLifetimeEvent(message, source, eventCode, eventDetailCode);
				}
				return new WebApplicationLifetimeEvent();
			case WebBaseEvent.SystemEventType.WebHeartbeatEvent:
				if (!createDummy)
				{
					return new WebHeartbeatEvent(message, eventCode);
				}
				return new WebHeartbeatEvent();
			case WebBaseEvent.SystemEventType.WebRequestEvent:
				if (!createDummy)
				{
					return new WebRequestEvent(message, source, eventCode, eventDetailCode);
				}
				return new WebRequestEvent();
			case WebBaseEvent.SystemEventType.WebRequestErrorEvent:
				if (!createDummy)
				{
					return new WebRequestErrorEvent(message, source, eventCode, eventDetailCode, exception);
				}
				return new WebRequestErrorEvent();
			case WebBaseEvent.SystemEventType.WebErrorEvent:
				if (!createDummy)
				{
					return new WebErrorEvent(message, source, eventCode, eventDetailCode, exception);
				}
				return new WebErrorEvent();
			case WebBaseEvent.SystemEventType.WebAuthenticationSuccessAuditEvent:
				if (!createDummy)
				{
					return new WebAuthenticationSuccessAuditEvent(message, source, eventCode, eventDetailCode, nameToAuthenticate);
				}
				return new WebAuthenticationSuccessAuditEvent();
			case WebBaseEvent.SystemEventType.WebSuccessAuditEvent:
				if (!createDummy)
				{
					return new WebSuccessAuditEvent(message, source, eventCode, eventDetailCode);
				}
				return new WebSuccessAuditEvent();
			case WebBaseEvent.SystemEventType.WebAuthenticationFailureAuditEvent:
				if (!createDummy)
				{
					return new WebAuthenticationFailureAuditEvent(message, source, eventCode, eventDetailCode, nameToAuthenticate);
				}
				return new WebAuthenticationFailureAuditEvent();
			case WebBaseEvent.SystemEventType.WebFailureAuditEvent:
				if (!createDummy)
				{
					return new WebFailureAuditEvent(message, source, eventCode, eventDetailCode);
				}
				return new WebFailureAuditEvent();
			case WebBaseEvent.SystemEventType.WebViewStateFailureAuditEvent:
				if (!createDummy)
				{
					return new WebViewStateFailureAuditEvent(message, source, eventCode, eventDetailCode, (ViewStateException)exception);
				}
				return new WebViewStateFailureAuditEvent();
			default:
				return null;
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00041BFC File Offset: 0x0003FDFC
		private static string CreateWebEventResourceCacheKey(string key)
		{
			return "x" + key;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00041C0C File Offset: 0x0003FE0C
		internal static string FormatResourceStringWithCache(string key)
		{
			if (HealthMonitoringManager.IsCacheDisposed)
			{
				return SR.Resources.GetString(key, CultureInfo.InstalledUICulture);
			}
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key2 = WebBaseEvent.CreateWebEventResourceCacheKey(key);
			string text = (string)internalCache.Get(key2);
			if (text != null)
			{
				return text;
			}
			text = SR.Resources.GetString(key, CultureInfo.InstalledUICulture);
			if (text != null)
			{
				internalCache.Insert(key2, text, null);
			}
			return text;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00041C74 File Offset: 0x0003FE74
		internal static string FormatResourceStringWithCache(string key, string arg0)
		{
			string text = WebBaseEvent.FormatResourceStringWithCache(key);
			if (text == null)
			{
				return null;
			}
			return string.Format(text, arg0);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00041C94 File Offset: 0x0003FE94
		internal static WebEventType WebEventTypeFromWebEvent(WebBaseEvent eventRaised)
		{
			if (eventRaised is WebAuthenticationSuccessAuditEvent)
			{
				return WebEventType.WEBEVENT_AUTHENTICATION_SUCCESS_AUDIT_EVENT;
			}
			if (eventRaised is WebAuthenticationFailureAuditEvent)
			{
				return WebEventType.WEBEVENT_AUTHENTICATION_FAILURE_AUDIT_EVENT;
			}
			if (eventRaised is WebViewStateFailureAuditEvent)
			{
				return WebEventType.WEBEVENT_VIEWSTATE_FAILURE_AUDIT_EVENT;
			}
			if (eventRaised is WebRequestErrorEvent)
			{
				return WebEventType.WEBEVENT_REQUEST_ERROR_EVENT;
			}
			if (eventRaised is WebErrorEvent)
			{
				return WebEventType.WEBEVENT_ERROR_EVENT;
			}
			if (eventRaised is WebSuccessAuditEvent)
			{
				return WebEventType.WEBEVENT_SUCCESS_AUDIT_EVENT;
			}
			if (eventRaised is WebFailureAuditEvent)
			{
				return WebEventType.WEBEVENT_FAILURE_AUDIT_EVENT;
			}
			if (eventRaised is WebHeartbeatEvent)
			{
				return WebEventType.WEBEVENT_HEARTBEAT_EVENT;
			}
			if (eventRaised is WebApplicationLifetimeEvent)
			{
				return WebEventType.WEBEVENT_APP_LIFETIME_EVENT;
			}
			if (eventRaised is WebRequestEvent)
			{
				return WebEventType.WEBEVENT_REQUEST_EVENT;
			}
			if (eventRaised is WebBaseErrorEvent)
			{
				return WebEventType.WEBEVENT_BASE_ERROR_EVENT;
			}
			if (eventRaised is WebAuditEvent)
			{
				return WebEventType.WEBEVENT_AUDIT_EVENT;
			}
			if (eventRaised is WebManagementEvent)
			{
				return WebEventType.WEBEVENT_MANAGEMENT_EVENT;
			}
			return WebEventType.WEBEVENT_BASE_EVENT;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00041D2C File Offset: 0x0003FF2C
		internal static void RaisePropertyDeserializationWebErrorEvent(SettingsProperty property, object source, Exception exception)
		{
			if (HttpContext.Current == null)
			{
				return;
			}
			WebBaseEvent.RaiseSystemEvent(SR.GetString("Webevent_msg_Property_Deserialization", new object[]
			{
				property.Name,
				property.SerializeAs.ToString(),
				property.PropertyType.AssemblyQualifiedName
			}), source, 3010, 0, exception);
		}

		// Token: 0x0400162A RID: 5674
		private DateTime _eventTimeUtc;

		// Token: 0x0400162B RID: 5675
		private int _code;

		// Token: 0x0400162C RID: 5676
		private int _detailCode;

		// Token: 0x0400162D RID: 5677
		private object _source;

		// Token: 0x0400162E RID: 5678
		private string _message;

		// Token: 0x0400162F RID: 5679
		private long _sequenceNumber;

		// Token: 0x04001630 RID: 5680
		private long _occurrenceNumber;

		// Token: 0x04001631 RID: 5681
		private Guid _id = Guid.Empty;

		// Token: 0x04001632 RID: 5682
		private static long s_globalSequenceNumber = 0L;

		// Token: 0x04001633 RID: 5683
		private static WebApplicationInformation s_applicationInfo = new WebApplicationInformation();

		// Token: 0x04001634 RID: 5684
		private const string WEBEVENT_RAISE_IN_PROGRESS = "_WEvtRIP";

		// Token: 0x04001635 RID: 5685
		private static readonly WebBaseEvent.SystemEventType[,] s_eventCodeToSystemEventTypeMappings = new WebBaseEvent.SystemEventType[WebEventCodes.GetEventArrayDimensionSize(0), WebEventCodes.GetEventArrayDimensionSize(1)];

		// Token: 0x04001636 RID: 5686
		private static readonly long[,] s_eventCodeOccurrence = new long[WebEventCodes.GetEventArrayDimensionSize(0), WebEventCodes.GetEventArrayDimensionSize(1)];

		// Token: 0x04001637 RID: 5687
		private static Hashtable s_customEventCodeOccurrence = new Hashtable();

		// Token: 0x04001638 RID: 5688
		private static ReadWriteSpinLock s_lockCustomEventCodeOccurrence;

		// Token: 0x04001639 RID: 5689
		private static WebBaseEvent.SystemEventTypeInfo[] s_systemEventTypeInfos = new WebBaseEvent.SystemEventTypeInfo[10];

		// Token: 0x02000910 RID: 2320
		private class CustomEventCodeOccurrence
		{
			// Token: 0x04003722 RID: 14114
			internal long _occurrence;
		}

		// Token: 0x02000911 RID: 2321
		private enum SystemEventType
		{
			// Token: 0x04003724 RID: 14116
			Unknown = -1,
			// Token: 0x04003725 RID: 14117
			WebApplicationLifetimeEvent,
			// Token: 0x04003726 RID: 14118
			WebHeartbeatEvent,
			// Token: 0x04003727 RID: 14119
			WebRequestEvent,
			// Token: 0x04003728 RID: 14120
			WebRequestErrorEvent,
			// Token: 0x04003729 RID: 14121
			WebErrorEvent,
			// Token: 0x0400372A RID: 14122
			WebAuthenticationSuccessAuditEvent,
			// Token: 0x0400372B RID: 14123
			WebSuccessAuditEvent,
			// Token: 0x0400372C RID: 14124
			WebAuthenticationFailureAuditEvent,
			// Token: 0x0400372D RID: 14125
			WebFailureAuditEvent,
			// Token: 0x0400372E RID: 14126
			WebViewStateFailureAuditEvent,
			// Token: 0x0400372F RID: 14127
			Last
		}

		// Token: 0x02000912 RID: 2322
		private class SystemEventTypeInfo
		{
			// Token: 0x060068FD RID: 26877 RVA: 0x00175FAD File Offset: 0x001741AD
			internal SystemEventTypeInfo(WebBaseEvent dummyEvent)
			{
				this._dummyEvent = dummyEvent;
				this._type = dummyEvent.GetType();
			}

			// Token: 0x04003730 RID: 14128
			internal WebBaseEvent _dummyEvent;

			// Token: 0x04003731 RID: 14129
			internal Type _type;
		}
	}
}
