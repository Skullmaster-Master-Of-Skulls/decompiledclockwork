using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003C8 RID: 968
	[DataContract(Namespace = "http://tpro.ca")]
	public class AdminUpdateGroupReq : BaseMessageReq
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0000A14E File Offset: 0x0000834E
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x0000A156 File Offset: 0x00008356
		[DataMember]
		public GroupDTO Group { get; set; }
	}
}
