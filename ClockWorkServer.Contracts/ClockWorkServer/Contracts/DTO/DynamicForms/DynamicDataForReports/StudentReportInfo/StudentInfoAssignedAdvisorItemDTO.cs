using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020006BE RID: 1726
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class StudentInfoAssignedAdvisorItemDTO : StudentInfoItemBaseDTO
	{
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000100BB File Offset: 0x0000E2BB
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x000100C3 File Offset: 0x0000E2C3
		[DataMember]
		public int AdvisorPersonId { get; set; }

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000100CC File Offset: 0x0000E2CC
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x000100D4 File Offset: 0x0000E2D4
		[DataMember]
		public string AdvisorFirstName { get; set; }

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000100DD File Offset: 0x0000E2DD
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x000100E5 File Offset: 0x0000E2E5
		[DataMember]
		public string AdvisorLastName { get; set; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000100EE File Offset: 0x0000E2EE
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x000100F6 File Offset: 0x0000E2F6
		[DataMember]
		public string AdvisorName { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000100FF File Offset: 0x0000E2FF
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x00010107 File Offset: 0x0000E307
		[DataMember]
		public string AdvisorTitle { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00010110 File Offset: 0x0000E310
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x00010118 File Offset: 0x0000E318
		[DataMember]
		public string AdvisorEmail { get; set; }

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x00010121 File Offset: 0x0000E321
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x00010129 File Offset: 0x0000E329
		[DataMember]
		public string AdvisorPhone { get; set; }
	}
}
