using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000680 RID: 1664
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFieldReq : BaseMessageReq
	{
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0000F767 File Offset: 0x0000D967
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x0000F76F File Offset: 0x0000D96F
		[DataMember]
		public DynamicFieldDTO Field { get; set; }
	}
}
