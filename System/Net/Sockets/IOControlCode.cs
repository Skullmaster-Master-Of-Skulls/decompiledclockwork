using System;

namespace System.Net.Sockets
{
	// Token: 0x020005AA RID: 1450
	public enum IOControlCode : long
	{
		// Token: 0x04002AB3 RID: 10931
		AsyncIO = 2147772029L,
		// Token: 0x04002AB4 RID: 10932
		NonBlockingIO,
		// Token: 0x04002AB5 RID: 10933
		DataToRead = 1074030207L,
		// Token: 0x04002AB6 RID: 10934
		OobDataRead = 1074033415L,
		// Token: 0x04002AB7 RID: 10935
		AssociateHandle = 2281701377L,
		// Token: 0x04002AB8 RID: 10936
		EnableCircularQueuing = 671088642L,
		// Token: 0x04002AB9 RID: 10937
		Flush = 671088644L,
		// Token: 0x04002ABA RID: 10938
		GetBroadcastAddress = 1207959557L,
		// Token: 0x04002ABB RID: 10939
		GetExtensionFunctionPointer = 3355443206L,
		// Token: 0x04002ABC RID: 10940
		GetQos,
		// Token: 0x04002ABD RID: 10941
		GetGroupQos,
		// Token: 0x04002ABE RID: 10942
		MultipointLoopback = 2281701385L,
		// Token: 0x04002ABF RID: 10943
		MulticastScope,
		// Token: 0x04002AC0 RID: 10944
		SetQos,
		// Token: 0x04002AC1 RID: 10945
		SetGroupQos,
		// Token: 0x04002AC2 RID: 10946
		TranslateHandle = 3355443213L,
		// Token: 0x04002AC3 RID: 10947
		RoutingInterfaceQuery = 3355443220L,
		// Token: 0x04002AC4 RID: 10948
		RoutingInterfaceChange = 2281701397L,
		// Token: 0x04002AC5 RID: 10949
		AddressListQuery = 1207959574L,
		// Token: 0x04002AC6 RID: 10950
		AddressListChange = 671088663L,
		// Token: 0x04002AC7 RID: 10951
		QueryTargetPnpHandle = 1207959576L,
		// Token: 0x04002AC8 RID: 10952
		NamespaceChange = 2281701401L,
		// Token: 0x04002AC9 RID: 10953
		AddressListSort = 3355443225L,
		// Token: 0x04002ACA RID: 10954
		ReceiveAll = 2550136833L,
		// Token: 0x04002ACB RID: 10955
		ReceiveAllMulticast,
		// Token: 0x04002ACC RID: 10956
		ReceiveAllIgmpMulticast,
		// Token: 0x04002ACD RID: 10957
		KeepAliveValues,
		// Token: 0x04002ACE RID: 10958
		AbsorbRouterAlert,
		// Token: 0x04002ACF RID: 10959
		UnicastInterface,
		// Token: 0x04002AD0 RID: 10960
		LimitBroadcasts,
		// Token: 0x04002AD1 RID: 10961
		BindToInterface,
		// Token: 0x04002AD2 RID: 10962
		MulticastInterface,
		// Token: 0x04002AD3 RID: 10963
		AddMulticastGroupOnInterface,
		// Token: 0x04002AD4 RID: 10964
		DeleteMulticastGroupFromInterface
	}
}
