using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003EA RID: 1002
	[DataContract(Namespace = "http://tpro.ca")]
	public class PermanentlyDeleteStudentsReq : BaseMessageReq
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0000A34C File Offset: 0x0000854C
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0000A354 File Offset: 0x00008554
		[DataMember]
		public IList<int> StudentPersonIdsToDelete { get; set; }
	}
}
