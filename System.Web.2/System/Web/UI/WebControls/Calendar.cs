using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200038D RID: 909
	[ControlValueProperty("SelectedDate", typeof(DateTime), "1/1/0001")]
	[DataBindingHandler("System.Web.UI.Design.WebControls.CalendarDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SelectionChanged")]
	[DefaultProperty("SelectedDate")]
	[Designer("System.Web.UI.Design.WebControls.CalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class Calendar : WebControl, IPostBackEventHandler
	{
		// Token: 0x06002A44 RID: 10820 RVA: 0x00088D32 File Offset: 0x00086F32
		public Calendar() : base(HtmlTextWriterTag.Table)
		{
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x00088D3C File Offset: 0x00086F3C
		// (set) Token: 0x06002A46 RID: 10822 RVA: 0x00085605 File Offset: 0x00083805
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Accessibility")]
		[WebSysDescription("Calendar_Caption")]
		public virtual string Caption
		{
			get
			{
				string text = (string)this.ViewState["Caption"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x00088D6C File Offset: 0x00086F6C
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x00085641 File Offset: 0x00083841
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_CaptionAlign")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj == null)
				{
					return TableCaptionAlign.NotSet;
				}
				return (TableCaptionAlign)obj;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x00088D98 File Offset: 0x00086F98
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x00088DC1 File Offset: 0x00086FC1
		[WebCategory("Layout")]
		[DefaultValue(2)]
		[WebSysDescription("Calendar_CellPadding")]
		public int CellPadding
		{
			get
			{
				object obj = this.ViewState["CellPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CellPadding"] = value;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00088DE8 File Offset: 0x00086FE8
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x00088E11 File Offset: 0x00087011
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("Calendar_CellSpacing")]
		public int CellSpacing
		{
			get
			{
				object obj = this.ViewState["CellSpacing"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CellSpacing"] = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00088E38 File Offset: 0x00087038
		[WebCategory("Styles")]
		[WebSysDescription("Calendar_DayHeaderStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle DayHeaderStyle
		{
			get
			{
				if (this.dayHeaderStyle == null)
				{
					this.dayHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dayHeaderStyle).TrackViewState();
					}
				}
				return this.dayHeaderStyle;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x00088E68 File Offset: 0x00087068
		// (set) Token: 0x06002A4F RID: 10831 RVA: 0x00088E91 File Offset: 0x00087091
		[WebCategory("Appearance")]
		[DefaultValue(DayNameFormat.Short)]
		[WebSysDescription("Calendar_DayNameFormat")]
		public DayNameFormat DayNameFormat
		{
			get
			{
				object obj = this.ViewState["DayNameFormat"];
				if (obj != null)
				{
					return (DayNameFormat)obj;
				}
				return DayNameFormat.Short;
			}
			set
			{
				if (value < DayNameFormat.Full || value > DayNameFormat.Shortest)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DayNameFormat"] = value;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x00088EBC File Offset: 0x000870BC
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("Calendar_DayStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle DayStyle
		{
			get
			{
				if (this.dayStyle == null)
				{
					this.dayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dayStyle).TrackViewState();
					}
				}
				return this.dayStyle;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x00088EEC File Offset: 0x000870EC
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x00088F15 File Offset: 0x00087115
		[WebCategory("Appearance")]
		[DefaultValue(FirstDayOfWeek.Default)]
		[WebSysDescription("Calendar_FirstDayOfWeek")]
		public FirstDayOfWeek FirstDayOfWeek
		{
			get
			{
				object obj = this.ViewState["FirstDayOfWeek"];
				if (obj != null)
				{
					return (FirstDayOfWeek)obj;
				}
				return FirstDayOfWeek.Default;
			}
			set
			{
				if (value < FirstDayOfWeek.Sunday || value > FirstDayOfWeek.Default)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["FirstDayOfWeek"] = value;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x00088F40 File Offset: 0x00087140
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x00088F6D File Offset: 0x0008716D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		[WebSysDescription("Calendar_NextMonthText")]
		public string NextMonthText
		{
			get
			{
				object obj = this.ViewState["NextMonthText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;";
			}
			set
			{
				this.ViewState["NextMonthText"] = value;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x00088F80 File Offset: 0x00087180
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x00088FA9 File Offset: 0x000871A9
		[WebCategory("Appearance")]
		[DefaultValue(NextPrevFormat.CustomText)]
		[WebSysDescription("Calendar_NextPrevFormat")]
		public NextPrevFormat NextPrevFormat
		{
			get
			{
				object obj = this.ViewState["NextPrevFormat"];
				if (obj != null)
				{
					return (NextPrevFormat)obj;
				}
				return NextPrevFormat.CustomText;
			}
			set
			{
				if (value < NextPrevFormat.CustomText || value > NextPrevFormat.FullMonth)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["NextPrevFormat"] = value;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00088FD4 File Offset: 0x000871D4
		[WebCategory("Styles")]
		[WebSysDescription("Calendar_NextPrevStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle NextPrevStyle
		{
			get
			{
				if (this.nextPrevStyle == null)
				{
					this.nextPrevStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.nextPrevStyle).TrackViewState();
					}
				}
				return this.nextPrevStyle;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x00089002 File Offset: 0x00087202
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("Calendar_OtherMonthDayStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle OtherMonthDayStyle
		{
			get
			{
				if (this.otherMonthDayStyle == null)
				{
					this.otherMonthDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.otherMonthDayStyle).TrackViewState();
					}
				}
				return this.otherMonthDayStyle;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00089030 File Offset: 0x00087230
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0008905D File Offset: 0x0008725D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&lt;")]
		[WebSysDescription("Calendar_PrevMonthText")]
		public string PrevMonthText
		{
			get
			{
				object obj = this.ViewState["PrevMonthText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;";
			}
			set
			{
				this.ViewState["PrevMonthText"] = value;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x00089070 File Offset: 0x00087270
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x00089091 File Offset: 0x00087291
		[Bindable(true, BindingDirection.TwoWay)]
		[DefaultValue(typeof(DateTime), "1/1/0001")]
		[WebSysDescription("Calendar_SelectedDate")]
		public DateTime SelectedDate
		{
			get
			{
				if (this.SelectedDates.Count == 0)
				{
					return DateTime.MinValue;
				}
				return this.SelectedDates[0];
			}
			set
			{
				if (value == DateTime.MinValue)
				{
					this.SelectedDates.Clear();
					return;
				}
				this.SelectedDates.SelectRange(value, value);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000890B9 File Offset: 0x000872B9
		[Browsable(false)]
		[WebSysDescription("Calendar_SelectedDates")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SelectedDatesCollection SelectedDates
		{
			get
			{
				if (this.selectedDates == null)
				{
					if (this.dateList == null)
					{
						this.dateList = new ArrayList();
					}
					this.selectedDates = new SelectedDatesCollection(this.dateList);
				}
				return this.selectedDates;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000890ED File Offset: 0x000872ED
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("Calendar_SelectedDayStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle SelectedDayStyle
		{
			get
			{
				if (this.selectedDayStyle == null)
				{
					this.selectedDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.selectedDayStyle).TrackViewState();
					}
				}
				return this.selectedDayStyle;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x0008911C File Offset: 0x0008731C
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x00089145 File Offset: 0x00087345
		[WebCategory("Behavior")]
		[DefaultValue(CalendarSelectionMode.Day)]
		[WebSysDescription("Calendar_SelectionMode")]
		public CalendarSelectionMode SelectionMode
		{
			get
			{
				object obj = this.ViewState["SelectionMode"];
				if (obj != null)
				{
					return (CalendarSelectionMode)obj;
				}
				return CalendarSelectionMode.Day;
			}
			set
			{
				if (value < CalendarSelectionMode.None || value > CalendarSelectionMode.DayWeekMonth)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x00089170 File Offset: 0x00087370
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x0008919D File Offset: 0x0008739D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&gt;&gt;")]
		[WebSysDescription("Calendar_SelectMonthText")]
		public string SelectMonthText
		{
			get
			{
				object obj = this.ViewState["SelectMonthText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;&gt;";
			}
			set
			{
				this.ViewState["SelectMonthText"] = value;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000891B0 File Offset: 0x000873B0
		[WebCategory("Styles")]
		[WebSysDescription("Calendar_SelectorStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle SelectorStyle
		{
			get
			{
				if (this.selectorStyle == null)
				{
					this.selectorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.selectorStyle).TrackViewState();
					}
				}
				return this.selectorStyle;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x000891E0 File Offset: 0x000873E0
		// (set) Token: 0x06002A66 RID: 10854 RVA: 0x0008920D File Offset: 0x0008740D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		[WebSysDescription("Calendar_SelectWeekText")]
		public string SelectWeekText
		{
			get
			{
				object obj = this.ViewState["SelectWeekText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;";
			}
			set
			{
				this.ViewState["SelectWeekText"] = value;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x00089220 File Offset: 0x00087420
		// (set) Token: 0x06002A68 RID: 10856 RVA: 0x00089249 File Offset: 0x00087449
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[WebSysDescription("Calendar_ShowDayHeader")]
		public bool ShowDayHeader
		{
			get
			{
				object obj = this.ViewState["ShowDayHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowDayHeader"] = value;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00089264 File Offset: 0x00087464
		// (set) Token: 0x06002A6A RID: 10858 RVA: 0x0008928D File Offset: 0x0008748D
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("Calendar_ShowGridLines")]
		public bool ShowGridLines
		{
			get
			{
				object obj = this.ViewState["ShowGridLines"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowGridLines"] = value;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x000892A8 File Offset: 0x000874A8
		// (set) Token: 0x06002A6C RID: 10860 RVA: 0x000892D1 File Offset: 0x000874D1
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[WebSysDescription("Calendar_ShowNextPrevMonth")]
		public bool ShowNextPrevMonth
		{
			get
			{
				object obj = this.ViewState["ShowNextPrevMonth"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowNextPrevMonth"] = value;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x000892EC File Offset: 0x000874EC
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x00089315 File Offset: 0x00087515
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[WebSysDescription("Calendar_ShowTitle")]
		public bool ShowTitle
		{
			get
			{
				object obj = this.ViewState["ShowTitle"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowTitle"] = value;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x00089330 File Offset: 0x00087530
		// (set) Token: 0x06002A70 RID: 10864 RVA: 0x00089359 File Offset: 0x00087559
		[WebCategory("Appearance")]
		[DefaultValue(TitleFormat.MonthYear)]
		[WebSysDescription("Calendar_TitleFormat")]
		public TitleFormat TitleFormat
		{
			get
			{
				object obj = this.ViewState["TitleFormat"];
				if (obj != null)
				{
					return (TitleFormat)obj;
				}
				return TitleFormat.MonthYear;
			}
			set
			{
				if (value < TitleFormat.Month || value > TitleFormat.MonthYear)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TitleFormat"] = value;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x00089384 File Offset: 0x00087584
		[WebCategory("Styles")]
		[WebSysDescription("Calendar_TitleStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle TitleStyle
		{
			get
			{
				if (this.titleStyle == null)
				{
					this.titleStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.titleStyle).TrackViewState();
					}
				}
				return this.titleStyle;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000893B2 File Offset: 0x000875B2
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("Calendar_TodayDayStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle TodayDayStyle
		{
			get
			{
				if (this.todayDayStyle == null)
				{
					this.todayDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.todayDayStyle).TrackViewState();
					}
				}
				return this.todayDayStyle;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000893E0 File Offset: 0x000875E0
		// (set) Token: 0x06002A74 RID: 10868 RVA: 0x0008940D File Offset: 0x0008760D
		[Browsable(false)]
		[WebSysDescription("Calendar_TodaysDate")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime TodaysDate
		{
			get
			{
				object obj = this.ViewState["TodaysDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Today;
			}
			set
			{
				this.ViewState["TodaysDate"] = value.Date;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x0008942C File Offset: 0x0008762C
		// (set) Token: 0x06002A76 RID: 10870 RVA: 0x0008592D File Offset: 0x00083B2D
		[DefaultValue(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("Table_UseAccessibleHeader")]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				object obj = this.ViewState["UseAccessibleHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00089458 File Offset: 0x00087658
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x00089485 File Offset: 0x00087685
		[Bindable(true)]
		[DefaultValue(typeof(DateTime), "1/1/0001")]
		[WebSysDescription("Calendar_VisibleDate")]
		public DateTime VisibleDate
		{
			get
			{
				object obj = this.ViewState["VisibleDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.MinValue;
			}
			set
			{
				this.ViewState["VisibleDate"] = value.Date;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000894A3 File Offset: 0x000876A3
		[WebCategory("Styles")]
		[WebSysDescription("Calendar_WeekendDayStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle WeekendDayStyle
		{
			get
			{
				if (this.weekendDayStyle == null)
				{
					this.weekendDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.weekendDayStyle).TrackViewState();
					}
				}
				return this.weekendDayStyle;
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002A7A RID: 10874 RVA: 0x000894D1 File Offset: 0x000876D1
		// (remove) Token: 0x06002A7B RID: 10875 RVA: 0x000894E4 File Offset: 0x000876E4
		[WebCategory("Action")]
		[WebSysDescription("Calendar_OnDayRender")]
		public event DayRenderEventHandler DayRender
		{
			add
			{
				base.Events.AddHandler(Calendar.EventDayRender, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.EventDayRender, value);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002A7C RID: 10876 RVA: 0x000894F7 File Offset: 0x000876F7
		// (remove) Token: 0x06002A7D RID: 10877 RVA: 0x0008950A File Offset: 0x0008770A
		[WebCategory("Action")]
		[WebSysDescription("Calendar_OnSelectionChanged")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.EventSelectionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.EventSelectionChanged, value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002A7E RID: 10878 RVA: 0x0008951D File Offset: 0x0008771D
		// (remove) Token: 0x06002A7F RID: 10879 RVA: 0x00089530 File Offset: 0x00087730
		[WebCategory("Action")]
		[WebSysDescription("Calendar_OnVisibleMonthChanged")]
		public event MonthChangedEventHandler VisibleMonthChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.EventVisibleMonthChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.EventVisibleMonthChanged, value);
			}
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00089544 File Offset: 0x00087744
		private void ApplyTitleStyle(TableCell titleCell, Table titleTable, TableItemStyle titleStyle)
		{
			if (titleStyle.BackColor != Color.Empty)
			{
				titleCell.BackColor = titleStyle.BackColor;
			}
			if (titleStyle.BorderColor != Color.Empty)
			{
				titleCell.BorderColor = titleStyle.BorderColor;
			}
			if (titleStyle.BorderWidth != Unit.Empty)
			{
				titleCell.BorderWidth = titleStyle.BorderWidth;
			}
			if (titleStyle.BorderStyle != BorderStyle.NotSet)
			{
				titleCell.BorderStyle = titleStyle.BorderStyle;
			}
			if (titleStyle.Height != Unit.Empty)
			{
				titleCell.Height = titleStyle.Height;
			}
			if (titleStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				titleCell.VerticalAlign = titleStyle.VerticalAlign;
			}
			if (titleStyle.CssClass.Length > 0)
			{
				titleTable.CssClass = titleStyle.CssClass;
			}
			else if (this.CssClass.Length > 0)
			{
				titleTable.CssClass = this.CssClass;
			}
			if (titleStyle.ForeColor != Color.Empty)
			{
				titleTable.ForeColor = titleStyle.ForeColor;
			}
			else if (this.ForeColor != Color.Empty)
			{
				titleTable.ForeColor = this.ForeColor;
			}
			titleTable.Font.CopyFrom(titleStyle.Font);
			titleTable.Font.MergeWith(this.Font);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00089687 File Offset: 0x00087887
		protected override ControlCollection CreateControlCollection()
		{
			return new InternalControlCollection(this);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00089690 File Offset: 0x00087890
		private DateTime EffectiveVisibleDate()
		{
			DateTime dateTime = this.VisibleDate;
			if (dateTime.Equals(DateTime.MinValue))
			{
				dateTime = this.TodaysDate;
			}
			if (this.IsMinSupportedYearMonth(dateTime))
			{
				return this.minSupportedDate;
			}
			return this.threadCalendar.AddDays(dateTime, -(this.threadCalendar.GetDayOfMonth(dateTime) - 1));
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000896E4 File Offset: 0x000878E4
		private DateTime FirstCalendarDay(DateTime visibleDate)
		{
			if (this.IsMinSupportedYearMonth(visibleDate))
			{
				return visibleDate;
			}
			int num = this.threadCalendar.GetDayOfWeek(visibleDate) - (DayOfWeek)this.NumericFirstDayOfWeek();
			if (num <= 0)
			{
				num += 7;
			}
			return this.threadCalendar.AddDays(visibleDate, -num);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00089728 File Offset: 0x00087928
		private string GetCalendarButtonText(string eventArgument, string buttonText, string title, bool showLink, Color foreColor)
		{
			if (showLink)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<a href=\"");
				stringBuilder.Append(this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true));
				stringBuilder.Append("\" style=\"color:");
				stringBuilder.Append(foreColor.IsEmpty ? this.defaultButtonColorText : ColorTranslator.ToHtml(foreColor));
				if (!string.IsNullOrEmpty(title))
				{
					stringBuilder.Append("\" title=\"");
					stringBuilder.Append(title);
				}
				stringBuilder.Append("\">");
				stringBuilder.Append(buttonText);
				stringBuilder.Append("</a>");
				return stringBuilder.ToString();
			}
			return buttonText;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000897D8 File Offset: 0x000879D8
		private int GetDefinedStyleMask()
		{
			int num = 8;
			if (this.dayStyle != null && !this.dayStyle.IsEmpty)
			{
				num |= 16;
			}
			if (this.todayDayStyle != null && !this.todayDayStyle.IsEmpty)
			{
				num |= 4;
			}
			if (this.otherMonthDayStyle != null && !this.otherMonthDayStyle.IsEmpty)
			{
				num |= 2;
			}
			if (this.weekendDayStyle != null && !this.weekendDayStyle.IsEmpty)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x0008984D File Offset: 0x00087A4D
		private string GetMonthName(int m, bool bFull)
		{
			if (bFull)
			{
				return DateTimeFormatInfo.CurrentInfo.GetMonthName(m);
			}
			return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(m);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00089869 File Offset: 0x00087A69
		protected bool HasWeekSelectors(CalendarSelectionMode selectionMode)
		{
			return selectionMode == CalendarSelectionMode.DayWeek || selectionMode == CalendarSelectionMode.DayWeekMonth;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00089878 File Offset: 0x00087A78
		private bool IsTheSameYearMonth(DateTime date1, DateTime date2)
		{
			return this.threadCalendar.GetEra(date1) == this.threadCalendar.GetEra(date2) && this.threadCalendar.GetYear(date1) == this.threadCalendar.GetYear(date2) && this.threadCalendar.GetMonth(date1) == this.threadCalendar.GetMonth(date2);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000898D5 File Offset: 0x00087AD5
		private bool IsMinSupportedYearMonth(DateTime date)
		{
			return this.IsTheSameYearMonth(this.minSupportedDate, date);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000898E4 File Offset: 0x00087AE4
		private bool IsMaxSupportedYearMonth(DateTime date)
		{
			return this.IsTheSameYearMonth(this.maxSupportedDate, date);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000898F4 File Offset: 0x00087AF4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					base.LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.TitleStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.NextPrevStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.DayStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.DayHeaderStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.TodayDayStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.WeekendDayStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					((IStateManager)this.OtherMonthDayStyle).LoadViewState(array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.SelectedDayStyle).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)this.SelectorStyle).LoadViewState(array[9]);
				}
				ArrayList arrayList = (ArrayList)this.ViewState["SD"];
				if (arrayList != null)
				{
					this.dateList = arrayList;
					this.selectedDates = null;
				}
			}
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000899F0 File Offset: 0x00087BF0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.titleStyle != null)
			{
				((IStateManager)this.titleStyle).TrackViewState();
			}
			if (this.nextPrevStyle != null)
			{
				((IStateManager)this.nextPrevStyle).TrackViewState();
			}
			if (this.dayStyle != null)
			{
				((IStateManager)this.dayStyle).TrackViewState();
			}
			if (this.dayHeaderStyle != null)
			{
				((IStateManager)this.dayHeaderStyle).TrackViewState();
			}
			if (this.todayDayStyle != null)
			{
				((IStateManager)this.todayDayStyle).TrackViewState();
			}
			if (this.weekendDayStyle != null)
			{
				((IStateManager)this.weekendDayStyle).TrackViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				((IStateManager)this.otherMonthDayStyle).TrackViewState();
			}
			if (this.selectedDayStyle != null)
			{
				((IStateManager)this.selectedDayStyle).TrackViewState();
			}
			if (this.selectorStyle != null)
			{
				((IStateManager)this.selectorStyle).TrackViewState();
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00089AAE File Offset: 0x00087CAE
		private int NumericFirstDayOfWeek()
		{
			if (this.FirstDayOfWeek != FirstDayOfWeek.Default)
			{
				return (int)this.FirstDayOfWeek;
			}
			return (int)DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00089ACC File Offset: 0x00087CCC
		protected virtual void OnDayRender(TableCell cell, CalendarDay day)
		{
			DayRenderEventHandler dayRenderEventHandler = (DayRenderEventHandler)base.Events[Calendar.EventDayRender];
			if (dayRenderEventHandler != null)
			{
				int days = day.Date.Subtract(Calendar.baseDate).Days;
				string selectUrl = null;
				Page page = this.Page;
				if (page != null)
				{
					string argument = days.ToString(CultureInfo.InvariantCulture);
					selectUrl = this.Page.ClientScript.GetPostBackClientHyperlink(this, argument, true);
				}
				dayRenderEventHandler(this, new DayRenderEventArgs(cell, day, selectUrl));
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00089B50 File Offset: 0x00087D50
		protected virtual void OnSelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Calendar.EventSelectionChanged];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00089B84 File Offset: 0x00087D84
		protected virtual void OnVisibleMonthChanged(DateTime newDate, DateTime previousDate)
		{
			MonthChangedEventHandler monthChangedEventHandler = (MonthChangedEventHandler)base.Events[Calendar.EventVisibleMonthChanged];
			if (monthChangedEventHandler != null)
			{
				monthChangedEventHandler(this, new MonthChangedEventArgs(newDate, previousDate));
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00089BB8 File Offset: 0x00087DB8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (base.AdapterInternal != null)
			{
				IPostBackEventHandler postBackEventHandler = base.AdapterInternal as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					postBackEventHandler.RaisePostBackEvent(eventArgument);
					return;
				}
			}
			else
			{
				if (string.Compare(eventArgument, 0, "V", 0, "V".Length, StringComparison.Ordinal) == 0)
				{
					DateTime previousDate = this.VisibleDate;
					if (previousDate.Equals(DateTime.MinValue))
					{
						previousDate = this.TodaysDate;
					}
					int num = int.Parse(eventArgument.Substring("V".Length), CultureInfo.InvariantCulture);
					this.VisibleDate = Calendar.baseDate.AddDays((double)num);
					if (this.VisibleDate == DateTime.MinValue)
					{
						this.VisibleDate = DateTimeFormatInfo.CurrentInfo.Calendar.AddDays(this.VisibleDate, 1);
					}
					this.OnVisibleMonthChanged(this.VisibleDate, previousDate);
					return;
				}
				if (string.Compare(eventArgument, 0, "R", 0, "R".Length, StringComparison.Ordinal) == 0)
				{
					int num2 = int.Parse(eventArgument.Substring("R".Length), CultureInfo.InvariantCulture);
					int num3 = num2 / 100;
					int num4 = num2 % 100;
					if (num4 < 1)
					{
						num4 = 100 + num4;
						num3--;
					}
					DateTime dateFrom = Calendar.baseDate.AddDays((double)num3);
					this.SelectRange(dateFrom, dateFrom.AddDays((double)(num4 - 1)));
					return;
				}
				int num5 = int.Parse(eventArgument, CultureInfo.InvariantCulture);
				DateTime dateTime = Calendar.baseDate.AddDays((double)num5);
				this.SelectRange(dateTime, dateTime);
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00089D33 File Offset: 0x00087F33
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00089D3C File Offset: 0x00087F3C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.Page.RegisterPostBackScript();
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00089D58 File Offset: 0x00087F58
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.threadCalendar = DateTimeFormatInfo.CurrentInfo.Calendar;
			this.minSupportedDate = this.threadCalendar.MinSupportedDateTime;
			this.maxSupportedDate = this.threadCalendar.MaxSupportedDateTime;
			DateTime visibleDate = this.EffectiveVisibleDate();
			DateTime firstDay = this.FirstCalendarDay(visibleDate);
			CalendarSelectionMode selectionMode = this.SelectionMode;
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			Page page = this.Page;
			bool buttonsActive = page != null && !base.DesignMode && base.IsEnabled;
			this.defaultForeColor = this.ForeColor;
			if (this.defaultForeColor == Color.Empty)
			{
				this.defaultForeColor = Calendar.DefaultForeColor;
			}
			this.defaultButtonColorText = ColorTranslator.ToHtml(this.defaultForeColor);
			Table table = new Table();
			if (this.ID != null)
			{
				table.ID = this.ClientID;
			}
			table.CopyBaseAttributes(this);
			if (base.ControlStyleCreated)
			{
				table.ApplyStyle(base.ControlStyle);
			}
			table.Width = this.Width;
			table.Height = this.Height;
			table.CellPadding = this.CellPadding;
			table.CellSpacing = this.CellSpacing;
			if (!base.ControlStyleCreated || !base.ControlStyle.IsSet(32) || this.BorderWidth.Equals(Unit.Empty))
			{
				table.BorderWidth = Unit.Pixel(1);
			}
			if (this.ShowGridLines)
			{
				table.GridLines = GridLines.Both;
			}
			else
			{
				table.GridLines = GridLines.None;
			}
			bool useAccessibleHeader = this.UseAccessibleHeader;
			if (useAccessibleHeader && table.Attributes["title"] == null)
			{
				table.Attributes["title"] = SR.GetString("Calendar_TitleText");
			}
			string caption = this.Caption;
			if (caption.Length > 0)
			{
				table.Caption = caption;
				table.CaptionAlign = this.CaptionAlign;
			}
			table.RenderBeginTag(writer);
			if (this.ShowTitle)
			{
				this.RenderTitle(writer, visibleDate, selectionMode, buttonsActive, useAccessibleHeader);
			}
			if (this.ShowDayHeader)
			{
				this.RenderDayHeader(writer, visibleDate, selectionMode, buttonsActive, useAccessibleHeader);
			}
			this.RenderDays(writer, firstDay, visibleDate, selectionMode, buttonsActive, useAccessibleHeader);
			table.RenderEndTag(writer);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x00089F90 File Offset: 0x00088190
		private void RenderCalendarCell(HtmlTextWriter writer, TableItemStyle style, string text, string title, bool hasButton, string eventArgument)
		{
			style.AddAttributesToRender(writer, this);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (hasButton)
			{
				Color foreColor = style.ForeColor;
				writer.Write("<a href=\"");
				writer.Write(this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true));
				writer.Write("\" style=\"color:");
				writer.Write(foreColor.IsEmpty ? this.defaultButtonColorText : ColorTranslator.ToHtml(foreColor));
				if (!string.IsNullOrEmpty(title))
				{
					writer.Write("\" title=\"");
					writer.Write(title);
				}
				writer.Write("\">");
				writer.Write(text);
				writer.Write("</a>");
			}
			else
			{
				writer.Write(text);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0008A050 File Offset: 0x00088250
		private void RenderCalendarHeaderCell(HtmlTextWriter writer, TableItemStyle style, string text, string abbrText)
		{
			style.AddAttributesToRender(writer, this);
			writer.AddAttribute("abbr", abbrText);
			writer.AddAttribute("scope", "col");
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0008A08C File Offset: 0x0008828C
		private void RenderDayHeader(HtmlTextWriter writer, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive, bool useAccessibleHeader)
		{
			writer.Write("<tr>");
			DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
			if (this.HasWeekSelectors(selectionMode))
			{
				TableItemStyle tableItemStyle = new TableItemStyle();
				tableItemStyle.HorizontalAlign = HorizontalAlign.Center;
				if (selectionMode == CalendarSelectionMode.DayWeekMonth)
				{
					int days = visibleDate.Subtract(Calendar.baseDate).Days;
					int num = this.threadCalendar.GetDaysInMonth(this.threadCalendar.GetYear(visibleDate), this.threadCalendar.GetMonth(visibleDate), this.threadCalendar.GetEra(visibleDate));
					if (this.IsMinSupportedYearMonth(visibleDate))
					{
						num = num - this.threadCalendar.GetDayOfMonth(visibleDate) + 1;
					}
					else if (this.IsMaxSupportedYearMonth(visibleDate))
					{
						num = this.threadCalendar.GetDayOfMonth(this.maxSupportedDate);
					}
					string eventArgument = "R" + (days * 100 + num).ToString(CultureInfo.InvariantCulture);
					tableItemStyle.CopyFrom(this.SelectorStyle);
					string title = null;
					if (useAccessibleHeader)
					{
						title = SR.GetString("Calendar_SelectMonthTitle");
					}
					this.RenderCalendarCell(writer, tableItemStyle, this.SelectMonthText, title, buttonsActive, eventArgument);
				}
				else
				{
					tableItemStyle.CopyFrom(this.DayHeaderStyle);
					this.RenderCalendarCell(writer, tableItemStyle, string.Empty, null, false, null);
				}
			}
			TableItemStyle tableItemStyle2 = new TableItemStyle();
			tableItemStyle2.HorizontalAlign = HorizontalAlign.Center;
			tableItemStyle2.CopyFrom(this.DayHeaderStyle);
			DayNameFormat dayNameFormat = this.DayNameFormat;
			int num2 = this.NumericFirstDayOfWeek();
			int i = num2;
			while (i < num2 + 7)
			{
				int num3 = i % 7;
				string text;
				switch (dayNameFormat)
				{
				case DayNameFormat.Full:
					text = currentInfo.GetDayName((DayOfWeek)num3);
					break;
				case DayNameFormat.Short:
					goto IL_1AF;
				case DayNameFormat.FirstLetter:
					text = currentInfo.GetDayName((DayOfWeek)num3).Substring(0, 1);
					break;
				case DayNameFormat.FirstTwoLetters:
					text = currentInfo.GetDayName((DayOfWeek)num3).Substring(0, 2);
					break;
				case DayNameFormat.Shortest:
					text = currentInfo.GetShortestDayName((DayOfWeek)num3);
					break;
				default:
					goto IL_1AF;
				}
				IL_1C5:
				if (useAccessibleHeader)
				{
					string dayName = currentInfo.GetDayName((DayOfWeek)num3);
					this.RenderCalendarHeaderCell(writer, tableItemStyle2, text, dayName);
				}
				else
				{
					this.RenderCalendarCell(writer, tableItemStyle2, text, null, false, null);
				}
				i++;
				continue;
				IL_1AF:
				text = currentInfo.GetAbbreviatedDayName((DayOfWeek)num3);
				goto IL_1C5;
			}
			writer.Write("</tr>");
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0008A2A4 File Offset: 0x000884A4
		private void RenderDays(HtmlTextWriter writer, DateTime firstDay, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive, bool useAccessibleHeader)
		{
			DateTime dateTime = firstDay;
			TableItemStyle tableItemStyle = null;
			bool flag = this.HasWeekSelectors(selectionMode);
			Unit defaultWidth;
			if (flag)
			{
				tableItemStyle = new TableItemStyle();
				tableItemStyle.Width = Unit.Percentage(12.0);
				tableItemStyle.HorizontalAlign = HorizontalAlign.Center;
				tableItemStyle.CopyFrom(this.SelectorStyle);
				defaultWidth = Unit.Percentage(12.0);
			}
			else
			{
				defaultWidth = Unit.Percentage(14.0);
			}
			bool flag2 = !(this.threadCalendar is HebrewCalendar);
			bool flag3 = base.GetType() != typeof(Calendar) || base.Events[Calendar.EventDayRender] != null;
			TableItemStyle[] array = new TableItemStyle[16];
			int definedStyleMask = this.GetDefinedStyleMask();
			DateTime todaysDate = this.TodaysDate;
			string selectWeekText = this.SelectWeekText;
			bool flag4 = buttonsActive && selectionMode > CalendarSelectionMode.None;
			int month = this.threadCalendar.GetMonth(visibleDate);
			int num = firstDay.Subtract(Calendar.baseDate).Days;
			bool flag5 = base.DesignMode && this.SelectionMode > CalendarSelectionMode.None;
			int i = 0;
			if (this.IsMinSupportedYearMonth(visibleDate))
			{
				i = this.threadCalendar.GetDayOfWeek(firstDay) - (DayOfWeek)this.NumericFirstDayOfWeek();
				if (i < 0)
				{
					i += 7;
				}
			}
			bool flag6 = false;
			DateTime date = this.threadCalendar.AddMonths(this.maxSupportedDate, -1);
			bool flag7 = this.IsMaxSupportedYearMonth(visibleDate) || this.IsTheSameYearMonth(date, visibleDate);
			int num2 = 0;
			while (num2 < 6 && !flag6)
			{
				writer.Write("<tr>");
				if (flag)
				{
					int num3 = num * 100 + 7;
					if (i > 0)
					{
						num3 -= i;
					}
					else if (flag7)
					{
						int days = this.maxSupportedDate.Subtract(dateTime).Days;
						if (days < 6)
						{
							num3 -= 6 - days;
						}
					}
					string eventArgument = "R" + num3.ToString(CultureInfo.InvariantCulture);
					string title = null;
					if (useAccessibleHeader)
					{
						title = SR.GetString("Calendar_SelectWeekTitle", new object[]
						{
							(num2 + 1).ToString(CultureInfo.InvariantCulture)
						});
					}
					this.RenderCalendarCell(writer, tableItemStyle, selectWeekText, title, buttonsActive, eventArgument);
				}
				for (int j = 0; j < 7; j++)
				{
					if (i > 0)
					{
						j += i;
						while (i > 0)
						{
							writer.RenderBeginTag(HtmlTextWriterTag.Td);
							writer.RenderEndTag();
							i--;
						}
					}
					else if (flag6)
					{
						while (j < 7)
						{
							writer.RenderBeginTag(HtmlTextWriterTag.Td);
							writer.RenderEndTag();
							j++;
						}
						break;
					}
					int dayOfWeek = (int)this.threadCalendar.GetDayOfWeek(dateTime);
					int dayOfMonth = this.threadCalendar.GetDayOfMonth(dateTime);
					string text;
					if (dayOfMonth <= 31 && flag2)
					{
						text = Calendar.cachedNumbers[dayOfMonth];
					}
					else
					{
						text = dateTime.ToString("dd", CultureInfo.CurrentCulture);
					}
					CalendarDay calendarDay = new CalendarDay(dateTime, dayOfWeek == 0 || dayOfWeek == 6, dateTime.Equals(todaysDate), this.selectedDates != null && this.selectedDates.Contains(dateTime), this.threadCalendar.GetMonth(dateTime) != month, text);
					int num4 = 16;
					if (calendarDay.IsSelected)
					{
						num4 |= 8;
					}
					if (calendarDay.IsOtherMonth)
					{
						num4 |= 2;
					}
					if (calendarDay.IsToday)
					{
						num4 |= 4;
					}
					if (calendarDay.IsWeekend)
					{
						num4 |= 1;
					}
					int num5 = definedStyleMask & num4;
					int num6 = num5 & 15;
					TableItemStyle tableItemStyle2 = array[num6];
					if (tableItemStyle2 == null)
					{
						tableItemStyle2 = new TableItemStyle();
						this.SetDayStyles(tableItemStyle2, num5, defaultWidth);
						array[num6] = tableItemStyle2;
					}
					string title2 = null;
					if (useAccessibleHeader)
					{
						title2 = dateTime.ToString("m", CultureInfo.CurrentCulture);
					}
					if (flag3)
					{
						TableCell tableCell = new TableCell();
						tableCell.ApplyStyle(tableItemStyle2);
						LiteralControl literalControl = new LiteralControl(text);
						tableCell.Controls.Add(literalControl);
						calendarDay.IsSelectable = flag4;
						this.OnDayRender(tableCell, calendarDay);
						literalControl.Text = this.GetCalendarButtonText(num.ToString(CultureInfo.InvariantCulture), text, title2, buttonsActive && calendarDay.IsSelectable, tableCell.ForeColor);
						tableCell.RenderControl(writer);
					}
					else
					{
						if (flag5 && tableItemStyle2.ForeColor.IsEmpty)
						{
							tableItemStyle2.ForeColor = this.defaultForeColor;
						}
						this.RenderCalendarCell(writer, tableItemStyle2, text, title2, flag4, num.ToString(CultureInfo.InvariantCulture));
					}
					if (flag7 && dateTime.Month == this.maxSupportedDate.Month && dateTime.Day == this.maxSupportedDate.Day)
					{
						flag6 = true;
					}
					else
					{
						dateTime = this.threadCalendar.AddDays(dateTime, 1);
						num++;
					}
				}
				writer.Write("</tr>");
				num2++;
			}
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0008A764 File Offset: 0x00088964
		private void RenderTitle(HtmlTextWriter writer, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive, bool useAccessibleHeader)
		{
			writer.Write("<tr>");
			TableCell tableCell = new TableCell();
			Table table = new Table();
			tableCell.ColumnSpan = (this.HasWeekSelectors(selectionMode) ? 8 : 7);
			tableCell.BackColor = Color.Silver;
			table.GridLines = GridLines.None;
			table.Width = Unit.Percentage(100.0);
			table.CellSpacing = 0;
			TableItemStyle tableItemStyle = this.TitleStyle;
			this.ApplyTitleStyle(tableCell, table, tableItemStyle);
			tableCell.RenderBeginTag(writer);
			table.RenderBeginTag(writer);
			writer.Write("<tr>");
			NextPrevFormat nextPrevFormat = this.NextPrevFormat;
			TableItemStyle tableItemStyle2 = new TableItemStyle();
			tableItemStyle2.Width = Unit.Percentage(15.0);
			tableItemStyle2.CopyFrom(this.NextPrevStyle);
			if (this.ShowNextPrevMonth)
			{
				if (this.IsMinSupportedYearMonth(visibleDate))
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.RenderEndTag();
				}
				else
				{
					string text;
					if (nextPrevFormat == NextPrevFormat.ShortMonth || nextPrevFormat == NextPrevFormat.FullMonth)
					{
						int month = this.threadCalendar.GetMonth(this.threadCalendar.AddMonths(visibleDate, -1));
						text = this.GetMonthName(month, nextPrevFormat == NextPrevFormat.FullMonth);
					}
					else
					{
						text = this.PrevMonthText;
					}
					DateTime date = this.threadCalendar.AddMonths(this.minSupportedDate, 1);
					DateTime dateTime;
					if (this.IsTheSameYearMonth(date, visibleDate))
					{
						dateTime = this.minSupportedDate;
					}
					else
					{
						dateTime = this.threadCalendar.AddMonths(visibleDate, -1);
					}
					string eventArgument = "V" + dateTime.Subtract(Calendar.baseDate).Days.ToString(CultureInfo.InvariantCulture);
					string title = null;
					if (useAccessibleHeader)
					{
						title = SR.GetString("Calendar_PreviousMonthTitle");
					}
					this.RenderCalendarCell(writer, tableItemStyle2, text, title, buttonsActive, eventArgument);
				}
			}
			TableItemStyle tableItemStyle3 = new TableItemStyle();
			if (tableItemStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				tableItemStyle3.HorizontalAlign = tableItemStyle.HorizontalAlign;
			}
			else
			{
				tableItemStyle3.HorizontalAlign = HorizontalAlign.Center;
			}
			tableItemStyle3.Wrap = tableItemStyle.Wrap;
			tableItemStyle3.Width = Unit.Percentage(70.0);
			TitleFormat titleFormat = this.TitleFormat;
			string text3;
			if (titleFormat != TitleFormat.Month)
			{
				if (titleFormat != TitleFormat.MonthYear)
				{
				}
				string text2 = DateTimeFormatInfo.CurrentInfo.YearMonthPattern;
				if (text2.IndexOf(',') >= 0)
				{
					text2 = "MMMM yyyy";
				}
				text3 = visibleDate.ToString(text2, CultureInfo.CurrentCulture);
			}
			else
			{
				text3 = visibleDate.ToString("MMMM", CultureInfo.CurrentCulture);
			}
			this.RenderCalendarCell(writer, tableItemStyle3, text3, null, false, null);
			if (this.ShowNextPrevMonth)
			{
				if (this.IsMaxSupportedYearMonth(visibleDate))
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					writer.RenderEndTag();
				}
				else
				{
					tableItemStyle2.HorizontalAlign = HorizontalAlign.Right;
					string text4;
					if (nextPrevFormat == NextPrevFormat.ShortMonth || nextPrevFormat == NextPrevFormat.FullMonth)
					{
						int month2 = this.threadCalendar.GetMonth(this.threadCalendar.AddMonths(visibleDate, 1));
						text4 = this.GetMonthName(month2, nextPrevFormat == NextPrevFormat.FullMonth);
					}
					else
					{
						text4 = this.NextMonthText;
					}
					string eventArgument2 = "V" + this.threadCalendar.AddMonths(visibleDate, 1).Subtract(Calendar.baseDate).Days.ToString(CultureInfo.InvariantCulture);
					string title2 = null;
					if (useAccessibleHeader)
					{
						title2 = SR.GetString("Calendar_NextMonthTitle");
					}
					this.RenderCalendarCell(writer, tableItemStyle2, text4, title2, buttonsActive, eventArgument2);
				}
			}
			writer.Write("</tr>");
			table.RenderEndTag(writer);
			tableCell.RenderEndTag(writer);
			writer.Write("</tr>");
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x0008AAAC File Offset: 0x00088CAC
		protected override object SaveViewState()
		{
			if (this.SelectedDates.Count > 0)
			{
				this.ViewState["SD"] = this.dateList;
			}
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this.titleStyle != null) ? ((IStateManager)this.titleStyle).SaveViewState() : null,
				(this.nextPrevStyle != null) ? ((IStateManager)this.nextPrevStyle).SaveViewState() : null,
				(this.dayStyle != null) ? ((IStateManager)this.dayStyle).SaveViewState() : null,
				(this.dayHeaderStyle != null) ? ((IStateManager)this.dayHeaderStyle).SaveViewState() : null,
				(this.todayDayStyle != null) ? ((IStateManager)this.todayDayStyle).SaveViewState() : null,
				(this.weekendDayStyle != null) ? ((IStateManager)this.weekendDayStyle).SaveViewState() : null,
				(this.otherMonthDayStyle != null) ? ((IStateManager)this.otherMonthDayStyle).SaveViewState() : null,
				(this.selectedDayStyle != null) ? ((IStateManager)this.selectedDayStyle).SaveViewState() : null,
				(this.selectorStyle != null) ? ((IStateManager)this.selectorStyle).SaveViewState() : null
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0008ABE8 File Offset: 0x00088DE8
		private void SelectRange(DateTime dateFrom, DateTime dateTo)
		{
			TimeSpan timeSpan = dateTo - dateFrom;
			if (this.SelectedDates.Count != timeSpan.Days + 1 || this.SelectedDates[0] != dateFrom || this.SelectedDates[this.SelectedDates.Count - 1] != dateTo)
			{
				this.SelectedDates.SelectRange(dateFrom, dateTo);
				this.OnSelectionChanged();
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x0008AC5C File Offset: 0x00088E5C
		private void SetDayStyles(TableItemStyle style, int styleMask, Unit defaultWidth)
		{
			style.Width = defaultWidth;
			style.HorizontalAlign = HorizontalAlign.Center;
			if ((styleMask & 16) != 0)
			{
				style.CopyFrom(this.DayStyle);
			}
			if ((styleMask & 1) != 0)
			{
				style.CopyFrom(this.WeekendDayStyle);
			}
			if ((styleMask & 2) != 0)
			{
				style.CopyFrom(this.OtherMonthDayStyle);
			}
			if ((styleMask & 4) != 0)
			{
				style.CopyFrom(this.TodayDayStyle);
			}
			if ((styleMask & 8) != 0)
			{
				style.ForeColor = Color.White;
				style.BackColor = Color.Silver;
				style.CopyFrom(this.SelectedDayStyle);
			}
		}

		// Token: 0x04001E9F RID: 7839
		private static readonly object EventDayRender = new object();

		// Token: 0x04001EA0 RID: 7840
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x04001EA1 RID: 7841
		private static readonly object EventVisibleMonthChanged = new object();

		// Token: 0x04001EA2 RID: 7842
		private TableItemStyle titleStyle;

		// Token: 0x04001EA3 RID: 7843
		private TableItemStyle nextPrevStyle;

		// Token: 0x04001EA4 RID: 7844
		private TableItemStyle dayHeaderStyle;

		// Token: 0x04001EA5 RID: 7845
		private TableItemStyle selectorStyle;

		// Token: 0x04001EA6 RID: 7846
		private TableItemStyle dayStyle;

		// Token: 0x04001EA7 RID: 7847
		private TableItemStyle otherMonthDayStyle;

		// Token: 0x04001EA8 RID: 7848
		private TableItemStyle todayDayStyle;

		// Token: 0x04001EA9 RID: 7849
		private TableItemStyle selectedDayStyle;

		// Token: 0x04001EAA RID: 7850
		private TableItemStyle weekendDayStyle;

		// Token: 0x04001EAB RID: 7851
		private string defaultButtonColorText;

		// Token: 0x04001EAC RID: 7852
		private static readonly Color DefaultForeColor = Color.Black;

		// Token: 0x04001EAD RID: 7853
		private Color defaultForeColor;

		// Token: 0x04001EAE RID: 7854
		private ArrayList dateList;

		// Token: 0x04001EAF RID: 7855
		private SelectedDatesCollection selectedDates;

		// Token: 0x04001EB0 RID: 7856
		private Calendar threadCalendar;

		// Token: 0x04001EB1 RID: 7857
		private DateTime minSupportedDate;

		// Token: 0x04001EB2 RID: 7858
		private DateTime maxSupportedDate;

		// Token: 0x04001EB3 RID: 7859
		private const string SELECT_RANGE_COMMAND = "R";

		// Token: 0x04001EB4 RID: 7860
		private const string NAVIGATE_MONTH_COMMAND = "V";

		// Token: 0x04001EB5 RID: 7861
		private static DateTime baseDate = new DateTime(2000, 1, 1);

		// Token: 0x04001EB6 RID: 7862
		private const int STYLEMASK_DAY = 16;

		// Token: 0x04001EB7 RID: 7863
		private const int STYLEMASK_UNIQUE = 15;

		// Token: 0x04001EB8 RID: 7864
		private const int STYLEMASK_SELECTED = 8;

		// Token: 0x04001EB9 RID: 7865
		private const int STYLEMASK_TODAY = 4;

		// Token: 0x04001EBA RID: 7866
		private const int STYLEMASK_OTHERMONTH = 2;

		// Token: 0x04001EBB RID: 7867
		private const int STYLEMASK_WEEKEND = 1;

		// Token: 0x04001EBC RID: 7868
		private const string ROWBEGINTAG = "<tr>";

		// Token: 0x04001EBD RID: 7869
		private const string ROWENDTAG = "</tr>";

		// Token: 0x04001EBE RID: 7870
		private const int cachedNumberMax = 31;

		// Token: 0x04001EBF RID: 7871
		private static readonly string[] cachedNumbers = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			"10",
			"11",
			"12",
			"13",
			"14",
			"15",
			"16",
			"17",
			"18",
			"19",
			"20",
			"21",
			"22",
			"23",
			"24",
			"25",
			"26",
			"27",
			"28",
			"29",
			"30",
			"31"
		};
	}
}
