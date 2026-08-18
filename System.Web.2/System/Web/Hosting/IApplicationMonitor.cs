using System;

namespace System.Web.Hosting
{
	// Token: 0x02000782 RID: 1922
	public interface IApplicationMonitor : IDisposable
	{
		// Token: 0x06005C41 RID: 23617
		void Start();

		// Token: 0x06005C42 RID: 23618
		void Stop();
	}
}
