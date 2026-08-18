using System;

namespace System.Net.Sockets
{
	// Token: 0x020005C6 RID: 1478
	public enum SocketOptionName
	{
		// Token: 0x04002BF7 RID: 11255
		Debug = 1,
		// Token: 0x04002BF8 RID: 11256
		AcceptConnection,
		// Token: 0x04002BF9 RID: 11257
		ReuseAddress = 4,
		// Token: 0x04002BFA RID: 11258
		KeepAlive = 8,
		// Token: 0x04002BFB RID: 11259
		DontRoute = 16,
		// Token: 0x04002BFC RID: 11260
		Broadcast = 32,
		// Token: 0x04002BFD RID: 11261
		UseLoopback = 64,
		// Token: 0x04002BFE RID: 11262
		Linger = 128,
		// Token: 0x04002BFF RID: 11263
		OutOfBandInline = 256,
		// Token: 0x04002C00 RID: 11264
		DontLinger = -129,
		// Token: 0x04002C01 RID: 11265
		ExclusiveAddressUse = -5,
		// Token: 0x04002C02 RID: 11266
		SendBuffer = 4097,
		// Token: 0x04002C03 RID: 11267
		ReceiveBuffer,
		// Token: 0x04002C04 RID: 11268
		SendLowWater,
		// Token: 0x04002C05 RID: 11269
		ReceiveLowWater,
		// Token: 0x04002C06 RID: 11270
		SendTimeout,
		// Token: 0x04002C07 RID: 11271
		ReceiveTimeout,
		// Token: 0x04002C08 RID: 11272
		Error,
		// Token: 0x04002C09 RID: 11273
		Type,
		// Token: 0x04002C0A RID: 11274
		MaxConnections = 2147483647,
		// Token: 0x04002C0B RID: 11275
		IPOptions = 1,
		// Token: 0x04002C0C RID: 11276
		HeaderIncluded,
		// Token: 0x04002C0D RID: 11277
		TypeOfService,
		// Token: 0x04002C0E RID: 11278
		IpTimeToLive,
		// Token: 0x04002C0F RID: 11279
		MulticastInterface = 9,
		// Token: 0x04002C10 RID: 11280
		MulticastTimeToLive,
		// Token: 0x04002C11 RID: 11281
		MulticastLoopback,
		// Token: 0x04002C12 RID: 11282
		AddMembership,
		// Token: 0x04002C13 RID: 11283
		DropMembership,
		// Token: 0x04002C14 RID: 11284
		DontFragment,
		// Token: 0x04002C15 RID: 11285
		AddSourceMembership,
		// Token: 0x04002C16 RID: 11286
		DropSourceMembership,
		// Token: 0x04002C17 RID: 11287
		BlockSource,
		// Token: 0x04002C18 RID: 11288
		UnblockSource,
		// Token: 0x04002C19 RID: 11289
		PacketInformation,
		// Token: 0x04002C1A RID: 11290
		HopLimit = 21,
		// Token: 0x04002C1B RID: 11291
		NoDelay = 1,
		// Token: 0x04002C1C RID: 11292
		BsdUrgent,
		// Token: 0x04002C1D RID: 11293
		Expedited = 2,
		// Token: 0x04002C1E RID: 11294
		NoChecksum = 1,
		// Token: 0x04002C1F RID: 11295
		ChecksumCoverage = 20,
		// Token: 0x04002C20 RID: 11296
		UpdateAcceptContext = 28683,
		// Token: 0x04002C21 RID: 11297
		UpdateConnectContext = 28688
	}
}
