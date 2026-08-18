using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000A33 RID: 2611
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MonthYearFastNavigationSettings : ObjectWithState
	{
		// Token: 0x060062C3 RID: 25283 RVA: 0x00173B94 File Offset: 0x00171D94
		public MonthYearFastNavigationSettings(StateBag OwnerStateBag, RadCalendar owner) : base("fns_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x00173BA9 File Offset: 0x00171DA9
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "owner")]
		public MonthYearFastNavigationSettings(StateBag OwnerStateBag, RadMonthYearPicker owner) : base("fns_", OwnerStateBag)
		{
			this.picker = owner;
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x060062C5 RID: 25285 RVA: 0x00173BBE File Offset: 0x00171DBE
		private LocalizationStrings Localization
		{
			get
			{
				if (this.picker != null)
				{
					return this.picker.Localization;
				}
				return this.owner.Localization;
			}
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x060062C6 RID: 25286 RVA: 0x00173BE0 File Offset: 0x00171DE0
		// (set) Token: 0x060062C7 RID: 25287 RVA: 0x00173C39 File Offset: 0x00171E39
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Today")]
		[Description("The caption of the \"Today\" button.")]
		[Localizable(true)]
		public virtual string TodayButtonCaption
		{
			get
			{
				object obj = base.ViewState["TodayButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.Localization.GetStringSafe("FastNavigationTodayButtonCaption") ?? this.Localization.GetString("MonthYearNavigationTodayButtonCaption");
			}
			set
			{
				base.ViewState["TodayButtonCaption"] = value;
			}
		}

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x00173C4C File Offset: 0x00171E4C
		// (set) Token: 0x060062C9 RID: 25289 RVA: 0x00173CA5 File Offset: 0x00171EA5
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("OK")]
		[Description("The caption of the \"OK\" button.")]
		public virtual string OkButtonCaption
		{
			get
			{
				object obj = base.ViewState["OkButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.Localization.GetStringSafe("FastNavigationOkButtonCaption") ?? this.Localization.GetString("MonthYearNavigationOkButtonCaption");
			}
			set
			{
				base.ViewState["OkButtonCaption"] = value;
			}
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x00173CB8 File Offset: 0x00171EB8
		// (set) Token: 0x060062CB RID: 25291 RVA: 0x00173D11 File Offset: 0x00171F11
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Cancel")]
		[Description("The caption of the \"Cancel\" button.")]
		public virtual string CancelButtonCaption
		{
			get
			{
				object obj = base.ViewState["CancelButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.Localization.GetStringSafe("FastNavigationCancelButtonCaption") ?? this.Localization.GetString("MonthYearNavigationCancelButtonCaption");
			}
			set
			{
				base.ViewState["CancelButtonCaption"] = value;
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x00173D24 File Offset: 0x00171F24
		// (set) Token: 0x060062CD RID: 25293 RVA: 0x00173D70 File Offset: 0x00171F70
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Date is out of range.")]
		[NotifyParentProperty(true)]
		[Description("The value of the \"Date is out of range\" error message")]
		[Localizable(true)]
		public virtual string DateIsOutOfRangeMessage
		{
			get
			{
				object obj = base.ViewState["DateIsOutOfRangeMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.Localization.GetStringSafe("FastNavigationDateIsOutOfRangeMessage") ?? this.Localization.GetString("MonthYearNavigationDateIsOutOfRangeMessage");
			}
			set
			{
				base.ViewState["DateIsOutOfRangeMessage"] = value;
			}
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x00173D84 File Offset: 0x00171F84
		// (set) Token: 0x060062CF RID: 25295 RVA: 0x00173DAD File Offset: 0x00171FAD
		[Description("Indicates whether the Today button performs date selection or navigation only.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool EnableTodayButtonSelection
		{
			get
			{
				object obj = base.ViewState["EnableTodayButtonSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableTodayButtonSelection"] = value;
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x00173DC8 File Offset: 0x00171FC8
		// (set) Token: 0x060062D1 RID: 25297 RVA: 0x00173DF1 File Offset: 0x00171FF1
		[Description("Indicates whether the months that are out of range will be disabled.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Appearance")]
		public virtual bool DisableOutOfRangeMonths
		{
			get
			{
				object obj = base.ViewState["DisableOutOfRangeMonths"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["DisableOutOfRangeMonths"] = value;
			}
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x060062D2 RID: 25298 RVA: 0x00173E09 File Offset: 0x00172009
		// (set) Token: 0x060062D3 RID: 25299 RVA: 0x00173E34 File Offset: 0x00172034
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets whether the screen boundaries should be taken into consideration when the Fast Navigation Popup is displayed.")]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return base.ViewState["EnableScreenBoundaryDetection"] == null || (bool)base.ViewState["EnableScreenBoundaryDetection"];
			}
			set
			{
				base.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x00173E4C File Offset: 0x0017204C
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CalendarAnimationSettings ShowAnimation
		{
			get
			{
				if (this._showAnimation == null)
				{
					this._showAnimation = new CalendarAnimationSettings("show", base.OwnerViewState);
				}
				return this._showAnimation;
			}
		}

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x00173E72 File Offset: 0x00172072
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CalendarAnimationSettings HideAnimation
		{
			get
			{
				if (this._hideAnimation == null)
				{
					this._hideAnimation = new CalendarAnimationSettings("hide", base.OwnerViewState);
				}
				return this._hideAnimation;
			}
		}

		// Token: 0x0400182A RID: 6186
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public RadCalendar owner;

		// Token: 0x0400182B RID: 6187
		private RadMonthYearPicker picker;

		// Token: 0x0400182C RID: 6188
		private CalendarAnimationSettings _showAnimation;

		// Token: 0x0400182D RID: 6189
		private CalendarAnimationSettings _hideAnimation;
	}
}
