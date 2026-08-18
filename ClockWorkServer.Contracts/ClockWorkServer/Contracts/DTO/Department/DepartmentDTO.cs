using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Department
{
	// Token: 0x020006EF RID: 1775
	[DataContract(Namespace = "http://tpro.ca")]
	public class DepartmentDTO
	{
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000108E6 File Offset: 0x0000EAE6
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x000108EE File Offset: 0x0000EAEE
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x000108F7 File Offset: 0x0000EAF7
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x000108FF File Offset: 0x0000EAFF
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x00010908 File Offset: 0x0000EB08
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x00010910 File Offset: 0x0000EB10
		[DataMember]
		public string Institution { get; set; }
	}
}
