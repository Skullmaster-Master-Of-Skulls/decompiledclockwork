using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005EA RID: 1514
	[DataContract(Namespace = "http://tpro.ca")]
	public class IntakeEntryDTO
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		[DataMember]
		public int[] PersonIds { get; set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0000DFED File Offset: 0x0000C1ED
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0000DFF6 File Offset: 0x0000C1F6
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0000DFFE File Offset: 0x0000C1FE
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0000E007 File Offset: 0x0000C207
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0000E00F File Offset: 0x0000C20F
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0000E018 File Offset: 0x0000C218
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0000E020 File Offset: 0x0000C220
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0000E029 File Offset: 0x0000C229
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0000E031 File Offset: 0x0000C231
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0000E03A File Offset: 0x0000C23A
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0000E042 File Offset: 0x0000C242
		[DataMember]
		public string Ip { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0000E04B File Offset: 0x0000C24B
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0000E053 File Offset: 0x0000C253
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0000E05C File Offset: 0x0000C25C
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0000E064 File Offset: 0x0000C264
		[DataMember]
		public IntakeStatusDTO Status { get; set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0000E06D File Offset: 0x0000C26D
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0000E075 File Offset: 0x0000C275
		[DataMember]
		public string Note { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0000E07E File Offset: 0x0000C27E
		// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x0000E086 File Offset: 0x0000C286
		[DataMember]
		public int ExistingClockWorkStudentPersonId { get; set; }
	}
}
