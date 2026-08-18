using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200056E RID: 1390
	public sealed class TitleStyle : TableItemStyle
	{
		// Token: 0x06004681 RID: 18049 RVA: 0x000E9216 File Offset: 0x000E7416
		public TitleStyle()
		{
			this.Wrap = false;
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000E9225 File Offset: 0x000E7425
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000E922D File Offset: 0x000E742D
		[DefaultValue(false)]
		public override bool Wrap
		{
			get
			{
				return base.Wrap;
			}
			set
			{
				base.Wrap = value;
			}
		}
	}
}
