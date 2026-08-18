using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E5 RID: 1253
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyStudentNoteDTO
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x0000C402 File Offset: 0x0000A602
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0000C40B File Offset: 0x0000A60B
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0000C413 File Offset: 0x0000A613
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0000C41C File Offset: 0x0000A61C
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0000C424 File Offset: 0x0000A624
		[DataMember]
		public string ControlValue { get; set; }
	}
}
