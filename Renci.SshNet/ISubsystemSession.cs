using System;
using System.Threading;

namespace Renci.SshNet
{
	// Token: 0x02000015 RID: 21
	internal interface ISubsystemSession : IDisposable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FD RID: 253
		bool IsOpen { get; }

		// Token: 0x060000FE RID: 254
		void Connect();

		// Token: 0x060000FF RID: 255
		void Disconnect();

		// Token: 0x06000100 RID: 256
		void WaitOnHandle(WaitHandle waitHandle, TimeSpan operationTimeout);
	}
}
