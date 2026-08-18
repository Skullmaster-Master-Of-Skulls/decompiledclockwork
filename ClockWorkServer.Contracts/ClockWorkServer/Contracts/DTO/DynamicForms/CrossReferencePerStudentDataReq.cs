using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063A RID: 1594
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferencePerStudentDataReq : BaseMessageReq
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0000ECA7 File Offset: 0x0000CEA7
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x0000ECAF File Offset: 0x0000CEAF
		[DataMember]
		public DataTable TableWithData { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
