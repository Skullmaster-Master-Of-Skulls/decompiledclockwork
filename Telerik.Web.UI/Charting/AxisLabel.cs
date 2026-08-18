using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001703 RID: 5891
	[ParseChildren(true)]
	[DefaultProperty("TextBlock")]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AxisLabel : AxisLabelHidden
	{
		// Token: 0x0600E4F9 RID: 58617 RVA: 0x0032E6A5 File Offset: 0x0032C8A5
		public AxisLabel() : this(null, null)
		{
		}

		// Token: 0x0600E4FA RID: 58618 RVA: 0x0032E6AF File Offset: 0x0032C8AF
		public AxisLabel(object parent, IContainer container) : base(parent, container, new StyleAxisLabel(), new TextBlockXAxisLabel(), null)
		{
		}
	}
}
