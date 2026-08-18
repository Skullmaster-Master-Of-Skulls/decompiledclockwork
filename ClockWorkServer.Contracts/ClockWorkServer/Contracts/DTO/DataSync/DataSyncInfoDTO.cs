using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070E RID: 1806
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncInfoDTO
	{
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000110CD File Offset: 0x0000F2CD
		// (set) Token: 0x0600254D RID: 9549 RVA: 0x000110D5 File Offset: 0x0000F2D5
		[DataMember]
		public int PreviewStudentDataReportId { get; set; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000110DE File Offset: 0x0000F2DE
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x000110E6 File Offset: 0x0000F2E6
		[DataMember]
		public int ImportStudentDataReportId { get; set; }

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000110EF File Offset: 0x0000F2EF
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x000110F7 File Offset: 0x0000F2F7
		[DataMember]
		public int ImportStudentCoursesReportId { get; set; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00011100 File Offset: 0x0000F300
		// (set) Token: 0x06002553 RID: 9555 RVA: 0x00011108 File Offset: 0x0000F308
		[DataMember]
		public int BatchDataSyncReportId { get; set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x00011111 File Offset: 0x0000F311
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x00011119 File Offset: 0x0000F319
		[DataMember]
		public int MoveDataIntoClockWorkReportId { get; set; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00011122 File Offset: 0x0000F322
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x0001112A File Offset: 0x0000F32A
		[DataMember]
		public int GroupsReportId { get; set; }
	}
}
