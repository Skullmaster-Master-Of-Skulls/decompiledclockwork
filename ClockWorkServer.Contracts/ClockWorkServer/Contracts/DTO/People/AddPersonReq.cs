using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000373 RID: 883
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddPersonReq : BaseMessageReq
	{
		// Token: 0x040006A7 RID: 1703
		[DataMember]
		public PersonBaseDTO Person;

		// Token: 0x040006A8 RID: 1704
		[DataMember]
		public eCoreGroupDTO CoreGroup;
	}
}
