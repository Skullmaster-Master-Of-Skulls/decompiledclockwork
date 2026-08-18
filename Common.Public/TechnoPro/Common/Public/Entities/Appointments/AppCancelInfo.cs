using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class AppCancelInfo
	{
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x00027CA7 File Offset: 0x00025EA7
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x00027CAF File Offset: 0x00025EAF
		public string CancelReasonText { get; set; }

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x00027CB8 File Offset: 0x00025EB8
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x00027CC0 File Offset: 0x00025EC0
		public AppCancelReason CancelReason { get; set; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00027CC9 File Offset: 0x00025EC9
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x00027CD1 File Offset: 0x00025ED1
		public PersonBase CancelledBy { get; set; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x00027CDA File Offset: 0x00025EDA
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x00027CE2 File Offset: 0x00025EE2
		public DateTime CancelledDate { get; set; }
	}
}
