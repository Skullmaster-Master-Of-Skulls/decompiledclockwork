using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200029C RID: 668
	[__DynamicallyInvokable]
	public abstract class IcmpV4Statistics
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060018CD RID: 6349
		[__DynamicallyInvokable]
		public abstract long AddressMaskRepliesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060018CE RID: 6350
		[__DynamicallyInvokable]
		public abstract long AddressMaskRepliesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060018CF RID: 6351
		[__DynamicallyInvokable]
		public abstract long AddressMaskRequestsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060018D0 RID: 6352
		[__DynamicallyInvokable]
		public abstract long AddressMaskRequestsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060018D1 RID: 6353
		[__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060018D2 RID: 6354
		[__DynamicallyInvokable]
		public abstract long DestinationUnreachableMessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060018D3 RID: 6355
		[__DynamicallyInvokable]
		public abstract long EchoRepliesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060018D4 RID: 6356
		[__DynamicallyInvokable]
		public abstract long EchoRepliesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060018D5 RID: 6357
		[__DynamicallyInvokable]
		public abstract long EchoRequestsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060018D6 RID: 6358
		[__DynamicallyInvokable]
		public abstract long EchoRequestsSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060018D7 RID: 6359
		[__DynamicallyInvokable]
		public abstract long ErrorsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060018D8 RID: 6360
		[__DynamicallyInvokable]
		public abstract long ErrorsSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060018D9 RID: 6361
		[__DynamicallyInvokable]
		public abstract long MessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060018DA RID: 6362
		[__DynamicallyInvokable]
		public abstract long MessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060018DB RID: 6363
		[__DynamicallyInvokable]
		public abstract long ParameterProblemsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060018DC RID: 6364
		[__DynamicallyInvokable]
		public abstract long ParameterProblemsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060018DD RID: 6365
		[__DynamicallyInvokable]
		public abstract long RedirectsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060018DE RID: 6366
		[__DynamicallyInvokable]
		public abstract long RedirectsSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060018DF RID: 6367
		[__DynamicallyInvokable]
		public abstract long SourceQuenchesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060018E0 RID: 6368
		[__DynamicallyInvokable]
		public abstract long SourceQuenchesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060018E1 RID: 6369
		[__DynamicallyInvokable]
		public abstract long TimeExceededMessagesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060018E2 RID: 6370
		[__DynamicallyInvokable]
		public abstract long TimeExceededMessagesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060018E3 RID: 6371
		[__DynamicallyInvokable]
		public abstract long TimestampRepliesReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060018E4 RID: 6372
		[__DynamicallyInvokable]
		public abstract long TimestampRepliesSent { [__DynamicallyInvokable] get; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060018E5 RID: 6373
		[__DynamicallyInvokable]
		public abstract long TimestampRequestsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060018E6 RID: 6374
		[__DynamicallyInvokable]
		public abstract long TimestampRequestsSent { [__DynamicallyInvokable] get; }

		// Token: 0x060018E7 RID: 6375 RVA: 0x0007DF11 File Offset: 0x0007C111
		[__DynamicallyInvokable]
		protected IcmpV4Statistics()
		{
		}
	}
}
