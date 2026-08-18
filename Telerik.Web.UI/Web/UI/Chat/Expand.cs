using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000086 RID: 134
	public class Expand : StateManager, IDefaultCheck
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000D34A File Offset: 0x0000B54A
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000D36A File Offset: 0x0000B56A
		[DefaultValue("")]
		public string Effects
		{
			get
			{
				return (string)(base.ViewState["Effects"] ?? "");
			}
			set
			{
				base.ViewState["Effects"] = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000D37D File Offset: 0x0000B57D
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		[DefaultValue(0.0)]
		public double Duration
		{
			get
			{
				return (double)(base.ViewState["Duration"] ?? 0.0);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 0.0;
			}
		}
	}
}
