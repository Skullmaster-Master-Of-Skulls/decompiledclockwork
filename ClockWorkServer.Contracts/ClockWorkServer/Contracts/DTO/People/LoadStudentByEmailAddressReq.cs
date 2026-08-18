using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BA RID: 954
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentByEmailAddressReq : BaseMessageReq
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00009F72 File Offset: 0x00008172
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00009F7A File Offset: 0x0000817A
		[DataMember]
		public string EmailAddress { get; set; }
	}
}
