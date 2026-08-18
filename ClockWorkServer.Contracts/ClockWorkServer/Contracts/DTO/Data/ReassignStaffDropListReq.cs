using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F2 RID: 1778
	public class ReassignStaffDropListReq : BaseMessageReq
	{
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x0001094C File Offset: 0x0000EB4C
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x00010954 File Offset: 0x0000EB54
		[DataMember]
		public int StaffDropListControlId { get; set; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x0001095D File Offset: 0x0000EB5D
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x00010965 File Offset: 0x0000EB65
		[DataMember]
		public int StaffPidOld { get; set; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x0001096E File Offset: 0x0000EB6E
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x00010976 File Offset: 0x0000EB76
		[DataMember]
		public int StaffPidNew { get; set; }
	}
}
