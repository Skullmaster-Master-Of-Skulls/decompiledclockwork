using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200065D RID: 1629
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesAtLeastOneSavedDataItemExistResp
	{
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0000F0A3 File Offset: 0x0000D2A3
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x0000F0AB File Offset: 0x0000D2AB
		[DataMember]
		public bool AtLeastOneDataItemExists { get; set; }
	}
}
