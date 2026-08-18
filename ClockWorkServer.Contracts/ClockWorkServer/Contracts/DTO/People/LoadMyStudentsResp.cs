using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BD RID: 957
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMyStudentsResp
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0000A01C File Offset: 0x0000821C
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0000A024 File Offset: 0x00008224
		[DataMember]
		public IList<StudentWithCommonInfoDTO> StudentsWithCommonInfo { get; set; }
	}
}
