using System;
using System.Collections.Generic;

namespace System.Web.Management
{
	// Token: 0x02000193 RID: 403
	public class WebAuditEvent : WebManagementEvent
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x0004292D File Offset: 0x00040B2D
		internal override void PreProcessEventInit()
		{
			base.PreProcessEventInit();
			this.InitRequestInformation();
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00041FBF File Offset: 0x000401BF
		protected internal WebAuditEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00041FCA File Offset: 0x000401CA
		protected internal WebAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00041F4B File Offset: 0x0004014B
		internal WebAuditEvent()
		{
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0004293B File Offset: 0x00040B3B
		private void InitRequestInformation()
		{
			if (this._requestInfo == null)
			{
				this._requestInfo = new WebRequestInformation();
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00042950 File Offset: 0x00040B50
		public WebRequestInformation RequestInformation
		{
			get
			{
				this.InitRequestInformation();
				return this._requestInfo;
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00042960 File Offset: 0x00040B60
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

		// Token: 0x0600159B RID: 5531 RVA: 0x00042A60 File Offset: 0x00040C60
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_request_information"));
			formatter.IndentationLevel++;
			this.RequestInformation.FormatToString(formatter);
			formatter.IndentationLevel--;
		}

		// Token: 0x04001645 RID: 5701
		private WebRequestInformation _requestInfo;
	}
}
