using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000239 RID: 569
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesQueueStudentItemDTO
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00005DB8 File Offset: 0x00003FB8
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00005DC0 File Offset: 0x00003FC0
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00005DC9 File Offset: 0x00003FC9
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00005DD1 File Offset: 0x00003FD1
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00005DDA File Offset: 0x00003FDA
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00005DE2 File Offset: 0x00003FE2
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00005DEB File Offset: 0x00003FEB
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00005DF3 File Offset: 0x00003FF3
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00005DFC File Offset: 0x00003FFC
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00005E04 File Offset: 0x00004004
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00005E0D File Offset: 0x0000400D
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00005E15 File Offset: 0x00004015
		[DataMember]
		public string Email { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00005E1E File Offset: 0x0000401E
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00005E26 File Offset: 0x00004026
		[DataMember]
		public string AssignedCounsellorFirstName { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00005E2F File Offset: 0x0000402F
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x00005E37 File Offset: 0x00004037
		[DataMember]
		public string AssignedCounsellorLastName { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00005E40 File Offset: 0x00004040
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00005E48 File Offset: 0x00004048
		[DataMember]
		public int AssignedCounsellorPersonId { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00005E51 File Offset: 0x00004051
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00005E59 File Offset: 0x00004059
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00005E62 File Offset: 0x00004062
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00005E6A File Offset: 0x0000406A
		[DataMember]
		public IList<StudentFilesQueueFileItemDTO> FileItems { get; set; }
	}
}
