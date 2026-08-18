using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000234 RID: 564
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFilesQueueFileItemsByStudentResp
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00005D52 File Offset: 0x00003F52
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00005D5A File Offset: 0x00003F5A
		[DataMember]
		public IList<StudentFilesQueueFileItemDTO> Items { get; set; }
	}
}
