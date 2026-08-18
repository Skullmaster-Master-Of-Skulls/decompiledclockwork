using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000028 RID: 40
	[ToolboxItem(true)]
	[ToolboxData("<{0}:CardMediaComponent runat=\"server\"></{0}:CardMediaComponent>")]
	[TelerikToolboxCategory("Layout")]
	public class CardMediaComponent : CardComponentBase
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000056F3 File Offset: 0x000038F3
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-media";
			}
		}
	}
}
