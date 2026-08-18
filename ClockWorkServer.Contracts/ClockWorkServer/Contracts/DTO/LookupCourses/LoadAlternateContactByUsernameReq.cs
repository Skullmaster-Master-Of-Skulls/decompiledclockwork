using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078F RID: 1935
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByUsernameReq : BaseMessageReq
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00012C41 File Offset: 0x00010E41
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x00012C49 File Offset: 0x00010E49
		[DataMember]
		public string Username { get; set; }
	}
}
