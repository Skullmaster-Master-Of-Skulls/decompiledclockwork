using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC1 RID: 3009
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobByMediaContentAndFormatResp
	{
		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x0001F405 File Offset: 0x0001D605
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x0001F40D File Offset: 0x0001D60D
		[DataMember]
		public IList<CompletedMediaJobDTO> MediaJobs { get; set; }
	}
}
