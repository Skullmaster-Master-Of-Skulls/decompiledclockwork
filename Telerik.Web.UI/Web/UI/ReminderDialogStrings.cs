using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F98 RID: 3992
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal class ReminderDialogStrings : LocalizationStrings, IReminderDialogStrings
	{
		// Token: 0x17003057 RID: 12375
		// (get) Token: 0x060098BD RID: 39101 RVA: 0x002216E0 File Offset: 0x0021F8E0
		// (set) Token: 0x060098BE RID: 39102 RVA: 0x002216ED File Offset: 0x0021F8ED
		[ScriptIgnore]
		[DefaultValue("before start")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string BeforeStart
		{
			get
			{
				return this.GetString("ReminderBeforeStart");
			}
			set
			{
				this.SetString("ReminderBeforeStart", value);
			}
		}

		// Token: 0x17003058 RID: 12376
		// (get) Token: 0x060098BF RID: 39103 RVA: 0x002216FB File Offset: 0x0021F8FB
		// (set) Token: 0x060098C0 RID: 39104 RVA: 0x00221708 File Offset: 0x0021F908
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("due in")]
		public string DueIn
		{
			get
			{
				return this.GetString("ReminderDueIn");
			}
			set
			{
				this.SetString("ReminderDueIn", value);
			}
		}

		// Token: 0x17003059 RID: 12377
		// (get) Token: 0x060098C1 RID: 39105 RVA: 0x00221716 File Offset: 0x0021F916
		// (set) Token: 0x060098C2 RID: 39106 RVA: 0x00221723 File Offset: 0x0021F923
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("overdue")]
		public string Overdue
		{
			get
			{
				return this.GetString("ReminderOverdue");
			}
			set
			{
				this.SetString("ReminderOverdue", value);
			}
		}

		// Token: 0x1700305A RID: 12378
		// (get) Token: 0x060098C3 RID: 39107 RVA: 0x00221731 File Offset: 0x0021F931
		// (set) Token: 0x060098C4 RID: 39108 RVA: 0x0022173E File Offset: 0x0021F93E
		[DefaultValue("minute")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string Minute
		{
			get
			{
				return this.GetString("ReminderMinute");
			}
			set
			{
				this.SetString("ReminderMinute", value);
			}
		}

		// Token: 0x1700305B RID: 12379
		// (get) Token: 0x060098C5 RID: 39109 RVA: 0x0022174C File Offset: 0x0021F94C
		// (set) Token: 0x060098C6 RID: 39110 RVA: 0x00221759 File Offset: 0x0021F959
		[DefaultValue("minutes")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Minutes
		{
			get
			{
				return this.GetString("ReminderMinutes");
			}
			set
			{
				this.SetString("ReminderMinutes", value);
			}
		}

		// Token: 0x1700305C RID: 12380
		// (get) Token: 0x060098C7 RID: 39111 RVA: 0x00221767 File Offset: 0x0021F967
		// (set) Token: 0x060098C8 RID: 39112 RVA: 0x00221774 File Offset: 0x0021F974
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("hour")]
		public string Hour
		{
			get
			{
				return this.GetString("ReminderHour");
			}
			set
			{
				this.SetString("ReminderHour", value);
			}
		}

		// Token: 0x1700305D RID: 12381
		// (get) Token: 0x060098C9 RID: 39113 RVA: 0x00221782 File Offset: 0x0021F982
		// (set) Token: 0x060098CA RID: 39114 RVA: 0x0022178F File Offset: 0x0021F98F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("hours")]
		public string Hours
		{
			get
			{
				return this.GetString("ReminderHours");
			}
			set
			{
				this.SetString("ReminderHours", value);
			}
		}

		// Token: 0x1700305E RID: 12382
		// (get) Token: 0x060098CB RID: 39115 RVA: 0x0022179D File Offset: 0x0021F99D
		// (set) Token: 0x060098CC RID: 39116 RVA: 0x002217AA File Offset: 0x0021F9AA
		[ScriptIgnore]
		[DefaultValue("day")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string Day
		{
			get
			{
				return this.GetString("ReminderDay");
			}
			set
			{
				this.SetString("ReminderDay", value);
			}
		}

		// Token: 0x1700305F RID: 12383
		// (get) Token: 0x060098CD RID: 39117 RVA: 0x002217B8 File Offset: 0x0021F9B8
		// (set) Token: 0x060098CE RID: 39118 RVA: 0x002217C5 File Offset: 0x0021F9C5
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("days")]
		[ScriptIgnore]
		public string Days
		{
			get
			{
				return this.GetString("ReminderDays");
			}
			set
			{
				this.SetString("ReminderDays", value);
			}
		}

		// Token: 0x17003060 RID: 12384
		// (get) Token: 0x060098CF RID: 39119 RVA: 0x002217D3 File Offset: 0x0021F9D3
		// (set) Token: 0x060098D0 RID: 39120 RVA: 0x002217E0 File Offset: 0x0021F9E0
		[Localizable(true)]
		[DefaultValue("week")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string Week
		{
			get
			{
				return this.GetString("ReminderWeek");
			}
			set
			{
				this.SetString("ReminderWeek", value);
			}
		}

		// Token: 0x17003061 RID: 12385
		// (get) Token: 0x060098D1 RID: 39121 RVA: 0x002217EE File Offset: 0x0021F9EE
		// (set) Token: 0x060098D2 RID: 39122 RVA: 0x002217FB File Offset: 0x0021F9FB
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("Snooze")]
		public string Snooze
		{
			get
			{
				return this.GetString("ReminderSnooze");
			}
			set
			{
				this.SetString("ReminderSnooze", value);
			}
		}

		// Token: 0x17003062 RID: 12386
		// (get) Token: 0x060098D3 RID: 39123 RVA: 0x00221809 File Offset: 0x0021FA09
		// (set) Token: 0x060098D4 RID: 39124 RVA: 0x00221816 File Offset: 0x0021FA16
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[DefaultValue("Dismiss")]
		public string Dismiss
		{
			get
			{
				return this.GetString("ReminderDismiss");
			}
			set
			{
				this.SetString("ReminderDismiss", value);
			}
		}

		// Token: 0x17003063 RID: 12387
		// (get) Token: 0x060098D5 RID: 39125 RVA: 0x00221824 File Offset: 0x0021FA24
		// (set) Token: 0x060098D6 RID: 39126 RVA: 0x00221831 File Offset: 0x0021FA31
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Dismiss All")]
		[Localizable(true)]
		public string DismissAll
		{
			get
			{
				return this.GetString("ReminderDismissAll");
			}
			set
			{
				this.SetString("ReminderDismissAll", value);
			}
		}

		// Token: 0x17003064 RID: 12388
		// (get) Token: 0x060098D7 RID: 39127 RVA: 0x0022183F File Offset: 0x0021FA3F
		// (set) Token: 0x060098D8 RID: 39128 RVA: 0x0022184C File Offset: 0x0021FA4C
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Open Item")]
		public string OpenItem
		{
			get
			{
				return this.GetString("ReminderOpenItem");
			}
			set
			{
				this.SetString("ReminderOpenItem", value);
			}
		}

		// Token: 0x17003065 RID: 12389
		// (get) Token: 0x060098D9 RID: 39129 RVA: 0x0022185A File Offset: 0x0021FA5A
		// (set) Token: 0x060098DA RID: 39130 RVA: 0x00221867 File Offset: 0x0021FA67
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Reminder")]
		public string Reminder
		{
			get
			{
				return this.GetString("Reminder");
			}
			set
			{
				this.SetString("Reminder", value);
			}
		}

		// Token: 0x17003066 RID: 12390
		// (get) Token: 0x060098DB RID: 39131 RVA: 0x00221875 File Offset: 0x0021FA75
		// (set) Token: 0x060098DC RID: 39132 RVA: 0x00221882 File Offset: 0x0021FA82
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Reminders")]
		public string Reminders
		{
			get
			{
				return this.GetString("Reminders");
			}
			set
			{
				this.SetString("Reminders", value);
			}
		}

		// Token: 0x17003067 RID: 12391
		// (get) Token: 0x060098DD RID: 39133 RVA: 0x00221890 File Offset: 0x0021FA90
		// (set) Token: 0x060098DE RID: 39134 RVA: 0x0022189D File Offset: 0x0021FA9D
		[ScriptIgnore]
		[DefaultValue("Click Snooze to be reminded again in:")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string SnoozeHint
		{
			get
			{
				return this.GetString("ReminderSnoozeHint");
			}
			set
			{
				this.SetString("ReminderSnoozeHint", value);
			}
		}

		// Token: 0x17003068 RID: 12392
		// (get) Token: 0x060098DF RID: 39135 RVA: 0x002218AB File Offset: 0x0021FAAB
		// (set) Token: 0x060098E0 RID: 39136 RVA: 0x002218B8 File Offset: 0x0021FAB8
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		public string Close
		{
			get
			{
				return this.GetString("AdvancedClose");
			}
			set
			{
				this.SetString("AdvancedClose", value);
			}
		}

		// Token: 0x060098E1 RID: 39137 RVA: 0x002218C6 File Offset: 0x0021FAC6
		internal ReminderDialogStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x060098E2 RID: 39138 RVA: 0x002218D0 File Offset: 0x0021FAD0
		public void CopyFromSchedulerStrings(SchedulerStrings localization)
		{
			this.BeforeStart = localization.ReminderBeforeStart;
			this.Close = localization.AdvancedClose;
			this.Day = localization.ReminderDay;
			this.Days = localization.ReminderDays;
			this.Dismiss = localization.ReminderDismiss;
			this.DismissAll = localization.ReminderDismissAll;
			this.DueIn = localization.ReminderDueIn;
			this.Hour = localization.ReminderHour;
			this.Hours = localization.ReminderHours;
			this.Minute = localization.ReminderMinute;
			this.Minutes = localization.ReminderMinutes;
			this.OpenItem = localization.ReminderOpenItem;
			this.Overdue = localization.ReminderOverdue;
			this.Reminder = localization.Reminder;
			this.Reminders = localization.Reminders;
			this.Snooze = localization.ReminderSnooze;
			this.SnoozeHint = localization.ReminderSnoozeHint;
			this.Week = localization.ReminderWeek;
		}
	}
}
