using System;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series
{
	// Token: 0x020004FE RID: 1278
	public abstract class ScatterAndBubbleSeriesBase : MarkersSeries
	{
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x00095F7D File Offset: 0x0009417D
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x00095F85 File Offset: 0x00094185
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string DataFieldX
		{
			get
			{
				return base.DataFieldX;
			}
			set
			{
				base.DataFieldX = value;
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00095F90 File Offset: 0x00094190
		internal override void SerializeDataboundFields(StringBuilder sb)
		{
			sb.Append("xField: '").Append(this.DataFieldX).Append("',");
			sb.Append("yField: '").Append(base.DataFieldY).Append("'");
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00095FE0 File Offset: 0x000941E0
		protected virtual void SerializeItem(StringBuilder sb, SeriesItem item)
		{
			sb.Append((item.XValue != null) ? base.GetSerializedField(item.XValue.ToString()) : "null").Append(",");
			sb.Append((item.YValue != null) ? base.GetSerializedField(item.YValue.ToString()) : "null");
		}

		// Token: 0x06002DBA RID: 11706
		protected abstract void AddSeriesItems(StringBuilder sb);
	}
}
