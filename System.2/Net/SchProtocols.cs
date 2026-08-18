using System;

namespace System.Net
{
	// Token: 0x02000211 RID: 529
	[Flags]
	internal enum SchProtocols
	{
		// Token: 0x0400157F RID: 5503
		Zero = 0,
		// Token: 0x04001580 RID: 5504
		PctClient = 2,
		// Token: 0x04001581 RID: 5505
		PctServer = 1,
		// Token: 0x04001582 RID: 5506
		Pct = 3,
		// Token: 0x04001583 RID: 5507
		Ssl2Client = 8,
		// Token: 0x04001584 RID: 5508
		Ssl2Server = 4,
		// Token: 0x04001585 RID: 5509
		Ssl2 = 12,
		// Token: 0x04001586 RID: 5510
		Ssl3Client = 32,
		// Token: 0x04001587 RID: 5511
		Ssl3Server = 16,
		// Token: 0x04001588 RID: 5512
		Ssl3 = 48,
		// Token: 0x04001589 RID: 5513
		Tls10Client = 128,
		// Token: 0x0400158A RID: 5514
		Tls10Server = 64,
		// Token: 0x0400158B RID: 5515
		Tls10 = 192,
		// Token: 0x0400158C RID: 5516
		Tls11Client = 512,
		// Token: 0x0400158D RID: 5517
		Tls11Server = 256,
		// Token: 0x0400158E RID: 5518
		Tls11 = 768,
		// Token: 0x0400158F RID: 5519
		Tls12Client = 2048,
		// Token: 0x04001590 RID: 5520
		Tls12Server = 1024,
		// Token: 0x04001591 RID: 5521
		Tls12 = 3072,
		// Token: 0x04001592 RID: 5522
		Tls13Client = 8192,
		// Token: 0x04001593 RID: 5523
		Tls13Server = 4096,
		// Token: 0x04001594 RID: 5524
		Tls13 = 12288,
		// Token: 0x04001595 RID: 5525
		Ssl3Tls = 240,
		// Token: 0x04001596 RID: 5526
		UniClient = -2147483648,
		// Token: 0x04001597 RID: 5527
		UniServer = 1073741824,
		// Token: 0x04001598 RID: 5528
		Unified = -1073741824,
		// Token: 0x04001599 RID: 5529
		ClientMask = -2147472726,
		// Token: 0x0400159A RID: 5530
		ServerMask = 1073747285
	}
}
