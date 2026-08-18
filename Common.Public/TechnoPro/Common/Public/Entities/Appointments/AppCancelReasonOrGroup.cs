using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BF RID: 1215
	public class AppCancelReasonOrGroup
	{
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00027C38 File Offset: 0x00025E38
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x00027C40 File Offset: 0x00025E40
		public AppCancelReason AppCancelReason { get; set; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00027C49 File Offset: 0x00025E49
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x00027C51 File Offset: 0x00025E51
		public AppCancelReasonGroup AppCancelReasonGroup { get; set; }
	}
}
