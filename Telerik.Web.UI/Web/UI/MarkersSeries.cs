using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI
{
	// Token: 0x020004FC RID: 1276
	public abstract class MarkersSeries : SeriesBase
	{
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00095DAF File Offset: 0x00093FAF
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x00095DD0 File Offset: 0x00093FD0
		[DefaultValue(MissingValuesBehavior.Interpolate)]
		public virtual MissingValuesBehavior MissingValues
		{
			get
			{
				return (MissingValuesBehavior)(base.ViewState["MissingValues"] ?? MissingValuesBehavior.Interpolate);
			}
			set
			{
				base.ViewState["MissingValues"] = value;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00095DE8 File Offset: 0x00093FE8
		[DefaultValue("MarkerssAppearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series markers visual settings")]
		[Category("Appearance")]
		public virtual MarkersAppearance MarkersAppearance
		{
			get
			{
				if (this._markersAppearance == null)
				{
					this._markersAppearance = new MarkersAppearance("ma", base.ViewState);
				}
				return this._markersAppearance;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x00095E0E File Offset: 0x0009400E
		[Category("Appearance")]
		[Description("Series labels visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("LabelsAppearance")]
		public LineAndScatterLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new LineAndScatterLabelsAppearance("lla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x00095E34 File Offset: 0x00094034
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder().Append(base.Serialize());
			this.SerializeMissingValues(stringBuilder);
			if (!this.IsDataBound)
			{
				this.AddSerializedItems(stringBuilder);
			}
			string text = this.LabelsAppearance.Serialize();
			if (text != string.Empty)
			{
				stringBuilder.Append(",").Append(text);
			}
			string text2 = this.MarkersAppearance.Serialize();
			if (text2 != string.Empty)
			{
				stringBuilder.Append(",").Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x00095EC4 File Offset: 0x000940C4
		protected internal virtual void SerializeMissingValues(StringBuilder sb)
		{
			if (this.MissingValues != MissingValuesBehavior.Interpolate)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.AppendFormat(",missingValues:'{0}'", this.MissingValues.ToString().ToLower());
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x00095EF7 File Offset: 0x000940F7
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
			base.SerializeNonEmptyProperty(sb, "yAxis", this.AxisName);
		}

		// Token: 0x06002DB0 RID: 11696
		internal abstract void AddSerializedItems(StringBuilder sb);

		// Token: 0x04000C38 RID: 3128
		private MarkersAppearance _markersAppearance;

		// Token: 0x04000C39 RID: 3129
		private LineAndScatterLabelsAppearance _labelsAppearance;
	}
}
