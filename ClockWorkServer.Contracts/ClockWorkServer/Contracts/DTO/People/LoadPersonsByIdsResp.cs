using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039F RID: 927
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonsByIdsResp
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00009C60 File Offset: 0x00007E60
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00009C68 File Offset: 0x00007E68
		[DataMember]
		public IList<PersonBaseDTO> Persons { get; set; }
	}
}
