using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000049 RID: 73
	[ClientCssResource("BubbleChart")]
	[ClientScriptResource("Sys.Extended.UI.BubbleChart", "BubbleChart")]
	[ToolboxBitmap(typeof(Accessor), "BubbleChart.bmp")]
	public class BubbleChart : ChartBase
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000088F8 File Offset: 0x00006AF8
		[ClientPropertyName("bubbleChartClientValues")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty(true, true)]
		public List<BubbleChartValue> BubbleChartClientValues
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00008900 File Offset: 0x00006B00
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(ChartBaseSeriesEditor<BubbleChartValue>), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<BubbleChartValue> BubbleChartValues
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00008908 File Offset: 0x00006B08
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00008910 File Offset: 0x00006B10
		[DefaultValue(6)]
		[ExtenderControlProperty]
		[ClientPropertyName("yAxisLines")]
		public int YAxisLines { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008919 File Offset: 0x00006B19
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00008921 File Offset: 0x00006B21
		[ClientPropertyName("xAxisLines")]
		[ExtenderControlProperty]
		[DefaultValue(6)]
		public int XAxisLines { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000892A File Offset: 0x00006B2A
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00008932 File Offset: 0x00006B32
		[DefaultValue(5)]
		[ClientPropertyName("bubbleSizes")]
		[ExtenderControlProperty]
		public int BubbleSizes { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000893B File Offset: 0x00006B3B
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00008943 File Offset: 0x00006B43
		[ExtenderControlProperty]
		[ClientPropertyName("yAxisLineColor")]
		[DefaultValue("")]
		public string YAxisLineColor { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000894C File Offset: 0x00006B4C
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00008954 File Offset: 0x00006B54
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("xAxisLineColor")]
		public string XAxisLineColor { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000895D File Offset: 0x00006B5D
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00008965 File Offset: 0x00006B65
		[ClientPropertyName("baseLineColor")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string BaseLineColor { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000896E File Offset: 0x00006B6E
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00008976 File Offset: 0x00006B76
		[DefaultValue("#FFC652")]
		[ClientPropertyName("tooltipBackgroundColor")]
		[ExtenderControlProperty]
		public string TooltipBackgroundColor { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000897F File Offset: 0x00006B7F
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008987 File Offset: 0x00006B87
		[ClientPropertyName("tooltipFontColor")]
		[ExtenderControlProperty]
		[DefaultValue("#0E426C")]
		public string TooltipFontColor { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008990 File Offset: 0x00006B90
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00008998 File Offset: 0x00006B98
		[ClientPropertyName("tooltipBorderColor")]
		[ExtenderControlProperty]
		[DefaultValue("#B85B3E")]
		public string TooltipBorderColor { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000089A1 File Offset: 0x00006BA1
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000089A9 File Offset: 0x00006BA9
		[ClientPropertyName("xAxisLabel")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string XAxisLabel { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000089B2 File Offset: 0x00006BB2
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000089BA File Offset: 0x00006BBA
		[ClientPropertyName("yAxisLabel")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string YAxisLabel { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000089C3 File Offset: 0x00006BC3
		// (set) Token: 0x0600027D RID: 637 RVA: 0x000089CB File Offset: 0x00006BCB
		[ClientPropertyName("bubbleLabel")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string BubbleLabel { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000089D4 File Offset: 0x00006BD4
		// (set) Token: 0x0600027F RID: 639 RVA: 0x000089DC File Offset: 0x00006BDC
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("axislabelFontColor")]
		public string AxislabelFontColor { get; set; }

		// Token: 0x06000280 RID: 640 RVA: 0x000089E8 File Offset: 0x00006BE8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.IsDesignMode)
			{
				return;
			}
			foreach (BubbleChartValue bubbleChartValue in this.BubbleChartValues)
			{
				if (string.IsNullOrWhiteSpace(bubbleChartValue.Category))
				{
					throw new Exception("Category is missing the BubbleChartValue. Please provide a Category in the BubbleChartValue.");
				}
				if (bubbleChartValue.Data == 0m)
				{
					throw new Exception("Data is missing the BubbleChartValue. Please provide a value of Data in the BubbleChartValue.");
				}
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008A7C File Offset: 0x00006C7C
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.ID = "_ParentDiv";
			htmlGenericControl.Attributes.Add("style", string.Format("border-style:solid; border-width:1px;width:{0};height:{1};", base.ChartWidth, base.ChartHeight));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script>");
			stringBuilder.Append("function init(evt) { ");
			stringBuilder.Append("    if ( window.svgDocument == null ) { ");
			stringBuilder.Append("        gDocument = evt.target.ownerDocument;");
			stringBuilder.Append("    } ");
			stringBuilder.Append("} ");
			stringBuilder.Append("function ShowTooltip(me, evt, category, data, bubbleLabel) { ");
			stringBuilder.Append(string.Format("    var tooltipDiv = document.getElementById('{0}_tooltipDiv');", this.ClientID));
			stringBuilder.Append("    tooltipDiv.innerHTML = String.format('{0}: {1} {2}', category, data, bubbleLabel) ;");
			stringBuilder.Append("    tooltipDiv.style.top = evt.pageY - 25 + 'px';");
			stringBuilder.Append("    tooltipDiv.style.left = evt.pageX + 20 + 'px';");
			stringBuilder.Append("    tooltipDiv.style.visibility = 'visible';");
			stringBuilder.Append("    me.style.strokeWidth = '4';");
			stringBuilder.Append("    me.style.fillOpacity = '1';");
			stringBuilder.Append("    me.style.strokeOpacity = '1';");
			stringBuilder.Append("} ");
			stringBuilder.Append("function HideTooltip(me, evt) { ");
			stringBuilder.Append(string.Format("    var tooltipDiv = document.getElementById('{0}_tooltipDiv');", this.ClientID));
			stringBuilder.Append("    tooltipDiv.innerHTML = '';");
			stringBuilder.Append("    tooltipDiv.style.visibility = 'hidden';");
			stringBuilder.Append("    me.style.strokeWidth = '0';");
			stringBuilder.Append("    me.style.fillOpacity = '0.7';");
			stringBuilder.Append("    me.style.strokeOpacity = '0.7';");
			stringBuilder.Append("} ");
			stringBuilder.Append("</script>");
			htmlGenericControl.InnerHtml = stringBuilder.ToString();
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x040000D6 RID: 214
		private List<BubbleChartValue> _values = new List<BubbleChartValue>();
	}
}
