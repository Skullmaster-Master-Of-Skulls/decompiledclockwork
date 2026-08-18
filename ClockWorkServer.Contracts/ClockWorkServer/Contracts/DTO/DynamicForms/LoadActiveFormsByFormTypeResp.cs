using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A1 RID: 1697
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadActiveFormsByFormTypeResp
	{
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0000FC0B File Offset: 0x0000DE0B
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0000FC13 File Offset: 0x0000DE13
		[DataMember]
		public IList<DynamicFormDTO> Forms { get; set; }
	}
}
