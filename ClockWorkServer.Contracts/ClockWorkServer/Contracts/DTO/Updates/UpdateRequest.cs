using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000167 RID: 359
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequest : BaseMessageReq
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00003F45 File Offset: 0x00002145
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00003F4D File Offset: 0x0000214D
		[DataMember]
		public string FileType { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00003F56 File Offset: 0x00002156
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00003F5E File Offset: 0x0000215E
		[DataMember]
		public string ClientVersion { get; set; }
	}
}
