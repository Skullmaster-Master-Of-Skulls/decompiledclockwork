using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B6 RID: 950
	public class FormApprovalScreenUserOptions
	{
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x00020F60 File Offset: 0x0001F160
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x00020F68 File Offset: 0x0001F168
		public int ScreenNum { get; set; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x00020F71 File Offset: 0x0001F171
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00020F79 File Offset: 0x0001F179
		public int PersonId { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x00020F82 File Offset: 0x0001F182
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x00020F8A File Offset: 0x0001F18A
		public bool IsEnabled { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00020F93 File Offset: 0x0001F193
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x00020F9B File Offset: 0x0001F19B
		public bool IsSupervisor { get; set; }
	}
}
