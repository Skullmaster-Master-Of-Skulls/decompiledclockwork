using System;
using System.Collections.Generic;

namespace System.Web.Management
{
	// Token: 0x02000198 RID: 408
	public class WebAuthenticationSuccessAuditEvent : WebSuccessAuditEvent
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x00042D01 File Offset: 0x00040F01
		private void Init(string name)
		{
			this._nameToAuthenticate = name;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00042D0A File Offset: 0x00040F0A
		protected internal WebAuthenticationSuccessAuditEvent(string message, object eventSource, int eventCode, string nameToAuthenticate) : base(message, eventSource, eventCode)
		{
			this.Init(nameToAuthenticate);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00042D1D File Offset: 0x00040F1D
		protected internal WebAuthenticationSuccessAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, string nameToAuthenticate) : base(message, eventSource, eventCode, eventDetailCode)
		{
			this.Init(nameToAuthenticate);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00042D32 File Offset: 0x00040F32
		internal WebAuthenticationSuccessAuditEvent()
		{
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00042D3A File Offset: 0x00040F3A
		public string NameToAuthenticate
		{
			get
			{
				return this._nameToAuthenticate;
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00042D42 File Offset: 0x00040F42
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("NameToAuthenticate", this.NameToAuthenticate, WebEventFieldType.String));
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00042D62 File Offset: 0x00040F62
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_name_to_authenticate", this._nameToAuthenticate));
		}

		// Token: 0x04001648 RID: 5704
		private string _nameToAuthenticate;
	}
}
