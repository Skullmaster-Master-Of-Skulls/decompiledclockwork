using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.Management
{
	// Token: 0x0200018C RID: 396
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class WebManagementEvent : WebBaseEvent
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x00041E3D File Offset: 0x0004003D
		protected internal WebManagementEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00041E48 File Offset: 0x00040048
		protected internal WebManagementEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00041E55 File Offset: 0x00040055
		internal WebManagementEvent()
		{
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00041E5D File Offset: 0x0004005D
		public WebProcessInformation ProcessInformation
		{
			get
			{
				return WebManagementEvent.s_processInfo;
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00041E64 File Offset: 0x00040064
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("AccountName", this.ProcessInformation.AccountName, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ProcessName", this.ProcessInformation.ProcessName, WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ProcessID", this.ProcessInformation.ProcessID.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00041EDC File Offset: 0x000400DC
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_process_information"));
			formatter.IndentationLevel++;
			this.ProcessInformation.FormatToString(formatter);
			formatter.IndentationLevel--;
		}

		// Token: 0x0400163D RID: 5693
		private static WebProcessInformation s_processInfo = new WebProcessInformation();
	}
}
