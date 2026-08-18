using System;

namespace System.IdentityModel
{
	// Token: 0x0200009D RID: 157
	internal enum SchProtocols
	{
		// Token: 0x04000461 RID: 1121
		Zero,
		// Token: 0x04000462 RID: 1122
		PctClient = 2,
		// Token: 0x04000463 RID: 1123
		PctServer = 1,
		// Token: 0x04000464 RID: 1124
		Pct = 3,
		// Token: 0x04000465 RID: 1125
		Ssl2Client = 8,
		// Token: 0x04000466 RID: 1126
		Ssl2Server = 4,
		// Token: 0x04000467 RID: 1127
		Ssl2 = 12,
		// Token: 0x04000468 RID: 1128
		Ssl3Client = 32,
		// Token: 0x04000469 RID: 1129
		Ssl3Server = 16,
		// Token: 0x0400046A RID: 1130
		Ssl3 = 48,
		// Token: 0x0400046B RID: 1131
		TlsClient = 128,
		// Token: 0x0400046C RID: 1132
		TlsServer = 64,
		// Token: 0x0400046D RID: 1133
		Tls = 192,
		// Token: 0x0400046E RID: 1134
		Ssl3Tls = 240,
		// Token: 0x0400046F RID: 1135
		Tls11Client = 512,
		// Token: 0x04000470 RID: 1136
		Tls11Server = 256,
		// Token: 0x04000471 RID: 1137
		Tls11 = 768,
		// Token: 0x04000472 RID: 1138
		Tls12Client = 2048,
		// Token: 0x04000473 RID: 1139
		Tls12Server = 1024,
		// Token: 0x04000474 RID: 1140
		Tls12 = 3072,
		// Token: 0x04000475 RID: 1141
		Tls13Client = 8192,
		// Token: 0x04000476 RID: 1142
		Tls13Server = 4096,
		// Token: 0x04000477 RID: 1143
		Tls13 = 12288,
		// Token: 0x04000478 RID: 1144
		UniClient = -2147483648,
		// Token: 0x04000479 RID: 1145
		UniServer = 1073741824,
		// Token: 0x0400047A RID: 1146
		Unified = -1073741824,
		// Token: 0x0400047B RID: 1147
		ClientMask = -2147472726,
		// Token: 0x0400047C RID: 1148
		ServerMask = 1073747285
	}
}
