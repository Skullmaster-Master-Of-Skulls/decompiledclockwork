using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200002A RID: 42
	[ToolboxData("<{0}:CardBodyComponent runat=\"server\"></{0}:CardBodyComponent>")]
	[TelerikToolboxCategory("Layout")]
	[ToolboxItem(true)]
	public class CardBodyComponent : CardComponentBase
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005755 File Offset: 0x00003955
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-body";
			}
		}
	}
}
