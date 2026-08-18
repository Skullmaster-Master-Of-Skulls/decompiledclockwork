using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Settings
{
	// Token: 0x0200026C RID: 620
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetValuesFromColumnsResp
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00006B83 File Offset: 0x00004D83
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00006B8B File Offset: 0x00004D8B
		[DataMember]
		public IList<KeyValuePair<int, string[]>> Values { get; set; }
	}
}
