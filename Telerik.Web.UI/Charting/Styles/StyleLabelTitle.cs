using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DD RID: 6109
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleLabelTitle : StyleLabel
	{
		// Token: 0x0600EDAD RID: 60845 RVA: 0x00362DB5 File Offset: 0x00360FB5
		public StyleLabelTitle() : this(null)
		{
		}

		// Token: 0x0600EDAE RID: 60846 RVA: 0x00362DBE File Offset: 0x00360FBE
		public StyleLabelTitle(Chart chart) : base(new FillStyleTitle(), null, new PositionTopLeft(), new DimensionsTitle())
		{
			this.styleBorder = new StyleTitleBorder();
			this.styleChart = chart;
		}

		// Token: 0x0600EDAF RID: 60847 RVA: 0x00362DE8 File Offset: 0x00360FE8
		internal override void Reset()
		{
			base.Reset();
			this.position = new PositionTopLeft();
			this.styleLabelFillStyle = new FillStyleTitle();
			this.dimensions = new DimensionsTitle();
			this.styleBorder = new StyleTitleBorder();
		}

		// Token: 0x0600EDB0 RID: 60848 RVA: 0x00362E1C File Offset: 0x0036101C
		internal override void SetAutoLayoutDefaults()
		{
			base.SetAutoLayoutDefaults();
			this.dimensions.Margins = new ChartMargins(DefaultValues.AUTO_MARGIN_TITLE);
		}
	}
}
