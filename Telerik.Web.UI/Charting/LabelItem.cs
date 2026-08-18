using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016FF RID: 5887
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("TextBlock")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class LabelItem : ChartLabel
	{
		// Token: 0x170045CA RID: 17866
		// (get) Token: 0x0600E4D3 RID: 58579 RVA: 0x0032D3FC File Offset: 0x0032B5FC
		// (set) Token: 0x0600E4D4 RID: 58580 RVA: 0x0032D41C File Offset: 0x0032B61C
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x0600E4D5 RID: 58581 RVA: 0x0032D42F File Offset: 0x0032B62F
		public LabelItem() : this(null, new StyleLabel(), new TextBlockLabelItem(new StyleTextBlock()), null)
		{
		}

		// Token: 0x0600E4D6 RID: 58582 RVA: 0x0032D448 File Offset: 0x0032B648
		public LabelItem(object parent) : this(parent, new StyleLabel(), null, null)
		{
		}

		// Token: 0x0600E4D7 RID: 58583 RVA: 0x0032D458 File Offset: 0x0032B658
		public LabelItem(string text) : this(null, new StyleLabel(), new TextBlockLabelItem(new StyleTextBlock()), text)
		{
		}

		// Token: 0x0600E4D8 RID: 58584 RVA: 0x0032D471 File Offset: 0x0032B671
		public LabelItem(StyleLabel appearance) : this(null, appearance, null, null)
		{
		}

		// Token: 0x0600E4D9 RID: 58585 RVA: 0x0032D47D File Offset: 0x0032B67D
		public LabelItem(StyleLabel appearance, object parent) : this(parent, appearance, null, null)
		{
		}

		// Token: 0x0600E4DA RID: 58586 RVA: 0x0032D489 File Offset: 0x0032B689
		public LabelItem(StyleLabel appearance, string text) : this(null, appearance, null, text)
		{
		}

		// Token: 0x0600E4DB RID: 58587 RVA: 0x0032D495 File Offset: 0x0032B695
		public LabelItem(StyleLabel appearance, TextBlockLabelItem textBlock) : this(null, appearance, textBlock, null)
		{
		}

		// Token: 0x0600E4DC RID: 58588 RVA: 0x0032D4A4 File Offset: 0x0032B6A4
		public LabelItem(object parent, StyleLabel appearance, TextBlockLabelItem textBlock, string text) : base(parent, null, appearance, textBlock, text)
		{
			base.Remove(this.chartBaseLabelTextBlock);
			this.chartBaseLabelTextBlock = new TextBlockLabelItem(this, this, text);
			base.Add(this.chartBaseLabelTextBlock);
			this.chartBaseLabelMarker.appearance = new StyleMarkerLegend();
		}

		// Token: 0x170045CB RID: 17867
		// (get) Token: 0x0600E4DD RID: 58589 RVA: 0x0032D4F4 File Offset: 0x0032B6F4
		// (set) Token: 0x0600E4DE RID: 58590 RVA: 0x0032D4FC File Offset: 0x0032B6FC
		internal bool IsBound
		{
			get
			{
				return this.labelItemIsBound;
			}
			set
			{
				this.labelItemIsBound = value;
			}
		}

		// Token: 0x040041FA RID: 16890
		private bool labelItemIsBound;
	}
}
