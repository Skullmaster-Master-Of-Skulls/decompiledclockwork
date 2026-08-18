using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000188 RID: 392
	internal class WebProxyData
	{
		// Token: 0x04001284 RID: 4740
		internal bool bypassOnLocal;

		// Token: 0x04001285 RID: 4741
		internal bool automaticallyDetectSettings;

		// Token: 0x04001286 RID: 4742
		internal Uri proxyAddress;

		// Token: 0x04001287 RID: 4743
		internal Hashtable proxyHostAddresses;

		// Token: 0x04001288 RID: 4744
		internal Uri scriptLocation;

		// Token: 0x04001289 RID: 4745
		internal ArrayList bypassList;
	}
}
