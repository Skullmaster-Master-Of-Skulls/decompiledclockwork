using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013E RID: 318
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateJustUserOrGroupPermissionsReq : BaseMessageReq
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000036B4 File Offset: 0x000018B4
		[DataMember]
		public UserOrGroupJustPermissionSetDTO PermissionSet { get; set; }
	}
}
