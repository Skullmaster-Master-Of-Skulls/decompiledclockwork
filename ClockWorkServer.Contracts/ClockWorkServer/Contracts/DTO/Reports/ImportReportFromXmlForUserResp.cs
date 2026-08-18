using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000337 RID: 823
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportReportFromXmlForUserResp
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000089B7 File Offset: 0x00006BB7
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000089BF File Offset: 0x00006BBF
		[DataMember]
		public IDictionary<string, int> UniqueIdsAndNewReportIds { get; set; }
	}
}
