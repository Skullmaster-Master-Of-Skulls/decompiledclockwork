using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000704 RID: 1796
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseAltContactDTO
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		[DataMember]
		public string ExternalId { get; set; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00010AB1 File Offset: 0x0000ECB1
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x00010AB9 File Offset: 0x0000ECB9
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00010AC2 File Offset: 0x0000ECC2
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x00010ACA File Offset: 0x0000ECCA
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00010AD3 File Offset: 0x0000ECD3
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x00010ADB File Offset: 0x0000ECDB
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x00010AEC File Offset: 0x0000ECEC
		[DataMember]
		public string EmployeeId { get; set; }

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00010AF5 File Offset: 0x0000ECF5
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x00010AFD File Offset: 0x0000ECFD
		[DataMember]
		public string Phone { get; set; }
	}
}
