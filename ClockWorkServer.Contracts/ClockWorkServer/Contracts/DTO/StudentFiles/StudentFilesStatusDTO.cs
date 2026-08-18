using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200023A RID: 570
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFilesStatusDTO
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00005E73 File Offset: 0x00004073
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00005E7B File Offset: 0x0000407B
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00005E84 File Offset: 0x00004084
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00005E8C File Offset: 0x0000408C
		[DataMember]
		public eStudentFileStatusType StatusType { get; set; }
	}
}
