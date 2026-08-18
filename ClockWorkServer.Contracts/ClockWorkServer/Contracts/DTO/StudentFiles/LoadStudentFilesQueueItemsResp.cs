using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000236 RID: 566
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFilesQueueItemsResp
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00005D74 File Offset: 0x00003F74
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00005D7C File Offset: 0x00003F7C
		[DataMember]
		public StudentFilesQueueItemsDTO QueueItems { get; set; }
	}
}
