using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B5A RID: 2906
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableMediaContentFileByStudentIdResp
	{
		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x0001E4FF File Offset: 0x0001C6FF
		// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x0001E507 File Offset: 0x0001C707
		[DataMember]
		public IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> MediaContentFiles { get; set; }
	}
}
