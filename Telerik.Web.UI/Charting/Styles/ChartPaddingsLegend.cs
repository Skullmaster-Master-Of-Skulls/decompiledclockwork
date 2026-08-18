using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017AD RID: 6061
	[DesignTimeVisible(true)]
	[TypeConverter(typeof(PaddingsConverter))]
	public class ChartPaddingsLegend : ChartPaddings
	{
		// Token: 0x0600EC00 RID: 60416 RVA: 0x0035AA51 File Offset: 0x00358C51
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartPaddingsLegend()
		{
			this.Reset();
		}

		// Token: 0x1700476F RID: 18287
		// (get) Token: 0x0600EC01 RID: 60417 RVA: 0x0035AA5F File Offset: 0x00358C5F
		// (set) Token: 0x0600EC02 RID: 60418 RVA: 0x0035AA67 File Offset: 0x00358C67
		[DefaultValue(typeof(Unit), "2px")]
		[Browsable(true)]
		[TypeConverter(typeof(UnitConverter))]
		[SkinnableProperty]
		[Description("Top padding.")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004770 RID: 18288
		// (get) Token: 0x0600EC03 RID: 60419 RVA: 0x0035AA70 File Offset: 0x00358C70
		// (set) Token: 0x0600EC04 RID: 60420 RVA: 0x0035AA78 File Offset: 0x00358C78
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "2px")]
		[Description("Right padding.")]
		[SkinnableProperty]
		[Browsable(true)]
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

		// Token: 0x17004771 RID: 18289
		// (get) Token: 0x0600EC05 RID: 60421 RVA: 0x0035AA81 File Offset: 0x00358C81
		// (set) Token: 0x0600EC06 RID: 60422 RVA: 0x0035AA89 File Offset: 0x00358C89
		[Description("Bottom padding.")]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "2px")]
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

		// Token: 0x17004772 RID: 18290
		// (get) Token: 0x0600EC07 RID: 60423 RVA: 0x0035AA92 File Offset: 0x00358C92
		// (set) Token: 0x0600EC08 RID: 60424 RVA: 0x0035AA9A File Offset: 0x00358C9A
		[DefaultValue(typeof(Unit), "3px")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Left padding.")]
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

		// Token: 0x0600EC09 RID: 60425 RVA: 0x0035AAA4 File Offset: 0x00358CA4
		internal override void Reset()
		{
			this.Top = DefaultValues.DEFAULT_PADDING_PIXEL2.Clone();
			this.Right = DefaultValues.DEFAULT_PADDING_PIXEL2.Clone();
			this.Bottom = DefaultValues.DEFAULT_PADDING_PIXEL2.Clone();
			this.Left = DefaultValues.DEFAULT_PADDING_PIXEL3.Clone();
		}
	}
}
