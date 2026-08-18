using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000EF RID: 239
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCampusListResp
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00002948 File Offset: 0x00000B48
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00002950 File Offset: 0x00000B50
		[DataMember]
		public IList<SchoolCampusDTO> CampusList { get; set; }
	}
}
