using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C9 RID: 1993
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDurationTermSubjectsBySessionReq : BaseMessageReq
	{
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000134F3 File Offset: 0x000116F3
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x000134FB File Offset: 0x000116FB
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
