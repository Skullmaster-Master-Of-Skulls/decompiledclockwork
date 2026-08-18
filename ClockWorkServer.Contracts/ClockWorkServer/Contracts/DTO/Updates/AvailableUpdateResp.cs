using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x0200016A RID: 362
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailableUpdateResp
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00003F78 File Offset: 0x00002178
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00003F80 File Offset: 0x00002180
		[DataMember]
		public IList<UpdateFileInfoDTO> UpdatesInfo { get; set; }
	}
}
