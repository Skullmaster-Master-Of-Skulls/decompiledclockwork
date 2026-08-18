using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070A RID: 1802
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseStudentSpecificRowPartDTO
	{
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00010F02 File Offset: 0x0000F102
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00010F0A File Offset: 0x0000F10A
		[DataMember]
		public string GradeLetter { get; set; }

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x00010F13 File Offset: 0x0000F113
		// (set) Token: 0x06002515 RID: 9493 RVA: 0x00010F1B File Offset: 0x0000F11B
		[DataMember]
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00010F24 File Offset: 0x0000F124
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x00010F2C File Offset: 0x0000F12C
		[DataMember]
		public double Grade { get; set; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x00010F35 File Offset: 0x0000F135
		// (set) Token: 0x06002519 RID: 9497 RVA: 0x00010F3D File Offset: 0x0000F13D
		[DataMember]
		public double InProgressGrade { get; set; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00010F46 File Offset: 0x0000F146
		// (set) Token: 0x0600251B RID: 9499 RVA: 0x00010F4E File Offset: 0x0000F14E
		[DataMember]
		public double TuitionCost { get; set; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00010F57 File Offset: 0x0000F157
		// (set) Token: 0x0600251D RID: 9501 RVA: 0x00010F5F File Offset: 0x0000F15F
		[DataMember]
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00010F68 File Offset: 0x0000F168
		// (set) Token: 0x0600251F RID: 9503 RVA: 0x00010F70 File Offset: 0x0000F170
		[DataMember]
		public string RegistrationNote { get; set; }
	}
}
