using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A1B RID: 6683
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal class RadSchedulerRecurrenceEditorStrings : LocalizationStrings, IRecurrenceEditorStrings
	{
		// Token: 0x17004E60 RID: 20064
		// (get) Token: 0x06010331 RID: 66353 RVA: 0x003A017C File Offset: 0x0039E37C
		// (set) Token: 0x06010332 RID: 66354 RVA: 0x003A0189 File Offset: 0x0039E389
		[DefaultValue("Recurrence")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Recurrence
		{
			get
			{
				return this.GetString("Recurrence");
			}
			set
			{
				this.SetString("Recurrence", value);
			}
		}

		// Token: 0x17004E61 RID: 20065
		// (get) Token: 0x06010333 RID: 66355 RVA: 0x003A0197 File Offset: 0x0039E397
		// (set) Token: 0x06010334 RID: 66356 RVA: 0x003A01A4 File Offset: 0x0039E3A4
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("Repeat Appointment")]
		public string RepeatAppointment
		{
			get
			{
				return this.GetString("RepeatAppointment");
			}
			set
			{
				this.SetString("RepeatAppointment", value);
			}
		}

		// Token: 0x17004E62 RID: 20066
		// (get) Token: 0x06010335 RID: 66357 RVA: 0x003A01B2 File Offset: 0x0039E3B2
		// (set) Token: 0x06010336 RID: 66358 RVA: 0x003A01BF File Offset: 0x0039E3BF
		[DefaultValue("Repeat")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Repeat
		{
			get
			{
				return this.GetString("Repeat");
			}
			set
			{
				this.SetString("Repeat", value);
			}
		}

		// Token: 0x17004E63 RID: 20067
		// (get) Token: 0x06010337 RID: 66359 RVA: 0x003A01CD File Offset: 0x0039E3CD
		// (set) Token: 0x06010338 RID: 66360 RVA: 0x003A01DA File Offset: 0x0039E3DA
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[DefaultValue("Repeat On")]
		[Localizable(true)]
		public string RepeatOn
		{
			get
			{
				return this.GetString("RepeatOn");
			}
			set
			{
				this.SetString("RepeatOn", value);
			}
		}

		// Token: 0x17004E64 RID: 20068
		// (get) Token: 0x06010339 RID: 66361 RVA: 0x003A01E8 File Offset: 0x0039E3E8
		// (set) Token: 0x0601033A RID: 66362 RVA: 0x003A01F5 File Offset: 0x0039E3F5
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Repeat End")]
		public string RepeatEnd
		{
			get
			{
				return this.GetString("RepeatEnd");
			}
			set
			{
				this.SetString("RepeatEnd", value);
			}
		}

		// Token: 0x17004E65 RID: 20069
		// (get) Token: 0x0601033B RID: 66363 RVA: 0x003A0203 File Offset: 0x0039E403
		// (set) Token: 0x0601033C RID: 66364 RVA: 0x003A0210 File Offset: 0x0039E410
		[DefaultValue("Never")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Never
		{
			get
			{
				return this.GetString("Never");
			}
			set
			{
				this.SetString("Never", value);
			}
		}

		// Token: 0x17004E66 RID: 20070
		// (get) Token: 0x0601033D RID: 66365 RVA: 0x003A021E File Offset: 0x0039E41E
		// (set) Token: 0x0601033E RID: 66366 RVA: 0x003A022B File Offset: 0x0039E42B
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("After")]
		[Localizable(true)]
		public string After
		{
			get
			{
				return this.GetString("After");
			}
			set
			{
				this.SetString("After", value);
			}
		}

		// Token: 0x17004E67 RID: 20071
		// (get) Token: 0x0601033F RID: 66367 RVA: 0x003A0239 File Offset: 0x0039E439
		// (set) Token: 0x06010340 RID: 66368 RVA: 0x003A0246 File Offset: 0x0039E446
		[DefaultValue("On")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string On
		{
			get
			{
				return this.GetString("On");
			}
			set
			{
				this.SetString("On", value);
			}
		}

		// Token: 0x17004E68 RID: 20072
		// (get) Token: 0x06010341 RID: 66369 RVA: 0x003A0254 File Offset: 0x0039E454
		// (set) Token: 0x06010342 RID: 66370 RVA: 0x003A0261 File Offset: 0x0039E461
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Day of the month")]
		[Localizable(true)]
		public string DayOfMonth
		{
			get
			{
				return this.GetString("DayOfMonth");
			}
			set
			{
				this.SetString("DayOfMonth", value);
			}
		}

		// Token: 0x17004E69 RID: 20073
		// (get) Token: 0x06010343 RID: 66371 RVA: 0x003A026F File Offset: 0x0039E46F
		// (set) Token: 0x06010344 RID: 66372 RVA: 0x003A027C File Offset: 0x0039E47C
		[DefaultValue("Day of the week")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string DayOfWeek
		{
			get
			{
				return this.GetString("DayOfWeek");
			}
			set
			{
				this.SetString("DayOfWeek", value);
			}
		}

		// Token: 0x17004E6A RID: 20074
		// (get) Token: 0x06010345 RID: 66373 RVA: 0x003A028A File Offset: 0x0039E48A
		// (set) Token: 0x06010346 RID: 66374 RVA: 0x003A0297 File Offset: 0x0039E497
		[DefaultValue("Hourly")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Hourly
		{
			get
			{
				return this.GetString("Hourly");
			}
			set
			{
				this.SetString("Hourly", value);
			}
		}

		// Token: 0x17004E6B RID: 20075
		// (get) Token: 0x06010347 RID: 66375 RVA: 0x003A02A5 File Offset: 0x0039E4A5
		// (set) Token: 0x06010348 RID: 66376 RVA: 0x003A02B2 File Offset: 0x0039E4B2
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Daily")]
		[Localizable(true)]
		public string Daily
		{
			get
			{
				return this.GetString("Daily");
			}
			set
			{
				this.SetString("Daily", value);
			}
		}

		// Token: 0x17004E6C RID: 20076
		// (get) Token: 0x06010349 RID: 66377 RVA: 0x003A02C0 File Offset: 0x0039E4C0
		// (set) Token: 0x0601034A RID: 66378 RVA: 0x003A02CD File Offset: 0x0039E4CD
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Weekly")]
		[NotifyParentProperty(true)]
		public string Weekly
		{
			get
			{
				return this.GetString("Weekly");
			}
			set
			{
				this.SetString("Weekly", value);
			}
		}

		// Token: 0x17004E6D RID: 20077
		// (get) Token: 0x0601034B RID: 66379 RVA: 0x003A02DB File Offset: 0x0039E4DB
		// (set) Token: 0x0601034C RID: 66380 RVA: 0x003A02E8 File Offset: 0x0039E4E8
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Monthly")]
		[Localizable(true)]
		public string Monthly
		{
			get
			{
				return this.GetString("Monthly");
			}
			set
			{
				this.SetString("Monthly", value);
			}
		}

		// Token: 0x17004E6E RID: 20078
		// (get) Token: 0x0601034D RID: 66381 RVA: 0x003A02F6 File Offset: 0x0039E4F6
		// (set) Token: 0x0601034E RID: 66382 RVA: 0x003A0303 File Offset: 0x0039E503
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Yearly")]
		[NotifyParentProperty(true)]
		public string Yearly
		{
			get
			{
				return this.GetString("Yearly");
			}
			set
			{
				this.SetString("Yearly", value);
			}
		}

		// Token: 0x17004E6F RID: 20079
		// (get) Token: 0x0601034F RID: 66383 RVA: 0x003A0311 File Offset: 0x0039E511
		// (set) Token: 0x06010350 RID: 66384 RVA: 0x003A031E File Offset: 0x0039E51E
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Every")]
		[Localizable(true)]
		public string Every
		{
			get
			{
				return this.GetString("Every");
			}
			set
			{
				this.SetString("Every", value);
			}
		}

		// Token: 0x17004E70 RID: 20080
		// (get) Token: 0x06010351 RID: 66385 RVA: 0x003A032C File Offset: 0x0039E52C
		// (set) Token: 0x06010352 RID: 66386 RVA: 0x003A0339 File Offset: 0x0039E539
		[DefaultValue("hour(s)")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string Hours
		{
			get
			{
				return this.GetString("Hours");
			}
			set
			{
				this.SetString("Hours", value);
			}
		}

		// Token: 0x17004E71 RID: 20081
		// (get) Token: 0x06010353 RID: 66387 RVA: 0x003A0347 File Offset: 0x0039E547
		// (set) Token: 0x06010354 RID: 66388 RVA: 0x003A0354 File Offset: 0x0039E554
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("day(s)")]
		[Localizable(true)]
		public string Days
		{
			get
			{
				return this.GetString("Days");
			}
			set
			{
				this.SetString("Days", value);
			}
		}

		// Token: 0x17004E72 RID: 20082
		// (get) Token: 0x06010355 RID: 66389 RVA: 0x003A0362 File Offset: 0x0039E562
		// (set) Token: 0x06010356 RID: 66390 RVA: 0x003A036F File Offset: 0x0039E56F
		[DefaultValue("week(s) on")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Weeks
		{
			get
			{
				return this.GetString("Weeks");
			}
			set
			{
				this.SetString("Weeks", value);
			}
		}

		// Token: 0x17004E73 RID: 20083
		// (get) Token: 0x06010357 RID: 66391 RVA: 0x003A037D File Offset: 0x0039E57D
		// (set) Token: 0x06010358 RID: 66392 RVA: 0x003A038A File Offset: 0x0039E58A
		[DefaultValue("month(s)")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Months
		{
			get
			{
				return this.GetString("Months");
			}
			set
			{
				this.SetString("Months", value);
			}
		}

		// Token: 0x17004E74 RID: 20084
		// (get) Token: 0x06010359 RID: 66393 RVA: 0x003A0398 File Offset: 0x0039E598
		// (set) Token: 0x0601035A RID: 66394 RVA: 0x003A03A5 File Offset: 0x0039E5A5
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("year(s)")]
		[Localizable(true)]
		public string Years
		{
			get
			{
				return this.GetString("Years");
			}
			set
			{
				this.SetString("Years", value);
			}
		}

		// Token: 0x17004E75 RID: 20085
		// (get) Token: 0x0601035B RID: 66395 RVA: 0x003A03B3 File Offset: 0x0039E5B3
		// (set) Token: 0x0601035C RID: 66396 RVA: 0x003A03C0 File Offset: 0x0039E5C0
		[Localizable(true)]
		[DefaultValue("Every weekday")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string EveryWeekday
		{
			get
			{
				return this.GetString("EveryWeekday");
			}
			set
			{
				this.SetString("EveryWeekday", value);
			}
		}

		// Token: 0x17004E76 RID: 20086
		// (get) Token: 0x0601035D RID: 66397 RVA: 0x003A03CE File Offset: 0x0039E5CE
		// (set) Token: 0x0601035E RID: 66398 RVA: 0x003A03DB File Offset: 0x0039E5DB
		[DefaultValue("Every working day")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string EveryWorkingDay
		{
			get
			{
				return this.GetString("EveryWorkingDay");
			}
			set
			{
				this.SetString("EveryWorkingDay", value);
			}
		}

		// Token: 0x17004E77 RID: 20087
		// (get) Token: 0x0601035F RID: 66399 RVA: 0x003A03E9 File Offset: 0x0039E5E9
		// (set) Token: 0x06010360 RID: 66400 RVA: 0x003A03F6 File Offset: 0x0039E5F6
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Recur every")]
		[NotifyParentProperty(true)]
		public string RecurEvery
		{
			get
			{
				return this.GetString("RecurEvery");
			}
			set
			{
				this.SetString("RecurEvery", value);
			}
		}

		// Token: 0x17004E78 RID: 20088
		// (get) Token: 0x06010361 RID: 66401 RVA: 0x003A0404 File Offset: 0x0039E604
		// (set) Token: 0x06010362 RID: 66402 RVA: 0x003A0411 File Offset: 0x0039E611
		[Localizable(true)]
		[DefaultValue("Day")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Day
		{
			get
			{
				return this.GetString("Day");
			}
			set
			{
				this.SetString("Day", value);
			}
		}

		// Token: 0x17004E79 RID: 20089
		// (get) Token: 0x06010363 RID: 66403 RVA: 0x003A041F File Offset: 0x0039E61F
		// (set) Token: 0x06010364 RID: 66404 RVA: 0x003A042C File Offset: 0x0039E62C
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("of every")]
		public string OfEvery
		{
			get
			{
				return this.GetString("OfEvery");
			}
			set
			{
				this.SetString("OfEvery", value);
			}
		}

		// Token: 0x17004E7A RID: 20090
		// (get) Token: 0x06010365 RID: 66405 RVA: 0x003A043A File Offset: 0x0039E63A
		// (set) Token: 0x06010366 RID: 66406 RVA: 0x003A0447 File Offset: 0x0039E647
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("first")]
		public string First
		{
			get
			{
				return this.GetString("First");
			}
			set
			{
				this.SetString("First", value);
			}
		}

		// Token: 0x17004E7B RID: 20091
		// (get) Token: 0x06010367 RID: 66407 RVA: 0x003A0455 File Offset: 0x0039E655
		// (set) Token: 0x06010368 RID: 66408 RVA: 0x003A0462 File Offset: 0x0039E662
		[ScriptIgnore]
		[DefaultValue("second")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Second
		{
			get
			{
				return this.GetString("Second");
			}
			set
			{
				this.SetString("Second", value);
			}
		}

		// Token: 0x17004E7C RID: 20092
		// (get) Token: 0x06010369 RID: 66409 RVA: 0x003A0470 File Offset: 0x0039E670
		// (set) Token: 0x0601036A RID: 66410 RVA: 0x003A047D File Offset: 0x0039E67D
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("third")]
		public string Third
		{
			get
			{
				return this.GetString("Third");
			}
			set
			{
				this.SetString("Third", value);
			}
		}

		// Token: 0x17004E7D RID: 20093
		// (get) Token: 0x0601036B RID: 66411 RVA: 0x003A048B File Offset: 0x0039E68B
		// (set) Token: 0x0601036C RID: 66412 RVA: 0x003A0498 File Offset: 0x0039E698
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("fourth")]
		public string Fourth
		{
			get
			{
				return this.GetString("Fourth");
			}
			set
			{
				this.SetString("Fourth", value);
			}
		}

		// Token: 0x17004E7E RID: 20094
		// (get) Token: 0x0601036D RID: 66413 RVA: 0x003A04A6 File Offset: 0x0039E6A6
		// (set) Token: 0x0601036E RID: 66414 RVA: 0x003A04B3 File Offset: 0x0039E6B3
		[Localizable(true)]
		[DefaultValue("last")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Last
		{
			get
			{
				return this.GetString("Last");
			}
			set
			{
				this.SetString("Last", value);
			}
		}

		// Token: 0x17004E7F RID: 20095
		// (get) Token: 0x0601036F RID: 66415 RVA: 0x003A04C1 File Offset: 0x0039E6C1
		// (set) Token: 0x06010370 RID: 66416 RVA: 0x003A04CE File Offset: 0x0039E6CE
		[DefaultValue("day")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string MaskDay
		{
			get
			{
				return this.GetString("MaskDay");
			}
			set
			{
				this.SetString("MaskDay", value);
			}
		}

		// Token: 0x17004E80 RID: 20096
		// (get) Token: 0x06010371 RID: 66417 RVA: 0x003A04DC File Offset: 0x0039E6DC
		// (set) Token: 0x06010372 RID: 66418 RVA: 0x003A04E9 File Offset: 0x0039E6E9
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("weekday")]
		[NotifyParentProperty(true)]
		public string MaskWeekday
		{
			get
			{
				return this.GetString("MaskWeekday");
			}
			set
			{
				this.SetString("MaskWeekday", value);
			}
		}

		// Token: 0x17004E81 RID: 20097
		// (get) Token: 0x06010373 RID: 66419 RVA: 0x003A04F7 File Offset: 0x0039E6F7
		// (set) Token: 0x06010374 RID: 66420 RVA: 0x003A0504 File Offset: 0x0039E704
		[Localizable(true)]
		[DefaultValue("weekend day")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string MaskWeekendDay
		{
			get
			{
				return this.GetString("MaskWeekendDay");
			}
			set
			{
				this.SetString("MaskWeekendDay", value);
			}
		}

		// Token: 0x17004E82 RID: 20098
		// (get) Token: 0x06010375 RID: 66421 RVA: 0x003A0512 File Offset: 0x0039E712
		// (set) Token: 0x06010376 RID: 66422 RVA: 0x003A051F File Offset: 0x0039E71F
		[DefaultValue("The")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string The
		{
			get
			{
				return this.GetString("The");
			}
			set
			{
				this.SetString("The", value);
			}
		}

		// Token: 0x17004E83 RID: 20099
		// (get) Token: 0x06010377 RID: 66423 RVA: 0x003A052D File Offset: 0x0039E72D
		// (set) Token: 0x06010378 RID: 66424 RVA: 0x003A053A File Offset: 0x0039E73A
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("of")]
		public string Of
		{
			get
			{
				return this.GetString("Of");
			}
			set
			{
				this.SetString("Of", value);
			}
		}

		// Token: 0x17004E84 RID: 20100
		// (get) Token: 0x06010379 RID: 66425 RVA: 0x003A0548 File Offset: 0x0039E748
		// (set) Token: 0x0601037A RID: 66426 RVA: 0x003A0555 File Offset: 0x0039E755
		[DefaultValue("No end date")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string NoEndDate
		{
			get
			{
				return this.GetString("NoEndDate");
			}
			set
			{
				this.SetString("NoEndDate", value);
			}
		}

		// Token: 0x17004E85 RID: 20101
		// (get) Token: 0x0601037B RID: 66427 RVA: 0x003A0563 File Offset: 0x0039E763
		// (set) Token: 0x0601037C RID: 66428 RVA: 0x003A0570 File Offset: 0x0039E770
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("End after")]
		public string EndAfter
		{
			get
			{
				return this.GetString("EndAfter");
			}
			set
			{
				this.SetString("EndAfter", value);
			}
		}

		// Token: 0x17004E86 RID: 20102
		// (get) Token: 0x0601037D RID: 66429 RVA: 0x003A057E File Offset: 0x0039E77E
		// (set) Token: 0x0601037E RID: 66430 RVA: 0x003A058B File Offset: 0x0039E78B
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("End by")]
		[NotifyParentProperty(true)]
		public string EndByThisDate
		{
			get
			{
				return this.GetString("EndByThisDate");
			}
			set
			{
				this.SetString("EndByThisDate", value);
			}
		}

		// Token: 0x17004E87 RID: 20103
		// (get) Token: 0x0601037F RID: 66431 RVA: 0x003A0599 File Offset: 0x0039E799
		// (set) Token: 0x06010380 RID: 66432 RVA: 0x003A05A6 File Offset: 0x0039E7A6
		[DefaultValue("occurrences")]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		public string Occurrences
		{
			get
			{
				return this.GetString("Occurrences");
			}
			set
			{
				this.SetString("Occurrences", value);
			}
		}

		// Token: 0x17004E88 RID: 20104
		// (get) Token: 0x06010381 RID: 66433 RVA: 0x003A05B4 File Offset: 0x0039E7B4
		// (set) Token: 0x06010382 RID: 66434 RVA: 0x003A05C1 File Offset: 0x0039E7C1
		[DefaultValue("OK")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string CalendarOK
		{
			get
			{
				return this.GetString("CalendarOK");
			}
			set
			{
				this.SetString("CalendarOK", value);
			}
		}

		// Token: 0x17004E89 RID: 20105
		// (get) Token: 0x06010383 RID: 66435 RVA: 0x003A05CF File Offset: 0x0039E7CF
		// (set) Token: 0x06010384 RID: 66436 RVA: 0x003A05DC File Offset: 0x0039E7DC
		[Localizable(true)]
		[DefaultValue("Cancel")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string CalendarCancel
		{
			get
			{
				return this.GetString("CalendarCancel");
			}
			set
			{
				this.SetString("CalendarCancel", value);
			}
		}

		// Token: 0x17004E8A RID: 20106
		// (get) Token: 0x06010385 RID: 66437 RVA: 0x003A05EA File Offset: 0x0039E7EA
		// (set) Token: 0x06010386 RID: 66438 RVA: 0x003A05F7 File Offset: 0x0039E7F7
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Today")]
		public string CalendarToday
		{
			get
			{
				return this.GetString("CalendarToday");
			}
			set
			{
				this.SetString("CalendarToday", value);
			}
		}

		// Token: 0x17004E8B RID: 20107
		// (get) Token: 0x06010387 RID: 66439 RVA: 0x003A0605 File Offset: 0x0039E805
		// (set) Token: 0x06010388 RID: 66440 RVA: 0x003A0612 File Offset: 0x0039E812
		[DefaultValue("Save")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
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

		// Token: 0x17004E8C RID: 20108
		// (get) Token: 0x06010389 RID: 66441 RVA: 0x003A0620 File Offset: 0x0039E820
		// (set) Token: 0x0601038A RID: 66442 RVA: 0x003A062D File Offset: 0x0039E82D
		[DefaultValue("Cancel")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
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

		// Token: 0x0601038B RID: 66443 RVA: 0x003A063B File Offset: 0x0039E83B
		internal RadSchedulerRecurrenceEditorStrings(LocalizationProvider provider) : base(provider)
		{
		}
	}
}
