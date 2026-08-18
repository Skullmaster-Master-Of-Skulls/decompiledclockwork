using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E4 RID: 1508
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadIntakeFormDataReq : BaseMessageReq
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0000DF7F File Offset: 0x0000C17F
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x0000DF87 File Offset: 0x0000C187
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
