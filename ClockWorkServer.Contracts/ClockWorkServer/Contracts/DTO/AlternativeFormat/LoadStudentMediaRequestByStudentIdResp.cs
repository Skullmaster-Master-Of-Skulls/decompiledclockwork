using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C35 RID: 3125
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByStudentIdResp
	{
		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0002003D File Offset: 0x0001E23D
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x00020045 File Offset: 0x0001E245
		[DataMember]
		public IList<StudentMediaRequestDTO> StudentMediaRequests { get; set; }
	}
}
