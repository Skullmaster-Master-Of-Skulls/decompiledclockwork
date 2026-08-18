using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000579 RID: 1401
	public interface ISynchronizeInvoke
	{
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060033F0 RID: 13296
		bool InvokeRequired { get; }

		// Token: 0x060033F1 RID: 13297
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		IAsyncResult BeginInvoke(Delegate method, object[] args);

		// Token: 0x060033F2 RID: 13298
		object EndInvoke(IAsyncResult result);

		// Token: 0x060033F3 RID: 13299
		object Invoke(Delegate method, object[] args);
	}
}
