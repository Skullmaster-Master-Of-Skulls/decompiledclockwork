using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061D RID: 1565
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentAccommodationsExpiryDateReq : BaseMessageReq
	{
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0000E709 File Offset: 0x0000C909
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x0000E711 File Offset: 0x0000C911
		[DataMember]
		public int PersonId { get; set; }
	}
}
