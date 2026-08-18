using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E1 RID: 6113
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleMarkerSeriesPoint : StyleMarker
	{
		// Token: 0x0600EDD6 RID: 60886 RVA: 0x003634FC File Offset: 0x003616FC
		public StyleMarkerSeriesPoint(ChartSeries series, string subPropertyName) : this()
		{
			this.styleContainerObject = series;
			this.dimensions = new DimensionsSeriesPointMark(series);
			this.styleMarkerFillStyle = new FillStyleSeriesPoint(series);
			this.styleBorder = new StyleBorder(series);
			this.styleMarkerCorners = new Corners(series);
			this.position = new Position(series);
		}

		// Token: 0x0600EDD7 RID: 60887 RVA: 0x00363552 File Offset: 0x00361752
		public StyleMarkerSeriesPoint()
		{
			this.dimensions = new DimensionsSeriesPointMark();
			this.styleMarkerFillStyle = new FillStyleSeriesPoint();
			this.position = new PositionCenter();
		}

		// Token: 0x170047F9 RID: 18425
		// (get) Token: 0x0600EDD8 RID: 60888 RVA: 0x0036357B File Offset: 0x0036177B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SkinnableProperty]
		[Browsable(false)]
		public override Position Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170047FA RID: 18426
		// (get) Token: 0x0600EDD9 RID: 60889 RVA: 0x00363583 File Offset: 0x00361783
		// (set) Token: 0x0600EDDA RID: 60890 RVA: 0x003635A4 File Offset: 0x003617A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x170047FB RID: 18427
		// (get) Token: 0x0600EDDB RID: 60891 RVA: 0x003635AD File Offset: 0x003617AD
		// (set) Token: 0x0600EDDC RID: 60892 RVA: 0x003635CD File Offset: 0x003617CD
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[SkinnableProperty]
		[Description("Specifies the shape of the item's point mark")]
		[DefaultValue(typeof(string), "Circle")]
		[Category("Point marks")]
		[NotifyParentProperty(true)]
		public override string Figure
		{
			get
			{
				return (string)(base.ViewState["Figure"] ?? "Circle");
			}
			set
			{
				base.ViewState["Figure"] = value;
			}
		}
	}
}
