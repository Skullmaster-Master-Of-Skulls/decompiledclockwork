using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DE RID: 6110
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StyleLabelEmptySeriesMessage : StyleLabel
	{
		// Token: 0x0600EDB1 RID: 60849 RVA: 0x00362E39 File Offset: 0x00361039
		public StyleLabelEmptySeriesMessage()
		{
			this.position = new PositionCenter();
		}

		// Token: 0x170047F0 RID: 18416
		// (get) Token: 0x0600EDB2 RID: 60850 RVA: 0x00362E4C File Offset: 0x0036104C
		// (set) Token: 0x0600EDB3 RID: 60851 RVA: 0x00362E6D File Offset: 0x0036106D
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600EDB4 RID: 60852 RVA: 0x00362E76 File Offset: 0x00361076
		internal override void Reset()
		{
			base.Reset();
			this.position = new PositionCenter();
			this.Visible = false;
		}
	}
}
