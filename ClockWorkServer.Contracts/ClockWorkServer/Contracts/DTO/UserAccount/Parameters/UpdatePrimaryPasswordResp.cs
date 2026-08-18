using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000159 RID: 345
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPasswordResp
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00003DA0 File Offset: 0x00001FA0
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00003DB1 File Offset: 0x00001FB1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00003DB9 File Offset: 0x00001FB9
		[DataMember]
		public string Message { get; set; }
	}
}
