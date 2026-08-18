using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AC RID: 172
	[DataContract(Namespace = "http://tpro.ca")]
	public class IM_User
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000021A5 File Offset: 0x000003A5
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x000021AD File Offset: 0x000003AD
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000021B6 File Offset: 0x000003B6
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x000021BE File Offset: 0x000003BE
		[DataMember]
		public string FullName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000021C7 File Offset: 0x000003C7
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x000021CF File Offset: 0x000003CF
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x000021E0 File Offset: 0x000003E0
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000021E9 File Offset: 0x000003E9
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x000021F1 File Offset: 0x000003F1
		[DataMember]
		public List<string> Roles { get; set; }
	}
}
