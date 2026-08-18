using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FF RID: 2047
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectsBySessionReq : BaseMessageReq
	{
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x00013D70 File Offset: 0x00011F70
		// (set) Token: 0x060029C6 RID: 10694 RVA: 0x00013D78 File Offset: 0x00011F78
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
