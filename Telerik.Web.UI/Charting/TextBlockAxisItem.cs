using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001711 RID: 5905
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Text")]
	[PersistChildren(false)]
	public class TextBlockAxisItem : TextBlock
	{
		// Token: 0x0600E58D RID: 58765 RVA: 0x0032FCB9 File Offset: 0x0032DEB9
		public TextBlockAxisItem() : this(null, null)
		{
		}

		// Token: 0x0600E58E RID: 58766 RVA: 0x0032FCC3 File Offset: 0x0032DEC3
		public TextBlockAxisItem(ChartAxisItem parent, IContainer container) : base(parent, container, new StyleAxisItemText())
		{
		}

		// Token: 0x0600E58F RID: 58767 RVA: 0x0032FCD4 File Offset: 0x0032DED4
		internal void DefineMaxLengthAuto(RenderEngine renderEngine)
		{
			if (!renderEngine.chart.AutoLayoutWrapper)
			{
				return;
			}
			RectangleF realBounds = Style.GetRealBounds(base.Appearance.Dimensions, new float?(((ChartAxisItemsCollection)((ChartAxisItem)base.Parent).Parent).GetItemRotationAngle((ChartAxisItem)base.Parent)));
			SizeF sizeF = new SizeF(realBounds.Width, realBounds.Height);
			if (sizeF.Width > this.textBlockWrapContext.ContainerWidth)
			{
				float num = sizeF.Width / (float)base.VisibleText.Length;
				int val = (int)((sizeF.Width - this.textBlockWrapContext.ContainerWidth) / num);
				int num2 = Math.Max(1, val);
				while (sizeF.Width > this.textBlockWrapContext.ContainerWidth)
				{
					this.textBlockCalculatedMaxLength = this.Text.Length - num2++;
					sizeF = this.Measure(renderEngine);
				}
			}
		}

		// Token: 0x0600E590 RID: 58768 RVA: 0x0032FDC0 File Offset: 0x0032DFC0
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			if (renderEngine.chart.ShouldApplyTextWrapping(base.Appearance.AutoTextWrap))
			{
				this.textBlockWrappedText = string.Empty;
				ChartText chartText = new ChartText(base.VisibleText, base.Appearance.TextProperties.Font, renderEngine.graphics);
				chartText.Distibute(renderEngine.chart.TextWrapFactor, this.textBlockWrapContext);
				this.textBlockWrappedText = chartText.ToString();
			}
			return base.Measure(renderEngine);
		}
	}
}
