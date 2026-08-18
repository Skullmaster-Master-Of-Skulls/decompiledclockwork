using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AB RID: 171
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthTicketResult
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00002169 File Offset: 0x00000369
		[DataMember]
		public bool IsSessionBased { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00002172 File Offset: 0x00000372
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000217A File Offset: 0x0000037A
		[DataMember]
		public string SessionTicket { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00002183 File Offset: 0x00000383
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000218B File Offset: 0x0000038B
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000219C File Offset: 0x0000039C
		[DataMember]
		public string UserRoles { get; set; }
	}
}
