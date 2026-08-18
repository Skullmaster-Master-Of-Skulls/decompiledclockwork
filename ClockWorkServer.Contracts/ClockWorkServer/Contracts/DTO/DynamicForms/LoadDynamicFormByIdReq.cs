using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200069A RID: 1690
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDynamicFormByIdReq : BaseMessageReq
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0000FB83 File Offset: 0x0000DD83
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x0000FB8B File Offset: 0x0000DD8B
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
