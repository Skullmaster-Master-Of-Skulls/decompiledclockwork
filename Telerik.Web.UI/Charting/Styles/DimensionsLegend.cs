using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177E RID: 6014
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class DimensionsLegend : Dimensions
	{
		// Token: 0x0600EA9E RID: 60062 RVA: 0x003574EB File Offset: 0x003556EB
		public DimensionsLegend()
		{
			this.dimensionsMargins = new ChartMarginsLegend();
			this.dimensionsPaddings = new ChartPaddingsLegend();
		}

		// Token: 0x17004715 RID: 18197
		// (get) Token: 0x0600EA9F RID: 60063 RVA: 0x00357509 File Offset: 0x00355709
		// (set) Token: 0x0600EAA0 RID: 60064 RVA: 0x00357511 File Offset: 0x00355711
		[PersistenceMode(PersistenceMode.Attribute)]
		[TypeConverter(typeof(MarginsConverter))]
		[SkinnableProperty]
		[DefaultValue(typeof(ChartMargins), "1px, 2%, 1px, 1px")]
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

		// Token: 0x17004716 RID: 18198
		// (get) Token: 0x0600EAA1 RID: 60065 RVA: 0x0035751A File Offset: 0x0035571A
		// (set) Token: 0x0600EAA2 RID: 60066 RVA: 0x00357522 File Offset: 0x00355722
		[DefaultValue(typeof(ChartPaddings), "2px, 2px, 2px, 3px")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[TypeConverter(typeof(PaddingsConverter))]
		[NotifyParentProperty(true)]
		public override ChartPaddings Paddings
		{
			get
			{
				return this.dimensionsPaddings;
			}
			set
			{
				this.dimensionsPaddings = value;
			}
		}

		// Token: 0x0600EAA3 RID: 60067 RVA: 0x0035752C File Offset: 0x0035572C
		internal override void Reset()
		{
			base.Reset();
			this.dimensionsMargins.Right = DefaultValues.DEFAULT_MARGIN_LEGEND_RIGHT;
			this.dimensionsPaddings.Top = DefaultValues.DEFAULT_PADDING_PIXEL2;
			this.dimensionsPaddings.Right = DefaultValues.DEFAULT_PADDING_PIXEL2;
			this.dimensionsPaddings.Bottom = DefaultValues.DEFAULT_PADDING_PIXEL2;
			this.dimensionsPaddings.Left = DefaultValues.DEFAULT_PADDING_PIXEL3;
		}
	}
}
