using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;
using Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000503 RID: 1283
	public class AreaSeries : MarkerSeriesWithLine, IStackedSeries
	{
		// Token: 0x06002DDD RID: 11741 RVA: 0x000966F7 File Offset: 0x000948F7
		public AreaSeries()
		{
			this.sType = SeriesType.Area;
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x00096706 File Offset: 0x00094906
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldY != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x00096745 File Offset: 0x00094945
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

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x00096760 File Offset: 0x00094960
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x00096781 File Offset: 0x00094981
		[DefaultValue(MissingValuesBehavior.Zero)]
		public override MissingValuesBehavior MissingValues
		{
			get
			{
				return (MissingValuesBehavior)(base.ViewState["MissingValues"] ?? MissingValuesBehavior.Zero);
			}
			set
			{
				base.ViewState["MissingValues"] = value;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x00096799 File Offset: 0x00094999
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000967B0 File Offset: 0x000949B0
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

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000967C8 File Offset: 0x000949C8
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000967E9 File Offset: 0x000949E9
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

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00096804 File Offset: 0x00094A04
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x00096830 File Offset: 0x00094A30
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0009685E File Offset: 0x00094A5E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x00096874 File Offset: 0x00094A74
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(base.Serialize());
			this.SerializeStackProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000968C0 File Offset: 0x00094AC0
		private void SerializeStackProperties(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			HtmlChartHelper.AddComma(sb);
			if (this.Stacked != null)
			{
				sb.AppendFormat("stack:{0}", this.Stacked.ToString().ToLower());
				HtmlChartHelper.AddComma(sb);
				if (this.Stacked == true)
				{
					sb.Append("stack:{");
					this.SerializeStackType(sb);
					HtmlChartHelper.RemoveEndingComma(sb);
					sb.Append("}");
					HtmlChartHelper.AddComma(sb);
				}
			}
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x00096960 File Offset: 0x00094B60
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

		// Token: 0x06002DEC RID: 11756 RVA: 0x000969B8 File Offset: 0x00094BB8
		internal override void AddSerializedItems(StringBuilder sb)
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
				sb.Append((seriesItem.YValue != null) ? (base.GetSerializedField(seriesItem.YValue.ToString()) + ",") : "null,");
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x00096A9C File Offset: 0x00094C9C
		internal void AddSeriesItems(StringBuilder sb)
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

		// Token: 0x06002DEE RID: 11758 RVA: 0x00096BD0 File Offset: 0x00094DD0
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x00096BE4 File Offset: 0x00094DE4
		internal override void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			this.SerializeLine(sb);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00096BF4 File Offset: 0x00094DF4
		protected override void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",line:{{{0}}},", base.LineAppearance.Serialize());
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x00096C0D File Offset: 0x00094E0D
		protected internal override void SerializeMissingValues(StringBuilder sb)
		{
			if (this.MissingValues != MissingValuesBehavior.Zero)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.AppendFormat(",missingValues:'{0}'", this.MissingValues.ToString().ToLower());
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00096DE0 File Offset: 0x00094FE0
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				CategorySeriesItem item = (CategorySeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x00096DFD File Offset: 0x00094FFD
		internal override SeriesItemBase GetSeriesItem()
		{
			return new CategorySeriesItem();
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x00096E04 File Offset: 0x00095004
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x00096E14 File Offset: 0x00095014
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			CategorySeriesItem categorySeriesItem = seriesItem as CategorySeriesItem;
			if (categorySeriesItem != null)
			{
				this.SeriesItems.Add(categorySeriesItem);
			}
		}

		// Token: 0x04000C3E RID: 3134
		private CategorySeriesItemCollection _categorySeriesItems;
	}
}
