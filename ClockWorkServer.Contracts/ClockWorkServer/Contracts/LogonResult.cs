using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B0 RID: 176
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogonResult
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x000023B2 File Offset: 0x000005B2
		[DataMember]
		public string FullName { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x000023C3 File Offset: 0x000005C3
		[DataMember]
		public Token SessionTicket { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x000023D4 File Offset: 0x000005D4
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x000023E5 File Offset: 0x000005E5
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x000023F6 File Offset: 0x000005F6
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00002407 File Offset: 0x00000607
		[DataMember]
		public IList<int> RoleIds { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00002418 File Offset: 0x00000618
		[DataMember]
		public AuthenticationSessionInfoDTO TokenStatus { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00002429 File Offset: 0x00000629
		[DataMember]
		public bool RequirePasswordChange { get; set; }
	}
}
