using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B7 RID: 951
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCommonInfoDTO
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00009EA6 File Offset: 0x000080A6
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x00009EAE File Offset: 0x000080AE
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00009EB7 File Offset: 0x000080B7
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x00009EBF File Offset: 0x000080BF
		[DataMember]
		public string Email { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00009EC8 File Offset: 0x000080C8
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x00009ED0 File Offset: 0x000080D0
		[DataMember]
		public bool OkToEmail { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00009ED9 File Offset: 0x000080D9
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00009EE1 File Offset: 0x000080E1
		[DataMember]
		public PersonBaseDTO AssignedCounsellor { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00009EEA File Offset: 0x000080EA
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00009EF2 File Offset: 0x000080F2
		[DataMember]
		public string AssignedCounsellorTitle { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00009EFB File Offset: 0x000080FB
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x00009F03 File Offset: 0x00008103
		[DataMember]
		public string AssignedCounsellorPhone { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00009F0C File Offset: 0x0000810C
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x00009F14 File Offset: 0x00008114
		[DataMember]
		public string AssignedCounsellorEmail { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00009F1D File Offset: 0x0000811D
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x00009F25 File Offset: 0x00008125
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00009F2E File Offset: 0x0000812E
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x00009F36 File Offset: 0x00008136
		[DataMember]
		public DateTime? DateOfBirth { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00009F3F File Offset: 0x0000813F
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x00009F47 File Offset: 0x00008147
		[DataMember]
		public eGender Gender { get; set; }
	}
}
