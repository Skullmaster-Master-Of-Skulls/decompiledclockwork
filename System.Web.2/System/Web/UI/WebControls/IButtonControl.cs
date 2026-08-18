using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200043A RID: 1082
	public interface IButtonControl
	{
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003474 RID: 13428
		// (set) Token: 0x06003475 RID: 13429
		bool CausesValidation { get; set; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003476 RID: 13430
		// (set) Token: 0x06003477 RID: 13431
		string CommandArgument { get; set; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003478 RID: 13432
		// (set) Token: 0x06003479 RID: 13433
		string CommandName { get; set; }

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600347A RID: 13434
		// (remove) Token: 0x0600347B RID: 13435
		event EventHandler Click;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x0600347C RID: 13436
		// (remove) Token: 0x0600347D RID: 13437
		event CommandEventHandler Command;

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x0600347E RID: 13438
		// (set) Token: 0x0600347F RID: 13439
		string PostBackUrl { get; set; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003480 RID: 13440
		// (set) Token: 0x06003481 RID: 13441
		string Text { get; set; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003482 RID: 13442
		// (set) Token: 0x06003483 RID: 13443
		string ValidationGroup { get; set; }
	}
}
