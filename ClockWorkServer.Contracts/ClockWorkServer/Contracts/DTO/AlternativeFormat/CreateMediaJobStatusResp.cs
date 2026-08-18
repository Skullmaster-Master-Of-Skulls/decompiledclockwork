using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE3 RID: 3043
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobStatusResp
	{
		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x0001F8AB File Offset: 0x0001DAAB
		// (set) Token: 0x06004038 RID: 16440 RVA: 0x0001F8B3 File Offset: 0x0001DAB3
		[DataMember]
		public int MediaJobStatusId { get; set; }
	}
}
