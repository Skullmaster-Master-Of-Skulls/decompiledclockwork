using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E6 RID: 1510
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetIntakeStatusesReq : BaseMessageReq
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0000DFA1 File Offset: 0x0000C1A1
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		[DataMember]
		public string[] StudentNumbers { get; set; }
	}
}
