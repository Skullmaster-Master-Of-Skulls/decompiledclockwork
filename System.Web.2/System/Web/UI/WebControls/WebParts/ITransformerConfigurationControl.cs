using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000543 RID: 1347
	public interface ITransformerConfigurationControl
	{
		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060044CB RID: 17611
		// (remove) Token: 0x060044CC RID: 17612
		event EventHandler Cancelled;

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060044CD RID: 17613
		// (remove) Token: 0x060044CE RID: 17614
		event EventHandler Succeeded;
	}
}
