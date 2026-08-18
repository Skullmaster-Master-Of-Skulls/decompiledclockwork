using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001716 RID: 5910
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Text")]
	[ParseChildren(true)]
	public class TextBlockLabelItem : TextBlock
	{
		// Token: 0x0600E5A1 RID: 58785 RVA: 0x0032FFF0 File Offset: 0x0032E1F0
		public TextBlockLabelItem() : this(null, null, new StyleTextBlock(), null)
		{
		}

		// Token: 0x0600E5A2 RID: 58786 RVA: 0x00330000 File Offset: 0x0032E200
		public TextBlockLabelItem(StyleTextBlock appearance) : this(null, null, appearance, null)
		{
		}

		// Token: 0x0600E5A3 RID: 58787 RVA: 0x0033000C File Offset: 0x0032E20C
		public TextBlockLabelItem(string text) : this(null, null, new StyleTextBlock(), text)
		{
		}

		// Token: 0x0600E5A4 RID: 58788 RVA: 0x0033001C File Offset: 0x0032E21C
		public TextBlockLabelItem(StyleTextBlock appearance, string text) : this(null, null, appearance, text)
		{
		}

		// Token: 0x0600E5A5 RID: 58789 RVA: 0x00330028 File Offset: 0x0032E228
		public TextBlockLabelItem(ChartBaseLabel parent, IContainer container) : this(parent, container, new StyleTextBlock(), null)
		{
		}

		// Token: 0x0600E5A6 RID: 58790 RVA: 0x00330038 File Offset: 0x0032E238
		public TextBlockLabelItem(ChartBaseLabel parent, IContainer container, string text) : this(parent, container, new StyleTextBlock(), text)
		{
		}

		// Token: 0x0600E5A7 RID: 58791 RVA: 0x00330048 File Offset: 0x0032E248
		public TextBlockLabelItem(ChartBaseLabel parent, IContainer container, StyleTextBlock appearance) : this(parent, container, appearance, null)
		{
		}

		// Token: 0x0600E5A8 RID: 58792 RVA: 0x00330054 File Offset: 0x0032E254
		public TextBlockLabelItem(ChartBaseLabel parent, IContainer container, StyleTextBlock appearance, string text) : base(parent, container, appearance, text)
		{
			this.textBlockWrapContext = new WrapContext(1f, 1f, WrapType.FixedProportion);
		}

		// Token: 0x0600E5A9 RID: 58793 RVA: 0x00330078 File Offset: 0x0032E278
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			AutoTextWrap textBlockAutoTextWrap = (base.Appearance.AutoTextWrap == AutoTextWrap.Auto) ? renderEngine.chart.Legend.Appearance.ItemTextAppearance.AutoTextWrap : base.Appearance.AutoTextWrap;
			if (renderEngine.chart.ShouldApplyTextWrapping(textBlockAutoTextWrap))
			{
				this.textBlockWrappedText = string.Empty;
				ChartText chartText = new ChartText(base.VisibleText, base.Appearance.TextProperties.Font, renderEngine.graphics);
				chartText.Distibute(renderEngine.chart.TextWrapFactor);
				this.textBlockWrappedText = chartText.ToString();
			}
			return base.Measure(renderEngine);
		}
	}
}
