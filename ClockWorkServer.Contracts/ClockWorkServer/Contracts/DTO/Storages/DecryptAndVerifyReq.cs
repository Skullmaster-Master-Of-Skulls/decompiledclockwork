using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Storages
{
	// Token: 0x0200025E RID: 606
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptAndVerifyReq : BaseMessageReq
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00006886 File Offset: 0x00004A86
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0000688E File Offset: 0x00004A8E
		[DataMember]
		public byte[] EncryptedFile { get; set; }
	}
}
