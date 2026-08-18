using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess
{
	// Token: 0x02000415 RID: 1045
	[DataContract(Namespace = "http://tpro.ca")]
	public class MultiAccessInfo
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		[DataMember]
		public eMultiAccessType AccessType { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0000A902 File Offset: 0x00008B02
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0000A90A File Offset: 0x00008B0A
		[DataMember]
		public MultiAccessContext Context { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0000A913 File Offset: 0x00008B13
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x0000A91B File Offset: 0x00008B1B
		[DataMember]
		public int WhoIsAccessingPersonId { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0000A924 File Offset: 0x00008B24
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0000A92C File Offset: 0x00008B2C
		[DataMember]
		public string WhoIsAccessingDisplayName { get; set; }
	}
}
