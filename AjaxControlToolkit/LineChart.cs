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
	// Token: 0x0200012B RID: 299
	[ToolboxBitmap(typeof(Accessor), "LineChart.bmp")]
	[ClientScriptResource("Sys.Extended.UI.LineChart", "LineChart")]
	[ClientCssResource("LineChart")]
	public class LineChart : ChartBase
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000140A8 File Offset: 0x000122A8
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001408E File Offset: 0x0001228E
		[ExtenderControlProperty]
		[DefaultValue(true)]
		[ClientPropertyName("displayValues")]
		public bool DisplayValues
		{
			get
			{
				object obj = this.ViewState["DisplayValues"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplayValues"] = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000140D1 File Offset: 0x000122D1
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x000140D9 File Offset: 0x000122D9
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("categoriesAxis")]
		public string CategoriesAxis { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000140E2 File Offset: 0x000122E2
		[ClientPropertyName("clientSeries")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty(true, true)]
		public List<LineChartSeries> ClientSeries
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000140EA File Offset: 0x000122EA
		[DefaultValue(null)]
		[Editor(typeof(ChartBaseSeriesEditor<LineChartSeries>), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		public List<LineChartSeries> Series
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000140F2 File Offset: 0x000122F2
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x000140FA File Offset: 0x000122FA
		[DefaultValue(LineChartType.Basic)]
		[ClientPropertyName("chartType")]
		[ExtenderControlProperty]
		public LineChartType ChartType { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00014103 File Offset: 0x00012303
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001410B File Offset: 0x0001230B
		[DefaultValue(9)]
		[ClientPropertyName("valueAxisLines")]
		[ExtenderControlProperty]
		public int ValueAxisLines { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00014114 File Offset: 0x00012314
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0001411C File Offset: 0x0001231C
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("valueAxisLineColor")]
		public string ValueAxisLineColor { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00014125 File Offset: 0x00012325
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0001412D File Offset: 0x0001232D
		[ClientPropertyName("categoryAxisLineColor")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string CategoryAxisLineColor { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00014136 File Offset: 0x00012336
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001413E File Offset: 0x0001233E
		[ClientPropertyName("baseLineColor")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string BaseLineColor { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00014147 File Offset: 0x00012347
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001414F File Offset: 0x0001234F
		[DefaultValue("#FFC652")]
		[ExtenderControlProperty]
		[ClientPropertyName("tooltipBackgroundColor")]
		public string TooltipBackgroundColor { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00014158 File Offset: 0x00012358
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00014160 File Offset: 0x00012360
		[ClientPropertyName("tooltipFontColor")]
		[ExtenderControlProperty]
		[DefaultValue("#0E426C")]
		public string TooltipFontColor { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00014169 File Offset: 0x00012369
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00014171 File Offset: 0x00012371
		[DefaultValue("#B85B3E")]
		[ClientPropertyName("tooltipBorderColor")]
		[ExtenderControlProperty]
		public string TooltipBorderColor { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001417A File Offset: 0x0001237A
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00014182 File Offset: 0x00012382
		[ClientPropertyName("areaDataLabel")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string AreaDataLabel { get; set; }

		// Token: 0x06000781 RID: 1921 RVA: 0x0001418C File Offset: 0x0001238C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.IsDesignMode)
			{
				return;
			}
			foreach (LineChartSeries lineChartSeries in this.Series)
			{
				if (string.IsNullOrWhiteSpace(lineChartSeries.Name))
				{
					throw new Exception("Name is missing in the LineChartSeries. Please provide a name in the LineChartSeries.");
				}
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00014200 File Offset: 0x00012400
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.ID = "_ParentDiv";
			htmlGenericControl.Attributes.Add("style", "border-style:solid; border-width:1px;");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script>");
			stringBuilder.Append("function init(evt) { ");
			stringBuilder.Append("    if ( window.svgDocument == null ) { ");
			stringBuilder.Append("        gDocument = evt.target.ownerDocument;");
			stringBuilder.Append("    } ");
			stringBuilder.Append("} ");
			stringBuilder.Append("function ShowTooltip(me, evt, data, areaDataLabel) { ");
			stringBuilder.Append(string.Format("    var tooltipDiv = document.getElementById('{0}_tooltipDiv');", this.ClientID));
			stringBuilder.Append("    tooltipDiv.innerHTML = String.format('{0}{1}', data, areaDataLabel) ;");
			stringBuilder.Append("    tooltipDiv.style.top = evt.pageY - 25 + 'px';");
			stringBuilder.Append("    tooltipDiv.style.left = evt.pageX + 20 + 'px';");
			stringBuilder.Append("    tooltipDiv.style.visibility = 'visible';");
			stringBuilder.Append("    me.style.strokeWidth = '5';");
			stringBuilder.Append("} ");
			stringBuilder.Append("function HideTooltip(me, evt) { ");
			stringBuilder.Append(string.Format("    var tooltipDiv = document.getElementById('{0}_tooltipDiv');", this.ClientID));
			stringBuilder.Append("    tooltipDiv.innerHTML = '';");
			stringBuilder.Append("    tooltipDiv.style.visibility = 'hidden';");
			stringBuilder.Append("    me.style.strokeWidth = '2';");
			stringBuilder.Append("} ");
			stringBuilder.Append("</script>");
			htmlGenericControl.InnerHtml = stringBuilder.ToString();
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x04000310 RID: 784
		private List<LineChartSeries> _series = new List<LineChartSeries>();
	}
}
