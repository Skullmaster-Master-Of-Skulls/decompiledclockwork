using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019CB RID: 6603
	public class MenuAnimationSettings : AnimationSettings
	{
		// Token: 0x0600FEFD RID: 65277 RVA: 0x00393C0D File Offset: 0x00391E0D
		public MenuAnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x17004CF1 RID: 19697
		// (get) Token: 0x0600FEFE RID: 65278 RVA: 0x00393C17 File Offset: 0x00391E17
		// (set) Token: 0x0600FEFF RID: 65279 RVA: 0x00393C3C File Offset: 0x00391E3C
		[DefaultValue(450)]
		public override int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 450);
			}
			set
			{
				base.Duration = value;
			}
		}
	}
}
