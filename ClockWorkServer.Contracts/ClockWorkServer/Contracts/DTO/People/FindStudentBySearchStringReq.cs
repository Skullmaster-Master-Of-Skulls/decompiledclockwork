using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036E RID: 878
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindStudentBySearchStringReq : BaseMessageReq
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0000975D File Offset: 0x0000795D
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x00009765 File Offset: 0x00007965
		[DataMember]
		public string SearchString { get; set; }
	}
}
