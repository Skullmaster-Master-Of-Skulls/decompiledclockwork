using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200072C RID: 1836
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateStudentsReq : BaseMessageReq
	{
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000113DB File Offset: 0x0000F5DB
		// (set) Token: 0x060025C6 RID: 9670 RVA: 0x000113E3 File Offset: 0x0000F5E3
		[DataMember]
		public bool PreviewMode { get; set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000113EC File Offset: 0x0000F5EC
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000113F4 File Offset: 0x0000F5F4
		[DataMember]
		public IList<MigrationStudentDTO> MigrationStudents { get; set; }
	}
}
