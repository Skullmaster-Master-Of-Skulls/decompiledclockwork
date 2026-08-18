using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D9 RID: 1241
	public class LoadProviderIdByStudentNumberReq : BaseMessageReq
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
