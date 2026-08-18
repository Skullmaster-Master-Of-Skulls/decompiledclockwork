using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Veteran
{
	// Token: 0x0200011F RID: 287
	[DataContract(Namespace = "http://tpro.ca")]
	public class BenefitApplicationDTO
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00003316 File Offset: 0x00001516
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0000331E File Offset: 0x0000151E
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00003327 File Offset: 0x00001527
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0000332F File Offset: 0x0000152F
		[DataMember]
		public SemesterDTO Semester { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00003338 File Offset: 0x00001538
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x00003340 File Offset: 0x00001540
		[DataMember]
		public VeteranChapterDTO Chapter { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00003349 File Offset: 0x00001549
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x00003351 File Offset: 0x00001551
		[DataMember]
		public eVeteranRequestStatus CounselorStatus { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000335A File Offset: 0x0000155A
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x00003362 File Offset: 0x00001562
		[DataMember]
		public eVeteranRequestStatus AdministratorStatus { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0000336B File Offset: 0x0000156B
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00003373 File Offset: 0x00001573
		[DataMember]
		public bool RegistrationComplete { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000337C File Offset: 0x0000157C
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00003384 File Offset: 0x00001584
		[DataMember]
		public bool ConsentAgreementComplete { get; set; }
	}
}
