using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008F1 RID: 2289
	public class ExternalUserInfoDTO
	{
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x00016125 File Offset: 0x00014325
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x0001612D File Offset: 0x0001432D
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x00016136 File Offset: 0x00014336
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x0001613E File Offset: 0x0001433E
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x00016147 File Offset: 0x00014347
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x0001614F File Offset: 0x0001434F
		[DataMember]
		public string Email { get; set; }
	}
}
