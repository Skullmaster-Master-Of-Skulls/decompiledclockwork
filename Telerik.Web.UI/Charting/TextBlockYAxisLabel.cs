using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001714 RID: 5908
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[DefaultProperty("Text")]
	[PersistChildren(false)]
	public class TextBlockYAxisLabel : TextBlock
	{
		// Token: 0x0600E597 RID: 58775 RVA: 0x0032FE89 File Offset: 0x0032E089
		public TextBlockYAxisLabel() : this(null, null)
		{
		}

		// Token: 0x0600E598 RID: 58776 RVA: 0x0032FE93 File Offset: 0x0032E093
		public TextBlockYAxisLabel(AxisYLabel parent, IContainer container) : base(parent, container, new StyleTextBlockAxisLabel())
		{
			this.DEFAULT_TEXT = "Y Axis";
		}

		// Token: 0x170045F0 RID: 17904
		// (get) Token: 0x0600E599 RID: 58777 RVA: 0x0032FEAD File Offset: 0x0032E0AD
		// (set) Token: 0x0600E59A RID: 58778 RVA: 0x0032FEB5 File Offset: 0x0032E0B5
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue("Y Axis")]
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

		// Token: 0x0600E59B RID: 58779 RVA: 0x0032FEC0 File Offset: 0x0032E0C0
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
