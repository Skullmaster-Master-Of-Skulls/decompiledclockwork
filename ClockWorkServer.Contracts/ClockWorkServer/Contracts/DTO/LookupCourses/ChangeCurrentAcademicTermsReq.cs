using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C0 RID: 1984
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeCurrentAcademicTermsReq : BaseMessageReq
	{
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0001346B File Offset: 0x0001166B
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x00013473 File Offset: 0x00011673
		[DataMember]
		public IList<AcademicTermDTO> AcademicTermList { get; set; }
	}
}
