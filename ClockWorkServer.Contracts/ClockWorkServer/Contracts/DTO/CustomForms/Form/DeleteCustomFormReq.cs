using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000755 RID: 1877
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCustomFormReq : BaseMessageReq
	{
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00011F95 File Offset: 0x00010195
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00011F9D File Offset: 0x0001019D
		[DataMember]
		public Guid FormId { get; set; }
	}
}
