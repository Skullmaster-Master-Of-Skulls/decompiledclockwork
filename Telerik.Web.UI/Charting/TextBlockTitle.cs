using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200170F RID: 5903
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("Text")]
	public class TextBlockTitle : TextBlock
	{
		// Token: 0x0600E583 RID: 58755 RVA: 0x0032FB64 File Offset: 0x0032DD64
		public TextBlockTitle() : this(null, null)
		{
		}

		// Token: 0x0600E584 RID: 58756 RVA: 0x0032FB6E File Offset: 0x0032DD6E
		public TextBlockTitle(ChartTitle parent, IContainer container) : base(parent, container, new StyleTextBlockTitle())
		{
			this.DEFAULT_TEXT = "Chart Title";
		}

		// Token: 0x170045ED RID: 17901
		// (get) Token: 0x0600E585 RID: 58757 RVA: 0x0032FB88 File Offset: 0x0032DD88
		// (set) Token: 0x0600E586 RID: 58758 RVA: 0x0032FB90 File Offset: 0x0032DD90
		[NotifyParentProperty(true)]
		[DefaultValue("Chart Title")]
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
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

		// Token: 0x0600E587 RID: 58759 RVA: 0x0032FB9C File Offset: 0x0032DD9C
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			if (renderEngine.chart.ShouldApplyTextWrapping(((StyleTextBlockTitle)base.Appearance).AutoTextWrap))
			{
				ChartText chartText = new ChartText(base.VisibleText, base.Appearance.TextProperties.Font, renderEngine.graphics);
				chartText.Distibute(renderEngine.chart.TextWrapFactor, this.textBlockWrapContext);
				this.textBlockWrappedText = chartText.ToString();
			}
			return base.Measure(renderEngine);
		}
	}
}
