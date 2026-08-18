using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E9 RID: 6121
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StyleSeriesItemTextBlock : StyleTextBlock
	{
		// Token: 0x0600EE63 RID: 61027 RVA: 0x00364E04 File Offset: 0x00363004
		public StyleSeriesItemTextBlock()
		{
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
			this.styleTextBlockTextProperties = new TextPropertiesSeriesItem();
		}

		// Token: 0x0600EE64 RID: 61028 RVA: 0x00364E24 File Offset: 0x00363024
		public StyleSeriesItemTextBlock(ChartSeries series) : this()
		{
			Dimensions dimensions = this.dimensions;
			FillStyle styleTextBlockFillStyle = this.styleTextBlockFillStyle;
			StyleBorder styleBorder = this.styleBorder;
			Corners styleTextBlockCorners = this.styleTextBlockCorners;
			this.position.positionContainerObject = series;
			styleTextBlockCorners.cornersContainerObject = series;
			styleBorder.lineStyleContainerObject = series;
			styleTextBlockFillStyle.fillStyleContainerObject = series;
			dimensions.containerObject = series;
			this.styleContainerObject = series;
		}

		// Token: 0x0600EE65 RID: 61029 RVA: 0x00364E86 File Offset: 0x00363086
		internal override void Reset()
		{
			base.Reset();
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
			this.styleTextBlockTextProperties = new TextPropertiesSeriesItem();
		}

		// Token: 0x0600EE66 RID: 61030 RVA: 0x00364EA4 File Offset: 0x003630A4
		protected override bool ShouldSerializeMaxLength()
		{
			return base.MaxLength != DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
		}

		// Token: 0x0600EE67 RID: 61031 RVA: 0x00364EB6 File Offset: 0x003630B6
		protected override void ResetMaxLength()
		{
			base.MaxLength = DefaultValues.DEFAULT_MAX_ITEM_TEXT_LENGTH;
		}
	}
}
