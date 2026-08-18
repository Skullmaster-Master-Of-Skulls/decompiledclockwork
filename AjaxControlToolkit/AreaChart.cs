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
	// Token: 0x02000037 RID: 55
	[ClientCssResource("AreaChart")]
	[ToolboxBitmap(typeof(Accessor), "AreaChart.bmp")]
	[ClientScriptResource("Sys.Extended.UI.AreaChart", "AreaChart")]
	public class AreaChart : ChartBase
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006FF0 File Offset: 0x000051F0
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00006FD8 File Offset: 0x000051D8
		[ExtenderControlProperty]
		[ClientPropertyName("displayValues")]
		[DefaultValue(true)]
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007019 File Offset: 0x00005219
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007021 File Offset: 0x00005221
		[DefaultValue("")]
		[ClientPropertyName("categoriesAxis")]
		[ExtenderControlProperty]
		public string CategoriesAxis { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000702A File Offset: 0x0000522A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ExtenderControlProperty(true, true)]
		[ClientPropertyName("clientSeries")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public List<AreaChartSeries> ClientSeries
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007032 File Offset: 0x00005232
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor(typeof(ChartBaseSeriesEditor<AreaChartSeries>), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public List<AreaChartSeries> Series
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000703A File Offset: 0x0000523A
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007042 File Offset: 0x00005242
		[DefaultValue(AreaChartType.Basic)]
		[ClientPropertyName("chartType")]
		[ExtenderControlProperty]
		public AreaChartType ChartType { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000704B File Offset: 0x0000524B
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00007053 File Offset: 0x00005253
		[ClientPropertyName("valueAxisLines")]
		[DefaultValue(9)]
		[ExtenderControlProperty]
		public int ValueAxisLines { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000705C File Offset: 0x0000525C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00007064 File Offset: 0x00005264
		[ClientPropertyName("valueAxisLineColor")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string ValueAxisLineColor { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000706D File Offset: 0x0000526D
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00007075 File Offset: 0x00005275
		[ClientPropertyName("categoryAxisLineColor")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string CategoryAxisLineColor { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000707E File Offset: 0x0000527E
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00007086 File Offset: 0x00005286
		[ClientPropertyName("baseLineColor")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string BaseLineColor { get; set; }

		// Token: 0x060001F6 RID: 502 RVA: 0x00007090 File Offset: 0x00005290
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.IsDesignMode)
			{
				return;
			}
			foreach (AreaChartSeries areaChartSeries in this.Series)
			{
				if (string.IsNullOrWhiteSpace(areaChartSeries.Name))
				{
					throw new Exception("Name is missing in the AreaChartSeries. Please provide a name in the AreaChartSeries.");
				}
			}
		}

		// Token: 0x04000099 RID: 153
		private List<AreaChartSeries> _series = new List<AreaChartSeries>();
	}
}
