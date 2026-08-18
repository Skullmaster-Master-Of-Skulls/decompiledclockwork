using System;

namespace System.Net.Sockets
{
	// Token: 0x020005AF RID: 1455
	public enum ProtocolType
	{
		// Token: 0x04002AFD RID: 11005
		IP,
		// Token: 0x04002AFE RID: 11006
		IPv6HopByHopOptions = 0,
		// Token: 0x04002AFF RID: 11007
		Icmp,
		// Token: 0x04002B00 RID: 11008
		Igmp,
		// Token: 0x04002B01 RID: 11009
		Ggp,
		// Token: 0x04002B02 RID: 11010
		IPv4,
		// Token: 0x04002B03 RID: 11011
		Tcp = 6,
		// Token: 0x04002B04 RID: 11012
		Pup = 12,
		// Token: 0x04002B05 RID: 11013
		Udp = 17,
		// Token: 0x04002B06 RID: 11014
		Idp = 22,
		// Token: 0x04002B07 RID: 11015
		IPv6 = 41,
		// Token: 0x04002B08 RID: 11016
		IPv6RoutingHeader = 43,
		// Token: 0x04002B09 RID: 11017
		IPv6FragmentHeader,
		// Token: 0x04002B0A RID: 11018
		IPSecEncapsulatingSecurityPayload = 50,
		// Token: 0x04002B0B RID: 11019
		IPSecAuthenticationHeader,
		// Token: 0x04002B0C RID: 11020
		IcmpV6 = 58,
		// Token: 0x04002B0D RID: 11021
		IPv6NoNextHeader,
		// Token: 0x04002B0E RID: 11022
		IPv6DestinationOptions,
		// Token: 0x04002B0F RID: 11023
		ND = 77,
		// Token: 0x04002B10 RID: 11024
		Raw = 255,
		// Token: 0x04002B11 RID: 11025
		Unspecified = 0,
		// Token: 0x04002B12 RID: 11026
		Ipx = 1000,
		// Token: 0x04002B13 RID: 11027
		Spx = 1256,
		// Token: 0x04002B14 RID: 11028
		SpxII,
		// Token: 0x04002B15 RID: 11029
		Unknown = -1
	}
}
