using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F97 RID: 3991
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal interface IReminderDialogStrings
	{
		// Token: 0x17003045 RID: 12357
		// (get) Token: 0x06009898 RID: 39064
		// (set) Token: 0x06009899 RID: 39065
		string BeforeStart { get; set; }

		// Token: 0x17003046 RID: 12358
		// (get) Token: 0x0600989A RID: 39066
		// (set) Token: 0x0600989B RID: 39067
		string DueIn { get; set; }

		// Token: 0x17003047 RID: 12359
		// (get) Token: 0x0600989C RID: 39068
		// (set) Token: 0x0600989D RID: 39069
		string Overdue { get; set; }

		// Token: 0x17003048 RID: 12360
		// (get) Token: 0x0600989E RID: 39070
		// (set) Token: 0x0600989F RID: 39071
		string Minute { get; set; }

		// Token: 0x17003049 RID: 12361
		// (get) Token: 0x060098A0 RID: 39072
		// (set) Token: 0x060098A1 RID: 39073
		string Minutes { get; set; }

		// Token: 0x1700304A RID: 12362
		// (get) Token: 0x060098A2 RID: 39074
		// (set) Token: 0x060098A3 RID: 39075
		string Hour { get; set; }

		// Token: 0x1700304B RID: 12363
		// (get) Token: 0x060098A4 RID: 39076
		// (set) Token: 0x060098A5 RID: 39077
		string Hours { get; set; }

		// Token: 0x1700304C RID: 12364
		// (get) Token: 0x060098A6 RID: 39078
		// (set) Token: 0x060098A7 RID: 39079
		string Day { get; set; }

		// Token: 0x1700304D RID: 12365
		// (get) Token: 0x060098A8 RID: 39080
		// (set) Token: 0x060098A9 RID: 39081
		string Days { get; set; }

		// Token: 0x1700304E RID: 12366
		// (get) Token: 0x060098AA RID: 39082
		// (set) Token: 0x060098AB RID: 39083
		string Week { get; set; }

		// Token: 0x1700304F RID: 12367
		// (get) Token: 0x060098AC RID: 39084
		// (set) Token: 0x060098AD RID: 39085
		string Snooze { get; set; }

		// Token: 0x17003050 RID: 12368
		// (get) Token: 0x060098AE RID: 39086
		// (set) Token: 0x060098AF RID: 39087
		string Dismiss { get; set; }

		// Token: 0x17003051 RID: 12369
		// (get) Token: 0x060098B0 RID: 39088
		// (set) Token: 0x060098B1 RID: 39089
		string DismissAll { get; set; }

		// Token: 0x17003052 RID: 12370
		// (get) Token: 0x060098B2 RID: 39090
		// (set) Token: 0x060098B3 RID: 39091
		string OpenItem { get; set; }

		// Token: 0x17003053 RID: 12371
		// (get) Token: 0x060098B4 RID: 39092
		// (set) Token: 0x060098B5 RID: 39093
		string Reminder { get; set; }

		// Token: 0x17003054 RID: 12372
		// (get) Token: 0x060098B6 RID: 39094
		// (set) Token: 0x060098B7 RID: 39095
		string Reminders { get; set; }

		// Token: 0x17003055 RID: 12373
		// (get) Token: 0x060098B8 RID: 39096
		// (set) Token: 0x060098B9 RID: 39097
		string SnoozeHint { get; set; }

		// Token: 0x17003056 RID: 12374
		// (get) Token: 0x060098BA RID: 39098
		// (set) Token: 0x060098BB RID: 39099
		string Close { get; set; }

		// Token: 0x060098BC RID: 39100
		void CopyFromSchedulerStrings(SchedulerStrings localization);
	}
}
