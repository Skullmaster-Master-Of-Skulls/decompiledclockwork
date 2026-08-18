using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000233 RID: 563
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFilesQueueFileItemsByStudentReq : BaseMessageReq
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00005D41 File Offset: 0x00003F41
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00005D49 File Offset: 0x00003F49
		[DataMember]
		public int PersonId { get; set; }
	}
}
