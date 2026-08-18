using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000278 RID: 632
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestCourseAssignmentDTO
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00007139 File Offset: 0x00005339
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00007141 File Offset: 0x00005341
		[DataMember]
		public int SPRequestCourseAssignmentId { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0000714A File Offset: 0x0000534A
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00007152 File Offset: 0x00005352
		[DataMember]
		public SPProviderDTO Provider { get; set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0000715B File Offset: 0x0000535B
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00007163 File Offset: 0x00005363
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0000716C File Offset: 0x0000536C
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00007174 File Offset: 0x00005374
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0000717D File Offset: 0x0000537D
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00007185 File Offset: 0x00005385
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0000718E File Offset: 0x0000538E
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00007196 File Offset: 0x00005396
		[DataMember]
		public DateTime? DateCancelled { get; set; }
	}
}
