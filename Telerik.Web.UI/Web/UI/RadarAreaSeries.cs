using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI
{
	// Token: 0x02000504 RID: 1284
	[RadarSeriesMarker]
	public class RadarAreaSeries : AreaSeries
	{
		// Token: 0x06002DF6 RID: 11766 RVA: 0x00096E37 File Offset: 0x00095037
		public RadarAreaSeries()
		{
			this.sType = SeriesType.RadarArea;
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x00096E47 File Offset: 0x00095047
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x00096E53 File Offset: 0x00095053
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string AxisName
		{
			get
			{
				throw new Exception("AxisName property is not supported for Radar and Polar series types.");
			}
			set
			{
				throw new Exception("AxisName property is not supported for Radar and Polar series types.");
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x00096E5F File Offset: 0x0009505F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[DefaultValue("LineAppearance")]
		[Description("Series line visual settings")]
		public new LineAppearance LineAppearance
		{
			get
			{
				if (this._lineAppearance == null)
				{
					this._lineAppearance = new LineAppearance("lineAppearance", base.ViewState);
				}
				return this._lineAppearance;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x00096E85 File Offset: 0x00095085
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x00096EA6 File Offset: 0x000950A6
		[DefaultValue(MissingValuesBehavior.Interpolate)]
		public override MissingValuesBehavior MissingValues
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

		// Token: 0x06002DFC RID: 11772 RVA: 0x00096EBE File Offset: 0x000950BE
		protected override void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",line:{{{0}}}", this.LineAppearance.Serialize());
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00096ED7 File Offset: 0x000950D7
		protected internal override void SerializeMissingValues(StringBuilder sb)
		{
			if (this.MissingValues != MissingValuesBehavior.Interpolate)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.AppendFormat(",missingValues:'{0}'", this.MissingValues.ToString().ToLower());
			}
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x00096F0A File Offset: 0x0009510A
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x04000C3F RID: 3135
		private LineAppearance _lineAppearance;
	}
}
