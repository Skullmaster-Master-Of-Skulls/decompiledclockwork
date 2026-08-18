using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000238 RID: 568
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateStudentFilesQueueStudentItemResp
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00005DA7 File Offset: 0x00003FA7
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00005DAF File Offset: 0x00003FAF
		[DataMember]
		public IList<StudentFilesQueueFileItemDTO> ReloadedFilesItems { get; set; }
	}
}
