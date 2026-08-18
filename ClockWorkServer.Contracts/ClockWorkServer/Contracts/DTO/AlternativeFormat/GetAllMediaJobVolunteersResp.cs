using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BEE RID: 3054
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllMediaJobVolunteersResp
	{
		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x0001FA43 File Offset: 0x0001DC43
		// (set) Token: 0x06004073 RID: 16499 RVA: 0x0001FA4B File Offset: 0x0001DC4B
		[DataMember]
		public IList<AlternateFormatVolunteerDTO> Volunteers { get; set; }
	}
}
