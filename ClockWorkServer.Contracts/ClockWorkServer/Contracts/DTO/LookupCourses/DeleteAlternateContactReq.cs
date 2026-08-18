using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079C RID: 1948
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAlternateContactReq : BaseMessageReq
	{
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x00012D40 File Offset: 0x00010F40
		// (set) Token: 0x06002802 RID: 10242 RVA: 0x00012D48 File Offset: 0x00010F48
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}
