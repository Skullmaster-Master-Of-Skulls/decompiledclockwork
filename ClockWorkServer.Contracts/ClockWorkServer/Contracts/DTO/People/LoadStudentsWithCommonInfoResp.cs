using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BF RID: 959
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWithCommonInfoResp
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0000A03E File Offset: 0x0000823E
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0000A046 File Offset: 0x00008246
		[DataMember]
		public IList<StudentWithCommonInfoDTO> StudentsWithCommonInfo { get; set; }
	}
}
