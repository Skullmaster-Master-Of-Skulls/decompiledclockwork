using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000365 RID: 869
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserMustChangePasswordResp
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00009551 File Offset: 0x00007751
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00009559 File Offset: 0x00007759
		[DataMember]
		public bool UserMustChangePassword { get; set; }
	}
}
