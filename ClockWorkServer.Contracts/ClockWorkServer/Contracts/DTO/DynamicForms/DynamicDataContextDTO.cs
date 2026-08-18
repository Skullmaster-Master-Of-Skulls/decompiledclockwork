using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000636 RID: 1590
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicDataContextDTO
	{
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0000EB5A File Offset: 0x0000CD5A
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0000EB62 File Offset: 0x0000CD62
		[DataMember]
		public int PrimaryId { get; set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0000EB6B File Offset: 0x0000CD6B
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0000EB73 File Offset: 0x0000CD73
		[DataMember]
		public int SecondaryId { get; set; }
	}
}
