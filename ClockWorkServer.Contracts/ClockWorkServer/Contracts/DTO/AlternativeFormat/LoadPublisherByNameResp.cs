using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C1B RID: 3099
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPublisherByNameResp
	{
		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x0001FDD9 File Offset: 0x0001DFD9
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x0001FDE1 File Offset: 0x0001DFE1
		[DataMember]
		public MediaPublisherDTO MediaPublisher { get; set; }
	}
}
