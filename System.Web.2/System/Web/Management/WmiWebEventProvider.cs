using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x020001A3 RID: 419
	public class WmiWebEventProvider : WebEventProvider
	{
		// Token: 0x060015FF RID: 5631 RVA: 0x00043C74 File Offset: 0x00041E74
		public override void Initialize(string name, NameValueCollection config)
		{
			int num = UnsafeNativeMethods.InitializeWmiManager();
			if (num != 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Wmi_provider_cant_initialize", new object[]
				{
					"0x" + num.ToString("X8", CultureInfo.CurrentCulture)
				}));
			}
			base.Initialize(name, config);
			ProviderUtil.CheckUnrecognizedAttributes(config, name);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00043CD0 File Offset: 0x00041ED0
		private string WmiFormatTime(DateTime dt)
		{
			StringBuilder stringBuilder = new StringBuilder(26);
			stringBuilder.Append(dt.ToString("yyyyMMddHHmmss.ffffff", CultureInfo.InstalledUICulture));
			double totalMinutes = TimeZone.CurrentTimeZone.GetUtcOffset(dt).TotalMinutes;
			if (totalMinutes >= 0.0)
			{
				stringBuilder.Append('+');
			}
			stringBuilder.Append(totalMinutes);
			return stringBuilder.ToString();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00043D34 File Offset: 0x00041F34
		private void FillBasicWmiDataFields(ref UnsafeNativeMethods.WmiData wmiData, WebBaseEvent eventRaised)
		{
			WebApplicationInformation applicationInformation = WebBaseEvent.ApplicationInformation;
			wmiData.eventType = (int)WebBaseEvent.WebEventTypeFromWebEvent(eventRaised);
			wmiData.eventCode = eventRaised.EventCode;
			wmiData.eventDetailCode = eventRaised.EventDetailCode;
			wmiData.eventTime = this.WmiFormatTime(eventRaised.EventTime);
			wmiData.eventMessage = eventRaised.Message;
			wmiData.sequenceNumber = eventRaised.EventSequence.ToString(CultureInfo.InstalledUICulture);
			wmiData.occurrence = eventRaised.EventOccurrence.ToString(CultureInfo.InstalledUICulture);
			wmiData.eventId = eventRaised.EventID.ToString("N", CultureInfo.InstalledUICulture);
			wmiData.appDomain = applicationInformation.ApplicationDomain;
			wmiData.trustLevel = applicationInformation.TrustLevel;
			wmiData.appVirtualPath = applicationInformation.ApplicationVirtualPath;
			wmiData.appPath = applicationInformation.ApplicationPath;
			wmiData.machineName = applicationInformation.MachineName;
			if (eventRaised.IsSystemEvent)
			{
				wmiData.details = string.Empty;
				return;
			}
			WebEventFormatter webEventFormatter = new WebEventFormatter();
			eventRaised.FormatCustomEventDetails(webEventFormatter);
			wmiData.details = webEventFormatter.ToString();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00043E44 File Offset: 0x00042044
		private void FillRequestWmiDataFields(ref UnsafeNativeMethods.WmiData wmiData, WebRequestInformation reqInfo)
		{
			IPrincipal principal = reqInfo.Principal;
			string userName;
			string userAuthenticationType;
			bool userAuthenticated;
			if (principal == null)
			{
				userName = string.Empty;
				userAuthenticationType = string.Empty;
				userAuthenticated = false;
			}
			else
			{
				IIdentity identity = principal.Identity;
				userName = identity.Name;
				userAuthenticated = identity.IsAuthenticated;
				userAuthenticationType = identity.AuthenticationType;
			}
			wmiData.requestUrl = reqInfo.RequestUrl;
			wmiData.requestPath = reqInfo.RequestPath;
			wmiData.userHostAddress = reqInfo.UserHostAddress;
			wmiData.userName = userName;
			wmiData.userAuthenticated = userAuthenticated;
			wmiData.userAuthenticationType = userAuthenticationType;
			wmiData.requestThreadAccountName = reqInfo.ThreadAccountName;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00043ED0 File Offset: 0x000420D0
		private void FillErrorWmiDataFields(ref UnsafeNativeMethods.WmiData wmiData, WebThreadInformation threadInfo)
		{
			wmiData.threadId = threadInfo.ThreadID;
			wmiData.threadAccountName = threadInfo.ThreadAccountName;
			wmiData.stackTrace = threadInfo.StackTrace;
			wmiData.isImpersonating = threadInfo.IsImpersonating;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00043F04 File Offset: 0x00042104
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			UnsafeNativeMethods.WmiData wmiData = default(UnsafeNativeMethods.WmiData);
			this.FillBasicWmiDataFields(ref wmiData, eventRaised);
			WebApplicationLifetimeEvent webApplicationLifetimeEvent = eventRaised as WebApplicationLifetimeEvent;
			if (eventRaised is WebManagementEvent)
			{
				WebProcessInformation processInformation = ((WebManagementEvent)eventRaised).ProcessInformation;
				wmiData.processId = processInformation.ProcessID;
				wmiData.processName = processInformation.ProcessName;
				wmiData.accountName = processInformation.AccountName;
			}
			if (eventRaised is WebRequestEvent)
			{
				this.FillRequestWmiDataFields(ref wmiData, ((WebRequestEvent)eventRaised).RequestInformation);
			}
			if (eventRaised is WebAuditEvent)
			{
				this.FillRequestWmiDataFields(ref wmiData, ((WebAuditEvent)eventRaised).RequestInformation);
			}
			if (eventRaised is WebAuthenticationSuccessAuditEvent)
			{
				wmiData.nameToAuthenticate = ((WebAuthenticationSuccessAuditEvent)eventRaised).NameToAuthenticate;
			}
			if (eventRaised is WebAuthenticationFailureAuditEvent)
			{
				wmiData.nameToAuthenticate = ((WebAuthenticationFailureAuditEvent)eventRaised).NameToAuthenticate;
			}
			if (eventRaised is WebViewStateFailureAuditEvent)
			{
				ViewStateException viewStateException = ((WebViewStateFailureAuditEvent)eventRaised).ViewStateException;
				wmiData.exceptionMessage = SR.GetString(viewStateException.ShortMessage);
				wmiData.remoteAddress = viewStateException.RemoteAddress;
				wmiData.remotePort = viewStateException.RemotePort;
				wmiData.userAgent = viewStateException.UserAgent;
				wmiData.persistedState = viewStateException.PersistedState;
				wmiData.referer = viewStateException.Referer;
				wmiData.path = viewStateException.Path;
			}
			if (eventRaised is WebHeartbeatEvent)
			{
				WebHeartbeatEvent webHeartbeatEvent = eventRaised as WebHeartbeatEvent;
				WebProcessStatistics processStatistics = webHeartbeatEvent.ProcessStatistics;
				wmiData.processStartTime = this.WmiFormatTime(processStatistics.ProcessStartTime);
				wmiData.threadCount = processStatistics.ThreadCount;
				wmiData.workingSet = processStatistics.WorkingSet.ToString(CultureInfo.InstalledUICulture);
				wmiData.peakWorkingSet = processStatistics.PeakWorkingSet.ToString(CultureInfo.InstalledUICulture);
				wmiData.managedHeapSize = processStatistics.ManagedHeapSize.ToString(CultureInfo.InstalledUICulture);
				wmiData.appdomainCount = processStatistics.AppDomainCount;
				wmiData.requestsExecuting = processStatistics.RequestsExecuting;
				wmiData.requestsQueued = processStatistics.RequestsQueued;
				wmiData.requestsRejected = processStatistics.RequestsRejected;
			}
			if (eventRaised is WebBaseErrorEvent)
			{
				Exception errorException = ((WebBaseErrorEvent)eventRaised).ErrorException;
				if (errorException == null)
				{
					wmiData.exceptionType = string.Empty;
					wmiData.exceptionMessage = string.Empty;
				}
				else
				{
					wmiData.exceptionType = errorException.GetType().Name;
					wmiData.exceptionMessage = errorException.Message;
				}
			}
			if (eventRaised is WebRequestErrorEvent)
			{
				WebRequestErrorEvent webRequestErrorEvent = eventRaised as WebRequestErrorEvent;
				WebRequestInformation requestInformation = webRequestErrorEvent.RequestInformation;
				WebThreadInformation threadInformation = webRequestErrorEvent.ThreadInformation;
				this.FillRequestWmiDataFields(ref wmiData, requestInformation);
				this.FillErrorWmiDataFields(ref wmiData, threadInformation);
			}
			if (eventRaised is WebErrorEvent)
			{
				WebErrorEvent webErrorEvent = eventRaised as WebErrorEvent;
				WebRequestInformation requestInformation2 = webErrorEvent.RequestInformation;
				WebThreadInformation threadInformation2 = webErrorEvent.ThreadInformation;
				this.FillRequestWmiDataFields(ref wmiData, requestInformation2);
				this.FillErrorWmiDataFields(ref wmiData, threadInformation2);
			}
			int num = UnsafeNativeMethods.RaiseWmiEvent(ref wmiData, AspCompatApplicationStep.IsInAspCompatMode);
			if (num != 0)
			{
				throw new HttpException(SR.GetString("Wmi_provider_error", new object[]
				{
					"0x" + num.ToString("X8", CultureInfo.InstalledUICulture)
				}));
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00006164 File Offset: 0x00004364
		public override void Shutdown()
		{
		}
	}
}
