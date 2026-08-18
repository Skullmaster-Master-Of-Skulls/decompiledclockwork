using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000753 RID: 1875
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomFormReq : BaseMessageReq
	{
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x00011F73 File Offset: 0x00010173
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x00011F7B File Offset: 0x0001017B
		[DataMember]
		public CustomFormDTO Form { get; set; }
	}
}
