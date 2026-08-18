using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x0200025D RID: 605
	[DataContract(Namespace = "http://tpro.ca")]
	public class SelfRegCourseInfoDTO
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00006853 File Offset: 0x00004A53
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0000685B File Offset: 0x00004A5B
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00006864 File Offset: 0x00004A64
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0000686C File Offset: 0x00004A6C
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00006875 File Offset: 0x00004A75
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0000687D File Offset: 0x00004A7D
		[DataMember]
		public string EncodedLucidForUrl { get; set; }
	}
}
