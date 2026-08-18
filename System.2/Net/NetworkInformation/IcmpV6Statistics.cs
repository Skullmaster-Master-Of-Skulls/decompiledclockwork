using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200029D RID: 669
	[__DynamicallyInvokable]
	public abstract class IcmpV6Statistics
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060018E8 RID: 6376
		[__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060018E9 RID: 6377
		[__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060018EA RID: 6378
		[__DynamicallyInvokable]
		public abstract long EchoRepliesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060018EB RID: 6379
		[__DynamicallyInvokable]
		public abstract long EchoRepliesSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060018EC RID: 6380
		[__DynamicallyInvokable]
		public abstract long EchoRequestsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060018ED RID: 6381
		[__DynamicallyInvokable]
		public abstract long EchoRequestsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060018EE RID: 6382
		[__DynamicallyInvokable]
		public abstract long ErrorsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060018EF RID: 6383
		[__DynamicallyInvokable]
		public abstract long ErrorsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060018F0 RID: 6384
		[__DynamicallyInvokable]
		public abstract long MembershipQueriesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060018F1 RID: 6385
		[__DynamicallyInvokable]
		public abstract long MembershipQueriesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060018F2 RID: 6386
		[__DynamicallyInvokable]
		public abstract long MembershipReductionsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060018F3 RID: 6387
		[__DynamicallyInvokable]
		public abstract long MembershipReductionsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060018F4 RID: 6388
		[__DynamicallyInvokable]
		public abstract long MembershipReportsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060018F5 RID: 6389
		[__DynamicallyInvokable]
		public abstract long MembershipReportsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060018F6 RID: 6390
		[__DynamicallyInvokable]
		public abstract long MessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060018F7 RID: 6391
		[__DynamicallyInvokable]
		public abstract long MessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060018F8 RID: 6392
		[__DynamicallyInvokable]
		public abstract long NeighborAdvertisementsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060018F9 RID: 6393
		[__DynamicallyInvokable]
		public abstract long NeighborAdvertisementsSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060018FA RID: 6394
		[__DynamicallyInvokable]
		public abstract long NeighborSolicitsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060018FB RID: 6395
		[__DynamicallyInvokable]
		public abstract long NeighborSolicitsSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060018FC RID: 6396
		[__DynamicallyInvokable]
		public abstract long PacketTooBigMessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060018FD RID: 6397
		[__DynamicallyInvokable]
		public abstract long PacketTooBigMessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060018FE RID: 6398
		[__DynamicallyInvokable]
		public abstract long ParameterProblemsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060018FF RID: 6399
		[__DynamicallyInvokable]
		public abstract long ParameterProblemsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001900 RID: 6400
		[__DynamicallyInvokable]
		public abstract long RedirectsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001901 RID: 6401
		[__DynamicallyInvokable]
		public abstract long RedirectsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001902 RID: 6402
		[__DynamicallyInvokable]
		public abstract long RouterAdvertisementsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001903 RID: 6403
		[__DynamicallyInvokable]
		public abstract long RouterAdvertisementsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001904 RID: 6404
		[__DynamicallyInvokable]
		public abstract long RouterSolicitsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001905 RID: 6405
		[__DynamicallyInvokable]
		public abstract long RouterSolicitsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001906 RID: 6406
		[__DynamicallyInvokable]
		public abstract long TimeExceededMessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001907 RID: 6407
		[__DynamicallyInvokable]
		public abstract long TimeExceededMessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x06001908 RID: 6408 RVA: 0x0007DF19 File Offset: 0x0007C119
		[__DynamicallyInvokable]
		protected IcmpV6Statistics()
		{
		}
	}
}
