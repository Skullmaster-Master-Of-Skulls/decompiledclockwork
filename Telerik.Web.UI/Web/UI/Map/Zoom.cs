using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005BA RID: 1466
	public class Zoom : StateManager, IDefaultCheck
	{
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000AD7DA File Offset: 0x000AB9DA
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x000AD7FB File Offset: 0x000AB9FB
		[DefaultValue(ZoomPosition.TopLeft)]
		public ZoomPosition Position
		{
			get
			{
				return (ZoomPosition)(base.ViewState["Position"] ?? ZoomPosition.TopLeft);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000AD813 File Offset: 0x000ABA13
		public bool IsDefault
		{
			get
			{
				return this.Position == ZoomPosition.TopLeft;
			}
		}
	}
}
