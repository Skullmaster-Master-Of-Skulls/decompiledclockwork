using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001718 RID: 5912
	[DefaultProperty("Text")]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class TextBlockMarkedZone : TextBlock
	{
		// Token: 0x0600E5AD RID: 58797 RVA: 0x0033019B File Offset: 0x0032E39B
		public TextBlockMarkedZone() : this(null, null)
		{
		}

		// Token: 0x0600E5AE RID: 58798 RVA: 0x003301A5 File Offset: 0x0032E3A5
		public TextBlockMarkedZone(MarkedZoneLabel parent, IContainer container) : base(parent, container, new StyleTextBlock())
		{
		}

		// Token: 0x0600E5AF RID: 58799 RVA: 0x003301B4 File Offset: 0x0032E3B4
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			if (renderEngine.chart.ShouldApplyTextWrapping(base.Appearance.AutoTextWrap))
			{
				ChartText chartText = new ChartText(base.VisibleText, base.Appearance.TextProperties.Font, renderEngine.graphics);
				chartText.Distibute(renderEngine.chart.TextWrapFactor, this.textBlockWrapContext);
				this.textBlockWrappedText = chartText.ToString();
			}
			return base.Measure(renderEngine);
		}
	}
}
