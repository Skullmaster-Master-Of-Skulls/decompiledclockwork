using System;

namespace System.Net.WebSockets
{
	// Token: 0x02000232 RID: 562
	public enum WebSocketCloseStatus
	{
		// Token: 0x0400168E RID: 5774
		NormalClosure = 1000,
		// Token: 0x0400168F RID: 5775
		EndpointUnavailable,
		// Token: 0x04001690 RID: 5776
		ProtocolError,
		// Token: 0x04001691 RID: 5777
		InvalidMessageType,
		// Token: 0x04001692 RID: 5778
		Empty = 1005,
		// Token: 0x04001693 RID: 5779
		InvalidPayloadData = 1007,
		// Token: 0x04001694 RID: 5780
		PolicyViolation,
		// Token: 0x04001695 RID: 5781
		MessageTooBig,
		// Token: 0x04001696 RID: 5782
		MandatoryExtension,
		// Token: 0x04001697 RID: 5783
		InternalServerError
	}
}
