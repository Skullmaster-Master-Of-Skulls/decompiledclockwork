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
	// Token: 0x02000508 RID: 1288
	public class LineSeries : MarkerSeriesWithLine, IStackedSeries
	{
		// Token: 0x06002E20 RID: 11808 RVA: 0x00097830 File Offset: 0x00095A30
		public LineSeries()
		{
			this.sType = SeriesType.Line;
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0009783F File Offset: 0x00095A3F
		internal override bool IsDataBound
		{
			get
			{
				return base.IsDataBound && this.SeriesItems.Count == 0 && (base.DataFieldY != string.Empty || base.Data != string.Empty);
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x0009787E File Offset: 0x00095A7E
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x00097895 File Offset: 0x00095A95
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

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000978AD File Offset: 0x00095AAD
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x000978CE File Offset: 0x00095ACE
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

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000978E6 File Offset: 0x00095AE6
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

		// Token: 0x06002E27 RID: 11815 RVA: 0x00097904 File Offset: 0x00095B04
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.SeriesItems).LoadViewState(array[1]);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00097930 File Offset: 0x00095B30
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SeriesItems).SaveViewState()
			};
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x0009795E File Offset: 0x00095B5E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SeriesItems).TrackViewState();
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x00097974 File Offset: 0x00095B74
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

		// Token: 0x06002E2B RID: 11819 RVA: 0x000979C0 File Offset: 0x00095BC0
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

		// Token: 0x06002E2C RID: 11820 RVA: 0x00097A60 File Offset: 0x00095C60
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

		// Token: 0x06002E2D RID: 11821 RVA: 0x00097AB8 File Offset: 0x00095CB8
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
				if (seriesItem.YValue != null)
				{
					sb.Append(base.GetSerializedField(seriesItem.YValue.ToString()));
				}
				else
				{
					sb.Append("null");
				}
				sb.Append(",");
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x00097BA4 File Offset: 0x00095DA4
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "axis", this.AxisName);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x00097BB8 File Offset: 0x00095DB8
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

		// Token: 0x06002E30 RID: 11824 RVA: 0x00097CEC File Offset: 0x00095EEC
		internal override void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			this.SerializeLine(sb);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x00097CFC File Offset: 0x00095EFC
		protected override void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",{0}", base.LineAppearance.Serialize());
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x00097EB8 File Offset: 0x000960B8
		internal override IEnumerable<SeriesItemBase> GetSeriesItems()
		{
			foreach (object obj in this.SeriesItems)
			{
				CategorySeriesItem item = (CategorySeriesItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x00097ED5 File Offset: 0x000960D5
		internal override SeriesItemBase GetSeriesItem()
		{
			return new CategorySeriesItem();
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00097EDC File Offset: 0x000960DC
		internal override void ClearSeriesItems()
		{
			this.SeriesItems.Clear();
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00097EEC File Offset: 0x000960EC
		internal override void AddSeriesItem(SeriesItemBase seriesItem)
		{
			CategorySeriesItem categorySeriesItem = seriesItem as CategorySeriesItem;
			if (categorySeriesItem != null)
			{
				this.SeriesItems.Add(categorySeriesItem);
			}
		}

		// Token: 0x04000C43 RID: 3139
		private CategorySeriesItemCollection _categorySeriesItems;
	}
}
