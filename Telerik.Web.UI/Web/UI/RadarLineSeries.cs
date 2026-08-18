using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI
{
	// Token: 0x02000509 RID: 1289
	[RadarSeriesMarker]
	public class RadarLineSeries : LineSeries
	{
		// Token: 0x06002E36 RID: 11830 RVA: 0x00097F0F File Offset: 0x0009610F
		public RadarLineSeries()
		{
			this.sType = SeriesType.RadarLine;
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x00097F1F File Offset: 0x0009611F
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x00097F2B File Offset: 0x0009612B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x00097F37 File Offset: 0x00096137
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("LineAppearance")]
		[Description("Series line visual settings")]
		[Category("Appearance")]
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

		// Token: 0x06002E3A RID: 11834 RVA: 0x00097F5D File Offset: 0x0009615D
		protected override void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",{0}", this.LineAppearance.Serialize());
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x00097F76 File Offset: 0x00096176
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x04000C44 RID: 3140
		private LineAppearance _lineAppearance;
	}
}
