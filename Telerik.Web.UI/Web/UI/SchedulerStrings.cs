using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020011C1 RID: 4545
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SchedulerStrings : LocalizationStrings
	{
		// Token: 0x17003C5F RID: 15455
		// (get) Token: 0x0600BAFE RID: 47870 RVA: 0x00299571 File Offset: 0x00297771
		// (set) Token: 0x0600BAFF RID: 47871 RVA: 0x0029957E File Offset: 0x0029777E
		[DefaultValue("today")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HeaderToday
		{
			get
			{
				return this.GetString("HeaderToday");
			}
			set
			{
				this.SetString("HeaderToday", value);
			}
		}

		// Token: 0x17003C60 RID: 15456
		// (get) Token: 0x0600BB00 RID: 47872 RVA: 0x0029958C File Offset: 0x0029778C
		// (set) Token: 0x0600BB01 RID: 47873 RVA: 0x00299599 File Offset: 0x00297799
		[DefaultValue("previous day")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HeaderPrevDay
		{
			get
			{
				return this.GetString("HeaderPrevDay");
			}
			set
			{
				this.SetString("HeaderPrevDay", value);
			}
		}

		// Token: 0x17003C61 RID: 15457
		// (get) Token: 0x0600BB02 RID: 47874 RVA: 0x002995A7 File Offset: 0x002977A7
		// (set) Token: 0x0600BB03 RID: 47875 RVA: 0x002995B4 File Offset: 0x002977B4
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("next day")]
		[Localizable(true)]
		public string HeaderNextDay
		{
			get
			{
				return this.GetString("HeaderNextDay");
			}
			set
			{
				this.SetString("HeaderNextDay", value);
			}
		}

		// Token: 0x17003C62 RID: 15458
		// (get) Token: 0x0600BB04 RID: 47876 RVA: 0x002995C2 File Offset: 0x002977C2
		// (set) Token: 0x0600BB05 RID: 47877 RVA: 0x002995CF File Offset: 0x002977CF
		[DefaultValue("Day")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string HeaderDay
		{
			get
			{
				return this.GetString("HeaderDay");
			}
			set
			{
				this.SetString("HeaderDay", value);
			}
		}

		// Token: 0x17003C63 RID: 15459
		// (get) Token: 0x0600BB06 RID: 47878 RVA: 0x002995DD File Offset: 0x002977DD
		// (set) Token: 0x0600BB07 RID: 47879 RVA: 0x002995EA File Offset: 0x002977EA
		[Localizable(true)]
		[DefaultValue("Week")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string HeaderWeek
		{
			get
			{
				return this.GetString("HeaderWeek");
			}
			set
			{
				this.SetString("HeaderWeek", value);
			}
		}

		// Token: 0x17003C64 RID: 15460
		// (get) Token: 0x0600BB08 RID: 47880 RVA: 0x002995F8 File Offset: 0x002977F8
		// (set) Token: 0x0600BB09 RID: 47881 RVA: 0x00299605 File Offset: 0x00297805
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Month")]
		[ScriptIgnore]
		public string HeaderMonth
		{
			get
			{
				return this.GetString("HeaderMonth");
			}
			set
			{
				this.SetString("HeaderMonth", value);
			}
		}

		// Token: 0x17003C65 RID: 15461
		// (get) Token: 0x0600BB0A RID: 47882 RVA: 0x00299613 File Offset: 0x00297813
		// (set) Token: 0x0600BB0B RID: 47883 RVA: 0x00299620 File Offset: 0x00297820
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Timeline")]
		[Localizable(true)]
		public string HeaderTimeline
		{
			get
			{
				return this.GetString("HeaderTimeline");
			}
			set
			{
				this.SetString("HeaderTimeline", value);
			}
		}

		// Token: 0x17003C66 RID: 15462
		// (get) Token: 0x0600BB0C RID: 47884 RVA: 0x0029962E File Offset: 0x0029782E
		// (set) Token: 0x0600BB0D RID: 47885 RVA: 0x0029963B File Offset: 0x0029783B
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Multi-day")]
		[ScriptIgnore]
		public string HeaderMultiDay
		{
			get
			{
				return this.GetString("HeaderMultiDay");
			}
			set
			{
				this.SetString("HeaderMultiDay", value);
			}
		}

		// Token: 0x17003C67 RID: 15463
		// (get) Token: 0x0600BB0E RID: 47886 RVA: 0x00299649 File Offset: 0x00297849
		// (set) Token: 0x0600BB0F RID: 47887 RVA: 0x00299656 File Offset: 0x00297856
		[DefaultValue("Agenda")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HeaderAgenda
		{
			get
			{
				return this.GetString("HeaderAgenda");
			}
			set
			{
				this.SetString("HeaderAgenda", value);
			}
		}

		// Token: 0x17003C68 RID: 15464
		// (get) Token: 0x0600BB10 RID: 47888 RVA: 0x00299664 File Offset: 0x00297864
		// (set) Token: 0x0600BB11 RID: 47889 RVA: 0x00299671 File Offset: 0x00297871
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Year")]
		public string HeaderYear
		{
			get
			{
				return this.GetString("HeaderYear");
			}
			set
			{
				this.SetString("HeaderYear", value);
			}
		}

		// Token: 0x17003C69 RID: 15465
		// (get) Token: 0x0600BB12 RID: 47890 RVA: 0x0029967F File Offset: 0x0029787F
		// (set) Token: 0x0600BB13 RID: 47891 RVA: 0x0029968C File Offset: 0x0029788C
		[NotifyParentProperty(true)]
		[DefaultValue("Date")]
		[ScriptIgnore]
		[Localizable(true)]
		public string HeaderAgendaDate
		{
			get
			{
				return this.GetString("HeaderAgendaDate");
			}
			set
			{
				this.SetString("HeaderAgendaDate", value);
			}
		}

		// Token: 0x17003C6A RID: 15466
		// (get) Token: 0x0600BB14 RID: 47892 RVA: 0x0029969A File Offset: 0x0029789A
		// (set) Token: 0x0600BB15 RID: 47893 RVA: 0x002996A7 File Offset: 0x002978A7
		[NotifyParentProperty(true)]
		[DefaultValue("Time")]
		[Localizable(true)]
		[ScriptIgnore]
		public string HeaderAgendaTime
		{
			get
			{
				return this.GetString("HeaderAgendaTime");
			}
			set
			{
				this.SetString("HeaderAgendaTime", value);
			}
		}

		// Token: 0x17003C6B RID: 15467
		// (get) Token: 0x0600BB16 RID: 47894 RVA: 0x002996B5 File Offset: 0x002978B5
		// (set) Token: 0x0600BB17 RID: 47895 RVA: 0x002996C2 File Offset: 0x002978C2
		[DefaultValue("Appointment")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string HeaderAgendaAppointment
		{
			get
			{
				return this.GetString("HeaderAgendaAppointment");
			}
			set
			{
				this.SetString("HeaderAgendaAppointment", value);
			}
		}

		// Token: 0x17003C6C RID: 15468
		// (get) Token: 0x0600BB18 RID: 47896 RVA: 0x002996D0 File Offset: 0x002978D0
		// (set) Token: 0x0600BB19 RID: 47897 RVA: 0x002996DD File Offset: 0x002978DD
		[DefaultValue("Resource")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string HeaderAgendaResource
		{
			get
			{
				return this.GetString("HeaderAgendaResource");
			}
			set
			{
				this.SetString("HeaderAgendaResource", value);
			}
		}

		// Token: 0x17003C6D RID: 15469
		// (get) Token: 0x0600BB1A RID: 47898 RVA: 0x002996EB File Offset: 0x002978EB
		// (set) Token: 0x0600BB1B RID: 47899 RVA: 0x002996F8 File Offset: 0x002978F8
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Add appointment")]
		[NotifyParentProperty(true)]
		public string HeaderAddAppointment
		{
			get
			{
				return this.GetString("HeaderAddAppointment");
			}
			set
			{
				this.SetString("HeaderAddAppointment", value);
			}
		}

		// Token: 0x17003C6E RID: 15470
		// (get) Token: 0x0600BB1C RID: 47900 RVA: 0x00299706 File Offset: 0x00297906
		// (set) Token: 0x0600BB1D RID: 47901 RVA: 0x00299713 File Offset: 0x00297913
		[DefaultValue("all day")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AllDay
		{
			get
			{
				return this.GetString("AllDay");
			}
			set
			{
				this.SetString("AllDay", value);
			}
		}

		// Token: 0x17003C6F RID: 15471
		// (get) Token: 0x0600BB1E RID: 47902 RVA: 0x00299721 File Offset: 0x00297921
		// (set) Token: 0x0600BB1F RID: 47903 RVA: 0x0029972E File Offset: 0x0029792E
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Show 24 hours...")]
		public string Show24Hours
		{
			get
			{
				return this.GetString("Show24Hours");
			}
			set
			{
				this.SetString("Show24Hours", value);
			}
		}

		// Token: 0x17003C70 RID: 15472
		// (get) Token: 0x0600BB20 RID: 47904 RVA: 0x0029973C File Offset: 0x0029793C
		// (set) Token: 0x0600BB21 RID: 47905 RVA: 0x00299749 File Offset: 0x00297949
		[DefaultValue("Show business hours...")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ShowBusinessHours
		{
			get
			{
				return this.GetString("ShowBusinessHours");
			}
			set
			{
				this.SetString("ShowBusinessHours", value);
			}
		}

		// Token: 0x17003C71 RID: 15473
		// (get) Token: 0x0600BB22 RID: 47906 RVA: 0x00299757 File Offset: 0x00297957
		// (set) Token: 0x0600BB23 RID: 47907 RVA: 0x00299764 File Offset: 0x00297964
		[NotifyParentProperty(true)]
		[DefaultValue("Save")]
		[Localizable(true)]
		public string Save
		{
			get
			{
				return this.GetString("Save");
			}
			set
			{
				this.SetString("Save", value);
			}
		}

		// Token: 0x17003C72 RID: 15474
		// (get) Token: 0x0600BB24 RID: 47908 RVA: 0x00299772 File Offset: 0x00297972
		// (set) Token: 0x0600BB25 RID: 47909 RVA: 0x0029977F File Offset: 0x0029797F
		[DefaultValue("Cancel")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x17003C73 RID: 15475
		// (get) Token: 0x0600BB26 RID: 47910 RVA: 0x0029978D File Offset: 0x0029798D
		// (set) Token: 0x0600BB27 RID: 47911 RVA: 0x0029979A File Offset: 0x0029799A
		[Localizable(true)]
		[DefaultValue("Delete")]
		[NotifyParentProperty(true)]
		public string Delete
		{
			get
			{
				return this.GetString("Delete");
			}
			set
			{
				this.SetString("Delete", value);
			}
		}

		// Token: 0x17003C74 RID: 15476
		// (get) Token: 0x0600BB28 RID: 47912 RVA: 0x002997A8 File Offset: 0x002979A8
		// (set) Token: 0x0600BB29 RID: 47913 RVA: 0x002997B5 File Offset: 0x002979B5
		[Localizable(true)]
		[DefaultValue("Options")]
		[NotifyParentProperty(true)]
		public string ShowAdvancedForm
		{
			get
			{
				return this.GetString("ShowAdvancedForm");
			}
			set
			{
				this.SetString("ShowAdvancedForm", value);
			}
		}

		// Token: 0x17003C75 RID: 15477
		// (get) Token: 0x0600BB2A RID: 47914 RVA: 0x002997C3 File Offset: 0x002979C3
		// (set) Token: 0x0600BB2B RID: 47915 RVA: 0x002997D0 File Offset: 0x002979D0
		[Localizable(true)]
		[DefaultValue("more...")]
		[NotifyParentProperty(true)]
		public string ShowMore
		{
			get
			{
				return this.GetString("ShowMore");
			}
			set
			{
				this.SetString("ShowMore", value);
			}
		}

		// Token: 0x17003C76 RID: 15478
		// (get) Token: 0x0600BB2C RID: 47916 RVA: 0x002997DE File Offset: 0x002979DE
		// (set) Token: 0x0600BB2D RID: 47917 RVA: 0x002997EB File Offset: 0x002979EB
		[DefaultValue("Subject")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string AdvancedSubject
		{
			get
			{
				return this.GetString("AdvancedSubject");
			}
			set
			{
				this.SetString("AdvancedSubject", value);
			}
		}

		// Token: 0x17003C77 RID: 15479
		// (get) Token: 0x0600BB2E RID: 47918 RVA: 0x002997F9 File Offset: 0x002979F9
		// (set) Token: 0x0600BB2F RID: 47919 RVA: 0x00299806 File Offset: 0x00297A06
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Description")]
		[ScriptIgnore]
		public string AdvancedDescription
		{
			get
			{
				return this.GetString("AdvancedDescription");
			}
			set
			{
				this.SetString("AdvancedDescription", value);
			}
		}

		// Token: 0x17003C78 RID: 15480
		// (get) Token: 0x0600BB30 RID: 47920 RVA: 0x00299814 File Offset: 0x00297A14
		// (set) Token: 0x0600BB31 RID: 47921 RVA: 0x00299821 File Offset: 0x00297A21
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Start time")]
		public string AdvancedFrom
		{
			get
			{
				return this.GetString("AdvancedFrom");
			}
			set
			{
				this.SetString("AdvancedFrom", value);
			}
		}

		// Token: 0x17003C79 RID: 15481
		// (get) Token: 0x0600BB32 RID: 47922 RVA: 0x0029982F File Offset: 0x00297A2F
		// (set) Token: 0x0600BB33 RID: 47923 RVA: 0x0029983C File Offset: 0x00297A3C
		[DefaultValue("End time")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedTo
		{
			get
			{
				return this.GetString("AdvancedTo");
			}
			set
			{
				this.SetString("AdvancedTo", value);
			}
		}

		// Token: 0x17003C7A RID: 15482
		// (get) Token: 0x0600BB34 RID: 47924 RVA: 0x0029984A File Offset: 0x00297A4A
		// (set) Token: 0x0600BB35 RID: 47925 RVA: 0x00299857 File Offset: 0x00297A57
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("All day")]
		[Localizable(true)]
		public string AdvancedAllDayEvent
		{
			get
			{
				return this.GetString("AdvancedAllDayEvent");
			}
			set
			{
				this.SetString("AdvancedAllDayEvent", value);
			}
		}

		// Token: 0x17003C7B RID: 15483
		// (get) Token: 0x0600BB36 RID: 47926 RVA: 0x00299865 File Offset: 0x00297A65
		// (set) Token: 0x0600BB37 RID: 47927 RVA: 0x00299872 File Offset: 0x00297A72
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Time zone")]
		[Localizable(true)]
		public string AdvancedTimeZone
		{
			get
			{
				return this.GetString("AdvancedTimeZone");
			}
			set
			{
				this.SetString("AdvancedTimeZone", value);
			}
		}

		// Token: 0x17003C7C RID: 15484
		// (get) Token: 0x0600BB38 RID: 47928 RVA: 0x00299880 File Offset: 0x00297A80
		// (set) Token: 0x0600BB39 RID: 47929 RVA: 0x0029988D File Offset: 0x00297A8D
		[DefaultValue("Recurrence")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedRecurrence
		{
			get
			{
				return this.GetString("AdvancedRecurrence");
			}
			set
			{
				this.SetString("AdvancedRecurrence", value);
			}
		}

		// Token: 0x17003C7D RID: 15485
		// (get) Token: 0x0600BB3A RID: 47930 RVA: 0x0029989B File Offset: 0x00297A9B
		// (set) Token: 0x0600BB3B RID: 47931 RVA: 0x002998A8 File Offset: 0x00297AA8
		[DefaultValue("Repeat")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AdvancedRepeat
		{
			get
			{
				return this.GetString("AdvancedRepeat");
			}
			set
			{
				this.SetString("AdvancedRepeat", value);
			}
		}

		// Token: 0x17003C7E RID: 15486
		// (get) Token: 0x0600BB3C RID: 47932 RVA: 0x002998B6 File Offset: 0x00297AB6
		// (set) Token: 0x0600BB3D RID: 47933 RVA: 0x002998C3 File Offset: 0x00297AC3
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Never")]
		public string AdvancedNever
		{
			get
			{
				return this.GetString("AdvancedNever");
			}
			set
			{
				this.SetString("AdvancedNever", value);
			}
		}

		// Token: 0x17003C7F RID: 15487
		// (get) Token: 0x0600BB3E RID: 47934 RVA: 0x002998D1 File Offset: 0x00297AD1
		// (set) Token: 0x0600BB3F RID: 47935 RVA: 0x002998DE File Offset: 0x00297ADE
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Hourly")]
		public string AdvancedHourly
		{
			get
			{
				return this.GetString("AdvancedHourly");
			}
			set
			{
				this.SetString("AdvancedHourly", value);
			}
		}

		// Token: 0x17003C80 RID: 15488
		// (get) Token: 0x0600BB40 RID: 47936 RVA: 0x002998EC File Offset: 0x00297AEC
		// (set) Token: 0x0600BB41 RID: 47937 RVA: 0x002998F9 File Offset: 0x00297AF9
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Daily")]
		public string AdvancedDaily
		{
			get
			{
				return this.GetString("AdvancedDaily");
			}
			set
			{
				this.SetString("AdvancedDaily", value);
			}
		}

		// Token: 0x17003C81 RID: 15489
		// (get) Token: 0x0600BB42 RID: 47938 RVA: 0x00299907 File Offset: 0x00297B07
		// (set) Token: 0x0600BB43 RID: 47939 RVA: 0x00299914 File Offset: 0x00297B14
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Weekly")]
		public string AdvancedWeekly
		{
			get
			{
				return this.GetString("AdvancedWeekly");
			}
			set
			{
				this.SetString("AdvancedWeekly", value);
			}
		}

		// Token: 0x17003C82 RID: 15490
		// (get) Token: 0x0600BB44 RID: 47940 RVA: 0x00299922 File Offset: 0x00297B22
		// (set) Token: 0x0600BB45 RID: 47941 RVA: 0x0029992F File Offset: 0x00297B2F
		[NotifyParentProperty(true)]
		[DefaultValue("Monthly")]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedMonthly
		{
			get
			{
				return this.GetString("AdvancedMonthly");
			}
			set
			{
				this.SetString("AdvancedMonthly", value);
			}
		}

		// Token: 0x17003C83 RID: 15491
		// (get) Token: 0x0600BB46 RID: 47942 RVA: 0x0029993D File Offset: 0x00297B3D
		// (set) Token: 0x0600BB47 RID: 47943 RVA: 0x0029994A File Offset: 0x00297B4A
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Yearly")]
		[NotifyParentProperty(true)]
		public string AdvancedYearly
		{
			get
			{
				return this.GetString("AdvancedYearly");
			}
			set
			{
				this.SetString("AdvancedYearly", value);
			}
		}

		// Token: 0x17003C84 RID: 15492
		// (get) Token: 0x0600BB48 RID: 47944 RVA: 0x00299958 File Offset: 0x00297B58
		// (set) Token: 0x0600BB49 RID: 47945 RVA: 0x00299965 File Offset: 0x00297B65
		[Localizable(true)]
		[DefaultValue("Every")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedEvery
		{
			get
			{
				return this.GetString("AdvancedEvery");
			}
			set
			{
				this.SetString("AdvancedEvery", value);
			}
		}

		// Token: 0x17003C85 RID: 15493
		// (get) Token: 0x0600BB4A RID: 47946 RVA: 0x00299973 File Offset: 0x00297B73
		// (set) Token: 0x0600BB4B RID: 47947 RVA: 0x00299980 File Offset: 0x00297B80
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("hour(s)")]
		public string AdvancedHours
		{
			get
			{
				return this.GetString("AdvancedHours");
			}
			set
			{
				this.SetString("AdvancedHours", value);
			}
		}

		// Token: 0x17003C86 RID: 15494
		// (get) Token: 0x0600BB4C RID: 47948 RVA: 0x0029998E File Offset: 0x00297B8E
		// (set) Token: 0x0600BB4D RID: 47949 RVA: 0x0029999B File Offset: 0x00297B9B
		[DefaultValue("day(s)")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedDays
		{
			get
			{
				return this.GetString("AdvancedDays");
			}
			set
			{
				this.SetString("AdvancedDays", value);
			}
		}

		// Token: 0x17003C87 RID: 15495
		// (get) Token: 0x0600BB4E RID: 47950 RVA: 0x002999A9 File Offset: 0x00297BA9
		// (set) Token: 0x0600BB4F RID: 47951 RVA: 0x002999B6 File Offset: 0x00297BB6
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("week(s) on")]
		[NotifyParentProperty(true)]
		public string AdvancedWeeks
		{
			get
			{
				return this.GetString("AdvancedWeeks");
			}
			set
			{
				this.SetString("AdvancedWeeks", value);
			}
		}

		// Token: 0x17003C88 RID: 15496
		// (get) Token: 0x0600BB50 RID: 47952 RVA: 0x002999C4 File Offset: 0x00297BC4
		// (set) Token: 0x0600BB51 RID: 47953 RVA: 0x002999D1 File Offset: 0x00297BD1
		[Localizable(true)]
		[DefaultValue("month(s)")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedMonths
		{
			get
			{
				return this.GetString("AdvancedMonths");
			}
			set
			{
				this.SetString("AdvancedMonths", value);
			}
		}

		// Token: 0x17003C89 RID: 15497
		// (get) Token: 0x0600BB52 RID: 47954 RVA: 0x002999DF File Offset: 0x00297BDF
		// (set) Token: 0x0600BB53 RID: 47955 RVA: 0x002999EC File Offset: 0x00297BEC
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[DefaultValue("year(s)")]
		[Localizable(true)]
		public string AdvancedYears
		{
			get
			{
				return this.GetString("AdvancedYears");
			}
			set
			{
				this.SetString("AdvancedYears", value);
			}
		}

		// Token: 0x17003C8A RID: 15498
		// (get) Token: 0x0600BB54 RID: 47956 RVA: 0x002999FA File Offset: 0x00297BFA
		// (set) Token: 0x0600BB55 RID: 47957 RVA: 0x00299A07 File Offset: 0x00297C07
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Every weekday")]
		[Localizable(true)]
		public string AdvancedEveryWeekday
		{
			get
			{
				return this.GetString("AdvancedEveryWeekday");
			}
			set
			{
				this.SetString("AdvancedEveryWeekday", value);
			}
		}

		// Token: 0x17003C8B RID: 15499
		// (get) Token: 0x0600BB56 RID: 47958 RVA: 0x00299A15 File Offset: 0x00297C15
		// (set) Token: 0x0600BB57 RID: 47959 RVA: 0x00299A22 File Offset: 0x00297C22
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Recur every")]
		[NotifyParentProperty(true)]
		public string AdvancedRecurEvery
		{
			get
			{
				return this.GetString("AdvancedRecurEvery");
			}
			set
			{
				this.SetString("AdvancedRecurEvery", value);
			}
		}

		// Token: 0x17003C8C RID: 15500
		// (get) Token: 0x0600BB58 RID: 47960 RVA: 0x00299A30 File Offset: 0x00297C30
		// (set) Token: 0x0600BB59 RID: 47961 RVA: 0x00299A3D File Offset: 0x00297C3D
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Day")]
		[Localizable(true)]
		public string AdvancedDay
		{
			get
			{
				return this.GetString("AdvancedDay");
			}
			set
			{
				this.SetString("AdvancedDay", value);
			}
		}

		// Token: 0x17003C8D RID: 15501
		// (get) Token: 0x0600BB5A RID: 47962 RVA: 0x00299A4B File Offset: 0x00297C4B
		// (set) Token: 0x0600BB5B RID: 47963 RVA: 0x00299A58 File Offset: 0x00297C58
		[ScriptIgnore]
		[DefaultValue("of every")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AdvancedOfEvery
		{
			get
			{
				return this.GetString("AdvancedOfEvery");
			}
			set
			{
				this.SetString("AdvancedOfEvery", value);
			}
		}

		// Token: 0x17003C8E RID: 15502
		// (get) Token: 0x0600BB5C RID: 47964 RVA: 0x00299A66 File Offset: 0x00297C66
		// (set) Token: 0x0600BB5D RID: 47965 RVA: 0x00299A73 File Offset: 0x00297C73
		[DefaultValue("first")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedFirst
		{
			get
			{
				return this.GetString("AdvancedFirst");
			}
			set
			{
				this.SetString("AdvancedFirst", value);
			}
		}

		// Token: 0x17003C8F RID: 15503
		// (get) Token: 0x0600BB5E RID: 47966 RVA: 0x00299A81 File Offset: 0x00297C81
		// (set) Token: 0x0600BB5F RID: 47967 RVA: 0x00299A8E File Offset: 0x00297C8E
		[ScriptIgnore]
		[DefaultValue("second")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AdvancedSecond
		{
			get
			{
				return this.GetString("AdvancedSecond");
			}
			set
			{
				this.SetString("AdvancedSecond", value);
			}
		}

		// Token: 0x17003C90 RID: 15504
		// (get) Token: 0x0600BB60 RID: 47968 RVA: 0x00299A9C File Offset: 0x00297C9C
		// (set) Token: 0x0600BB61 RID: 47969 RVA: 0x00299AA9 File Offset: 0x00297CA9
		[ScriptIgnore]
		[DefaultValue("third")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AdvancedThird
		{
			get
			{
				return this.GetString("AdvancedThird");
			}
			set
			{
				this.SetString("AdvancedThird", value);
			}
		}

		// Token: 0x17003C91 RID: 15505
		// (get) Token: 0x0600BB62 RID: 47970 RVA: 0x00299AB7 File Offset: 0x00297CB7
		// (set) Token: 0x0600BB63 RID: 47971 RVA: 0x00299AC4 File Offset: 0x00297CC4
		[DefaultValue("fourth")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AdvancedFourth
		{
			get
			{
				return this.GetString("AdvancedFourth");
			}
			set
			{
				this.SetString("AdvancedFourth", value);
			}
		}

		// Token: 0x17003C92 RID: 15506
		// (get) Token: 0x0600BB64 RID: 47972 RVA: 0x00299AD2 File Offset: 0x00297CD2
		// (set) Token: 0x0600BB65 RID: 47973 RVA: 0x00299ADF File Offset: 0x00297CDF
		[DefaultValue("last")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedLast
		{
			get
			{
				return this.GetString("AdvancedLast");
			}
			set
			{
				this.SetString("AdvancedLast", value);
			}
		}

		// Token: 0x17003C93 RID: 15507
		// (get) Token: 0x0600BB66 RID: 47974 RVA: 0x00299AED File Offset: 0x00297CED
		// (set) Token: 0x0600BB67 RID: 47975 RVA: 0x00299AFA File Offset: 0x00297CFA
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("day")]
		[ScriptIgnore]
		public string AdvancedMaskDay
		{
			get
			{
				return this.GetString("AdvancedMaskDay");
			}
			set
			{
				this.SetString("AdvancedMaskDay", value);
			}
		}

		// Token: 0x17003C94 RID: 15508
		// (get) Token: 0x0600BB68 RID: 47976 RVA: 0x00299B08 File Offset: 0x00297D08
		// (set) Token: 0x0600BB69 RID: 47977 RVA: 0x00299B15 File Offset: 0x00297D15
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("weekday")]
		[ScriptIgnore]
		public string AdvancedMaskWeekday
		{
			get
			{
				return this.GetString("AdvancedMaskWeekday");
			}
			set
			{
				this.SetString("AdvancedMaskWeekday", value);
			}
		}

		// Token: 0x17003C95 RID: 15509
		// (get) Token: 0x0600BB6A RID: 47978 RVA: 0x00299B23 File Offset: 0x00297D23
		// (set) Token: 0x0600BB6B RID: 47979 RVA: 0x00299B30 File Offset: 0x00297D30
		[Localizable(true)]
		[DefaultValue("weekend day")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedMaskWeekendDay
		{
			get
			{
				return this.GetString("AdvancedMaskWeekendDay");
			}
			set
			{
				this.SetString("AdvancedMaskWeekendDay", value);
			}
		}

		// Token: 0x17003C96 RID: 15510
		// (get) Token: 0x0600BB6C RID: 47980 RVA: 0x00299B3E File Offset: 0x00297D3E
		// (set) Token: 0x0600BB6D RID: 47981 RVA: 0x00299B4B File Offset: 0x00297D4B
		[DefaultValue("The")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedThe
		{
			get
			{
				return this.GetString("AdvancedThe");
			}
			set
			{
				this.SetString("AdvancedThe", value);
			}
		}

		// Token: 0x17003C97 RID: 15511
		// (get) Token: 0x0600BB6E RID: 47982 RVA: 0x00299B59 File Offset: 0x00297D59
		// (set) Token: 0x0600BB6F RID: 47983 RVA: 0x00299B66 File Offset: 0x00297D66
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("of")]
		public string AdvancedOf
		{
			get
			{
				return this.GetString("AdvancedOf");
			}
			set
			{
				this.SetString("AdvancedOf", value);
			}
		}

		// Token: 0x17003C98 RID: 15512
		// (get) Token: 0x0600BB70 RID: 47984 RVA: 0x00299B74 File Offset: 0x00297D74
		// (set) Token: 0x0600BB71 RID: 47985 RVA: 0x00299B81 File Offset: 0x00297D81
		[DefaultValue("reset exceptions")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AdvancedReset
		{
			get
			{
				return this.GetString("AdvancedReset");
			}
			set
			{
				this.SetString("AdvancedReset", value);
			}
		}

		// Token: 0x17003C99 RID: 15513
		// (get) Token: 0x0600BB72 RID: 47986 RVA: 0x00299B8F File Offset: 0x00297D8F
		// (set) Token: 0x0600BB73 RID: 47987 RVA: 0x00299B9C File Offset: 0x00297D9C
		[Localizable(true)]
		[DefaultValue("No end date")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedNoEndDate
		{
			get
			{
				return this.GetString("AdvancedNoEndDate");
			}
			set
			{
				this.SetString("AdvancedNoEndDate", value);
			}
		}

		// Token: 0x17003C9A RID: 15514
		// (get) Token: 0x0600BB74 RID: 47988 RVA: 0x00299BAA File Offset: 0x00297DAA
		// (set) Token: 0x0600BB75 RID: 47989 RVA: 0x00299BB7 File Offset: 0x00297DB7
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("End after")]
		[ScriptIgnore]
		public string AdvancedEndAfter
		{
			get
			{
				return this.GetString("AdvancedEndAfter");
			}
			set
			{
				this.SetString("AdvancedEndAfter", value);
			}
		}

		// Token: 0x17003C9B RID: 15515
		// (get) Token: 0x0600BB76 RID: 47990 RVA: 0x00299BC5 File Offset: 0x00297DC5
		// (set) Token: 0x0600BB77 RID: 47991 RVA: 0x00299BD2 File Offset: 0x00297DD2
		[DefaultValue("End by")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedEndByThisDate
		{
			get
			{
				return this.GetString("AdvancedEndByThisDate");
			}
			set
			{
				this.SetString("AdvancedEndByThisDate", value);
			}
		}

		// Token: 0x17003C9C RID: 15516
		// (get) Token: 0x0600BB78 RID: 47992 RVA: 0x00299BE0 File Offset: 0x00297DE0
		// (set) Token: 0x0600BB79 RID: 47993 RVA: 0x00299BED File Offset: 0x00297DED
		[DefaultValue("occurrences")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedOccurrences
		{
			get
			{
				return this.GetString("AdvancedOccurrences");
			}
			set
			{
				this.SetString("AdvancedOccurrences", value);
			}
		}

		// Token: 0x17003C9D RID: 15517
		// (get) Token: 0x0600BB7A RID: 47994 RVA: 0x00299BFB File Offset: 0x00297DFB
		// (set) Token: 0x0600BB7B RID: 47995 RVA: 0x00299C08 File Offset: 0x00297E08
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("OK")]
		[NotifyParentProperty(true)]
		public string AdvancedCalendarOK
		{
			get
			{
				return this.GetString("AdvancedCalendarOK");
			}
			set
			{
				this.SetString("AdvancedCalendarOK", value);
			}
		}

		// Token: 0x17003C9E RID: 15518
		// (get) Token: 0x0600BB7C RID: 47996 RVA: 0x00299C16 File Offset: 0x00297E16
		// (set) Token: 0x0600BB7D RID: 47997 RVA: 0x00299C23 File Offset: 0x00297E23
		[Localizable(true)]
		[DefaultValue("Cancel")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedCalendarCancel
		{
			get
			{
				return this.GetString("AdvancedCalendarCancel");
			}
			set
			{
				this.SetString("AdvancedCalendarCancel", value);
			}
		}

		// Token: 0x17003C9F RID: 15519
		// (get) Token: 0x0600BB7E RID: 47998 RVA: 0x00299C31 File Offset: 0x00297E31
		// (set) Token: 0x0600BB7F RID: 47999 RVA: 0x00299C3E File Offset: 0x00297E3E
		[NotifyParentProperty(true)]
		[DefaultValue("Today")]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedCalendarToday
		{
			get
			{
				return this.GetString("AdvancedCalendarToday");
			}
			set
			{
				this.SetString("AdvancedCalendarToday", value);
			}
		}

		// Token: 0x17003CA0 RID: 15520
		// (get) Token: 0x0600BB80 RID: 48000 RVA: 0x00299C4C File Offset: 0x00297E4C
		// (set) Token: 0x0600BB81 RID: 48001 RVA: 0x00299C59 File Offset: 0x00297E59
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Please provide appointment subject")]
		public string AdvancedSubjectRequired
		{
			get
			{
				return this.GetString("AdvancedSubjectRequired");
			}
			set
			{
				this.SetString("AdvancedSubjectRequired", value);
			}
		}

		// Token: 0x17003CA1 RID: 15521
		// (get) Token: 0x0600BB82 RID: 48002 RVA: 0x00299C67 File Offset: 0x00297E67
		// (set) Token: 0x0600BB83 RID: 48003 RVA: 0x00299C74 File Offset: 0x00297E74
		[Localizable(true)]
		[DefaultValue("Start time must be before end time")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string AdvancedStartTimeBeforeEndTime
		{
			get
			{
				return this.GetString("AdvancedStartTimeBeforeEndTime");
			}
			set
			{
				this.SetString("AdvancedStartTimeBeforeEndTime", value);
			}
		}

		// Token: 0x17003CA2 RID: 15522
		// (get) Token: 0x0600BB84 RID: 48004 RVA: 0x00299C82 File Offset: 0x00297E82
		// (set) Token: 0x0600BB85 RID: 48005 RVA: 0x00299C8F File Offset: 0x00297E8F
		[ScriptIgnore]
		[DefaultValue("Start time is required")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AdvancedStartTimeRequired
		{
			get
			{
				return this.GetString("AdvancedStartTimeRequired");
			}
			set
			{
				this.SetString("AdvancedStartTimeRequired", value);
			}
		}

		// Token: 0x17003CA3 RID: 15523
		// (get) Token: 0x0600BB86 RID: 48006 RVA: 0x00299C9D File Offset: 0x00297E9D
		// (set) Token: 0x0600BB87 RID: 48007 RVA: 0x00299CAA File Offset: 0x00297EAA
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Start date is required")]
		[ScriptIgnore]
		public string AdvancedStartDateRequired
		{
			get
			{
				return this.GetString("AdvancedStartDateRequired");
			}
			set
			{
				this.SetString("AdvancedStartDateRequired", value);
			}
		}

		// Token: 0x17003CA4 RID: 15524
		// (get) Token: 0x0600BB88 RID: 48008 RVA: 0x00299CB8 File Offset: 0x00297EB8
		// (set) Token: 0x0600BB89 RID: 48009 RVA: 0x00299CC5 File Offset: 0x00297EC5
		[NotifyParentProperty(true)]
		[DefaultValue("End time is required")]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedEndTimeRequired
		{
			get
			{
				return this.GetString("AdvancedEndTimeRequired");
			}
			set
			{
				this.SetString("AdvancedEndTimeRequired", value);
			}
		}

		// Token: 0x17003CA5 RID: 15525
		// (get) Token: 0x0600BB8A RID: 48010 RVA: 0x00299CD3 File Offset: 0x00297ED3
		// (set) Token: 0x0600BB8B RID: 48011 RVA: 0x00299CE0 File Offset: 0x00297EE0
		[NotifyParentProperty(true)]
		[DefaultValue("End date is required")]
		[Localizable(true)]
		[ScriptIgnore]
		public string AdvancedEndDateRequired
		{
			get
			{
				return this.GetString("AdvancedEndDateRequired");
			}
			set
			{
				this.SetString("AdvancedEndDateRequired", value);
			}
		}

		// Token: 0x17003CA6 RID: 15526
		// (get) Token: 0x0600BB8C RID: 48012 RVA: 0x00299CEE File Offset: 0x00297EEE
		// (set) Token: 0x0600BB8D RID: 48013 RVA: 0x00299CFB File Offset: 0x00297EFB
		[DefaultValue("Invalid number")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		public string AdvancedInvalidNumber
		{
			get
			{
				return this.GetString("AdvancedInvalidNumber");
			}
			set
			{
				this.SetString("AdvancedInvalidNumber", value);
			}
		}

		// Token: 0x17003CA7 RID: 15527
		// (get) Token: 0x0600BB8E RID: 48014 RVA: 0x00299D09 File Offset: 0x00297F09
		// (set) Token: 0x0600BB8F RID: 48015 RVA: 0x00299D16 File Offset: 0x00297F16
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Working...")]
		public string AdvancedWorking
		{
			get
			{
				return this.GetString("AdvancedWorking");
			}
			set
			{
				this.SetString("AdvancedWorking", value);
			}
		}

		// Token: 0x17003CA8 RID: 15528
		// (get) Token: 0x0600BB90 RID: 48016 RVA: 0x00299D24 File Offset: 0x00297F24
		// (set) Token: 0x0600BB91 RID: 48017 RVA: 0x00299D31 File Offset: 0x00297F31
		[DefaultValue("Done")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AdvancedDone
		{
			get
			{
				return this.GetString("AdvancedDone");
			}
			set
			{
				this.SetString("AdvancedDone", value);
			}
		}

		// Token: 0x17003CA9 RID: 15529
		// (get) Token: 0x0600BB92 RID: 48018 RVA: 0x00299D3F File Offset: 0x00297F3F
		// (set) Token: 0x0600BB93 RID: 48019 RVA: 0x00299D4C File Offset: 0x00297F4C
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("New Appointment")]
		public string AdvancedNewAppointment
		{
			get
			{
				return this.GetString("AdvancedNewAppointment");
			}
			set
			{
				this.SetString("AdvancedNewAppointment", value);
			}
		}

		// Token: 0x17003CAA RID: 15530
		// (get) Token: 0x0600BB94 RID: 48020 RVA: 0x00299D5A File Offset: 0x00297F5A
		// (set) Token: 0x0600BB95 RID: 48021 RVA: 0x00299D67 File Offset: 0x00297F67
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Edit Appointment")]
		public string AdvancedEditAppointment
		{
			get
			{
				return this.GetString("AdvancedEditAppointment");
			}
			set
			{
				this.SetString("AdvancedEditAppointment", value);
			}
		}

		// Token: 0x17003CAB RID: 15531
		// (get) Token: 0x0600BB96 RID: 48022 RVA: 0x00299D75 File Offset: 0x00297F75
		// (set) Token: 0x0600BB97 RID: 48023 RVA: 0x00299D82 File Offset: 0x00297F82
		[Localizable(true)]
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		public string AdvancedClose
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

		// Token: 0x17003CAC RID: 15532
		// (get) Token: 0x0600BB98 RID: 48024 RVA: 0x00299D90 File Offset: 0x00297F90
		// (set) Token: 0x0600BB99 RID: 48025 RVA: 0x00299D9D File Offset: 0x00297F9D
		[Localizable(true)]
		[DefaultValue("Reminder")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003CAD RID: 15533
		// (get) Token: 0x0600BB9A RID: 48026 RVA: 0x00299DAB File Offset: 0x00297FAB
		// (set) Token: 0x0600BB9B RID: 48027 RVA: 0x00299DB8 File Offset: 0x00297FB8
		[DefaultValue("Reminders")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003CAE RID: 15534
		// (get) Token: 0x0600BB9C RID: 48028 RVA: 0x00299DC6 File Offset: 0x00297FC6
		// (set) Token: 0x0600BB9D RID: 48029 RVA: 0x00299DD3 File Offset: 0x00297FD3
		[Localizable(true)]
		[DefaultValue("minute")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderMinute
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

		// Token: 0x17003CAF RID: 15535
		// (get) Token: 0x0600BB9E RID: 48030 RVA: 0x00299DE1 File Offset: 0x00297FE1
		// (set) Token: 0x0600BB9F RID: 48031 RVA: 0x00299DEE File Offset: 0x00297FEE
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("minutes")]
		[ScriptIgnore]
		public string ReminderMinutes
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

		// Token: 0x17003CB0 RID: 15536
		// (get) Token: 0x0600BBA0 RID: 48032 RVA: 0x00299DFC File Offset: 0x00297FFC
		// (set) Token: 0x0600BBA1 RID: 48033 RVA: 0x00299E09 File Offset: 0x00298009
		[Localizable(true)]
		[DefaultValue("hour")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderHour
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

		// Token: 0x17003CB1 RID: 15537
		// (get) Token: 0x0600BBA2 RID: 48034 RVA: 0x00299E17 File Offset: 0x00298017
		// (set) Token: 0x0600BBA3 RID: 48035 RVA: 0x00299E24 File Offset: 0x00298024
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("hours")]
		public string ReminderHours
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

		// Token: 0x17003CB2 RID: 15538
		// (get) Token: 0x0600BBA4 RID: 48036 RVA: 0x00299E32 File Offset: 0x00298032
		// (set) Token: 0x0600BBA5 RID: 48037 RVA: 0x00299E3F File Offset: 0x0029803F
		[DefaultValue("day")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderDay
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

		// Token: 0x17003CB3 RID: 15539
		// (get) Token: 0x0600BBA6 RID: 48038 RVA: 0x00299E4D File Offset: 0x0029804D
		// (set) Token: 0x0600BBA7 RID: 48039 RVA: 0x00299E5A File Offset: 0x0029805A
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("days")]
		[ScriptIgnore]
		public string ReminderDays
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

		// Token: 0x17003CB4 RID: 15540
		// (get) Token: 0x0600BBA8 RID: 48040 RVA: 0x00299E68 File Offset: 0x00298068
		// (set) Token: 0x0600BBA9 RID: 48041 RVA: 0x00299E75 File Offset: 0x00298075
		[Localizable(true)]
		[DefaultValue("week")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderWeek
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

		// Token: 0x17003CB5 RID: 15541
		// (get) Token: 0x0600BBAA RID: 48042 RVA: 0x00299E83 File Offset: 0x00298083
		// (set) Token: 0x0600BBAB RID: 48043 RVA: 0x00299E90 File Offset: 0x00298090
		[DefaultValue("weeks")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ReminderWeeks
		{
			get
			{
				return this.GetString("ReminderWeeks");
			}
			set
			{
				this.SetString("ReminderWeeks", value);
			}
		}

		// Token: 0x17003CB6 RID: 15542
		// (get) Token: 0x0600BBAC RID: 48044 RVA: 0x00299E9E File Offset: 0x0029809E
		// (set) Token: 0x0600BBAD RID: 48045 RVA: 0x00299EAB File Offset: 0x002980AB
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("None")]
		[ScriptIgnore]
		public string ReminderNone
		{
			get
			{
				return this.GetString("ReminderNone");
			}
			set
			{
				this.SetString("ReminderNone", value);
			}
		}

		// Token: 0x17003CB7 RID: 15543
		// (get) Token: 0x0600BBAE RID: 48046 RVA: 0x00299EB9 File Offset: 0x002980B9
		// (set) Token: 0x0600BBAF RID: 48047 RVA: 0x00299EC6 File Offset: 0x002980C6
		[Localizable(true)]
		[DefaultValue("before start")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderBeforeStart
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

		// Token: 0x17003CB8 RID: 15544
		// (get) Token: 0x0600BBB0 RID: 48048 RVA: 0x00299ED4 File Offset: 0x002980D4
		// (set) Token: 0x0600BBB1 RID: 48049 RVA: 0x00299EE1 File Offset: 0x002980E1
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("Snooze")]
		public string ReminderSnooze
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

		// Token: 0x17003CB9 RID: 15545
		// (get) Token: 0x0600BBB2 RID: 48050 RVA: 0x00299EEF File Offset: 0x002980EF
		// (set) Token: 0x0600BBB3 RID: 48051 RVA: 0x00299EFC File Offset: 0x002980FC
		[DefaultValue("Dismiss")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderDismiss
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

		// Token: 0x17003CBA RID: 15546
		// (get) Token: 0x0600BBB4 RID: 48052 RVA: 0x00299F0A File Offset: 0x0029810A
		// (set) Token: 0x0600BBB5 RID: 48053 RVA: 0x00299F17 File Offset: 0x00298117
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Dismiss All")]
		[ScriptIgnore]
		public string ReminderDismissAll
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

		// Token: 0x17003CBB RID: 15547
		// (get) Token: 0x0600BBB6 RID: 48054 RVA: 0x00299F25 File Offset: 0x00298125
		// (set) Token: 0x0600BBB7 RID: 48055 RVA: 0x00299F32 File Offset: 0x00298132
		[Localizable(true)]
		[DefaultValue("Open Item")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string ReminderOpenItem
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

		// Token: 0x17003CBC RID: 15548
		// (get) Token: 0x0600BBB8 RID: 48056 RVA: 0x00299F40 File Offset: 0x00298140
		// (set) Token: 0x0600BBB9 RID: 48057 RVA: 0x00299F4D File Offset: 0x0029814D
		[ScriptIgnore]
		[DefaultValue("Click Snooze to be reminded again in:")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ReminderSnoozeHint
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

		// Token: 0x17003CBD RID: 15549
		// (get) Token: 0x0600BBBA RID: 48058 RVA: 0x00299F5B File Offset: 0x0029815B
		// (set) Token: 0x0600BBBB RID: 48059 RVA: 0x00299F68 File Offset: 0x00298168
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("due in")]
		public string ReminderDueIn
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

		// Token: 0x17003CBE RID: 15550
		// (get) Token: 0x0600BBBC RID: 48060 RVA: 0x00299F76 File Offset: 0x00298176
		// (set) Token: 0x0600BBBD RID: 48061 RVA: 0x00299F83 File Offset: 0x00298183
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("overdue")]
		public string ReminderOverdue
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

		// Token: 0x17003CBF RID: 15551
		// (get) Token: 0x0600BBBE RID: 48062 RVA: 0x00299F91 File Offset: 0x00298191
		// (set) Token: 0x0600BBBF RID: 48063 RVA: 0x00299F9E File Offset: 0x0029819E
		[DefaultValue("Editing a recurring appointment")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceEditTitle
		{
			get
			{
				return this.GetString("ConfirmRecurrenceEditTitle");
			}
			set
			{
				this.SetString("ConfirmRecurrenceEditTitle", value);
			}
		}

		// Token: 0x17003CC0 RID: 15552
		// (get) Token: 0x0600BBC0 RID: 48064 RVA: 0x00299FAC File Offset: 0x002981AC
		// (set) Token: 0x0600BBC1 RID: 48065 RVA: 0x00299FB9 File Offset: 0x002981B9
		[NotifyParentProperty(true)]
		[DefaultValue("Edit only this occurrence.")]
		[Localizable(true)]
		public string ConfirmRecurrenceEditOccurrence
		{
			get
			{
				return this.GetString("ConfirmRecurrenceEditOccurrence");
			}
			set
			{
				this.SetString("ConfirmRecurrenceEditOccurrence", value);
			}
		}

		// Token: 0x17003CC1 RID: 15553
		// (get) Token: 0x0600BBC2 RID: 48066 RVA: 0x00299FC7 File Offset: 0x002981C7
		// (set) Token: 0x0600BBC3 RID: 48067 RVA: 0x00299FD4 File Offset: 0x002981D4
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Edit the series.")]
		public string ConfirmRecurrenceEditSeries
		{
			get
			{
				return this.GetString("ConfirmRecurrenceEditSeries");
			}
			set
			{
				this.SetString("ConfirmRecurrenceEditSeries", value);
			}
		}

		// Token: 0x17003CC2 RID: 15554
		// (get) Token: 0x0600BBC4 RID: 48068 RVA: 0x00299FE2 File Offset: 0x002981E2
		// (set) Token: 0x0600BBC5 RID: 48069 RVA: 0x00299FEF File Offset: 0x002981EF
		[DefaultValue("Resizing a recurring appointment")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceResizeTitle
		{
			get
			{
				return this.GetString("ConfirmRecurrenceResizeTitle");
			}
			set
			{
				this.SetString("ConfirmRecurrenceResizeTitle", value);
			}
		}

		// Token: 0x17003CC3 RID: 15555
		// (get) Token: 0x0600BBC6 RID: 48070 RVA: 0x00299FFD File Offset: 0x002981FD
		// (set) Token: 0x0600BBC7 RID: 48071 RVA: 0x0029A00A File Offset: 0x0029820A
		[Localizable(true)]
		[DefaultValue("Resize only this occurrence.")]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceResizeOccurrence
		{
			get
			{
				return this.GetString("ConfirmRecurrenceResizeOccurrence");
			}
			set
			{
				this.SetString("ConfirmRecurrenceResizeOccurrence", value);
			}
		}

		// Token: 0x17003CC4 RID: 15556
		// (get) Token: 0x0600BBC8 RID: 48072 RVA: 0x0029A018 File Offset: 0x00298218
		// (set) Token: 0x0600BBC9 RID: 48073 RVA: 0x0029A025 File Offset: 0x00298225
		[DefaultValue("Resize the series.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ConfirmRecurrenceResizeSeries
		{
			get
			{
				return this.GetString("ConfirmRecurrenceResizeSeries");
			}
			set
			{
				this.SetString("ConfirmRecurrenceResizeSeries", value);
			}
		}

		// Token: 0x17003CC5 RID: 15557
		// (get) Token: 0x0600BBCA RID: 48074 RVA: 0x0029A033 File Offset: 0x00298233
		// (set) Token: 0x0600BBCB RID: 48075 RVA: 0x0029A040 File Offset: 0x00298240
		[NotifyParentProperty(true)]
		[DefaultValue("Deleting a recurring appointment")]
		[Localizable(true)]
		public string ConfirmRecurrenceDeleteTitle
		{
			get
			{
				return this.GetString("ConfirmRecurrenceDeleteTitle");
			}
			set
			{
				this.SetString("ConfirmRecurrenceDeleteTitle", value);
			}
		}

		// Token: 0x17003CC6 RID: 15558
		// (get) Token: 0x0600BBCC RID: 48076 RVA: 0x0029A04E File Offset: 0x0029824E
		// (set) Token: 0x0600BBCD RID: 48077 RVA: 0x0029A05B File Offset: 0x0029825B
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Delete only this occurrence.")]
		public string ConfirmRecurrenceDeleteOccurrence
		{
			get
			{
				return this.GetString("ConfirmRecurrenceDeleteOccurrence");
			}
			set
			{
				this.SetString("ConfirmRecurrenceDeleteOccurrence", value);
			}
		}

		// Token: 0x17003CC7 RID: 15559
		// (get) Token: 0x0600BBCE RID: 48078 RVA: 0x0029A069 File Offset: 0x00298269
		// (set) Token: 0x0600BBCF RID: 48079 RVA: 0x0029A076 File Offset: 0x00298276
		[DefaultValue("Delete the series.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceDeleteSeries
		{
			get
			{
				return this.GetString("ConfirmRecurrenceDeleteSeries");
			}
			set
			{
				this.SetString("ConfirmRecurrenceDeleteSeries", value);
			}
		}

		// Token: 0x17003CC8 RID: 15560
		// (get) Token: 0x0600BBD0 RID: 48080 RVA: 0x0029A084 File Offset: 0x00298284
		// (set) Token: 0x0600BBD1 RID: 48081 RVA: 0x0029A091 File Offset: 0x00298291
		[Localizable(true)]
		[DefaultValue("Confirm delete")]
		[NotifyParentProperty(true)]
		public string ConfirmDeleteTitle
		{
			get
			{
				return this.GetString("ConfirmDeleteTitle");
			}
			set
			{
				this.SetString("ConfirmDeleteTitle", value);
			}
		}

		// Token: 0x17003CC9 RID: 15561
		// (get) Token: 0x0600BBD2 RID: 48082 RVA: 0x0029A09F File Offset: 0x0029829F
		// (set) Token: 0x0600BBD3 RID: 48083 RVA: 0x0029A0AC File Offset: 0x002982AC
		[Localizable(true)]
		[DefaultValue("Are you sure you want to delete this appointment?")]
		[NotifyParentProperty(true)]
		public string ConfirmDeleteText
		{
			get
			{
				return this.GetString("ConfirmDeleteText");
			}
			set
			{
				this.SetString("ConfirmDeleteText", value);
			}
		}

		// Token: 0x17003CCA RID: 15562
		// (get) Token: 0x0600BBD4 RID: 48084 RVA: 0x0029A0BA File Offset: 0x002982BA
		// (set) Token: 0x0600BBD5 RID: 48085 RVA: 0x0029A0C7 File Offset: 0x002982C7
		[DefaultValue("Moving a recurring appointment")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceMoveTitle
		{
			get
			{
				return this.GetString("ConfirmRecurrenceMoveTitle");
			}
			set
			{
				this.SetString("ConfirmRecurrenceMoveTitle", value);
			}
		}

		// Token: 0x17003CCB RID: 15563
		// (get) Token: 0x0600BBD6 RID: 48086 RVA: 0x0029A0D5 File Offset: 0x002982D5
		// (set) Token: 0x0600BBD7 RID: 48087 RVA: 0x0029A0E2 File Offset: 0x002982E2
		[NotifyParentProperty(true)]
		[DefaultValue("Move only this occurrence.")]
		[Localizable(true)]
		public string ConfirmRecurrenceMoveOccurrence
		{
			get
			{
				return this.GetString("ConfirmRecurrenceMoveOccurrence");
			}
			set
			{
				this.SetString("ConfirmRecurrenceMoveOccurrence", value);
			}
		}

		// Token: 0x17003CCC RID: 15564
		// (get) Token: 0x0600BBD8 RID: 48088 RVA: 0x0029A0F0 File Offset: 0x002982F0
		// (set) Token: 0x0600BBD9 RID: 48089 RVA: 0x0029A0FD File Offset: 0x002982FD
		[DefaultValue("Move the series.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ConfirmRecurrenceMoveSeries
		{
			get
			{
				return this.GetString("ConfirmRecurrenceMoveSeries");
			}
			set
			{
				this.SetString("ConfirmRecurrenceMoveSeries", value);
			}
		}

		// Token: 0x17003CCD RID: 15565
		// (get) Token: 0x0600BBDA RID: 48090 RVA: 0x0029A10B File Offset: 0x0029830B
		// (set) Token: 0x0600BBDB RID: 48091 RVA: 0x0029A118 File Offset: 0x00298318
		[DefaultValue("OK")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ConfirmOK
		{
			get
			{
				return this.GetString("ConfirmOK");
			}
			set
			{
				this.SetString("ConfirmOK", value);
			}
		}

		// Token: 0x17003CCE RID: 15566
		// (get) Token: 0x0600BBDC RID: 48092 RVA: 0x0029A126 File Offset: 0x00298326
		// (set) Token: 0x0600BBDD RID: 48093 RVA: 0x0029A133 File Offset: 0x00298333
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ConfirmCancel
		{
			get
			{
				return this.GetString("ConfirmCancel");
			}
			set
			{
				this.SetString("ConfirmCancel", value);
			}
		}

		// Token: 0x17003CCF RID: 15567
		// (get) Token: 0x0600BBDE RID: 48094 RVA: 0x0029A141 File Offset: 0x00298341
		// (set) Token: 0x0600BBDF RID: 48095 RVA: 0x0029A14E File Offset: 0x0029834E
		[NotifyParentProperty(true)]
		[DefaultValue("Confirm reset exceptions")]
		[Localizable(true)]
		public string ConfirmResetExceptionsTitle
		{
			get
			{
				return this.GetString("ConfirmResetExceptionsTitle");
			}
			set
			{
				this.SetString("ConfirmResetExceptionsTitle", value);
			}
		}

		// Token: 0x17003CD0 RID: 15568
		// (get) Token: 0x0600BBE0 RID: 48096 RVA: 0x0029A15C File Offset: 0x0029835C
		// (set) Token: 0x0600BBE1 RID: 48097 RVA: 0x0029A169 File Offset: 0x00298369
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Do you want to delete all existing recurrence exceptions?")]
		public string ConfirmResetExceptionsText
		{
			get
			{
				return this.GetString("ConfirmResetExceptionsText");
			}
			set
			{
				this.SetString("ConfirmResetExceptionsText", value);
			}
		}

		// Token: 0x17003CD1 RID: 15569
		// (get) Token: 0x0600BBE2 RID: 48098 RVA: 0x0029A177 File Offset: 0x00298377
		// (set) Token: 0x0600BBE3 RID: 48099 RVA: 0x0029A184 File Offset: 0x00298384
		[Localizable(true)]
		[DefaultValue("Edit")]
		[NotifyParentProperty(true)]
		public string ContextMenuEdit
		{
			get
			{
				return this.GetString("ContextMenuEdit");
			}
			set
			{
				this.SetString("ContextMenuEdit", value);
			}
		}

		// Token: 0x17003CD2 RID: 15570
		// (get) Token: 0x0600BBE4 RID: 48100 RVA: 0x0029A192 File Offset: 0x00298392
		// (set) Token: 0x0600BBE5 RID: 48101 RVA: 0x0029A19F File Offset: 0x0029839F
		[NotifyParentProperty(true)]
		[DefaultValue("Delete")]
		[Localizable(true)]
		public string ContextMenuDelete
		{
			get
			{
				return this.GetString("ContextMenuDelete");
			}
			set
			{
				this.SetString("ContextMenuDelete", value);
			}
		}

		// Token: 0x17003CD3 RID: 15571
		// (get) Token: 0x0600BBE6 RID: 48102 RVA: 0x0029A1AD File Offset: 0x002983AD
		// (set) Token: 0x0600BBE7 RID: 48103 RVA: 0x0029A1BA File Offset: 0x002983BA
		[DefaultValue("New Appointment")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ContextMenuAddAppointment
		{
			get
			{
				return this.GetString("ContextMenuAddAppointment");
			}
			set
			{
				this.SetString("ContextMenuAddAppointment", value);
			}
		}

		// Token: 0x17003CD4 RID: 15572
		// (get) Token: 0x0600BBE8 RID: 48104 RVA: 0x0029A1C8 File Offset: 0x002983C8
		// (set) Token: 0x0600BBE9 RID: 48105 RVA: 0x0029A1D5 File Offset: 0x002983D5
		[DefaultValue("New Recurring Appointment")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ContextMenuAddRecurringAppointment
		{
			get
			{
				return this.GetString("ContextMenuAddRecurringAppointment");
			}
			set
			{
				this.SetString("ContextMenuAddRecurringAppointment", value);
			}
		}

		// Token: 0x17003CD5 RID: 15573
		// (get) Token: 0x0600BBEA RID: 48106 RVA: 0x0029A1E3 File Offset: 0x002983E3
		// (set) Token: 0x0600BBEB RID: 48107 RVA: 0x0029A1F0 File Offset: 0x002983F0
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Go to today")]
		public string ContextMenuGoToToday
		{
			get
			{
				return this.GetString("ContextMenuGoToToday");
			}
			set
			{
				this.SetString("ContextMenuGoToToday", value);
			}
		}

		// Token: 0x0600BBEC RID: 48108 RVA: 0x0029A1FE File Offset: 0x002983FE
		internal SchedulerStrings(LocalizationProvider provider) : base(provider)
		{
		}
	}
}
