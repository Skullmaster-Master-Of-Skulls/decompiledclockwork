using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B9 RID: 185
	[DataContract(Namespace = "http://tpro.ca")]
	public class SettingsBaseMessageReq : BaseMessageReq
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00002507 File Offset: 0x00000707
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000250F File Offset: 0x0000070F
		[DataMember]
		public string InstanceName { get; set; }
	}
}
