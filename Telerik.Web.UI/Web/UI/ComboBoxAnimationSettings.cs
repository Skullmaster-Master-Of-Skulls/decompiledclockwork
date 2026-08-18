using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200180E RID: 6158
	public class ComboBoxAnimationSettings : AnimationSettings
	{
		// Token: 0x0600EFFC RID: 61436 RVA: 0x0036A15D File Offset: 0x0036835D
		public ComboBoxAnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x17004898 RID: 18584
		// (get) Token: 0x0600EFFD RID: 61437 RVA: 0x0036A167 File Offset: 0x00368367
		// (set) Token: 0x0600EFFE RID: 61438 RVA: 0x0036A18C File Offset: 0x0036838C
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
