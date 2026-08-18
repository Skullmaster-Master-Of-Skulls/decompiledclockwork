using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177B RID: 6011
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DimensionsTitle : Dimensions
	{
		// Token: 0x0600EA8B RID: 60043 RVA: 0x003572D6 File Offset: 0x003554D6
		public DimensionsTitle()
		{
			this.dimensionsMargins = new ChartMarginsTitle();
			this.dimensionsPaddings = new ChartPaddingsTitle();
		}

		// Token: 0x17004710 RID: 18192
		// (get) Token: 0x0600EA8C RID: 60044 RVA: 0x003572F4 File Offset: 0x003554F4
		// (set) Token: 0x0600EA8D RID: 60045 RVA: 0x003572FC File Offset: 0x003554FC
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[TypeConverter(typeof(MarginsConverter))]
		[DefaultValue(typeof(ChartMargins), "4%, 10px, 14px, 7%")]
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

		// Token: 0x17004711 RID: 18193
		// (get) Token: 0x0600EA8E RID: 60046 RVA: 0x00357305 File Offset: 0x00355505
		// (set) Token: 0x0600EA8F RID: 60047 RVA: 0x0035730D File Offset: 0x0035550D
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ChartPaddings), "3px, 5px, 3px, 5px")]
		[TypeConverter(typeof(PaddingsConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
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

		// Token: 0x0600EA90 RID: 60048 RVA: 0x00357318 File Offset: 0x00355518
		internal override void Reset()
		{
			base.Reset();
			this.dimensionsMargins.Top = DefaultValues.DEFAULT_MARGIN_TITLE_TOP;
			this.dimensionsMargins.Right = DefaultValues.DEFAULT_MARGIN_TITLE_RIGHT;
			this.dimensionsMargins.Bottom = DefaultValues.DEFAULT_MARGIN_TITLE_BOTTOM;
			this.dimensionsMargins.Left = DefaultValues.DEFAULT_MARGIN_TITLE_LEFT;
			this.dimensionsPaddings.Top = (this.dimensionsPaddings.Bottom = DefaultValues.DEFAULT_PADDING_PIXEL3);
			this.dimensionsPaddings.Right = (this.dimensionsPaddings.Left = DefaultValues.DEFAULT_PADDING_PIXEL5);
		}
	}
}
