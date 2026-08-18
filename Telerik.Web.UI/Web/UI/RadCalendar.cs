using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.Collections;
using Telerik.Web.UI.Calendar.Persistence;
using Telerik.Web.UI.Calendar.Utils;
using Telerik.Web.UI.Calendar.View;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Design.DatePickerAttributes;

namespace Telerik.Web.UI
{
	// Token: 0x02000199 RID: 409
	[ControlValueProperty("SelectedDate")]
	[ClientScriptResource("Telerik.Web.UI.RadCalendar", "Telerik.Web.UI.Calendar.RadCalendarCommonScript.js")]
	[EmbeddedSkin("Calendar", typeof(RadCalendar))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadCalendar))]
	[ToolboxBitmap(typeof(RadCalendar), "Telerik.Web.UI.Calendar.png")]
	[ToolboxData("<{0}:RadCalendar Runat=\"server\"></{0}:RadCalendar>")]
	[TelerikToolboxCategory("Calendar, Scheduler and Gantt")]
	[Designer("Telerik.Web.Design.RadCalendarDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultEvent("SelectionChanged")]
	[DefaultProperty("SelectedDate")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadCalendar))]
	[Description("Telerik RadCalendar")]
	[ClientScriptResource("Telerik.Web.UI.RadCalendar", "Telerik.Web.UI.Calendar.RadCalendarScript.js")]
	[EmbeddedSkin("Calendar", "Default", typeof(RadCalendar))]
	[ClientScriptResource("Telerik.Web.UI.RadCalendar", "Telerik.Web.UI.Common.Navigation.OverlayScript.js")]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredScript(typeof(jQuery))]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadCalendar))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadCalendar))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadCalendar : PropertiesControl, ILocalizableControl, IPostBackEventHandler, INamingContainer
	{
		// Token: 0x06000DDB RID: 3547 RVA: 0x0003467D File Offset: 0x0003287D
		public RadCalendar()
		{
			this._CalendarDayTemplates = new CalendarDayTemplateCollection();
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x000346F0 File Offset: 0x000328F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Navigation Management")]
		[Description("The subproperties can be used to modify the fast Month/Year navigation popup settings.")]
		public MonthYearFastNavigationSettings FastNavigationSettings
		{
			get
			{
				if (this._fastNavigaionSettings == null)
				{
					this._fastNavigaionSettings = new MonthYearFastNavigationSettings(this.ViewState, this);
				}
				return this._fastNavigaionSettings;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00034712 File Offset: 0x00032912
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00034715 File Offset: 0x00032915
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003471D File Offset: 0x0003291D
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0003474C File Offset: 0x0003294C
		[Category("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Specifies default path for the grid images when EnableEmbeddedSkins is set to false.")]
		public virtual string ImagesPath
		{
			get
			{
				if (this.ViewState["ImagesPath"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ImagesPath"];
			}
			set
			{
				this.ViewState["ImagesPath"] = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003475F File Offset: 0x0003295F
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0003478A File Offset: 0x0003298A
		[ClientControlProperty]
		[Description("Gets or sets whether popup shadows will appear around the month-year fast navigation.")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[ClientPropertyName("_enableShadows")]
		public bool EnableShadows
		{
			get
			{
				return this.ViewState["EnableShadows"] == null || (bool)this.ViewState["EnableShadows"];
			}
			set
			{
				this.ViewState["EnableShadows"] = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000347A4 File Offset: 0x000329A4
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000347DE File Offset: 0x000329DE
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the calendar will create an overlay element to ensure popups are over a flash element or Java applet.")]
		[DefaultValue(false)]
		[Browsable(true)]
		[Bindable(true)]
		public bool Overlay
		{
			get
			{
				bool? flag = this.ViewState["Overlay"] as bool?;
				return flag != null && flag.Value;
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000347F8 File Offset: 0x000329F8
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00034832 File Offset: 0x00032A32
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Set this property to false if like to disable the selection of weekends.")]
		[DefaultValue(false)]
		[Bindable(true)]
		public bool EnableWeekends
		{
			get
			{
				bool? flag = this.ViewState["_EW"] as bool?;
				return flag == null || flag.Value;
			}
			set
			{
				this.ViewState["_EW"] = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0003484A File Offset: 0x00032A4A
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0003486B File Offset: 0x00032A6B
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00034884 File Offset: 0x00032A84
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x000348AD File Offset: 0x00032AAD
		[Category("Action")]
		[Description("Enable client side navigation with arrow keys")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool EnableKeyboardNavigation
		{
			get
			{
				object obj = this.ViewState["EnableKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableKeyboardNavigation"] = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x000348C8 File Offset: 0x00032AC8
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x000348F1 File Offset: 0x00032AF1
		[Description("Gets or sets the range selection mode of the calendar component.")]
		[NotifyParentProperty(true)]
		[Category("Action")]
		[DefaultValue(RangeSelectionMode.None)]
		public RangeSelectionMode RangeSelectionMode
		{
			get
			{
				object obj = this.Properties["RangeSelectionMode"];
				if (obj != null)
				{
					return (RangeSelectionMode)obj;
				}
				return RangeSelectionMode.None;
			}
			set
			{
				this.Properties["RangeSelectionMode"] = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00034909 File Offset: 0x00032B09
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00034911 File Offset: 0x00032B11
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003491A File Offset: 0x00032B1A
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00034922 File Offset: 0x00032B22
		[DatePickerBrowsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003492B File Offset: 0x00032B2B
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("The collection of the CalendarDayTemplates of RadCalendar.")]
		[DatePickerBrowsable(false)]
		public CalendarDayTemplateCollection CalendarDayTemplates
		{
			get
			{
				return this._CalendarDayTemplates;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00034934 File Offset: 0x00032B34
		[Description("A set of properties that get or set the names of the JavaScript functions that are invoked upon specific client-side events.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client Settings")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CalendarClientEvents ClientEvents
		{
			get
			{
				object obj = this.Properties["ClientEvents"];
				if (obj == null)
				{
					obj = (this.Properties["ClientEvents"] = new CalendarClientEvents());
				}
				return (CalendarClientEvents)obj;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00034974 File Offset: 0x00032B74
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x000349A2 File Offset: 0x00032BA2
		[ClientControlProperty]
		[DefaultValue(true)]
		[Description("Gets or sets whether the repeatable days logic should be supported on the client (effective for client calendar).")]
		[Category("Client Settings")]
		[NotifyParentProperty(true)]
		public bool EnableRepeatableDaysOnClient
		{
			get
			{
				object obj = this.Properties["EnableRepeatableDaysOnClient"];
				return !(obj is bool) || (bool)obj;
			}
			set
			{
				this.Properties["EnableRepeatableDaysOnClient"] = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x000349BC File Offset: 0x00032BBC
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x000349E9 File Offset: 0x00032BE9
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the formatting string that will be applied to the days in the calendar.")]
		[Category("MonthView Specific Settings")]
		[ClientControlProperty]
		[DefaultValue("%d")]
		public string CellDayFormat
		{
			get
			{
				string text = (string)this.Properties["CellDayFormat"];
				if (text != null)
				{
					return text;
				}
				return "%d";
			}
			set
			{
				this.Properties["CellDayFormat"] = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000349FC File Offset: 0x00032BFC
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00034A25 File Offset: 0x00032C25
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Category("General View Settings")]
		[DefaultValue(6)]
		[Description("The the count of rows to be displayed by a single CalendarView")]
		public int SingleViewRows
		{
			get
			{
				object obj = this.Properties["SingleViewRows"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 6;
			}
			set
			{
				this.Properties["SingleViewRows"] = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00034A40 File Offset: 0x00032C40
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00034A69 File Offset: 0x00032C69
		[DefaultValue(7)]
		[Category("General View Settings")]
		[NotifyParentProperty(true)]
		[Description("The the count of columns to be displayed by a single CalendarView")]
		[ClientControlProperty]
		public int SingleViewColumns
		{
			get
			{
				object obj = this.Properties["SingleViewColumns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 7;
			}
			set
			{
				this.Properties["SingleViewColumns"] = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00034A84 File Offset: 0x00032C84
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x00034AB6 File Offset: 0x00032CB6
		[Description("The width applied to a single CalendarView")]
		[DefaultValue(typeof(Unit), "0px")]
		[NotifyParentProperty(true)]
		[Category("General View Settings")]
		public Unit SingleViewWidth
		{
			get
			{
				object obj = this.Properties["SingleViewWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return new Unit("0px");
			}
			set
			{
				this.Properties["SingleViewWidth"] = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00034AD0 File Offset: 0x00032CD0
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00034B02 File Offset: 0x00032D02
		[DefaultValue(typeof(Unit), "0px")]
		[Category("General View Settings")]
		[Description("The Height applied to a single CalendarView")]
		[NotifyParentProperty(true)]
		public Unit SingleViewHeight
		{
			get
			{
				object obj = this.Properties["SingleViewHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return new Unit("0px");
			}
			set
			{
				this.Properties["SingleViewHeight"] = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00034B1C File Offset: 0x00032D1C
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00034B48 File Offset: 0x00032D48
		[Description("This property allows using presets, regarding the layout of the calendar area. Sets or gets predefined pairs of rows and columns, so that the product of the two values is exactly 42, which guarantees valid calendar layout.")]
		[DefaultValue(MonthLayout.Layout_7columns_x_6rows)]
		[NotifyParentProperty(true)]
		[Category("General View Settings")]
		public MonthLayout MonthLayout
		{
			get
			{
				object obj = this.Properties["MonthLayout"];
				if (obj != null)
				{
					return (MonthLayout)obj;
				}
				return MonthLayout.Layout_7columns_x_6rows;
			}
			set
			{
				this.Properties["MonthLayout"] = value;
				if (value <= MonthLayout.Layout_7rows_x_6columns)
				{
					switch (value)
					{
					case MonthLayout.Layout_7columns_x_6rows:
						this.SingleViewRows = 6;
						this.SingleViewColumns = 7;
						return;
					case MonthLayout.Layout_14columns_x_3rows:
						this.SingleViewRows = 3;
						this.SingleViewColumns = 14;
						return;
					case (MonthLayout)3:
						break;
					case MonthLayout.Layout_21columns_x_2rows:
						this.SingleViewRows = 2;
						this.SingleViewColumns = 21;
						return;
					default:
						if (value != MonthLayout.Layout_7rows_x_6columns)
						{
							return;
						}
						this.SingleViewRows = 7;
						this.SingleViewColumns = 6;
						return;
					}
				}
				else
				{
					if (value == MonthLayout.Layout_14rows_x_3columns)
					{
						this.SingleViewRows = 14;
						this.SingleViewColumns = 3;
						return;
					}
					if (value != MonthLayout.Layout_21rows_x_2columns)
					{
						return;
					}
					this.SingleViewRows = 21;
					this.SingleViewColumns = 2;
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00034BF8 File Offset: 0x00032DF8
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00034C21 File Offset: 0x00032E21
		[Category("General View Settings")]
		[Description("Specifies the horizonal alignment of the day cells in the calendar")]
		[DefaultValue(HorizontalAlign.Center)]
		[NotifyParentProperty(true)]
		public HorizontalAlign CellAlign
		{
			get
			{
				object obj = this.Properties["CellAlign"];
				if (obj != null)
				{
					return (HorizontalAlign)obj;
				}
				return HorizontalAlign.Center;
			}
			set
			{
				this.Properties["CellAlign"] = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00034C3C File Offset: 0x00032E3C
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00034C65 File Offset: 0x00032E65
		[Category("General View Settings")]
		[Description("Specifies the vertical alignment of the day cells in the calendar.")]
		[NotifyParentProperty(true)]
		[DefaultValue(VerticalAlign.Middle)]
		public VerticalAlign CellVAlign
		{
			get
			{
				object obj = this.Properties["CellVAlign"];
				if (obj != null)
				{
					return (VerticalAlign)obj;
				}
				return VerticalAlign.Middle;
			}
			set
			{
				this.Properties["CellVAlign"] = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00034C80 File Offset: 0x00032E80
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00034CA9 File Offset: 0x00032EA9
		[DefaultValue(1)]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the number of month rows in a multi view calendar.")]
		[DatePickerBrowsable(false)]
		[Category("General View Settings")]
		public int MultiViewRows
		{
			get
			{
				object obj = this.Properties["MultiViewRows"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				this.Properties["MultiViewRows"] = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00034CC4 File Offset: 0x00032EC4
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00034CED File Offset: 0x00032EED
		[Category("General View Settings")]
		[Description("Gets or sets the number of month columns in a multi view calendar.")]
		[DatePickerBrowsable(false)]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(1)]
		public int MultiViewColumns
		{
			get
			{
				object obj = this.Properties["MultiViewColumns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				this.Properties["MultiViewColumns"] = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00034D08 File Offset: 0x00032F08
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00034D48 File Offset: 0x00032F48
		[DatePickerBrowsable(false)]
		[Category("Dates Management")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(DateTime), "12/30/2099")]
		[Description("Gets or sets the maximal date valid for selection by Telerik RadCalendar. Must be interpreted as the Higher bound of the valid dates range available for selection. Telerik RadCalendar will not allow navigation or selection past this date.")]
		public DateTime RangeMaxDate
		{
			get
			{
				DateTime result = new DateTime(2099, 12, 30);
				object obj = this.Properties["MaxD"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				this.Properties["MaxD"] = dateTime;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00034D74 File Offset: 0x00032F74
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00034DB0 File Offset: 0x00032FB0
		[DatePickerBrowsable(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the minimal date valid for selection by Telerik RadCalendar. Must be interpreted as the Lower bound of the valid dates range available for selection. Telerik RadCalendar will not allow navigation or selection prior to this date.")]
		[Category("Dates Management")]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		public DateTime RangeMinDate
		{
			get
			{
				DateTime result = new DateTime(1980, 1, 1);
				object obj = this.Properties["MinD"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				this.Properties["MinD"] = dateTime;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00034DDC File Offset: 0x00032FDC
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00034E05 File Offset: 0x00033005
		[Category("Dates Management")]
		[Description("Specifies the day to display as the first day of the week.")]
		[NotifyParentProperty(true)]
		[DefaultValue(FirstDayOfWeek.Default)]
		public FirstDayOfWeek FirstDayOfWeek
		{
			get
			{
				object obj = this.Properties["FirstDayOfWeek"];
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
				this.Properties["FirstDayOfWeek"] = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00034E30 File Offset: 0x00033030
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00034E5C File Offset: 0x0003305C
		[SimplePersistenceSetting]
		[DatePickerBrowsable(false)]
		[Category("Dates Management")]
		public DateTime SelectedDate
		{
			get
			{
				if (this.SelectedDates.Count == 0)
				{
					return DateTime.MinValue;
				}
				return this.SelectedDates[0].Date;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				if (dateTime == DateTime.MinValue)
				{
					this.SelectedDates.Clear();
					return;
				}
				this.SelectedDates.SelectRange(dateTime, dateTime);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00034E98 File Offset: 0x00033098
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00034ED4 File Offset: 0x000330D4
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the start date when calendar range selection is enabled.")]
		[DatePickerBrowsable(false)]
		[Category("Dates Management")]
		public DateTime RangeSelectionStartDate
		{
			get
			{
				DateTime result = new DateTime(1980, 1, 1);
				object obj = this.Properties["RangeSelectionStartDate"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				this.Properties["RangeSelectionStartDate"] = dateTime;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00034F00 File Offset: 0x00033100
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00034F40 File Offset: 0x00033140
		[Description("Gets or sets the end date when calendar range selection is enabled.")]
		[DatePickerBrowsable(false)]
		[DefaultValue(typeof(DateTime), "12/30/2099")]
		[NotifyParentProperty(true)]
		[Category("Dates Management")]
		public DateTime RangeSelectionEndDate
		{
			get
			{
				DateTime result = new DateTime(2099, 12, 30);
				object obj = this.Properties["RangeSelectionEndDate"];
				if (!(obj is DateTime))
				{
					return result;
				}
				return (DateTime)obj;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				this.Properties["RangeSelectionEndDate"] = dateTime;
				if (this.RangeSelectionStartDate != new DateTime(1980, 1, 1) && dateTime != new DateTime(2099, 12, 30))
				{
					this.SelectedDates.SelectRange(this.RangeSelectionStartDate, dateTime);
				}
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00034FAC File Offset: 0x000331AC
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00035034 File Offset: 0x00033234
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[Category("Dates Management")]
		[NotifyParentProperty(true)]
		[Description("The date used by RadCalendar to determine the viewable area displayed")]
		[DatePickerBrowsable(false)]
		[SimplePersistenceSetting]
		public DateTime FocusedDate
		{
			get
			{
				DateTime dateTime = this.IsDesignMode ? new DateTime(1980, 1, 1) : DateTime.Today;
				object obj = this.Properties["FocD"];
				if (obj is DateTime)
				{
					dateTime = (DateTime)obj;
				}
				if (this.MultiViewColumns == 1 && this.MultiViewRows == 1)
				{
					if (dateTime > this.RangeMaxDate)
					{
						dateTime = this.RangeMaxDate;
					}
					else if (dateTime < this.RangeMinDate)
					{
						dateTime = this.RangeMinDate;
					}
				}
				return dateTime;
			}
			set
			{
				DateTime dateTime = RadCalendar.TruncateTimeComponent(value);
				this.Properties["FocD"] = dateTime;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00035060 File Offset: 0x00033260
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0003508E File Offset: 0x0003328E
		[Category("Dates Management")]
		[DefaultValue(0)]
		[Description("The row index where the focused date will be positioned inside a multi view area.")]
		[Obsolete("This property is obsolete and will be removed.")]
		[NotifyParentProperty(true)]
		public int FocusedDateRow
		{
			get
			{
				object obj = this.Properties["FocusedDateRow"];
				if (!(obj is int))
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.Properties["FocusedDateRow"] = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000350A8 File Offset: 0x000332A8
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x000350D6 File Offset: 0x000332D6
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("The column index where the focused date will be positioned inside a multi view area.")]
		[Obsolete("This property is obsolete and will be removed.")]
		[Category("Dates Management")]
		public int FocusedDateColumn
		{
			get
			{
				object obj = this.Properties["FocusedDateColumn"];
				if (!(obj is int))
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.Properties["FocusedDateColumn"] = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000350F0 File Offset: 0x000332F0
		[DatePickerBrowsable(false)]
		[NotifyParentProperty(true)]
		[Description("RadDate objects collection that represent the selected dates.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Dates Management")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DateTimeCollection SelectedDates
		{
			get
			{
				object obj = this.Properties["SelectedDates"];
				if (obj == null)
				{
					obj = (this.Properties["SelectedDates"] = new DateTimeCollection());
				}
				return (DateTimeCollection)obj;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00035130 File Offset: 0x00033330
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00035159 File Offset: 0x00033359
		[Description("Gets or sets the number of views that will be scrolled when the user clicks on a fast navigation link.")]
		[Category("Navigation Management")]
		[DefaultValue(3)]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public int FastNavigationStep
		{
			get
			{
				object obj = this.Properties["FastNavigationStep"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 3;
			}
			set
			{
				this.Properties["FastNavigationStep"] = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00035171 File Offset: 0x00033371
		internal CalendarStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new CalendarStrings(new LocalizationProvider("RadCalendar.Main", this, base.DesignMode ? "" : this.LocalizationPath));
				}
				return this._localization;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x000351AC File Offset: 0x000333AC
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x000351CC File Offset: 0x000333CC
		[Description("Gets or sets a value indicating where RadCalendar will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00035220 File Offset: 0x00033420
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0003528A File Offset: 0x0003348A
		[Category("Appearance")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				CultureInfo cultureInfo = ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
				if (cultureInfo.DateTimeFormat.Calendar.GetType() != typeof(GregorianCalendar))
				{
					cultureInfo = new CultureInfo(cultureInfo.Name);
					cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
				}
				return cultureInfo;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x000352B8 File Offset: 0x000334B8
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x000352E6 File Offset: 0x000334E6
		[Description("Specifies the display format for the days of the week on RadCalendar.")]
		[DefaultValue(DayNameFormat.FirstLetter)]
		[NotifyParentProperty(true)]
		[Category("Localization Settings")]
		public DayNameFormat DayNameFormat
		{
			get
			{
				object obj = this.Properties["DayNameFormat"];
				if (!(obj is DayNameFormat))
				{
					return DayNameFormat.FirstLetter;
				}
				return (DayNameFormat)obj;
			}
			set
			{
				this.Properties["DayNameFormat"] = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000352FE File Offset: 0x000334FE
		[Category("Localization Settings")]
		[Description("Gets the default DateTimeFormatInfo instance as speified by the default culture.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				return this.CultureInfo.DateTimeFormat;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003530C File Offset: 0x0003350C
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x00035388 File Offset: 0x00033588
		[DatePickerBrowsable(false)]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Localization Settings")]
		[Description("Gets or sets the information about a specific culture that will be applied to the calendar representation.")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(CultureInfoConverter))]
		public CultureInfo CultureInfo
		{
			get
			{
				CultureInfo cultureInfo = (this.Properties["CultureInfoID"] == null) ? CultureInfo.CurrentCulture : ((CultureInfo)this.Properties["CultureInfoID"]);
				if (cultureInfo.DateTimeFormat.Calendar.GetType() != typeof(GregorianCalendar))
				{
					cultureInfo = new CultureInfo(cultureInfo.Name);
					cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
				}
				return cultureInfo;
			}
			set
			{
				this.Properties["CultureNameID"] = value.Name;
				this.Properties["CultureID"] = value.LCID;
				this.Properties["CultureInfoID"] = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x000353D7 File Offset: 0x000335D7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Localization Settings")]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[Description("Gets the default System.Globalization.Calendar instance as speified by the default culture.")]
		public System.Globalization.Calendar Calendar
		{
			get
			{
				if (this.CultureInfo.DateTimeFormat.Calendar.GetType() != typeof(GregorianCalendar))
				{
					return new GregorianCalendar();
				}
				return this.CultureInfo.DateTimeFormat.Calendar;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00035418 File Offset: 0x00033618
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00035446 File Offset: 0x00033646
		[Description("Gets the default type used by Telerik RadCalendar to handle its layout, and how will react to user interaction.")]
		[NotifyParentProperty(true)]
		[DefaultValue(PresentationType.Interactive)]
		[DatePickerBrowsable(false)]
		[ClientControlProperty]
		[Category("Appearance")]
		public PresentationType PresentationType
		{
			get
			{
				object obj = this.Properties["PresentationType"];
				if (!(obj is PresentationType))
				{
					return PresentationType.Interactive;
				}
				return (PresentationType)obj;
			}
			set
			{
				this.Properties["PresentationType"] = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00035460 File Offset: 0x00033660
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00035489 File Offset: 0x00033689
		[DefaultValue(Telerik.Web.UI.Calendar.Orientation.RenderInRows)]
		[Category("Behavior")]
		[Description("Gets or sets the orientation (rendering direction) of the calendar component.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public Telerik.Web.UI.Calendar.Orientation Orientation
		{
			get
			{
				object obj = this.Properties["Orientation"];
				if (obj != null)
				{
					return (Telerik.Web.UI.Calendar.Orientation)obj;
				}
				return Telerik.Web.UI.Calendar.Orientation.RenderInRows;
			}
			set
			{
				this.Properties["Orientation"] = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000354A4 File Offset: 0x000336A4
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x000354CD File Offset: 0x000336CD
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether a postback to the server automatically occurs when the user interacts with the control.")]
		public virtual bool AutoPostBack
		{
			get
			{
				object obj = this.Properties["AutoPostBack"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.Properties["AutoPostBack"] = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000354E5 File Offset: 0x000336E5
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x00035510 File Offset: 0x00033710
		[Description("Gets or sets a value indicating whether a tooltips for day cells should be rendered.")]
		[ClientControlProperty]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool ShowDayCellToolTips
		{
			get
			{
				return this.Properties["_sdctt"] == null || (bool)this.Properties["_sdctt"];
			}
			set
			{
				this.Properties["_sdctt"] = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00035528 File Offset: 0x00033728
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00035557 File Offset: 0x00033757
		[Category("Accessibility")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value for DaysView summary.")]
		[DefaultValue("Table containing all dates for the currently selected month.")]
		public virtual string DaysViewSummary
		{
			get
			{
				if (this.Properties["DaysViewCaption"] == null)
				{
					return "Table containing all dates for the currently selected month.";
				}
				return (string)this.Properties["DaysViewCaption"];
			}
			set
			{
				this.Properties["DaysViewCaption"] = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0003556A File Offset: 0x0003376A
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00035599 File Offset: 0x00033799
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Title and navigation which can change and show the current year and month.")]
		[Description("Gets or sets a value for navigation controls summary.")]
		public virtual string NavigationSummary
		{
			get
			{
				if (this.Properties["_ns"] == null)
				{
					return "Title and navigation which can change and show the current year and month.";
				}
				return (string)this.Properties["_ns"];
			}
			set
			{
				this.Properties["_ns"] = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000355AC File Offset: 0x000337AC
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x000355DB File Offset: 0x000337DB
		[Category("Accessibility")]
		[Localizable(true)]
		[Description("Gets or sets a value for navigation controls caption.")]
		[DefaultValue("Title and navigation")]
		[NotifyParentProperty(true)]
		public virtual string NavigationCaption
		{
			get
			{
				if (this.Properties["NavigationCaption"] == null)
				{
					return "Title and navigation";
				}
				return (string)this.Properties["NavigationCaption"];
			}
			set
			{
				this.Properties["NavigationCaption"] = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x000355EE File Offset: 0x000337EE
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0003561D File Offset: 0x0003381D
		[Category("Accessibility")]
		[DefaultValue("Calendar control which enables the selection of dates.")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value for RadCalendar summary.")]
		[Localizable(true)]
		public virtual string CalendarSummary
		{
			get
			{
				if (this.Properties["_cs"] == null)
				{
					return "Calendar control which enables the selection of dates.";
				}
				return (string)this.Properties["_cs"];
			}
			set
			{
				this.Properties["_cs"] = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00035630 File Offset: 0x00033830
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0003565F File Offset: 0x0003385F
		[Category("Accessibility")]
		[DefaultValue("Calendar")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets a value for RadCalendar caption.")]
		public virtual string CalendarCaption
		{
			get
			{
				if (this.Properties["CalendarCaption"] == null)
				{
					return "Calendar";
				}
				return (string)this.Properties["_cs"];
			}
			set
			{
				this.Properties["CalendarCaption"] = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00035672 File Offset: 0x00033872
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0003567A File Offset: 0x0003387A
		[DefaultValue(null)]
		[Description("The header template of RadCalendar.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(TemplateContainer))]
		[Category("Templates Management")]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00035683 File Offset: 0x00033883
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0003568B File Offset: 0x0003388B
		[TemplateContainer(typeof(TemplateContainer))]
		[Description("The footer template of RadCalendar.")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Category("Templates Management")]
		public ITemplate FooterTemplate
		{
			get
			{
				return this._footerTemplate;
			}
			set
			{
				this._footerTemplate = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00035694 File Offset: 0x00033894
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x000356BD File Offset: 0x000338BD
		[DefaultValue(true)]
		[Category("Navigation Management")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets whether the navigation controls will be shown.")]
		[ClientControlProperty]
		[ClientPropertyName("calendarEnableNavigation")]
		public bool EnableNavigation
		{
			get
			{
				object obj = this.Properties["EnableNavigation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["EnableNavigation"] = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x000356D8 File Offset: 0x000338D8
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00035701 File Offset: 0x00033901
		[Description("Gets or sets whether the navigation buttons will be shown.")]
		[Category("Navigation Management")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ShowNavigationButtons
		{
			get
			{
				object obj = this.Properties["ShowNavigationButtons"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["ShowNavigationButtons"] = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003571C File Offset: 0x0003391C
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00035745 File Offset: 0x00033945
		[Description("Gets or sets whether the fast navigation buttons will be shown.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		public bool ShowFastNavigationButtons
		{
			get
			{
				object obj = this.Properties["ShowFastNavigationButtons"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["ShowFastNavigationButtons"] = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00035760 File Offset: 0x00033960
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x00035789 File Offset: 0x00033989
		[Category("Navigation Management")]
		[ClientPropertyName("calendarEnableMonthYearFastNavigation")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets whether the month/year fast navigation controls will be enabled.")]
		[ClientControlProperty]
		public bool EnableMonthYearFastNavigation
		{
			get
			{
				object obj = this.Properties["EnableMonthYearFastNavigation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["EnableMonthYearFastNavigation"] = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x000357A1 File Offset: 0x000339A1
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x000357C7 File Offset: 0x000339C7
		[DefaultValue("&lt;")]
		[Description("Gets or sets the text displayed for the previous month navigation control.")]
		[Localizable(true)]
		[Category("Navigation Management")]
		[NotifyParentProperty(true)]
		public string NavigationPrevText
		{
			get
			{
				return ((string)this.Properties["NavigationPrevText"]) ?? this.Localization.NavigationPrevText;
			}
			set
			{
				this.Properties["NavigationPrevText"] = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000357DA File Offset: 0x000339DA
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00035800 File Offset: 0x00033A00
		[Category("Navigation Management")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the text displayed for the next month navigation control.")]
		[DefaultValue("&gt;")]
		public string NavigationNextText
		{
			get
			{
				return ((string)this.Properties["NavigationNextText"]) ?? this.Localization.NavigationNextText;
			}
			set
			{
				this.Properties["NavigationNextText"] = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00035813 File Offset: 0x00033A13
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00035839 File Offset: 0x00033A39
		[DefaultValue("&lt;&lt;")]
		[Description("(Classic Mode Only) Gets or sets the text displayed for the fast previous navigation control.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		public string FastNavigationPrevText
		{
			get
			{
				return ((string)this.Properties["FastNavigationPrevText"]) ?? this.Localization.FastNavigationPrevText;
			}
			set
			{
				this.Properties["FastNavigationPrevText"] = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0003584C File Offset: 0x00033A4C
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00035872 File Offset: 0x00033A72
		[DefaultValue("&gt;&gt;")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the fast next navigation control.")]
		public string FastNavigationNextText
		{
			get
			{
				return (this.Properties["FastNavigationNextText"] as string) ?? this.Localization.FastNavigationNextText;
			}
			set
			{
				this.Properties["FastNavigationNextText"] = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00035888 File Offset: 0x00033A88
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x000358B5 File Offset: 0x00033AB5
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Localizable(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets name of the image that is displayed for the previous month navigation control.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string NavigationPrevImage
		{
			get
			{
				string text = (string)this.Properties["NavigationPrevImage"];
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				this.Properties["NavigationPrevImage"] = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000358C8 File Offset: 0x00033AC8
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x000358F5 File Offset: 0x00033AF5
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the image that is displayed for the next month navigation control.")]
		[Category("Navigation Management")]
		[DefaultValue("")]
		[UrlProperty]
		public string NavigationNextImage
		{
			get
			{
				string text = (string)this.Properties["NavigationNextImage"];
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				this.Properties["NavigationNextImage"] = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00035908 File Offset: 0x00033B08
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00035935 File Offset: 0x00033B35
		[DefaultValue("")]
		[UrlProperty]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets the name of the image that is displayed for the fast previous navigation control.")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string FastNavigationPrevImage
		{
			get
			{
				string text = (string)this.Properties["FastNavigationPrevImage"];
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				this.Properties["FastNavigationPrevImage"] = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00035948 File Offset: 0x00033B48
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00035975 File Offset: 0x00033B75
		[Localizable(true)]
		[Category("Navigation Management")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the image that is displayed for the fast next navigation control.")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string FastNavigationNextImage
		{
			get
			{
				string text = (string)this.Properties["FastNavigationNextImage"];
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				this.Properties["FastNavigationNextImage"] = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00035988 File Offset: 0x00033B88
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x000359AE File Offset: 0x00033BAE
		[NotifyParentProperty(true)]
		[DatePickerBrowsable(false)]
		[DefaultValue("<")]
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the previous month navigation control.")]
		public string NavigationPrevToolTip
		{
			get
			{
				return ((string)this.Properties["NavigationPrevToolTip"]) ?? this.Localization.NavigationPrevToolTip;
			}
			set
			{
				this.Properties["NavigationPrevToolTip"] = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000359C1 File Offset: 0x00033BC1
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x000359E7 File Offset: 0x00033BE7
		[DatePickerBrowsable(false)]
		[Description("Gets or sets the text displayed for the next month navigation control.")]
		[DefaultValue(">")]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		public string NavigationNextToolTip
		{
			get
			{
				return ((string)this.Properties["NavigationNextToolTip"]) ?? this.Localization.NavigationNextToolTip;
			}
			set
			{
				this.Properties["NavigationNextToolTip"] = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000359FA File Offset: 0x00033BFA
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x00035A20 File Offset: 0x00033C20
		[Category("Navigation Management")]
		[Description("Gets or sets the text displayed for the fast previous navigation control.")]
		[DatePickerBrowsable(false)]
		[DefaultValue("<<")]
		[NotifyParentProperty(true)]
		public string FastNavigationPrevToolTip
		{
			get
			{
				return ((string)this.Properties["FastNavigationPrevToolTip"]) ?? this.Localization.FastNavigationPrevToolTip;
			}
			set
			{
				this.Properties["FastNavigationPrevToolTip"] = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00035A33 File Offset: 0x00033C33
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x00035A59 File Offset: 0x00033C59
		[DefaultValue(">>")]
		[Description("Gets or sets the text displayed for the fast next navigation control.")]
		[DatePickerBrowsable(false)]
		[Category("Navigation Management")]
		[NotifyParentProperty(true)]
		public string FastNavigationNextToolTip
		{
			get
			{
				return ((string)this.Properties["FastNavigationNextToolTip"]) ?? this.Localization.FastNavigationNextToolTip;
			}
			set
			{
				this.Properties["FastNavigationNextToolTip"] = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00035A6C File Offset: 0x00033C6C
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00035A74 File Offset: 0x00033C74
		[Browsable(false)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00035A80 File Offset: 0x00033C80
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00035AA9 File Offset: 0x00033CA9
		[NotifyParentProperty(true)]
		[Description("Gets or sets the cell spacing that is applied to the title table.")]
		[DefaultValue(0)]
		[Category("Navigation Management")]
		public int NavigationCellSpacing
		{
			get
			{
				object obj = this.Properties["NavigationCellSpacing"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.Properties["NavigationCellSpacing"] = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00035AC4 File Offset: 0x00033CC4
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00035AED File Offset: 0x00033CED
		[DefaultValue(-1)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		[Description("Gets or sets the cell padding that is applied to the title table.")]
		public int NavigationCellPadding
		{
			get
			{
				object obj = this.Properties["NavigationCellPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this.Properties["NavigationCellPadding"] = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00035B08 File Offset: 0x00033D08
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00035B31 File Offset: 0x00033D31
		[Category("Title Settings")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[Description("Gets or sets the horizontal alignment of the calendar title.")]
		[NotifyParentProperty(true)]
		public HorizontalAlign TitleAlign
		{
			get
			{
				object obj = this.Properties["TitleAlign"];
				if (obj != null)
				{
					return (HorizontalAlign)obj;
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				this.Properties["TitleAlign"] = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00035B4C File Offset: 0x00033D4C
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00035B79 File Offset: 0x00033D79
		[Localizable(true)]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Category("Title Settings")]
		[DefaultValue("MMMM yyyy")]
		[Description("Gets or sets the format string that is applied to the calendar title.")]
		public string TitleFormat
		{
			get
			{
				string text = (string)this.Properties["TitleFormat"];
				if (text != null)
				{
					return text;
				}
				return "MMMM yyyy";
			}
			set
			{
				this.Properties["TitleFormat"] = value;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00035B8C File Offset: 0x00033D8C
		internal string ResolvedTitleFormat()
		{
			if (this.Properties["TitleFormat"] == null)
			{
				return this.CultureInfo.DateTimeFormat.YearMonthPattern;
			}
			return this.TitleFormat;
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00035BB8 File Offset: 0x00033DB8
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x00035BE5 File Offset: 0x00033DE5
		[Category("Title Settings")]
		[ClientControlProperty]
		[Localizable(true)]
		[Description("Gets or sets the format string that is applied to the days cells tooltip.")]
		[DefaultValue("dddd, MMMM dd, yyyy")]
		[NotifyParentProperty(true)]
		public string DayCellToolTipFormat
		{
			get
			{
				string text = (string)this.Properties["DayCellToolTipFormat"];
				if (text != null)
				{
					return text;
				}
				return "dddd, MMMM dd, yyyy";
			}
			set
			{
				this.Properties["DayCellToolTipFormat"] = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00035BF8 File Offset: 0x00033DF8
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x00035C25 File Offset: 0x00033E25
		[ClientControlProperty]
		[Category("Title Settings")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(" - ")]
		[Description("Gets or sets the separator string that will be put between start and end months in a multi view title.")]
		public string DateRangeSeparator
		{
			get
			{
				string text = (string)this.Properties["DateRangeSeparator"];
				if (text != null)
				{
					return text;
				}
				return " - ";
			}
			set
			{
				this.Properties["DateRangeSeparator"] = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00035C38 File Offset: 0x00033E38
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x00035C63 File Offset: 0x00033E63
		[DatePickerBrowsable(false)]
		[Description("Gets or sets a value indicating whether the navigation control should be visible when disabled.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Navigation Management")]
		public bool HideNavigationControls
		{
			get
			{
				return this.Properties["hideNavigationControls"] != null && (bool)this.Properties["hideNavigationControls"];
			}
			set
			{
				this.Properties["hideNavigationControls"] = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00035C7C File Offset: 0x00033E7C
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x00035CA9 File Offset: 0x00033EA9
		[Category("Behavior")]
		[Editor("System.Web.UI.Design.UrlEditor", "System.Drawing.Design.UITypeEditor")]
		[Description("Gets or sets the name of the file containing the CSS definition used by RadCalendar. Use \"~/\" (tilde) as a substitution of the web-application root directory.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string CssFile
		{
			get
			{
				string text = (string)this.Properties["CssFile"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["CssFile"] = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00035CBC File Offset: 0x00033EBC
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00035CE5 File Offset: 0x00033EE5
		[Description("Gets or sets the cell padding of the table where are rendered the calendar days.")]
		[Category("Appearance")]
		[DefaultValue(-1)]
		[NotifyParentProperty(true)]
		public int DefaultCellPadding
		{
			get
			{
				object obj = this.Properties["DefaultCellPadding"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this.Properties["DefaultCellPadding"] = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00035D00 File Offset: 0x00033F00
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00035D29 File Offset: 0x00033F29
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Gets or sets the cell spacing of the table where are rendered the calendar days.")]
		public int DefaultCellSpacing
		{
			get
			{
				object obj = this.Properties["DefaultCellSpacing"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.Properties["DefaultCellSpacing"] = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00035D44 File Offset: 0x00033F44
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("A collection of special days in the calendar to which may be applied specific formatting.")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public CalendarDayCollection SpecialDays
		{
			get
			{
				object obj = this.Properties["SpecialDays"];
				if (obj == null)
				{
					obj = (this.Properties["SpecialDays"] = new CalendarDayCollection(this));
				}
				return (CalendarDayCollection)obj;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00035D85 File Offset: 0x00033F85
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to days.")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
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

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00035DB3 File Offset: 0x00033FB3
		[Description("The style applied to weekend days.")]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00035DE1 File Offset: 0x00033FE1
		[RefreshProperties(RefreshProperties.All)]
		[Description("The style applied to the Calendar table container.")]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle CalendarTableStyle
		{
			get
			{
				if (this.calendarTableStyle == null)
				{
					this.calendarTableStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.calendarTableStyle).TrackViewState();
					}
				}
				return this.calendarTableStyle;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00035E0F File Offset: 0x0003400F
		[RefreshProperties(RefreshProperties.All)]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("The style applied to days from adjacent months.")]
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

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00035E3D File Offset: 0x0003403D
		[Description("The style applied to days that are out of the valid range for selection.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public TableItemStyle OutOfRangeDayStyle
		{
			get
			{
				if (this.outOfRangeDayStyle == null)
				{
					this.outOfRangeDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.outOfRangeDayStyle).TrackViewState();
					}
				}
				return this.outOfRangeDayStyle;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00035E6B File Offset: 0x0003406B
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("The style of currently disabled days.")]
		public TableItemStyle DisabledDayStyle
		{
			get
			{
				if (this.disabledDayStyle == null)
				{
					this.disabledDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.disabledDayStyle).TrackViewState();
					}
				}
				return this.disabledDayStyle;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00035E99 File Offset: 0x00034099
		[Category("Appearance")]
		[Description("The style of currently selected days.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00035EC7 File Offset: 0x000340C7
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied when hovering over the Calendar days.")]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle DayOverStyle
		{
			get
			{
				if (this.dayOverStyle == null)
				{
					this.dayOverStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dayOverStyle).TrackViewState();
					}
				}
				return this.dayOverStyle;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00035EF5 File Offset: 0x000340F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to the title")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Appearance")]
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

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00035F23 File Offset: 0x00034123
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to the row and column headers.")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00035F51 File Offset: 0x00034151
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("The style applied to the month/year fast navigation.")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle FastNavigationStyle
		{
			get
			{
				if (this.fastNavigationStyle == null)
				{
					this.fastNavigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.fastNavigationStyle).TrackViewState();
					}
				}
				return this.fastNavigationStyle;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00035F7F File Offset: 0x0003417F
		[NotifyParentProperty(true)]
		[Description("The style applied to the row and column headers.")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle ViewSelectorStyle
		{
			get
			{
				if (this.viewSelectorStyle == null)
				{
					this.viewSelectorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.viewSelectorStyle).TrackViewState();
					}
				}
				return this.viewSelectorStyle;
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00035FAD File Offset: 0x000341AD
		internal void SetCalendarView(CalendarView inputView)
		{
			this._CalendarView = inputView;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00035FB6 File Offset: 0x000341B6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CalendarView CalendarView
		{
			get
			{
				if (this._CalendarView == null)
				{
					this._CalendarView = new MonthView(this);
					this._CalendarView.Initialize();
				}
				return this._CalendarView;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00035FE0 File Offset: 0x000341E0
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00036009 File Offset: 0x00034209
		[Description("Determines whether the column headers will appear on the calendar.")]
		[NotifyParentProperty(true)]
		[Category("Header Settings")]
		[DefaultValue(true)]
		public bool ShowColumnHeaders
		{
			get
			{
				object obj = this.Properties["ECS"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["ECS"] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00036024 File Offset: 0x00034224
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0003604D File Offset: 0x0003424D
		[Category("Header Settings")]
		[Description("Determines whether the row headers will appear on the calendar.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowRowHeaders
		{
			get
			{
				object obj = this.Properties["ERS"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["ERS"] = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00036068 File Offset: 0x00034268
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00036091 File Offset: 0x00034291
		[Description("Determines whether a selector for the entire month will appear on the calendar.")]
		[NotifyParentProperty(true)]
		[Category("Header Settings")]
		[DefaultValue(false)]
		public bool EnableViewSelector
		{
			get
			{
				object obj = this.Properties["EVS"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.Properties["EVS"] = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x000360AC File Offset: 0x000342AC
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x000360D5 File Offset: 0x000342D5
		[NotifyParentProperty(true)]
		[Description("Gets or sets whether the month matrix, when rendered will show days from other (previous or next) months or will render only blank cells.")]
		[ClientControlProperty]
		[Category("MonthView Specific Settings")]
		[DefaultValue(true)]
		public bool ShowOtherMonthsDays
		{
			get
			{
				object obj = this.Properties["ShowOtherMonthsDays"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["ShowOtherMonthsDays"] = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000360F0 File Offset: 0x000342F0
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x00036119 File Offset: 0x00034319
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Category("MonthView Specific Settings")]
		[DefaultValue(true)]
		[Description("When the ShowColumnHeaders and/or ShowRowHeaders properties are set to true, the UseColumnHeadersAsSelectors property specifies whether to use the days of the week, which overrides the used text/image header if any.")]
		public virtual bool UseColumnHeadersAsSelectors
		{
			get
			{
				object obj = this.Properties["UseColumnHeadersAsSelectors"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["UseColumnHeadersAsSelectors"] = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00036134 File Offset: 0x00034334
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0003615D File Offset: 0x0003435D
		[Category("MonthView Specific Settings")]
		[ClientControlProperty]
		[DefaultValue(true)]
		[Description("Specifies whether to use the number of the week which overrides the used text/image header if any.")]
		[NotifyParentProperty(true)]
		public virtual bool UseRowHeadersAsSelectors
		{
			get
			{
				object obj = this.Properties["UseRowHeadersAsSelectors"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["UseRowHeadersAsSelectors"] = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00036178 File Offset: 0x00034378
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x000361A5 File Offset: 0x000343A5
		[Category("Header Settings")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		[Description("Provides custom text for the row header cells.")]
		[Bindable(false)]
		public string RowHeaderText
		{
			get
			{
				object obj = this.Properties["RowHeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["RowHeaderText"] = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000361B8 File Offset: 0x000343B8
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x000361E5 File Offset: 0x000343E5
		[Localizable(true)]
		[Description("The image displayed for the <strong>CalendarView</strong> row header element.")]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Header Settings")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string RowHeaderImage
		{
			get
			{
				object obj = this.Properties["RowHeaderImage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["RowHeaderImage"] = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000361F8 File Offset: 0x000343F8
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00036225 File Offset: 0x00034425
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Header Settings")]
		[Description("Provides custom text for the column header cells.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ColumnHeaderText
		{
			get
			{
				object obj = this.Properties["ColumnHeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["ColumnHeaderText"] = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00036238 File Offset: 0x00034438
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00036265 File Offset: 0x00034465
		[Bindable(false)]
		[Localizable(true)]
		[Category("Header Settings")]
		[DefaultValue("")]
		[Description("The image displayed for the column header cells.")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string ColumnHeaderImage
		{
			get
			{
				object obj = this.Properties["ColumnHeaderImage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["ColumnHeaderImage"] = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00036278 File Offset: 0x00034478
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x000362A5 File Offset: 0x000344A5
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Bindable(false)]
		[Category("Header Settings")]
		[DefaultValue("x")]
		[Description("The text displayed in the view selector cell.")]
		public string ViewSelectorText
		{
			get
			{
				object obj = this.Properties["ViewSelectorText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "x";
			}
			set
			{
				this.Properties["ViewSelectorText"] = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000362B8 File Offset: 0x000344B8
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x000362E5 File Offset: 0x000344E5
		[Localizable(true)]
		[Bindable(false)]
		[Category("Header Settings")]
		[DefaultValue("")]
		[Description("The image displayed in the view selector cell.")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string ViewSelectorImage
		{
			get
			{
				object obj = this.Properties["ViewSelectorImage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.Properties["ViewSelectorImage"] = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x000362F8 File Offset: 0x000344F8
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00036321 File Offset: 0x00034521
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Allows the selection of multiple dates. If not set, only a single date is selected, and if any dates are all ready selected, they are cleared.")]
		[DatePickerBrowsable(false)]
		public virtual bool EnableMultiSelect
		{
			get
			{
				object obj = this.Properties["EnableMultiSelect"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.Properties["EnableMultiSelect"] = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0003633C File Offset: 0x0003453C
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00036365 File Offset: 0x00034565
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("General View Settings")]
		[DefaultValue(false)]
		[Description("Allows the selection of multiple dates. If not set, only a single date is selected, and if any dates are all ready selected, they are cleared.")]
		[DatePickerBrowsable(false)]
		public virtual bool EnableNavigationAnimation
		{
			get
			{
				object obj = this.Properties["EnableNavigationAnimation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.Properties["EnableNavigationAnimation"] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0003637D File Offset: 0x0003457D
		protected internal bool EmptySkin
		{
			get
			{
				return string.IsNullOrEmpty(base.RuntimeSkin);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0003638A File Offset: 0x0003458A
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00036392 File Offset: 0x00034592
		[Description("Gets or sets the name of the skin used.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003639C File Offset: 0x0003459C
		internal string GetProperWebResourceUrl(string webResourceName)
		{
			string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetWebResourceType(), webResourceName);
			return webResourceUrl.Replace("&t", "&amp;t");
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000363D4 File Offset: 0x000345D4
		internal Type GetWebResourceType()
		{
			Type type = base.GetType();
			while (type != typeof(RadCalendar))
			{
				type = type.BaseType;
			}
			return type;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00036404 File Offset: 0x00034604
		protected internal string GetImage(string fileName)
		{
			if (this.IsWebResourceUrl(fileName))
			{
				return fileName;
			}
			if (!VirtualPathUtility.IsAbsolute(fileName) && VirtualPathUtility.IsAppRelative(fileName))
			{
				if (this.IsDesignMode)
				{
					return base.ResolveClientUrl(VirtualPathUtility.ToAppRelative(fileName));
				}
				return VirtualPathUtility.ToAbsolute(fileName);
			}
			else
			{
				if (!string.IsNullOrEmpty(this.ImagesPath.Trim()) && fileName.IndexOf("/") == -1 && fileName.IndexOf("\\") == -1)
				{
					return base.ResolveUrl(Path.Combine(this.ImagesPath.Trim(), fileName));
				}
				if (!string.IsNullOrEmpty(this.ImagesPath.Trim()))
				{
					return base.ResolveUrl(fileName);
				}
				return string.Empty;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000364AD File Offset: 0x000346AD
		private bool IsWebResourceUrl(string path)
		{
			return path != null && path.IndexOf("WebResource.axd") != -1;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000364C8 File Offset: 0x000346C8
		protected override void CreateChildControls()
		{
			this.InstantiateDayTemplates(true);
			if (this.HeaderTemplate != null)
			{
				TemplateContainer templateContainer = new TemplateContainer();
				templateContainer.ID = "HeaderTemplateContainer";
				this.HeaderTemplate.InstantiateIn(templateContainer);
				this.Controls.Add(templateContainer);
			}
			if (this.FooterTemplate != null)
			{
				TemplateContainer templateContainer2 = new TemplateContainer();
				templateContainer2.ID = "FooterTemplateContainer";
				this.FooterTemplate.InstantiateIn(templateContainer2);
				this.Controls.Add(templateContainer2);
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00036540 File Offset: 0x00034740
		private DayTemplate FindTemplateByID(string searchedID)
		{
			for (int i = 0; i < this.CalendarDayTemplates.Count; i++)
			{
				if (this.CalendarDayTemplates[i].ID == searchedID)
				{
					return this.CalendarDayTemplates[i];
				}
			}
			return null;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00036594 File Offset: 0x00034794
		private void InstantiateDayTemplates(bool enforceRefresh)
		{
			if (enforceRefresh)
			{
				this.Controls.Clear();
			}
			if (!this.EnableWeekends)
			{
				RadCalendarDay radCalendarDay = new RadCalendarDay(this);
				this.SpecialDays.Add(radCalendarDay);
				radCalendarDay.Date = DateTime.Parse("2011/1/1");
				radCalendarDay.Repeatable = RecurringEvents.Week;
				radCalendarDay.IsSelectable = false;
				radCalendarDay.IsDisabled = true;
				radCalendarDay = new RadCalendarDay(this);
				this.SpecialDays.Add(radCalendarDay);
				radCalendarDay.Date = DateTime.Parse("2011/1/2");
				radCalendarDay.Repeatable = RecurringEvents.Week;
				radCalendarDay.IsSelectable = false;
				radCalendarDay.IsDisabled = true;
			}
			for (int i = 0; i < this.SpecialDays.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.SpecialDays[i].TemplateID))
				{
					DayTemplate dayTemplate = this.FindTemplateByID(this.SpecialDays[i].TemplateID);
					if (dayTemplate == null)
					{
						throw new Exception("Template with ID: " + this.SpecialDays[i].TemplateID + " not found.");
					}
					string id = string.Empty;
					if (this.SpecialDays[i].Repeatable != RecurringEvents.Today)
					{
						id = Utility.SetCellID("dt", this.SpecialDays[i].Date);
					}
					else
					{
						id = Utility.SetCellID("dt", DateTime.Today);
					}
					if (this.FindControl(id) == null)
					{
						TemplateContainer templateContainer = new TemplateContainer(this.SpecialDays[i]);
						dayTemplate.Content.InstantiateIn(templateContainer);
						templateContainer.ID = id;
						templateContainer.DataBind();
						this.Controls.Add(templateContainer);
					}
				}
			}
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				control.Visible = false;
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000367A4 File Offset: 0x000349A4
		public void ResetTemplates()
		{
			this.InstantiateDayTemplates(true);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000367B0 File Offset: 0x000349B0
		private Control FindControlRecursive(string controlID, ControlCollection controlsCollection)
		{
			if (controlsCollection == null)
			{
				controlsCollection = this.Controls;
			}
			foreach (object obj in controlsCollection)
			{
				Control control = (Control)obj;
				if (control.ID == controlID)
				{
					return control;
				}
				if (control.Controls.Count > 0)
				{
					Control control2 = this.FindControlRecursive(controlID, control.Controls);
					if (control2 != null)
					{
						return control2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00036844 File Offset: 0x00034A44
		public override Control FindControl(string id)
		{
			this.EnsureChildControls();
			return this.FindControlRecursive(id, this.Controls);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00036859 File Offset: 0x00034A59
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00036864 File Offset: 0x00034A64
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument != null && eventArgument.Length > 0)
			{
				char[] separator = new char[]
				{
					Convert.ToChar(":")
				};
				string[] array = eventArgument.Split(separator);
				if (array.Length > 0)
				{
					DateTime focusedDate = this.FocusedDate;
					string s = string.Empty;
					string s2 = string.Empty;
					string s3 = string.Empty;
					string text = array[0];
					if (array.Length > 1)
					{
						s = array[1];
					}
					if (array.Length > 2)
					{
						s2 = array[2];
					}
					if (array.Length > 3)
					{
						s3 = array[3];
					}
					string a;
					if ((a = text) != null)
					{
						if (a == "n")
						{
							int navigationStep = int.Parse(s);
							this.NavigateWithStep(navigationStep);
							this.OnDefaultViewChanged(this.FocusedDate, focusedDate);
							return;
						}
						if (a == "nd")
						{
							int year = int.Parse(s);
							int month = int.Parse(s2);
							int day = int.Parse(s3);
							DateTime dateTime = this.DateTimeFormat.Calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
							CalendarView calendarView = new MonthView(this, dateTime);
							this.SetCalendarView(calendarView);
							this.FocusedDate = dateTime;
							this.CalendarView.Initialize();
							if (this.FocusedDate != focusedDate)
							{
								this.OnDefaultViewChanged(this.FocusedDate, focusedDate);
							}
							this.OnSelectionChanged();
							return;
						}
						if (a == "d")
						{
							this.OnSelectionChanged();
							return;
						}
					}
					throw new ApplicationException("Invalid navigation argument: [" + eventArgument + "]");
				}
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000369E4 File Offset: 0x00034BE4
		protected virtual void NavigateWithStep(int navigationStep)
		{
			CalendarView newViewFromStep = this.GetNewViewFromStep(navigationStep);
			DateTime focusedDate;
			if (newViewFromStep.IsSingleView)
			{
				focusedDate = ((MonthView)newViewFromStep).MonthStartDate;
			}
			else
			{
				focusedDate = newViewFromStep.ViewStartDate;
			}
			this.SetCalendarView(newViewFromStep);
			this.FocusedDate = focusedDate;
			this.CalendarView.Initialize();
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00036A30 File Offset: 0x00034C30
		private CalendarView GetNewViewFromStep(int navigationStep)
		{
			bool flag = false;
			if (navigationStep < 0)
			{
				navigationStep = -navigationStep;
				flag = true;
			}
			CalendarView result;
			if (flag)
			{
				result = this.CalendarView.GetPreviousView(navigationStep);
			}
			else
			{
				result = this.CalendarView.GetNextView(navigationStep);
			}
			return result;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00036A6C File Offset: 0x00034C6C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.ClientID + "_SD"];
			if (text != null)
			{
				this.SelectedDates.Clear();
				this.ClearSelectionFromSpecialDays();
				Utility.ConvertToServerDateTimeCollection(this.SelectedDates, text);
				this.ApplySelectionToSpecialDays();
			}
			string text2 = postCollection[this.ClientID + "_AD"];
			if (text2 != null)
			{
				DateTimeCollection dateTimeCollection = new DateTimeCollection();
				Utility.ConvertToServerDateTimeCollection(dateTimeCollection, text2);
				this.RangeMinDate = dateTimeCollection[0].Date;
				this.RangeMaxDate = dateTimeCollection[1].Date;
				this.FocusedDate = dateTimeCollection[2].Date;
			}
			string text3 = postCollection[this.ClientID + "_RS"];
			if (text3 != null)
			{
				DateTimeCollection dateTimeCollection2 = new DateTimeCollection();
				Utility.ConvertToServerDateTimeCollection(dateTimeCollection2, text3);
				if (dateTimeCollection2.Count > 0)
				{
					this.RangeSelectionStartDate = dateTimeCollection2[0].Date;
				}
				if (dateTimeCollection2.Count > 1)
				{
					this.RangeSelectionEndDate = dateTimeCollection2[1].Date;
				}
			}
			return false;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00036B90 File Offset: 0x00034D90
		private void ClearSelectionFromSpecialDays()
		{
			foreach (object obj in this.SpecialDays)
			{
				RadCalendarDay radCalendarDay = (RadCalendarDay)obj;
				radCalendarDay.IsSelected = false;
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00036BEC File Offset: 0x00034DEC
		private void ApplySelectionToSpecialDays()
		{
			foreach (object obj in this.SelectedDates)
			{
				RadDate radDate = (RadDate)obj;
				if (this.SpecialDays.IndexOf(radDate.Date) > -1)
				{
					this.SpecialDays[radDate.Date].IsSelected = true;
				}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00036C74 File Offset: 0x00034E74
		internal ArrayList GetClientDateFormatInfo()
		{
			return new ArrayList
			{
				this.DateTimeFormat.DayNames,
				this.DateTimeFormat.AbbreviatedDayNames,
				this.DateTimeFormat.MonthNames,
				this.DateTimeFormat.AbbreviatedMonthNames,
				this.DateTimeFormat.FullDateTimePattern,
				this.DateTimeFormat.LongDatePattern,
				this.DateTimeFormat.LongTimePattern,
				this.DateTimeFormat.MonthDayPattern,
				this.DateTimeFormat.RFC1123Pattern,
				this.DateTimeFormat.ShortDatePattern,
				this.DateTimeFormat.ShortTimePattern,
				this.DateTimeFormat.SortableDateTimePattern,
				this.DateTimeFormat.UniversalSortableDateTimePattern,
				this.DateTimeFormat.YearMonthPattern,
				this.DateTimeFormat.AMDesignator,
				this.DateTimeFormat.PMDesignator,
				this.DateTimeFormat.DateSeparator,
				this.DateTimeFormat.TimeSeparator,
				this.DateTimeFormat.FirstDayOfWeek
			};
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00036DE3 File Offset: 0x00034FE3
		internal ArrayList GetClientSpecialDays()
		{
			return ((IClientData)this.SpecialDays).GetClientData();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00036DF0 File Offset: 0x00034FF0
		internal ArrayList GetClientDayTemplates()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.SpecialDays.Count; i++)
			{
				RadCalendarDay radCalendarDay = this.SpecialDays[i];
				if (!string.IsNullOrEmpty(radCalendarDay.TemplateID))
				{
					string id = Utility.SetCellID("dt", radCalendarDay.Date);
					Control control = this.FindControl(id);
					if (control != null)
					{
						arrayList.Add(control);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00036E60 File Offset: 0x00035060
		internal ArrayList GetViewsIDs()
		{
			return ((IClientData)this.CalendarView).GetClientData();
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00036E70 File Offset: 0x00035070
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.Page != null && ScriptManager.GetCurrent(this.Page) != null && ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
			{
				this.RenderDefaultView();
			}
			base.DescribeComponent(descriptor);
			this.ClientEvents.DescribeEvents(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00036EC8 File Offset: 0x000350C8
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			if (!this.EmptySkin)
			{
				descriptor.AddProperty("skin", base.RuntimeSkin);
			}
			descriptor.AddProperty("enabled", base.IsEnabled);
			descriptor.AddProperty("_firstDayOfWeek", this.FirstDayOfWeek);
			descriptor.AddProperty("_postBackCall", this.GetPostBackEventReference());
			descriptor.AddProperty("_calendarWeekRule", this.CultureInfo.DateTimeFormat.CalendarWeekRule);
			descriptor.AddProperty("_culture", this.CultureInfo.Name);
			descriptor.AddProperty("_enableViewSelector", this.EnableViewSelector);
			descriptor.AddProperty("_enableKeyboardNavigation", this.EnableKeyboardNavigation);
			base.DescribeRenderMode(descriptor);
			if (this.Overlay)
			{
				descriptor.AddProperty("_overlay", this.Overlay);
			}
			if (this.EnableKeyboardNavigation)
			{
				descriptor.AddProperty("_showRowHeaders", this.ShowRowHeaders);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			descriptor.AddScriptProperty("specialDaysArray", Utility.ConvertToClientArray1D(this.GetClientSpecialDays()));
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddScriptProperty("_FormatInfoArray", javaScriptSerializer.Serialize(this.GetClientDateFormatInfo()));
			descriptor.AddScriptProperty("_rangeSelectionMode", javaScriptSerializer.Serialize(this.RangeSelectionMode));
			descriptor.AddScriptProperty("_ViewsHash", Utility.ConvertToClientHash(this.GetViewsIDs()));
			descriptor.AddScriptProperty("_DayRenderChangedDays", this.GetDayRenderChangedDays());
			descriptor.AddScriptProperty("_ViewRepeatableDays", this.GetViewRepeatableDays());
			descriptor.AddScriptProperty("stylesHash", this.GetStyles());
			descriptor.AddScriptProperty("monthYearNavigationSettings", Utility.ConvertToClientArray1D(new string[]
			{
				this.FastNavigationSettings.TodayButtonCaption,
				this.FastNavigationSettings.OkButtonCaption,
				this.FastNavigationSettings.CancelButtonCaption,
				string.IsNullOrEmpty(this.FastNavigationSettings.DateIsOutOfRangeMessage) ? " " : this.FastNavigationSettings.DateIsOutOfRangeMessage,
				this.FastNavigationSettings.EnableTodayButtonSelection.ToString(),
				this.FastNavigationSettings.EnableScreenBoundaryDetection.ToString(),
				this.FastNavigationSettings.ShowAnimation.Duration.ToString(),
				((int)this.FastNavigationSettings.ShowAnimation.Type).ToString(),
				this.FastNavigationSettings.HideAnimation.Duration.ToString(),
				((int)this.FastNavigationSettings.HideAnimation.Type).ToString(),
				this.FastNavigationSettings.DisableOutOfRangeMonths.ToString()
			}));
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000371A8 File Offset: 0x000353A8
		private string GetStyles()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(Utility.GetStyle("DayStyle", this.DayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("CalendarTableStyle", this.CalendarTableStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("OtherMonthDayStyle", this.OtherMonthDayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("TitleStyle", this.TitleStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("SelectedDayStyle", this.SelectedDayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("SelectorStyle", this.HeaderStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("DisabledDayStyle", this.DisabledDayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("OutOfRangeDayStyle", this.OutOfRangeDayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("WeekendDayStyle", this.WeekendDayStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("DayOverStyle", this.DayOverStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("FastNavigationStyle", this.FastNavigationStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("ViewSelectorStyle", this.ViewSelectorStyle));
			stringBuilder.Append("}");
			return stringBuilder.ToString().Replace("&#32;", " ");
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003736A File Offset: 0x0003556A
		internal virtual string GetPostBackEventReference()
		{
			return this.Page.ClientScript.GetPostBackEventReference(this, "@@");
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00037384 File Offset: 0x00035584
		protected override void LoadViewState(object savedState)
		{
			if (!this.EnableViewState)
			{
				return;
			}
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.DayStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.CalendarTableStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.OtherMonthDayStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.TitleStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.SelectedDayStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.DisabledDayStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.OutOfRangeDayStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.WeekendDayStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.DayOverStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.FastNavigationStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.ViewSelectorStyle).LoadViewState(array[12]);
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000374A4 File Offset: 0x000356A4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				(this.dayStyle != null) ? ((IStateManager)this.dayStyle).SaveViewState() : null,
				(this.calendarTableStyle != null) ? ((IStateManager)this.calendarTableStyle).SaveViewState() : null,
				(this.otherMonthDayStyle != null) ? ((IStateManager)this.otherMonthDayStyle).SaveViewState() : null,
				(this.titleStyle != null) ? ((IStateManager)this.titleStyle).SaveViewState() : null,
				(this.selectedDayStyle != null) ? ((IStateManager)this.selectedDayStyle).SaveViewState() : null,
				(this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null,
				(this.disabledDayStyle != null) ? ((IStateManager)this.disabledDayStyle).SaveViewState() : null,
				(this.outOfRangeDayStyle != null) ? ((IStateManager)this.outOfRangeDayStyle).SaveViewState() : null,
				(this.weekendDayStyle != null) ? ((IStateManager)this.weekendDayStyle).SaveViewState() : null,
				(this.dayOverStyle != null) ? ((IStateManager)this.dayOverStyle).SaveViewState() : null,
				(this.fastNavigationStyle != null) ? ((IStateManager)this.fastNavigationStyle).SaveViewState() : null,
				(this.viewSelectorStyle != null) ? ((IStateManager)this.viewSelectorStyle).SaveViewState() : null
			};
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000375F4 File Offset: 0x000357F4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.dayStyle != null)
			{
				((IStateManager)this.dayStyle).TrackViewState();
			}
			if (this.calendarTableStyle != null)
			{
				((IStateManager)this.calendarTableStyle).TrackViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				((IStateManager)this.otherMonthDayStyle).TrackViewState();
			}
			if (this.titleStyle != null)
			{
				((IStateManager)this.titleStyle).TrackViewState();
			}
			if (this.selectedDayStyle != null)
			{
				((IStateManager)this.selectedDayStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.disabledDayStyle != null)
			{
				((IStateManager)this.disabledDayStyle).TrackViewState();
			}
			if (this.outOfRangeDayStyle != null)
			{
				((IStateManager)this.outOfRangeDayStyle).TrackViewState();
			}
			if (this.weekendDayStyle != null)
			{
				((IStateManager)this.weekendDayStyle).TrackViewState();
			}
			if (this.dayOverStyle != null)
			{
				((IStateManager)this.dayOverStyle).TrackViewState();
			}
			if (this.fastNavigationStyle != null)
			{
				((IStateManager)this.fastNavigationStyle).TrackViewState();
			}
			if (this.viewSelectorStyle != null)
			{
				((IStateManager)this.viewSelectorStyle).TrackViewState();
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000EC1 RID: 3777 RVA: 0x000376EB File Offset: 0x000358EB
		// (remove) Token: 0x06000EC2 RID: 3778 RVA: 0x000376FE File Offset: 0x000358FE
		public event EventHandler<ChildViewRenderEventArgs> ChildViewRender
		{
			add
			{
				base.Events.AddHandler(RadCalendar.EventChildViewRender, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCalendar.EventChildViewRender, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000EC3 RID: 3779 RVA: 0x00037711 File Offset: 0x00035911
		// (remove) Token: 0x06000EC4 RID: 3780 RVA: 0x00037724 File Offset: 0x00035924
		public event Telerik.Web.UI.Calendar.DayRenderEventHandler DayRender
		{
			add
			{
				base.Events.AddHandler(RadCalendar.EventDayRender, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCalendar.EventDayRender, value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000EC5 RID: 3781 RVA: 0x00037737 File Offset: 0x00035937
		// (remove) Token: 0x06000EC6 RID: 3782 RVA: 0x0003774A File Offset: 0x0003594A
		public event HeaderCellRenderEventHandler HeaderCellRender
		{
			add
			{
				base.Events.AddHandler(RadCalendar.EventHeaderCellRender, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCalendar.EventHeaderCellRender, value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000EC7 RID: 3783 RVA: 0x0003775D File Offset: 0x0003595D
		// (remove) Token: 0x06000EC8 RID: 3784 RVA: 0x00037770 File Offset: 0x00035970
		public event SelectedDatesEventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RadCalendar.EventSelectedDates, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCalendar.EventSelectedDates, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000EC9 RID: 3785 RVA: 0x00037783 File Offset: 0x00035983
		// (remove) Token: 0x06000ECA RID: 3786 RVA: 0x00037796 File Offset: 0x00035996
		public event DefaultViewChangedEventHandler DefaultViewChanged
		{
			add
			{
				base.Events.AddHandler(RadCalendar.EventDefaultViewChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCalendar.EventDefaultViewChanged, value);
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000377AC File Offset: 0x000359AC
		protected internal virtual void OnChildViewRender(CalendarView calView)
		{
			EventHandler<ChildViewRenderEventArgs> eventHandler = base.Events[RadCalendar.EventChildViewRender] as EventHandler<ChildViewRenderEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, new ChildViewRenderEventArgs(calView));
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000377E0 File Offset: 0x000359E0
		protected internal virtual void OnDayRender(TableCell cell, RadCalendarDay day, MonthView currentView)
		{
			Telerik.Web.UI.Calendar.DayRenderEventHandler dayRenderEventHandler = (Telerik.Web.UI.Calendar.DayRenderEventHandler)base.Events[RadCalendar.EventDayRender];
			if (dayRenderEventHandler != null)
			{
				dayRenderEventHandler(this, new Telerik.Web.UI.Calendar.DayRenderEventArgs(cell, day, currentView));
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00037818 File Offset: 0x00035A18
		protected internal virtual void OnHeaderCellRender(TableCell cell, HeaderType type)
		{
			HeaderCellRenderEventHandler headerCellRenderEventHandler = (HeaderCellRenderEventHandler)base.Events[RadCalendar.EventHeaderCellRender];
			if (headerCellRenderEventHandler != null)
			{
				headerCellRenderEventHandler(this, new HeaderCellRenderEventArgs(cell, type));
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003784C File Offset: 0x00035A4C
		protected virtual void OnSelectionChanged()
		{
			SelectedDatesEventHandler selectedDatesEventHandler = (SelectedDatesEventHandler)base.Events[RadCalendar.EventSelectedDates];
			if (selectedDatesEventHandler != null)
			{
				selectedDatesEventHandler(this, new SelectedDatesEventArgs(this.SelectedDates));
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00037884 File Offset: 0x00035A84
		protected virtual void OnDefaultViewChanged(DateTime newDate, DateTime previousDate)
		{
			DefaultViewChangedEventHandler defaultViewChangedEventHandler = (DefaultViewChangedEventHandler)base.Events[RadCalendar.EventDefaultViewChanged];
			if (defaultViewChangedEventHandler != null)
			{
				MonthView monthView = new MonthView(this);
				defaultViewChangedEventHandler(this, new DefaultViewChangedEventArgs(monthView.CreateViewForDate(previousDate), monthView.CreateViewForDate(newDate)));
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000378CC File Offset: 0x00035ACC
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (!this.EnableMultiSelect && this.SelectedDates.Count > 1)
			{
				this.SelectedDates.RemoveRange(0, this.SelectedDates.Count - 1);
			}
			if (this.Page != null)
			{
				this.GetPostBackEventReference();
				this.Page.RegisterRequiresPostBack(this);
			}
			if (!this.EmptySkin)
			{
				this.SetDefaultItemStyles();
			}
			Control control = this.FindControl("HeaderTemplateContainer");
			if (control != null)
			{
				control.Visible = false;
			}
			Control control2 = this.FindControl("FooterTemplateContainer");
			if (control2 != null)
			{
				control2.Visible = false;
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00037964 File Offset: 0x00035B64
		private void SetDefaultItemStyles()
		{
			this.CalendarTableStyle.CssClass = this.FormatCssClass("rcMainTable", this.CalendarTableStyle.CssClass);
			this.OtherMonthDayStyle.CssClass = this.FormatCssClass("rcOtherMonth", this.OtherMonthDayStyle.CssClass);
			this.SelectedDayStyle.CssClass = this.FormatCssClass("rcSelected", this.SelectedDayStyle.CssClass);
			this.DisabledDayStyle.CssClass = this.FormatCssClass("rcDisabled", this.DisabledDayStyle.CssClass);
			this.OutOfRangeDayStyle.CssClass = this.FormatCssClass("rcOutOfRange", this.OutOfRangeDayStyle.CssClass);
			this.WeekendDayStyle.CssClass = this.FormatCssClass("rcWeekend", this.WeekendDayStyle.CssClass);
			this.DayOverStyle.CssClass = this.FormatCssClass("rcHover", this.DayOverStyle.CssClass);
			this.FastNavigationStyle.CssClass = this.FormatCssClass("RadCalendarMonthView", this.FastNavigationStyle.CssClass);
			this.ViewSelectorStyle.CssClass = this.FormatCssClass("rcViewSel", this.ViewSelectorStyle.CssClass);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00037A9C File Offset: 0x00035C9C
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text;
			if (prefix == "RadCalendar" || prefix == "RadCalendarMonthView" || prefix == "RadCalendarMultiView")
			{
				text = (this.EmptySkin ? prefix : string.Format("{0} {0}_{1}", prefix, base.RuntimeSkin));
			}
			else
			{
				text = prefix;
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				userDefined = Regex.Replace(userDefined, prefix + "_\\S+\\s?", "").Trim();
			}
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined) || userDefined == prefix)
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00037B48 File Offset: 0x00035D48
		internal WebControl GetNavControl(string navImage, string navText, string toolTip, string navIDSuffix)
		{
			string navigateUrl = "#";
			HyperLink hyperLink = new HyperLink();
			hyperLink.ID = this.ClientID + navIDSuffix;
			hyperLink.NavigateUrl = navigateUrl;
			string str = "t-button ";
			string str2 = "";
			if (string.IsNullOrEmpty(navImage) && navIDSuffix != null)
			{
				if (!(navIDSuffix == "_FNP"))
				{
					if (!(navIDSuffix == "_NP"))
					{
						if (!(navIDSuffix == "_NN"))
						{
							if (navIDSuffix == "_FNN")
							{
								hyperLink.CssClass = str + "rcFastNext";
								str2 = "t-i-arrow-double-60-right";
							}
						}
						else
						{
							hyperLink.CssClass = str + "rcNext";
							str2 = "t-i-arrow-right";
						}
					}
					else
					{
						hyperLink.CssClass = str + "rcPrev";
						str2 = "t-i-arrow-left";
					}
				}
				else
				{
					hyperLink.CssClass = str + "rcFastPrev";
					str2 = "t-i-arrow-double-60-left";
				}
			}
			if (!string.IsNullOrEmpty(navImage))
			{
				HtmlImage htmlImage = new HtmlImage();
				htmlImage.Src = this.GetImage(navImage);
				htmlImage.Attributes["alt"] = toolTip;
				htmlImage.Style["border"] = "0px";
				hyperLink.Controls.Add(htmlImage);
			}
			else if (this.ResolvedRenderMode == RenderMode.Classic)
			{
				hyperLink.Controls.Add(new LiteralControl(navText));
			}
			else
			{
				Label label = new Label();
				label.CssClass = "t-font-icon " + str2;
				hyperLink.Controls.Add(label);
			}
			hyperLink.Attributes["title"] = toolTip;
			if (this.EnableAriaSupport)
			{
				hyperLink.Attributes["aria-label"] = toolTip;
			}
			return hyperLink;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00037CFC File Offset: 0x00035EFC
		internal Panel GetTitlePanel()
		{
			Panel panel = new Panel();
			panel.CssClass = "rcTitlebar";
			if (!string.IsNullOrEmpty(this.ImagesPath))
			{
				if (string.IsNullOrEmpty(this.FastNavigationPrevImage))
				{
					this.FastNavigationPrevImage = "fastNavLeft.gif";
				}
				if (string.IsNullOrEmpty(this.NavigationPrevImage))
				{
					this.NavigationPrevImage = "arrowLeft.gif";
				}
				if (string.IsNullOrEmpty(this.NavigationNextImage))
				{
					this.NavigationNextImage = "arrowRight.gif";
				}
				if (string.IsNullOrEmpty(this.FastNavigationNextImage))
				{
					this.FastNavigationNextImage = "fastNavRight.gif";
				}
			}
			if (this.EnableNavigation)
			{
				if (this.ShowFastNavigationButtons)
				{
					panel.Controls.Add(this.GetNavControl(this.FastNavigationPrevImage, this.FastNavigationPrevText, this.FastNavigationPrevToolTip, "_FNP"));
				}
				if (this.ShowNavigationButtons)
				{
					panel.Controls.Add(this.GetNavControl(this.NavigationPrevImage, this.NavigationPrevText, this.NavigationPrevToolTip, "_NP"));
				}
				Panel panel2 = new Panel();
				panel2.CssClass = "rcNextButtons";
				panel.Controls.Add(panel2);
				if (this.ShowNavigationButtons)
				{
					panel2.Controls.Add(this.GetNavControl(this.NavigationNextImage, this.NavigationNextText, this.NavigationNextToolTip, "_NN"));
				}
				if (this.ShowFastNavigationButtons)
				{
					panel2.Controls.Add(this.GetNavControl(this.FastNavigationNextImage, this.FastNavigationNextText, this.FastNavigationNextToolTip, "_FNN"));
				}
			}
			Label label = new Label();
			label.CssClass = "rcTitle";
			label.ID = this.ClientID + "_Title";
			label.Text = this.CalendarView.Title;
			panel.Controls.Add(label);
			panel.ApplyStyle(this.TitleStyle);
			return panel;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00037EC0 File Offset: 0x000360C0
		internal Table GetTitleTable()
		{
			Table table = new RadCalendar.AccessibleTable();
			table.CellPadding = this.NavigationCellPadding;
			if (this.ResolvedRenderMode == RenderMode.Classic)
			{
				table.Attributes["cellspacing"] = this.NavigationCellSpacing.ToString();
			}
			TableHeaderRow tableHeaderRow = new TableHeaderRow();
			if (!this.IsDesignMode)
			{
				table.Rows.Add(tableHeaderRow);
			}
			tableHeaderRow.TableSection = TableRowSection.TableHeader;
			tableHeaderRow.Style[HtmlTextWriterStyle.Display] = "none";
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Text = this.NavigationCaption;
			tableHeaderRow.Cells.Add(tableHeaderCell);
			tableHeaderCell.Attributes["scope"] = "col";
			TableRow tableRow = new TableRow();
			tableRow.TableSection = TableRowSection.TableBody;
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			TableCell tableCell3 = new TableCell();
			TableCell tableCell4 = new TableCell();
			TableCell tableCell5 = new TableCell();
			if (this.ShowFastNavigationButtons)
			{
				tableRow.Cells.Add(tableCell);
			}
			if (this.ShowNavigationButtons)
			{
				tableRow.Cells.Add(tableCell3);
			}
			tableRow.Cells.Add(tableCell5);
			if (this.ShowNavigationButtons)
			{
				tableRow.Cells.Add(tableCell4);
			}
			if (this.ShowFastNavigationButtons)
			{
				tableRow.Cells.Add(tableCell2);
			}
			if (!string.IsNullOrEmpty(this.ImagesPath))
			{
				if (string.IsNullOrEmpty(this.FastNavigationPrevImage))
				{
					this.FastNavigationPrevImage = "fastNavLeft.gif";
				}
				if (string.IsNullOrEmpty(this.NavigationPrevImage))
				{
					this.NavigationPrevImage = "arrowLeft.gif";
				}
				if (string.IsNullOrEmpty(this.NavigationNextImage))
				{
					this.NavigationNextImage = "arrowRight.gif";
				}
				if (string.IsNullOrEmpty(this.FastNavigationNextImage))
				{
					this.FastNavigationNextImage = "fastNavRight.gif";
				}
			}
			if (this.EnableNavigation)
			{
				if (this.ShowFastNavigationButtons)
				{
					tableCell.Controls.Add(this.GetNavControl(this.FastNavigationPrevImage, this.FastNavigationPrevText, this.FastNavigationPrevToolTip, "_FNP"));
				}
				if (this.ShowNavigationButtons)
				{
					tableCell3.Controls.Add(this.GetNavControl(this.NavigationPrevImage, this.NavigationPrevText, this.NavigationPrevToolTip, "_NP"));
				}
			}
			try
			{
				tableCell5.Text = this.CalendarView.Title;
				tableCell5.ID = this.ClientID + "_Title";
			}
			catch (FormatException)
			{
				throw new FormatException("TitleFormat format is invalid.");
			}
			if (this.TitleAlign != HorizontalAlign.NotSet)
			{
				tableCell5.Style["text-align"] = this.TitleAlign.ToString().ToLower();
			}
			tableCell5.CssClass = "rcTitle";
			if (this.EnableNavigation)
			{
				if (this.ShowNavigationButtons)
				{
					tableCell4.Controls.Add(this.GetNavControl(this.NavigationNextImage, this.NavigationNextText, this.NavigationNextToolTip, "_NN"));
				}
				if (this.ShowFastNavigationButtons)
				{
					tableCell2.Controls.Add(this.GetNavControl(this.FastNavigationNextImage, this.FastNavigationNextText, this.FastNavigationNextToolTip, "_FNN"));
				}
			}
			table.ApplyStyle(this.TitleStyle);
			if (!string.IsNullOrEmpty(this.NavigationSummary) && this.EnableAriaSupport)
			{
				table.Attributes["summary"] = this.NavigationSummary;
			}
			if (!string.IsNullOrEmpty(this.NavigationCaption))
			{
				table.Caption = string.Format("<span style='display:none;'>{0}</span>", this.NavigationCaption);
			}
			return table;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00038230 File Offset: 0x00036430
		internal void WriteLigthHtmlContent(HtmlTextWriter writer)
		{
			Panel titlePanel = this.GetTitlePanel();
			titlePanel.RenderControl(writer);
			Control control = this.FindControl("HeaderTemplateContainer");
			if (control != null)
			{
				Panel panel = new Panel
				{
					CssClass = "rcHeader"
				};
				control.Visible = true;
				panel.Controls.Add(control);
				panel.RenderControl(writer);
				control.Visible = false;
			}
			Panel panel2 = new Panel();
			panel2.CssClass = "rcMain";
			Table calendarViewStructure = this.CalendarView.GetCalendarViewStructure();
			calendarViewStructure.ID = this.CalendarView.ID;
			panel2.Controls.Add(calendarViewStructure);
			panel2.RenderControl(writer);
			Control control2 = this.FindControl("FooterTemplateContainer");
			if (control2 != null)
			{
				Panel panel3 = new Panel
				{
					CssClass = "rcFooter"
				};
				control2.Visible = true;
				panel3.Controls.Add(control2);
				panel3.RenderControl(writer);
				control2.Visible = false;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00038324 File Offset: 0x00036524
		internal void WriteHtmlContent(HtmlTextWriter writer)
		{
			Table table = new RadCalendar.AccessibleTable();
			table.ID = this.ClientID;
			if (!string.IsNullOrEmpty(this.CalendarSummary) && this.EnableAriaSupport)
			{
				table.Attributes["summary"] = this.CalendarSummary;
			}
			if (!string.IsNullOrEmpty(this.CalendarCaption))
			{
				table.Caption = string.Format("<span style='display:none;'>{0}</span>", this.CalendarCaption);
			}
			if (this.ResolvedRenderMode == RenderMode.Classic)
			{
				table.Attributes["cellspacing"] = this.DefaultCellSpacing.ToString();
			}
			if (this.Width != Unit.Empty)
			{
				table.Style["width"] = this.Width.ToString();
			}
			if (this.Height != Unit.Empty)
			{
				table.Style["height"] = this.Height.ToString();
			}
			if (this.BackColor != Color.Empty)
			{
				table.BackColor = this.BackColor;
			}
			if (this.BorderColor != Color.Empty)
			{
				table.BorderColor = this.BorderColor;
			}
			if (this.BorderStyle != BorderStyle.NotSet)
			{
				table.Style.Add("border-style", this.BorderStyle.ToString());
			}
			if (this.BorderWidth != Unit.Empty)
			{
				table.BorderWidth = int.Parse(this.BorderWidth.Value.ToString());
			}
			if (this.ForeColor != Color.Empty)
			{
				table.Style.Add("color", ColorTranslator.ToHtml(this.ForeColor));
			}
			if (!this.CalendarView.IsSingleView)
			{
				this.CssClass = this.FormatCssClass(string.Format("RadCalendarMultiView", base.RuntimeSkin), this.CssClass);
			}
			table.Attributes["class"] = this.FormatCssClass("RadCalendar", this.CssClass);
			if (base.Style.Count > 0)
			{
				foreach (object obj in base.Style.Keys)
				{
					string text = (string)obj;
					if (!this.IsDesignMode || !(text.ToLower() == "position"))
					{
						table.Style.Add(text, base.Style[text]);
					}
				}
			}
			TableHeaderRow tableHeaderRow = new TableHeaderRow();
			tableHeaderRow.TableSection = TableRowSection.TableHeader;
			TableRow tableRow = new TableRow();
			tableRow.TableSection = TableRowSection.TableBody;
			TableRow tableRow2 = new TableRow();
			TableRow tableRow3 = new TableRow();
			tableRow3.TableSection = TableRowSection.TableFooter;
			table.Rows.Add(tableHeaderRow);
			TableCell tableCell = new TableCell();
			tableCell.CssClass = this.FormatCssClass(string.Format("rcTitlebar{0}", (!this.EnableMonthYearFastNavigation) ? " rcNoNav" : ""), this.TitleStyle.CssClass);
			tableCell.Controls.Add(this.GetTitleTable());
			tableHeaderRow.Cells.Add(tableCell);
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "rcHeader";
			tableCell2.ID = this.ClientID + "_Header";
			Control control = this.FindControl("HeaderTemplateContainer");
			if (control != null)
			{
				table.Rows.Add(tableRow);
				StringBuilder stringBuilder = new StringBuilder();
				control.Visible = true;
				control.RenderControl(CalendarRenderer.CreateHtmlWriter(stringBuilder));
				control.Visible = false;
				tableCell2.Text = stringBuilder.ToString();
				tableRow.Cells.Add(tableCell2);
			}
			table.Rows.Add(tableRow2);
			TableCell tableCell3 = new TableCell();
			tableCell3.CssClass = string.Format("rcMain{0}{1}", (!this.CalendarView.IsSingleView) ? " rcCalendars" : "", (this.PresentationType == PresentationType.Preview) ? " rcPreview" : "");
			this.RenderDefaultView(tableCell3);
			tableRow2.Cells.Add(tableCell3);
			TableCell tableCell4 = new TableCell();
			tableCell4.ID = this.ClientID + "_Footer";
			tableCell4.CssClass = "rcFooter";
			Control control2 = this.FindControl("FooterTemplateContainer");
			if (control2 != null)
			{
				table.Rows.Add(tableRow3);
				StringBuilder stringBuilder2 = new StringBuilder();
				control2.Visible = true;
				control2.RenderControl(CalendarRenderer.CreateHtmlWriter(stringBuilder2));
				control2.Visible = false;
				tableCell4.Text = stringBuilder2.ToString();
				tableRow3.Cells.Add(tableCell4);
			}
			table.RenderControl(writer);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000387F4 File Offset: 0x000369F4
		protected void RenderDefaultView(TableCell inputCell)
		{
			if (this.CalendarView != null)
			{
				inputCell.Text = this.RenderDefaultView();
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003880C File Offset: 0x00036A0C
		internal string RenderDefaultView()
		{
			if (this.CalendarView != null)
			{
				if (string.IsNullOrEmpty(this._defaultViewContent))
				{
					StringBuilder stringBuilder = new StringBuilder();
					this.CalendarView.Render(CalendarRenderer.CreateHtmlWriter(stringBuilder));
					this._defaultViewContent = stringBuilder.ToString();
				}
				return this._defaultViewContent;
			}
			return string.Empty;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00038860 File Offset: 0x00036A60
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x0003888B File Offset: 0x00036A8B
		internal bool RenderInvisible
		{
			get
			{
				bool result = false;
				object obj = this.Properties["RenderInvisible"];
				if (obj == null)
				{
					return result;
				}
				return (bool)obj;
			}
			set
			{
				this.Properties["RenderInvisible"] = value;
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000388A3 File Offset: 0x00036AA3
		protected override IRenderer CreateControlRenderer()
		{
			return new CalendarRenderer(this);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000388AB File Offset: 0x00036AAB
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000388C1 File Offset: 0x00036AC1
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			BaseClass.RenderAjaxCssReferences(this, writer);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000388CC File Offset: 0x00036ACC
		protected override void Render(HtmlTextWriter writer)
		{
			string text = this.ClientID;
			if (this.ResolvedRenderMode == RenderMode.Classic)
			{
				text += "_wrapper";
			}
			if (!base.DesignMode)
			{
				if (!this.Visible)
				{
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
				if (this.RenderInvisible)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
				if (this.ResolvedRenderMode == RenderMode.Lightweight)
				{
					if (!this.CalendarView.IsSingleView)
					{
						this.CssClass = this.FormatCssClass(string.Format("RadCalendarMultiView", base.RuntimeSkin), this.CssClass);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("RadCalendar", this.CssClass));
					foreach (object obj in base.Style.Keys)
					{
						string text2 = (string)obj;
						writer.AddStyleAttribute(text2, base.Style[text2]);
					}
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				BaseClass.RenderVersionStamp(writer);
			}
			else
			{
				if (!this.EmptySkin)
				{
					this.SetDefaultItemStyles();
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				writer.AddAttribute(HtmlTextWriterAttribute.Style, base.Style.Value);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.EnsureChildControls();
			}
			CalendarRenderer renderer = (CalendarRenderer)this.Renderer;
			if (this.ResolvedRenderMode == RenderMode.Lightweight && !base.DesignMode)
			{
				this.WriteLigthHtmlContent(writer);
			}
			else
			{
				this.WriteHtmlContent(writer);
			}
			if (!base.DesignMode)
			{
				this.RenderSelectedDates(renderer, writer);
				this.RenderAuxiliaryDates(renderer, writer);
				if (this.RangeSelectionMode != RangeSelectionMode.None)
				{
					this.RenderRangeSelectionDates(renderer, writer);
				}
			}
			writer.RenderEndTag();
			base.Render(writer);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00038A8C File Offset: 0x00036C8C
		private void RenderSelectedDates(CalendarRenderer renderer, HtmlTextWriter writer)
		{
			object[] array = new object[this.SelectedDates.Count];
			this.SelectedDates.CopyTo(array, 0);
			renderer.WriteHiddenFieldRegistration(writer, "_SD", array);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00038AC4 File Offset: 0x00036CC4
		private void RenderAuxiliaryDates(CalendarRenderer renderer, HtmlTextWriter writer)
		{
			object[] data = new object[]
			{
				this.RangeMinDate,
				this.RangeMaxDate,
				this.FocusedDate
			};
			renderer.WriteHiddenFieldRegistration(writer, "_AD", data);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00038B14 File Offset: 0x00036D14
		private void RenderRangeSelectionDates(CalendarRenderer renderer, HtmlTextWriter writer)
		{
			if (this.SelectedDates.Count == 0)
			{
				this.RangeSelectionStartDate = new DateTime(1980, 1, 1);
				this.RangeSelectionEndDate = new DateTime(2099, 12, 30);
			}
			object[] data = new object[]
			{
				this.RangeSelectionStartDate,
				this.RangeSelectionEndDate
			};
			renderer.WriteHiddenFieldRegistration(writer, "_RS", data);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00038B86 File Offset: 0x00036D86
		private static DateTime TruncateTimeComponent(DateTime value)
		{
			return value.Subtract(value.TimeOfDay);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00038B96 File Offset: 0x00036D96
		internal void AddDayRenderChangedDay(string dateKey, string styleValue)
		{
			this.dayRenderChangedDays[dateKey] = styleValue;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00038BA5 File Offset: 0x00036DA5
		private string GetDayRenderChangedDays()
		{
			return Utility.GetClientSideHash(this.dayRenderChangedDays);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00038BB2 File Offset: 0x00036DB2
		internal void AddViewRepeatableDay(string dateKey, string specialDayID)
		{
			this.viewRepeatableDays[dateKey] = "\"" + specialDayID + "\"";
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00038BD0 File Offset: 0x00036DD0
		private string GetViewRepeatableDays()
		{
			return Utility.GetClientSideHash(this.viewRepeatableDays);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00038BE0 File Offset: 0x00036DE0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<string>(descriptor, "cellDayFormat", this.CellDayFormat, "%d");
			base.DescribeProperty<string>(descriptor, "dateRangeSeparator", this.DateRangeSeparator, " - ");
			base.DescribeProperty<string>(descriptor, "dayCellToolTipFormat", this.DayCellToolTipFormat, "dddd, MMMM dd, yyyy");
			base.DescribeProperty<bool>(descriptor, "calendarEnableMonthYearFastNavigation", this.EnableMonthYearFastNavigation, true);
			base.DescribeProperty<bool>(descriptor, "enableMultiSelect", this.EnableMultiSelect, true);
			base.DescribeProperty<bool>(descriptor, "calendarEnableNavigation", this.EnableNavigation, true);
			base.DescribeProperty<bool>(descriptor, "enableNavigationAnimation", this.EnableNavigationAnimation, false);
			base.DescribeProperty<bool>(descriptor, "enableRepeatableDaysOnClient", this.EnableRepeatableDaysOnClient, true);
			base.DescribeProperty<bool>(descriptor, "_enableShadows", this.EnableShadows, true);
			base.DescribeProperty<int>(descriptor, "fastNavigationStep", this.FastNavigationStep, 3);
			base.DescribeProperty<int>(descriptor, "multiViewColumns", this.MultiViewColumns, 1);
			base.DescribeProperty<int>(descriptor, "multiViewRows", this.MultiViewRows, 1);
			base.DescribeProperty<Telerik.Web.UI.Calendar.Orientation>(descriptor, "orientation", this.Orientation, Telerik.Web.UI.Calendar.Orientation.RenderInRows);
			base.DescribeProperty<PresentationType>(descriptor, "presentationType", this.PresentationType, PresentationType.Interactive);
			base.DescribeProperty<bool>(descriptor, "showDayCellToolTips", this.ShowDayCellToolTips, true);
			base.DescribeProperty<bool>(descriptor, "showOtherMonthsDays", this.ShowOtherMonthsDays, true);
			base.DescribeProperty<int>(descriptor, "singleViewColumns", this.SingleViewColumns, 7);
			base.DescribeProperty<int>(descriptor, "singleViewRows", this.SingleViewRows, 6);
			base.DescribeProperty<string>(descriptor, "titleFormat", this.ResolvedTitleFormat(), "MMMM yyyy");
			base.DescribeProperty<bool>(descriptor, "useColumnHeadersAsSelectors", this.UseColumnHeadersAsSelectors, true);
			base.DescribeProperty<bool>(descriptor, "useRowHeadersAsSelectors", this.UseRowHeadersAsSelectors, true);
			base.DescribeProperty<bool>(descriptor, "hideNavigationControls", this.HideNavigationControls, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00038DB9 File Offset: 0x00036FB9
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040003F6 RID: 1014
		internal const string RangeMaxDateID = "MaxD";

		// Token: 0x040003F7 RID: 1015
		internal const string RangeMinDateID = "MinD";

		// Token: 0x040003F8 RID: 1016
		internal const string FocusedDateID = "FocD";

		// Token: 0x040003F9 RID: 1017
		private DateTimeCollection _SelectedDates = new DateTimeCollection();

		// Token: 0x040003FA RID: 1018
		private Hashtable dayRenderChangedDays = new Hashtable();

		// Token: 0x040003FB RID: 1019
		private Hashtable viewRepeatableDays = new Hashtable();

		// Token: 0x040003FC RID: 1020
		private MonthYearFastNavigationSettings _fastNavigaionSettings;

		// Token: 0x040003FD RID: 1021
		internal CalendarDayTemplateCollection _CalendarDayTemplates;

		// Token: 0x040003FE RID: 1022
		private CalendarStrings _localization;

		// Token: 0x040003FF RID: 1023
		private ITemplate _headerTemplate;

		// Token: 0x04000400 RID: 1024
		private ITemplate _footerTemplate;

		// Token: 0x04000401 RID: 1025
		private TableItemStyle dayStyle;

		// Token: 0x04000402 RID: 1026
		private TableItemStyle weekendDayStyle;

		// Token: 0x04000403 RID: 1027
		private TableItemStyle calendarTableStyle;

		// Token: 0x04000404 RID: 1028
		private TableItemStyle otherMonthDayStyle;

		// Token: 0x04000405 RID: 1029
		private TableItemStyle outOfRangeDayStyle;

		// Token: 0x04000406 RID: 1030
		private TableItemStyle disabledDayStyle;

		// Token: 0x04000407 RID: 1031
		private TableItemStyle selectedDayStyle;

		// Token: 0x04000408 RID: 1032
		private TableItemStyle dayOverStyle;

		// Token: 0x04000409 RID: 1033
		private TableItemStyle titleStyle;

		// Token: 0x0400040A RID: 1034
		private TableItemStyle headerStyle;

		// Token: 0x0400040B RID: 1035
		private TableItemStyle fastNavigationStyle;

		// Token: 0x0400040C RID: 1036
		private TableItemStyle viewSelectorStyle;

		// Token: 0x0400040D RID: 1037
		private CalendarView _CalendarView;

		// Token: 0x0400040E RID: 1038
		private static readonly object EventDayRender = new object();

		// Token: 0x0400040F RID: 1039
		private static readonly object EventHeaderCellRender = new object();

		// Token: 0x04000410 RID: 1040
		private static readonly object EventSelectedDates = new object();

		// Token: 0x04000411 RID: 1041
		private static readonly object EventDefaultViewChanged = new object();

		// Token: 0x04000412 RID: 1042
		private static readonly object EventChildViewRender = new object();

		// Token: 0x04000413 RID: 1043
		private string _defaultViewContent = string.Empty;

		// Token: 0x0200019A RID: 410
		internal class AccessibleTable : Table
		{
			// Token: 0x06000EEA RID: 3818 RVA: 0x00038DC4 File Offset: 0x00036FC4
			protected virtual bool HasRowSections()
			{
				bool result = false;
				foreach (object obj in this.Rows)
				{
					TableRow tableRow = (TableRow)obj;
					if (tableRow.TableSection != TableRowSection.TableBody)
					{
						result = true;
						break;
					}
				}
				return result;
			}

			// Token: 0x06000EEB RID: 3819 RVA: 0x00038E28 File Offset: 0x00037028
			protected override void RenderContents(HtmlTextWriter writer)
			{
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
				TableRowCollection rows = this.Rows;
				if (rows.Count > 0)
				{
					if (this.HasRowSections())
					{
						TableRowSection tableRowSection = TableRowSection.TableHeader;
						bool flag = false;
						foreach (object obj in rows)
						{
							TableRow tableRow = (TableRow)obj;
							if (tableRowSection < tableRow.TableSection || (tableRow.TableSection == TableRowSection.TableHeader && !flag))
							{
								if (flag)
								{
									if (tableRowSection != TableRowSection.TableBody)
									{
										writer.RenderEndTag();
									}
									else
									{
										htmlTextWriter.RenderEndTag();
									}
								}
								tableRowSection = tableRow.TableSection;
								flag = true;
								switch (tableRowSection)
								{
								case TableRowSection.TableHeader:
									writer.RenderBeginTag(HtmlTextWriterTag.Thead);
									break;
								case TableRowSection.TableBody:
									htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
									break;
								case TableRowSection.TableFooter:
									writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
									break;
								}
							}
							if (tableRowSection != TableRowSection.TableBody)
							{
								tableRow.RenderControl(writer);
							}
							else
							{
								tableRow.RenderControl(htmlTextWriter);
							}
						}
						if (tableRowSection != TableRowSection.TableBody)
						{
							writer.RenderEndTag();
						}
						else
						{
							htmlTextWriter.RenderEndTag();
						}
						writer.Write(htmlTextWriter.InnerWriter.ToString());
						return;
					}
					foreach (object obj2 in rows)
					{
						TableRow tableRow2 = (TableRow)obj2;
						tableRow2.RenderControl(writer);
					}
				}
			}
		}
	}
}
