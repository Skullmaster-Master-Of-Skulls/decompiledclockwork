using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001705 RID: 5893
	[DefaultProperty("TextBlock")]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class MarkedZoneLabel : ChartLabel
	{
		// Token: 0x0600E4FD RID: 58621 RVA: 0x0032E6E3 File Offset: 0x0032C8E3
		public MarkedZoneLabel() : this(null, null)
		{
		}

		// Token: 0x0600E4FE RID: 58622 RVA: 0x0032E6ED File Offset: 0x0032C8ED
		public MarkedZoneLabel(StyleLabel appearance) : base(appearance, new TextBlockMarkedZone())
		{
		}

		// Token: 0x0600E4FF RID: 58623 RVA: 0x0032E6FB File Offset: 0x0032C8FB
		public MarkedZoneLabel(StyleLabel appearance, object parent) : base(parent, null, appearance, new TextBlockMarkedZone(), null)
		{
		}

		// Token: 0x0600E500 RID: 58624 RVA: 0x0032E70C File Offset: 0x0032C90C
		public MarkedZoneLabel(StyleLabel appearance, string text) : base(null, null, appearance, new TextBlockMarkedZone(), text)
		{
		}

		// Token: 0x0600E501 RID: 58625 RVA: 0x0032E71D File Offset: 0x0032C91D
		public MarkedZoneLabel(Chart parent, IContainer container) : base(parent, container, new StyleLabel(), new TextBlockMarkedZone(), null)
		{
		}
	}
}
