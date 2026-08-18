using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Management
{
	// Token: 0x02000191 RID: 401
	public class WebErrorEvent : WebBaseErrorEvent
	{
		// Token: 0x0600157C RID: 5500 RVA: 0x00006164 File Offset: 0x00004364
		private void Init(Exception e)
		{
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004239F File Offset: 0x0004059F
		internal override void PreProcessEventInit()
		{
			base.PreProcessEventInit();
			this.InitRequestInformation();
			this.InitThreadInformation();
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000423B3 File Offset: 0x000405B3
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, Exception exception) : base(message, eventSource, eventCode, exception)
		{
			this.Init(exception);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000423C8 File Offset: 0x000405C8
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception) : base(message, eventSource, eventCode, eventDetailCode, exception)
		{
			this.Init(exception);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000423DF File Offset: 0x000405DF
		internal WebErrorEvent()
		{
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000423E7 File Offset: 0x000405E7
		private void InitRequestInformation()
		{
			if (this._requestInfo == null)
			{
				this._requestInfo = new WebRequestInformation();
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000423FC File Offset: 0x000405FC
		public WebRequestInformation RequestInformation
		{
			get
			{
				this.InitRequestInformation();
				return this._requestInfo;
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0004240A File Offset: 0x0004060A
		private void InitThreadInformation()
		{
			if (this._threadInfo == null)
			{
				this._threadInfo = new WebThreadInformation(base.ErrorException);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00042425 File Offset: 0x00040625
		public WebThreadInformation ThreadInformation
		{
			get
			{
				this.InitThreadInformation();
				return this._threadInfo;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00042434 File Offset: 0x00040634
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

		// Token: 0x06001586 RID: 5510 RVA: 0x000424D0 File Offset: 0x000406D0
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

		// Token: 0x06001587 RID: 5511 RVA: 0x00042653 File Offset: 0x00040853
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_HTTP_INFRA_ERROR);
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.GLOBAL_EVENTS_HTTP_INFRA_ERROR);
		}

		// Token: 0x04001641 RID: 5697
		private WebRequestInformation _requestInfo;

		// Token: 0x04001642 RID: 5698
		private WebThreadInformation _threadInfo;
	}
}
