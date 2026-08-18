using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068D RID: 1677
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateListResp
	{
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0000F844 File Offset: 0x0000DA44
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x0000F84C File Offset: 0x0000DA4C
		[DataMember]
		public int LookupGroupId { get; set; }
	}
}
