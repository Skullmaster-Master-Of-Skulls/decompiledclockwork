using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x0200016B RID: 363
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApplyUpdateReq : BaseMessageReq
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00003F89 File Offset: 0x00002189
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00003F91 File Offset: 0x00002191
		[DataMember]
		public IList<UpdateFileInfoDTO> Updates { get; set; }
	}
}
