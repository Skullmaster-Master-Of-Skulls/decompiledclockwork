using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000184 RID: 388
	[ToolboxItem(false)]
	[ToolboxData("<{0}:SeadragonControl runat=\"server\"></{0}:SeadragonControl>")]
	public class SeadragonControl : Panel
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x0001C62C File Offset: 0x0001A82C
		public SeadragonControl()
		{
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001C634 File Offset: 0x0001A834
		public SeadragonControl(Control ctl, ControlAnchor anchor)
		{
			this._anchor = anchor;
			this.Controls.Add(ctl);
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001C64F File Offset: 0x0001A84F
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0001C657 File Offset: 0x0001A857
		public ControlAnchor Anchor
		{
			get
			{
				return this._anchor;
			}
			set
			{
				this._anchor = value;
			}
		}

		// Token: 0x04000419 RID: 1049
		private ControlAnchor _anchor;
	}
}
