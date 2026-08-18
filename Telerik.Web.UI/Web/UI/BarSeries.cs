using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000505 RID: 1285
	public class BarSeries : SeriesBase, ISpacedSeries, IStackedSeries, IGroupableSeries
	{
		// Token: 0x06002DFF RID: 11775 RVA: 0x00096F0C File Offset: 0x0009510C
		public BarSeries()
		{
			this.sType = SeriesType.Bar;
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x00096F1B File Offset: 0x0009511B
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldY != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x00096F5A File Offset: 0x0009515A
		// (set) Token: 0x06002E02 RID: 11778 RVA: 0x00096F71 File Offset: 0x00095171
		[DefaultValue(null)]
		public bool? Stacked
		{
			get
			{
				return (bool?)base.ViewState["Stacked"];
			}
			set
			{
				base.ViewState["Stacked"] = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00096F89 File Offset: 0x00095189
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00096FAA File Offset: 0x000951AA
		[DefaultValue(HtmlChartStackType.Normal)]
		public HtmlChartStackType StackType
		{
			get
			{
				return (HtmlChartStackType)(base.ViewState["StackType"] ?? HtmlChartStackType.Normal);
			}
			set
			{
				base.ViewState["StackType"] = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00096FC2 File Offset: 0x000951C2
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x00096FE2 File Offset: 0x000951E2
		[DefaultValue("")]
		public string GroupName
		{
			get
			{
				return (string)(base.ViewState["GroupName"] ?? string.Empty);
			}
			set
			{
				base.ViewState["GroupName"] = value;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x00096FF5 File Offset: 0x000951F5
		[DefaultValue("LabelsAppearance")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series labels visual settings")]
		public BarColumnSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new BarColumnSeriesLabelsAppearance("bla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0009701B File Offset: 0x0009521B
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SeriesBorderAppearance BorderAppearance
		{
			get
			{
				if (this._borderAppearance == null)
				{
					this._borderAppearance = new SeriesBorderAppearance();
				}
				return this._borderAppearance;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x00097036 File Offset: 0x00095236
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CategorySeriesItemCollection SeriesItems
		{
			get
			{
				if (this._categorySeriesItems == null)
				{
					this._categorySeriesItems = new CategorySeriesItemCollection();
				}
				return this._categorySeriesItems;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x00097051 File Offset: 0x00095251
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x0009706D File Offset: 0x0009526D
		[DefaultValue(null)]
		public virtual double? Gap
		{
			get
			{
				return (double?)(base.ViewState["Gap"] ?? null);
			}
			set
			{
				base.ViewState["Gap"] = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x00097085 File Offset: 0x00095285
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x000970A1 File Offset: 0x000952A1
		[DefaultValue(null)]
		public virtual double? Spacing
		{
			get
			{
				return (double?)(base.ViewState["Spacing"] ?? null);
			}
			set
			{
				base.ViewState["Spacing"] = value;
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000970BC File Offset: 0x000952BC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000970E8 File Offset: 0x000952E8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00097116 File Offset: 0x00095316
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0009712C File Offset: 0x0009532C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.Append(base.Serialize());
			if (!this.IsDataBound)
			{
				this.AddSerializedItems(stringBuilder);
			}
			string text = this.LabelsAppearance.Serialize();
			if (text != string.Empty)
			{
				stringBuilder.Append(",").Append(text);
			}
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("border:").Append(value);
			}
			HtmlChartHelper.AddComma(stringBuilder);
			if (this.Gap != null)
			{
				stringBuilder.AppendFormat("gap:{0}", HtmlChartHelper.ToStringInvariant(this.Gap));
				HtmlChartHelper.AddComma(stringBuilder);
			}
			if (this.Spacing != null)
			{
				stringBuilder.AppendFormat("spacing:{0}", HtmlChartHelper.ToStringInvariant(this.Spacing));
				HtmlChartHelper.AddComma(stringBuilder);
			}
			this.SerializeStackProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00097240 File Offset: 0x00095440
		private void SerializeStackProperties(StringBuilder sb)
		{
			if (this.Stacked != null)
			{
				sb.AppendFormat("stack:{0}", this.Stacked.ToString().ToLower());
				HtmlChartHelper.AddComma(sb);
			}
			if (!string.IsNullOrEmpty(this.GroupName))
			{
				sb.Append("stack:{");
				if (!string.IsNullOrEmpty(this.GroupName))
				{
					sb.AppendFormat("group:'{0}'", this.GroupName);
					HtmlChartHelper.AddComma(sb);
				}
				this.SerializeStackType(sb);
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append("}");
				HtmlChartHelper.AddComma(sb);
				return;
			}
			if (this.Stacked == true)
			{
				sb.Append("stack:{");
				this.SerializeStackType(sb);
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append("}");
				HtmlChartHelper.AddComma(sb);
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00097330 File Offset: 0x00095530
		private void SerializeStackType(StringBuilder sb)
		{
			if (this.StackType == HtmlChartStackType.Stack100)
			{
				sb.AppendFormat("type:'{0}'", "100%");
				HtmlChartHelper.AddComma(sb);
				return;
			}
			sb.AppendFormat("type:'{0}'", HtmlChartHelper.StringToLowerCamelCase(this.StackType.ToString()));
			HtmlChartHelper.AddComma(sb);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00097385 File Offset: 0x00095585
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0009739C File Offset: 0x0009559C
		protected void AddSerializedItems(StringBuilder sb)
		{
			if (this.SeriesItems.Count > 0)
			{
				this.AddSeriesItems(sb);
				return;
			}
			if (base.Items.Count == 0)
			{
				return;
			}
			sb.Append(", data: [");
			foreach (object obj in base.Items)
			{
				SeriesItem seriesItem = (SeriesItem)obj;
				sb.Append("{");
				sb.Append("value: ").Append((seriesItem.YValue != null) ? base.GetSerializedField(seriesItem.YValue.ToString()) : "null");
				if (seriesItem.BackgroundColor.A != 0)
				{
					sb.Append(",");
					string value = HtmlChartHelper.ColorToHex(seriesItem.BackgroundColor);
					base.SerializeNonEmptyProperty(sb, "color", value);
				}
				sb.Append("},");
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000974CC File Offset: 0x000956CC
		protected void AddSeriesItems(StringBuilder sb)
		{
			sb.Append(", data: [");
			foreach (object obj in this.SeriesItems)
			{
				CategorySeriesItem categorySeriesItem = (CategorySeriesItem)obj;
				sb.Append("{");
				sb.Append("value: ").Append((categorySeriesItem.Y != null) ? base.GetSerializedField(categorySeriesItem.Y.ToString()) : "null");
				if (categorySeriesItem.BackgroundColor.A != 0)
				{
					sb.Append(",");
					string value = HtmlChartHelper.ColorToHex(categorySeriesItem.BackgroundColor);
					base.SerializeNonEmptyProperty(sb, "color", value);
				}
				sb.Append("},");
			}
			if (sb.Length - 1 >= 0 && sb[sb.Length - 1] == ',')
			{
				sb.Remove(sb.Length - 1, 1);
			}
			sb.Append("]");
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000977A0 File Offset: 0x000959A0
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				CategorySeriesItem item = (CategorySeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000977BD File Offset: 0x000959BD
		internal override SeriesItemBase GetSeriesItem()
		{
			return new CategorySeriesItem();
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000977C4 File Offset: 0x000959C4
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000977D4 File Offset: 0x000959D4
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			CategorySeriesItem categorySeriesItem = seriesItem as CategorySeriesItem;
			if (categorySeriesItem != null)
			{
				this.SeriesItems.Add(categorySeriesItem);
			}
		}

		// Token: 0x04000C40 RID: 3136
		private BarColumnSeriesLabelsAppearance _labelsAppearance;

		// Token: 0x04000C41 RID: 3137
		private SeriesBorderAppearance _borderAppearance;

		// Token: 0x04000C42 RID: 3138
		private CategorySeriesItemCollection _categorySeriesItems;
	}
}
