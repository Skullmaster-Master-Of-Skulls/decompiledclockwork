using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067A RID: 1658
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByFormReq : BaseMessageReq
	{
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		[DataMember]
		public DynamicFormDTO Form { get; set; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0000F701 File Offset: 0x0000D901
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0000F709 File Offset: 0x0000D909
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
