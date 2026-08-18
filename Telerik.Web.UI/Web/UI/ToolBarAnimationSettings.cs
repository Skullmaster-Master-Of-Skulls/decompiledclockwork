using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B57 RID: 6999
	public class ToolBarAnimationSettings : AnimationSettings
	{
		// Token: 0x06010F44 RID: 69444 RVA: 0x003C0F30 File Offset: 0x003BF130
		public ToolBarAnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x170052C3 RID: 21187
		// (get) Token: 0x06010F45 RID: 69445 RVA: 0x003C0F3A File Offset: 0x003BF13A
		// (set) Token: 0x06010F46 RID: 69446 RVA: 0x003C0F5F File Offset: 0x003BF15F
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
