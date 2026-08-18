using System;
using System.Collections.Generic;

namespace System.Web.Management
{
	// Token: 0x02000195 RID: 405
	public class WebAuthenticationFailureAuditEvent : WebFailureAuditEvent
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x00042AEE File Offset: 0x00040CEE
		private void Init(string name)
		{
			this._nameToAuthenticate = name;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00042AF7 File Offset: 0x00040CF7
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, string nameToAuthenticate) : base(message, eventSource, eventCode)
		{
			this.Init(nameToAuthenticate);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00042B0A File Offset: 0x00040D0A
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, string nameToAuthenticate) : base(message, eventSource, eventCode, eventDetailCode)
		{
			this.Init(nameToAuthenticate);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00042B1F File Offset: 0x00040D1F
		internal WebAuthenticationFailureAuditEvent()
		{
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00042B27 File Offset: 0x00040D27
		public string NameToAuthenticate
		{
			get
			{
				return this._nameToAuthenticate;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00042B2F File Offset: 0x00040D2F
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("NameToAuthenticate", this.NameToAuthenticate, WebEventFieldType.String));
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00042B4F File Offset: 0x00040D4F
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_name_to_authenticate", this._nameToAuthenticate));
		}

		// Token: 0x04001646 RID: 5702
		private string _nameToAuthenticate;
	}
}
