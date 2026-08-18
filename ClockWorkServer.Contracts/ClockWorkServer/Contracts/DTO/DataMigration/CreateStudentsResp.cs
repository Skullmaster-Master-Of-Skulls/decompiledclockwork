using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200072D RID: 1837
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateStudentsResp
	{
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000113FD File Offset: 0x0000F5FD
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x00011405 File Offset: 0x0000F605
		[DataMember]
		public IList<MigrationCreateStudentResultDTO> Results { get; set; }
	}
}
