using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001713 RID: 5907
	[DefaultProperty("Text")]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class TextBlockHidden : TextBlock
	{
		// Token: 0x0600E593 RID: 58771 RVA: 0x0032FE55 File Offset: 0x0032E055
		public TextBlockHidden() : this(null, null)
		{
		}

		// Token: 0x0600E594 RID: 58772 RVA: 0x0032FE5F File Offset: 0x0032E05F
		public TextBlockHidden(ChartBaseLabel parent, IContainer container) : base(parent, container, new StyleTextBlockHidden())
		{
		}

		// Token: 0x170045EF RID: 17903
		// (get) Token: 0x0600E595 RID: 58773 RVA: 0x0032FE6E File Offset: 0x0032E06E
		// (set) Token: 0x0600E596 RID: 58774 RVA: 0x0032FE7B File Offset: 0x0032E07B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
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
	}
}
