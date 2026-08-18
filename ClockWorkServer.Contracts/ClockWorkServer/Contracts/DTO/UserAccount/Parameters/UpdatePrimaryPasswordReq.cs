using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000158 RID: 344
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPasswordReq : BaseMessageReq
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00003D7E File Offset: 0x00001F7E
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x00003D86 File Offset: 0x00001F86
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00003D8F File Offset: 0x00001F8F
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00003D97 File Offset: 0x00001F97
		[DataMember]
		public string NewPassword { get; set; }
	}
}
