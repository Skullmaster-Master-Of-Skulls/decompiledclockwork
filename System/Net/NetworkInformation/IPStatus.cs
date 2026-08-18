using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E2 RID: 1506
	public enum IPStatus
	{
		// Token: 0x04002CAB RID: 11435
		Success,
		// Token: 0x04002CAC RID: 11436
		DestinationNetworkUnreachable = 11002,
		// Token: 0x04002CAD RID: 11437
		DestinationHostUnreachable,
		// Token: 0x04002CAE RID: 11438
		DestinationProtocolUnreachable,
		// Token: 0x04002CAF RID: 11439
		DestinationPortUnreachable,
		// Token: 0x04002CB0 RID: 11440
		DestinationProhibited = 11004,
		// Token: 0x04002CB1 RID: 11441
		NoResources = 11006,
		// Token: 0x04002CB2 RID: 11442
		BadOption,
		// Token: 0x04002CB3 RID: 11443
		HardwareError,
		// Token: 0x04002CB4 RID: 11444
		PacketTooBig,
		// Token: 0x04002CB5 RID: 11445
		TimedOut,
		// Token: 0x04002CB6 RID: 11446
		BadRoute = 11012,
		// Token: 0x04002CB7 RID: 11447
		TtlExpired,
		// Token: 0x04002CB8 RID: 11448
		TtlReassemblyTimeExceeded,
		// Token: 0x04002CB9 RID: 11449
		ParameterProblem,
		// Token: 0x04002CBA RID: 11450
		SourceQuench,
		// Token: 0x04002CBB RID: 11451
		BadDestination = 11018,
		// Token: 0x04002CBC RID: 11452
		DestinationUnreachable = 11040,
		// Token: 0x04002CBD RID: 11453
		TimeExceeded,
		// Token: 0x04002CBE RID: 11454
		BadHeader,
		// Token: 0x04002CBF RID: 11455
		UnrecognizedNextHeader,
		// Token: 0x04002CC0 RID: 11456
		IcmpError,
		// Token: 0x04002CC1 RID: 11457
		DestinationScopeMismatch,
		// Token: 0x04002CC2 RID: 11458
		Unknown = -1
	}
}
