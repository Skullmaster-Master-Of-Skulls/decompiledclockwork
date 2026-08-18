using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014F RID: 335
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePasswordRequireChangeReq : BaseMessageReq
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00003C6E File Offset: 0x00001E6E
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00003C76 File Offset: 0x00001E76
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00003C7F File Offset: 0x00001E7F
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00003C87 File Offset: 0x00001E87
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00003C90 File Offset: 0x00001E90
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00003C98 File Offset: 0x00001E98
		[DataMember]
		public bool NewDoesRequirePasswordChange { get; set; }
	}
}
