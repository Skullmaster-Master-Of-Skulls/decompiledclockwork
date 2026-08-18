using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C2D RID: 3117
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateStudentMediaResp
	{
		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0001FFC6 File Offset: 0x0001E1C6
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		[DataMember]
		public StudentMediaRequestDTO MediaRequest { get; set; }
	}
}
