using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000062 RID: 98
	[TargetControlType(typeof(TextBox))]
	[ClientCssResource("Calendar")]
	[ClientScriptResource("Sys.Extended.UI.CalendarBehavior", "Calendar")]
	[ToolboxBitmap(typeof(Accessor), "Calendar.bmp")]
	[RequiredScript(typeof(ThreadingScripts), 4)]
	[Designer(typeof(CalendarExtenderDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[RequiredScript(typeof(DateTimeScripts), 1)]
	[RequiredScript(typeof(PopupExtender), 2)]
	[RequiredScript(typeof(AnimationScripts), 3)]
	public class CalendarExtender : ExtenderControlBase
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A536 File Offset: 0x00008736
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000A548 File Offset: 0x00008748
		[ClientPropertyName("cssClass")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public virtual string CssClass
		{
			get
			{
				return base.GetPropertyValue<string>("CssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CssClass", value);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A556 File Offset: 0x00008756
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000A568 File Offset: 0x00008768
		[ClientPropertyName("format")]
		[DefaultValue("d")]
		[ExtenderControlProperty]
		public virtual string Format
		{
			get
			{
				return base.GetPropertyValue<string>("Format", "d");
			}
			set
			{
				base.SetPropertyValue<string>("Format", value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A576 File Offset: 0x00008776
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000A588 File Offset: 0x00008788
		[ExtenderControlProperty]
		[DefaultValue("MMMM d, yyyy")]
		[ClientPropertyName("todaysDateFormat")]
		public virtual string TodaysDateFormat
		{
			get
			{
				return base.GetPropertyValue<string>("TodaysDateFormat", "MMMM d, yyyy");
			}
			set
			{
				base.SetPropertyValue<string>("TodaysDateFormat", value);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A596 File Offset: 0x00008796
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000A5A8 File Offset: 0x000087A8
		[ExtenderControlProperty]
		[ClientPropertyName("daysModeTitleFormat")]
		[DefaultValue("MMMM, yyyy")]
		public virtual string DaysModeTitleFormat
		{
			get
			{
				return base.GetPropertyValue<string>("DaysModeTitleFormat", "MMMM, yyyy");
			}
			set
			{
				base.SetPropertyValue<string>("DaysModeTitleFormat", value);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A5B6 File Offset: 0x000087B6
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000A5C4 File Offset: 0x000087C4
		[ClientPropertyName("clearTime")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public virtual bool ClearTime
		{
			get
			{
				return base.GetPropertyValue<bool>("ClearTime", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ClearTime", value);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A5D2 File Offset: 0x000087D2
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000A5E0 File Offset: 0x000087E0
		[ClientPropertyName("enabled")]
		[DefaultValue(true)]
		[ExtenderControlProperty]
		public virtual bool EnabledOnClient
		{
			get
			{
				return base.GetPropertyValue<bool>("EnabledOnClient", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnabledOnClient", value);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A5EE File Offset: 0x000087EE
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000A5FC File Offset: 0x000087FC
		[ExtenderControlProperty]
		[ClientPropertyName("animated")]
		[DefaultValue(true)]
		public virtual bool Animated
		{
			get
			{
				return base.GetPropertyValue<bool>("Animated", true);
			}
			set
			{
				base.SetPropertyValue<bool>("Animated", value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A60A File Offset: 0x0000880A
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000A618 File Offset: 0x00008818
		[DefaultValue(FirstDayOfWeek.Default)]
		[ExtenderControlProperty]
		[ClientPropertyName("firstDayOfWeek")]
		public virtual FirstDayOfWeek FirstDayOfWeek
		{
			get
			{
				return base.GetPropertyValue<FirstDayOfWeek>("FirstDayOfWeek", FirstDayOfWeek.Default);
			}
			set
			{
				base.SetPropertyValue<FirstDayOfWeek>("FirstDayOfWeek", value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A626 File Offset: 0x00008826
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000A638 File Offset: 0x00008838
		[DefaultValue("")]
		[ClientPropertyName("button")]
		[ElementReference]
		[ExtenderControlProperty]
		[IDReferenceProperty]
		public virtual string PopupButtonID
		{
			get
			{
				return base.GetPropertyValue<string>("PopupButtonID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopupButtonID", value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A646 File Offset: 0x00008846
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000A654 File Offset: 0x00008854
		[ExtenderControlProperty]
		[DefaultValue(CalendarPosition.BottomLeft)]
		[Description("Indicates where you want the calendar displayed, bottom or top of the textbox.")]
		[ClientPropertyName("popupPosition")]
		public virtual CalendarPosition PopupPosition
		{
			get
			{
				return base.GetPropertyValue<CalendarPosition>("PopupPosition", CalendarPosition.BottomLeft);
			}
			set
			{
				base.SetPropertyValue<CalendarPosition>("PopupPosition", value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A664 File Offset: 0x00008864
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000A6AC File Offset: 0x000088AC
		[ClientPropertyName("selectedDate")]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		public DateTime? SelectedDate
		{
			get
			{
				DateTime? propertyValue = base.GetPropertyValue<DateTime?>("SelectedDate", null);
				if (propertyValue == null)
				{
					return null;
				}
				return new DateTime?(DateTime.SpecifyKind(propertyValue.Value, DateTimeKind.Utc));
			}
			set
			{
				DateTime? value2 = (value != null) ? new DateTime?(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)) : null;
				base.SetPropertyValue<DateTime?>("SelectedDate", value2);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000A6EC File Offset: 0x000088EC
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000A6FA File Offset: 0x000088FA
		[DefaultValue(CalendarDefaultView.Days)]
		[ClientPropertyName("defaultView")]
		[ExtenderControlProperty]
		[Description("Default view of the calendar when it first pops up.")]
		public virtual CalendarDefaultView DefaultView
		{
			get
			{
				return base.GetPropertyValue<CalendarDefaultView>("DefaultView", CalendarDefaultView.Days);
			}
			set
			{
				base.SetPropertyValue<CalendarDefaultView>("DefaultView", value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000A708 File Offset: 0x00008908
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000A71A File Offset: 0x0000891A
		[ClientPropertyName("showing")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public virtual string OnClientShowing
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShowing", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShowing", value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000A728 File Offset: 0x00008928
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000A73A File Offset: 0x0000893A
		[ClientPropertyName("shown")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public virtual string OnClientShown
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientShown", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientShown", value);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000A748 File Offset: 0x00008948
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000A75A File Offset: 0x0000895A
		[DefaultValue("")]
		[ClientPropertyName("hiding")]
		[ExtenderControlEvent]
		public virtual string OnClientHiding
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHiding", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHiding", value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000A77A File Offset: 0x0000897A
		[DefaultValue("")]
		[ClientPropertyName("hidden")]
		[ExtenderControlEvent]
		public virtual string OnClientHidden
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientHidden", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientHidden", value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A788 File Offset: 0x00008988
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000A79A File Offset: 0x0000899A
		[ClientPropertyName("dateSelectionChanged")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public virtual string OnClientDateSelectionChanged
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientDateSelectionChanged", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientDateSelectionChanged", value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000A7A8 File Offset: 0x000089A8
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000A7F0 File Offset: 0x000089F0
		[DefaultValue(null)]
		[ClientPropertyName("startDate")]
		[ExtenderControlProperty]
		public DateTime? StartDate
		{
			get
			{
				DateTime? propertyValue = base.GetPropertyValue<DateTime?>("StartDate", null);
				if (propertyValue == null)
				{
					return null;
				}
				return new DateTime?(DateTime.SpecifyKind(propertyValue.Value, DateTimeKind.Utc));
			}
			set
			{
				base.SetPropertyValue<DateTime?>("StartDate", (value != null) ? new DateTime?(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)) : null);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A830 File Offset: 0x00008A30
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000A878 File Offset: 0x00008A78
		[ClientPropertyName("endDate")]
		[ExtenderControlProperty]
		[DefaultValue(null)]
		public DateTime? EndDate
		{
			get
			{
				DateTime? propertyValue = base.GetPropertyValue<DateTime?>("EndDate", null);
				if (propertyValue == null)
				{
					return null;
				}
				return new DateTime?(DateTime.SpecifyKind(propertyValue.Value, DateTimeKind.Utc));
			}
			set
			{
				base.SetPropertyValue<DateTime?>("EndDate", (value != null) ? new DateTime?(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)) : null);
			}
		}
	}
}
