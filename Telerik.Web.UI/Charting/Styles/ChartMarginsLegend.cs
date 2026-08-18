using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A8 RID: 6056
	[TypeConverter(typeof(MarginsConverter))]
	[DesignTimeVisible(true)]
	public class ChartMarginsLegend : ChartMargins
	{
		// Token: 0x0600EBE9 RID: 60393 RVA: 0x0035A909 File Offset: 0x00358B09
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartMarginsLegend()
		{
			this.Reset();
		}

		// Token: 0x1700476A RID: 18282
		// (get) Token: 0x0600EBEA RID: 60394 RVA: 0x0035A917 File Offset: 0x00358B17
		// (set) Token: 0x0600EBEB RID: 60395 RVA: 0x0035A91F File Offset: 0x00358B1F
		[Description("Right margin.")]
		[DefaultValue(typeof(Unit), "2%")]
		[TypeConverter(typeof(UnitConverter))]
		[SkinnableProperty]
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

		// Token: 0x0600EBEC RID: 60396 RVA: 0x0035A928 File Offset: 0x00358B28
		internal override void Reset()
		{
			base.Reset();
			this.Right = DefaultValues.DEFAULT_MARGIN_LEGEND_RIGHT.Clone();
		}
	}
}
