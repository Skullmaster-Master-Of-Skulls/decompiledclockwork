using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000237 RID: 567
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateStudentFilesQueueStudentItemReq : BaseMessageReq
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00005D85 File Offset: 0x00003F85
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00005D8D File Offset: 0x00003F8D
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00005D96 File Offset: 0x00003F96
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00005D9E File Offset: 0x00003F9E
		[DataMember]
		public IList<StudentFilesQueueFileItemDTO> AllUpdatedFileItemsForStudent { get; set; }
	}
}
