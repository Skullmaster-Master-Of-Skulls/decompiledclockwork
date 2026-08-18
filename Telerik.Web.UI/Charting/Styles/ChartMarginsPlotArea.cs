using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A7 RID: 6055
	[TypeConverter(typeof(MarginsConverter))]
	[DesignTimeVisible(true)]
	public class ChartMarginsPlotArea : ChartMargins
	{
		// Token: 0x0600EBDF RID: 60383 RVA: 0x0035A869 File Offset: 0x00358A69
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartMarginsPlotArea()
		{
			this.Reset();
		}

		// Token: 0x17004766 RID: 18278
		// (get) Token: 0x0600EBE0 RID: 60384 RVA: 0x0035A877 File Offset: 0x00358A77
		// (set) Token: 0x0600EBE1 RID: 60385 RVA: 0x0035A87F File Offset: 0x00358A7F
		[Description("Left margin.")]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "10%")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Left
		{
			get
			{
				return base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17004767 RID: 18279
		// (get) Token: 0x0600EBE2 RID: 60386 RVA: 0x0035A888 File Offset: 0x00358A88
		// (set) Token: 0x0600EBE3 RID: 60387 RVA: 0x0035A890 File Offset: 0x00358A90
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Description("Right margin.")]
		[Browsable(true)]
		[DefaultValue(typeof(Unit), "24%")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Right
		{
			get
			{
				return base.Right;
			}
			set
			{
				base.Right = value;
			}
		}

		// Token: 0x17004768 RID: 18280
		// (get) Token: 0x0600EBE4 RID: 60388 RVA: 0x0035A899 File Offset: 0x00358A99
		// (set) Token: 0x0600EBE5 RID: 60389 RVA: 0x0035A8A1 File Offset: 0x00358AA1
		[SkinnableProperty]
		[Description("Top margin.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "18%")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Top
		{
			get
			{
				return base.Top;
			}
			set
			{
				base.Top = value;
			}
		}

		// Token: 0x17004769 RID: 18281
		// (get) Token: 0x0600EBE6 RID: 60390 RVA: 0x0035A8AA File Offset: 0x00358AAA
		// (set) Token: 0x0600EBE7 RID: 60391 RVA: 0x0035A8B2 File Offset: 0x00358AB2
		[Description("Bottom margin.")]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "12%")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Bottom
		{
			get
			{
				return base.Bottom;
			}
			set
			{
				base.Bottom = value;
			}
		}

		// Token: 0x0600EBE8 RID: 60392 RVA: 0x0035A8BC File Offset: 0x00358ABC
		internal override void Reset()
		{
			this.Top = DefaultValues.DEFAULT_MARGIN_PLOTAREA_TOP.Clone();
			this.Right = DefaultValues.DEFAULT_MARGIN_PLOTAREA_RIGHT.Clone();
			this.Bottom = DefaultValues.DEFAULT_MARGIN_PLOTAREA_BOTTOM.Clone();
			this.Left = DefaultValues.DEFAULT_MARGIN_PLOTAREA_LEFT.Clone();
		}
	}
}
