using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B6B RID: 7019
	public class TreeViewAnimationSettings : AnimationSettings
	{
		// Token: 0x06011004 RID: 69636 RVA: 0x003C20AB File Offset: 0x003C02AB
		public TreeViewAnimationSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x17005307 RID: 21255
		// (get) Token: 0x06011005 RID: 69637 RVA: 0x003C20B5 File Offset: 0x003C02B5
		// (set) Token: 0x06011006 RID: 69638 RVA: 0x003C20DA File Offset: 0x003C02DA
		[DefaultValue(200)]
		public override int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 200);
			}
			set
			{
				base.Duration = value;
			}
		}
	}
}
