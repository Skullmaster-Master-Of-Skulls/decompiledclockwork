using System;

namespace System.Net.Sockets
{
	// Token: 0x0200036B RID: 875
	public enum IOControlCode : long
	{
		// Token: 0x04001DBF RID: 7615
		AsyncIO = 2147772029L,
		// Token: 0x04001DC0 RID: 7616
		NonBlockingIO,
		// Token: 0x04001DC1 RID: 7617
		DataToRead = 1074030207L,
		// Token: 0x04001DC2 RID: 7618
		OobDataRead = 1074033415L,
		// Token: 0x04001DC3 RID: 7619
		AssociateHandle = 2281701377L,
		// Token: 0x04001DC4 RID: 7620
		EnableCircularQueuing = 671088642L,
		// Token: 0x04001DC5 RID: 7621
		Flush = 671088644L,
		// Token: 0x04001DC6 RID: 7622
		GetBroadcastAddress = 1207959557L,
		// Token: 0x04001DC7 RID: 7623
		GetExtensionFunctionPointer = 3355443206L,
		// Token: 0x04001DC8 RID: 7624
		GetQos,
		// Token: 0x04001DC9 RID: 7625
		GetGroupQos,
		// Token: 0x04001DCA RID: 7626
		MultipointLoopback = 2281701385L,
		// Token: 0x04001DCB RID: 7627
		MulticastScope,
		// Token: 0x04001DCC RID: 7628
		SetQos,
		// Token: 0x04001DCD RID: 7629
		SetGroupQos,
		// Token: 0x04001DCE RID: 7630
		TranslateHandle = 3355443213L,
		// Token: 0x04001DCF RID: 7631
		RoutingInterfaceQuery = 3355443220L,
		// Token: 0x04001DD0 RID: 7632
		RoutingInterfaceChange = 2281701397L,
		// Token: 0x04001DD1 RID: 7633
		AddressListQuery = 1207959574L,
		// Token: 0x04001DD2 RID: 7634
		AddressListChange = 671088663L,
		// Token: 0x04001DD3 RID: 7635
		QueryTargetPnpHandle = 1207959576L,
		// Token: 0x04001DD4 RID: 7636
		NamespaceChange = 2281701401L,
		// Token: 0x04001DD5 RID: 7637
		AddressListSort = 3355443225L,
		// Token: 0x04001DD6 RID: 7638
		ReceiveAll = 2550136833L,
		// Token: 0x04001DD7 RID: 7639
		ReceiveAllMulticast,
		// Token: 0x04001DD8 RID: 7640
		ReceiveAllIgmpMulticast,
		// Token: 0x04001DD9 RID: 7641
		KeepAliveValues,
		// Token: 0x04001DDA RID: 7642
		AbsorbRouterAlert,
		// Token: 0x04001DDB RID: 7643
		UnicastInterface,
		// Token: 0x04001DDC RID: 7644
		LimitBroadcasts,
		// Token: 0x04001DDD RID: 7645
		BindToInterface,
		// Token: 0x04001DDE RID: 7646
		MulticastInterface,
		// Token: 0x04001DDF RID: 7647
		AddMulticastGroupOnInterface,
		// Token: 0x04001DE0 RID: 7648
		DeleteMulticastGroupFromInterface
	}
}
