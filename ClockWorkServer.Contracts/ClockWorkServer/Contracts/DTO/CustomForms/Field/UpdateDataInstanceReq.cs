using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x02000760 RID: 1888
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateDataInstanceReq : BaseMessageReq
	{
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00012050 File Offset: 0x00010250
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x00012058 File Offset: 0x00010258
		[DataMember]
		public CustomDataInstanceDTO DataInstance { get; set; }
	}
}
