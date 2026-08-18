using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting
{
	// Token: 0x02001717 RID: 5911
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Text")]
	public class TextBlockLegend : TextBlockHidden
	{
		// Token: 0x0600E5AA RID: 58794 RVA: 0x00330119 File Offset: 0x0032E319
		public TextBlockLegend() : this(null, null)
		{
		}

		// Token: 0x0600E5AB RID: 58795 RVA: 0x00330123 File Offset: 0x0032E323
		public TextBlockLegend(ExtendedLabel parent, IContainer container) : base(parent, container)
		{
		}

		// Token: 0x0600E5AC RID: 58796 RVA: 0x00330130 File Offset: 0x0032E330
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			if (renderEngine.chart.ShouldApplyTextWrapping(base.Appearance.AutoTextWrap))
			{
				ChartText chartText = new ChartText(base.VisibleText, base.Appearance.TextProperties.Font, renderEngine.graphics);
				chartText.Distibute(renderEngine.chart.TextWrapFactor);
				this.textBlockWrappedText = chartText.ToString();
			}
			return base.Measure(renderEngine);
		}
	}
}
