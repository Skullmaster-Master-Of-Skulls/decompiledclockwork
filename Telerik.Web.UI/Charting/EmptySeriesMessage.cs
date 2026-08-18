using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001734 RID: 5940
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class EmptySeriesMessage : ChartLabel
	{
		// Token: 0x0600E75A RID: 59226 RVA: 0x0033C488 File Offset: 0x0033A688
		public EmptySeriesMessage()
		{
		}

		// Token: 0x0600E75B RID: 59227 RVA: 0x0033C490 File Offset: 0x0033A690
		public EmptySeriesMessage(ChartPlotArea parent) : this(parent, null)
		{
		}

		// Token: 0x0600E75C RID: 59228 RVA: 0x0033C49A File Offset: 0x0033A69A
		public EmptySeriesMessage(IContainer container) : this(null, container)
		{
		}

		// Token: 0x0600E75D RID: 59229 RVA: 0x0033C4A4 File Offset: 0x0033A6A4
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EmptySeriesMessage(ChartPlotArea parent, IContainer container) : base(parent, container, new StyleLabelEmptySeriesMessage(), new TextBlockEmptySeriesMessage(), null)
		{
			base.Marker.Appearance.styleChart = parent.Chart;
			this.Appearance.styleChart = parent.Chart;
		}

		// Token: 0x17004669 RID: 18025
		// (get) Token: 0x0600E75E RID: 59230 RVA: 0x0033C4E0 File Offset: 0x0033A6E0
		// (set) Token: 0x0600E75F RID: 59231 RVA: 0x0033C4ED File Offset: 0x0033A6ED
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool Visible
		{
			get
			{
				return this.Appearance.Visible;
			}
			set
			{
				this.Appearance.Visible = value;
			}
		}

		// Token: 0x0600E760 RID: 59232 RVA: 0x0033C4FC File Offset: 0x0033A6FC
		internal override bool IsVisible()
		{
			ChartPlotArea chartPlotArea = this.chartBaseLabelParent as ChartPlotArea;
			if (chartPlotArea != null)
			{
				this.Appearance.Visible = chartPlotArea.SeriesCollection().IsSeriesEmpty();
			}
			else
			{
				this.Appearance.Visible = false;
			}
			return this.Appearance.Visible;
		}
	}
}
