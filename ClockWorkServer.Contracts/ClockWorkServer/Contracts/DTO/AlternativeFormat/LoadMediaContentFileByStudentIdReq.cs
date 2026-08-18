using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B57 RID: 2903
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByStudentIdReq : BaseMessageReq
	{
		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x0001E4AA File Offset: 0x0001C6AA
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x0001E4B2 File Offset: 0x0001C6B2
		[DataMember]
		public int StudentId { get; set; }
	}
}
