using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000778 RID: 1912
	public class ProgressBarAnimationSettings : StateManager
	{
		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000D3729 File Offset: 0x000D1929
		// (set) Token: 0x06004391 RID: 17297 RVA: 0x000D374E File Offset: 0x000D194E
		[DefaultValue(400)]
		public int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 400);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x000D3766 File Offset: 0x000D1966
		// (set) Token: 0x06004393 RID: 17299 RVA: 0x000D3787 File Offset: 0x000D1987
		[DefaultValue(false)]
		public bool EnableChunkAnimation
		{
			get
			{
				return (bool)(base.ViewState["EnableChunkAnimation"] ?? false);
			}
			set
			{
				base.ViewState["EnableChunkAnimation"] = value;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000D379F File Offset: 0x000D199F
		public bool IsDefault
		{
			get
			{
				return this.Duration == 400 && !this.EnableChunkAnimation;
			}
		}
	}
}
