using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x02000507 RID: 1287
	[RadarSeriesMarker]
	public class RadarColumnSeries : ColumnSeries
	{
		// Token: 0x06002E1C RID: 11804 RVA: 0x00097806 File Offset: 0x00095A06
		public RadarColumnSeries()
		{
			this.sType = SeriesType.RadarColumn;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00097816 File Offset: 0x00095A16
		internal override void SerializeAxisProperty(StringBuilder sb)
		{
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x00097818 File Offset: 0x00095A18
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x00097824 File Offset: 0x00095A24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
	}
}
