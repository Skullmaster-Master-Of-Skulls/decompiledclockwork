using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016FD RID: 5885
	[DefaultProperty("TextBlock")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ChartLabel : ChartBaseLabel
	{
		// Token: 0x170045C6 RID: 17862
		// (get) Token: 0x0600E4A9 RID: 58537 RVA: 0x0032C506 File Offset: 0x0032A706
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public virtual StyleLabel Appearance
		{
			get
			{
				return (StyleLabel)this.appearance;
			}
		}

		// Token: 0x0600E4AA RID: 58538 RVA: 0x0032C513 File Offset: 0x0032A713
		public ChartLabel() : this(new StyleLabel())
		{
		}

		// Token: 0x0600E4AB RID: 58539 RVA: 0x0032C520 File Offset: 0x0032A720
		public ChartLabel(object parent) : this(new StyleLabel(), parent)
		{
		}

		// Token: 0x0600E4AC RID: 58540 RVA: 0x0032C52E File Offset: 0x0032A72E
		public ChartLabel(string text) : this(new StyleLabel(), text)
		{
		}

		// Token: 0x0600E4AD RID: 58541 RVA: 0x0032C53C File Offset: 0x0032A73C
		public ChartLabel(StyleLabel appearance) : this(appearance, new TextBlock(new StyleTextBlock()))
		{
		}

		// Token: 0x0600E4AE RID: 58542 RVA: 0x0032C54F File Offset: 0x0032A74F
		public ChartLabel(StyleLabel appearance, object parent) : this(parent, null, appearance, null, null)
		{
		}

		// Token: 0x0600E4AF RID: 58543 RVA: 0x0032C55C File Offset: 0x0032A75C
		public ChartLabel(StyleLabel appearance, string text) : this(null, null, appearance, null, text)
		{
		}

		// Token: 0x0600E4B0 RID: 58544 RVA: 0x0032C569 File Offset: 0x0032A769
		public ChartLabel(StyleLabel appearance, TextBlock textBlock) : this(null, null, appearance, textBlock, null)
		{
		}

		// Token: 0x0600E4B1 RID: 58545 RVA: 0x0032C576 File Offset: 0x0032A776
		public ChartLabel(StyleLabel appearance, TextBlock textBlock, object parent) : this(parent, null, appearance, textBlock, null)
		{
		}

		// Token: 0x0600E4B2 RID: 58546 RVA: 0x0032C583 File Offset: 0x0032A783
		public ChartLabel(Chart parent, IContainer container) : this(parent, container, new StyleLabel(), null, null)
		{
		}

		// Token: 0x0600E4B3 RID: 58547 RVA: 0x0032C594 File Offset: 0x0032A794
		public ChartLabel(object parent, IContainer container, StyleLabel appearance, TextBlock textBlock, string text) : base(parent, container, textBlock, appearance)
		{
			if (textBlock != null && text != null)
			{
				textBlock.Text = text;
			}
		}

		// Token: 0x0600E4B4 RID: 58548 RVA: 0x0032C5B2 File Offset: 0x0032A7B2
		internal override bool IsVisible()
		{
			return this.Visible && base.IsVisible();
		}
	}
}
