using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x02000456 RID: 1110
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMiscSettingValueReq : BaseMessageReq
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0000AFC0 File Offset: 0x000091C0
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0000AFC8 File Offset: 0x000091C8
		[DataMember]
		public int Code { get; set; }
	}
}
