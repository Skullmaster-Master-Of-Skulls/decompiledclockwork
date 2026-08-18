using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x02000455 RID: 1109
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMiscSettingValueResp
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0000AFAF File Offset: 0x000091AF
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x0000AFB7 File Offset: 0x000091B7
		[DataMember]
		public string Value { get; set; }
	}
}
