using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022E RID: 558
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesLookupStatusDTO
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00005BF4 File Offset: 0x00003DF4
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00005BFC File Offset: 0x00003DFC
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00005C05 File Offset: 0x00003E05
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00005C0D File Offset: 0x00003E0D
		[DataMember]
		public eStudentFileStatusType StatusType { get; set; }
	}
}
