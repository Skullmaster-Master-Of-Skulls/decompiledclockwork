using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015B RID: 347
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPassword2Resp
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00003DD3 File Offset: 0x00001FD3
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00003DDB File Offset: 0x00001FDB
		[DataMember]
		public bool PasswordChangeWasSuccessful { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00003DE4 File Offset: 0x00001FE4
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00003DEC File Offset: 0x00001FEC
		[DataMember]
		public string Message { get; set; }
	}
}
