using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.General
{
	// Token: 0x020005EF RID: 1519
	[DataContract(Namespace = "http://tpro.ca")]
	public class ModificationHistoryItemDTO
	{
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0000E1DB File Offset: 0x0000C3DB
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x0000E1E3 File Offset: 0x0000C3E3
		[DataMember]
		public DateTime? DateCreated { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		[DataMember]
		public PersonBaseDTO WhoCreated { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0000E1FD File Offset: 0x0000C3FD
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x0000E205 File Offset: 0x0000C405
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0000E20E File Offset: 0x0000C40E
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x0000E216 File Offset: 0x0000C416
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }
	}
}
