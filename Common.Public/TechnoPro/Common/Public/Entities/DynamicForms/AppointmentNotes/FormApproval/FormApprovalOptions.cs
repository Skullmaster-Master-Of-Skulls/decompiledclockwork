using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B4 RID: 948
	public class FormApprovalOptions
	{
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00020E38 File Offset: 0x0001F038
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x00020E40 File Offset: 0x0001F040
		public bool IsEnabled { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00020E49 File Offset: 0x0001F049
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x00020E51 File Offset: 0x0001F051
		public int ScreenNum { get; set; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x00020E5A File Offset: 0x0001F05A
		// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x00020E62 File Offset: 0x0001F062
		public int[] SupervisorGroupIds { get; set; }

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00020E6B File Offset: 0x0001F06B
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x00020E73 File Offset: 0x0001F073
		public int[] ExemptGroupIds { get; set; }
	}
}
