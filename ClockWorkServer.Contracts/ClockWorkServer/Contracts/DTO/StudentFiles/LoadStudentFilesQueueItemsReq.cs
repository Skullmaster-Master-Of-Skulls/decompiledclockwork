using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000235 RID: 565
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFilesQueueItemsReq : BaseMessageReq
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00005D63 File Offset: 0x00003F63
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00005D6B File Offset: 0x00003F6B
		[DataMember]
		public StudentFilesQueueLoadParametersDTO LoadParameters { get; set; }
	}
}
