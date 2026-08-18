using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000F8D RID: 3981
	public interface IReminderData
	{
		// Token: 0x17003034 RID: 12340
		// (get) Token: 0x0600986B RID: 39019
		// (set) Token: 0x0600986C RID: 39020
		string ID { get; set; }

		// Token: 0x17003035 RID: 12341
		// (get) Token: 0x0600986D RID: 39021
		// (set) Token: 0x0600986E RID: 39022
		int TriggerMinutes { get; set; }

		// Token: 0x17003036 RID: 12342
		// (get) Token: 0x0600986F RID: 39023
		// (set) Token: 0x06009870 RID: 39024
		IDictionary<string, string> Attributes { get; set; }

		// Token: 0x06009871 RID: 39025
		void CopyFrom(Reminder srcReminder);

		// Token: 0x06009872 RID: 39026
		void CopyTo(Reminder destReminder);
	}
}
