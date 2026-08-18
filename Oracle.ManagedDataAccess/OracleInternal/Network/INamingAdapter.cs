using System;
using System.Collections;

namespace OracleInternal.Network
{
	// Token: 0x0200015E RID: 350
	internal interface INamingAdapter
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000DE2 RID: 3554
		string ID { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000DE3 RID: 3555
		Hashtable Map { get; }

		// Token: 0x06000DE4 RID: 3556
		string Resolve(string TNSname, out ConnectionOption CO, string InstanceName = null);

		// Token: 0x06000DE5 RID: 3557
		void Refresh();
	}
}
