using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A1 RID: 1185
	[ControlBuilder(typeof(PlaceHolderControlBuilder))]
	public class PlaceHolder : Control
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}
	}
}
