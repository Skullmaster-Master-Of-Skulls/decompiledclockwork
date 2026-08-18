using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020003C9 RID: 969
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class SeriesBase : StateManager
	{
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000765B4 File Offset: 0x000747B4
		// (set) Token: 0x06002371 RID: 9073 RVA: 0x000765DA File Offset: 0x000747DA
		[DefaultValue(1)]
		public decimal Opacity
		{
			get
			{
				return (decimal)(base.ViewState["Opacity"] ?? 1m);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000765F4 File Offset: 0x000747F4
		internal virtual bool IsDataBound
		{
			get
			{
				return this.Items.Count == 0 && (this.DataFieldX != string.Empty || this.DataFieldY != string.Empty || this.DataFieldSize != string.Empty || this.DataFieldTooltip != string.Empty || this.Data != string.Empty);
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0007666A File Offset: 0x0007486A
		public SeriesType Type
		{
			get
			{
				return this.sType;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00076672 File Offset: 0x00074872
		// (set) Token: 0x06002375 RID: 9077 RVA: 0x00076692 File Offset: 0x00074892
		[DefaultValue("")]
		internal string Data
		{
			get
			{
				return (string)(base.ViewState["Data"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Data"] = value;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000766A5 File Offset: 0x000748A5
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x000766C5 File Offset: 0x000748C5
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000766D8 File Offset: 0x000748D8
		// (set) Token: 0x06002379 RID: 9081 RVA: 0x000766F9 File Offset: 0x000748F9
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00076711 File Offset: 0x00074911
		// (set) Token: 0x0600237B RID: 9083 RVA: 0x00076732 File Offset: 0x00074932
		[DefaultValue(true)]
		public bool VisibleInLegend
		{
			get
			{
				return (bool)(base.ViewState["VisibleInLegend"] ?? true);
			}
			set
			{
				base.ViewState["VisibleInLegend"] = value;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x0007674A File Offset: 0x0007494A
		// (set) Token: 0x0600237D RID: 9085 RVA: 0x0007676A File Offset: 0x0007496A
		[DefaultValue("")]
		public virtual string AxisName
		{
			get
			{
				return ((string)base.ViewState["AxisName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["AxisName"] = value;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x0007677D File Offset: 0x0007497D
		// (set) Token: 0x0600237F RID: 9087 RVA: 0x00076785 File Offset: 0x00074985
		[Obsolete("This property is becoming obsolete in favor of the DataFieldY. Therefore, please, use DataFieldY instead.", false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string DataField
		{
			get
			{
				return this.DataFieldY;
			}
			set
			{
				this.DataFieldY = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x0007678E File Offset: 0x0007498E
		// (set) Token: 0x06002381 RID: 9089 RVA: 0x000767AE File Offset: 0x000749AE
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string DataFieldX
		{
			get
			{
				return (string)(base.ViewState["DataFieldX"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldX"] = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000767C1 File Offset: 0x000749C1
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x000767E1 File Offset: 0x000749E1
		[DefaultValue("")]
		public string DataFieldY
		{
			get
			{
				return (string)(base.ViewState["DataFieldY"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldY"] = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000767F4 File Offset: 0x000749F4
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x00076814 File Offset: 0x00074A14
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataFieldSize
		{
			get
			{
				return (string)(base.ViewState["DataFieldSize"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldSize"] = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00076827 File Offset: 0x00074A27
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x00076847 File Offset: 0x00074A47
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataFieldTooltip
		{
			get
			{
				return (string)(base.ViewState["DataFieldTooltip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFieldTooltip"] = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0007685A File Offset: 0x00074A5A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SeriesItemCollection Items
		{
			get
			{
				if (this._seriesItems == null)
				{
					this._seriesItems = new SeriesItemCollection();
				}
				return this._seriesItems;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x00076875 File Offset: 0x00074A75
		[Description("Series visual settings")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("Appearance")]
		public SeriesAppearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new SeriesAppearance(base.ViewState);
				}
				return this._appearance;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00076896 File Offset: 0x00074A96
		[Category("Appearance")]
		[Description("Tooltips visual settings")]
		[DefaultValue("TooltipsAppearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SeriesTooltipsAppearance TooltipsAppearance
		{
			get
			{
				if (this._tooltipsAppearance == null)
				{
					this._tooltipsAppearance = new SeriesTooltipsAppearance("sta", base.ViewState);
				}
				return this._tooltipsAppearance;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000768BC File Offset: 0x00074ABC
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x000768DC File Offset: 0x00074ADC
		[DefaultValue("")]
		public string ColorField
		{
			get
			{
				return (string)(base.ViewState["ColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ColorField"] = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000768EF File Offset: 0x00074AEF
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x0007690B File Offset: 0x00074B0B
		[DefaultValue(null)]
		public int? ZIndex
		{
			get
			{
				return (int?)(base.ViewState["ZIndex"] ?? null);
			}
			set
			{
				base.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x00076923 File Offset: 0x00074B23
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Highlight visual settings")]
		[DefaultValue("HighlightAppearance")]
		public SeriesHighlightAppearance HighlightAppearance
		{
			get
			{
				if (this._highlightAppearance == null)
				{
					this._highlightAppearance = new SeriesHighlightAppearance("sha", base.ViewState);
				}
				return this._highlightAppearance;
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0007694C File Offset: 0x00074B4C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00076978 File Offset: 0x00074B78
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000769A6 File Offset: 0x00074BA6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000769BC File Offset: 0x00074BBC
		internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.SerializeSeriesType(stringBuilder);
			HtmlChartHelper.AddComma(stringBuilder);
			stringBuilder.Append(this.Appearance.Serialize());
			HtmlChartHelper.AddComma(stringBuilder);
			if (this.IsDataBound)
			{
				if (this.Items.Count == 0 && this.Data != string.Empty)
				{
					stringBuilder.Append("data: ").Append(this.Data);
				}
				else
				{
					this.SerializeDataboundFields(stringBuilder);
				}
			}
			HtmlChartHelper.AddComma(stringBuilder);
			this.SerializeNonEmptyProperty(stringBuilder, "name", this.Name);
			if (!this.Visible)
			{
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("visible: false");
			}
			if (!this.VisibleInLegend)
			{
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("visibleInLegend: false");
			}
			if (this.Opacity != 1m)
			{
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.AppendFormat("opacity:{0}", this.Opacity);
			}
			HtmlChartHelper.AddComma(stringBuilder);
			this.SerializeAxisProperty(stringBuilder);
			HtmlChartHelper.AddComma(stringBuilder);
			stringBuilder.Append(this.TooltipsAppearance.Serialize());
			stringBuilder.Append(this.HighlightAppearance.Serialize());
			if (this.IsDataBound && !string.IsNullOrEmpty(this.ColorField))
			{
				HtmlChartHelper.AddComma(stringBuilder);
				this.SerializeNonEmptyProperty(stringBuilder, "colorField", this.ColorField);
			}
			if (this.ZIndex != null)
			{
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.AppendFormat("{0}: {1}", "zIndex", this.ZIndex);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00076B53 File Offset: 0x00074D53
		internal virtual void SerializeDataboundFields(StringBuilder sb)
		{
			sb.Append("field: '").Append(this.DataFieldY).Append("'");
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00076B78 File Offset: 0x00074D78
		internal string GetSerializedField(string val)
		{
			decimal num;
			if (decimal.TryParse(val, out num))
			{
				return num.ToString(CultureInfo.InvariantCulture);
			}
			return string.Format("\"{0}\"", num);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00076BAC File Offset: 0x00074DAC
		protected void SerializeNonEmptyProperty(StringBuilder sb, string name, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.SerializeProperty(sb, name, value);
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00076BBF File Offset: 0x00074DBF
		protected void SerializeProperty(StringBuilder sb, string name, object value)
		{
			sb.AppendFormat("{0}: '{1}'", name, value);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00076BCF File Offset: 0x00074DCF
		internal virtual void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00076BD1 File Offset: 0x00074DD1
		protected internal virtual void SerializeSeriesType(StringBuilder sb)
		{
			sb.AppendFormat("type:'{0}',", HtmlChartHelper.StringToLowerCamelCase(this.Type.ToString()));
		}

		// Token: 0x0600239A RID: 9114
		internal abstract SeriesItemBase GetSeriesItem();

		// Token: 0x0600239B RID: 9115
		internal abstract void ClearSeriesItems();

		// Token: 0x0600239C RID: 9116
		internal abstract void AddSeriesItem(SeriesItemBase seriesItem);

		// Token: 0x0600239D RID: 9117
		internal abstract IEnumerable<SeriesItemBase> GetSeriesItems();

		// Token: 0x0600239E RID: 9118 RVA: 0x00076BF4 File Offset: 0x00074DF4
		internal virtual void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
		}

		// Token: 0x04000949 RID: 2377
		protected SeriesType sType;

		// Token: 0x0400094A RID: 2378
		private SeriesItemCollection _seriesItems;

		// Token: 0x0400094B RID: 2379
		private SeriesAppearance _appearance;

		// Token: 0x0400094C RID: 2380
		private SeriesTooltipsAppearance _tooltipsAppearance;

		// Token: 0x0400094D RID: 2381
		private SeriesHighlightAppearance _highlightAppearance;
	}
}
