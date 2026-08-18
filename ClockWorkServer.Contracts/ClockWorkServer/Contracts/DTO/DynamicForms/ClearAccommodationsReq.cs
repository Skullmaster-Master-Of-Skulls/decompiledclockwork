using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061A RID: 1562
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0000E692 File Offset: 0x0000C892
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x0000E69A File Offset: 0x0000C89A
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0000E6A3 File Offset: 0x0000C8A3
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x0000E6AB File Offset: 0x0000C8AB
		[DataMember]
		public int CourseId { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x0000E6BC File Offset: 0x0000C8BC
		[DataMember]
		public bool RequiresApproval { get; set; }
	}
}
