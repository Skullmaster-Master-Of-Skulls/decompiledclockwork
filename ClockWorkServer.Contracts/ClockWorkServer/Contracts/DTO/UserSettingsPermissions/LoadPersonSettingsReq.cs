using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000128 RID: 296
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonSettingsReq : BaseMessageReq
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000347B File Offset: 0x0000167B
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00003483 File Offset: 0x00001683
		[DataMember]
		public int PersonId { get; set; }
	}
}
