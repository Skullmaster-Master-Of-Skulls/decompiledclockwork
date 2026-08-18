using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000690 RID: 1680
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadControlIdsOnFormsReq : BaseMessageReq
	{
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0000F888 File Offset: 0x0000DA88
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x0000F890 File Offset: 0x0000DA90
		[DataMember]
		public IList<int> ScreenNums { get; set; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0000F899 File Offset: 0x0000DA99
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x0000F8A1 File Offset: 0x0000DAA1
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
