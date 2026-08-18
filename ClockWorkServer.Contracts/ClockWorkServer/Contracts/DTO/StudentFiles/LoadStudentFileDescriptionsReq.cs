using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000228 RID: 552
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFileDescriptionsReq : BaseMessageReq
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00005B6C File Offset: 0x00003D6C
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x00005B74 File Offset: 0x00003D74
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
