using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000231 RID: 561
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesQueueItemsDTO
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00005CFD File Offset: 0x00003EFD
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00005D05 File Offset: 0x00003F05
		[DataMember]
		public IList<StudentFilesQueueStudentItemDTO> StudentItems { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00005D0E File Offset: 0x00003F0E
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00005D16 File Offset: 0x00003F16
		[DataMember]
		public IList<StudentFilesLookupStatusDTO> LookupStatuses { get; set; }
	}
}
