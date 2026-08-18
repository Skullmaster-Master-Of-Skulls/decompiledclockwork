using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067F RID: 1663
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldByNameResp
	{
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0000F756 File Offset: 0x0000D956
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0000F75E File Offset: 0x0000D95E
		[DataMember]
		public DynamicFieldDTO Field { get; set; }
	}
}
