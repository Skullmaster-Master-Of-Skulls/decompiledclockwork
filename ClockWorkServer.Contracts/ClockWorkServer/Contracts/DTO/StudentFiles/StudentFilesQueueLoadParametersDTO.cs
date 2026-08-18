using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000232 RID: 562
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesQueueLoadParametersDTO
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00005D1F File Offset: 0x00003F1F
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00005D27 File Offset: 0x00003F27
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00005D30 File Offset: 0x00003F30
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00005D38 File Offset: 0x00003F38
		[DataMember]
		public bool ExcludeItemsWithClosedStatuses { get; set; }
	}
}
