using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F0 RID: 1776
	public class LoadAssignmentsForStaffDropListReq : BaseMessageReq
	{
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00010919 File Offset: 0x0000EB19
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x00010921 File Offset: 0x0000EB21
		[DataMember]
		public int StaffDropListControlId { get; set; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x0001092A File Offset: 0x0000EB2A
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x00010932 File Offset: 0x0000EB32
		[DataMember]
		public int StaffPid { get; set; }
	}
}
