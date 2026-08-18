using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000668 RID: 1640
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationDataForMultipleStudentsAsDataTableReq : BaseMessageReq
	{
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0000F208 File Offset: 0x0000D408
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x0000F210 File Offset: 0x0000D410
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0000F219 File Offset: 0x0000D419
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x0000F221 File Offset: 0x0000D421
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
