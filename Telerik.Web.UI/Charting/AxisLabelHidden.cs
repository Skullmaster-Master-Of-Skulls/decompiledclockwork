using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001702 RID: 5890
	[DefaultProperty("TextBlock")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class AxisLabelHidden : ChartLabel
	{
		// Token: 0x170045D1 RID: 17873
		// (get) Token: 0x0600E4F3 RID: 58611 RVA: 0x0032E648 File Offset: 0x0032C848
		// (set) Token: 0x0600E4F4 RID: 58612 RVA: 0x0032E655 File Offset: 0x0032C855
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool Visible
		{
			get
			{
				return this.appearance.Visible;
			}
			set
			{
				this.appearance.Visible = value;
			}
		}

		// Token: 0x0600E4F5 RID: 58613 RVA: 0x0032E663 File Offset: 0x0032C863
		public AxisLabelHidden() : this(null, null, new StyleLabelHidden(), null, null)
		{
		}

		// Token: 0x0600E4F6 RID: 58614 RVA: 0x0032E674 File Offset: 0x0032C874
		public AxisLabelHidden(object parent) : this(parent, null, new StyleLabelHidden(), null, null)
		{
		}

		// Token: 0x0600E4F7 RID: 58615 RVA: 0x0032E685 File Offset: 0x0032C885
		public AxisLabelHidden(string text) : this(null, null, new StyleLabelHidden(), null, text)
		{
		}

		// Token: 0x0600E4F8 RID: 58616 RVA: 0x0032E696 File Offset: 0x0032C896
		public AxisLabelHidden(object parent, IContainer container, StyleLabelHidden appearance, TextBlock textBlock, string text) : base(parent, container, appearance, textBlock, text)
		{
		}
	}
}
