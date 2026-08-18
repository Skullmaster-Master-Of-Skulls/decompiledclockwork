using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F0 RID: 752
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllSeatsReq : BaseMessageReq
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00008161 File Offset: 0x00006361
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00008169 File Offset: 0x00006369
		[DataMember]
		public bool IgnoreCache { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00008172 File Offset: 0x00006372
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0000817A File Offset: 0x0000637A
		[DataMember]
		public string ClockWorkSettingsInstanceName { get; set; }
	}
}
