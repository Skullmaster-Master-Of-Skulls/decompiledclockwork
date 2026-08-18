using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B67 RID: 2919
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableMediaContentFileByStudentAndMediaContentResp
	{
		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x0001E653 File Offset: 0x0001C853
		// (set) Token: 0x06003DEC RID: 15852 RVA: 0x0001E65B File Offset: 0x0001C85B
		[DataMember]
		public IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> MediaContentFilesResult { get; set; }
	}
}
