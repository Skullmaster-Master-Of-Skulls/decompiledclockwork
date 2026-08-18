using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020016BD RID: 5821
	public class CalendarAnimationSettings : ObjectWithState
	{
		// Token: 0x0600E0B9 RID: 57529 RVA: 0x0031F2E5 File Offset: 0x0031D4E5
		public CalendarAnimationSettings(string prefix, StateBag OwnerStateBag) : base(prefix, OwnerStateBag)
		{
		}

		// Token: 0x170044E1 RID: 17633
		// (get) Token: 0x0600E0BA RID: 57530 RVA: 0x0031F2EF File Offset: 0x0031D4EF
		// (set) Token: 0x0600E0BB RID: 57531 RVA: 0x0031F314 File Offset: 0x0031D514
		[DefaultValue(300)]
		public int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 300);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x170044E2 RID: 17634
		// (get) Token: 0x0600E0BC RID: 57532 RVA: 0x0031F32C File Offset: 0x0031D52C
		// (set) Token: 0x0600E0BD RID: 57533 RVA: 0x0031F34D File Offset: 0x0031D54D
		[DefaultValue(CalendarAnimationType.Fade)]
		public CalendarAnimationType Type
		{
			get
			{
				return (CalendarAnimationType)(base.ViewState["Type"] ?? CalendarAnimationType.Fade);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}
	}
}
