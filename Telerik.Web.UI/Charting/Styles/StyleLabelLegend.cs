using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DA RID: 6106
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleLabelLegend : StyleExtendedLabel
	{
		// Token: 0x0600EDA1 RID: 60833 RVA: 0x00362C30 File Offset: 0x00360E30
		public StyleLabelLegend() : base(new FillStyleTitle(), new PositionRight(), new DimensionsLegend())
		{
			this.styleBorder = new StyleLegendBorder();
			this.styleExtendedLabelItemMarkerAppearance = new StyleMarkerLegend();
		}

		// Token: 0x170047ED RID: 18413
		// (get) Token: 0x0600EDA2 RID: 60834 RVA: 0x00362C5D File Offset: 0x00360E5D
		[SkinnableProperty]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Figure
		{
			get
			{
				return "Rectangle";
			}
		}

		// Token: 0x170047EE RID: 18414
		// (get) Token: 0x0600EDA3 RID: 60835 RVA: 0x00362C64 File Offset: 0x00360E64
		// (set) Token: 0x0600EDA4 RID: 60836 RVA: 0x00362C85 File Offset: 0x00360E85
		[SkinnableProperty]
		[DefaultValue(typeof(Overflow), "Column")]
		public override Overflow Overflow
		{
			get
			{
				return (Overflow)(base.ViewState["Overflow"] ?? Overflow.Column);
			}
			set
			{
				base.Overflow = value;
			}
		}

		// Token: 0x0600EDA5 RID: 60837 RVA: 0x00362C90 File Offset: 0x00360E90
		internal override void Reset()
		{
			base.Reset();
			this.position = new PositionRight();
			this.styleBorder = new StyleLegendBorder();
			this.styleLabelFillStyle = new FillStyleTitle();
			this.dimensions = new DimensionsLegend();
			this.styleExtendedLabelItemMarkerAppearance = new StyleMarkerLegend();
			this.CompositionType = LabelItemsCompositionTypes.RowImageText;
		}

		// Token: 0x0600EDA6 RID: 60838 RVA: 0x00362CE1 File Offset: 0x00360EE1
		internal override void SetAutoLayoutDefaults()
		{
			base.SetAutoLayoutDefaults();
			this.dimensions.Margins = new ChartMargins(DefaultValues.AUTO_MARGIN_LEGEND);
		}
	}
}
