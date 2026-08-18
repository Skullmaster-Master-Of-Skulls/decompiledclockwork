using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018D RID: 397
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentMyTutorsResp
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0000431D File Offset: 0x0000251D
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00004325 File Offset: 0x00002525
		[DataMember]
		public IList<MyTutorDTO> MyTutors { get; set; }
	}
}
