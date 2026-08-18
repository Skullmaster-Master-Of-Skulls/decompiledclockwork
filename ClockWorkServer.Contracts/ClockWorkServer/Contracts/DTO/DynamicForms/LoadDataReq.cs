using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064C RID: 1612
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataReq : BaseMessageReq
	{
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0000EEFA File Offset: 0x0000D0FA
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0000EF02 File Offset: 0x0000D102
		[DataMember]
		public DynamicFormDTO Form { get; set; }
	}
}
