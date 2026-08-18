using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001715 RID: 5909
	[DefaultProperty("Text")]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class TextBlockXAxisLabel : TextBlock
	{
		// Token: 0x0600E59C RID: 58780 RVA: 0x0032FF3C File Offset: 0x0032E13C
		public TextBlockXAxisLabel() : this(null, null)
		{
		}

		// Token: 0x0600E59D RID: 58781 RVA: 0x0032FF46 File Offset: 0x0032E146
		public TextBlockXAxisLabel(AxisLabel parent, IContainer container) : base(parent, container, new StyleTextBlockAxisLabel())
		{
			this.DEFAULT_TEXT = "X Axis";
		}

		// Token: 0x170045F1 RID: 17905
		// (get) Token: 0x0600E59E RID: 58782 RVA: 0x0032FF60 File Offset: 0x0032E160
		// (set) Token: 0x0600E59F RID: 58783 RVA: 0x0032FF68 File Offset: 0x0032E168
		[PersistenceMode(PersistenceMode.Attribute)]
		[Localizable(true)]
		[DefaultValue("X Axis")]
		[NotifyParentProperty(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x0600E5A0 RID: 58784 RVA: 0x0032FF74 File Offset: 0x0032E174
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
