using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000151 RID: 337
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePasswordResp
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00003CD4 File Offset: 0x00001ED4
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00003CDC File Offset: 0x00001EDC
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00003CE5 File Offset: 0x00001EE5
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00003CED File Offset: 0x00001EED
		[DataMember]
		public string Message { get; set; }
	}
}
