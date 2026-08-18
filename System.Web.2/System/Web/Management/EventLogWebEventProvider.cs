using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x0200016E RID: 366
	public sealed class EventLogWebEventProvider : WebEventProvider, IInternalWebEventProvider
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0003C6E9 File Offset: 0x0003A8E9
		internal EventLogWebEventProvider()
		{
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003C6F1 File Offset: 0x0003A8F1
		public override void Initialize(string name, NameValueCollection config)
		{
			this._maxTruncatedParamLen = 30718 - "...".Length;
			base.Initialize(name, config);
			ProviderUtil.CheckUnrecognizedAttributes(config, name);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003C718 File Offset: 0x0003A918
		private void AddBasicDataFields(ArrayList dataFields, WebBaseEvent eventRaised)
		{
			WebApplicationInformation applicationInformation = WebBaseEvent.ApplicationInformation;
			dataFields.Add(eventRaised.EventCode.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(eventRaised.Message);
			dataFields.Add(eventRaised.EventTime.ToString());
			dataFields.Add(eventRaised.EventTimeUtc.ToString());
			dataFields.Add(eventRaised.EventID.ToString("N", CultureInfo.InstalledUICulture));
			dataFields.Add(eventRaised.EventSequence.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(eventRaised.EventOccurrence.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(eventRaised.EventDetailCode.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(applicationInformation.ApplicationDomain);
			dataFields.Add(applicationInformation.TrustLevel);
			dataFields.Add(applicationInformation.ApplicationVirtualPath);
			dataFields.Add(applicationInformation.ApplicationPath);
			dataFields.Add(applicationInformation.MachineName);
			if (eventRaised.IsSystemEvent)
			{
				dataFields.Add(null);
				return;
			}
			WebEventFormatter webEventFormatter = new WebEventFormatter();
			eventRaised.FormatCustomEventDetails(webEventFormatter);
			dataFields.Add(webEventFormatter.ToString());
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003C85C File Offset: 0x0003AA5C
		private void AddWebProcessInformationDataFields(ArrayList dataFields, WebProcessInformation processEventInfo)
		{
			dataFields.Add(processEventInfo.ProcessID.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(processEventInfo.ProcessName);
			dataFields.Add(processEventInfo.AccountName);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		private void AddWebRequestInformationDataFields(ArrayList dataFields, WebRequestInformation reqInfo)
		{
			IPrincipal principal = reqInfo.Principal;
			string value;
			bool flag;
			string value2;
			if (principal == null)
			{
				value = null;
				flag = false;
				value2 = null;
			}
			else
			{
				IIdentity identity = principal.Identity;
				value = identity.Name;
				flag = identity.IsAuthenticated;
				value2 = identity.AuthenticationType;
			}
			dataFields.Add(HttpUtility.UrlDecode(reqInfo.RequestUrl));
			dataFields.Add(reqInfo.RequestPath);
			dataFields.Add(reqInfo.UserHostAddress);
			dataFields.Add(value);
			dataFields.Add(flag.ToString());
			dataFields.Add(value2);
			dataFields.Add(reqInfo.ThreadAccountName);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0003C938 File Offset: 0x0003AB38
		private void AddWebProcessStatisticsDataFields(ArrayList dataFields, WebProcessStatistics procStats)
		{
			dataFields.Add(procStats.ProcessStartTime.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.ThreadCount.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.WorkingSet.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.PeakWorkingSet.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.ManagedHeapSize.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.AppDomainCount.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.RequestsExecuting.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.RequestsQueued.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(procStats.RequestsRejected.ToString(CultureInfo.InstalledUICulture));
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0003CA30 File Offset: 0x0003AC30
		private void AddExceptionDataFields(ArrayList dataFields, Exception exception)
		{
			if (exception == null)
			{
				dataFields.Add(null);
				dataFields.Add(null);
				return;
			}
			dataFields.Add(exception.GetType().Name);
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num = 0;
			while (num < 8000 && exception != null)
			{
				string text = EventLogWebEventProvider.ReplaceInsertionStringPlaceholders(exception.Message);
				stringBuilder.Append(text);
				num += text.Length;
				int num2 = 8000 - num;
				if (num2 > 0)
				{
					string text2 = exception.StackTrace;
					if (!string.IsNullOrEmpty(text2))
					{
						if (text2.Length > num2)
						{
							text2 = text2.Substring(0, num2);
						}
						stringBuilder.Append("\n");
						stringBuilder.Append(text2);
						num += text2.Length + 1;
					}
					stringBuilder.Append("\n\n");
					num += 2;
				}
				exception = exception.InnerException;
			}
			dataFields.Add(stringBuilder.ToString());
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0003CB18 File Offset: 0x0003AD18
		private void AddWebThreadInformationDataFields(ArrayList dataFields, WebThreadInformation threadInfo)
		{
			dataFields.Add(threadInfo.ThreadID.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(threadInfo.ThreadAccountName);
			dataFields.Add(threadInfo.IsImpersonating.ToString(CultureInfo.InstalledUICulture));
			dataFields.Add(threadInfo.StackTrace);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003CB74 File Offset: 0x0003AD74
		private void AddViewStateExceptionDataFields(ArrayList dataFields, ViewStateException vse)
		{
			dataFields.Add(SR.GetString(vse.ShortMessage));
			dataFields.Add(vse.RemoteAddress);
			dataFields.Add(vse.RemotePort);
			dataFields.Add(vse.UserAgent);
			dataFields.Add(vse.PersistedState);
			dataFields.Add(vse.Referer);
			dataFields.Add(vse.Path);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003CBE4 File Offset: 0x0003ADE4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			ArrayList arrayList = new ArrayList(35);
			WebEventType eventType = WebBaseEvent.WebEventTypeFromWebEvent(eventRaised);
			this.AddBasicDataFields(arrayList, eventRaised);
			if (eventRaised is WebManagementEvent)
			{
				this.AddWebProcessInformationDataFields(arrayList, ((WebManagementEvent)eventRaised).ProcessInformation);
			}
			if (eventRaised is WebHeartbeatEvent)
			{
				this.AddWebProcessStatisticsDataFields(arrayList, ((WebHeartbeatEvent)eventRaised).ProcessStatistics);
			}
			if (eventRaised is WebRequestEvent)
			{
				this.AddWebRequestInformationDataFields(arrayList, ((WebRequestEvent)eventRaised).RequestInformation);
			}
			if (eventRaised is WebBaseErrorEvent)
			{
				this.AddExceptionDataFields(arrayList, ((WebBaseErrorEvent)eventRaised).ErrorException);
			}
			if (eventRaised is WebAuditEvent)
			{
				this.AddWebRequestInformationDataFields(arrayList, ((WebAuditEvent)eventRaised).RequestInformation);
			}
			if (eventRaised is WebRequestErrorEvent)
			{
				this.AddWebRequestInformationDataFields(arrayList, ((WebRequestErrorEvent)eventRaised).RequestInformation);
				this.AddWebThreadInformationDataFields(arrayList, ((WebRequestErrorEvent)eventRaised).ThreadInformation);
			}
			if (eventRaised is WebErrorEvent)
			{
				this.AddWebRequestInformationDataFields(arrayList, ((WebErrorEvent)eventRaised).RequestInformation);
				this.AddWebThreadInformationDataFields(arrayList, ((WebErrorEvent)eventRaised).ThreadInformation);
			}
			if (eventRaised is WebAuthenticationSuccessAuditEvent)
			{
				arrayList.Add(((WebAuthenticationSuccessAuditEvent)eventRaised).NameToAuthenticate);
			}
			if (eventRaised is WebAuthenticationFailureAuditEvent)
			{
				arrayList.Add(((WebAuthenticationFailureAuditEvent)eventRaised).NameToAuthenticate);
			}
			if (eventRaised is WebViewStateFailureAuditEvent)
			{
				this.AddViewStateExceptionDataFields(arrayList, ((WebViewStateFailureAuditEvent)eventRaised).ViewStateException);
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				object obj = arrayList[i];
				if (obj == null)
				{
					arrayList[i] = string.Empty;
				}
				else
				{
					int length = ((string)obj).Length;
					if (length > 30718)
					{
						arrayList[i] = ((string)obj).Substring(0, this._maxTruncatedParamLen) + "...";
					}
				}
			}
			int num = UnsafeNativeMethods.RaiseEventlogEvent((int)eventType, (string[])arrayList.ToArray(typeof(string)), arrayList.Count);
			if (num != 0)
			{
				throw new HttpException(SR.GetString("Event_log_provider_error", new object[]
				{
					"0x" + num.ToString("X8", CultureInfo.InstalledUICulture)
				}));
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00006164 File Offset: 0x00004364
		public override void Shutdown()
		{
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		private static string ReplaceInsertionStringPlaceholders(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			int length = s.Length;
			int num = length - 1;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (s[i] == '%' && char.IsDigit(s[i + 1]))
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				return s;
			}
			char[] array = new char[length + 2 * num2];
			int num3 = 0;
			for (int j = 0; j < num; j++)
			{
				if (s[j] == '%' && char.IsDigit(s[j + 1]))
				{
					array[num3++] = '[';
					array[num3++] = '%';
					array[num3++] = ']';
				}
				else
				{
					array[num3++] = s[j];
				}
			}
			array[array.Length - 1] = s[num];
			return new string(array);
		}

		// Token: 0x04001537 RID: 5431
		private const int EventLogParameterMaxLength = 30718;

		// Token: 0x04001538 RID: 5432
		private const string _truncateWarning = "...";

		// Token: 0x04001539 RID: 5433
		private int _maxTruncatedParamLen;

		// Token: 0x0400153A RID: 5434
		private const int MAX_CHARS_IN_EXCEPTION_MSG = 8000;
	}
}
