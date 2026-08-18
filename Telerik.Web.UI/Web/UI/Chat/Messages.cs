using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000088 RID: 136
	public class Messages : StateManager, IDefaultCheck
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000D46A File Offset: 0x0000B66A
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000D48A File Offset: 0x0000B68A
		[DefaultValue("Type a message...")]
		public string Placeholder
		{
			get
			{
				return (string)(base.ViewState["Placeholder"] ?? "Type a message...");
			}
			set
			{
				base.ViewState["Placeholder"] = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000D49D File Offset: 0x0000B69D
		public bool IsDefault
		{
			get
			{
				return this.Placeholder == "Type a message...";
			}
		}
	}
}
