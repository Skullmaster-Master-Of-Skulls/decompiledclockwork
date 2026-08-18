using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000682 RID: 1666
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormsWithControls2Req : BaseMessageReq
	{
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0000F789 File Offset: 0x0000D989
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x0000F791 File Offset: 0x0000D991
		[DataMember]
		public bool ExcludeNonDataHoldingControls { get; set; }

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0000F79A File Offset: 0x0000D99A
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x0000F7A2 File Offset: 0x0000D9A2
		[DataMember]
		public IList<int> ScreenNumsToExclude { get; set; }
	}
}
