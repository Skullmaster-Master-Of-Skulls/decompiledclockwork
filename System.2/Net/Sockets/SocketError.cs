using System;

namespace System.Net.Sockets
{
	// Token: 0x0200037D RID: 893
	[__DynamicallyInvokable]
	public enum SocketError
	{
		// Token: 0x04001EC5 RID: 7877
		[__DynamicallyInvokable]
		Success,
		// Token: 0x04001EC6 RID: 7878
		[__DynamicallyInvokable]
		SocketError = -1,
		// Token: 0x04001EC7 RID: 7879
		[__DynamicallyInvokable]
		Interrupted = 10004,
		// Token: 0x04001EC8 RID: 7880
		[__DynamicallyInvokable]
		AccessDenied = 10013,
		// Token: 0x04001EC9 RID: 7881
		[__DynamicallyInvokable]
		Fault,
		// Token: 0x04001ECA RID: 7882
		[__DynamicallyInvokable]
		InvalidArgument = 10022,
		// Token: 0x04001ECB RID: 7883
		[__DynamicallyInvokable]
		TooManyOpenSockets = 10024,
		// Token: 0x04001ECC RID: 7884
		[__DynamicallyInvokable]
		WouldBlock = 10035,
		// Token: 0x04001ECD RID: 7885
		[__DynamicallyInvokable]
		InProgress,
		// Token: 0x04001ECE RID: 7886
		[__DynamicallyInvokable]
		AlreadyInProgress,
		// Token: 0x04001ECF RID: 7887
		[__DynamicallyInvokable]
		NotSocket,
		// Token: 0x04001ED0 RID: 7888
		[__DynamicallyInvokable]
		DestinationAddressRequired,
		// Token: 0x04001ED1 RID: 7889
		[__DynamicallyInvokable]
		MessageSize,
		// Token: 0x04001ED2 RID: 7890
		[__DynamicallyInvokable]
		ProtocolType,
		// Token: 0x04001ED3 RID: 7891
		[__DynamicallyInvokable]
		ProtocolOption,
		// Token: 0x04001ED4 RID: 7892
		[__DynamicallyInvokable]
		ProtocolNotSupported,
		// Token: 0x04001ED5 RID: 7893
		[__DynamicallyInvokable]
		SocketNotSupported,
		// Token: 0x04001ED6 RID: 7894
		[__DynamicallyInvokable]
		OperationNotSupported,
		// Token: 0x04001ED7 RID: 7895
		[__DynamicallyInvokable]
		ProtocolFamilyNotSupported,
		// Token: 0x04001ED8 RID: 7896
		[__DynamicallyInvokable]
		AddressFamilyNotSupported,
		// Token: 0x04001ED9 RID: 7897
		[__DynamicallyInvokable]
		AddressAlreadyInUse,
		// Token: 0x04001EDA RID: 7898
		[__DynamicallyInvokable]
		AddressNotAvailable,
		// Token: 0x04001EDB RID: 7899
		[__DynamicallyInvokable]
		NetworkDown,
		// Token: 0x04001EDC RID: 7900
		[__DynamicallyInvokable]
		NetworkUnreachable,
		// Token: 0x04001EDD RID: 7901
		[__DynamicallyInvokable]
		NetworkReset,
		// Token: 0x04001EDE RID: 7902
		[__DynamicallyInvokable]
		ConnectionAborted,
		// Token: 0x04001EDF RID: 7903
		[__DynamicallyInvokable]
		ConnectionReset,
		// Token: 0x04001EE0 RID: 7904
		[__DynamicallyInvokable]
		NoBufferSpaceAvailable,
		// Token: 0x04001EE1 RID: 7905
		[__DynamicallyInvokable]
		IsConnected,
		// Token: 0x04001EE2 RID: 7906
		[__DynamicallyInvokable]
		NotConnected,
		// Token: 0x04001EE3 RID: 7907
		[__DynamicallyInvokable]
		Shutdown,
		// Token: 0x04001EE4 RID: 7908
		[__DynamicallyInvokable]
		TimedOut = 10060,
		// Token: 0x04001EE5 RID: 7909
		[__DynamicallyInvokable]
		ConnectionRefused,
		// Token: 0x04001EE6 RID: 7910
		[__DynamicallyInvokable]
		HostDown = 10064,
		// Token: 0x04001EE7 RID: 7911
		[__DynamicallyInvokable]
		HostUnreachable,
		// Token: 0x04001EE8 RID: 7912
		[__DynamicallyInvokable]
		ProcessLimit = 10067,
		// Token: 0x04001EE9 RID: 7913
		[__DynamicallyInvokable]
		SystemNotReady = 10091,
		// Token: 0x04001EEA RID: 7914
		[__DynamicallyInvokable]
		VersionNotSupported,
		// Token: 0x04001EEB RID: 7915
		[__DynamicallyInvokable]
		NotInitialized,
		// Token: 0x04001EEC RID: 7916
		[__DynamicallyInvokable]
		Disconnecting = 10101,
		// Token: 0x04001EED RID: 7917
		[__DynamicallyInvokable]
		TypeNotFound = 10109,
		// Token: 0x04001EEE RID: 7918
		[__DynamicallyInvokable]
		HostNotFound = 11001,
		// Token: 0x04001EEF RID: 7919
		[__DynamicallyInvokable]
		TryAgain,
		// Token: 0x04001EF0 RID: 7920
		[__DynamicallyInvokable]
		NoRecovery,
		// Token: 0x04001EF1 RID: 7921
		[__DynamicallyInvokable]
		NoData,
		// Token: 0x04001EF2 RID: 7922
		[__DynamicallyInvokable]
		IOPending = 997,
		// Token: 0x04001EF3 RID: 7923
		[__DynamicallyInvokable]
		OperationAborted = 995
	}
}
