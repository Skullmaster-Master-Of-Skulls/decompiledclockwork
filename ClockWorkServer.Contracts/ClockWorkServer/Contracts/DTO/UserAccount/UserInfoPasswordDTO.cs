using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount
{
	// Token: 0x02000149 RID: 329
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserInfoPasswordDTO
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00003B80 File Offset: 0x00001D80
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00003B88 File Offset: 0x00001D88
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00003B91 File Offset: 0x00001D91
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00003B99 File Offset: 0x00001D99
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00003BA2 File Offset: 0x00001DA2
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00003BAA File Offset: 0x00001DAA
		[DataMember]
		public string Password { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00003BB3 File Offset: 0x00001DB3
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00003BBB File Offset: 0x00001DBB
		[DataMember]
		public bool RequiresPasswordChange { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00003BC4 File Offset: 0x00001DC4
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00003BCC File Offset: 0x00001DCC
		[DataMember]
		public DateTime LastPasswordChangeDate { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00003BD5 File Offset: 0x00001DD5
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00003BDD File Offset: 0x00001DDD
		[DataMember]
		public DateTime? PasswordExpiryDate { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00003BE6 File Offset: 0x00001DE6
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00003BEE File Offset: 0x00001DEE
		[DataMember]
		public bool IsEncrypted { get; set; }
	}
}
