using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A9 RID: 681
	public enum IPStatus
	{
		// Token: 0x040018F8 RID: 6392
		Success,
		// Token: 0x040018F9 RID: 6393
		DestinationNetworkUnreachable = 11002,
		// Token: 0x040018FA RID: 6394
		DestinationHostUnreachable,
		// Token: 0x040018FB RID: 6395
		DestinationProtocolUnreachable,
		// Token: 0x040018FC RID: 6396
		DestinationPortUnreachable,
		// Token: 0x040018FD RID: 6397
		DestinationProhibited = 11004,
		// Token: 0x040018FE RID: 6398
		NoResources = 11006,
		// Token: 0x040018FF RID: 6399
		BadOption,
		// Token: 0x04001900 RID: 6400
		HardwareError,
		// Token: 0x04001901 RID: 6401
		PacketTooBig,
		// Token: 0x04001902 RID: 6402
		TimedOut,
		// Token: 0x04001903 RID: 6403
		BadRoute = 11012,
		// Token: 0x04001904 RID: 6404
		TtlExpired,
		// Token: 0x04001905 RID: 6405
		TtlReassemblyTimeExceeded,
		// Token: 0x04001906 RID: 6406
		ParameterProblem,
		// Token: 0x04001907 RID: 6407
		SourceQuench,
		// Token: 0x04001908 RID: 6408
		BadDestination = 11018,
		// Token: 0x04001909 RID: 6409
		DestinationUnreachable = 11040,
		// Token: 0x0400190A RID: 6410
		TimeExceeded,
		// Token: 0x0400190B RID: 6411
		BadHeader,
		// Token: 0x0400190C RID: 6412
		UnrecognizedNextHeader,
		// Token: 0x0400190D RID: 6413
		IcmpError,
		// Token: 0x0400190E RID: 6414
		DestinationScopeMismatch,
		// Token: 0x0400190F RID: 6415
		Unknown = -1
	}
}
