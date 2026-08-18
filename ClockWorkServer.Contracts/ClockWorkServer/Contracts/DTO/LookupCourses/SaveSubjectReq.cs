using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000803 RID: 2051
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveSubjectReq : BaseMessageReq
	{
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x00013DB4 File Offset: 0x00011FB4
		// (set) Token: 0x060029D2 RID: 10706 RVA: 0x00013DBC File Offset: 0x00011FBC
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }
	}
}
