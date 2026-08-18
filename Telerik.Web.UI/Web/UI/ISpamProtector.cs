using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020016BF RID: 5823
	internal interface ISpamProtector
	{
		// Token: 0x170044E3 RID: 17635
		// (get) Token: 0x0600E0BE RID: 57534
		bool IsValid { get; }

		// Token: 0x0600E0BF RID: 57535
		void AddChildControls(Control container);

		// Token: 0x0600E0C0 RID: 57536
		void LoadPostBackData(Control container);

		// Token: 0x0600E0C1 RID: 57537
		void ValidatePostBackData();

		// Token: 0x0600E0C2 RID: 57538
		void PreRenderHandler();

		// Token: 0x170044E4 RID: 17636
		// (get) Token: 0x0600E0C3 RID: 57539
		// (set) Token: 0x0600E0C4 RID: 57540
		bool Visible { get; set; }
	}
}
