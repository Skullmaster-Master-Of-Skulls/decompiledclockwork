using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000991 RID: 2449
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WizardPostBackCommand
	{
		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x06005D35 RID: 23861 RVA: 0x0011C714 File Offset: 0x0011A914
		// (set) Token: 0x06005D36 RID: 23862 RVA: 0x0011C71C File Offset: 0x0011A91C
		public RadWizardCommand Type { get; set; }

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x06005D37 RID: 23863 RVA: 0x0011C725 File Offset: 0x0011A925
		// (set) Token: 0x06005D38 RID: 23864 RVA: 0x0011C72D File Offset: 0x0011A92D
		public int Index { get; set; }
	}
}
