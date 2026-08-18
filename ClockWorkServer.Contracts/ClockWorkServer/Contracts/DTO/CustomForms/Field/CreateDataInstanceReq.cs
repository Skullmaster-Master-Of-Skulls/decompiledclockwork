using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x0200075C RID: 1884
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateDataInstanceReq : BaseMessageReq
	{
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0001201D File Offset: 0x0001021D
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x00012025 File Offset: 0x00010225
		[DataMember]
		public CustomDataInstanceDTO DataInstance { get; set; }
	}
}
