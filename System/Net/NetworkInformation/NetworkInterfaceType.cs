using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005D9 RID: 1497
	public enum NetworkInterfaceType
	{
		// Token: 0x04002C73 RID: 11379
		Unknown = 1,
		// Token: 0x04002C74 RID: 11380
		Ethernet = 6,
		// Token: 0x04002C75 RID: 11381
		TokenRing = 9,
		// Token: 0x04002C76 RID: 11382
		Fddi = 15,
		// Token: 0x04002C77 RID: 11383
		BasicIsdn = 20,
		// Token: 0x04002C78 RID: 11384
		PrimaryIsdn,
		// Token: 0x04002C79 RID: 11385
		Ppp = 23,
		// Token: 0x04002C7A RID: 11386
		Loopback,
		// Token: 0x04002C7B RID: 11387
		Ethernet3Megabit = 26,
		// Token: 0x04002C7C RID: 11388
		Slip = 28,
		// Token: 0x04002C7D RID: 11389
		Atm = 37,
		// Token: 0x04002C7E RID: 11390
		GenericModem = 48,
		// Token: 0x04002C7F RID: 11391
		FastEthernetT = 62,
		// Token: 0x04002C80 RID: 11392
		Isdn,
		// Token: 0x04002C81 RID: 11393
		FastEthernetFx = 69,
		// Token: 0x04002C82 RID: 11394
		Wireless80211 = 71,
		// Token: 0x04002C83 RID: 11395
		AsymmetricDsl = 94,
		// Token: 0x04002C84 RID: 11396
		RateAdaptDsl,
		// Token: 0x04002C85 RID: 11397
		SymmetricDsl,
		// Token: 0x04002C86 RID: 11398
		VeryHighSpeedDsl,
		// Token: 0x04002C87 RID: 11399
		IPOverAtm = 114,
		// Token: 0x04002C88 RID: 11400
		GigabitEthernet = 117,
		// Token: 0x04002C89 RID: 11401
		Tunnel = 131,
		// Token: 0x04002C8A RID: 11402
		MultiRateSymmetricDsl = 143,
		// Token: 0x04002C8B RID: 11403
		HighPerformanceSerialBus
	}
}
