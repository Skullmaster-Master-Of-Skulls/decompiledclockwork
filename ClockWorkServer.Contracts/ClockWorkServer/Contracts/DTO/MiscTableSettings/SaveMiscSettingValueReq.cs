using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x02000457 RID: 1111
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveMiscSettingValueReq : BaseMessageReq
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0000AFD1 File Offset: 0x000091D1
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x0000AFD9 File Offset: 0x000091D9
		[DataMember]
		public int Code { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0000AFE2 File Offset: 0x000091E2
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x0000AFEA File Offset: 0x000091EA
		[DataMember]
		public string Value { get; set; }
	}
}
