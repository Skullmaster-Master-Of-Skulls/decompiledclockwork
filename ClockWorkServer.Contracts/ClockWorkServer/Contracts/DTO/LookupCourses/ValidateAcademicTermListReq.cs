using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C4 RID: 1988
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateAcademicTermListReq : BaseMessageReq
	{
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0001348D File Offset: 0x0001168D
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x00013495 File Offset: 0x00011695
		[DataMember]
		public IList<AcademicTermDTO> ProposedTermsList { get; set; }
	}
}
