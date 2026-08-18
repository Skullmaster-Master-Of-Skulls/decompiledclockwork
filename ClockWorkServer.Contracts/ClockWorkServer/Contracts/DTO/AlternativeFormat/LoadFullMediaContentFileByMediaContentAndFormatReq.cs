using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B65 RID: 2917
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFullMediaContentFileByMediaContentAndFormatReq : BaseMessageReq
	{
		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x0001E5E4 File Offset: 0x0001C7E4
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x0001E5F5 File Offset: 0x0001C7F5
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x0001E5FE File Offset: 0x0001C7FE
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x0001E606 File Offset: 0x0001C806
		[DataMember]
		public int StudentId { get; set; }
	}
}
