using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000825 RID: 2085
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetCourseLetterDateByStudentAndCourseReq : BaseMessageReq
	{
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000142C6 File Offset: 0x000124C6
		// (set) Token: 0x06002A81 RID: 10881 RVA: 0x000142CE File Offset: 0x000124CE
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000142D7 File Offset: 0x000124D7
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x000142DF File Offset: 0x000124DF
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000142E8 File Offset: 0x000124E8
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x000142F0 File Offset: 0x000124F0
		[DataMember]
		public DateTime? Date { get; set; }
	}
}
