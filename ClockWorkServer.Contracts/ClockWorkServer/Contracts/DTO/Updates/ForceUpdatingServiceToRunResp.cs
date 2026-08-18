using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000164 RID: 356
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForceUpdatingServiceToRunResp
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00003E8E File Offset: 0x0000208E
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00003E96 File Offset: 0x00002096
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00003E9F File Offset: 0x0000209F
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00003EA7 File Offset: 0x000020A7
		[DataMember]
		public bool Worked { get; set; }
	}
}
