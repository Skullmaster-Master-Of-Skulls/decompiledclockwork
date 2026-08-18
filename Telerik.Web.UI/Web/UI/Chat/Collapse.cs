using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000084 RID: 132
	public class Collapse : StateManager, IDefaultCheck
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000D22A File Offset: 0x0000B42A
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000D24A File Offset: 0x0000B44A
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000D25D File Offset: 0x0000B45D
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000D286 File Offset: 0x0000B486
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000D29E File Offset: 0x0000B49E
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 0.0;
			}
		}
	}
}
