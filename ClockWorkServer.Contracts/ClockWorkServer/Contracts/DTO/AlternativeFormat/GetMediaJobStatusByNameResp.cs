using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE5 RID: 3045
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobStatusByNameResp
	{
		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x0001F8CD File Offset: 0x0001DACD
		// (set) Token: 0x0600403E RID: 16446 RVA: 0x0001F8D5 File Offset: 0x0001DAD5
		[DataMember]
		public MediaJobStatusDTO MediaJobStatus { get; set; }
	}
}
