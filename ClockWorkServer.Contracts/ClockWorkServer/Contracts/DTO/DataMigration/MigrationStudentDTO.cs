using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x0200073C RID: 1852
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationStudentDTO
	{
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x00011BB5 File Offset: 0x0000FDB5
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x00011BBD File Offset: 0x0000FDBD
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x00011BC6 File Offset: 0x0000FDC6
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x00011BCE File Offset: 0x0000FDCE
		[DataMember]
		public IList<int> ClockWorkGroupIds { get; set; }

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00011BD7 File Offset: 0x0000FDD7
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x00011BDF File Offset: 0x0000FDDF
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00011BF9 File Offset: 0x0000FDF9
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x00011C01 File Offset: 0x0000FE01
		[DataMember]
		public string LastName { get; set; }
	}
}
