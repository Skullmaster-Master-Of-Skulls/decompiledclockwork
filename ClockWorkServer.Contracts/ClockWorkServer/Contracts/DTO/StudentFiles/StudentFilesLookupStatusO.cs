using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022F RID: 559
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesLookupStatusO
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00005C16 File Offset: 0x00003E16
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00005C1E File Offset: 0x00003E1E
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00005C27 File Offset: 0x00003E27
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00005C2F File Offset: 0x00003E2F
		[DataMember]
		public eStudentFileStatusType StatusType { get; set; }
	}
}
