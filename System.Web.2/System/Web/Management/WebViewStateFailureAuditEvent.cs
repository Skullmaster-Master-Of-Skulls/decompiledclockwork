using System;
using System.Collections.Generic;
using System.Web.UI;

namespace System.Web.Management
{
	// Token: 0x02000196 RID: 406
	public class WebViewStateFailureAuditEvent : WebFailureAuditEvent
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x00042B7A File Offset: 0x00040D7A
		protected internal WebViewStateFailureAuditEvent(string message, object eventSource, int eventCode, ViewStateException viewStateException) : base(message, eventSource, eventCode)
		{
			this._viewStateException = viewStateException;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00042B8D File Offset: 0x00040D8D
		protected internal WebViewStateFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, ViewStateException viewStateException) : base(message, eventSource, eventCode, eventDetailCode)
		{
			this._viewStateException = viewStateException;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00042B1F File Offset: 0x00040D1F
		internal WebViewStateFailureAuditEvent()
		{
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00042BA2 File Offset: 0x00040DA2
		public ViewStateException ViewStateException
		{
			get
			{
				return this._viewStateException;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00042BAC File Offset: 0x00040DAC
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("ViewStateExceptionMessage", this.ViewStateException.Message, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("RemoteAddress", this.ViewStateException.RemoteAddress, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("RemotePort", this.ViewStateException.RemotePort, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("UserAgent", this.ViewStateException.UserAgent, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("PersistedState", this.ViewStateException.PersistedState, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("Path", this.ViewStateException.Path, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("Referer", this.ViewStateException.Referer, WebEventFieldType.String));
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00042C84 File Offset: 0x00040E84
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_ViewStateException_information"));
			formatter.IndentationLevel++;
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_exception_message", this._viewStateException.Message));
			formatter.IndentationLevel--;
		}

		// Token: 0x04001647 RID: 5703
		private ViewStateException _viewStateException;
	}
}
