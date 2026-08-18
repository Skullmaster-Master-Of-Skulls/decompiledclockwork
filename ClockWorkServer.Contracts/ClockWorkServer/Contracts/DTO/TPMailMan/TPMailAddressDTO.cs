using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001AF RID: 431
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public class TPMailAddressDTO
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000046BC File Offset: 0x000028BC
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x000046C4 File Offset: 0x000028C4
		[DataMember]
		public string EmailAddress { get; set; }
	}
}
