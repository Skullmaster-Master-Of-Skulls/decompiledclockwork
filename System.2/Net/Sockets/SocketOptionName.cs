using System;

namespace System.Net.Sockets
{
	// Token: 0x02000380 RID: 896
	public enum SocketOptionName
	{
		// Token: 0x04001F06 RID: 7942
		Debug = 1,
		// Token: 0x04001F07 RID: 7943
		AcceptConnection,
		// Token: 0x04001F08 RID: 7944
		ReuseAddress = 4,
		// Token: 0x04001F09 RID: 7945
		KeepAlive = 8,
		// Token: 0x04001F0A RID: 7946
		DontRoute = 16,
		// Token: 0x04001F0B RID: 7947
		Broadcast = 32,
		// Token: 0x04001F0C RID: 7948
		UseLoopback = 64,
		// Token: 0x04001F0D RID: 7949
		Linger = 128,
		// Token: 0x04001F0E RID: 7950
		OutOfBandInline = 256,
		// Token: 0x04001F0F RID: 7951
		DontLinger = -129,
		// Token: 0x04001F10 RID: 7952
		ExclusiveAddressUse = -5,
		// Token: 0x04001F11 RID: 7953
		SendBuffer = 4097,
		// Token: 0x04001F12 RID: 7954
		ReceiveBuffer,
		// Token: 0x04001F13 RID: 7955
		SendLowWater,
		// Token: 0x04001F14 RID: 7956
		ReceiveLowWater,
		// Token: 0x04001F15 RID: 7957
		SendTimeout,
		// Token: 0x04001F16 RID: 7958
		ReceiveTimeout,
		// Token: 0x04001F17 RID: 7959
		Error,
		// Token: 0x04001F18 RID: 7960
		Type,
		// Token: 0x04001F19 RID: 7961
		ReuseUnicastPort = 12295,
		// Token: 0x04001F1A RID: 7962
		MaxConnections = 2147483647,
		// Token: 0x04001F1B RID: 7963
		IPOptions = 1,
		// Token: 0x04001F1C RID: 7964
		HeaderIncluded,
		// Token: 0x04001F1D RID: 7965
		TypeOfService,
		// Token: 0x04001F1E RID: 7966
		IpTimeToLive,
		// Token: 0x04001F1F RID: 7967
		MulticastInterface = 9,
		// Token: 0x04001F20 RID: 7968
		MulticastTimeToLive,
		// Token: 0x04001F21 RID: 7969
		MulticastLoopback,
		// Token: 0x04001F22 RID: 7970
		AddMembership,
		// Token: 0x04001F23 RID: 7971
		DropMembership,
		// Token: 0x04001F24 RID: 7972
		DontFragment,
		// Token: 0x04001F25 RID: 7973
		AddSourceMembership,
		// Token: 0x04001F26 RID: 7974
		DropSourceMembership,
		// Token: 0x04001F27 RID: 7975
		BlockSource,
		// Token: 0x04001F28 RID: 7976
		UnblockSource,
		// Token: 0x04001F29 RID: 7977
		PacketInformation,
		// Token: 0x04001F2A RID: 7978
		HopLimit = 21,
		// Token: 0x04001F2B RID: 7979
		IPProtectionLevel = 23,
		// Token: 0x04001F2C RID: 7980
		IPv6Only = 27,
		// Token: 0x04001F2D RID: 7981
		NoDelay = 1,
		// Token: 0x04001F2E RID: 7982
		BsdUrgent,
		// Token: 0x04001F2F RID: 7983
		Expedited = 2,
		// Token: 0x04001F30 RID: 7984
		NoChecksum = 1,
		// Token: 0x04001F31 RID: 7985
		ChecksumCoverage = 20,
		// Token: 0x04001F32 RID: 7986
		UpdateAcceptContext = 28683,
		// Token: 0x04001F33 RID: 7987
		UpdateConnectContext = 28688
	}
}
