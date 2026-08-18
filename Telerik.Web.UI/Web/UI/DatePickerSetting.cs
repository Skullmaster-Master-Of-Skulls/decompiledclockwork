using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200055D RID: 1373
	public class DatePickerSetting : DateInputSetting
	{
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000A2944 File Offset: 0x000A0B44
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x000A2971 File Offset: 0x000A0B71
		[Description("The ID of the RadCalendar that will be shared among several RadDatePickers.")]
		[DefaultValue("")]
		[Category("Behavior")]
		[TypeConverter("Telerik.Web.Design.CalendarIdConverter")]
		public virtual string SharedCalendarID
		{
			get
			{
				object obj = base.ViewState["SharedCalendarID"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["SharedCalendarID"] = value;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000A2984 File Offset: 0x000A0B84
		// (set) Token: 0x06003179 RID: 12665 RVA: 0x000A29AF File Offset: 0x000A0BAF
		[ClientControlProperty]
		[ClientPropertyName("_enableShadows")]
		[Category("Appearance")]
		[Description("Gets or sets whether popup shadows will appear.")]
		[DefaultValue(true)]
		public bool EnableShadows
		{
			get
			{
				return base.ViewState["EnableShadows"] == null || (bool)base.ViewState["EnableShadows"];
			}
			set
			{
				base.ViewState["EnableShadows"] = value;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000A29C8 File Offset: 0x000A0BC8
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x000A2A02 File Offset: 0x000A0C02
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets a value indicating whether the picker will create an overlay element to ensure popups are over a flash element or Java applet.")]
		[Bindable(true)]
		[DefaultValue(false)]
		[ClientPropertyName("_overlay")]
		[Browsable(true)]
		public bool Overlay
		{
			get
			{
				bool? flag = base.ViewState["Overlay"] as bool?;
				return flag != null && flag.Value;
			}
			set
			{
				base.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000A2A1A File Offset: 0x000A0C1A
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000A2A46 File Offset: 0x000A0C46
		[ClientPropertyName("_popupDirection")]
		[ClientControlProperty]
		[Description("Gets or sets the direction in which the popup Calendar (or TimeView) is displayed, with relation to the DatePicker control.")]
		[Category("Behavior")]
		[DefaultValue(DatePickerPopupDirection.BottomRight)]
		public DatePickerPopupDirection PopupDirection
		{
			get
			{
				if (base.ViewState["PopupDirection"] == null)
				{
					return DatePickerPopupDirection.BottomRight;
				}
				return (DatePickerPopupDirection)base.ViewState["PopupDirection"];
			}
			set
			{
				base.ViewState["PopupDirection"] = value;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000A2A5E File Offset: 0x000A0C5E
		// (set) Token: 0x0600317F RID: 12671 RVA: 0x000A2A89 File Offset: 0x000A0C89
		[Description("Gets or sets whether the screen boundaries should be taken into consideration when the Calendar or TimeView are displayed.")]
		[ClientControlProperty]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
		[Category("Behavior")]
		[DefaultValue(true)]
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

		// Token: 0x06003180 RID: 12672 RVA: 0x000A2AA4 File Offset: 0x000A0CA4
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			if (this.PopupDirection != DatePickerPopupDirection.BottomRight)
			{
				descriptor.AddProperty("_popupDirection", this.PopupDirection);
			}
			if (this.Overlay)
			{
				descriptor.AddProperty("_overlay", true);
			}
			if (!this.EnableShadows)
			{
				descriptor.AddProperty("_enableShadows", false);
			}
			if (!this.EnableScreenBoundaryDetection)
			{
				descriptor.AddProperty("_enableScreenBoundaryDetection", false);
			}
			if (this.SharedCalendar != null)
			{
				descriptor.AddComponentProperty("calendar", this.SharedCalendar.ClientID);
			}
		}

		// Token: 0x04000D62 RID: 3426
		internal RadCalendar SharedCalendar;
	}
}
