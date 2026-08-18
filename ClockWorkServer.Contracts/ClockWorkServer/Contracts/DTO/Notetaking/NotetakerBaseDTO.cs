using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000419 RID: 1049
	[DataContract(Namespace = "http://tpro.ca")]
	public class NotetakerBaseDTO
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0000AA1B File Offset: 0x00008C1B
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0000AA23 File Offset: 0x00008C23
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0000AA2C File Offset: 0x00008C2C
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0000AA34 File Offset: 0x00008C34
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0000AA3D File Offset: 0x00008C3D
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0000AA45 File Offset: 0x00008C45
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0000AA4E File Offset: 0x00008C4E
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0000AA56 File Offset: 0x00008C56
		[DataMember]
		public string Email { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0000AA5F File Offset: 0x00008C5F
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x0000AA67 File Offset: 0x00008C67
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0000AA70 File Offset: 0x00008C70
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x0000AA78 File Offset: 0x00008C78
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0000AA81 File Offset: 0x00008C81
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0000AA89 File Offset: 0x00008C89
		[DataMember]
		public string Username { get; set; }
	}
}
