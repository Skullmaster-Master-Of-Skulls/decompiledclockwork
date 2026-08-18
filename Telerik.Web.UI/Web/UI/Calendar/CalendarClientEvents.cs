using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Web.UI.Calendar.Persistence;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000FF7 RID: 4087
	public class CalendarClientEvents : PropertiesObject
	{
		// Token: 0x06009FDA RID: 40922 RVA: 0x00239EA8 File Offset: 0x002380A8
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x06009FDB RID: 40923 RVA: 0x00239EB8 File Offset: 0x002380B8
		internal void DescribeEvents(IScriptDescriptor descriptor)
		{
			string[] array = new string[]
			{
				"Init",
				"Load",
				"DateSelecting",
				"DateSelected",
				"DateClick",
				"RowHeaderClick",
				"ColumnHeaderClick",
				"ViewSelectorClick",
				"CalendarViewChanging",
				"CalendarViewChanged",
				"DayRender"
			};
			foreach (string text in array)
			{
				string text2 = (string)DataBinder.GetPropertyValue(this, string.Format("On{0}", text));
				if (!string.IsNullOrEmpty(text2))
				{
					descriptor.AddEvent(Regex.Replace(text, "^[A-Z]", new MatchEvaluator(CalendarClientEvents.ToLower)), text2);
				}
			}
		}

		// Token: 0x17003281 RID: 12929
		// (get) Token: 0x06009FDC RID: 40924 RVA: 0x00239F82 File Offset: 0x00238182
		// (set) Token: 0x06009FDD RID: 40925 RVA: 0x00239FA2 File Offset: 0x002381A2
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("The event is fired after the RadCalendar client object has been completely initialized.")]
		public string OnInit
		{
			get
			{
				return (base.Properties["A"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["A"] = value;
			}
		}

		// Token: 0x17003282 RID: 12930
		// (get) Token: 0x06009FDE RID: 40926 RVA: 0x00239FB5 File Offset: 0x002381B5
		// (set) Token: 0x06009FDF RID: 40927 RVA: 0x00239FD5 File Offset: 0x002381D5
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The event is fired immediately after the page onload event.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnLoad
		{
			get
			{
				return (base.Properties["B"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["B"] = value;
			}
		}

		// Token: 0x17003283 RID: 12931
		// (get) Token: 0x06009FE0 RID: 40928 RVA: 0x00239FE8 File Offset: 0x002381E8
		// (set) Token: 0x06009FE1 RID: 40929 RVA: 0x0023A008 File Offset: 0x00238208
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The event is fired when a valid date is being selected.")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnDateSelecting
		{
			get
			{
				return (base.Properties["Z"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["Z"] = value;
			}
		}

		// Token: 0x17003284 RID: 12932
		// (get) Token: 0x06009FE2 RID: 40930 RVA: 0x0023A01B File Offset: 0x0023821B
		// (set) Token: 0x06009FE3 RID: 40931 RVA: 0x0023A03B File Offset: 0x0023823B
		[Category("Client-side events")]
		[Description("The event is fired after a valid date has been selected.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnDateSelected
		{
			get
			{
				return (base.Properties["E"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["E"] = value;
			}
		}

		// Token: 0x17003285 RID: 12933
		// (get) Token: 0x06009FE4 RID: 40932 RVA: 0x0023A04E File Offset: 0x0023824E
		// (set) Token: 0x06009FE5 RID: 40933 RVA: 0x0023A06E File Offset: 0x0023826E
		[Description("The event is fired when a calendar date is clicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnDateClick
		{
			get
			{
				return (base.Properties["J"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["J"] = value;
			}
		}

		// Token: 0x17003286 RID: 12934
		// (get) Token: 0x06009FE6 RID: 40934 RVA: 0x0023A081 File Offset: 0x00238281
		// (set) Token: 0x06009FE7 RID: 40935 RVA: 0x0023A0A1 File Offset: 0x002382A1
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The event is fired when a calendar row header is clicked")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnRowHeaderClick
		{
			get
			{
				return (base.Properties["R"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["R"] = value;
			}
		}

		// Token: 0x17003287 RID: 12935
		// (get) Token: 0x06009FE8 RID: 40936 RVA: 0x0023A0B4 File Offset: 0x002382B4
		// (set) Token: 0x06009FE9 RID: 40937 RVA: 0x0023A0D4 File Offset: 0x002382D4
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The event is fired when a calendar column header is clicked")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnColumnHeaderClick
		{
			get
			{
				return (base.Properties["C"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["C"] = value;
			}
		}

		// Token: 0x17003288 RID: 12936
		// (get) Token: 0x06009FEA RID: 40938 RVA: 0x0023A0E7 File Offset: 0x002382E7
		// (set) Token: 0x06009FEB RID: 40939 RVA: 0x0023A107 File Offset: 0x00238307
		[Category("Client-side events")]
		[Description("The event is fired when a calendar view selector is clicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnViewSelectorClick
		{
			get
			{
				return (base.Properties["V"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["V"] = value;
			}
		}

		// Token: 0x17003289 RID: 12937
		// (get) Token: 0x06009FEC RID: 40940 RVA: 0x0023A11A File Offset: 0x0023831A
		// (set) Token: 0x06009FED RID: 40941 RVA: 0x0023A13A File Offset: 0x0023833A
		[Description("The event is fired when the calendar view is about to change.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnCalendarViewChanging
		{
			get
			{
				return (base.Properties["F"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["F"] = value;
			}
		}

		// Token: 0x1700328A RID: 12938
		// (get) Token: 0x06009FEE RID: 40942 RVA: 0x0023A14D File Offset: 0x0023834D
		// (set) Token: 0x06009FEF RID: 40943 RVA: 0x0023A16D File Offset: 0x0023836D
		[Category("Client-side events")]
		[Description("The event is fired when the calendar view has changed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnCalendarViewChanged
		{
			get
			{
				return (base.Properties["Y"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["Y"] = value;
			}
		}

		// Token: 0x1700328B RID: 12939
		// (get) Token: 0x06009FF0 RID: 40944 RVA: 0x0023A180 File Offset: 0x00238380
		// (set) Token: 0x06009FF1 RID: 40945 RVA: 0x0023A1A0 File Offset: 0x002383A0
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The event is fired for each calendar cell when a client-side navigation occurs.")]
		[NotifyParentProperty(true)]
		public string OnDayRender
		{
			get
			{
				return (base.Properties["I"] as string) ?? string.Empty;
			}
			set
			{
				base.Properties["I"] = value;
			}
		}

		// Token: 0x04002CC1 RID: 11457
		internal const string OnInitID = "A";

		// Token: 0x04002CC2 RID: 11458
		internal const string OnLoadID = "B";

		// Token: 0x04002CC3 RID: 11459
		internal const string OnDateSelectingID = "Z";

		// Token: 0x04002CC4 RID: 11460
		internal const string OnDateSelectedID = "E";

		// Token: 0x04002CC5 RID: 11461
		internal const string OnCalendarViewChangingID = "F";

		// Token: 0x04002CC6 RID: 11462
		internal const string OnCalendarViewChangedID = "Y";

		// Token: 0x04002CC7 RID: 11463
		internal const string OnShowContextMenuID = "G";

		// Token: 0x04002CC8 RID: 11464
		internal const string OnCalendarChangeID = "H";

		// Token: 0x04002CC9 RID: 11465
		internal const string OnDateClickID = "J";

		// Token: 0x04002CCA RID: 11466
		internal const string OnDayRenderID = "I";

		// Token: 0x04002CCB RID: 11467
		internal const string OnRowHeaderClickID = "R";

		// Token: 0x04002CCC RID: 11468
		internal const string OnColumnHeaderClickID = "C";

		// Token: 0x04002CCD RID: 11469
		internal const string OnViewSelectorClickID = "V";
	}
}
