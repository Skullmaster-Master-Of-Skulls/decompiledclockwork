using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000751 RID: 1873
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormByIdReq : BaseMessageReq
	{
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x00011F51 File Offset: 0x00010151
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x00011F59 File Offset: 0x00010159
		[DataMember]
		public Guid FormId { get; set; }
	}
}
