using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001712 RID: 5906
	[DefaultProperty("Text")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class TextBlockSeriesItem : TextBlock
	{
		// Token: 0x0600E591 RID: 58769 RVA: 0x0032FE3C File Offset: 0x0032E03C
		public TextBlockSeriesItem() : this(null, null)
		{
		}

		// Token: 0x0600E592 RID: 58770 RVA: 0x0032FE46 File Offset: 0x0032E046
		public TextBlockSeriesItem(SeriesItemLabel parent, IContainer container) : base(parent, container, new StyleSeriesItemTextBlock())
		{
		}
	}
}
