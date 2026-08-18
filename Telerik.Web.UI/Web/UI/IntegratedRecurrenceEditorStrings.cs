using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A19 RID: 6681
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal class IntegratedRecurrenceEditorStrings : LocalizationStrings, IRecurrenceEditorStrings
	{
		// Token: 0x17004E31 RID: 20017
		// (get) Token: 0x060102D1 RID: 66257 RVA: 0x0039FBD0 File Offset: 0x0039DDD0
		// (set) Token: 0x060102D2 RID: 66258 RVA: 0x0039FBDD File Offset: 0x0039DDDD
		[DefaultValue("Recurrence")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Recurrence
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

		// Token: 0x17004E32 RID: 20018
		// (get) Token: 0x060102D3 RID: 66259 RVA: 0x0039FBEB File Offset: 0x0039DDEB
		// (set) Token: 0x060102D4 RID: 66260 RVA: 0x0039FBF8 File Offset: 0x0039DDF8
		[NotifyParentProperty(true)]
		[DefaultValue("Repeat Appointment")]
		[ScriptIgnore]
		[Localizable(true)]
		public string RepeatAppointment
		{
			get
			{
				return this.GetString("AdvancedRepeatAppointment");
			}
			set
			{
				this.SetString("AdvancedRepeatAppointment", value);
			}
		}

		// Token: 0x17004E33 RID: 20019
		// (get) Token: 0x060102D5 RID: 66261 RVA: 0x0039FC06 File Offset: 0x0039DE06
		// (set) Token: 0x060102D6 RID: 66262 RVA: 0x0039FC13 File Offset: 0x0039DE13
		[DefaultValue("Repeat")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string Repeat
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

		// Token: 0x17004E34 RID: 20020
		// (get) Token: 0x060102D7 RID: 66263 RVA: 0x0039FC21 File Offset: 0x0039DE21
		// (set) Token: 0x060102D8 RID: 66264 RVA: 0x0039FC2E File Offset: 0x0039DE2E
		[DefaultValue("Repeat On")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string RepeatOn
		{
			get
			{
				return this.GetString("AdvancedRepeatOn");
			}
			set
			{
				this.SetString("AdvancedRepeatOn", value);
			}
		}

		// Token: 0x17004E35 RID: 20021
		// (get) Token: 0x060102D9 RID: 66265 RVA: 0x0039FC3C File Offset: 0x0039DE3C
		// (set) Token: 0x060102DA RID: 66266 RVA: 0x0039FC49 File Offset: 0x0039DE49
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Repeat End")]
		public string RepeatEnd
		{
			get
			{
				return this.GetString("AdvancedRepeatEnd");
			}
			set
			{
				this.SetString("AdvancedRepeatEnd", value);
			}
		}

		// Token: 0x17004E36 RID: 20022
		// (get) Token: 0x060102DB RID: 66267 RVA: 0x0039FC57 File Offset: 0x0039DE57
		// (set) Token: 0x060102DC RID: 66268 RVA: 0x0039FC64 File Offset: 0x0039DE64
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Never")]
		public string Never
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

		// Token: 0x17004E37 RID: 20023
		// (get) Token: 0x060102DD RID: 66269 RVA: 0x0039FC72 File Offset: 0x0039DE72
		// (set) Token: 0x060102DE RID: 66270 RVA: 0x0039FC7F File Offset: 0x0039DE7F
		[Localizable(true)]
		[ScriptIgnore]
		[DefaultValue("After")]
		[NotifyParentProperty(true)]
		public string After
		{
			get
			{
				return this.GetString("AdvancedAfter");
			}
			set
			{
				this.SetString("AdvancedAfter", value);
			}
		}

		// Token: 0x17004E38 RID: 20024
		// (get) Token: 0x060102DF RID: 66271 RVA: 0x0039FC8D File Offset: 0x0039DE8D
		// (set) Token: 0x060102E0 RID: 66272 RVA: 0x0039FC9A File Offset: 0x0039DE9A
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("On")]
		[ScriptIgnore]
		public string On
		{
			get
			{
				return this.GetString("AdvancedOn");
			}
			set
			{
				this.SetString("AdvancedOn", value);
			}
		}

		// Token: 0x17004E39 RID: 20025
		// (get) Token: 0x060102E1 RID: 66273 RVA: 0x0039FCA8 File Offset: 0x0039DEA8
		// (set) Token: 0x060102E2 RID: 66274 RVA: 0x0039FCB5 File Offset: 0x0039DEB5
		[DefaultValue("Day of the month")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string DayOfMonth
		{
			get
			{
				return this.GetString("AdvancedDayOfMonth");
			}
			set
			{
				this.SetString("AdvancedDayOfMonth", value);
			}
		}

		// Token: 0x17004E3A RID: 20026
		// (get) Token: 0x060102E3 RID: 66275 RVA: 0x0039FCC3 File Offset: 0x0039DEC3
		// (set) Token: 0x060102E4 RID: 66276 RVA: 0x0039FCD0 File Offset: 0x0039DED0
		[NotifyParentProperty(true)]
		[DefaultValue("Day of the week")]
		[Localizable(true)]
		[ScriptIgnore]
		public string DayOfWeek
		{
			get
			{
				return this.GetString("AdvancedDayOfWeek");
			}
			set
			{
				this.SetString("AdvancedDayOfWeek", value);
			}
		}

		// Token: 0x17004E3B RID: 20027
		// (get) Token: 0x060102E5 RID: 66277 RVA: 0x0039FCDE File Offset: 0x0039DEDE
		// (set) Token: 0x060102E6 RID: 66278 RVA: 0x0039FCEB File Offset: 0x0039DEEB
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Hourly")]
		[Localizable(true)]
		public string Hourly
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

		// Token: 0x17004E3C RID: 20028
		// (get) Token: 0x060102E7 RID: 66279 RVA: 0x0039FCF9 File Offset: 0x0039DEF9
		// (set) Token: 0x060102E8 RID: 66280 RVA: 0x0039FD06 File Offset: 0x0039DF06
		[DefaultValue("Daily")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string Daily
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

		// Token: 0x17004E3D RID: 20029
		// (get) Token: 0x060102E9 RID: 66281 RVA: 0x0039FD14 File Offset: 0x0039DF14
		// (set) Token: 0x060102EA RID: 66282 RVA: 0x0039FD21 File Offset: 0x0039DF21
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Weekly")]
		[ScriptIgnore]
		public string Weekly
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

		// Token: 0x17004E3E RID: 20030
		// (get) Token: 0x060102EB RID: 66283 RVA: 0x0039FD2F File Offset: 0x0039DF2F
		// (set) Token: 0x060102EC RID: 66284 RVA: 0x0039FD3C File Offset: 0x0039DF3C
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Monthly")]
		[NotifyParentProperty(true)]
		public string Monthly
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

		// Token: 0x17004E3F RID: 20031
		// (get) Token: 0x060102ED RID: 66285 RVA: 0x0039FD4A File Offset: 0x0039DF4A
		// (set) Token: 0x060102EE RID: 66286 RVA: 0x0039FD57 File Offset: 0x0039DF57
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Yearly")]
		[Localizable(true)]
		public string Yearly
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

		// Token: 0x17004E40 RID: 20032
		// (get) Token: 0x060102EF RID: 66287 RVA: 0x0039FD65 File Offset: 0x0039DF65
		// (set) Token: 0x060102F0 RID: 66288 RVA: 0x0039FD72 File Offset: 0x0039DF72
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Every")]
		public string Every
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

		// Token: 0x17004E41 RID: 20033
		// (get) Token: 0x060102F1 RID: 66289 RVA: 0x0039FD80 File Offset: 0x0039DF80
		// (set) Token: 0x060102F2 RID: 66290 RVA: 0x0039FD8D File Offset: 0x0039DF8D
		[NotifyParentProperty(true)]
		[DefaultValue("hour(s)")]
		[ScriptIgnore]
		[Localizable(true)]
		public string Hours
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

		// Token: 0x17004E42 RID: 20034
		// (get) Token: 0x060102F3 RID: 66291 RVA: 0x0039FD9B File Offset: 0x0039DF9B
		// (set) Token: 0x060102F4 RID: 66292 RVA: 0x0039FDA8 File Offset: 0x0039DFA8
		[Localizable(true)]
		[DefaultValue("day(s)")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Days
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

		// Token: 0x17004E43 RID: 20035
		// (get) Token: 0x060102F5 RID: 66293 RVA: 0x0039FDB6 File Offset: 0x0039DFB6
		// (set) Token: 0x060102F6 RID: 66294 RVA: 0x0039FDC3 File Offset: 0x0039DFC3
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("week(s) on")]
		[ScriptIgnore]
		public string Weeks
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

		// Token: 0x17004E44 RID: 20036
		// (get) Token: 0x060102F7 RID: 66295 RVA: 0x0039FDD1 File Offset: 0x0039DFD1
		// (set) Token: 0x060102F8 RID: 66296 RVA: 0x0039FDDE File Offset: 0x0039DFDE
		[NotifyParentProperty(true)]
		[DefaultValue("month(s)")]
		[ScriptIgnore]
		[Localizable(true)]
		public string Months
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

		// Token: 0x17004E45 RID: 20037
		// (get) Token: 0x060102F9 RID: 66297 RVA: 0x0039FDEC File Offset: 0x0039DFEC
		// (set) Token: 0x060102FA RID: 66298 RVA: 0x0039FDF9 File Offset: 0x0039DFF9
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("year(s)")]
		public string Years
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

		// Token: 0x17004E46 RID: 20038
		// (get) Token: 0x060102FB RID: 66299 RVA: 0x0039FE07 File Offset: 0x0039E007
		// (set) Token: 0x060102FC RID: 66300 RVA: 0x0039FE14 File Offset: 0x0039E014
		[NotifyParentProperty(true)]
		[DefaultValue("Every weekday")]
		[ScriptIgnore]
		[Localizable(true)]
		public string EveryWeekday
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

		// Token: 0x17004E47 RID: 20039
		// (get) Token: 0x060102FD RID: 66301 RVA: 0x0039FE22 File Offset: 0x0039E022
		// (set) Token: 0x060102FE RID: 66302 RVA: 0x0039FE2F File Offset: 0x0039E02F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Every working day")]
		[ScriptIgnore]
		public string EveryWorkingDay
		{
			get
			{
				return this.GetString("AdvancedEveryWorkingDay");
			}
			set
			{
				this.SetString("AdvancedEveryWorkingDay", value);
			}
		}

		// Token: 0x17004E48 RID: 20040
		// (get) Token: 0x060102FF RID: 66303 RVA: 0x0039FE3D File Offset: 0x0039E03D
		// (set) Token: 0x06010300 RID: 66304 RVA: 0x0039FE4A File Offset: 0x0039E04A
		[NotifyParentProperty(true)]
		[DefaultValue("Recur every")]
		[ScriptIgnore]
		[Localizable(true)]
		public string RecurEvery
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

		// Token: 0x17004E49 RID: 20041
		// (get) Token: 0x06010301 RID: 66305 RVA: 0x0039FE58 File Offset: 0x0039E058
		// (set) Token: 0x06010302 RID: 66306 RVA: 0x0039FE65 File Offset: 0x0039E065
		[Localizable(true)]
		[DefaultValue("Day")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string Day
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

		// Token: 0x17004E4A RID: 20042
		// (get) Token: 0x06010303 RID: 66307 RVA: 0x0039FE73 File Offset: 0x0039E073
		// (set) Token: 0x06010304 RID: 66308 RVA: 0x0039FE80 File Offset: 0x0039E080
		[NotifyParentProperty(true)]
		[DefaultValue("of every")]
		[ScriptIgnore]
		[Localizable(true)]
		public string OfEvery
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

		// Token: 0x17004E4B RID: 20043
		// (get) Token: 0x06010305 RID: 66309 RVA: 0x0039FE8E File Offset: 0x0039E08E
		// (set) Token: 0x06010306 RID: 66310 RVA: 0x0039FE9B File Offset: 0x0039E09B
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("first")]
		[ScriptIgnore]
		public string First
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

		// Token: 0x17004E4C RID: 20044
		// (get) Token: 0x06010307 RID: 66311 RVA: 0x0039FEA9 File Offset: 0x0039E0A9
		// (set) Token: 0x06010308 RID: 66312 RVA: 0x0039FEB6 File Offset: 0x0039E0B6
		[NotifyParentProperty(true)]
		[DefaultValue("second")]
		[ScriptIgnore]
		[Localizable(true)]
		public string Second
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

		// Token: 0x17004E4D RID: 20045
		// (get) Token: 0x06010309 RID: 66313 RVA: 0x0039FEC4 File Offset: 0x0039E0C4
		// (set) Token: 0x0601030A RID: 66314 RVA: 0x0039FED1 File Offset: 0x0039E0D1
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("third")]
		public string Third
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

		// Token: 0x17004E4E RID: 20046
		// (get) Token: 0x0601030B RID: 66315 RVA: 0x0039FEDF File Offset: 0x0039E0DF
		// (set) Token: 0x0601030C RID: 66316 RVA: 0x0039FEEC File Offset: 0x0039E0EC
		[NotifyParentProperty(true)]
		[DefaultValue("fourth")]
		[ScriptIgnore]
		[Localizable(true)]
		public string Fourth
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

		// Token: 0x17004E4F RID: 20047
		// (get) Token: 0x0601030D RID: 66317 RVA: 0x0039FEFA File Offset: 0x0039E0FA
		// (set) Token: 0x0601030E RID: 66318 RVA: 0x0039FF07 File Offset: 0x0039E107
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("last")]
		[ScriptIgnore]
		public string Last
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

		// Token: 0x17004E50 RID: 20048
		// (get) Token: 0x0601030F RID: 66319 RVA: 0x0039FF15 File Offset: 0x0039E115
		// (set) Token: 0x06010310 RID: 66320 RVA: 0x0039FF22 File Offset: 0x0039E122
		[NotifyParentProperty(true)]
		[DefaultValue("day")]
		[ScriptIgnore]
		[Localizable(true)]
		public string MaskDay
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

		// Token: 0x17004E51 RID: 20049
		// (get) Token: 0x06010311 RID: 66321 RVA: 0x0039FF30 File Offset: 0x0039E130
		// (set) Token: 0x06010312 RID: 66322 RVA: 0x0039FF3D File Offset: 0x0039E13D
		[DefaultValue("weekday")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string MaskWeekday
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

		// Token: 0x17004E52 RID: 20050
		// (get) Token: 0x06010313 RID: 66323 RVA: 0x0039FF4B File Offset: 0x0039E14B
		// (set) Token: 0x06010314 RID: 66324 RVA: 0x0039FF58 File Offset: 0x0039E158
		[NotifyParentProperty(true)]
		[DefaultValue("weekend day")]
		[ScriptIgnore]
		[Localizable(true)]
		public string MaskWeekendDay
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

		// Token: 0x17004E53 RID: 20051
		// (get) Token: 0x06010315 RID: 66325 RVA: 0x0039FF66 File Offset: 0x0039E166
		// (set) Token: 0x06010316 RID: 66326 RVA: 0x0039FF73 File Offset: 0x0039E173
		[Localizable(true)]
		[DefaultValue("The")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string The
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

		// Token: 0x17004E54 RID: 20052
		// (get) Token: 0x06010317 RID: 66327 RVA: 0x0039FF81 File Offset: 0x0039E181
		// (set) Token: 0x06010318 RID: 66328 RVA: 0x0039FF8E File Offset: 0x0039E18E
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("of")]
		[ScriptIgnore]
		public string Of
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

		// Token: 0x17004E55 RID: 20053
		// (get) Token: 0x06010319 RID: 66329 RVA: 0x0039FF9C File Offset: 0x0039E19C
		// (set) Token: 0x0601031A RID: 66330 RVA: 0x0039FFA9 File Offset: 0x0039E1A9
		[NotifyParentProperty(true)]
		[DefaultValue("No end date")]
		[ScriptIgnore]
		[Localizable(true)]
		public string NoEndDate
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

		// Token: 0x17004E56 RID: 20054
		// (get) Token: 0x0601031B RID: 66331 RVA: 0x0039FFB7 File Offset: 0x0039E1B7
		// (set) Token: 0x0601031C RID: 66332 RVA: 0x0039FFC4 File Offset: 0x0039E1C4
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("End after")]
		public string EndAfter
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

		// Token: 0x17004E57 RID: 20055
		// (get) Token: 0x0601031D RID: 66333 RVA: 0x0039FFD2 File Offset: 0x0039E1D2
		// (set) Token: 0x0601031E RID: 66334 RVA: 0x0039FFDF File Offset: 0x0039E1DF
		[NotifyParentProperty(true)]
		[DefaultValue("End by")]
		[ScriptIgnore]
		[Localizable(true)]
		public string EndByThisDate
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

		// Token: 0x17004E58 RID: 20056
		// (get) Token: 0x0601031F RID: 66335 RVA: 0x0039FFED File Offset: 0x0039E1ED
		// (set) Token: 0x06010320 RID: 66336 RVA: 0x0039FFFA File Offset: 0x0039E1FA
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("occurrences")]
		[ScriptIgnore]
		public string Occurrences
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

		// Token: 0x17004E59 RID: 20057
		// (get) Token: 0x06010321 RID: 66337 RVA: 0x003A0008 File Offset: 0x0039E208
		// (set) Token: 0x06010322 RID: 66338 RVA: 0x003A0015 File Offset: 0x0039E215
		[NotifyParentProperty(true)]
		[DefaultValue("OK")]
		[ScriptIgnore]
		[Localizable(true)]
		public string CalendarOK
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

		// Token: 0x17004E5A RID: 20058
		// (get) Token: 0x06010323 RID: 66339 RVA: 0x003A0023 File Offset: 0x0039E223
		// (set) Token: 0x06010324 RID: 66340 RVA: 0x003A0030 File Offset: 0x0039E230
		[ScriptIgnore]
		[Localizable(true)]
		[DefaultValue("Cancel")]
		[NotifyParentProperty(true)]
		public string CalendarCancel
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

		// Token: 0x17004E5B RID: 20059
		// (get) Token: 0x06010325 RID: 66341 RVA: 0x003A003E File Offset: 0x0039E23E
		// (set) Token: 0x06010326 RID: 66342 RVA: 0x003A004B File Offset: 0x0039E24B
		[NotifyParentProperty(true)]
		[DefaultValue("Today")]
		[ScriptIgnore]
		[Localizable(true)]
		public string CalendarToday
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

		// Token: 0x17004E5C RID: 20060
		// (get) Token: 0x06010327 RID: 66343 RVA: 0x003A0059 File Offset: 0x0039E259
		// (set) Token: 0x06010328 RID: 66344 RVA: 0x003A0066 File Offset: 0x0039E266
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Save")]
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

		// Token: 0x17004E5D RID: 20061
		// (get) Token: 0x06010329 RID: 66345 RVA: 0x003A0074 File Offset: 0x0039E274
		// (set) Token: 0x0601032A RID: 66346 RVA: 0x003A0081 File Offset: 0x0039E281
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		[ScriptIgnore]
		[Localizable(true)]
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

		// Token: 0x0601032B RID: 66347 RVA: 0x003A008F File Offset: 0x0039E28F
		internal IntegratedRecurrenceEditorStrings(LocalizationProvider provider) : base(provider)
		{
		}
	}
}
