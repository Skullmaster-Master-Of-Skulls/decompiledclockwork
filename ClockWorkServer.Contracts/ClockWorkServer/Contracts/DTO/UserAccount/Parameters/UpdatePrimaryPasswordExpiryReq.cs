using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015C RID: 348
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPasswordExpiryReq : BaseMessageReq
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00003DF5 File Offset: 0x00001FF5
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00003DFD File Offset: 0x00001FFD
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00003E06 File Offset: 0x00002006
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x00003E0E File Offset: 0x0000200E
		[DataMember]
		public DateTime? NewExpiryDate { get; set; }
	}
}
