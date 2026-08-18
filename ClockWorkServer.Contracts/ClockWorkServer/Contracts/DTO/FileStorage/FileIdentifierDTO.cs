using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FE RID: 1534
	[DataContract(Namespace = "http://tpro.ca")]
	public class FileIdentifierDTO
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0000E447 File Offset: 0x0000C647
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0000E44F File Offset: 0x0000C64F
		[DataMember]
		public Guid? FileUniqueId { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0000E458 File Offset: 0x0000C658
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0000E460 File Offset: 0x0000C660
		[DataMember]
		public eFileSource Source { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0000E469 File Offset: 0x0000C669
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x0000E471 File Offset: 0x0000C671
		[DataMember]
		public int LegacyId { get; set; }
	}
}
