using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E4 RID: 2276
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindStudentByUserNameResp
	{
		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x00015E91 File Offset: 0x00014091
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x00015E99 File Offset: 0x00014099
		[DataMember]
		public PersonBaseDTO Student { get; set; }
	}
}
