using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177C RID: 6012
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DimensionsPlotArea : Dimensions
	{
		// Token: 0x0600EA91 RID: 60049 RVA: 0x003573A7 File Offset: 0x003555A7
		public DimensionsPlotArea()
		{
			this.dimensionsMargins = new ChartMarginsPlotArea();
		}

		// Token: 0x17004712 RID: 18194
		// (get) Token: 0x0600EA92 RID: 60050 RVA: 0x003573BA File Offset: 0x003555BA
		// (set) Token: 0x0600EA93 RID: 60051 RVA: 0x003573C2 File Offset: 0x003555C2
		[TypeConverter(typeof(MarginsConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[DefaultValue(typeof(ChartMargins), "18%, 24%, 12%, 10%")]
		[NotifyParentProperty(true)]
		public override ChartMargins Margins
		{
			get
			{
				return this.dimensionsMargins;
			}
			set
			{
				this.dimensionsMargins = value;
			}
		}

		// Token: 0x0600EA94 RID: 60052 RVA: 0x003573CC File Offset: 0x003555CC
		internal override void Reset()
		{
			base.Reset();
			this.dimensionsMargins.Top = DefaultValues.DEFAULT_MARGIN_PLOTAREA_TOP;
			this.dimensionsMargins.Right = DefaultValues.DEFAULT_MARGIN_PLOTAREA_RIGHT;
			this.dimensionsMargins.Bottom = DefaultValues.DEFAULT_MARGIN_PLOTAREA_BOTTOM;
			this.dimensionsMargins.Left = DefaultValues.DEFAULT_MARGIN_PLOTAREA_LEFT;
		}
	}
}
