using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017AC RID: 6060
	[DesignTimeVisible(true)]
	[TypeConverter(typeof(PaddingsConverter))]
	public class ChartPaddingsTitle : ChartPaddings
	{
		// Token: 0x0600EBF6 RID: 60406 RVA: 0x0035A9AF File Offset: 0x00358BAF
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartPaddingsTitle()
		{
			this.Reset();
		}

		// Token: 0x1700476B RID: 18283
		// (get) Token: 0x0600EBF7 RID: 60407 RVA: 0x0035A9BD File Offset: 0x00358BBD
		// (set) Token: 0x0600EBF8 RID: 60408 RVA: 0x0035A9C5 File Offset: 0x00358BC5
		[Description("Left padding.")]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "5px")]
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

		// Token: 0x1700476C RID: 18284
		// (get) Token: 0x0600EBF9 RID: 60409 RVA: 0x0035A9CE File Offset: 0x00358BCE
		// (set) Token: 0x0600EBFA RID: 60410 RVA: 0x0035A9D6 File Offset: 0x00358BD6
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "5px")]
		[TypeConverter(typeof(UnitConverter))]
		[Browsable(true)]
		[SkinnableProperty]
		[Description("Right padding.")]
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

		// Token: 0x1700476D RID: 18285
		// (get) Token: 0x0600EBFB RID: 60411 RVA: 0x0035A9DF File Offset: 0x00358BDF
		// (set) Token: 0x0600EBFC RID: 60412 RVA: 0x0035A9E7 File Offset: 0x00358BE7
		[SkinnableProperty]
		[DefaultValue(typeof(Unit), "3px")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Top padding.")]
		[Browsable(true)]
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

		// Token: 0x1700476E RID: 18286
		// (get) Token: 0x0600EBFD RID: 60413 RVA: 0x0035A9F0 File Offset: 0x00358BF0
		// (set) Token: 0x0600EBFE RID: 60414 RVA: 0x0035A9F8 File Offset: 0x00358BF8
		[Description("Bottom padding.")]
		[DefaultValue(typeof(Unit), "3px")]
		[TypeConverter(typeof(UnitConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
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

		// Token: 0x0600EBFF RID: 60415 RVA: 0x0035AA04 File Offset: 0x00358C04
		internal override void Reset()
		{
			this.Top = DefaultValues.DEFAULT_PADDING_PIXEL3.Clone();
			this.Bottom = DefaultValues.DEFAULT_PADDING_PIXEL3.Clone();
			this.Right = DefaultValues.DEFAULT_PADDING_PIXEL5.Clone();
			this.Left = DefaultValues.DEFAULT_PADDING_PIXEL5.Clone();
		}
	}
}
