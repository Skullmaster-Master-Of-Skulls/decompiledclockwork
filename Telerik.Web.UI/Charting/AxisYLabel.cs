using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001704 RID: 5892
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("TextBlock")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class AxisYLabel : AxisLabelHidden
	{
		// Token: 0x0600E4FB RID: 58619 RVA: 0x0032E6C4 File Offset: 0x0032C8C4
		public AxisYLabel() : this(null, null)
		{
		}

		// Token: 0x0600E4FC RID: 58620 RVA: 0x0032E6CE File Offset: 0x0032C8CE
		public AxisYLabel(object parent, IContainer container) : base(parent, container, new StyleYAxisLabel(), new TextBlockYAxisLabel(), null)
		{
		}
	}
}
