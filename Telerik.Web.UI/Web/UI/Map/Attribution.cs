using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200058D RID: 1421
	public class Attribution : StateManager, IDefaultCheck
	{
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000AAA5E File Offset: 0x000A8C5E
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x000AAA7F File Offset: 0x000A8C7F
		[DefaultValue(AttributionPosition.BottomRight)]
		public AttributionPosition Position
		{
			get
			{
				return (AttributionPosition)(base.ViewState["Position"] ?? AttributionPosition.BottomRight);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000AAA97 File Offset: 0x000A8C97
		public bool IsDefault
		{
			get
			{
				return this.Position == AttributionPosition.BottomRight;
			}
		}
	}
}
