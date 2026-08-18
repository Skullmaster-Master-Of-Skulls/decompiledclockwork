using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A8 RID: 1448
	public class Navigator : StateManager, IDefaultCheck
	{
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000AC5AA File Offset: 0x000AA7AA
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x000AC5CB File Offset: 0x000AA7CB
		[DefaultValue(NavigatorPosition.TopLeft)]
		public NavigatorPosition Position
		{
			get
			{
				return (NavigatorPosition)(base.ViewState["Position"] ?? NavigatorPosition.TopLeft);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000AC5E3 File Offset: 0x000AA7E3
		public bool IsDefault
		{
			get
			{
				return this.Position == NavigatorPosition.TopLeft;
			}
		}
	}
}
