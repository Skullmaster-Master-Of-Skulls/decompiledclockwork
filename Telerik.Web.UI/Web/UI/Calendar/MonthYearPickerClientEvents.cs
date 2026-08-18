using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000A30 RID: 2608
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MonthYearPickerClientEvents : ObjectWithState
	{
		// Token: 0x060062AD RID: 25261 RVA: 0x0017396B File Offset: 0x00171B6B
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x00173978 File Offset: 0x00171B78
		public MonthYearPickerClientEvents(StateBag OwnerStateBag, RadMonthYearPicker owner) : base("dpce_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x00173990 File Offset: 0x00171B90
		internal void DescribeEvents(IScriptDescriptor descriptor)
		{
			string[] array = new string[]
			{
				"DateSelected",
				"PopupOpening",
				"PopupClosing",
				"ViewChanged",
				"YearSelected",
				"MonthSelected"
			};
			foreach (string text in array)
			{
				string text2 = (string)DataBinder.GetPropertyValue(this, string.Format(CultureInfo.InvariantCulture, "On{0}", new object[]
				{
					text
				}));
				if (!string.IsNullOrEmpty(text2))
				{
					descriptor.AddEvent(Regex.Replace(text, "^[A-Z]", new MatchEvaluator(MonthYearPickerClientEvents.ToLower)), text2);
				}
			}
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x00173A42 File Offset: 0x00171C42
		// (set) Token: 0x060062B1 RID: 25265 RVA: 0x00173A62 File Offset: 0x00171C62
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("The name of the client-side event handler that is executed whenever the selected date of the datepicker is changed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnDateSelected
		{
			get
			{
				return (base.ViewState["OnDateSelected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDateSelected"] = value;
			}
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x060062B2 RID: 25266 RVA: 0x00173A75 File Offset: 0x00171C75
		// (set) Token: 0x060062B3 RID: 25267 RVA: 0x00173A95 File Offset: 0x00171C95
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("The name of the client-side event handler that is executed whenever the selected month of the picker is changed.")]
		public string OnMonthSelected
		{
			get
			{
				return (base.ViewState["OnMonthSelected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnMonthSelected"] = value;
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x060062B4 RID: 25268 RVA: 0x00173AA8 File Offset: 0x00171CA8
		// (set) Token: 0x060062B5 RID: 25269 RVA: 0x00173AC8 File Offset: 0x00171CC8
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the client-side event handler that is executed whenever the selected year of the picker is changed.")]
		[NotifyParentProperty(true)]
		public string OnYearSelected
		{
			get
			{
				return (base.ViewState["OnYearSelected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnYearSelected"] = value;
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x060062B6 RID: 25270 RVA: 0x00173ADB File Offset: 0x00171CDB
		// (set) Token: 0x060062B7 RID: 25271 RVA: 0x00173AFB File Offset: 0x00171CFB
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the client-side event handler that is executed whenever the years view is changed.")]
		public string OnViewChanged
		{
			get
			{
				return (base.ViewState["OnViewChanged"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnViewChanged"] = value;
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x060062B8 RID: 25272 RVA: 0x00173B0E File Offset: 0x00171D0E
		// (set) Token: 0x060062B9 RID: 25273 RVA: 0x00173B2E File Offset: 0x00171D2E
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnPopupOpening
		{
			get
			{
				return (base.ViewState["OnPopupOpening"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnPopupOpening"] = value;
			}
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x00173B41 File Offset: 0x00171D41
		// (set) Token: 0x060062BB RID: 25275 RVA: 0x00173B61 File Offset: 0x00171D61
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("")]
		[Category("Client-side events")]
		public string OnPopupClosing
		{
			get
			{
				return (base.ViewState["OnPopupClosing"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnPopupClosing"] = value;
			}
		}

		// Token: 0x04001828 RID: 6184
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public RadMonthYearPicker owner;
	}
}
