using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A8 RID: 1704
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormsWithExtendedInfoByScreenNumsReq : BaseMessageReq
	{
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x0000FC82 File Offset: 0x0000DE82
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x0000FC8A File Offset: 0x0000DE8A
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
