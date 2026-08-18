using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes
{
	// Token: 0x020006C2 RID: 1730
	[DataContract(Namespace = "http://tpro.ca")]
	public class NotesAppointmentDTO
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x00010176 File Offset: 0x0000E376
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x0001017E File Offset: 0x0000E37E
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00010187 File Offset: 0x0000E387
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x0001018F File Offset: 0x0000E38F
		[DataMember]
		public DateTime DateBooked { get; set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00010198 File Offset: 0x0000E398
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x000101A0 File Offset: 0x0000E3A0
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000101A9 File Offset: 0x0000E3A9
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x000101B1 File Offset: 0x0000E3B1
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000101BA File Offset: 0x0000E3BA
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x000101C2 File Offset: 0x0000E3C2
		[DataMember]
		public AppTypeDTO AppointmentType { get; set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000101CB File Offset: 0x0000E3CB
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x000101D3 File Offset: 0x0000E3D3
		[DataMember]
		public AppShowTimeAsTypeDTO ShowTimeAs { get; set; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000101DC File Offset: 0x0000E3DC
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x000101E4 File Offset: 0x0000E3E4
		[DataMember]
		public bool IsPrimaryStudentNoShow { get; set; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000101ED File Offset: 0x0000E3ED
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x000101F5 File Offset: 0x0000E3F5
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000101FE File Offset: 0x0000E3FE
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x00010206 File Offset: 0x0000E406
		[DataMember]
		public string CancelReason { get; set; }

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0001020F File Offset: 0x0000E40F
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x00010217 File Offset: 0x0000E417
		[DataMember]
		public bool HasNotes { get; set; }

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x00010220 File Offset: 0x0000E420
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x00010228 File Offset: 0x0000E428
		[DataMember]
		public bool IsPrivate { get; set; }

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00010231 File Offset: 0x0000E431
		// (set) Token: 0x0600234E RID: 9038 RVA: 0x00010239 File Offset: 0x0000E439
		[DataMember]
		public bool IsLocked { get; set; }

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00010242 File Offset: 0x0000E442
		// (set) Token: 0x06002350 RID: 9040 RVA: 0x0001024A File Offset: 0x0000E44A
		[DataMember]
		public int WhoBookedPersonId { get; set; }

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00010253 File Offset: 0x0000E453
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x0001025B File Offset: 0x0000E45B
		[DataMember]
		public PersonBaseDTO PrimaryStudent { get; set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00010264 File Offset: 0x0000E464
		// (set) Token: 0x06002354 RID: 9044 RVA: 0x0001026C File Offset: 0x0000E46C
		[DataMember]
		public IList<AttendeeDTO> Attendees { get; set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x00010275 File Offset: 0x0000E475
		// (set) Token: 0x06002356 RID: 9046 RVA: 0x0001027D File Offset: 0x0000E47D
		[DataMember]
		public string MemoText { get; set; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00010286 File Offset: 0x0000E486
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x0001028E File Offset: 0x0000E48E
		[DataMember]
		public string Subtitle { get; set; }
	}
}
