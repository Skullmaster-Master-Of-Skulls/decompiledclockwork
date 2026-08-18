using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019CC RID: 6604
	public class PanelBarAnimationSettings : AnimationSettings
	{
		// Token: 0x0600FF00 RID: 65280 RVA: 0x00393C45 File Offset: 0x00391E45
		public PanelBarAnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x17004CF2 RID: 19698
		// (get) Token: 0x0600FF01 RID: 65281 RVA: 0x00393C4F File Offset: 0x00391E4F
		// (set) Token: 0x0600FF02 RID: 65282 RVA: 0x00393C74 File Offset: 0x00391E74
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
