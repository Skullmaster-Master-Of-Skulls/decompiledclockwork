using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000678 RID: 1656
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByFormIdReq : BaseMessageReq
	{
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0000F6BD File Offset: 0x0000D8BD
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0000F6C5 File Offset: 0x0000D8C5
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0000F6CE File Offset: 0x0000D8CE
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0000F6D6 File Offset: 0x0000D8D6
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
