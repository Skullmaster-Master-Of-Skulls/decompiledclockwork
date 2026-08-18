using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A6 RID: 6054
	[TypeConverter(typeof(MarginsConverter))]
	[DesignTimeVisible(true)]
	public class ChartMarginsTitle : ChartMargins
	{
		// Token: 0x0600EBD5 RID: 60373 RVA: 0x0035A7C8 File Offset: 0x003589C8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartMarginsTitle()
		{
			this.Reset();
		}

		// Token: 0x17004762 RID: 18274
		// (get) Token: 0x0600EBD6 RID: 60374 RVA: 0x0035A7D6 File Offset: 0x003589D6
		// (set) Token: 0x0600EBD7 RID: 60375 RVA: 0x0035A7DE File Offset: 0x003589DE
		[DefaultValue(typeof(Unit), "24%")]
		[SkinnableProperty]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Right margin.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004763 RID: 18275
		// (get) Token: 0x0600EBD8 RID: 60376 RVA: 0x0035A7E7 File Offset: 0x003589E7
		// (set) Token: 0x0600EBD9 RID: 60377 RVA: 0x0035A7EF File Offset: 0x003589EF
		[Description("Top margin.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "18%")]
		[TypeConverter(typeof(UnitConverter))]
		[SkinnableProperty]
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

		// Token: 0x17004764 RID: 18276
		// (get) Token: 0x0600EBDA RID: 60378 RVA: 0x0035A7F8 File Offset: 0x003589F8
		// (set) Token: 0x0600EBDB RID: 60379 RVA: 0x0035A800 File Offset: 0x00358A00
		[SkinnableProperty]
		[Description("Bottom margin.")]
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

		// Token: 0x17004765 RID: 18277
		// (get) Token: 0x0600EBDC RID: 60380 RVA: 0x0035A809 File Offset: 0x00358A09
		// (set) Token: 0x0600EBDD RID: 60381 RVA: 0x0035A811 File Offset: 0x00358A11
		[Browsable(true)]
		[Description("Left margin.")]
		[SkinnableProperty]
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

		// Token: 0x0600EBDE RID: 60382 RVA: 0x0035A81C File Offset: 0x00358A1C
		internal override void Reset()
		{
			this.Top = DefaultValues.DEFAULT_MARGIN_TITLE_TOP.Clone();
			this.Right = DefaultValues.DEFAULT_MARGIN_TITLE_RIGHT.Clone();
			this.Bottom = DefaultValues.DEFAULT_MARGIN_TITLE_BOTTOM.Clone();
			this.Left = DefaultValues.DEFAULT_MARGIN_TITLE_LEFT.Clone();
		}
	}
}
