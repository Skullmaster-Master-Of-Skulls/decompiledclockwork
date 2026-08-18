using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AF RID: 943
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveAssignedAdvisorStoredSignatureWithImageBytesReq : BaseMessageReq
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00009DEB File Offset: 0x00007FEB
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x00009DF3 File Offset: 0x00007FF3
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00009DFC File Offset: 0x00007FFC
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x00009E04 File Offset: 0x00008004
		[DataMember]
		public byte[] ImageBytes { get; set; }
	}
}
