using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C33 RID: 3123
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByIdResp
	{
		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0002000A File Offset: 0x0001E20A
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x00020012 File Offset: 0x0001E212
		[DataMember]
		public StudentMediaRequestDTO MediaRequest { get; set; }
	}
}
