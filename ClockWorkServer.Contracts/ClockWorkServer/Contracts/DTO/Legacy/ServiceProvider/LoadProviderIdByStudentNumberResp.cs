using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004DA RID: 1242
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderIdByStudentNumberResp
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
