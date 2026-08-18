using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C4D RID: 3149
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddStudentContentMediaRequestInfoResp
	{
		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060041C7 RID: 16839 RVA: 0x0002026E File Offset: 0x0001E46E
		// (set) Token: 0x060041C8 RID: 16840 RVA: 0x00020276 File Offset: 0x0001E476
		[DataMember]
		public int MediaContentRequestedInfoId { get; set; }
	}
}
