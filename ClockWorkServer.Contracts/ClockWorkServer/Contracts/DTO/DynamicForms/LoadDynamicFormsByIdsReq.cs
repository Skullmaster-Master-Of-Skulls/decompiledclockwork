using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A2 RID: 1698
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDynamicFormsByIdsReq : BaseMessageReq
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0000FC24 File Offset: 0x0000DE24
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
