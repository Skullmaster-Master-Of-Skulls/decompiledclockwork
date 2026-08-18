using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000046 RID: 70
	[ClientScriptResource("Sys.Extended.UI.BarChart", "BarChart")]
	[ToolboxBitmap(typeof(Accessor), "BarChart.bmp")]
	[ClientCssResource("BarChart")]
	public class BarChart : ChartBase
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000087A8 File Offset: 0x000069A8
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000087B0 File Offset: 0x000069B0
		[DefaultValue("")]
		[ClientPropertyName("categoriesAxis")]
		[ExtenderControlProperty]
		public string CategoriesAxis { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000087B9 File Offset: 0x000069B9
		[Browsable(false)]
		[ClientPropertyName("clientSeries")]
		[ExtenderControlProperty(true, true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public List<BarChartSeries> ClientSeries
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000087C1 File Offset: 0x000069C1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Editor(typeof(ChartBaseSeriesEditor<BarChartSeries>), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public List<BarChartSeries> Series
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000087C9 File Offset: 0x000069C9
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000087D1 File Offset: 0x000069D1
		[ClientPropertyName("chartType")]
		[ExtenderControlProperty]
		[DefaultValue(BarChartType.Column)]
		public BarChartType ChartType { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000087DA File Offset: 0x000069DA
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000087E2 File Offset: 0x000069E2
		[ExtenderControlProperty]
		[ClientPropertyName("valueAxisLines")]
		[DefaultValue(9)]
		public int ValueAxisLines { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000087EB File Offset: 0x000069EB
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000087F3 File Offset: 0x000069F3
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("valueAxisLineColor")]
		public string ValueAxisLineColor { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00008804 File Offset: 0x00006A04
		[ExtenderControlProperty]
		[ClientPropertyName("categoryAxisLineColor")]
		[DefaultValue("")]
		public string CategoryAxisLineColor { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000880D File Offset: 0x00006A0D
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00008815 File Offset: 0x00006A15
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("baseLineColor")]
		public string BaseLineColor { get; set; }

		// Token: 0x0600025B RID: 603 RVA: 0x00008820 File Offset: 0x00006A20
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.IsDesignMode)
			{
				return;
			}
			foreach (BarChartSeries barChartSeries in this.Series)
			{
				if (string.IsNullOrWhiteSpace(barChartSeries.Name))
				{
					throw new Exception("Name is missing the BarChartSeries. Please provide a name in the BarChartSeries.");
				}
			}
		}

		// Token: 0x040000C7 RID: 199
		private List<BarChartSeries> _series = new List<BarChartSeries>();
	}
}
