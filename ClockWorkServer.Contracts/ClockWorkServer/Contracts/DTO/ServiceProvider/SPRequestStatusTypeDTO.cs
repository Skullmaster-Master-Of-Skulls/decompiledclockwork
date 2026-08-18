using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200027B RID: 635
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestStatusTypeDTO
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0000728D File Offset: 0x0000548D
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00007295 File Offset: 0x00005495
		[DataMember]
		public int SPRequestStatusTypeId { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0000729E File Offset: 0x0000549E
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000072A6 File Offset: 0x000054A6
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000072AF File Offset: 0x000054AF
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x000072B7 File Offset: 0x000054B7
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000072C0 File Offset: 0x000054C0
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x000072C8 File Offset: 0x000054C8
		[DataMember]
		public bool AssignmentIsRequired { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000072D1 File Offset: 0x000054D1
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x000072D9 File Offset: 0x000054D9
		[DataMember]
		public SPUrgencyLevelTypeDTO UrgencyLevel { get; set; }
	}
}
