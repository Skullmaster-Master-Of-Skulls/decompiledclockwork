using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070C RID: 1804
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseTimetableItemDTO
	{
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x00011023 File Offset: 0x0000F223
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x0001102B File Offset: 0x0000F22B
		[DataMember]
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00011034 File Offset: 0x0000F234
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x0001103C File Offset: 0x0000F23C
		[DataMember]
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x00011045 File Offset: 0x0000F245
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x0001104D File Offset: 0x0000F24D
		[DataMember]
		public TimeSpan EndTime { get; set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x00011056 File Offset: 0x0000F256
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x0001105E File Offset: 0x0000F25E
		[DataMember]
		public string Room { get; set; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00011067 File Offset: 0x0000F267
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x0001106F File Offset: 0x0000F26F
		[DataMember]
		public DataSyncExternalCourseInstructorDTO Instructor { get; set; }
	}
}
