using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003EB RID: 1003
	[DataContract(Namespace = "http://tpro.ca")]
	public class PermanentlyDeleteStudentsResp
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0000A35D File Offset: 0x0000855D
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0000A365 File Offset: 0x00008565
		[DataMember]
		public IList<PersonBaseDTO> StudentsDeleted { get; set; }
	}
}
