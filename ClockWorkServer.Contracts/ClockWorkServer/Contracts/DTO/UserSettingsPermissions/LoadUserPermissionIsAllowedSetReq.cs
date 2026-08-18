using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000138 RID: 312
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUserPermissionIsAllowedSetReq : BaseMessageReq
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00003635 File Offset: 0x00001835
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0000363D File Offset: 0x0000183D
		[DataMember]
		public int ForPersonId { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00003646 File Offset: 0x00001846
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0000364E File Offset: 0x0000184E
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
