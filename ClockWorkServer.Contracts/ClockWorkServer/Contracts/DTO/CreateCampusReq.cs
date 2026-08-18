using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F0 RID: 240
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCampusReq : BaseMessageReq
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00002959 File Offset: 0x00000B59
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00002961 File Offset: 0x00000B61
		[DataMember]
		public SchoolCampusDTO Campus { get; set; }
	}
}
