using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067E RID: 1662
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldByNameReq : BaseMessageReq
	{
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x0000F745 File Offset: 0x0000D945
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x0000F74D File Offset: 0x0000D94D
		[DataMember]
		public string Name { get; set; }
	}
}
