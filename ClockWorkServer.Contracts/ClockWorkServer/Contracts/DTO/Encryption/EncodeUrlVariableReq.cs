using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000612 RID: 1554
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncodeUrlVariableReq : BaseMessageReq
	{
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x0000E601 File Offset: 0x0000C801
		[DataMember]
		public string VariableValue { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0000E60A File Offset: 0x0000C80A
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x0000E612 File Offset: 0x0000C812
		[DataMember]
		public bool IsEncrypted { get; set; }
	}
}
