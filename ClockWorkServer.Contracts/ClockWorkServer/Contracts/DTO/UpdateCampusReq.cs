using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F2 RID: 242
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCampusReq : BaseMessageReq
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000297B File Offset: 0x00000B7B
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00002983 File Offset: 0x00000B83
		[DataMember]
		public SchoolCampusDTO Campus { get; set; }
	}
}
