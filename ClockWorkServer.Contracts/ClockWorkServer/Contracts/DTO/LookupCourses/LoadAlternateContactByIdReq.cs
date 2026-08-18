using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000795 RID: 1941
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByIdReq : BaseMessageReq
	{
		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00012CC9 File Offset: 0x00010EC9
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x00012CD1 File Offset: 0x00010ED1
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}
