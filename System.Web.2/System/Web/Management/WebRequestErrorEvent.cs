using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Management
{
	// Token: 0x02000192 RID: 402
	public class WebRequestErrorEvent : WebBaseErrorEvent
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x00006164 File Offset: 0x00004364
		private void Init(Exception e)
		{
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00042669 File Offset: 0x00040869
		internal override void PreProcessEventInit()
		{
			base.PreProcessEventInit();
			this.InitRequestInformation();
			this.InitThreadInformation();
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0004267D File Offset: 0x0004087D
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, Exception exception) : base(message, eventSource, eventCode, exception)
		{
			this.Init(exception);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00042692 File Offset: 0x00040892
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception) : base(message, eventSource, eventCode, eventDetailCode, exception)
		{
			this.Init(exception);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000423DF File Offset: 0x000405DF
		internal WebRequestErrorEvent()
		{
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000426A9 File Offset: 0x000408A9
		private void InitRequestInformation()
		{
			if (this._requestInfo == null)
			{
				this._requestInfo = new WebRequestInformation();
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000426BE File Offset: 0x000408BE
		public WebRequestInformation RequestInformation
		{
			get
			{
				this.InitRequestInformation();
				return this._requestInfo;
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000426CC File Offset: 0x000408CC
		private void InitThreadInformation()
		{
			if (this._threadInfo == null)
			{
				this._threadInfo = new WebThreadInformation(base.ErrorException);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000426E7 File Offset: 0x000408E7
		public WebThreadInformation ThreadInformation
		{
			get
			{
				this.InitThreadInformation();
				return this._threadInfo;
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000426F8 File Offset: 0x000408F8
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_information"));
			formatter.IndentationLevel++;
			this.RequestInformation.FormatToString(formatter);
			formatter.IndentationLevel--;
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_thread_information"));
			formatter.IndentationLevel++;
			this.ThreadInformation.FormatToString(formatter);
			formatter.IndentationLevel--;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00042794 File Offset: 0x00040994
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("RequestUrl", this.RequestInformation.RequestUrl, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("RequestPath", this.RequestInformation.RequestPath, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("UserHostAddress", this.RequestInformation.UserHostAddress, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("UserName", this.RequestInformation.Principal.Identity.Name, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("UserAuthenticated", this.RequestInformation.Principal.Identity.IsAuthenticated.ToString(), WebEventFieldType.Bool));
			fields.Add(new WebEventFieldData("UserAuthenticationType", this.RequestInformation.Principal.Identity.AuthenticationType, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("RequestThreadAccountName", this.RequestInformation.ThreadAccountName, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ThreadID", this.ThreadInformation.ThreadID.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
			fields.Add(new WebEventFieldData("ThreadAccountName", this.ThreadInformation.ThreadAccountName, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("StackTrace", this.ThreadInformation.StackTrace, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("IsImpersonating", this.ThreadInformation.IsImpersonating.ToString(), WebEventFieldType.Bool));
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00042917 File Offset: 0x00040B17
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_HTTP_REQ_ERROR);
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.GLOBAL_EVENTS_HTTP_REQ_ERROR);
		}

		// Token: 0x04001643 RID: 5699
		private WebRequestInformation _requestInfo;

		// Token: 0x04001644 RID: 5700
		private WebThreadInformation _threadInfo;
	}
}
