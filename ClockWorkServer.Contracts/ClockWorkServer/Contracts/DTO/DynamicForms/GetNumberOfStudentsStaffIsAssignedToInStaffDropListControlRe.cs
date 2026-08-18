using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066D RID: 1645
	public class GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq : BaseMessageReq
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0000F2B2 File Offset: 0x0000D4B2
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0000F2BA File Offset: 0x0000D4BA
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0000F2C3 File Offset: 0x0000D4C3
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0000F2CB File Offset: 0x0000D4CB
		[DataMember]
		public int PersonId { get; set; }
	}
}
