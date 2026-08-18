using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Academic
{
	// Token: 0x02000C9A RID: 3226
	[DataContract(Namespace = "http://tpro.ca")]
	public class SemesterDTO
	{
		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x00024687 File Offset: 0x00022887
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x0002468F File Offset: 0x0002288F
		[DataMember]
		public int SemesterId { get; set; }

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x00024698 File Offset: 0x00022898
		// (set) Token: 0x0600435B RID: 17243 RVA: 0x000246A0 File Offset: 0x000228A0
		[DataMember]
		public string SemesterTitle { get; set; }

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x000246A9 File Offset: 0x000228A9
		// (set) Token: 0x0600435D RID: 17245 RVA: 0x000246B1 File Offset: 0x000228B1
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000246BA File Offset: 0x000228BA
		// (set) Token: 0x0600435F RID: 17247 RVA: 0x000246C2 File Offset: 0x000228C2
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
