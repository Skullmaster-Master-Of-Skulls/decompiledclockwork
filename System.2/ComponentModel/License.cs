using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200057C RID: 1404
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class License : IDisposable
	{
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060033FB RID: 13307
		public abstract string LicenseKey { get; }

		// Token: 0x060033FC RID: 13308
		public abstract void Dispose();
	}
}
