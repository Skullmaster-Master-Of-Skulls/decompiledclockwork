using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039B RID: 923
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonsByUsernameResp
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00009C1C File Offset: 0x00007E1C
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00009C24 File Offset: 0x00007E24
		[DataMember]
		public IList<PersonBaseDTO> People { get; set; }
	}
}
