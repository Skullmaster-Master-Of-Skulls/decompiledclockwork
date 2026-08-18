using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040D RID: 1037
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentOnlineFormsReq : BaseMessageReq
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0000A79D File Offset: 0x0000899D
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x0000A7A5 File Offset: 0x000089A5
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
