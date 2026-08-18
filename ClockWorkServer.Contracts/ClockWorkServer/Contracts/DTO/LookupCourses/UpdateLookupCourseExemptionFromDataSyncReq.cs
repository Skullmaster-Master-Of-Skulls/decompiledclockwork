using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C8 RID: 1992
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLookupCourseExemptionFromDataSyncReq : BaseMessageReq
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x000134D1 File Offset: 0x000116D1
		// (set) Token: 0x060028BE RID: 10430 RVA: 0x000134D9 File Offset: 0x000116D9
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000134E2 File Offset: 0x000116E2
		// (set) Token: 0x060028C0 RID: 10432 RVA: 0x000134EA File Offset: 0x000116EA
		[DataMember]
		public bool NewIsExempt { get; set; }
	}
}
