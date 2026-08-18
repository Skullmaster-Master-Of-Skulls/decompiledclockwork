using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D1 RID: 209
	public enum DisconnectReason
	{
		// Token: 0x04000388 RID: 904
		None,
		// Token: 0x04000389 RID: 905
		HostNotAllowedToConnect,
		// Token: 0x0400038A RID: 906
		ProtocolError,
		// Token: 0x0400038B RID: 907
		KeyExchangeFailed,
		// Token: 0x0400038C RID: 908
		Reserved,
		// Token: 0x0400038D RID: 909
		MacError,
		// Token: 0x0400038E RID: 910
		CompressionError,
		// Token: 0x0400038F RID: 911
		ServiceNotAvailable,
		// Token: 0x04000390 RID: 912
		ProtocolVersionNotSupported,
		// Token: 0x04000391 RID: 913
		HostKeyNotVerifiable,
		// Token: 0x04000392 RID: 914
		ConnectionLost,
		// Token: 0x04000393 RID: 915
		ByApplication,
		// Token: 0x04000394 RID: 916
		TooManyConnections,
		// Token: 0x04000395 RID: 917
		AuthenticationCanceledByUser,
		// Token: 0x04000396 RID: 918
		NoMoreAuthenticationMethodsAvailable,
		// Token: 0x04000397 RID: 919
		IllegalUserName
	}
}
