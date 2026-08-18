using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000026 RID: 38
	[TelerikToolboxCategory("Layout")]
	[ToolboxData("<{0}:CardHeaderComponent runat=\"server\"></{0}:CardHeaderComponent>")]
	[ToolboxItem(true)]
	public class CardHeaderComponent : CardComponentBase
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000055D5 File Offset: 0x000037D5
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-header";
			}
		}
	}
}
