using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B2 RID: 178
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogonUserInfoDTO
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000246D File Offset: 0x0000066D
		[DataMember]
		public string Firstname { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00002476 File Offset: 0x00000676
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000247E File Offset: 0x0000067E
		[DataMember]
		public string Lastname { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000248F File Offset: 0x0000068F
		[DataMember]
		public string Username { get; set; }
	}
}
