using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI
{
	// Token: 0x020003D2 RID: 978
	public class HtmlChartClientEvents : SerializableChartElement
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x00077918 File Offset: 0x00075B18
		public HtmlChartClientEvents()
		{
			base.RegisterConverters(new List<JavaScriptConverter>
			{
				new HtmlChartClientEventsConverter()
			});
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x00077943 File Offset: 0x00075B43
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x00077963 File Offset: 0x00075B63
		[Description("Gets or sets the name of the JavaScript function that will be called when a series is clicked.")]
		[DefaultValue("")]
		public string OnSeriesClick
		{
			get
			{
				return (string)(base.ViewState["OnSeriesClick"] ?? "");
			}
			set
			{
				base.ViewState["OnSeriesClick"] = value;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x00077976 File Offset: 0x00075B76
		// (set) Token: 0x060023EB RID: 9195 RVA: 0x00077996 File Offset: 0x00075B96
		[Description("Gets or sets the name of the JavaScript function that will be called when a series is hovered.")]
		[DefaultValue("")]
		public string OnSeriesHover
		{
			get
			{
				return (string)(base.ViewState["OnSeriesHover"] ?? "");
			}
			set
			{
				base.ViewState["OnSeriesHover"] = value;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000779A9 File Offset: 0x00075BA9
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x000779C9 File Offset: 0x00075BC9
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function that will be called when a legend item is clicked.")]
		public string OnLegendItemClick
		{
			get
			{
				return (string)(base.ViewState["OnLegendItemClick"] ?? "");
			}
			set
			{
				base.ViewState["OnLegendItemClick"] = value;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000779DC File Offset: 0x00075BDC
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000779FC File Offset: 0x00075BFC
		[Description("Gets or sets the name of the JavaScript function that will be called when a legend item is hovered.")]
		[DefaultValue("")]
		public string OnLegendItemHover
		{
			get
			{
				return (string)(base.ViewState["OnLegendItemHover"] ?? "");
			}
			set
			{
				base.ViewState["OnLegendItemHover"] = value;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00077A0F File Offset: 0x00075C0F
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x00077A2F File Offset: 0x00075C2F
		[Description("Gets or sets the name of the JavaScript function that will be called when the client load event is raised.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x00077A42 File Offset: 0x00075C42
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x00077A62 File Offset: 0x00075C62
		[Description("Specifies a function that will be called when the user starts dragging the chart.")]
		[DefaultValue("")]
		public string OnDragStart
		{
			get
			{
				return (string)(base.ViewState["OnDragStart"] ?? "");
			}
			set
			{
				base.ViewState["OnDragStart"] = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x00077A75 File Offset: 0x00075C75
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x00077A95 File Offset: 0x00075C95
		[Description("Specifies a function that will be called when the user is dragging the chart.")]
		[DefaultValue("")]
		public string OnDrag
		{
			get
			{
				return (string)(base.ViewState["OnDrag"] ?? "");
			}
			set
			{
				base.ViewState["OnDrag"] = value;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00077AA8 File Offset: 0x00075CA8
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x00077AC8 File Offset: 0x00075CC8
		[Description("Specifies a function that will be called when the user stops dragging the chart.")]
		[DefaultValue("")]
		public string OnDragEnd
		{
			get
			{
				return (string)(base.ViewState["OnDragEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnDragEnd"] = value;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00077ADB File Offset: 0x00075CDB
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x00077AFB File Offset: 0x00075CFB
		[DefaultValue("")]
		[Description("Specifies a function that will be called when the user stops starts zooming the chart using the mouse wheel.")]
		public string OnZoomStart
		{
			get
			{
				return (string)(base.ViewState["OnZoomStart"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomStart"] = value;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00077B0E File Offset: 0x00075D0E
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x00077B2E File Offset: 0x00075D2E
		[Description("Specifies a function that will be called when the user is zooming of the chart using the mouse wheel.")]
		[DefaultValue("")]
		public string OnZoom
		{
			get
			{
				return (string)(base.ViewState["OnZoom"] ?? "");
			}
			set
			{
				base.ViewState["OnZoom"] = value;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00077B41 File Offset: 0x00075D41
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00077B61 File Offset: 0x00075D61
		[Description("Specifies a function that will be called when the user stops zooming the chart.")]
		[DefaultValue("")]
		public string OnZoomEnd
		{
			get
			{
				return (string)(base.ViewState["OnZoomEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomEnd"] = value;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00077B74 File Offset: 0x00075D74
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x00077B94 File Offset: 0x00075D94
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Specifies a function that will be called just before the Kendo chart is initialized.")]
		[ClientControlEvent]
		public string OnKendoWidgetInitializing
		{
			get
			{
				return (string)(base.ViewState["OnKendoWidgetInitializing"] ?? "");
			}
			set
			{
				base.ViewState["OnKendoWidgetInitializing"] = value;
			}
		}
	}
}
