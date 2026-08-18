using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Management
{
	// Token: 0x02000190 RID: 400
	public class WebBaseErrorEvent : WebManagementEvent
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x00042233 File Offset: 0x00040433
		private void Init(Exception e)
		{
			this._exception = e;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004223C File Offset: 0x0004043C
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, Exception e) : base(message, eventSource, eventCode)
		{
			this.Init(e);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004224F File Offset: 0x0004044F
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception e) : base(message, eventSource, eventCode, eventDetailCode)
		{
			this.Init(e);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00041F4B File Offset: 0x0004014B
		internal WebBaseErrorEvent()
		{
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00042264 File Offset: 0x00040464
		public Exception ErrorException
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004226C File Offset: 0x0004046C
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			if (this._exception == null)
			{
				return;
			}
			Exception ex = this._exception;
			int num = 0;
			while (ex != null && num <= 2)
			{
				formatter.AppendLine(string.Empty);
				if (num == 0)
				{
					formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_exception_information"));
				}
				else
				{
					formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_inner_exception_information", num.ToString(CultureInfo.InstalledUICulture)));
				}
				formatter.IndentationLevel++;
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_exception_type", ex.GetType().ToString()));
				formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_exception_message", ex.Message));
				formatter.IndentationLevel--;
				ex = ex.InnerException;
				num++;
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00042338 File Offset: 0x00040538
		internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
		{
			base.GenerateFieldsForMarshal(fields);
			fields.Add(new WebEventFieldData("ExceptionType", this.ErrorException.GetType().ToString(), WebEventFieldType.String));
			fields.Add(new WebEventFieldData("ExceptionMessage", this.ErrorException.Message, WebEventFieldType.String));
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00042389 File Offset: 0x00040589
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.EVENTS_ERROR);
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.GLOBAL_EVENTS_ERROR);
		}

		// Token: 0x04001640 RID: 5696
		private Exception _exception;
	}
}
