using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000656 RID: 1622
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetExistingPerDateEntryReq : BaseMessageReq
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		// (set) Token: 0x06002100 RID: 8448 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0000EFF9 File Offset: 0x0000D1F9
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x0000F001 File Offset: 0x0000D201
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
