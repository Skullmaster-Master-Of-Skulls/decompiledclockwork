using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068B RID: 1675
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFieldPossibleValuesResp
	{
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0000F811 File Offset: 0x0000DA11
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x0000F819 File Offset: 0x0000DA19
		[DataMember]
		public IList<string> Values { get; set; }
	}
}
