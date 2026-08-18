using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A1 RID: 929
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTempStudentNumberResp
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00009C93 File Offset: 0x00007E93
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00009C9B File Offset: 0x00007E9B
		[DataMember]
		public string TempStudentNumber { get; set; }
	}
}
