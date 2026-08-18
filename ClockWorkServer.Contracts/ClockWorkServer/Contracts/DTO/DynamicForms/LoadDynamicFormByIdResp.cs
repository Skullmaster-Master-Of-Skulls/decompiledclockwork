using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200069B RID: 1691
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDynamicFormByIdResp
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x0000FB94 File Offset: 0x0000DD94
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		[DataMember]
		public DynamicFormDTO DynamicForm { get; set; }
	}
}
