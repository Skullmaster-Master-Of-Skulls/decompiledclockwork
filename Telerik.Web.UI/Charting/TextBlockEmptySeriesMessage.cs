using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001710 RID: 5904
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[DefaultProperty("Text")]
	public class TextBlockEmptySeriesMessage : TextBlock
	{
		// Token: 0x0600E588 RID: 58760 RVA: 0x0032FC12 File Offset: 0x0032DE12
		public TextBlockEmptySeriesMessage() : this(null, null)
		{
		}

		// Token: 0x0600E589 RID: 58761 RVA: 0x0032FC1C File Offset: 0x0032DE1C
		public TextBlockEmptySeriesMessage(EmptySeriesMessage parent, IContainer container) : base(parent, container, new StyleTextBlockError())
		{
			this.DEFAULT_TEXT = "There is no or empty series";
		}

		// Token: 0x170045EE RID: 17902
		// (get) Token: 0x0600E58A RID: 58762 RVA: 0x0032FC36 File Offset: 0x0032DE36
		// (set) Token: 0x0600E58B RID: 58763 RVA: 0x0032FC3E File Offset: 0x0032DE3E
		[Localizable(true)]
		[DefaultValue("There is no or empty series")]
		[NotifyParentProperty(true)]
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

		// Token: 0x0600E58C RID: 58764 RVA: 0x0032FC48 File Offset: 0x0032DE48
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
