using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200091E RID: 2334
	public class TileTitle : StateManager
	{
		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06005861 RID: 22625 RVA: 0x0010DDAE File Offset: 0x0010BFAE
		// (set) Token: 0x06005862 RID: 22626 RVA: 0x0010DDCE File Offset: 0x0010BFCE
		[DefaultValue("")]
		[ClientControlProperty]
		public string ImageUrl
		{
			get
			{
				return ((string)base.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06005863 RID: 22627 RVA: 0x0010DDE1 File Offset: 0x0010BFE1
		// (set) Token: 0x06005864 RID: 22628 RVA: 0x0010DE01 File Offset: 0x0010C001
		[ClientControlProperty]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
