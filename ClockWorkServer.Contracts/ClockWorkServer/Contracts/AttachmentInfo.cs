using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A9 RID: 169
	[DataContract(Namespace = "http://tpro.ca")]
	public class AttachmentInfo
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000208C File Offset: 0x0000028C
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00002095 File Offset: 0x00000295
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000209D File Offset: 0x0000029D
		[DataMember]
		public string Extension { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000020A6 File Offset: 0x000002A6
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x000020AE File Offset: 0x000002AE
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000020B7 File Offset: 0x000002B7
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000020BF File Offset: 0x000002BF
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000020C8 File Offset: 0x000002C8
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x000020D0 File Offset: 0x000002D0
		[DataMember]
		public bool RequiredReceivingConfirmation { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000020D9 File Offset: 0x000002D9
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000020E1 File Offset: 0x000002E1
		[DataMember]
		public DateTime IssuedOn { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000020F2 File Offset: 0x000002F2
		[DataMember]
		public IM_User From { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00002103 File Offset: 0x00000303
		[DataMember]
		public string To { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00002114 File Offset: 0x00000314
		[DataMember]
		public bool WasRead { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00002125 File Offset: 0x00000325
		[DataMember]
		public int SizeInBytes { get; set; }
	}
}
