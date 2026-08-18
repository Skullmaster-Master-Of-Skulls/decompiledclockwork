using System;

namespace System.Net.Sockets
{
	// Token: 0x020005C3 RID: 1475
	public enum SocketError
	{
		// Token: 0x04002BB6 RID: 11190
		Success,
		// Token: 0x04002BB7 RID: 11191
		SocketError = -1,
		// Token: 0x04002BB8 RID: 11192
		Interrupted = 10004,
		// Token: 0x04002BB9 RID: 11193
		AccessDenied = 10013,
		// Token: 0x04002BBA RID: 11194
		Fault,
		// Token: 0x04002BBB RID: 11195
		InvalidArgument = 10022,
		// Token: 0x04002BBC RID: 11196
		TooManyOpenSockets = 10024,
		// Token: 0x04002BBD RID: 11197
		WouldBlock = 10035,
		// Token: 0x04002BBE RID: 11198
		InProgress,
		// Token: 0x04002BBF RID: 11199
		AlreadyInProgress,
		// Token: 0x04002BC0 RID: 11200
		NotSocket,
		// Token: 0x04002BC1 RID: 11201
		DestinationAddressRequired,
		// Token: 0x04002BC2 RID: 11202
		MessageSize,
		// Token: 0x04002BC3 RID: 11203
		ProtocolType,
		// Token: 0x04002BC4 RID: 11204
		ProtocolOption,
		// Token: 0x04002BC5 RID: 11205
		ProtocolNotSupported,
		// Token: 0x04002BC6 RID: 11206
		SocketNotSupported,
		// Token: 0x04002BC7 RID: 11207
		OperationNotSupported,
		// Token: 0x04002BC8 RID: 11208
		ProtocolFamilyNotSupported,
		// Token: 0x04002BC9 RID: 11209
		AddressFamilyNotSupported,
		// Token: 0x04002BCA RID: 11210
		AddressAlreadyInUse,
		// Token: 0x04002BCB RID: 11211
		AddressNotAvailable,
		// Token: 0x04002BCC RID: 11212
		NetworkDown,
		// Token: 0x04002BCD RID: 11213
		NetworkUnreachable,
		// Token: 0x04002BCE RID: 11214
		NetworkReset,
		// Token: 0x04002BCF RID: 11215
		ConnectionAborted,
		// Token: 0x04002BD0 RID: 11216
		ConnectionReset,
		// Token: 0x04002BD1 RID: 11217
		NoBufferSpaceAvailable,
		// Token: 0x04002BD2 RID: 11218
		IsConnected,
		// Token: 0x04002BD3 RID: 11219
		NotConnected,
		// Token: 0x04002BD4 RID: 11220
		Shutdown,
		// Token: 0x04002BD5 RID: 11221
		TimedOut = 10060,
		// Token: 0x04002BD6 RID: 11222
		ConnectionRefused,
		// Token: 0x04002BD7 RID: 11223
		HostDown = 10064,
		// Token: 0x04002BD8 RID: 11224
		HostUnreachable,
		// Token: 0x04002BD9 RID: 11225
		ProcessLimit = 10067,
		// Token: 0x04002BDA RID: 11226
		SystemNotReady = 10091,
		// Token: 0x04002BDB RID: 11227
		VersionNotSupported,
		// Token: 0x04002BDC RID: 11228
		NotInitialized,
		// Token: 0x04002BDD RID: 11229
		Disconnecting = 10101,
		// Token: 0x04002BDE RID: 11230
		TypeNotFound = 10109,
		// Token: 0x04002BDF RID: 11231
		HostNotFound = 11001,
		// Token: 0x04002BE0 RID: 11232
		TryAgain,
		// Token: 0x04002BE1 RID: 11233
		NoRecovery,
		// Token: 0x04002BE2 RID: 11234
		NoData,
		// Token: 0x04002BE3 RID: 11235
		IOPending = 997,
		// Token: 0x04002BE4 RID: 11236
		OperationAborted = 995
	}
}
