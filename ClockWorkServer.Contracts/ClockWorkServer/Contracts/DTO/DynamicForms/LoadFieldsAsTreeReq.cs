using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000674 RID: 1652
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsAsTreeReq : BaseMessageReq
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0000F668 File Offset: 0x0000D868
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x0000F670 File Offset: 0x0000D870
		[DataMember]
		public DynamicFormDTO Form { get; set; }
	}
}
