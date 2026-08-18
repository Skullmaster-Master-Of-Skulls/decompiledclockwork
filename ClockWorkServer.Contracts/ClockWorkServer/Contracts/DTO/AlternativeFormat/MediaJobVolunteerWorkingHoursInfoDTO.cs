using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BEB RID: 3051
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaJobVolunteerWorkingHoursInfoDTO
	{
		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		// (set) Token: 0x06004062 RID: 16482 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		[DataMember]
		public int JobVolunteerWorkingHoursId { get; set; }

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x0001F9DD File Offset: 0x0001DBDD
		// (set) Token: 0x06004064 RID: 16484 RVA: 0x0001F9E5 File Offset: 0x0001DBE5
		[DataMember]
		public AlternateFormatVolunteerDTO Volunteer { get; set; }

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x0001F9EE File Offset: 0x0001DBEE
		// (set) Token: 0x06004066 RID: 16486 RVA: 0x0001F9F6 File Offset: 0x0001DBF6
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x0001F9FF File Offset: 0x0001DBFF
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x0001FA07 File Offset: 0x0001DC07
		[DataMember]
		public DateTime StartWorkingTime { get; set; }

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x0001FA10 File Offset: 0x0001DC10
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x0001FA18 File Offset: 0x0001DC18
		[DataMember]
		public DateTime EndWorkingTime { get; set; }

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x0001FA21 File Offset: 0x0001DC21
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x0001FA29 File Offset: 0x0001DC29
		[DataMember]
		public PersonBaseDTO WhoAddWorkingHours { get; set; }

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x0001FA32 File Offset: 0x0001DC32
		// (set) Token: 0x0600406E RID: 16494 RVA: 0x0001FA3A File Offset: 0x0001DC3A
		[DataMember]
		public string VolunteerWorkingHoursNotes { get; set; }
	}
}
