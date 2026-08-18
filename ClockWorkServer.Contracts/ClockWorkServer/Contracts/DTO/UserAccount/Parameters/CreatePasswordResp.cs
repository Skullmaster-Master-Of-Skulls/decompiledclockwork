using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014E RID: 334
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePasswordResp
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00003C4C File Offset: 0x00001E4C
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00003C54 File Offset: 0x00001E54
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00003C5D File Offset: 0x00001E5D
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00003C65 File Offset: 0x00001E65
		[DataMember]
		public string Message { get; set; }
	}
}
