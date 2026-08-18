using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BEA RID: 3050
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaJobVolunteerInfoDTO
	{
		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x0001F911 File Offset: 0x0001DB11
		// (set) Token: 0x0600404B RID: 16459 RVA: 0x0001F919 File Offset: 0x0001DB19
		[DataMember]
		public int JobVolunteerId { get; set; }

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x0001F922 File Offset: 0x0001DB22
		// (set) Token: 0x0600404D RID: 16461 RVA: 0x0001F92A File Offset: 0x0001DB2A
		[DataMember]
		public AlternateFormatVolunteerDTO Volunteer { get; set; }

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x0001F933 File Offset: 0x0001DB33
		// (set) Token: 0x0600404F RID: 16463 RVA: 0x0001F93B File Offset: 0x0001DB3B
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x0001F944 File Offset: 0x0001DB44
		// (set) Token: 0x06004051 RID: 16465 RVA: 0x0001F94C File Offset: 0x0001DB4C
		[DataMember]
		public DateTime MediaJobStartTime { get; set; }

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x0001F955 File Offset: 0x0001DB55
		// (set) Token: 0x06004053 RID: 16467 RVA: 0x0001F95D File Offset: 0x0001DB5D
		[DataMember]
		public DateTime MediaJobDueDate { get; set; }

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x0001F966 File Offset: 0x0001DB66
		// (set) Token: 0x06004055 RID: 16469 RVA: 0x0001F96E File Offset: 0x0001DB6E
		[DataMember]
		public string MediaContentTitle { get; set; }

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x0001F977 File Offset: 0x0001DB77
		// (set) Token: 0x06004057 RID: 16471 RVA: 0x0001F97F File Offset: 0x0001DB7F
		[DataMember]
		public MediaContentFormat MediaContentFormatName { get; set; }

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x0001F988 File Offset: 0x0001DB88
		// (set) Token: 0x06004059 RID: 16473 RVA: 0x0001F990 File Offset: 0x0001DB90
		[DataMember]
		public DateTime WhenWasAssigned { get; set; }

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x0001F999 File Offset: 0x0001DB99
		// (set) Token: 0x0600405B RID: 16475 RVA: 0x0001F9A1 File Offset: 0x0001DBA1
		[DataMember]
		public PersonBaseDTO WhoAssigned { get; set; }

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x0001F9B2 File Offset: 0x0001DBB2
		[DataMember]
		public string JobVolunteerNotes { get; set; }

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x0001F9BB File Offset: 0x0001DBBB
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x0001F9C3 File Offset: 0x0001DBC3
		[DataMember]
		public bool IsActive { get; set; }
	}
}
