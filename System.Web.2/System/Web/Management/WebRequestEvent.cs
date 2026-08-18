using System;
using System.Collections.Generic;

namespace System.Web.Management
{
	// Token: 0x0200018F RID: 399
	public class WebRequestEvent : WebManagementEvent
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x00042099 File Offset: 0x00040299
		internal override void PreProcessEventInit()
		{
			base.PreProcessEventInit();
			this.InitRequestInformation();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00041FBF File Offset: 0x000401BF
		protected internal WebRequestEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00041FCA File Offset: 0x000401CA
		protected internal WebRequestEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00041F4B File Offset: 0x0004014B
		internal WebRequestEvent()
		{
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000420A7 File Offset: 0x000402A7
		private void InitRequestInformation()
		{
			if (this._requestInfo == null)
			{
				this._requestInfo = new WebRequestInformation();
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000420BC File Offset: 0x000402BC
		public WebRequestInformation RequestInformation
		{
			get
			{
				this.InitRequestInformation();
				return this._requestInfo;
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000420CC File Offset: 0x000402CC
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
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000421CC File Offset: 0x000403CC
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_information"));
			formatter.IndentationLevel++;
			this.RequestInformation.FormatToString(formatter);
			formatter.IndentationLevel--;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00042224 File Offset: 0x00040424
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_WEB_REQ);
		}

		// Token: 0x0400163F RID: 5695
		private WebRequestInformation _requestInfo;
	}
}
