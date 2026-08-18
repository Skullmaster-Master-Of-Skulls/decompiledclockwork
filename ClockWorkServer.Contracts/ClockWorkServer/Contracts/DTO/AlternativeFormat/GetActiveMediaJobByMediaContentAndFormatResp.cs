using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAD RID: 2989
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobByMediaContentAndFormatResp
	{
		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x0001F24B File Offset: 0x0001D44B
		// (set) Token: 0x06003F42 RID: 16194 RVA: 0x0001F253 File Offset: 0x0001D453
		[DataMember]
		public IList<MediaJobDTO> MediaJobs { get; set; }
	}
}
