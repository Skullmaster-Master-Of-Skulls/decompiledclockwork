using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006AA RID: 1706
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFormReq : BaseMessageReq
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		[DataMember]
		public DynamicFormWithExtendedInfoDTO Form { get; set; }
	}
}
