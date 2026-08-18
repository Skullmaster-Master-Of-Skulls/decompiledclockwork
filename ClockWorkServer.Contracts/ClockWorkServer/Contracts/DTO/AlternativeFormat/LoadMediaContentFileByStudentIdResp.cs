using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B58 RID: 2904
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByStudentIdResp
	{
		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x0001E4C3 File Offset: 0x0001C6C3
		[DataMember]
		public IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> MediaContentFiles { get; set; }
	}
}
