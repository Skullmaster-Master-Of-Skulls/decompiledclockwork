using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006AB RID: 1707
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFormResp
	{
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0000FCB5 File Offset: 0x0000DEB5
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0000FCBD File Offset: 0x0000DEBD
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
