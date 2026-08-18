using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Web.UI.Design.DatePickerAttributes;

namespace Telerik.Web.UI
{
	// Token: 0x02000A34 RID: 2612
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MonthYearNavigationSettings : MonthYearFastNavigationSettings
	{
		// Token: 0x060062D6 RID: 25302 RVA: 0x00173E98 File Offset: 0x00172098
		public MonthYearNavigationSettings(StateBag OwnerStateBag, RadMonthYearPicker owner) : base(OwnerStateBag, owner)
		{
			this.owner = owner;
		}

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x00173EAC File Offset: 0x001720AC
		// (set) Token: 0x060062D8 RID: 25304 RVA: 0x00173EE8 File Offset: 0x001720E8
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the image that is displayed for the next year navigation control.")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Navigation Management")]
		[Localizable(true)]
		[UrlProperty]
		public string NavigationNextImage
		{
			get
			{
				object obj = base.ViewState["_mNavigationNextImg"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string text = base.ViewState["_mNavigationNextImg"] as string;
				base.ViewState["_mNavigationNextImg"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x00173F4D File Offset: 0x0017214D
		// (set) Token: 0x060062DA RID: 25306 RVA: 0x00173F55 File Offset: 0x00172155
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool EnableTodayButtonSelection
		{
			get
			{
				return base.EnableTodayButtonSelection;
			}
			set
			{
				base.EnableTodayButtonSelection = value;
			}
		}

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x00173F60 File Offset: 0x00172160
		// (set) Token: 0x060062DC RID: 25308 RVA: 0x00173FA8 File Offset: 0x001721A8
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("&gt;")]
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the next year navigation control.")]
		public string NavigationNextText
		{
			get
			{
				object obj = base.ViewState["_mNavigationNextText"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationNextText;
			}
			set
			{
				string text = base.ViewState["_mNavigationNextText"] as string;
				base.ViewState["_mNavigationNextText"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x00174010 File Offset: 0x00172210
		// (set) Token: 0x060062DE RID: 25310 RVA: 0x00174058 File Offset: 0x00172258
		[DatePickerBrowsable(false)]
		[DefaultValue(">")]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the next year navigation control.")]
		public string NavigationNextToolTip
		{
			get
			{
				object obj = base.ViewState["_mNavigationNextToolTip"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationNextToolTip;
			}
			set
			{
				string text = base.ViewState["_mNavigationNextToolTip"] as string;
				base.ViewState["_mNavigationNextToolTip"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x001740C0 File Offset: 0x001722C0
		// (set) Token: 0x060062E0 RID: 25312 RVA: 0x001740FC File Offset: 0x001722FC
		[UrlProperty]
		[Category("Navigation Management")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Description("Gets or sets name of the image that is displayed for the previous year navigation control.")]
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string NavigationPrevImage
		{
			get
			{
				object obj = base.ViewState["_mNavigationPrevImage"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string text = base.ViewState["_mNavigationPrevImage"] as string;
				base.ViewState["_mNavigationPrevImage"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x00174164 File Offset: 0x00172364
		// (set) Token: 0x060062E2 RID: 25314 RVA: 0x001741AC File Offset: 0x001723AC
		[Description("Gets or sets the text displayed for the previous year navigation control.")]
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;")]
		[Localizable(true)]
		[Category("Navigation Management")]
		public string NavigationPrevText
		{
			get
			{
				object obj = base.ViewState["_mNavigationPrevText"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationPrevText;
			}
			set
			{
				string text = base.ViewState["_mNavigationPrevText"] as string;
				base.ViewState["_mNavigationPrevText"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x00174214 File Offset: 0x00172414
		// (set) Token: 0x060062E4 RID: 25316 RVA: 0x0017425C File Offset: 0x0017245C
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the previous year navigation control.")]
		[DatePickerBrowsable(false)]
		[DefaultValue("<")]
		public string NavigationPrevToolTip
		{
			get
			{
				object obj = base.ViewState["_mNavigationPrevToolTip"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationPrevToolTip;
			}
			set
			{
				string text = base.ViewState["_mNavigationPrevToolTip"] as string;
				base.ViewState["_mNavigationPrevToolTip"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x001742C4 File Offset: 0x001724C4
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x0017430C File Offset: 0x0017250C
		[Description("The caption of the \"Today\" button.")]
		[DefaultValue("Today")]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public override string TodayButtonCaption
		{
			get
			{
				object obj = base.ViewState["_mTodayButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationTodayButtonCaption;
			}
			set
			{
				string text = base.ViewState["_mTodayButtonCaption"] as string;
				base.ViewState["_mTodayButtonCaption"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x00174374 File Offset: 0x00172574
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x001743BC File Offset: 0x001725BC
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("OK")]
		[Description("The caption of the \"OK\" button.")]
		public override string OkButtonCaption
		{
			get
			{
				object obj = base.ViewState["_mOkButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationOkButtonCaption;
			}
			set
			{
				string text = base.ViewState["_mOkButtonCaption"] as string;
				base.ViewState["_mOkButtonCaption"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x00174424 File Offset: 0x00172624
		// (set) Token: 0x060062EA RID: 25322 RVA: 0x0017446C File Offset: 0x0017266C
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Cancel")]
		[Description("The caption of the \"Cancel\" button.")]
		public override string CancelButtonCaption
		{
			get
			{
				object obj = base.ViewState["_mCancelButtonCaption"];
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					return (string)obj;
				}
				return this.owner.Localization.MonthYearNavigationCancelButtonCaption;
			}
			set
			{
				string text = base.ViewState["_mCancelButtonCaption"] as string;
				base.ViewState["_mCancelButtonCaption"] = value;
				if (text != null || !string.IsNullOrEmpty(value))
				{
					string a = text;
					if (a != value)
					{
						this.owner.EnsureChildControls();
						this.owner.MonthYearTableView.RecreateNavigationChildControls();
					}
				}
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x060062EB RID: 25323 RVA: 0x001744D1 File Offset: 0x001726D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DatePickerBrowsable(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public new CalendarAnimationSettings ShowAnimation
		{
			get
			{
				return base.ShowAnimation;
			}
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x060062EC RID: 25324 RVA: 0x001744D9 File Offset: 0x001726D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DatePickerBrowsable(false)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public new CalendarAnimationSettings HideAnimation
		{
			get
			{
				return base.HideAnimation;
			}
		}

		// Token: 0x0400182E RID: 6190
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected new RadMonthYearPicker owner;
	}
}
