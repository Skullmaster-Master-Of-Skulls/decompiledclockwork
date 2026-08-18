using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.General
{
	// Token: 0x020005EE RID: 1518
	[DataContract(Namespace = "http://tpro.ca")]
	public class ModificationHistoryItemBaseDTO
	{
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0000E197 File Offset: 0x0000C397
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0000E19F File Offset: 0x0000C39F
		[DataMember]
		public DateTime? DateCreated { get; set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[DataMember]
		public virtual int WhoCreatedPersonId { get; set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0000E1C1 File Offset: 0x0000C3C1
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0000E1D2 File Offset: 0x0000C3D2
		[DataMember]
		public virtual int WhoLastModifiedPersonId { get; set; }
	}
}
