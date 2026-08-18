using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000618 RID: 1560
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetBatchLetterSentDatesResp
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0000E681 File Offset: 0x0000C881
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x0000E689 File Offset: 0x0000C889
		[DataMember]
		public IDictionary<int, DateTime?> BatchLetterSentDates { get; set; }
	}
}
