using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036F RID: 879
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindStudentBySearchStringResp
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0000976E File Offset: 0x0000796E
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00009776 File Offset: 0x00007976
		[DataMember]
		public IList<PersonBaseDTO> Students { get; set; }
	}
}
