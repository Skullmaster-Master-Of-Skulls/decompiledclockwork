using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x0200075E RID: 1886
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteDataInstanceReq : BaseMessageReq
	{
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x0001203F File Offset: 0x0001023F
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x00012047 File Offset: 0x00010247
		[DataMember]
		public Guid DataInstanceId { get; set; }
	}
}
