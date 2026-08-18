using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A0 RID: 1696
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadActiveFormsByFormTypeReq : BaseMessageReq
	{
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0000FBFA File Offset: 0x0000DDFA
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x0000FC02 File Offset: 0x0000DE02
		[DataMember]
		public eDynamicFormTypeDTO[] FormTypes { get; set; }
	}
}
