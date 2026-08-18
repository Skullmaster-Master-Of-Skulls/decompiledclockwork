using System;

namespace System.Net
{
	// Token: 0x02000544 RID: 1348
	internal enum SchProtocols
	{
		// Token: 0x040027F0 RID: 10224
		Zero,
		// Token: 0x040027F1 RID: 10225
		PctClient = 2,
		// Token: 0x040027F2 RID: 10226
		PctServer = 1,
		// Token: 0x040027F3 RID: 10227
		Pct = 3,
		// Token: 0x040027F4 RID: 10228
		Ssl2Client = 8,
		// Token: 0x040027F5 RID: 10229
		Ssl2Server = 4,
		// Token: 0x040027F6 RID: 10230
		Ssl2 = 12,
		// Token: 0x040027F7 RID: 10231
		Ssl3Client = 32,
		// Token: 0x040027F8 RID: 10232
		Ssl3Server = 16,
		// Token: 0x040027F9 RID: 10233
		Ssl3 = 48,
		// Token: 0x040027FA RID: 10234
		TlsClient = 128,
		// Token: 0x040027FB RID: 10235
		TlsServer = 64,
		// Token: 0x040027FC RID: 10236
		Tls = 192,
		// Token: 0x040027FD RID: 10237
		Tls11Client = 512,
		// Token: 0x040027FE RID: 10238
		Tls11Server = 256,
		// Token: 0x040027FF RID: 10239
		Tls11 = 768,
		// Token: 0x04002800 RID: 10240
		Tls12Client = 2048,
		// Token: 0x04002801 RID: 10241
		Tls12Server = 1024,
		// Token: 0x04002802 RID: 10242
		Tls12 = 3072,
		// Token: 0x04002803 RID: 10243
		Ssl3Tls = 240,
		// Token: 0x04002804 RID: 10244
		UniClient = -2147483648,
		// Token: 0x04002805 RID: 10245
		UniServer = 1073741824,
		// Token: 0x04002806 RID: 10246
		Unified = -1073741824,
		// Token: 0x04002807 RID: 10247
		ClientMask = -2147480918,
		// Token: 0x04002808 RID: 10248
		ServerMask = 1073743189
	}
}
