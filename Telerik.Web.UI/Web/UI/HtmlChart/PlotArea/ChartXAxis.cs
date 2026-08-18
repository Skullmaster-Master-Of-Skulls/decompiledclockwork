using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Axes.BaseUnitSteps;
using Telerik.Web.UI.HtmlChart.PlotArea.Axes;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004D0 RID: 1232
	public class ChartXAxis : AxisBase
	{
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x00092F49 File Offset: 0x00091149
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x00092F69 File Offset: 0x00091169
		[DefaultValue("")]
		public string DataLabelsField
		{
			get
			{
				return (string)(base.ViewState["DataLabelsField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataLabelsField"] = value;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x00092F7C File Offset: 0x0009117C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AxisItemCollection Items
		{
			get
			{
				if (this._axisItems == null)
				{
					this._axisItems = new AxisItemCollection();
				}
				return this._axisItems;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x00092F97 File Offset: 0x00091197
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x00092FB8 File Offset: 0x000911B8
		[DefaultValue(AxisType.Auto)]
		public AxisType Type
		{
			get
			{
				return (AxisType)(base.ViewState["Type"] ?? AxisType.Auto);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x00092FD0 File Offset: 0x000911D0
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x00092FEC File Offset: 0x000911EC
		[DefaultValue(null)]
		public DateTime? MinDateValue
		{
			get
			{
				return (DateTime?)(base.ViewState["MinDateValue"] ?? null);
			}
			set
			{
				base.ViewState["MinDateValue"] = value;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x00093004 File Offset: 0x00091204
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x00093020 File Offset: 0x00091220
		[DefaultValue(null)]
		public DateTime? MaxDateValue
		{
			get
			{
				return (DateTime?)(base.ViewState["MaxDateValue"] ?? null);
			}
			set
			{
				base.ViewState["MaxDateValue"] = value;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x00093038 File Offset: 0x00091238
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x00093059 File Offset: 0x00091259
		[DefaultValue(DateTimeBaseUnit.Auto)]
		public DateTimeBaseUnit BaseUnit
		{
			get
			{
				return (DateTimeBaseUnit)(base.ViewState["BaseUnit"] ?? DateTimeBaseUnit.Auto);
			}
			set
			{
				base.ViewState["BaseUnit"] = value;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x00093071 File Offset: 0x00091271
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x00093093 File Offset: 0x00091293
		[DefaultValue(10)]
		public int MaxDateGroups
		{
			get
			{
				return (int)(base.ViewState["MaxDateGroups"] ?? 10);
			}
			set
			{
				base.ViewState["MaxDateGroups"] = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000930AB File Offset: 0x000912AB
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000930CC File Offset: 0x000912CC
		[DefaultValue(1)]
		public int BaseUnitStep
		{
			get
			{
				return (int)(base.ViewState["baseUnitStep"] ?? 1);
			}
			set
			{
				base.ViewState["baseUnitStep"] = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000930E4 File Offset: 0x000912E4
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x00093105 File Offset: 0x00091305
		[DefaultValue(false)]
		public bool EnableBaseUnitStepAuto
		{
			get
			{
				return (bool)(base.ViewState["enableBaseUnitStepAuto"] ?? false);
			}
			set
			{
				base.ViewState["enableBaseUnitStepAuto"] = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x0009311D File Offset: 0x0009131D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BaseUnitSteps AutoBaseUnitSteps
		{
			get
			{
				if (this._autoBaseUnitSteps == null)
				{
					this._autoBaseUnitSteps = new BaseUnitSteps();
				}
				return this._autoBaseUnitSteps;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00093138 File Offset: 0x00091338
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AxisCrossingPointsCollection AxisCrossingPoints
		{
			get
			{
				if (this._axisCrossingPoints == null)
				{
					this._axisCrossingPoints = new AxisCrossingPointsCollection();
				}
				return this._axisCrossingPoints;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x00093153 File Offset: 0x00091353
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x00093179 File Offset: 0x00091379
		[DefaultValue(0)]
		public int? StartAngle
		{
			get
			{
				return new int?((int)(base.ViewState["StartAngle"] ?? 0));
			}
			set
			{
				base.ViewState["StartAngle"] = value;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00093191 File Offset: 0x00091391
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x000931A8 File Offset: 0x000913A8
		[DefaultValue(null)]
		public bool? Justified
		{
			get
			{
				return (bool?)base.ViewState["Justified"];
			}
			set
			{
				base.ViewState["Justified"] = value;
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000931C0 File Offset: 0x000913C0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000931EC File Offset: 0x000913EC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0009321A File Offset: 0x0009141A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x00093230 File Offset: 0x00091430
		internal override string Serialize()
		{
			if (base.PlotType == PlotType.Pie || base.PlotType == PlotType.Funnel)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((base.PlotType == PlotType.Numeric || base.PlotType == PlotType.Polar) ? "xAxis: {" : "categoryAxis: {");
			stringBuilder.Append(base.Serialize());
			if (this.Justified != null)
			{
				stringBuilder.Append(",justified: ").Append(this.Justified.ToString().ToLowerInvariant());
			}
			if (this.Type != AxisType.Auto)
			{
				stringBuilder.Append(",type: '").Append(this.Type.ToString().ToLower()).Append("'");
			}
			if (this.BaseUnit != DateTimeBaseUnit.Auto)
			{
				stringBuilder.Append(",baseUnit: '").Append(this.BaseUnit.ToString().ToLower()).Append("'");
			}
			if (this.MaxDateGroups != 10)
			{
				stringBuilder.AppendFormat(",maxDateGroups: {0}", this.MaxDateGroups);
			}
			if (this.BaseUnitStep > 1 && !this.EnableBaseUnitStepAuto)
			{
				stringBuilder.AppendFormat(",baseUnitStep: {0}", this.BaseUnitStep);
			}
			else if (this.EnableBaseUnitStepAuto)
			{
				stringBuilder.Append(",baseUnitStep: 'auto'");
			}
			if (!this.AutoBaseUnitSteps.IsDefault && (this.BaseUnit.Equals(DateTimeBaseUnit.Fit) || this.EnableBaseUnitStepAuto))
			{
				stringBuilder.AppendFormat(",autoBaseUnitSteps: {0}", this.AutoBaseUnitSteps.Serialize());
			}
			if (this.StartAngle != null && this.StartAngle != 0)
			{
				stringBuilder.Append(",startAngle: ").Append(this.StartAngle.ToString());
			}
			if (this.Items.Count > 0 && base.PlotType != PlotType.Numeric && base.PlotType != PlotType.Polar)
			{
				this._shouldDataBind = false;
				stringBuilder.Append(", categories: [");
				decimal value = 0m;
				foreach (object obj in this.Items)
				{
					AxisItem axisItem = (AxisItem)obj;
					string labelText = axisItem.LabelText;
					bool flag = decimal.TryParse(labelText, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
					if (flag && base.LabelsAppearance.DataFormatString != string.Empty)
					{
						stringBuilder.Append(HtmlChartHelper.ToStringInvariant(new decimal?(value)));
					}
					else
					{
						DateTime value2 = default(DateTime);
						bool flag2 = DateTime.TryParse(labelText, out value2);
						if ((!flag2 && !flag) || base.LabelsAppearance.DataFormatString == string.Empty)
						{
							stringBuilder.Append("'").Append(labelText).Append("'");
						}
						else
						{
							stringBuilder.Append(flag2 ? HtmlChartHelper.GetSerializedDate(new DateTime?(value2)) : labelText);
						}
					}
					stringBuilder.Append(",");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]");
			}
			else if (this.DataLabelsField != string.Empty)
			{
				stringBuilder.Append(", field:'").Append(this.DataLabelsField).Append("'");
				this.IsDataBound = true;
			}
			stringBuilder.Append(this.SerializeAxisScaling());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.AppendFormat(",{0}", this.AxisCrossingPoints.Serialize());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0009362C File Offset: 0x0009182C
		protected override string SerializeAxisScaling()
		{
			if (base.PlotType == PlotType.Numeric)
			{
				StringBuilder stringBuilder = new StringBuilder(base.SerializeAxisScaling());
				if (this.MinDateValue != null)
				{
					DateTime value = this.MinDateValue.Value;
					stringBuilder.Append(", min: ").Append(HtmlChartHelper.GetSerializedDate(this.MinDateValue));
				}
				if (this.MaxDateValue != null)
				{
					DateTime value2 = this.MaxDateValue.Value;
					stringBuilder.Append(", max: ").Append(HtmlChartHelper.GetSerializedDate(this.MaxDateValue));
				}
				return stringBuilder.ToString();
			}
			if (base.PlotType == PlotType.Categorial && this.Type != AxisType.Date)
			{
				return base.SerializeAxisScaling();
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (this.Type == AxisType.Date && this.MinDateValue != null && base.PlotType != PlotType.Radar && base.PlotType != PlotType.Polar)
			{
				DateTime value3 = this.MinDateValue.Value;
				stringBuilder2.Append(", min: ").Append(HtmlChartHelper.GetSerializedDate(this.MinDateValue));
			}
			if (this.Type == AxisType.Date && this.MaxDateValue != null && base.PlotType != PlotType.Radar && base.PlotType != PlotType.Polar)
			{
				DateTime value4 = this.MaxDateValue.Value;
				stringBuilder2.Append(", max: ").Append(HtmlChartHelper.GetSerializedDate(this.MaxDateValue));
			}
			if (base.Step != null)
			{
				stringBuilder2.Append(", majorUnit: ").Append(HtmlChartHelper.ToStringInvariant(base.Step));
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x04000B88 RID: 2952
		internal bool _shouldDataBind = true;

		// Token: 0x04000B89 RID: 2953
		private AxisItemCollection _axisItems;

		// Token: 0x04000B8A RID: 2954
		private BaseUnitSteps _autoBaseUnitSteps;

		// Token: 0x04000B8B RID: 2955
		private AxisCrossingPointsCollection _axisCrossingPoints;
	}
}
