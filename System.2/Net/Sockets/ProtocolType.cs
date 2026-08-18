using System;

namespace System.Net.Sockets
{
	// Token: 0x02000372 RID: 882
	public enum ProtocolType
	{
		// Token: 0x04001E16 RID: 7702
		IP,
		// Token: 0x04001E17 RID: 7703
		IPv6HopByHopOptions = 0,
		// Token: 0x04001E18 RID: 7704
		Icmp,
		// Token: 0x04001E19 RID: 7705
		Igmp,
		// Token: 0x04001E1A RID: 7706
		Ggp,
		// Token: 0x04001E1B RID: 7707
		IPv4,
		// Token: 0x04001E1C RID: 7708
		Tcp = 6,
		// Token: 0x04001E1D RID: 7709
		Pup = 12,
		// Token: 0x04001E1E RID: 7710
		Udp = 17,
		// Token: 0x04001E1F RID: 7711
		Idp = 22,
		// Token: 0x04001E20 RID: 7712
		IPv6 = 41,
		// Token: 0x04001E21 RID: 7713
		IPv6RoutingHeader = 43,
		// Token: 0x04001E22 RID: 7714
		IPv6FragmentHeader,
		// Token: 0x04001E23 RID: 7715
		IPSecEncapsulatingSecurityPayload = 50,
		// Token: 0x04001E24 RID: 7716
		IPSecAuthenticationHeader,
		// Token: 0x04001E25 RID: 7717
		IcmpV6 = 58,
		// Token: 0x04001E26 RID: 7718
		IPv6NoNextHeader,
		// Token: 0x04001E27 RID: 7719
		IPv6DestinationOptions,
		// Token: 0x04001E28 RID: 7720
		ND = 77,
		// Token: 0x04001E29 RID: 7721
		Raw = 255,
		// Token: 0x04001E2A RID: 7722
		Unspecified = 0,
		// Token: 0x04001E2B RID: 7723
		Ipx = 1000,
		// Token: 0x04001E2C RID: 7724
		Spx = 1256,
		// Token: 0x04001E2D RID: 7725
		SpxII,
		// Token: 0x04001E2E RID: 7726
		Unknown = -1
	}
}
