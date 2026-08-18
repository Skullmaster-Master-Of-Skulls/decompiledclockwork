using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017ED RID: 6125
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleTextBlockHidden : StyleTextBlock
	{
		// Token: 0x1700482C RID: 18476
		// (get) Token: 0x0600EE70 RID: 61040 RVA: 0x00364F6A File Offset: 0x0036316A
		// (set) Token: 0x0600EE71 RID: 61041 RVA: 0x00364F8B File Offset: 0x0036318B
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
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

		// Token: 0x0600EE72 RID: 61042 RVA: 0x00364F94 File Offset: 0x00363194
		internal override void Reset()
		{
			base.Reset();
			this.Visible = false;
		}
	}
}
