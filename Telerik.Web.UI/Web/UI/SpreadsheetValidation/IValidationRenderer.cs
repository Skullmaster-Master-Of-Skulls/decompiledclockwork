using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetValidation
{
	// Token: 0x020008DC RID: 2268
	internal interface IValidationRenderer
	{
		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06005555 RID: 21845
		IValidationView View { get; }

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06005556 RID: 21846
		// (set) Token: 0x06005557 RID: 21847
		WebControl CriteriaPanel { get; set; }

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06005558 RID: 21848
		// (set) Token: 0x06005559 RID: 21849
		WebControl NumberCriteriaPanel { get; set; }

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x0600555A RID: 21850
		// (set) Token: 0x0600555B RID: 21851
		WebControl TextCriteriaPanel { get; set; }

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x0600555C RID: 21852
		// (set) Token: 0x0600555D RID: 21853
		WebControl DateCriteriaPanel { get; set; }

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x0600555E RID: 21854
		// (set) Token: 0x0600555F RID: 21855
		WebControl CustomCriteriaPanel { get; set; }

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06005560 RID: 21856
		// (set) Token: 0x06005561 RID: 21857
		WebControl InvalidDataPanel { get; set; }

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06005562 RID: 21858
		// (set) Token: 0x06005563 RID: 21859
		WebControl HintPanel { get; set; }

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06005564 RID: 21860
		// (set) Token: 0x06005565 RID: 21861
		Panel ButtonsPanel { get; set; }

		// Token: 0x06005566 RID: 21862
		void CreateLayout(Control container);

		// Token: 0x06005567 RID: 21863
		void CreateControls();
	}
}
